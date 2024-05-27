using SProjectClient.NET.IO;
using SProjectServer.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SProjectClient
{
    public partial class Form1 : Form
    {
        public PacketReader _packetReader;
        private static TcpClient _client;

        private string username;
        private string password;
        private string email;
        private string uid;

        private BigInteger p;
        private BigInteger g;
        private BigInteger clientPrivateKey;
        private BigInteger clientPublicKey;
        private BigInteger serverPublicKey;
        private BigInteger sharedKey;
        private byte[] masterKey;

        public Form1()
        {
            InitializeComponent();
            sendButton.Enabled = false;
            sendButton2.Enabled = false;

            loginButton.Enabled = false;
            loginEmailBox.Enabled = false;
            loginPasswordBox.Enabled = false;
            registerEmailBox.Enabled = false;
            registerUsernameBox.Enabled = false;
            registerPasswordBox.Enabled = false;
            registerButton.Enabled = false;
            users.Enabled = false;

            _client = new TcpClient();
        }

        private void connectServerButton_Click(object sender, EventArgs e)
        {
            try
            {
                _client.Connect(ipBox.Text, int.Parse(portBox.Text));
                _packetReader = new PacketReader(_client.GetStream());
                process();

                connectServerButton.Enabled = false;
                ipBox.Enabled = false;
                portBox.Enabled = false;
            }
            catch(Exception ex)
            {
                BeginInvoke(new Action(() =>
                {
                    console.Text += "Connection failed: "+ ex.Message +"\n";
                }));
            }
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            email = loginEmailBox.Text;
            password = loginPasswordBox.Text;
            loginButton.Enabled = false;
            loginEmailBox.Enabled = false;
            loginPasswordBox.Enabled = false;
            registerEmailBox.Enabled = false;
            registerUsernameBox.Enabled = false;
            registerPasswordBox.Enabled = false;
            registerButton.Enabled = false;
            PacketBuilder loginPacket = new PacketBuilder();
            loginPacket.WriteOpCode(1);
            loginPacket.WriteEncryptedMessage(loginEmailBox.Text, masterKey);
            loginPacket.WriteEncryptedMessage(AesUtil.ComputeSha256Hash(loginPasswordBox.Text), masterKey);
            _client.Client.Send(loginPacket.GetPacketBytes());
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            username = registerUsernameBox.Text;
            email = registerEmailBox.Text;
            password = registerPasswordBox.Text;
            loginButton.Enabled = false;
            loginEmailBox.Enabled = false;
            loginPasswordBox.Enabled = false;
            registerEmailBox.Enabled = false;
            registerUsernameBox.Enabled = false;
            registerPasswordBox.Enabled = false;
            registerButton.Enabled = false;
            PacketBuilder registerPacket = new PacketBuilder();
            registerPacket.WriteOpCode(2);
            registerPacket.WriteEncryptedMessage(registerUsernameBox.Text, masterKey);
            registerPacket.WriteEncryptedMessage(registerEmailBox.Text, masterKey);
            registerPacket.WriteEncryptedMessage(AesUtil.ComputeSha256Hash(registerPasswordBox.Text), masterKey);
            _client.Client.Send(registerPacket.GetPacketBytes());
        }

        private void process()
        {
            Task.Run(() =>
            {
                try
                {
                    while (true)
                    {
                        var opcode = _packetReader.ReadByte();
                        switch (opcode)
                        {
                            case 0:
                                p = BigInteger.Parse(_packetReader.ReadMessage());
                                g = BigInteger.Parse(_packetReader.ReadMessage());
                                serverPublicKey = BigInteger.Parse(_packetReader.ReadMessage());

                                Random rnd = new Random();

                                clientPrivateKey = rnd.Next(100000, 999999);

                                clientPublicKey = BigInteger.ModPow(p, clientPrivateKey, g);
                                PacketBuilder keyPacket = new PacketBuilder();
                                keyPacket.WriteOpCode(0);
                                keyPacket.WriteMessage(clientPublicKey.ToString());
                                _client.Client.Send(keyPacket.GetPacketBytes());

                                sharedKey = BigInteger.ModPow(serverPublicKey, clientPrivateKey, g);
                                byte[] doubleBytes = sharedKey.ToByteArray();
                                using (SHA256 sha256 = SHA256.Create())
                                {
                                    masterKey = sha256.ComputeHash(doubleBytes);
                                }

                                BeginInvoke(new Action(() =>
                                {
                                    console.Text += "P: " + p + "\n";
                                    console.Text += "G: " + g + "\n";
                                    console.Text += "Server public key: " + serverPublicKey + "\n";
                                    console.Text += "Client public key: " + clientPublicKey + "\n";
                                    console.Text += "Client private key: " + clientPrivateKey + "\n";
                                    console.Text += "Shared key: " + sharedKey + "\n";
                                    loginButton.Enabled = true;
                                    loginEmailBox.Enabled = true;
                                    loginPasswordBox.Enabled = true;
                                    registerEmailBox.Enabled = true;
                                    registerUsernameBox.Enabled = true;
                                    registerPasswordBox.Enabled = true;
                                    registerButton.Enabled = true;
                                }));
                                break;
                            case 1:
                                var loginStatus = _packetReader.ReadEncryptedMessage(masterKey);
                                if (loginStatus == "true")
                                {
                                    uid = _packetReader.ReadEncryptedMessage(masterKey);
                                    username = _packetReader.ReadEncryptedMessage(masterKey);
                                    BeginInvoke(new Action(() =>
                                    {
                                        users.Enabled = true;
                                        console.Text += "Login successful\n";
                                    }));
                                }
                                else
                                {
                                    var loginErrorMessage = _packetReader.ReadEncryptedMessage(masterKey);
                                    this.BeginInvoke(new Action(() =>
                                    {
                                        loginButton.Enabled = true;
                                        loginEmailBox.Enabled = true;
                                        loginPasswordBox.Enabled = true;
                                        registerEmailBox.Enabled = true;
                                        registerUsernameBox.Enabled = true;
                                        registerPasswordBox.Enabled = true;
                                        registerButton.Enabled = true;
                                        console.Text += "Login failed\n";
                                        console.Text += loginErrorMessage + "\n";
                                    }));
                                    
                                }
                                break;
                            case 2:
                                var registerStatus = _packetReader.ReadEncryptedMessage(masterKey);
                                if (registerStatus == "true")
                                {
                                    uid = _packetReader.ReadEncryptedMessage(masterKey);
                                    BeginInvoke(new Action(() =>
                                    {
                                        users.Enabled = true;
                                        console.Text += "Register successful\n";
                                    }));
                                }
                                else
                                {
                                    var registerErrorMessage = _packetReader.ReadEncryptedMessage(masterKey);
                                    this.BeginInvoke(new Action(() =>
                                    {
                                        loginButton.Enabled = true;
                                        loginEmailBox.Enabled = true;
                                        loginPasswordBox.Enabled = true;
                                        registerEmailBox.Enabled = true;
                                        registerUsernameBox.Enabled = true;
                                        registerPasswordBox.Enabled = true;
                                        registerButton.Enabled = true;
                                        console.Text += "Register failed\n";
                                        console.Text += registerErrorMessage + "\n";
                                    }));
                                }
                                break;
                            case 3:
                                var connectedUser = _packetReader.ReadEncryptedMessage(masterKey);
                                var connectedUserUID = _packetReader.ReadEncryptedMessage(masterKey);
                                if (!users.Items.Contains(connectedUser))
                                {
                                    if (connectedUser != null || connectedUser != "")
                                    {
                                        users.Invoke(new Action(() =>
                                        {
                                            users.Items.Add(connectedUser);
                                        }));
                                        console.Invoke(new Action(() =>
                                        {
                                            console.Text += connectedUser + " connected, user ID: " + connectedUserUID + "\n";
                                        }));
                                    }
                                }
                                break;
                            case 4:
                                var disconnectedUser = _packetReader.ReadEncryptedMessage(masterKey);
                                var disconnectedUserUID = _packetReader.ReadEncryptedMessage(masterKey);
                                if (users.Items.Contains(disconnectedUser))
                                {
                                    users.Invoke(new Action(() =>
                                    {
                                        users.Items.Remove(disconnectedUser);
                                    }));
                                    console.Invoke(new Action(() =>
                                    {
                                        console.Text += disconnectedUser + " disconnected, userID: " + disconnectedUser + "\n";
                                    }));
                                }
                                break;
                            case 5:
                                var encryptedMessageUsername = _packetReader.ReadEncryptedMessage(masterKey);
                                var encryptedMessage = _packetReader.ReadEncryptedMessage(masterKey);
                                console.Invoke(new Action(() =>
                                {
                                    console.Text += encryptedMessageUsername + " -> " + username + " : " + encryptedMessage + "\n";
                                }));
                                break;
                            case 6:
                                var messageUsername = _packetReader.ReadMessage();
                                var message = _packetReader.ReadMessage();
                                console.Invoke(new Action(() =>
                                {
                                    console.Text += messageUsername + " -> " + username + " : " + message + "\n";
                                }));
                                break;
                            case 7:
                                var oldMessageSender = _packetReader.ReadEncryptedMessage(masterKey);
                                var oldMessageReceiver = _packetReader.ReadEncryptedMessage(masterKey);
                                var oldMessageText = _packetReader.ReadEncryptedMessage(masterKey);
                                console.Invoke(new Action(() =>
                                {
                                    console.Text += oldMessageSender + " -> " + oldMessageReceiver + " : " + oldMessageText + "\n";
                                }));
                                break;
                            default:
                                console.Invoke(new Action(() =>
                                {
                                    console.Text += "Unknown opcode received\n";
                                }));
                                break;
                        }
                    }
                }
                catch(Exception ex)
                {
                    BeginInvoke(new Action(() =>
                    {
                        console.Text += "Disconnected from server: " + ex.Message + "\n";
                        _client.Close();
                    }));
                }
            });
        }

        #region UI Bileşenleri
        private void sendButton_Click(object sender, EventArgs e)
        {
            if (users.SelectedItem != null)
            {
                console.Invoke(new Action(() =>
                {
                    console.Text += username + " -> " + users.SelectedItem + " : " + messageBox.Text + "\n";
                }));
                if (messageBox.Text.Length > 0)
                {
                    PacketBuilder pb = new PacketBuilder();
                    pb.WriteOpCode(5);
                    pb.WriteEncryptedMessage(users.SelectedItem.ToString(), masterKey);
                    pb.WriteEncryptedMessage(messageBox.Text, masterKey);
                    _client.Client.Send(pb.GetPacketBytes());
                }
                messageBox.Text = "";
                sendButton.Enabled = false;
                sendButton2.Enabled = false;
            }
        }
        private void sendButton2_Click(object sender, EventArgs e)
        {
            if (users.SelectedItems != null)
            {
                console.Invoke(new Action(() =>
                {
                    console.Text += username + " -> " + users.SelectedItem + " : " + messageBox.Text + "\n";
                }));
                if (messageBox.Text.Length > 0)
                {
                    PacketBuilder pb = new PacketBuilder();
                    pb.WriteOpCode(6);
                    pb.WriteMessage(users.SelectedItem.ToString());
                    pb.WriteMessage(messageBox.Text);
                    _client.Client.Send(pb.GetPacketBytes());
                }
                messageBox.Text = "";
                sendButton.Enabled = false;
                sendButton2.Enabled = false;
            }
        }


        private void ipBox_TextChanged(object sender, EventArgs e)
        {
            connectServerButton.Enabled = ipBox.Text.Length > 0 && portBox.Text.Length > 0;
        }

        private void portBox_TextChanged(object sender, EventArgs e)
        {
            connectServerButton.Enabled = ipBox.Text.Length > 0 && portBox.Text.Length > 0;
        }

        private void users_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(users.SelectedItem != null)
            {
                if(loginButton.Enabled == true && registerButton.Enabled == true)
                {
                    sendButton.Enabled = false;
                    sendButton2.Enabled = false;
                }
                else if(messageBox.Text.Length<1)
                {
                    sendButton.Enabled = false;
                    sendButton2.Enabled = false;
                }
                else
                {
                    sendButton.Enabled = true;
                    sendButton2.Enabled = true;
                }
            }
            else
            {
                sendButton.Enabled = false;
                sendButton2.Enabled = false;
            }
        }
        #endregion


        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void messageBox_TextChanged(object sender, EventArgs e)
        {
            if(messageBox.Text.Length > 0)
            {
                if (users.SelectedItem != null)
                {
                    sendButton.Enabled = true;
                    sendButton2.Enabled = true;
                }
            }
            else
            {
                sendButton.Enabled = false;
                sendButton2.Enabled = false;
            }   
        }
    }
}
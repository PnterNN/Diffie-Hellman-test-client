using SProjectClient.NET.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SProjectClient
{
    public partial class Form1 : Form
    {
        public PacketReader PacketReader;
        private static TcpClient _client;

        private byte[] masterKey;
        private ECDiffieHellmanCng ecdh;

        public Form1()
        {
            InitializeComponent();
            connectServerButton.Enabled = false;
            sendButton.Enabled = false;
            _client = new TcpClient();
        }

        private void connectServerButton_Click(object sender, EventArgs e)
        {
            try
            {
                _client.Connect(ipBox.Text, int.Parse(portBox.Text));
                PacketReader = new PacketReader(_client.GetStream());
                process();

                ecdh = new ECDiffieHellmanCng();
                ecdh.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
                ecdh.HashAlgorithm = CngAlgorithm.Sha256;
                byte[] publicKey = ecdh.PublicKey.ToByteArray();

                PacketBuilder certPacket = new PacketBuilder();
                certPacket.WriteOpCode(1);
                certPacket.WritePublicKey(publicKey);
                _client.Client.Send(certPacket.GetPacketBytes());

                connectServerButton.Enabled = false;
                ipBox.Enabled = false;
                portBox.Enabled = false;
                nameBox.Enabled = false;
            }
            catch
            {
                MessageBox.Show("Failed to connect to server");
            }
        }

        private void process()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    var opcode = PacketReader.ReadByte();
                    switch (opcode)
                    {
                        case 99:
                            var otherPublicKey = PacketReader.ReadPublicKey();
                            masterKey = ecdh.DeriveKeyMaterial(CngKey.Import(otherPublicKey, CngKeyBlobFormat.EccPublicBlob));

                            console.Invoke(new Action(() =>
                            {
                                console.Text += "masterkey: "+ BitConverter.ToString(masterKey).Replace("-", "") + "\n";
                            }));

                            PacketBuilder pb = new PacketBuilder();
                            pb.WriteOpCode(0);
                            pb.WriteMessage(nameBox.Text);
                            _client.Client.Send(pb.GetPacketBytes());
                            break;
                        case 0:
                            var status = PacketReader.ReadMessage();
                            if (status == "false")
                            {
                                console.Invoke(new Action(() =>
                                {
                                    console.Text += "username already connected\n";
                                }));
                                _client.Close();
                                _client = new TcpClient();

                                connectServerButton.Enabled = true;
                                nameBox.Enabled = true;
                                ipBox.Enabled = true;
                                portBox.Enabled = true;
                                sendButton.Enabled = false;
                            }
                            else
                            {
                                console.Invoke(new Action(() =>
                                {
                                    console.Text += "Connected to server\n";
                                }));
                            }
                            break;
                        case 1:
                            var connecteduser = PacketReader.ReadMessage();
                            if (!users.Items.Contains(connecteduser))
                            {
                                if(connecteduser != null || connecteduser != "")
                                {
                                    users.Invoke(new Action(() =>
                                    {
                                        users.Items.Add(connecteduser);
                                    }));
                                    console.Invoke(new Action(() =>
                                    {
                                        console.Text += connecteduser + " connected\n";
                                    }));
                                }
                            }
                            break;
                        case 2:
                            var disconnecteduser = PacketReader.ReadMessage();
                            if (users.Items.Contains(disconnecteduser))
                            {
                                users.Invoke(new Action(() =>
                                {
                                    users.Items.Remove(disconnecteduser);
                                }));
                                console.Invoke(new Action(() =>
                                {
                                    console.Text += disconnecteduser + " disconnected\n";
                                }));
                            }
                            break;
                        case 5:
                            var msg = PacketReader.ReadMessage();
                            MessageBox.Show(msg);
                            console.Invoke(new Action(() =>
                            {
                                console.Invoke(new Action(() =>
                                {
                                    console.Text += msg + "\n";
                                }));
                            }));
                            break;

                    }
                }
            });
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if (users.SelectedItems != null)
            {
                console.Invoke(new Action(() =>
                {
                    console.Text += users.SelectedItem + " -> " + nameBox.Text + " : " + messageBox.Text + "\n";
                }));
                if (messageBox.Text.Length > 0)
                {
                    PacketBuilder pb = new PacketBuilder();
                    pb.WriteOpCode(5);
                    pb.WriteMessage(users.SelectedItem.ToString());
                    pb.WriteMessage(nameBox.Text + " -> " + users.SelectedItem.ToString() + " : " + messageBox.Text);
                    _client.Client.Send(pb.GetPacketBytes());
                }
                messageBox.Text = "";
            }
            
        }

        private void ipBox_TextChanged(object sender, EventArgs e)
        {
            if(portBox.Text.Length > 0 && ipBox.Text.Length > 0 && nameBox.Text.Length > 0)
            {
                connectServerButton.Enabled = true;
            }
            else
            {
                connectServerButton.Enabled = false;
            }
        }

        private void portBox_TextChanged(object sender, EventArgs e)
        {
            if(portBox.Text.Length > 0 && ipBox.Text.Length > 0 && nameBox.Text.Length > 0)
            {
                connectServerButton.Enabled = true;
            }
            else
            {
                connectServerButton.Enabled = false;
            }
        }

        private void nameBox_TextChanged(object sender, EventArgs e)
        {
            if (portBox.Text.Length > 0 && ipBox.Text.Length > 0 && nameBox.Text.Length > 0)
            {
                connectServerButton.Enabled = true;
            }
            else
            {
                connectServerButton.Enabled = false;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void users_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(users.SelectedItem != null)
            {
                sendButton.Enabled = true;
            }
            else
            {
                sendButton.Enabled = false;
            }
        }
    }
}

using SProjectClient.NET.IO;
using System;
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
            connectServerButton.Enabled = false;
            sendButton.Enabled = false;
            sendButton2.Enabled = false;
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
                nameBox.Enabled = false;
            }
            catch
            {
                MessageBox.Show("Sunucuya bağlanılamadı");
            }
        }

        private void process()
        {
            Task.Run(() =>
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
                           
                            clientPrivateKey = rnd.Next(100, 10000);
                            
                            clientPublicKey = BigInteger.ModPow(p, clientPrivateKey, g);
                            console.Invoke(new Action(() =>
                            {
                                console.Text += "p: " + p + "\n";
                                console.Text += "g: " + g + "\n";
                                console.Text += "clientPrivateKey: " + clientPrivateKey + "\n";
                                console.Text += "serverPublicKey: " + serverPublicKey + "\n";
                                console.Text += "clientPublicKey: " + clientPublicKey + "\n";
                            }));


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

                            console.Invoke(new Action(() =>
                            {
                                console.Text += "sharedKey: " + sharedKey + "\n";
                                console.Text += "Ana anahtar: " + BitConverter.ToString(masterKey).Replace("-", "") + "\n";
                            }));

                            PacketBuilder username = new PacketBuilder();
                            username.WriteOpCode(1);
                            username.WriteMessage(Convert.ToBase64String(EncryptStringToBytes_Aes(nameBox.Text, masterKey)));
                            _client.Client.Send(username.GetPacketBytes());
                            break;
                        case 1:
                            var status = DecryptStringFromBytes_Aes(Convert.FromBase64String(_packetReader.ReadMessage()), masterKey);
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
                        case 2:
                            var connectedUser = DecryptStringFromBytes_Aes(Convert.FromBase64String(_packetReader.ReadMessage()), masterKey);
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
                                        console.Text += connectedUser + " connected\n";
                                    }));
                                }
                            }
                            break;
                        case 3:
                            var disconnectedUser = DecryptStringFromBytes_Aes(Convert.FromBase64String(_packetReader.ReadMessage()), masterKey);
                            if (users.Items.Contains(disconnectedUser))
                            {
                                users.Invoke(new Action(() =>
                                {
                                    users.Items.Remove(disconnectedUser);
                                }));
                                console.Invoke(new Action(() =>
                                {
                                    console.Text += disconnectedUser + " disconnected\n";
                                }));
                            }
                            break;
                        case 4:
                            var message = DecryptStringFromBytes_Aes(Convert.FromBase64String(_packetReader.ReadMessage()), masterKey);
                            console.Invoke(new Action(() =>
                            {
                                console.Text += message + "\n";
                            }));
                            break;
                        case 5:
                            var message2 = _packetReader.ReadMessage();
                            console.Invoke(new Action(() =>
                            {
                                console.Text += message2 + "\n";
                            }));
                            break;
                    }
                }
            });
        }

        #region UI Bileşenleri
        private void sendButton_Click(object sender, EventArgs e)
        {
            if (users.SelectedItems != null)
            {
                console.Invoke(new Action(() =>
                {
                    console.Text += nameBox.Text + " -> " + users.SelectedItem + " : " + messageBox.Text + "\n";
                }));
                if (messageBox.Text.Length > 0)
                {
                    PacketBuilder pb = new PacketBuilder();
                    pb.WriteOpCode(4);
                    pb.WriteMessage(Convert.ToBase64String(EncryptStringToBytes_Aes(users.SelectedItem.ToString(), masterKey)));
                    pb.WriteMessage(Convert.ToBase64String(EncryptStringToBytes_Aes(nameBox.Text + " -> " + users.SelectedItem + " : " + messageBox.Text, masterKey)));
                    _client.Client.Send(pb.GetPacketBytes());
                }
                messageBox.Text = "";
            }
        }

        private void ipBox_TextChanged(object sender, EventArgs e)
        {
            connectServerButton.Enabled = ipBox.Text.Length > 0 && portBox.Text.Length > 0 && nameBox.Text.Length > 0;
        }

        private void portBox_TextChanged(object sender, EventArgs e)
        {
            connectServerButton.Enabled = ipBox.Text.Length > 0 && portBox.Text.Length > 0 && nameBox.Text.Length > 0;
        }

        private void nameBox_TextChanged(object sender, EventArgs e)
        {
            connectServerButton.Enabled = ipBox.Text.Length > 0 && portBox.Text.Length > 0 && nameBox.Text.Length > 0;
        }

        private void users_SelectedIndexChanged(object sender, EventArgs e)
        {
            sendButton.Enabled = users.SelectedItem != null;
            sendButton2.Enabled = users.SelectedItem != null;
        }
        #endregion

        #region encryption and decryption
        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] key)
        {
            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.Mode = CipherMode.CBC; // Cipher Block Chaining
                aesAlg.Padding = PaddingMode.PKCS7; // PKCS7 padding

                // IV (Initialization Vector) oluştur
                aesAlg.GenerateIV();

                // IV'yi şifrelenmiş metnin başına ekleyerek kaydet
                byte[] iv = aesAlg.IV;

                using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                {
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                // Metni şifrele
                                swEncrypt.Write(plainText);
                            }
                            encrypted = msEncrypt.ToArray();
                        }
                    }
                }

                // IV'yi şifrelenmiş metnin başına ekleyerek kaydet
                byte[] result = new byte[iv.Length + encrypted.Length];
                Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                Buffer.BlockCopy(encrypted, 0, result, iv.Length, encrypted.Length);
                return result;
            }
        }

        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] key)
        {
            // İlk 16 byte IV, geri kalanı şifrelenmiş metin
            byte[] iv = new byte[16];
            byte[] cipher = new byte[cipherText.Length - 16];
            Buffer.BlockCopy(cipherText, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(cipherText, iv.Length, cipher, 0, cipher.Length);

            string plaintext = null;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                {
                    using (MemoryStream msDecrypt = new MemoryStream(cipher))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }

            return plaintext;
        }
        #endregion

        private void sendButton2_Click(object sender, EventArgs e)
        {
            if (users.SelectedItems != null)
            {
                console.Invoke(new Action(() =>
                {
                    console.Text += nameBox.Text+ " -> " + users.SelectedItem + " : " + messageBox.Text + "\n";
                }));
                if (messageBox.Text.Length > 0)
                {
                    PacketBuilder pb = new PacketBuilder();
                    pb.WriteOpCode(5);
                    pb.WriteMessage(users.SelectedItem.ToString());
                    pb.WriteMessage(nameBox.Text + " -> " + users.SelectedItem + " : " + messageBox.Text);
                    _client.Client.Send(pb.GetPacketBytes());
                }
                messageBox.Text = "";
            }
        }
    }
}
namespace SProjectClient
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.sendButton2 = new System.Windows.Forms.Button();
            this.sendButton = new System.Windows.Forms.Button();
            this.messageBox = new System.Windows.Forms.TextBox();
            this.console = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ipBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.connectServerButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.portBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.loginEmailBox = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.users = new System.Windows.Forms.ListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.loginButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.loginPasswordBox = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.registerButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.registerPasswordBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.registerUsernameBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.registerEmailBox = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.sendButton2);
            this.groupBox2.Controls.Add(this.sendButton);
            this.groupBox2.Controls.Add(this.messageBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 294);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1006, 79);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Server";
            // 
            // sendButton2
            // 
            this.sendButton2.Location = new System.Drawing.Point(206, 49);
            this.sendButton2.Name = "sendButton2";
            this.sendButton2.Size = new System.Drawing.Size(185, 24);
            this.sendButton2.TabIndex = 3;
            this.sendButton2.Text = "Send Message";
            this.sendButton2.UseVisualStyleBackColor = true;
            this.sendButton2.Click += new System.EventHandler(this.sendButton2_Click);
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(9, 49);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(185, 24);
            this.sendButton.TabIndex = 2;
            this.sendButton.Text = "Send Message (Encrypted)";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // messageBox
            // 
            this.messageBox.Location = new System.Drawing.Point(9, 21);
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(991, 22);
            this.messageBox.TabIndex = 0;
            this.messageBox.TextChanged += new System.EventHandler(this.messageBox_TextChanged);
            // 
            // console
            // 
            this.console.Location = new System.Drawing.Point(445, 12);
            this.console.Name = "console";
            this.console.Size = new System.Drawing.Size(570, 276);
            this.console.TabIndex = 5;
            this.console.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ipBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.connectServerButton);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.portBox);
            this.groupBox1.Location = new System.Drawing.Point(229, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 121);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // ipBox
            // 
            this.ipBox.Location = new System.Drawing.Point(73, 21);
            this.ipBox.MaxLength = 99;
            this.ipBox.Name = "ipBox";
            this.ipBox.Size = new System.Drawing.Size(121, 22);
            this.ipBox.TabIndex = 0;
            this.ipBox.Text = "127.0.0.1";
            this.ipBox.TextChanged += new System.EventHandler(this.ipBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Ip";
            // 
            // connectServerButton
            // 
            this.connectServerButton.Location = new System.Drawing.Point(9, 77);
            this.connectServerButton.Name = "connectServerButton";
            this.connectServerButton.Size = new System.Drawing.Size(185, 38);
            this.connectServerButton.TabIndex = 2;
            this.connectServerButton.Text = "Connect Server";
            this.connectServerButton.UseVisualStyleBackColor = true;
            this.connectServerButton.Click += new System.EventHandler(this.connectServerButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port";
            // 
            // portBox
            // 
            this.portBox.Location = new System.Drawing.Point(73, 49);
            this.portBox.MaxLength = 5;
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(121, 22);
            this.portBox.TabIndex = 0;
            this.portBox.Text = "901";
            this.portBox.TextChanged += new System.EventHandler(this.portBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Email";
            // 
            // loginEmailBox
            // 
            this.loginEmailBox.Location = new System.Drawing.Point(73, 21);
            this.loginEmailBox.MaxLength = 99;
            this.loginEmailBox.Name = "loginEmailBox";
            this.loginEmailBox.Size = new System.Drawing.Size(121, 22);
            this.loginEmailBox.TabIndex = 4;
            this.loginEmailBox.Text = "pnternn@hotmail.com";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.users);
            this.groupBox3.Location = new System.Drawing.Point(229, 139);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 149);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Users";
            // 
            // users
            // 
            this.users.FormattingEnabled = true;
            this.users.ItemHeight = 16;
            this.users.Items.AddRange(new object[] {
            "Everyone"});
            this.users.Location = new System.Drawing.Point(6, 21);
            this.users.Name = "users";
            this.users.Size = new System.Drawing.Size(188, 116);
            this.users.TabIndex = 0;
            this.users.SelectedIndexChanged += new System.EventHandler(this.users_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.loginButton);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.loginPasswordBox);
            this.groupBox4.Controls.Add(this.loginEmailBox);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Location = new System.Drawing.Point(9, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 121);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "LOGIN";
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(12, 77);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(182, 38);
            this.loginButton.TabIndex = 7;
            this.loginButton.Text = "Login";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Password";
            // 
            // loginPasswordBox
            // 
            this.loginPasswordBox.Location = new System.Drawing.Point(73, 49);
            this.loginPasswordBox.MaxLength = 99;
            this.loginPasswordBox.Name = "loginPasswordBox";
            this.loginPasswordBox.PasswordChar = '•';
            this.loginPasswordBox.Size = new System.Drawing.Size(121, 22);
            this.loginPasswordBox.TabIndex = 5;
            this.loginPasswordBox.Text = "12050079Okmk";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.registerButton);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.registerPasswordBox);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.registerUsernameBox);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.registerEmailBox);
            this.groupBox5.Location = new System.Drawing.Point(12, 139);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 149);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "REGISTER";
            // 
            // registerButton
            // 
            this.registerButton.Location = new System.Drawing.Point(12, 105);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(182, 38);
            this.registerButton.TabIndex = 16;
            this.registerButton.Text = "registerButton";
            this.registerButton.UseVisualStyleBackColor = true;
            this.registerButton.Click += new System.EventHandler(this.registerButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 16);
            this.label7.TabIndex = 12;
            this.label7.Text = "Password";
            // 
            // registerPasswordBox
            // 
            this.registerPasswordBox.Location = new System.Drawing.Point(73, 77);
            this.registerPasswordBox.Name = "registerPasswordBox";
            this.registerPasswordBox.PasswordChar = '•';
            this.registerPasswordBox.Size = new System.Drawing.Size(121, 22);
            this.registerPasswordBox.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "Email";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // registerUsernameBox
            // 
            this.registerUsernameBox.Location = new System.Drawing.Point(73, 21);
            this.registerUsernameBox.Name = "registerUsernameBox";
            this.registerUsernameBox.Size = new System.Drawing.Size(121, 22);
            this.registerUsernameBox.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Username";
            // 
            // registerEmailBox
            // 
            this.registerEmailBox.Location = new System.Drawing.Point(73, 49);
            this.registerEmailBox.Name = "registerEmailBox";
            this.registerEmailBox.Size = new System.Drawing.Size(121, 22);
            this.registerEmailBox.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 381);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.console);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Client";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.TextBox messageBox;
        private System.Windows.Forms.RichTextBox console;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button connectServerButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox portBox;
        private System.Windows.Forms.TextBox ipBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox loginEmailBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox users;
        private System.Windows.Forms.Button sendButton2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox loginPasswordBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox registerPasswordBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox registerUsernameBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox registerEmailBox;
        private System.Windows.Forms.Button registerButton;
    }
}


namespace SciChain
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            loginBut = new Button();
            statusStrip = new StatusStrip();
            toolStripStatusLabel = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            balanceLabel = new Label();
            addrBox = new TextBox();
            sendBut = new Button();
            openFileDialog = new OpenFileDialog();
            label1 = new Label();
            amountBox = new NumericUpDown();
            maskedTextBox = new MaskedTextBox();
            label6 = new Label();
            updateBut = new Button();
            label7 = new Label();
            sendToNameBox = new TextBox();
            getAddrBut = new Button();
            timer = new System.Windows.Forms.Timer(components);
            textBox1 = new TextBox();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)amountBox).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // loginBut
            // 
            loginBut.Location = new Point(344, 89);
            loginBut.Name = "loginBut";
            loginBut.Size = new Size(132, 23);
            loginBut.TabIndex = 0;
            loginBut.Text = "Login";
            loginBut.UseVisualStyleBackColor = true;
            loginBut.Click += loginBut_Click;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel, toolStripStatusLabel2 });
            statusStrip.Location = new Point(0, 153);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(493, 22);
            statusStrip.TabIndex = 1;
            statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            toolStripStatusLabel.Name = "toolStripStatusLabel";
            toolStripStatusLabel.Size = new Size(0, 17);
            toolStripStatusLabel.Click += toolStripStatusLabel_Click;
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(0, 17);
            // 
            // balanceLabel
            // 
            balanceLabel.AutoSize = true;
            balanceLabel.Location = new Point(6, 3);
            balanceLabel.Name = "balanceLabel";
            balanceLabel.Size = new Size(51, 15);
            balanceLabel.TabIndex = 2;
            balanceLabel.Text = "Balance:";
            // 
            // addrBox
            // 
            addrBox.Location = new Point(71, 31);
            addrBox.Name = "addrBox";
            addrBox.Size = new Size(232, 23);
            addrBox.TabIndex = 3;
            // 
            // sendBut
            // 
            sendBut.Enabled = false;
            sendBut.Location = new Point(395, 60);
            sendBut.Name = "sendBut";
            sendBut.Size = new Size(81, 23);
            sendBut.TabIndex = 4;
            sendBut.Text = "Send";
            sendBut.UseVisualStyleBackColor = true;
            sendBut.Click += sendBut_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 34);
            label1.Name = "label1";
            label1.Size = new Size(52, 15);
            label1.TabIndex = 8;
            label1.Text = "Address:";
            // 
            // amountBox
            // 
            amountBox.DecimalPlaces = 28;
            amountBox.Location = new Point(309, 31);
            amountBox.Name = "amountBox";
            amountBox.Size = new Size(167, 23);
            amountBox.TabIndex = 9;
            // 
            // maskedTextBox
            // 
            maskedTextBox.Location = new Point(104, 89);
            maskedTextBox.Name = "maskedTextBox";
            maskedTextBox.PasswordChar = '*';
            maskedTextBox.Size = new Size(234, 23);
            maskedTextBox.TabIndex = 20;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(5, 92);
            label6.Name = "label6";
            label6.Size = new Size(93, 15);
            label6.TabIndex = 21;
            label6.Text = "Wallet Password";
            // 
            // updateBut
            // 
            updateBut.Enabled = false;
            updateBut.Location = new Point(396, 2);
            updateBut.Name = "updateBut";
            updateBut.Size = new Size(81, 23);
            updateBut.TabIndex = 22;
            updateBut.Text = "Update";
            updateBut.UseVisualStyleBackColor = true;
            updateBut.Click += updateBut_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 64);
            label7.Name = "label7";
            label7.Size = new Size(42, 15);
            label7.TabIndex = 23;
            label7.Text = "Name:";
            // 
            // sendToNameBox
            // 
            sendToNameBox.Location = new Point(71, 60);
            sendToNameBox.Name = "sendToNameBox";
            sendToNameBox.Size = new Size(232, 23);
            sendToNameBox.TabIndex = 24;
            // 
            // getAddrBut
            // 
            getAddrBut.Enabled = false;
            getAddrBut.Location = new Point(309, 60);
            getAddrBut.Name = "getAddrBut";
            getAddrBut.Size = new Size(81, 23);
            getAddrBut.TabIndex = 25;
            getAddrBut.Text = "Get Address";
            getAddrBut.UseVisualStyleBackColor = true;
            getAddrBut.Click += getAddrBut_Click;
            // 
            // timer
            // 
            timer.Interval = 1000;
            timer.Tick += timer_Tick;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.Black;
            textBox1.Dock = DockStyle.Fill;
            textBox1.ForeColor = Color.White;
            textBox1.Location = new Point(3, 3);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(479, 119);
            textBox1.TabIndex = 35;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(493, 153);
            tabControl1.TabIndex = 36;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(balanceLabel);
            tabPage1.Controls.Add(loginBut);
            tabPage1.Controls.Add(getAddrBut);
            tabPage1.Controls.Add(addrBox);
            tabPage1.Controls.Add(sendToNameBox);
            tabPage1.Controls.Add(sendBut);
            tabPage1.Controls.Add(label7);
            tabPage1.Controls.Add(updateBut);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(label6);
            tabPage1.Controls.Add(amountBox);
            tabPage1.Controls.Add(maskedTextBox);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(485, 125);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Wallet";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(textBox1);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(485, 125);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Console";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AcceptButton = loginBut;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(493, 175);
            Controls.Add(tabControl1);
            Controls.Add(statusStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "SciChain";
            FormClosing += MainForm_FormClosing;
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)amountBox).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button loginBut;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel toolStripStatusLabel;
        private Label balanceLabel;
        private TextBox addrBox;
        private Button sendBut;
        private OpenFileDialog openFileDialog;
        private Label label1;
        private NumericUpDown amountBox;
        private MaskedTextBox maskedTextBox;
        private Label label6;
        private Button updateBut;
        private Label label7;
        private TextBox sendToNameBox;
        private Button getAddrBut;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Timer timer;
        private TextBox textBox1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
    }
}

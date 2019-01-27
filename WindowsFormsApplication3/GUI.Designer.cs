namespace LegoControllerHCI
{
    partial class openconnection
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(openconnection));
            this.buttonleft = new System.Windows.Forms.Button();
            this.buttonup = new System.Windows.Forms.Button();
            this.buttondown = new System.Windows.Forms.Button();
            this.buttonright = new System.Windows.Forms.Button();
            this.StimCodeTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.runButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comporttextbox = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cBoxCommPort = new System.Windows.Forms.ComboBox();
            this.comport = new System.Windows.Forms.Button();
            this.trialprogress = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.motorASpeed = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.motorBSpeed = new System.Windows.Forms.NumericUpDown();
            this.checkBoxMotorASpeedEnable = new System.Windows.Forms.CheckBox();
            this.checkBoxMotorBSpeedEnable = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.motorASpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.motorBSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonleft
            // 
            this.buttonleft.Image = ((System.Drawing.Image)(resources.GetObject("buttonleft.Image")));
            this.buttonleft.Location = new System.Drawing.Point(221, 334);
            this.buttonleft.Name = "buttonleft";
            this.buttonleft.Size = new System.Drawing.Size(160, 162);
            this.buttonleft.TabIndex = 0;
            this.buttonleft.Text = "Left Backward";
            this.buttonleft.UseVisualStyleBackColor = true;
            this.buttonleft.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonleft_MouseClick);
            // 
            // buttonup
            // 
            this.buttonup.Image = ((System.Drawing.Image)(resources.GetObject("buttonup.Image")));
            this.buttonup.Location = new System.Drawing.Point(387, 254);
            this.buttonup.Name = "buttonup";
            this.buttonup.Size = new System.Drawing.Size(164, 160);
            this.buttonup.TabIndex = 1;
            this.buttonup.Text = "Right Backward";
            this.buttonup.UseVisualStyleBackColor = true;
            this.buttonup.Click += new System.EventHandler(this.buttonup_Click);
            // 
            // buttondown
            // 
            this.buttondown.Image = ((System.Drawing.Image)(resources.GetObject("buttondown.Image")));
            this.buttondown.Location = new System.Drawing.Point(387, 420);
            this.buttondown.Name = "buttondown";
            this.buttondown.Size = new System.Drawing.Size(164, 162);
            this.buttondown.TabIndex = 2;
            this.buttondown.Text = "Right Forward";
            this.buttondown.UseVisualStyleBackColor = true;
            this.buttondown.Click += new System.EventHandler(this.buttondown_Click);
            // 
            // buttonright
            // 
            this.buttonright.Image = ((System.Drawing.Image)(resources.GetObject("buttonright.Image")));
            this.buttonright.Location = new System.Drawing.Point(557, 334);
            this.buttonright.Name = "buttonright";
            this.buttonright.Size = new System.Drawing.Size(158, 162);
            this.buttonright.TabIndex = 3;
            this.buttonright.UseVisualStyleBackColor = true;
            this.buttonright.Click += new System.EventHandler(this.buttonright_Click);
            // 
            // StimCodeTextBox
            // 
            this.StimCodeTextBox.Location = new System.Drawing.Point(19, 22);
            this.StimCodeTextBox.Name = "StimCodeTextBox";
            this.StimCodeTextBox.Size = new System.Drawing.Size(110, 20);
            this.StimCodeTextBox.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(532, 53);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 87);
            this.button1.TabIndex = 8;
            this.button1.Text = "Open Connection";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(24, 23);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(110, 20);
            this.textBox1.TabIndex = 9;
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(24, 49);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(110, 40);
            this.runButton.TabIndex = 10;
            this.runButton.Text = "New Thread Run Counter";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.runButton);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Location = new System.Drawing.Point(13, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(151, 107);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Multi-Threading";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.StimCodeTextBox);
            this.groupBox2.Location = new System.Drawing.Point(170, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(142, 106);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Stim codes Received";
            // 
            // comporttextbox
            // 
            this.comporttextbox.Location = new System.Drawing.Point(865, 12);
            this.comporttextbox.Name = "comporttextbox";
            this.comporttextbox.Size = new System.Drawing.Size(131, 20);
            this.comporttextbox.TabIndex = 15;
            this.comporttextbox.Text = "COM11";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cBoxCommPort);
            this.groupBox3.Location = new System.Drawing.Point(353, 53);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(173, 87);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Conncection Type (usb /COM)";
            // 
            // cBoxCommPort
            // 
            this.cBoxCommPort.FormattingEnabled = true;
            this.cBoxCommPort.Location = new System.Drawing.Point(24, 34);
            this.cBoxCommPort.Name = "cBoxCommPort";
            this.cBoxCommPort.Size = new System.Drawing.Size(131, 21);
            this.cBoxCommPort.TabIndex = 19;
            this.cBoxCommPort.Text = "COM11";
            // 
            // comport
            // 
            this.comport.Location = new System.Drawing.Point(865, 36);
            this.comport.Name = "comport";
            this.comport.Size = new System.Drawing.Size(131, 21);
            this.comport.TabIndex = 16;
            this.comport.Text = "Not Required";
            this.comport.UseVisualStyleBackColor = true;
            this.comport.Click += new System.EventHandler(this.comport_Click);
            // 
            // trialprogress
            // 
            this.trialprogress.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.trialprogress.Location = new System.Drawing.Point(24, 45);
            this.trialprogress.Name = "trialprogress";
            this.trialprogress.Size = new System.Drawing.Size(217, 27);
            this.trialprogress.TabIndex = 17;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.trialprogress);
            this.groupBox4.Location = new System.Drawing.Point(353, 146);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(264, 102);
            this.groupBox4.TabIndex = 18;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Progress of Trial";
            // 
            // motorASpeed
            // 
            this.motorASpeed.Enabled = false;
            this.motorASpeed.Location = new System.Drawing.Point(794, 111);
            this.motorASpeed.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.motorASpeed.Name = "motorASpeed";
            this.motorASpeed.Size = new System.Drawing.Size(131, 20);
            this.motorASpeed.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(791, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Motor A Speed";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(791, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Motor B Speed";
            // 
            // motorBSpeed
            // 
            this.motorBSpeed.Enabled = false;
            this.motorBSpeed.Location = new System.Drawing.Point(794, 153);
            this.motorBSpeed.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.motorBSpeed.Name = "motorBSpeed";
            this.motorBSpeed.Size = new System.Drawing.Size(131, 20);
            this.motorBSpeed.TabIndex = 21;
            // 
            // checkBoxMotorASpeedEnable
            // 
            this.checkBoxMotorASpeedEnable.AutoSize = true;
            this.checkBoxMotorASpeedEnable.Location = new System.Drawing.Point(931, 114);
            this.checkBoxMotorASpeedEnable.Name = "checkBoxMotorASpeedEnable";
            this.checkBoxMotorASpeedEnable.Size = new System.Drawing.Size(59, 17);
            this.checkBoxMotorASpeedEnable.TabIndex = 23;
            this.checkBoxMotorASpeedEnable.Text = "Enable";
            this.checkBoxMotorASpeedEnable.UseVisualStyleBackColor = true;
            this.checkBoxMotorASpeedEnable.CheckedChanged += new System.EventHandler(this.checkBoxMotorASpeedEnable_CheckedChanged);
            // 
            // checkBoxMotorBSpeedEnable
            // 
            this.checkBoxMotorBSpeedEnable.AutoSize = true;
            this.checkBoxMotorBSpeedEnable.Location = new System.Drawing.Point(931, 156);
            this.checkBoxMotorBSpeedEnable.Name = "checkBoxMotorBSpeedEnable";
            this.checkBoxMotorBSpeedEnable.Size = new System.Drawing.Size(59, 17);
            this.checkBoxMotorBSpeedEnable.TabIndex = 24;
            this.checkBoxMotorBSpeedEnable.Text = "Enable";
            this.checkBoxMotorBSpeedEnable.UseVisualStyleBackColor = true;
            this.checkBoxMotorBSpeedEnable.CheckedChanged += new System.EventHandler(this.checkBoxMotorBSpeedEnable_CheckedChanged);
            // 
            // openconnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.checkBoxMotorBSpeedEnable);
            this.Controls.Add(this.checkBoxMotorASpeedEnable);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.motorBSpeed);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.motorASpeed);
            this.Controls.Add(this.comport);
            this.Controls.Add(this.comporttextbox);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonright);
            this.Controls.Add(this.buttondown);
            this.Controls.Add(this.buttonup);
            this.Controls.Add(this.buttonleft);
            this.Name = "openconnection";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GUI_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.motorASpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.motorBSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonleft;
        private System.Windows.Forms.Button buttonup;
        private System.Windows.Forms.Button buttondown;
        private System.Windows.Forms.Button buttonright;
        private System.Windows.Forms.TextBox StimCodeTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox comporttextbox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button comport;
        private System.Windows.Forms.TextBox trialprogress;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cBoxCommPort;
        private System.Windows.Forms.NumericUpDown motorASpeed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown motorBSpeed;
        private System.Windows.Forms.CheckBox checkBoxMotorASpeedEnable;
        private System.Windows.Forms.CheckBox checkBoxMotorBSpeedEnable;
    }
}


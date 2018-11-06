namespace DzhMonitor
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.comboBoxApp = new System.Windows.Forms.ComboBox();
            this.comboBoxInvalidate = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonSZ = new System.Windows.Forms.Button();
            this.buttonSH = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.radioButton2Baidu = new System.Windows.Forms.RadioButton();
            this.radioButton2Local = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioButton1Baidu = new System.Windows.Forms.RadioButton();
            this.radioButton1Local = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton0Baidu = new System.Windows.Forms.RadioButton();
            this.radioButton0Local = new System.Windows.Forms.RadioButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox0 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label0 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonStop = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox0)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(318, 116);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "退出";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBoxApp
            // 
            this.comboBoxApp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxApp.FormattingEnabled = true;
            this.comboBoxApp.Items.AddRange(new object[] {
            "dzh2",
            "TdxW"});
            this.comboBoxApp.Location = new System.Drawing.Point(86, 12);
            this.comboBoxApp.Name = "comboBoxApp";
            this.comboBoxApp.Size = new System.Drawing.Size(121, 20);
            this.comboBoxApp.TabIndex = 1;
            // 
            // comboBoxInvalidate
            // 
            this.comboBoxInvalidate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInvalidate.FormattingEnabled = true;
            this.comboBoxInvalidate.Items.AddRange(new object[] {
            "100",
            "200",
            "500",
            "1000"});
            this.comboBoxInvalidate.Location = new System.Drawing.Point(86, 41);
            this.comboBoxInvalidate.Name = "comboBoxInvalidate";
            this.comboBoxInvalidate.Size = new System.Drawing.Size(121, 20);
            this.comboBoxInvalidate.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonSZ);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.buttonSH);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Location = new System.Drawing.Point(7, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 78);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "屏幕版面";
            // 
            // buttonSZ
            // 
            this.buttonSZ.Location = new System.Drawing.Point(6, 49);
            this.buttonSZ.Name = "buttonSZ";
            this.buttonSZ.Size = new System.Drawing.Size(96, 23);
            this.buttonSZ.TabIndex = 4;
            this.buttonSZ.Text = "深市版面微调";
            this.buttonSZ.UseVisualStyleBackColor = true;
            this.buttonSZ.Click += new System.EventHandler(this.buttonSZ_Click);
            // 
            // buttonSH
            // 
            this.buttonSH.Location = new System.Drawing.Point(108, 49);
            this.buttonSH.Name = "buttonSH";
            this.buttonSH.Size = new System.Drawing.Size(86, 23);
            this.buttonSH.TabIndex = 4;
            this.buttonSH.Text = "沪市版面微调";
            this.buttonSH.UseVisualStyleBackColor = true;
            this.buttonSH.Click += new System.EventHandler(this.buttonSH_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(229, 12);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 5;
            this.buttonStart.Text = "开始";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 6;
            this.label10.Text = "监控程序";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(3, 44);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(77, 12);
            this.label20.TabIndex = 7;
            this.label20.Text = "截图间隔时间";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel3);
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Controls.Add(this.pictureBox2);
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Controls.Add(this.pictureBox0);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label0);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(7, 152);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(408, 194);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "监控结果";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.radioButton2Baidu);
            this.panel3.Controls.Add(this.radioButton2Local);
            this.panel3.Location = new System.Drawing.Point(288, 67);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(91, 50);
            this.panel3.TabIndex = 18;
            // 
            // radioButton2Baidu
            // 
            this.radioButton2Baidu.AutoSize = true;
            this.radioButton2Baidu.Checked = true;
            this.radioButton2Baidu.Location = new System.Drawing.Point(3, 26);
            this.radioButton2Baidu.Name = "radioButton2Baidu";
            this.radioButton2Baidu.Size = new System.Drawing.Size(71, 16);
            this.radioButton2Baidu.TabIndex = 17;
            this.radioButton2Baidu.TabStop = true;
            this.radioButton2Baidu.Text = "百度引擎";
            this.radioButton2Baidu.UseVisualStyleBackColor = true;
            // 
            // radioButton2Local
            // 
            this.radioButton2Local.AutoSize = true;
            this.radioButton2Local.Location = new System.Drawing.Point(3, 5);
            this.radioButton2Local.Name = "radioButton2Local";
            this.radioButton2Local.Size = new System.Drawing.Size(71, 16);
            this.radioButton2Local.TabIndex = 16;
            this.radioButton2Local.Text = "本地引擎";
            this.radioButton2Local.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radioButton1Baidu);
            this.panel2.Controls.Add(this.radioButton1Local);
            this.panel2.Location = new System.Drawing.Point(154, 67);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(99, 47);
            this.panel2.TabIndex = 17;
            // 
            // radioButton1Baidu
            // 
            this.radioButton1Baidu.AutoSize = true;
            this.radioButton1Baidu.Location = new System.Drawing.Point(14, 25);
            this.radioButton1Baidu.Name = "radioButton1Baidu";
            this.radioButton1Baidu.Size = new System.Drawing.Size(71, 16);
            this.radioButton1Baidu.TabIndex = 15;
            this.radioButton1Baidu.Text = "百度引擎";
            this.radioButton1Baidu.UseVisualStyleBackColor = true;
            // 
            // radioButton1Local
            // 
            this.radioButton1Local.AutoSize = true;
            this.radioButton1Local.Checked = true;
            this.radioButton1Local.Location = new System.Drawing.Point(14, 5);
            this.radioButton1Local.Name = "radioButton1Local";
            this.radioButton1Local.Size = new System.Drawing.Size(71, 16);
            this.radioButton1Local.TabIndex = 14;
            this.radioButton1Local.TabStop = true;
            this.radioButton1Local.Text = "本地引擎";
            this.radioButton1Local.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton0Baidu);
            this.panel1.Controls.Add(this.radioButton0Local);
            this.panel1.Location = new System.Drawing.Point(17, 67);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(99, 47);
            this.panel1.TabIndex = 16;
            // 
            // radioButton0Baidu
            // 
            this.radioButton0Baidu.AutoSize = true;
            this.radioButton0Baidu.Checked = true;
            this.radioButton0Baidu.Location = new System.Drawing.Point(5, 25);
            this.radioButton0Baidu.Name = "radioButton0Baidu";
            this.radioButton0Baidu.Size = new System.Drawing.Size(71, 16);
            this.radioButton0Baidu.TabIndex = 12;
            this.radioButton0Baidu.TabStop = true;
            this.radioButton0Baidu.Text = "百度引擎";
            this.radioButton0Baidu.UseVisualStyleBackColor = true;
            // 
            // radioButton0Local
            // 
            this.radioButton0Local.AutoSize = true;
            this.radioButton0Local.Location = new System.Drawing.Point(5, 3);
            this.radioButton0Local.Name = "radioButton0Local";
            this.radioButton0Local.Size = new System.Drawing.Size(71, 16);
            this.radioButton0Local.TabIndex = 11;
            this.radioButton0Local.Text = "本地引擎";
            this.radioButton0Local.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(288, 133);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(99, 36);
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(154, 133);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(99, 36);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox0
            // 
            this.pictureBox0.Location = new System.Drawing.Point(17, 133);
            this.pictureBox0.Name = "pictureBox0";
            this.pictureBox0.Size = new System.Drawing.Size(99, 36);
            this.pictureBox0.TabIndex = 7;
            this.pictureBox0.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(346, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "label9";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(199, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "label8";
            // 
            // label0
            // 
            this.label0.AutoSize = true;
            this.label0.Location = new System.Drawing.Point(67, 51);
            this.label0.Name = "label0";
            this.label0.Size = new System.Drawing.Size(41, 12);
            this.label0.TabIndex = 4;
            this.label0.Text = "label7";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(287, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "买一笔数";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(152, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "买一量";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "买一价";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "当前股票";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(229, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "label7";
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(318, 12);
            this.buttonStop.Margin = new System.Windows.Forms.Padding(2);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 10;
            this.buttonStop.Text = "停止";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(9, 20);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(108, 20);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(229, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "label8";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 358);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboBoxInvalidate);
            this.Controls.Add(this.comboBoxApp);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "监控";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox0)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBoxApp;
        private System.Windows.Forms.ComboBox comboBoxInvalidate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonSH;
        private System.Windows.Forms.Button buttonSZ;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label0;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox0;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton radioButton2Baidu;
        private System.Windows.Forms.RadioButton radioButton2Local;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioButton1Baidu;
        private System.Windows.Forms.RadioButton radioButton1Local;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButton0Baidu;
        private System.Windows.Forms.RadioButton radioButton0Local;
        private System.Windows.Forms.Label label8;
    }
}


﻿namespace OthelloAI
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(66, 45);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(400, 400);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("ＭＳ 明朝", 12F);
            this.checkBox1.Location = new System.Drawing.Point(564, 45);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(74, 20);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "黒(AI)";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Font = new System.Drawing.Font("ＭＳ 明朝", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox2.Location = new System.Drawing.Point(564, 69);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(74, 20);
            this.checkBox2.TabIndex = 5;
            this.checkBox2.Text = "白(AI)";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.label1.Location = new System.Drawing.Point(559, 114);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "先読みする手数";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.label2.Location = new System.Drawing.Point(709, 114);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "手";
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("ＭＳ 明朝", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(564, 182);
            this.listBox1.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(143, 260);
            this.listBox1.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.label3.Location = new System.Drawing.Point(561, 164);
            this.label3.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "マス";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.label4.Location = new System.Drawing.Point(652, 164);
            this.label4.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "評価値";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(738, 420);
            this.button1.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(52, 18);
            this.button1.TabIndex = 11;
            this.button1.Text = "Reset";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 25F);
            this.label5.Location = new System.Drawing.Point(77, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 34);
            this.label5.TabIndex = 12;
            this.label5.Text = "a";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 25F);
            this.label6.Location = new System.Drawing.Point(125, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 34);
            this.label6.TabIndex = 13;
            this.label6.Text = "b";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 25F);
            this.label7.Location = new System.Drawing.Point(176, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 34);
            this.label7.TabIndex = 14;
            this.label7.Text = "c";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("MS UI Gothic", 25F);
            this.label8.Location = new System.Drawing.Point(226, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 34);
            this.label8.TabIndex = 15;
            this.label8.Text = "d";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("MS UI Gothic", 25F);
            this.label9.Location = new System.Drawing.Point(276, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 34);
            this.label9.TabIndex = 16;
            this.label9.Text = "e";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("MS UI Gothic", 25F);
            this.label10.Location = new System.Drawing.Point(326, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(25, 34);
            this.label10.TabIndex = 17;
            this.label10.Text = "f";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("MS UI Gothic", 25F);
            this.label11.Location = new System.Drawing.Point(376, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 34);
            this.label11.TabIndex = 18;
            this.label11.Text = "g";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("MS UI Gothic", 25F);
            this.label12.Location = new System.Drawing.Point(425, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 34);
            this.label12.TabIndex = 19;
            this.label12.Text = "h";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("MS UI Gothic", 25F);
            this.label13.Location = new System.Drawing.Point(30, 55);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(32, 34);
            this.label13.TabIndex = 20;
            this.label13.Text = "1";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("MS UI Gothic", 25F);
            this.label14.Location = new System.Drawing.Point(30, 100);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(32, 34);
            this.label14.TabIndex = 21;
            this.label14.Text = "2";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("MS UI Gothic", 25F);
            this.label15.Location = new System.Drawing.Point(30, 150);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(32, 34);
            this.label15.TabIndex = 22;
            this.label15.Text = "3";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("MS UI Gothic", 25F);
            this.label16.Location = new System.Drawing.Point(29, 203);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(32, 34);
            this.label16.TabIndex = 23;
            this.label16.Text = "4";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("MS UI Gothic", 25F);
            this.label17.Location = new System.Drawing.Point(29, 253);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(32, 34);
            this.label17.TabIndex = 24;
            this.label17.Text = "5";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("MS UI Gothic", 25F);
            this.label18.Location = new System.Drawing.Point(30, 304);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(32, 34);
            this.label18.TabIndex = 25;
            this.label18.Text = "6";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("MS UI Gothic", 25F);
            this.label19.Location = new System.Drawing.Point(30, 353);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(32, 34);
            this.label19.TabIndex = 26;
            this.label19.Text = "7";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("MS UI Gothic", 25F);
            this.label20.Location = new System.Drawing.Point(30, 404);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(32, 34);
            this.label20.TabIndex = 27;
            this.label20.Text = "8";
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.comboBox1.Location = new System.Drawing.Point(674, 110);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(35, 24);
            this.comboBox1.TabIndex = 28;
            this.comboBox1.Text = "2";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("MS UI Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label21.Location = new System.Drawing.Point(78, 458);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(71, 27);
            this.label21.TabIndex = 29;
            this.label21.Text = "手番:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 529);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label21;
    }
}


namespace WinFormsAppV3FlorenBooksV3
{
    partial class AdaugaCarteForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            textBox1 = new System.Windows.Forms.TextBox();
            textBox2 = new System.Windows.Forms.TextBox();
            textBox3 = new System.Windows.Forms.TextBox();
            textBox4 = new System.Windows.Forms.TextBox();
            textBox5 = new System.Windows.Forms.TextBox();
            button1 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            buttonChooseCover = new System.Windows.Forms.Button();
            pictureBoxCover = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxCover).BeginInit();
            SuspendLayout();
            //
            // label1
            //
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(75, 11);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(77, 15);
            label1.TabIndex = 0;
            label1.Text = "Adauga carte";
            //
            // label2
            //
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(20, 56);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(30, 15);
            label2.TabIndex = 0;
            label2.Text = "Titlu";
            //
            // label3
            //
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(20, 95);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(37, 15);
            label3.TabIndex = 0;
            label3.Text = "Autor";
            //
            // label4
            //
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(20, 132);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(44, 15);
            label4.TabIndex = 0;
            label4.Text = "Editura";
            //
            // label5
            //
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(20, 166);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(32, 15);
            label5.TabIndex = 0;
            label5.Text = "Anul";
            //
            // label6
            //
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(20, 205);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(28, 15);
            label6.TabIndex = 0;
            label6.Text = "Pret";
            //
            // textBox1
            //
            textBox1.Location = new System.Drawing.Point(90, 51);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(150, 23);
            textBox1.TabIndex = 1;
            //
            // textBox2
            //
            textBox2.Location = new System.Drawing.Point(90, 91);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(150, 23);
            textBox2.TabIndex = 1;
            //
            // textBox3
            //
            textBox3.Location = new System.Drawing.Point(90, 126);
            textBox3.Name = "textBox3";
            textBox3.Size = new System.Drawing.Size(150, 23);
            textBox3.TabIndex = 1;
            //
            // textBox4
            //
            textBox4.Location = new System.Drawing.Point(90, 162);
            textBox4.Name = "textBox4";
            textBox4.Size = new System.Drawing.Size(150, 23);
            textBox4.TabIndex = 1;
            //
            // textBox5
            //
            textBox5.Location = new System.Drawing.Point(90, 200);
            textBox5.Name = "textBox5";
            textBox5.Size = new System.Drawing.Size(150, 23);
            textBox5.TabIndex = 1;
            //
            // button1
            //
            button1.Location = new System.Drawing.Point(20, 264);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(90, 28);
            button1.TabIndex = 2;
            button1.Text = "Adauga";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            //
            // button2
            //
            button2.Location = new System.Drawing.Point(130, 264);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(75, 28);
            button2.TabIndex = 2;
            button2.Text = "Reset";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            //
            // buttonChooseCover
            //
            buttonChooseCover.Location = new System.Drawing.Point(20, 226);
            buttonChooseCover.Name = "buttonChooseCover";
            buttonChooseCover.Size = new System.Drawing.Size(140, 23);
            buttonChooseCover.TabIndex = 3;
            buttonChooseCover.Text = "Alege coperta...";
            buttonChooseCover.UseVisualStyleBackColor = true;
            buttonChooseCover.Click += buttonChooseCover_Click;
            //
            // pictureBoxCover
            //
            pictureBoxCover.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pictureBoxCover.Location = new System.Drawing.Point(250, 51);
            pictureBoxCover.Name = "pictureBoxCover";
            pictureBoxCover.Size = new System.Drawing.Size(58, 92);
            pictureBoxCover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBoxCover.TabIndex = 5;
            pictureBoxCover.TabStop = false;
            //
            // AdaugaCarteForm
            //
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(330, 310);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(label5);
            Controls.Add(label6);
            Controls.Add(textBox1);
            Controls.Add(textBox2);
            Controls.Add(textBox3);
            Controls.Add(textBox4);
            Controls.Add(textBox5);
            Controls.Add(button1);
            Controls.Add(button2);
            Controls.Add(buttonChooseCover);
            Controls.Add(pictureBoxCover);
            Name = "AdaugaCarteForm";
            Text = "Adauga Carte";
            ((System.ComponentModel.ISupportInitialize)pictureBoxCover).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonChooseCover;
        private System.Windows.Forms.PictureBox pictureBoxCover;
    }
}

namespace WinFormsAppV3FlorenBooksV3
{
    partial class Librarydashboard
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            textBox5 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            buttonRefreshBooks = new Button();
            dataGridViewBooks = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            Titlu = new DataGridViewTextBoxColumn();
            Autor = new DataGridViewTextBoxColumn();
            Editura = new DataGridViewTextBoxColumn();
            Anul = new DataGridViewTextBoxColumn();
            Pret = new DataGridViewTextBoxColumn();
            Status = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridViewBooks).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(155, 52);
            label1.Name = "label1";
            label1.Size = new Size(77, 15);
            label1.TabIndex = 0;
            label1.Text = "Adauga carte";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(72, 97);
            label2.Name = "label2";
            label2.Size = new Size(30, 15);
            label2.TabIndex = 0;
            label2.Text = "Titlu";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(72, 136);
            label3.Name = "label3";
            label3.Size = new Size(37, 15);
            label3.TabIndex = 0;
            label3.Text = "Autor";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(72, 173);
            label4.Name = "label4";
            label4.Size = new Size(44, 15);
            label4.TabIndex = 0;
            label4.Text = "Editura";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(72, 207);
            label5.Name = "label5";
            label5.Size = new Size(32, 15);
            label5.TabIndex = 0;
            label5.Text = "Anul";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(72, 246);
            label6.Name = "label6";
            label6.Size = new Size(28, 15);
            label6.TabIndex = 0;
            label6.Text = "Pret";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(142, 92);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 1;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(142, 132);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 1;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(142, 167);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(100, 23);
            textBox3.TabIndex = 1;
            textBox3.TextChanged += textBox3_TextChanged;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(142, 203);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(100, 23);
            textBox4.TabIndex = 1;
            textBox4.TextChanged += textBox4_TextChanged;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(142, 241);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(100, 23);
            textBox5.TabIndex = 1;
            textBox5.TextChanged += textBox5_TextChanged;
            // 
            // button1
            // 
            button1.Location = new Point(86, 295);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 2;
            button1.Text = "Adauga";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(191, 295);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 2;
            button2.Text = "Reset";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // buttonRefreshBooks
            // 
            buttonRefreshBooks.Location = new Point(334, 12);
            buttonRefreshBooks.Name = "buttonRefreshBooks";
            buttonRefreshBooks.Size = new Size(90, 23);
            buttonRefreshBooks.TabIndex = 3;
            buttonRefreshBooks.Text = "Refresh";
            buttonRefreshBooks.UseVisualStyleBackColor = true;
            buttonRefreshBooks.Click += buttonRefreshBooks_Click;
            // 
            // dataGridViewBooks
            // 
            dataGridViewBooks.AllowUserToAddRows = false;
            dataGridViewBooks.AllowUserToDeleteRows = false;
            dataGridViewBooks.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewBooks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewBooks.Columns.AddRange(new DataGridViewColumn[] { colId, Titlu, Autor, Editura, Anul, Pret, Status });
            dataGridViewBooks.Location = new Point(334, 41);
            dataGridViewBooks.Name = "dataGridViewBooks";
            dataGridViewBooks.ReadOnly = true;
            dataGridViewBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewBooks.Size = new Size(554, 290);
            dataGridViewBooks.TabIndex = 4;
            // 
            // colId
            // 
            colId.HeaderText = "Id";
            colId.Name = "colId";
            colId.ReadOnly = true;
            colId.Visible = false;
            // 
            // Titlu
            // 
            Titlu.HeaderText = "Titlu";
            Titlu.Name = "Titlu";
            Titlu.ReadOnly = true;
            // 
            // Autor
            // 
            Autor.HeaderText = "Autor";
            Autor.Name = "Autor";
            Autor.ReadOnly = true;
            // 
            // Editura
            // 
            Editura.HeaderText = "Editura";
            Editura.Name = "Editura";
            Editura.ReadOnly = true;
            // 
            // Anul
            // 
            Anul.HeaderText = "Anul";
            Anul.Name = "Anul";
            Anul.ReadOnly = true;
            // 
            // Pret
            // 
            Pret.HeaderText = "Pret";
            Pret.Name = "Pret";
            Pret.ReadOnly = true;
            // 
            // Status
            // 
            Status.HeaderText = "Status";
            Status.Name = "Status";
            Status.ReadOnly = true;
            // 
            // Librarydashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 343);
            Controls.Add(dataGridViewBooks);
            Controls.Add(buttonRefreshBooks);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox5);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Librarydashboard";
            Text = "Librarydashboard";
            Load += Librarydashboard_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewBooks).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private TextBox textBox5;
        private Button button1;
        private Button button2;
        private Button buttonRefreshBooks;
        private DataGridView dataGridViewBooks;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn Titlu;
        private DataGridViewTextBoxColumn Autor;
        private DataGridViewTextBoxColumn Editura;
        private DataGridViewTextBoxColumn Anul;
        private DataGridViewTextBoxColumn Pret;
        private DataGridViewTextBoxColumn Status;
    }
}

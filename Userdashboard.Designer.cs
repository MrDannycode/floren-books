namespace WinFormsAppV3FlorenBooksV3
{
    partial class Userdashboard
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
            dataGridView1 = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            Titlu = new DataGridViewTextBoxColumn();
            Autor = new DataGridViewTextBoxColumn();
            Editura = new DataGridViewTextBoxColumn();
            Anul = new DataGridViewTextBoxColumn();
            Pret = new DataGridViewTextBoxColumn();
            colBuy = new DataGridViewButtonColumn();
            colBorrow = new DataGridViewButtonColumn();
            buttonExportCsv = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { colId, Titlu, Autor, Editura, Anul, Pret, colBuy, colBorrow });
            dataGridView1.Location = new Point(12, 41);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(776, 397);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
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
            // colBuy
            // 
            colBuy.HeaderText = "Buy";
            colBuy.Name = "colBuy";
            colBuy.ReadOnly = true;
            colBuy.Text = "Buy";
            colBuy.UseColumnTextForButtonValue = true;
            // 
            // colBorrow
            // 
            colBorrow.HeaderText = "Borrow";
            colBorrow.Name = "colBorrow";
            colBorrow.ReadOnly = true;
            colBorrow.Text = "Borrow";
            colBorrow.UseColumnTextForButtonValue = true;
            // 
            // buttonExportCsv
            // 
            buttonExportCsv.Location = new Point(12, 12);
            buttonExportCsv.Name = "buttonExportCsv";
            buttonExportCsv.Size = new Size(90, 23);
            buttonExportCsv.TabIndex = 1;
            buttonExportCsv.Text = "Export CSV";
            buttonExportCsv.UseVisualStyleBackColor = true;
            buttonExportCsv.Click += buttonExportCsv_Click;
            // 
            // Userdashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonExportCsv);
            Controls.Add(dataGridView1);
            Name = "Userdashboard";
            Text = "Userdashboard";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn Titlu;
        private DataGridViewTextBoxColumn Autor;
        private DataGridViewTextBoxColumn Editura;
        private DataGridViewTextBoxColumn Anul;
        private DataGridViewTextBoxColumn Pret;
        private DataGridViewButtonColumn colBuy;
        private DataGridViewButtonColumn colBorrow;
        private Button buttonExportCsv;
    }
}

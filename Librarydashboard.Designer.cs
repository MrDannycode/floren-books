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
            buttonAdaugaCarte = new Button();
            buttonRefreshBooks = new Button();
            dataGridViewBooks = new DataGridView();
            Coperta = new DataGridViewImageColumn();
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
            // buttonAdaugaCarte
            //
            buttonAdaugaCarte.Location = new Point(12, 12);
            buttonAdaugaCarte.Name = "buttonAdaugaCarte";
            buttonAdaugaCarte.Size = new Size(105, 23);
            buttonAdaugaCarte.TabIndex = 6;
            buttonAdaugaCarte.Text = "Adauga carte";
            buttonAdaugaCarte.UseVisualStyleBackColor = true;
            buttonAdaugaCarte.Click += buttonAdaugaCarte_Click;
            //
            // buttonRefreshBooks
            //
            buttonRefreshBooks.Location = new Point(123, 12);
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
            dataGridViewBooks.Columns.AddRange(new DataGridViewColumn[] { Coperta, colId, Titlu, Autor, Editura, Anul, Pret, Status });
            dataGridViewBooks.Location = new Point(12, 41);
            dataGridViewBooks.Name = "dataGridViewBooks";
            dataGridViewBooks.ReadOnly = true;
            dataGridViewBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewBooks.Size = new Size(876, 290);
            dataGridViewBooks.TabIndex = 4;
            dataGridViewBooks.RowTemplate.Height = 60;
            //
            // Coperta
            //
            Coperta.HeaderText = "Coperta";
            Coperta.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Coperta.Name = "Coperta";
            Coperta.ReadOnly = true;
            Coperta.Width = 70;
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
            Controls.Add(buttonAdaugaCarte);
            Controls.Add(buttonRefreshBooks);
            Name = "Librarydashboard";
            Text = "Librarydashboard";
            Load += Librarydashboard_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewBooks).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button buttonAdaugaCarte;
        private Button buttonRefreshBooks;
        private DataGridView dataGridViewBooks;
        private DataGridViewImageColumn Coperta;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn Titlu;
        private DataGridViewTextBoxColumn Autor;
        private DataGridViewTextBoxColumn Editura;
        private DataGridViewTextBoxColumn Anul;
        private DataGridViewTextBoxColumn Pret;
        private DataGridViewTextBoxColumn Status;
    }
}

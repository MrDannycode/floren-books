namespace WinFormsAppV3FlorenBooksV3
{
    partial class Borrowdashboard
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colBookId = new DataGridViewTextBoxColumn();
            UserEmail = new DataGridViewTextBoxColumn();
            BookTitle = new DataGridViewTextBoxColumn();
            BookAuthor = new DataGridViewTextBoxColumn();
            BorrowDate = new DataGridViewTextBoxColumn();
            ReturnDate = new DataGridViewTextBoxColumn();
            Status = new DataGridViewTextBoxColumn();
            colReturn = new DataGridViewButtonColumn();
            buttonRefresh = new Button();
            buttonExportCsv = new Button();
            buttonSetExemplare = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { colId, colBookId, UserEmail, BookTitle, BookAuthor, BorrowDate, ReturnDate, Status, colReturn });
            dataGridView1.Location = new Point(0, 41);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(900, 409);
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
            // colBookId
            // 
            colBookId.HeaderText = "BookId";
            colBookId.Name = "colBookId";
            colBookId.ReadOnly = true;
            colBookId.Visible = false;
            // 
            // UserEmail
            // 
            UserEmail.FillWeight = 24F;
            UserEmail.HeaderText = "User Email";
            UserEmail.Name = "UserEmail";
            UserEmail.ReadOnly = true;
            // 
            // BookTitle
            // 
            BookTitle.FillWeight = 24F;
            BookTitle.HeaderText = "Book Title";
            BookTitle.Name = "BookTitle";
            BookTitle.ReadOnly = true;
            // 
            // BookAuthor
            // 
            BookAuthor.FillWeight = 18F;
            BookAuthor.HeaderText = "Author";
            BookAuthor.Name = "BookAuthor";
            BookAuthor.ReadOnly = true;
            // 
            // BorrowDate
            // 
            BorrowDate.FillWeight = 14F;
            BorrowDate.HeaderText = "Borrow Date";
            BorrowDate.Name = "BorrowDate";
            BorrowDate.ReadOnly = true;
            // 
            // ReturnDate
            // 
            ReturnDate.FillWeight = 14F;
            ReturnDate.HeaderText = "Return Date";
            ReturnDate.Name = "ReturnDate";
            ReturnDate.ReadOnly = true;
            // 
            // Status
            // 
            Status.FillWeight = 10F;
            Status.HeaderText = "Status";
            Status.Name = "Status";
            Status.ReadOnly = true;
            // 
            // colReturn
            // 
            colReturn.FillWeight = 10F;
            colReturn.HeaderText = "Action";
            colReturn.Name = "colReturn";
            colReturn.ReadOnly = true;
            // 
            // buttonRefresh
            // 
            buttonRefresh.Location = new Point(12, 12);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new Size(90, 23);
            buttonRefresh.TabIndex = 1;
            buttonRefresh.Text = "Refresh";
            buttonRefresh.UseVisualStyleBackColor = true;
            buttonRefresh.Click += buttonRefresh_Click;
            // 
            // buttonExportCsv
            // 
            buttonExportCsv.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonExportCsv.Location = new Point(595, 12);
            buttonExportCsv.Name = "buttonExportCsv";
            buttonExportCsv.Size = new Size(90, 23);
            buttonExportCsv.TabIndex = 2;
            buttonExportCsv.Text = "Export CSV";
            buttonExportCsv.UseVisualStyleBackColor = true;
            buttonExportCsv.Click += buttonExportCsv_Click;
            // 
            // buttonSetExemplare
            // 
            buttonSetExemplare.Location = new Point(116, 12);
            buttonSetExemplare.Name = "buttonSetExemplare";
            buttonSetExemplare.Size = new Size(140, 23);
            buttonSetExemplare.TabIndex = 3;
            buttonSetExemplare.Text = "Setează Exemplare";
            buttonSetExemplare.UseVisualStyleBackColor = true;
            buttonSetExemplare.Click += buttonSetExemplare_Click;
            // 
            // Borrowdashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 450);
            Controls.Add(buttonExportCsv);
            Controls.Add(buttonRefresh);
            Controls.Add(buttonSetExemplare);
            Controls.Add(dataGridView1);
            Name = "Borrowdashboard";
            Text = "Borrowdashboard";
            Load += Borrowdashboard_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colBookId;
        private DataGridViewTextBoxColumn UserEmail;
        private DataGridViewTextBoxColumn BookTitle;
        private DataGridViewTextBoxColumn BookAuthor;
        private DataGridViewTextBoxColumn BorrowDate;
        private DataGridViewTextBoxColumn ReturnDate;
        private DataGridViewTextBoxColumn Status;
        private DataGridViewButtonColumn colReturn;
        private Button buttonRefresh;
        private Button buttonExportCsv;
        private Button buttonSetExemplare;
    }
}

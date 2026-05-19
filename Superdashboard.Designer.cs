namespace WinFormsAppV3FlorenBooksV3
{
    partial class Superdashboard
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
            Email = new DataGridViewTextBoxColumn();
            role = new DataGridViewTextBoxColumn();
            colEditEmail = new DataGridViewButtonColumn();
            colChangeRole = new DataGridViewButtonColumn();
            colDelete = new DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { colId, Email, role, colEditEmail, colChangeRole, colDelete });
            dataGridView1.Location = new Point(-1, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(800, 449);
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
            // Email
            // 
            Email.FillWeight = 45F;
            Email.HeaderText = "Email";
            Email.Name = "Email";
            Email.ReadOnly = true;
            // 
            // role
            // 
            role.FillWeight = 25F;
            role.HeaderText = "Role";
            role.Name = "role";
            role.ReadOnly = true;
            // 
            // colEditEmail
            // 
            colEditEmail.FillWeight = 10F;
            colEditEmail.HeaderText = "Edit Email";
            colEditEmail.Name = "colEditEmail";
            colEditEmail.ReadOnly = true;
            colEditEmail.Text = "Edit";
            colEditEmail.UseColumnTextForButtonValue = true;
            // 
            // colChangeRole
            // 
            colChangeRole.FillWeight = 10F;
            colChangeRole.HeaderText = "Change Role";
            colChangeRole.Name = "colChangeRole";
            colChangeRole.ReadOnly = true;
            colChangeRole.Text = "Role";
            colChangeRole.UseColumnTextForButtonValue = true;
            // 
            // colDelete
            // 
            colDelete.FillWeight = 10F;
            colDelete.HeaderText = "Delete";
            colDelete.Name = "colDelete";
            colDelete.ReadOnly = true;
            colDelete.Text = "Delete";
            colDelete.UseColumnTextForButtonValue = true;
            // 
            // Superdashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridView1);
            Name = "Superdashboard";
            Text = "Superdashboard";
            Load += Superdashboard_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn Email;
        private DataGridViewTextBoxColumn role;
        private DataGridViewButtonColumn colEditEmail;
        private DataGridViewButtonColumn colChangeRole;
        private DataGridViewButtonColumn colDelete;
    }
}
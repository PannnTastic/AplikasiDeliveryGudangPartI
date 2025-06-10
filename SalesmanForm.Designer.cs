namespace DeliveryApp
{
    partial class SalesmanForm
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
            this.dataGridViewSalesman = new System.Windows.Forms.DataGridView();
            this.textBoxSalesmanName = new System.Windows.Forms.TextBox();
            this.labelSalesmanName = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.labelPhone = new System.Windows.Forms.Label();
            this.textBoxPhone = new System.Windows.Forms.TextBox();
            this.buttonRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSalesman)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewSalesman
            // 
            this.dataGridViewSalesman.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSalesman.Location = new System.Drawing.Point(20, 20);
            this.dataGridViewSalesman.Name = "dataGridViewSalesman";
            this.dataGridViewSalesman.RowHeadersWidth = 51;
            this.dataGridViewSalesman.RowTemplate.Height = 24;
            this.dataGridViewSalesman.Size = new System.Drawing.Size(555, 200);
            this.dataGridViewSalesman.TabIndex = 0;
            this.dataGridViewSalesman.SelectionChanged += new System.EventHandler(this.DataGridViewSalesman_SelectionChanged);
            // 
            // textBoxSalesmanName
            // 
            this.textBoxSalesmanName.Location = new System.Drawing.Point(20, 240);
            this.textBoxSalesmanName.Name = "textBoxSalesmanName";
            this.textBoxSalesmanName.Size = new System.Drawing.Size(400, 22);
            this.textBoxSalesmanName.TabIndex = 1;
            // 
            // labelSalesmanName
            // 
            this.labelSalesmanName.AutoSize = true;
            this.labelSalesmanName.Location = new System.Drawing.Point(20, 220);
            this.labelSalesmanName.Name = "labelSalesmanName";
            this.labelSalesmanName.Size = new System.Drawing.Size(111, 16);
            this.labelSalesmanName.TabIndex = 2;
            this.labelSalesmanName.Text = "Salesman Name:";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(20, 357);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(100, 40);
            this.buttonAdd.TabIndex = 3;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(140, 357);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(100, 40);
            this.buttonUpdate.TabIndex = 4;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.ButtonUpdate_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(260, 357);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(100, 40);
            this.buttonDelete.TabIndex = 5;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // labelPhone
            // 
            this.labelPhone.AutoSize = true;
            this.labelPhone.Location = new System.Drawing.Point(20, 270);
            this.labelPhone.Name = "labelPhone";
            this.labelPhone.Size = new System.Drawing.Size(49, 16);
            this.labelPhone.TabIndex = 6;
            this.labelPhone.Text = "Phone:";
            // 
            // textBoxPhone
            // 
            this.textBoxPhone.Location = new System.Drawing.Point(20, 290);
            this.textBoxPhone.Name = "textBoxPhone";
            this.textBoxPhone.Size = new System.Drawing.Size(400, 22);
            this.textBoxPhone.TabIndex = 7;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(383, 357);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(103, 40);
            this.buttonRefresh.TabIndex = 8;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // SalesmanForm
            // 
            this.ClientSize = new System.Drawing.Size(592, 434);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.labelSalesmanName);
            this.Controls.Add(this.textBoxSalesmanName);
            this.Controls.Add(this.dataGridViewSalesman);
            this.Controls.Add(this.labelPhone);
            this.Controls.Add(this.textBoxPhone);
            this.Name = "SalesmanForm";
            this.Text = "Manage Salesman";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSalesman)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.DataGridView dataGridViewSalesman;
        private System.Windows.Forms.TextBox textBoxSalesmanName;
        private System.Windows.Forms.Label labelSalesmanName;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Label labelPhone;
        private System.Windows.Forms.TextBox textBoxPhone;
        private System.Windows.Forms.Button buttonRefresh;
    }
}

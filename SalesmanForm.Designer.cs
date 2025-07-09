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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalesmanForm));
            this.dataGridViewSalesman = new System.Windows.Forms.DataGridView();
            this.textBoxSalesmanName = new System.Windows.Forms.TextBox();
            this.labelSalesmanName = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.labelPhone = new System.Windows.Forms.Label();
            this.textBoxPhone = new System.Windows.Forms.TextBox();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonExportCsv = new System.Windows.Forms.Button();
            this.buttonExportPdf = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.searchBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSalesman)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewSalesman
            // 
            this.dataGridViewSalesman.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSalesman.Location = new System.Drawing.Point(20, 64);
            this.dataGridViewSalesman.Name = "dataGridViewSalesman";
            this.dataGridViewSalesman.ReadOnly = true;
            this.dataGridViewSalesman.RowHeadersWidth = 51;
            this.dataGridViewSalesman.RowTemplate.Height = 24;
            this.dataGridViewSalesman.Size = new System.Drawing.Size(555, 200);
            this.dataGridViewSalesman.TabIndex = 0;
            this.dataGridViewSalesman.SelectionChanged += new System.EventHandler(this.DataGridViewSalesman_SelectionChanged);
            // 
            // textBoxSalesmanName
            // 
            this.textBoxSalesmanName.Location = new System.Drawing.Point(20, 284);
            this.textBoxSalesmanName.Name = "textBoxSalesmanName";
            this.textBoxSalesmanName.Size = new System.Drawing.Size(400, 25);
            this.textBoxSalesmanName.TabIndex = 1;
            // 
            // labelSalesmanName
            // 
            this.labelSalesmanName.AutoSize = true;
            this.labelSalesmanName.BackColor = System.Drawing.Color.Transparent;
            this.labelSalesmanName.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSalesmanName.ForeColor = System.Drawing.Color.Black;
            this.labelSalesmanName.Location = new System.Drawing.Point(20, 264);
            this.labelSalesmanName.Name = "labelSalesmanName";
            this.labelSalesmanName.Size = new System.Drawing.Size(105, 17);
            this.labelSalesmanName.TabIndex = 2;
            this.labelSalesmanName.Text = "Salesman Name:";
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackColor = System.Drawing.Color.LawnGreen;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonAdd.Location = new System.Drawing.Point(20, 412);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(100, 40);
            this.buttonAdd.TabIndex = 3;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.BackColor = System.Drawing.Color.CornflowerBlue;
            this.buttonUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonUpdate.Location = new System.Drawing.Point(140, 412);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(100, 40);
            this.buttonUpdate.TabIndex = 4;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = false;
            this.buttonUpdate.Click += new System.EventHandler(this.ButtonUpdate_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackColor = System.Drawing.Color.Tomato;
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonDelete.Location = new System.Drawing.Point(260, 412);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(100, 40);
            this.buttonDelete.TabIndex = 5;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // labelPhone
            // 
            this.labelPhone.AutoSize = true;
            this.labelPhone.BackColor = System.Drawing.Color.Transparent;
            this.labelPhone.Location = new System.Drawing.Point(20, 314);
            this.labelPhone.Name = "labelPhone";
            this.labelPhone.Size = new System.Drawing.Size(47, 17);
            this.labelPhone.TabIndex = 6;
            this.labelPhone.Text = "Phone:";
            // 
            // textBoxPhone
            // 
            this.textBoxPhone.Location = new System.Drawing.Point(20, 334);
            this.textBoxPhone.Name = "textBoxPhone";
            this.textBoxPhone.Size = new System.Drawing.Size(400, 25);
            this.textBoxPhone.TabIndex = 7;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.BackColor = System.Drawing.Color.Yellow;
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonRefresh.Location = new System.Drawing.Point(383, 412);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(103, 40);
            this.buttonRefresh.TabIndex = 8;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = false;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // buttonExportCsv
            // 
            this.buttonExportCsv.BackColor = System.Drawing.Color.Transparent;
            this.buttonExportCsv.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonExportCsv.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.buttonExportCsv.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonExportCsv.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lime;
            this.buttonExportCsv.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonExportCsv.ForeColor = System.Drawing.Color.Black;
            this.buttonExportCsv.Location = new System.Drawing.Point(490, 334);
            this.buttonExportCsv.Name = "buttonExportCsv";
            this.buttonExportCsv.Size = new System.Drawing.Size(85, 40);
            this.buttonExportCsv.TabIndex = 9;
            this.buttonExportCsv.Text = "Export CSV";
            this.buttonExportCsv.UseVisualStyleBackColor = false;
            this.buttonExportCsv.Click += new System.EventHandler(this.buttonExportCsv_Click);
            // 
            // buttonExportPdf
            // 
            this.buttonExportPdf.BackColor = System.Drawing.Color.Transparent;
            this.buttonExportPdf.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.buttonExportPdf.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.buttonExportPdf.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonExportPdf.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonExportPdf.Location = new System.Drawing.Point(490, 284);
            this.buttonExportPdf.Name = "buttonExportPdf";
            this.buttonExportPdf.Size = new System.Drawing.Size(85, 40);
            this.buttonExportPdf.TabIndex = 10;
            this.buttonExportPdf.Text = "Export PDF";
            this.buttonExportPdf.UseVisualStyleBackColor = false;
            this.buttonExportPdf.Click += new System.EventHandler(this.buttonExportPdf_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(19, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(19, 22);
            this.pictureBox1.TabIndex = 25;
            this.pictureBox1.TabStop = false;
            // 
            // searchBox
            // 
            this.searchBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.searchBox.CausesValidation = false;
            this.searchBox.Location = new System.Drawing.Point(44, 25);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(245, 25);
            this.searchBox.TabIndex = 24;
            this.searchBox.Text = "Search";
            this.searchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged);
            // 
            // SalesmanForm
            // 
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(592, 472);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.buttonExportPdf);
            this.Controls.Add(this.buttonExportCsv);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.labelSalesmanName);
            this.Controls.Add(this.textBoxSalesmanName);
            this.Controls.Add(this.dataGridViewSalesman);
            this.Controls.Add(this.labelPhone);
            this.Controls.Add(this.textBoxPhone);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SalesmanForm";
            this.Text = "Manage Salesman";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SalesmanForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSalesman)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private System.Windows.Forms.Button buttonExportCsv;
        private System.Windows.Forms.Button buttonExportPdf;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox searchBox;
    }
}

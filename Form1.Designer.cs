namespace DeliveryApp
{
    partial class Delivery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Delivery));
            this.groupBoxInput = new System.Windows.Forms.GroupBox();
            this.labelDeliveryDate = new System.Windows.Forms.Label();
            this.dateTimePickerDeliveryDate = new System.Windows.Forms.DateTimePicker();
            this.labelSalesmanId = new System.Windows.Forms.Label();
            this.comboBoxSalesmanId = new System.Windows.Forms.ComboBox();
            this.labelProductId = new System.Windows.Forms.Label();
            this.comboBoxProductId = new System.Windows.Forms.ComboBox();
            this.labelQuantity = new System.Windows.Forms.Label();
            this.numericUpDownQuantity = new System.Windows.Forms.NumericUpDown();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.dataGridViewDelivery = new System.Windows.Forms.DataGridView();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonOpenSalesmanForm = new System.Windows.Forms.Button();
            this.buttonOpenProductForm = new System.Windows.Forms.Button();
            this.buttonExportReport = new System.Windows.Forms.Button();
            this.buttonAnalyzeQuery = new System.Windows.Forms.Button();
            this.buttonExportPdf = new System.Windows.Forms.Button();
            this.buttonOpenReportDelivery = new System.Windows.Forms.Button();
            this.buttonOpenReportProducts = new System.Windows.Forms.Button();
            this.buttonOpenReportSalesman = new System.Windows.Forms.Button();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBoxInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDelivery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxInput
            // 
            this.groupBoxInput.Controls.Add(this.labelDeliveryDate);
            this.groupBoxInput.Controls.Add(this.dateTimePickerDeliveryDate);
            this.groupBoxInput.Controls.Add(this.labelSalesmanId);
            this.groupBoxInput.Controls.Add(this.comboBoxSalesmanId);
            this.groupBoxInput.Controls.Add(this.labelProductId);
            this.groupBoxInput.Controls.Add(this.comboBoxProductId);
            this.groupBoxInput.Controls.Add(this.labelQuantity);
            this.groupBoxInput.Controls.Add(this.numericUpDownQuantity);
            this.groupBoxInput.Controls.Add(this.buttonSubmit);
            this.groupBoxInput.Controls.Add(this.buttonUpdate);
            this.groupBoxInput.Controls.Add(this.buttonDelete);
            this.groupBoxInput.Location = new System.Drawing.Point(20, 20);
            this.groupBoxInput.Name = "groupBoxInput";
            this.groupBoxInput.Size = new System.Drawing.Size(417, 289);
            this.groupBoxInput.TabIndex = 0;
            this.groupBoxInput.TabStop = false;
            this.groupBoxInput.Text = "Delivery Details";
            // 
            // labelDeliveryDate
            // 
            this.labelDeliveryDate.AutoSize = true;
            this.labelDeliveryDate.Location = new System.Drawing.Point(29, 42);
            this.labelDeliveryDate.Name = "labelDeliveryDate";
            this.labelDeliveryDate.Size = new System.Drawing.Size(89, 16);
            this.labelDeliveryDate.TabIndex = 2;
            this.labelDeliveryDate.Text = "Delivery Date";
            // 
            // dateTimePickerDeliveryDate
            // 
            this.dateTimePickerDeliveryDate.CustomFormat = "dd-MM-yyyy HH:mm";
            this.dateTimePickerDeliveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDeliveryDate.Location = new System.Drawing.Point(159, 42);
            this.dateTimePickerDeliveryDate.Name = "dateTimePickerDeliveryDate";
            this.dateTimePickerDeliveryDate.ShowUpDown = true;
            this.dateTimePickerDeliveryDate.Size = new System.Drawing.Size(200, 22);
            this.dateTimePickerDeliveryDate.TabIndex = 3;
            // 
            // labelSalesmanId
            // 
            this.labelSalesmanId.AutoSize = true;
            this.labelSalesmanId.Location = new System.Drawing.Point(29, 82);
            this.labelSalesmanId.Name = "labelSalesmanId";
            this.labelSalesmanId.Size = new System.Drawing.Size(84, 16);
            this.labelSalesmanId.TabIndex = 4;
            this.labelSalesmanId.Text = "Salesman ID";
            // 
            // comboBoxSalesmanId
            // 
            this.comboBoxSalesmanId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSalesmanId.FormattingEnabled = true;
            this.comboBoxSalesmanId.Location = new System.Drawing.Point(159, 82);
            this.comboBoxSalesmanId.Name = "comboBoxSalesmanId";
            this.comboBoxSalesmanId.Size = new System.Drawing.Size(200, 24);
            this.comboBoxSalesmanId.TabIndex = 5;
            // 
            // labelProductId
            // 
            this.labelProductId.AutoSize = true;
            this.labelProductId.Location = new System.Drawing.Point(29, 122);
            this.labelProductId.Name = "labelProductId";
            this.labelProductId.Size = new System.Drawing.Size(69, 16);
            this.labelProductId.TabIndex = 6;
            this.labelProductId.Text = "Product ID";
            // 
            // comboBoxProductId
            // 
            this.comboBoxProductId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProductId.FormattingEnabled = true;
            this.comboBoxProductId.Location = new System.Drawing.Point(159, 122);
            this.comboBoxProductId.Name = "comboBoxProductId";
            this.comboBoxProductId.Size = new System.Drawing.Size(200, 24);
            this.comboBoxProductId.TabIndex = 7;
            // 
            // labelQuantity
            // 
            this.labelQuantity.AutoSize = true;
            this.labelQuantity.Location = new System.Drawing.Point(29, 162);
            this.labelQuantity.Name = "labelQuantity";
            this.labelQuantity.Size = new System.Drawing.Size(55, 16);
            this.labelQuantity.TabIndex = 8;
            this.labelQuantity.Text = "Quantity";
            // 
            // numericUpDownQuantity
            // 
            this.numericUpDownQuantity.Location = new System.Drawing.Point(159, 162);
            this.numericUpDownQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownQuantity.Name = "numericUpDownQuantity";
            this.numericUpDownQuantity.Size = new System.Drawing.Size(200, 22);
            this.numericUpDownQuantity.TabIndex = 9;
            this.numericUpDownQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.BackColor = System.Drawing.Color.LightGreen;
            this.buttonSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSubmit.Location = new System.Drawing.Point(20, 230);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(95, 30);
            this.buttonSubmit.TabIndex = 10;
            this.buttonSubmit.Text = "Submit";
            this.buttonSubmit.UseVisualStyleBackColor = true;
            this.buttonSubmit.Click += new System.EventHandler(this.ButtonSubmit_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.BackColor = System.Drawing.Color.LightSkyBlue;
            this.buttonUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonUpdate.Location = new System.Drawing.Point(150, 230);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(95, 30);
            this.buttonUpdate.TabIndex = 11;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.ButtonUpdate_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackColor = System.Drawing.Color.Salmon;
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.Location = new System.Drawing.Point(280, 230);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(95, 30);
            this.buttonDelete.TabIndex = 12;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // dataGridViewDelivery
            // 
            this.dataGridViewDelivery.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewDelivery.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDelivery.Location = new System.Drawing.Point(20, 389);
            this.dataGridViewDelivery.Name = "dataGridViewDelivery";
            this.dataGridViewDelivery.RowHeadersWidth = 51;
            this.dataGridViewDelivery.RowTemplate.Height = 24;
            this.dataGridViewDelivery.Size = new System.Drawing.Size(760, 200);
            this.dataGridViewDelivery.TabIndex = 13;
            this.dataGridViewDelivery.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewDelivery_CellClick);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.BackColor = System.Drawing.Color.LightYellow;
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefresh.Location = new System.Drawing.Point(20, 599);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(95, 30);
            this.buttonRefresh.TabIndex = 13;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.ButtonRefresh_Click);
            // 
            // buttonOpenSalesmanForm
            // 
            this.buttonOpenSalesmanForm.Location = new System.Drawing.Point(618, 279);
            this.buttonOpenSalesmanForm.Name = "buttonOpenSalesmanForm";
            this.buttonOpenSalesmanForm.Size = new System.Drawing.Size(150, 30);
            this.buttonOpenSalesmanForm.TabIndex = 14;
            this.buttonOpenSalesmanForm.Text = "Manage Salesman";
            this.buttonOpenSalesmanForm.Click += new System.EventHandler(this.ButtonOpenSalesmanForm_Click);
            // 
            // buttonOpenProductForm
            // 
            this.buttonOpenProductForm.Location = new System.Drawing.Point(618, 233);
            this.buttonOpenProductForm.Name = "buttonOpenProductForm";
            this.buttonOpenProductForm.Size = new System.Drawing.Size(150, 30);
            this.buttonOpenProductForm.TabIndex = 15;
            this.buttonOpenProductForm.Text = "Manage Products";
            this.buttonOpenProductForm.Click += new System.EventHandler(this.ButtonOpenProductForm_Click);
            // 
            // buttonExportReport
            // 
            this.buttonExportReport.BackColor = System.Drawing.Color.Lime;
            this.buttonExportReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExportReport.Location = new System.Drawing.Point(685, 599);
            this.buttonExportReport.Name = "buttonExportReport";
            this.buttonExportReport.Size = new System.Drawing.Size(95, 30);
            this.buttonExportReport.TabIndex = 16;
            this.buttonExportReport.Text = "Export CSV";
            this.buttonExportReport.UseVisualStyleBackColor = false;
            this.buttonExportReport.Click += new System.EventHandler(this.ButtonExportReport_Click);
            // 
            // buttonAnalyzeQuery
            // 
            this.buttonAnalyzeQuery.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonAnalyzeQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAnalyzeQuery.Location = new System.Drawing.Point(361, 599);
            this.buttonAnalyzeQuery.Name = "buttonAnalyzeQuery";
            this.buttonAnalyzeQuery.Size = new System.Drawing.Size(75, 30);
            this.buttonAnalyzeQuery.TabIndex = 17;
            this.buttonAnalyzeQuery.Text = "Analyze";
            this.buttonAnalyzeQuery.UseVisualStyleBackColor = false;
            this.buttonAnalyzeQuery.Click += new System.EventHandler(this.ButtonAnalyzeQuery_Click);
            // 
            // buttonExportPdf
            // 
            this.buttonExportPdf.BackColor = System.Drawing.Color.Crimson;
            this.buttonExportPdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExportPdf.Location = new System.Drawing.Point(587, 599);
            this.buttonExportPdf.Name = "buttonExportPdf";
            this.buttonExportPdf.Size = new System.Drawing.Size(92, 30);
            this.buttonExportPdf.TabIndex = 18;
            this.buttonExportPdf.Text = "Export PDF";
            this.buttonExportPdf.UseVisualStyleBackColor = false;
            this.buttonExportPdf.Click += new System.EventHandler(this.buttonExportPdf_Click);
            // 
            // buttonOpenReportDelivery
            // 
            this.buttonOpenReportDelivery.Location = new System.Drawing.Point(618, 37);
            this.buttonOpenReportDelivery.Name = "buttonOpenReportDelivery";
            this.buttonOpenReportDelivery.Size = new System.Drawing.Size(150, 30);
            this.buttonOpenReportDelivery.TabIndex = 19;
            this.buttonOpenReportDelivery.Text = "Report Delivery";
            this.buttonOpenReportDelivery.Click += new System.EventHandler(this.ButtonOpenReportDelivery_Click);
            // 
            // buttonOpenReportProducts
            // 
            this.buttonOpenReportProducts.Location = new System.Drawing.Point(618, 73);
            this.buttonOpenReportProducts.Name = "buttonOpenReportProducts";
            this.buttonOpenReportProducts.Size = new System.Drawing.Size(150, 30);
            this.buttonOpenReportProducts.TabIndex = 20;
            this.buttonOpenReportProducts.Text = "Report Products";
            this.buttonOpenReportProducts.Click += new System.EventHandler(this.ButtonOpenReportProducts_Click);
            // 
            // buttonOpenReportSalesman
            // 
            this.buttonOpenReportSalesman.Location = new System.Drawing.Point(618, 109);
            this.buttonOpenReportSalesman.Name = "buttonOpenReportSalesman";
            this.buttonOpenReportSalesman.Size = new System.Drawing.Size(150, 30);
            this.buttonOpenReportSalesman.TabIndex = 21;
            this.buttonOpenReportSalesman.Text = "Report Salesman";
            this.buttonOpenReportSalesman.Click += new System.EventHandler(this.ButtonOpenReportSalesman_Click);
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(40, 348);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(245, 22);
            this.searchBox.TabIndex = 22;
            this.searchBox.Text = "Search";
            this.searchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(15, 348);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(19, 22);
            this.pictureBox1.TabIndex = 23;
            this.pictureBox1.TabStop = false;
            // 
            // Delivery
            // 
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(800, 639);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.buttonOpenReportSalesman);
            this.Controls.Add(this.buttonOpenReportProducts);
            this.Controls.Add(this.buttonOpenReportDelivery);
            this.Controls.Add(this.buttonExportPdf);
            this.Controls.Add(this.buttonAnalyzeQuery);
            this.Controls.Add(this.buttonExportReport);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.buttonOpenSalesmanForm);
            this.Controls.Add(this.buttonOpenProductForm);
            this.Controls.Add(this.groupBoxInput);
            this.Controls.Add(this.dataGridViewDelivery);
            this.Name = "Delivery";
            this.Text = "Delivery Management";
            this.groupBoxInput.ResumeLayout(false);
            this.groupBoxInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDelivery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonOpenSalesmanForm;
        private System.Windows.Forms.Button buttonOpenProductForm;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.GroupBox groupBoxInput;
        private System.Windows.Forms.DateTimePicker dateTimePickerDeliveryDate;
        private System.Windows.Forms.ComboBox comboBoxSalesmanId;
        private System.Windows.Forms.ComboBox comboBoxProductId;
        private System.Windows.Forms.NumericUpDown numericUpDownQuantity;
        private System.Windows.Forms.Button buttonSubmit;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.DataGridView dataGridViewDelivery;
        private System.Windows.Forms.Label labelDeliveryDate;
        private System.Windows.Forms.Label labelSalesmanId;
        private System.Windows.Forms.Label labelProductId;
        private System.Windows.Forms.Label labelQuantity;
        private System.Windows.Forms.Button buttonExportReport;
        private System.Windows.Forms.Button buttonAnalyzeQuery;
        private System.Windows.Forms.Button buttonExportPdf;
        private System.Windows.Forms.Button buttonOpenReportDelivery;
        private System.Windows.Forms.Button buttonOpenReportProducts;
        private System.Windows.Forms.Button buttonOpenReportSalesman;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

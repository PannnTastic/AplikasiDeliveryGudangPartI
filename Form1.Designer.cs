namespace DeliveryApp
{
    partial class Form1
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
            this.groupBoxInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDelivery)).BeginInit();
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
            this.dateTimePickerDeliveryDate.Location = new System.Drawing.Point(159, 42);
            this.dateTimePickerDeliveryDate.Name = "dateTimePickerDeliveryDate";
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
            this.dataGridViewDelivery.Location = new System.Drawing.Point(20, 340);
            this.dataGridViewDelivery.Name = "dataGridViewDelivery";
            this.dataGridViewDelivery.RowHeadersWidth = 51;
            this.dataGridViewDelivery.RowTemplate.Height = 24;
            this.dataGridViewDelivery.Size = new System.Drawing.Size(760, 200);
            this.dataGridViewDelivery.TabIndex = 13;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.BackColor = System.Drawing.Color.LightYellow;
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefresh.Location = new System.Drawing.Point(20, 550);
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
            // Form1
            // 
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.buttonOpenSalesmanForm);
            this.Controls.Add(this.buttonOpenProductForm);
            this.Controls.Add(this.groupBoxInput);
            this.Controls.Add(this.dataGridViewDelivery);
            this.Name = "Form1";
            this.Text = "Delivery Management";
            this.groupBoxInput.ResumeLayout(false);
            this.groupBoxInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDelivery)).EndInit();
            this.ResumeLayout(false);
            this.dataGridViewDelivery.SelectionChanged += new System.EventHandler(this.DataGridViewDelivery_SelectionChanged);


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
    }
}

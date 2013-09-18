namespace DataAdapter1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.vehicleDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.vehicleDataSet = new DataAdapter1.VehicleDataSet();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.createDistributedTxButton = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vehicleDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vehicleDataSet)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.DataSource = this.vehicleDataSetBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(603, 348);
            this.dataGridView1.TabIndex = 0;
            // 
            // vehicleDataSetBindingSource
            // 
            this.vehicleDataSetBindingSource.DataSource = this.vehicleDataSet;
            this.vehicleDataSetBindingSource.Position = 0;
            // 
            // vehicleDataSet
            // 
            this.vehicleDataSet.DataSetName = "VehicleDataSet";
            this.vehicleDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createDistributedTxButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(603, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // createDistributedTxButton
            // 
            this.createDistributedTxButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.createDistributedTxButton.Image = ((System.Drawing.Image)(resources.GetObject("createDistributedTxButton.Image")));
            this.createDistributedTxButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.createDistributedTxButton.Name = "createDistributedTxButton";
            this.createDistributedTxButton.Size = new System.Drawing.Size(23, 22);
            this.createDistributedTxButton.Text = "Create Distributed Tx";
            this.createDistributedTxButton.ToolTipText = "Create Distributed Transaction";
            this.createDistributedTxButton.Click += new System.EventHandler(this.createDistributedTx_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 348);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Data Adapter";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vehicleDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vehicleDataSet)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource vehicleDataSetBindingSource;
        private VehicleDataSet vehicleDataSet;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton createDistributedTxButton;
    }
}


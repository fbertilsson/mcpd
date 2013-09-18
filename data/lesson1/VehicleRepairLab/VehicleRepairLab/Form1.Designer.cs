namespace VehicleRepairLab
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
            this.dgVehicles = new System.Windows.Forms.DataGridView();
            this.dgRepairs = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgVehicles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgRepairs)).BeginInit();
            this.SuspendLayout();
            // 
            // dgVehicles
            // 
            this.dgVehicles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgVehicles.Location = new System.Drawing.Point(13, 13);
            this.dgVehicles.Name = "dgVehicles";
            this.dgVehicles.Size = new System.Drawing.Size(909, 220);
            this.dgVehicles.TabIndex = 0;
            // 
            // dgRepairs
            // 
            this.dgRepairs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgRepairs.Location = new System.Drawing.Point(13, 239);
            this.dgRepairs.Name = "dgRepairs";
            this.dgRepairs.Size = new System.Drawing.Size(909, 226);
            this.dgRepairs.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 480);
            this.Controls.Add(this.dgRepairs);
            this.Controls.Add(this.dgVehicles);
            this.Name = "Form1";
            this.Text = "Vehicle Repair Lab";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1FormClosing);
            this.Load += new System.EventHandler(this.Form1Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgVehicles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgRepairs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgVehicles;
        private System.Windows.Forms.DataGridView dgRepairs;
    }
}


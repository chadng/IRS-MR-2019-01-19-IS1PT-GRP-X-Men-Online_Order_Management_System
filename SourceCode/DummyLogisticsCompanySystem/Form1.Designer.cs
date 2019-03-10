namespace DummyLogisticsCompanySystem
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
            this.btnShipped = new System.Windows.Forms.Button();
            this.tbTrackingNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnShipped
            // 
            this.btnShipped.Location = new System.Drawing.Point(505, 293);
            this.btnShipped.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnShipped.Name = "btnShipped";
            this.btnShipped.Size = new System.Drawing.Size(129, 34);
            this.btnShipped.TabIndex = 2;
            this.btnShipped.Text = "Confirm Delivery";
            this.btnShipped.UseVisualStyleBackColor = true;
            this.btnShipped.Click += new System.EventHandler(this.btnShipped_Click);
            // 
            // tbTrackingNumber
            // 
            this.tbTrackingNumber.Location = new System.Drawing.Point(188, 60);
            this.tbTrackingNumber.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbTrackingNumber.Name = "tbTrackingNumber";
            this.tbTrackingNumber.Size = new System.Drawing.Size(205, 22);
            this.tbTrackingNumber.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 60);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Tracking Number";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 359);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbTrackingNumber);
            this.Controls.Add(this.btnShipped);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnShipped;
        private System.Windows.Forms.TextBox tbTrackingNumber;
        private System.Windows.Forms.Label label1;
    }
}


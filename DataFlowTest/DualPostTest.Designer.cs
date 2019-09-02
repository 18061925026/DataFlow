namespace DataFlowTest
{
    partial class DualPostTest
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
            this.txtResult1 = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtResult2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtResult1
            // 
            this.txtResult1.Font = new System.Drawing.Font("宋体", 25F);
            this.txtResult1.Location = new System.Drawing.Point(13, 13);
            this.txtResult1.Margin = new System.Windows.Forms.Padding(4);
            this.txtResult1.Multiline = true;
            this.txtResult1.Name = "txtResult1";
            this.txtResult1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult1.Size = new System.Drawing.Size(383, 466);
            this.txtResult1.TabIndex = 7;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(666, 487);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(143, 48);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtResult2
            // 
            this.txtResult2.Font = new System.Drawing.Font("宋体", 25F);
            this.txtResult2.Location = new System.Drawing.Point(441, 13);
            this.txtResult2.Margin = new System.Windows.Forms.Padding(4);
            this.txtResult2.Multiline = true;
            this.txtResult2.Name = "txtResult2";
            this.txtResult2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult2.Size = new System.Drawing.Size(384, 466);
            this.txtResult2.TabIndex = 8;
            // 
            // DualPostTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 542);
            this.Controls.Add(this.txtResult2);
            this.Controls.Add(this.txtResult1);
            this.Controls.Add(this.btnStart);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DualPostTest";
            this.Text = "DualPostTest";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtResult1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtResult2;
    }
}
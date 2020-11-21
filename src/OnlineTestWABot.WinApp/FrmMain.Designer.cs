namespace OnlineTestWABot.WinApp
{
    partial class FrmMain
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
            this.btnProsesPesan = new System.Windows.Forms.Button();
            this.txtPesan = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnProsesPesan
            // 
            this.btnProsesPesan.Location = new System.Drawing.Point(131, 14);
            this.btnProsesPesan.Name = "btnProsesPesan";
            this.btnProsesPesan.Size = new System.Drawing.Size(128, 23);
            this.btnProsesPesan.TabIndex = 0;
            this.btnProsesPesan.Text = "Proses Pesan";
            this.btnProsesPesan.UseVisualStyleBackColor = true;
            this.btnProsesPesan.Click += new System.EventHandler(this.btnProsesPesan_Click);
            // 
            // txtPesan
            // 
            this.txtPesan.Location = new System.Drawing.Point(12, 14);
            this.txtPesan.Name = "txtPesan";
            this.txtPesan.Size = new System.Drawing.Size(100, 20);
            this.txtPesan.TabIndex = 1;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 261);
            this.Controls.Add(this.txtPesan);
            this.Controls.Add(this.btnProsesPesan);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Online Test WA Bot";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnProsesPesan;
        private System.Windows.Forms.TextBox txtPesan;
    }
}


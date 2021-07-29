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
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.txtLokasiWhatsAppNETAPINodeJs = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnLokasiWAAutomateNodejs = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(347, 51);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(428, 52);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // txtLokasiWhatsAppNETAPINodeJs
            // 
            this.txtLokasiWhatsAppNETAPINodeJs.Location = new System.Drawing.Point(15, 25);
            this.txtLokasiWhatsAppNETAPINodeJs.Name = "txtLokasiWhatsAppNETAPINodeJs";
            this.txtLokasiWhatsAppNETAPINodeJs.ReadOnly = true;
            this.txtLokasiWhatsAppNETAPINodeJs.Size = new System.Drawing.Size(448, 20);
            this.txtLokasiWhatsAppNETAPINodeJs.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(175, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Lokasi WhatsApp NET API NodeJs";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnLokasiWAAutomateNodejs
            // 
            this.btnLokasiWAAutomateNodejs.Location = new System.Drawing.Point(469, 23);
            this.btnLokasiWAAutomateNodejs.Name = "btnLokasiWAAutomateNodejs";
            this.btnLokasiWAAutomateNodejs.Size = new System.Drawing.Size(34, 23);
            this.btnLokasiWAAutomateNodejs.TabIndex = 5;
            this.btnLokasiWAAutomateNodejs.Text = "...";
            this.btnLokasiWAAutomateNodejs.UseVisualStyleBackColor = true;
            this.btnLokasiWAAutomateNodejs.Click += new System.EventHandler(this.btnLokasiWAAutomateNodejs_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 83);
            this.Controls.Add(this.btnLokasiWAAutomateNodejs);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtLokasiWhatsAppNETAPINodeJs);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Online Test WA Bot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TextBox txtLokasiWhatsAppNETAPINodeJs;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnLokasiWAAutomateNodejs;
    }
}


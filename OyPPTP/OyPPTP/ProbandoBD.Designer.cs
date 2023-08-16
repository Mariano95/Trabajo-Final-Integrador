
namespace OyPPTP
{
    partial class ProbandoBD
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
            this.probandoBD_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // probandoBD_label
            // 
            this.probandoBD_label.AutoSize = true;
            this.probandoBD_label.Location = new System.Drawing.Point(281, 179);
            this.probandoBD_label.Name = "probandoBD_label";
            this.probandoBD_label.Size = new System.Drawing.Size(257, 15);
            this.probandoBD_label.TabIndex = 0;
            this.probandoBD_label.Text = "Inicializando base de datos, aguarde por favor...";
            // 
            // ProbandoBD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.probandoBD_label);
            this.Name = "ProbandoBD";
            this.Text = "ProbandoBD";
            this.Load += new System.EventHandler(this.ProbandoBD_Load);
            this.Shown += new System.EventHandler(this.ProbandoBD_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label probandoBD_label;
    }
}

namespace OyPPTP
{
    partial class LoadingForm
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
            this.loadingForm_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // loadingForm_label
            // 
            this.loadingForm_label.AutoSize = true;
            this.loadingForm_label.Location = new System.Drawing.Point(205, 202);
            this.loadingForm_label.Name = "loadingForm_label";
            this.loadingForm_label.Size = new System.Drawing.Size(404, 15);
            this.loadingForm_label.TabIndex = 0;
            this.loadingForm_label.Text = "Inicializando base de datos con las credenciales dadas. Aguarde por favor ...";
            // 
            // LoadingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.loadingForm_label);
            this.Name = "LoadingForm";
            this.Text = "LoadingForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label loadingForm_label;
    }
}
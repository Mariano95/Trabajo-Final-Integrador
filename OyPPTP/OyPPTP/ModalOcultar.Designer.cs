
namespace OyPPTP
{
    partial class ModalOcultar
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
            this.modal_ocultar_label = new System.Windows.Forms.Label();
            this.no = new System.Windows.Forms.Button();
            this.si = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // modal_ocultar_label
            // 
            this.modal_ocultar_label.AutoSize = true;
            this.modal_ocultar_label.Location = new System.Drawing.Point(37, 55);
            this.modal_ocultar_label.Name = "modal_ocultar_label";
            this.modal_ocultar_label.Size = new System.Drawing.Size(286, 15);
            this.modal_ocultar_label.TabIndex = 0;
            this.modal_ocultar_label.Text = "Tu usuario ya no aparecerá en búsquedas, continuar?";
            // 
            // no
            // 
            this.no.Location = new System.Drawing.Point(46, 142);
            this.no.Name = "no";
            this.no.Size = new System.Drawing.Size(75, 23);
            this.no.TabIndex = 1;
            this.no.Text = "No";
            this.no.UseVisualStyleBackColor = true;
            this.no.Click += new System.EventHandler(this.no_Click);
            // 
            // si
            // 
            this.si.Location = new System.Drawing.Point(248, 142);
            this.si.Name = "si";
            this.si.Size = new System.Drawing.Size(75, 23);
            this.si.TabIndex = 2;
            this.si.Text = "Sí";
            this.si.UseVisualStyleBackColor = true;
            this.si.Click += new System.EventHandler(this.si_Click);
            // 
            // ModalOcultar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 203);
            this.Controls.Add(this.si);
            this.Controls.Add(this.no);
            this.Controls.Add(this.modal_ocultar_label);
            this.Name = "ModalOcultar";
            this.Text = "ModalOcultar";
            this.Load += new System.EventHandler(this.ModalOcultar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label modal_ocultar_label;
        private System.Windows.Forms.Button no;
        private System.Windows.Forms.Button si;
    }
}
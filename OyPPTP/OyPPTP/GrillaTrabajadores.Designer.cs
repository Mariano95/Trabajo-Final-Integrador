
namespace OyPPTP
{
    partial class GrillaTrabajadores
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
            this.grilla_trabajadores = new System.Windows.Forms.DataGridView();
            this.grilla_trabajadores_label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grilla_trabajadores)).BeginInit();
            this.SuspendLayout();
            // 
            // grilla_trabajadores
            // 
            this.grilla_trabajadores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grilla_trabajadores.Location = new System.Drawing.Point(12, 54);
            this.grilla_trabajadores.Name = "grilla_trabajadores";
            this.grilla_trabajadores.RowTemplate.Height = 25;
            this.grilla_trabajadores.Size = new System.Drawing.Size(776, 289);
            this.grilla_trabajadores.TabIndex = 0;
            // 
            // grilla_trabajadores_label
            // 
            this.grilla_trabajadores_label.AutoSize = true;
            this.grilla_trabajadores_label.Location = new System.Drawing.Point(265, 26);
            this.grilla_trabajadores_label.Name = "grilla_trabajadores_label";
            this.grilla_trabajadores_label.Size = new System.Drawing.Size(248, 15);
            this.grilla_trabajadores_label.TabIndex = 1;
            this.grilla_trabajadores_label.Text = "Se han encontrado los siguientes trabajadores";
            // 
            // GrillaTrabajadores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.grilla_trabajadores_label);
            this.Controls.Add(this.grilla_trabajadores);
            this.Name = "GrillaTrabajadores";
            this.Text = "GrillaTrabajadores";
            this.Load += new System.EventHandler(this.GrillaTrabajadores_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grilla_trabajadores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grilla_trabajadores;
        private System.Windows.Forms.Label grilla_trabajadores_label;
    }
}
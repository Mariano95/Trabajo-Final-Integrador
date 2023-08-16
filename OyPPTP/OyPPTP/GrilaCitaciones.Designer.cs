
namespace OyPPTP
{
    partial class GrilaCitaciones
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
            this.grilla_citaciones = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grilla_citaciones)).BeginInit();
            this.SuspendLayout();
            // 
            // grilla_citaciones
            // 
            this.grilla_citaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grilla_citaciones.Location = new System.Drawing.Point(12, 81);
            this.grilla_citaciones.Name = "grilla_citaciones";
            this.grilla_citaciones.RowTemplate.Height = 25;
            this.grilla_citaciones.Size = new System.Drawing.Size(776, 289);
            this.grilla_citaciones.TabIndex = 1;
            // 
            // GrilaCitaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.grilla_citaciones);
            this.Name = "GrilaCitaciones";
            this.Text = "GrillaCitaciones";
            this.Load += new System.EventHandler(this.GrilaCitaciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grilla_citaciones)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grilla_citaciones;
    }
}
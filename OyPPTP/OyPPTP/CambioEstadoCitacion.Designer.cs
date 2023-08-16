
namespace OyPPTP
{
    partial class CambioEstadoCitacion
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
            this.nuevo_estado = new System.Windows.Forms.Label();
            this.nuevo_estado_combo = new System.Windows.Forms.ComboBox();
            this.guardar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nuevo_estado
            // 
            this.nuevo_estado.AutoSize = true;
            this.nuevo_estado.Location = new System.Drawing.Point(89, 104);
            this.nuevo_estado.Name = "nuevo_estado";
            this.nuevo_estado.Size = new System.Drawing.Size(80, 15);
            this.nuevo_estado.TabIndex = 0;
            this.nuevo_estado.Text = "Nuevo estado";
            // 
            // nuevo_estado_combo
            // 
            this.nuevo_estado_combo.FormattingEnabled = true;
            this.nuevo_estado_combo.Items.AddRange(new object[] {
            "Aceptada",
            "Cancelada",
            "Rechazada",
            "Cumplida"});
            this.nuevo_estado_combo.Location = new System.Drawing.Point(218, 101);
            this.nuevo_estado_combo.Name = "nuevo_estado_combo";
            this.nuevo_estado_combo.Size = new System.Drawing.Size(137, 23);
            this.nuevo_estado_combo.TabIndex = 1;
            // 
            // guardar
            // 
            this.guardar.Location = new System.Drawing.Point(178, 157);
            this.guardar.Name = "guardar";
            this.guardar.Size = new System.Drawing.Size(75, 23);
            this.guardar.TabIndex = 2;
            this.guardar.Text = "Guardar";
            this.guardar.UseVisualStyleBackColor = true;
            this.guardar.Click += new System.EventHandler(this.guardar_Click);
            // 
            // CambioEstadoCitacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 192);
            this.Controls.Add(this.guardar);
            this.Controls.Add(this.nuevo_estado_combo);
            this.Controls.Add(this.nuevo_estado);
            this.Name = "CambioEstadoCitacion";
            this.Text = "CambioEstadoCitacion";
            this.Load += new System.EventHandler(this.CambioEstadoCitacion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nuevo_estado;
        private System.Windows.Forms.ComboBox nuevo_estado_combo;
        private System.Windows.Forms.Button guardar;
    }
}
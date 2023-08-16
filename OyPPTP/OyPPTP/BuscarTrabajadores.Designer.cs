
namespace OyPPTP
{
    partial class BuscarTrabajadores
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
            this.buscar_trabajadores_label = new System.Windows.Forms.Label();
            this.proedio_de_calificacion_minimo = new System.Windows.Forms.Label();
            this.tipo_de_servicio = new System.Windows.Forms.Label();
            this.distancia_maxima = new System.Windows.Forms.Label();
            this.tipo_de_servicio_combo = new System.Windows.Forms.ComboBox();
            this.promedio_de_calificacion_minimo_numeric = new System.Windows.Forms.NumericUpDown();
            this.distancia_maxima_numeric = new System.Windows.Forms.NumericUpDown();
            this.buscar_trabajadores = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.promedio_de_calificacion_minimo_numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.distancia_maxima_numeric)).BeginInit();
            this.SuspendLayout();
            // 
            // buscar_trabajadores_label
            // 
            this.buscar_trabajadores_label.AutoSize = true;
            this.buscar_trabajadores_label.Location = new System.Drawing.Point(247, 132);
            this.buscar_trabajadores_label.Name = "buscar_trabajadores_label";
            this.buscar_trabajadores_label.Size = new System.Drawing.Size(308, 15);
            this.buscar_trabajadores_label.TabIndex = 0;
            this.buscar_trabajadores_label.Text = "Podés buscar trabajadores a partir de los siguientes filtros";
            // 
            // proedio_de_calificacion_minimo
            // 
            this.proedio_de_calificacion_minimo.AutoSize = true;
            this.proedio_de_calificacion_minimo.Location = new System.Drawing.Point(232, 297);
            this.proedio_de_calificacion_minimo.Name = "proedio_de_calificacion_minimo";
            this.proedio_de_calificacion_minimo.Size = new System.Drawing.Size(183, 15);
            this.proedio_de_calificacion_minimo.TabIndex = 1;
            this.proedio_de_calificacion_minimo.Text = "Promedio de calificación mínimo";
            // 
            // tipo_de_servicio
            // 
            this.tipo_de_servicio.AutoSize = true;
            this.tipo_de_servicio.Location = new System.Drawing.Point(232, 202);
            this.tipo_de_servicio.Name = "tipo_de_servicio";
            this.tipo_de_servicio.Size = new System.Drawing.Size(89, 15);
            this.tipo_de_servicio.TabIndex = 2;
            this.tipo_de_servicio.Text = "Tipo de servicio";
            // 
            // distancia_maxima
            // 
            this.distancia_maxima.AutoSize = true;
            this.distancia_maxima.Location = new System.Drawing.Point(232, 251);
            this.distancia_maxima.Name = "distancia_maxima";
            this.distancia_maxima.Size = new System.Drawing.Size(101, 15);
            this.distancia_maxima.TabIndex = 3;
            this.distancia_maxima.Text = "Distancia máxima";
            // 
            // tipo_de_servicio_combo
            // 
            this.tipo_de_servicio_combo.FormattingEnabled = true;
            this.tipo_de_servicio_combo.Location = new System.Drawing.Point(479, 202);
            this.tipo_de_servicio_combo.Name = "tipo_de_servicio_combo";
            this.tipo_de_servicio_combo.Size = new System.Drawing.Size(121, 23);
            this.tipo_de_servicio_combo.TabIndex = 4;
            // 
            // promedio_de_calificacion_minimo_numeric
            // 
            this.promedio_de_calificacion_minimo_numeric.Location = new System.Drawing.Point(480, 295);
            this.promedio_de_calificacion_minimo_numeric.Name = "promedio_de_calificacion_minimo_numeric";
            this.promedio_de_calificacion_minimo_numeric.Size = new System.Drawing.Size(120, 23);
            this.promedio_de_calificacion_minimo_numeric.TabIndex = 5;
            // 
            // distancia_maxima_numeric
            // 
            this.distancia_maxima_numeric.Location = new System.Drawing.Point(480, 249);
            this.distancia_maxima_numeric.Name = "distancia_maxima_numeric";
            this.distancia_maxima_numeric.Size = new System.Drawing.Size(120, 23);
            this.distancia_maxima_numeric.TabIndex = 6;
            // 
            // buscar_trabajadores
            // 
            this.buscar_trabajadores.Location = new System.Drawing.Point(356, 371);
            this.buscar_trabajadores.Name = "buscar_trabajadores";
            this.buscar_trabajadores.Size = new System.Drawing.Size(130, 42);
            this.buscar_trabajadores.TabIndex = 7;
            this.buscar_trabajadores.Text = "Buscar trabajadores";
            this.buscar_trabajadores.UseVisualStyleBackColor = true;
            this.buscar_trabajadores.Click += new System.EventHandler(this.buscar_trabajadores_Click);
            // 
            // BuscarTrabajadores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buscar_trabajadores);
            this.Controls.Add(this.distancia_maxima_numeric);
            this.Controls.Add(this.promedio_de_calificacion_minimo_numeric);
            this.Controls.Add(this.tipo_de_servicio_combo);
            this.Controls.Add(this.distancia_maxima);
            this.Controls.Add(this.tipo_de_servicio);
            this.Controls.Add(this.proedio_de_calificacion_minimo);
            this.Controls.Add(this.buscar_trabajadores_label);
            this.Name = "BuscarTrabajadores";
            this.Text = "BuscarTrabajadores";
            this.Load += new System.EventHandler(this.BuscarTrabajadores_Load);
            ((System.ComponentModel.ISupportInitialize)(this.promedio_de_calificacion_minimo_numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.distancia_maxima_numeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label buscar_trabajadores_label;
        private System.Windows.Forms.Label proedio_de_calificacion_minimo;
        private System.Windows.Forms.Label tipo_de_servicio;
        private System.Windows.Forms.Label distancia_maxima;
        private System.Windows.Forms.ComboBox tipo_de_servicio_combo;
        private System.Windows.Forms.NumericUpDown promedio_de_calificacion_minimo_numeric;
        private System.Windows.Forms.NumericUpDown distancia_maxima_numeric;
        private System.Windows.Forms.Button buscar_trabajadores;
    }
}
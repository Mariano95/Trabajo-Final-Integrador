
namespace OyPPTP
{
    partial class CitarTrabajador
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
            this.citar_trabajador_label = new System.Windows.Forms.Label();
            this.servicio_a_citar_combo = new System.Windows.Forms.ComboBox();
            this.servicio_a_citar = new System.Windows.Forms.Label();
            this.horario_inicio_citacion_date = new System.Windows.Forms.DateTimePicker();
            this.horario_fin_citacion_date = new System.Windows.Forms.DateTimePicker();
            this.horario_inicio_citacion = new System.Windows.Forms.Label();
            this.horario_fin_citacion = new System.Windows.Forms.Label();
            this.generar_citacion = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // citar_trabajador_label
            // 
            this.citar_trabajador_label.AutoSize = true;
            this.citar_trabajador_label.Location = new System.Drawing.Point(350, 92);
            this.citar_trabajador_label.Name = "citar_trabajador_label";
            this.citar_trabajador_label.Size = new System.Drawing.Size(94, 15);
            this.citar_trabajador_label.TabIndex = 0;
            this.citar_trabajador_label.Text = "Por favor indicar";
            // 
            // servicio_a_citar_combo
            // 
            this.servicio_a_citar_combo.FormattingEnabled = true;
            this.servicio_a_citar_combo.Items.AddRange(new object[] {
            "Servicio 1",
            "Servicio 2",
            "Servicio 3",
            "Servicio 4",
            "Servicio 5"});
            this.servicio_a_citar_combo.Location = new System.Drawing.Point(381, 162);
            this.servicio_a_citar_combo.Name = "servicio_a_citar_combo";
            this.servicio_a_citar_combo.Size = new System.Drawing.Size(207, 23);
            this.servicio_a_citar_combo.TabIndex = 1;
            // 
            // servicio_a_citar
            // 
            this.servicio_a_citar.AutoSize = true;
            this.servicio_a_citar.Location = new System.Drawing.Point(217, 165);
            this.servicio_a_citar.Name = "servicio_a_citar";
            this.servicio_a_citar.Size = new System.Drawing.Size(83, 15);
            this.servicio_a_citar.TabIndex = 2;
            this.servicio_a_citar.Text = "Servicio a citar";
            // 
            // horario_inicio_citacion_date
            // 
            this.horario_inicio_citacion_date.Location = new System.Drawing.Point(381, 210);
            this.horario_inicio_citacion_date.Name = "horario_inicio_citacion_date";
            this.horario_inicio_citacion_date.Size = new System.Drawing.Size(207, 23);
            this.horario_inicio_citacion_date.TabIndex = 3;
            // 
            // horario_fin_citacion_date
            // 
            this.horario_fin_citacion_date.Location = new System.Drawing.Point(381, 261);
            this.horario_fin_citacion_date.Name = "horario_fin_citacion_date";
            this.horario_fin_citacion_date.Size = new System.Drawing.Size(207, 23);
            this.horario_fin_citacion_date.TabIndex = 4;
            // 
            // horario_inicio_citacion
            // 
            this.horario_inicio_citacion.AutoSize = true;
            this.horario_inicio_citacion.Location = new System.Drawing.Point(217, 216);
            this.horario_inicio_citacion.Name = "horario_inicio_citacion";
            this.horario_inicio_citacion.Size = new System.Drawing.Size(124, 15);
            this.horario_inicio_citacion.TabIndex = 5;
            this.horario_inicio_citacion.Text = "Horario inicio citación";
            // 
            // horario_fin_citacion
            // 
            this.horario_fin_citacion.AutoSize = true;
            this.horario_fin_citacion.Location = new System.Drawing.Point(217, 267);
            this.horario_fin_citacion.Name = "horario_fin_citacion";
            this.horario_fin_citacion.Size = new System.Drawing.Size(109, 15);
            this.horario_fin_citacion.TabIndex = 6;
            this.horario_fin_citacion.Text = "Horario fin citación";
            // 
            // generar_citacion
            // 
            this.generar_citacion.Location = new System.Drawing.Point(350, 323);
            this.generar_citacion.Name = "generar_citacion";
            this.generar_citacion.Size = new System.Drawing.Size(120, 43);
            this.generar_citacion.TabIndex = 7;
            this.generar_citacion.Text = "Generar citación";
            this.generar_citacion.UseVisualStyleBackColor = true;
            this.generar_citacion.Click += new System.EventHandler(this.generar_citacion_Click);
            // 
            // CitarTrabajador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.generar_citacion);
            this.Controls.Add(this.horario_fin_citacion);
            this.Controls.Add(this.horario_inicio_citacion);
            this.Controls.Add(this.horario_fin_citacion_date);
            this.Controls.Add(this.horario_inicio_citacion_date);
            this.Controls.Add(this.servicio_a_citar);
            this.Controls.Add(this.servicio_a_citar_combo);
            this.Controls.Add(this.citar_trabajador_label);
            this.Name = "CitarTrabajador";
            this.Text = "CitarTrabajador";
            this.Load += new System.EventHandler(this.CitarTrabajador_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label citar_trabajador_label;
        private System.Windows.Forms.ComboBox servicio_a_citar_combo;
        private System.Windows.Forms.Label servicio_a_citar;
        private System.Windows.Forms.DateTimePicker horario_inicio_citacion_date;
        private System.Windows.Forms.DateTimePicker horario_fin_citacion_date;
        private System.Windows.Forms.Label horario_inicio_citacion;
        private System.Windows.Forms.Label horario_fin_citacion;
        private System.Windows.Forms.Button generar_citacion;
    }
}
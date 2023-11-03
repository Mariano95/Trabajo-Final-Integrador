
namespace OyPPTP
{
    partial class PreBitacora
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
            this.fecha_inicio_date = new System.Windows.Forms.DateTimePicker();
            this.fecha_fin_date = new System.Windows.Forms.DateTimePicker();
            this.evento_combo = new System.Windows.Forms.ComboBox();
            this.usuario_combo = new System.Windows.Forms.ComboBox();
            this.fecha_inicio = new System.Windows.Forms.Label();
            this.fecha_fin = new System.Windows.Forms.Label();
            this.evento = new System.Windows.Forms.Label();
            this.usuario = new System.Windows.Forms.Label();
            this.pre_bitacora_label = new System.Windows.Forms.Label();
            this.buscar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // fecha_inicio_date
            // 
            this.fecha_inicio_date.Location = new System.Drawing.Point(245, 83);
            this.fecha_inicio_date.Name = "fecha_inicio_date";
            this.fecha_inicio_date.Size = new System.Drawing.Size(200, 23);
            this.fecha_inicio_date.TabIndex = 0;
            // 
            // fecha_fin_date
            // 
            this.fecha_fin_date.Location = new System.Drawing.Point(245, 129);
            this.fecha_fin_date.Name = "fecha_fin_date";
            this.fecha_fin_date.Size = new System.Drawing.Size(200, 23);
            this.fecha_fin_date.TabIndex = 1;
            // 
            // evento_combo
            // 
            this.evento_combo.FormattingEnabled = true;
            this.evento_combo.Location = new System.Drawing.Point(245, 181);
            this.evento_combo.Name = "evento_combo";
            this.evento_combo.Size = new System.Drawing.Size(200, 23);
            this.evento_combo.TabIndex = 2;
            // 
            // usuario_combo
            // 
            this.usuario_combo.FormattingEnabled = true;
            this.usuario_combo.Location = new System.Drawing.Point(245, 233);
            this.usuario_combo.Name = "usuario_combo";
            this.usuario_combo.Size = new System.Drawing.Size(200, 23);
            this.usuario_combo.TabIndex = 3;
            // 
            // fecha_inicio
            // 
            this.fecha_inicio.AutoSize = true;
            this.fecha_inicio.Location = new System.Drawing.Point(61, 91);
            this.fecha_inicio.Name = "fecha_inicio";
            this.fecha_inicio.Size = new System.Drawing.Size(70, 15);
            this.fecha_inicio.TabIndex = 4;
            this.fecha_inicio.Text = "Fecha inicio";
            // 
            // fecha_fin
            // 
            this.fecha_fin.AutoSize = true;
            this.fecha_fin.Location = new System.Drawing.Point(61, 137);
            this.fecha_fin.Name = "fecha_fin";
            this.fecha_fin.Size = new System.Drawing.Size(55, 15);
            this.fecha_fin.TabIndex = 5;
            this.fecha_fin.Text = "Fecha fin";
            // 
            // evento
            // 
            this.evento.AllowDrop = true;
            this.evento.AutoSize = true;
            this.evento.Location = new System.Drawing.Point(61, 188);
            this.evento.Name = "evento";
            this.evento.Size = new System.Drawing.Size(43, 15);
            this.evento.TabIndex = 6;
            this.evento.Text = "Evento";
            // 
            // usuario
            // 
            this.usuario.AutoSize = true;
            this.usuario.Location = new System.Drawing.Point(61, 240);
            this.usuario.Name = "usuario";
            this.usuario.Size = new System.Drawing.Size(47, 15);
            this.usuario.TabIndex = 7;
            this.usuario.Text = "Usuario";
            // 
            // pre_bitacora_label
            // 
            this.pre_bitacora_label.AutoSize = true;
            this.pre_bitacora_label.Location = new System.Drawing.Point(181, 27);
            this.pre_bitacora_label.Name = "pre_bitacora_label";
            this.pre_bitacora_label.Size = new System.Drawing.Size(121, 15);
            this.pre_bitacora_label.TabIndex = 8;
            this.pre_bitacora_label.Text = "Búsqueda en bitácora";
            // 
            // buscar
            // 
            this.buscar.Location = new System.Drawing.Point(202, 291);
            this.buscar.Name = "buscar";
            this.buscar.Size = new System.Drawing.Size(75, 23);
            this.buscar.TabIndex = 9;
            this.buscar.Text = "Buscar";
            this.buscar.UseVisualStyleBackColor = true;
            this.buscar.Click += new System.EventHandler(this.buscar_Click);
            // 
            // PreBitacora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 326);
            this.Controls.Add(this.buscar);
            this.Controls.Add(this.pre_bitacora_label);
            this.Controls.Add(this.usuario);
            this.Controls.Add(this.evento);
            this.Controls.Add(this.fecha_fin);
            this.Controls.Add(this.fecha_inicio);
            this.Controls.Add(this.usuario_combo);
            this.Controls.Add(this.evento_combo);
            this.Controls.Add(this.fecha_fin_date);
            this.Controls.Add(this.fecha_inicio_date);
            this.Name = "PreBitacora";
            this.Text = "PreBitacora";
            this.Load += new System.EventHandler(this.PreBitacora_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label fecha_inicio;
        private System.Windows.Forms.Label fecha_fin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label usuario;
        private System.Windows.Forms.Label pre_bitacora_label;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label evento;
        private System.Windows.Forms.DateTimePicker fecha_inicio_date;
        private System.Windows.Forms.DateTimePicker fecha_fin_date;
        private System.Windows.Forms.ComboBox evento_combo;
        private System.Windows.Forms.ComboBox usuario_combo;
        private System.Windows.Forms.Button buscar;
    }
}
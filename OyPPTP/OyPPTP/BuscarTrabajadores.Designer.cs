
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
            this.label1 = new System.Windows.Forms.Label();
            this.labelPromedio = new System.Windows.Forms.Label();
            this.labelServicio = new System.Windows.Forms.Label();
            this.labelDistancia = new System.Windows.Forms.Label();
            this.serviciosCombo = new System.Windows.Forms.ComboBox();
            this.calificacionNumeric = new System.Windows.Forms.NumericUpDown();
            this.distanciaNumeric = new System.Windows.Forms.NumericUpDown();
            this.buscarTrabajadoresBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.calificacionNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.distanciaNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(247, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(308, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Podés buscar trabajadores a partir de los siguientes filtros";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // labelPromedio
            // 
            this.labelPromedio.AutoSize = true;
            this.labelPromedio.Location = new System.Drawing.Point(232, 297);
            this.labelPromedio.Name = "labelPromedio";
            this.labelPromedio.Size = new System.Drawing.Size(183, 15);
            this.labelPromedio.TabIndex = 1;
            this.labelPromedio.Text = "Promedio de calificación mínimo";
            // 
            // labelServicio
            // 
            this.labelServicio.AutoSize = true;
            this.labelServicio.Location = new System.Drawing.Point(232, 202);
            this.labelServicio.Name = "labelServicio";
            this.labelServicio.Size = new System.Drawing.Size(89, 15);
            this.labelServicio.TabIndex = 2;
            this.labelServicio.Text = "Tipo de servicio";
            // 
            // labelDistancia
            // 
            this.labelDistancia.AutoSize = true;
            this.labelDistancia.Location = new System.Drawing.Point(232, 251);
            this.labelDistancia.Name = "labelDistancia";
            this.labelDistancia.Size = new System.Drawing.Size(101, 15);
            this.labelDistancia.TabIndex = 3;
            this.labelDistancia.Text = "Distancia máxima";
            // 
            // serviciosCombo
            // 
            this.serviciosCombo.FormattingEnabled = true;
            this.serviciosCombo.Location = new System.Drawing.Point(479, 202);
            this.serviciosCombo.Name = "serviciosCombo";
            this.serviciosCombo.Size = new System.Drawing.Size(121, 23);
            this.serviciosCombo.TabIndex = 4;
            // 
            // calificacionNumeric
            // 
            this.calificacionNumeric.Location = new System.Drawing.Point(480, 295);
            this.calificacionNumeric.Name = "calificacionNumeric";
            this.calificacionNumeric.Size = new System.Drawing.Size(120, 23);
            this.calificacionNumeric.TabIndex = 5;
            // 
            // distanciaNumeric
            // 
            this.distanciaNumeric.Location = new System.Drawing.Point(480, 249);
            this.distanciaNumeric.Name = "distanciaNumeric";
            this.distanciaNumeric.Size = new System.Drawing.Size(120, 23);
            this.distanciaNumeric.TabIndex = 6;
            // 
            // buscarTrabajadoresBtn
            // 
            this.buscarTrabajadoresBtn.Location = new System.Drawing.Point(356, 371);
            this.buscarTrabajadoresBtn.Name = "buscarTrabajadoresBtn";
            this.buscarTrabajadoresBtn.Size = new System.Drawing.Size(130, 42);
            this.buscarTrabajadoresBtn.TabIndex = 7;
            this.buscarTrabajadoresBtn.Text = "Buscar trabajadores";
            this.buscarTrabajadoresBtn.UseVisualStyleBackColor = true;
            this.buscarTrabajadoresBtn.Click += new System.EventHandler(this.buscarTrabajadoresBtn_Click);
            // 
            // BuscarTrabajadores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buscarTrabajadoresBtn);
            this.Controls.Add(this.distanciaNumeric);
            this.Controls.Add(this.calificacionNumeric);
            this.Controls.Add(this.serviciosCombo);
            this.Controls.Add(this.labelDistancia);
            this.Controls.Add(this.labelServicio);
            this.Controls.Add(this.labelPromedio);
            this.Controls.Add(this.label1);
            this.Name = "BuscarTrabajadores";
            this.Text = "BuscarTrabajadores";
            this.Load += new System.EventHandler(this.BuscarTrabajadores_Load);
            ((System.ComponentModel.ISupportInitialize)(this.calificacionNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.distanciaNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelPromedio;
        private System.Windows.Forms.Label labelServicio;
        private System.Windows.Forms.Label labelDistancia;
        private System.Windows.Forms.ComboBox serviciosCombo;
        private System.Windows.Forms.NumericUpDown calificacionNumeric;
        private System.Windows.Forms.NumericUpDown distanciaNumeric;
        private System.Windows.Forms.Button buscarTrabajadoresBtn;
    }
}
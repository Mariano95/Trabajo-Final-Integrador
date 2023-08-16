
namespace OyPPTP
{
    partial class Backup
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
            this.desde = new System.Windows.Forms.Label();
            this.hasta = new System.Windows.Forms.Label();
            this.guardar_resultado_en = new System.Windows.Forms.Label();
            this.desde_date = new System.Windows.Forms.DateTimePicker();
            this.hasta_date = new System.Windows.Forms.DateTimePicker();
            this.guardar_resultado_en_text = new System.Windows.Forms.TextBox();
            this.cancelar = new System.Windows.Forms.Button();
            this.iniciar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(120, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Crear copia de seguridad del sistema";
            // 
            // desde
            // 
            this.desde.AutoSize = true;
            this.desde.Location = new System.Drawing.Point(58, 75);
            this.desde.Name = "desde";
            this.desde.Size = new System.Drawing.Size(39, 15);
            this.desde.TabIndex = 1;
            this.desde.Text = "Desde";
            // 
            // hasta
            // 
            this.hasta.AutoSize = true;
            this.hasta.Location = new System.Drawing.Point(58, 104);
            this.hasta.Name = "hasta";
            this.hasta.Size = new System.Drawing.Size(37, 15);
            this.hasta.TabIndex = 2;
            this.hasta.Text = "Hasta";
            // 
            // guardar_resultado_en
            // 
            this.guardar_resultado_en.AutoSize = true;
            this.guardar_resultado_en.Location = new System.Drawing.Point(58, 131);
            this.guardar_resultado_en.Name = "guardar_resultado_en";
            this.guardar_resultado_en.Size = new System.Drawing.Size(120, 15);
            this.guardar_resultado_en.TabIndex = 3;
            this.guardar_resultado_en.Text = "Guardar resultado en ";
            // 
            // desde_date
            // 
            this.desde_date.Location = new System.Drawing.Point(202, 69);
            this.desde_date.Name = "desde_date";
            this.desde_date.Size = new System.Drawing.Size(200, 23);
            this.desde_date.TabIndex = 4;
            // 
            // hasta_date
            // 
            this.hasta_date.Location = new System.Drawing.Point(202, 98);
            this.hasta_date.Name = "hasta_date";
            this.hasta_date.Size = new System.Drawing.Size(200, 23);
            this.hasta_date.TabIndex = 5;
            // 
            // guardar_resultado_en_text
            // 
            this.guardar_resultado_en_text.Location = new System.Drawing.Point(202, 128);
            this.guardar_resultado_en_text.Name = "guardar_resultado_en_text";
            this.guardar_resultado_en_text.Size = new System.Drawing.Size(200, 23);
            this.guardar_resultado_en_text.TabIndex = 6;
            // 
            // cancelar
            // 
            this.cancelar.Location = new System.Drawing.Point(58, 184);
            this.cancelar.Name = "cancelar";
            this.cancelar.Size = new System.Drawing.Size(75, 23);
            this.cancelar.TabIndex = 7;
            this.cancelar.Text = "Cancelar";
            this.cancelar.UseVisualStyleBackColor = true;
            this.cancelar.Click += new System.EventHandler(this.cancelar_Click);
            // 
            // iniciar
            // 
            this.iniciar.Location = new System.Drawing.Point(327, 184);
            this.iniciar.Name = "iniciar";
            this.iniciar.Size = new System.Drawing.Size(75, 23);
            this.iniciar.TabIndex = 8;
            this.iniciar.Text = "Iniciar";
            this.iniciar.UseVisualStyleBackColor = true;
            this.iniciar.Click += new System.EventHandler(this.iniciar_Click);
            // 
            // Backup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 253);
            this.Controls.Add(this.iniciar);
            this.Controls.Add(this.cancelar);
            this.Controls.Add(this.guardar_resultado_en_text);
            this.Controls.Add(this.hasta_date);
            this.Controls.Add(this.desde_date);
            this.Controls.Add(this.guardar_resultado_en);
            this.Controls.Add(this.hasta);
            this.Controls.Add(this.desde);
            this.Controls.Add(this.label1);
            this.Name = "Backup";
            this.Text = "Backup";
            this.Load += new System.EventHandler(this.Backup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label desde;
        private System.Windows.Forms.Label hasta;
        private System.Windows.Forms.Label guardar_resultado_en;
        private System.Windows.Forms.DateTimePicker desde_date;
        private System.Windows.Forms.DateTimePicker hasta_date;
        private System.Windows.Forms.TextBox guardar_resultado_en_text;
        private System.Windows.Forms.Button cancelar;
        private System.Windows.Forms.Button iniciar;
    }
}
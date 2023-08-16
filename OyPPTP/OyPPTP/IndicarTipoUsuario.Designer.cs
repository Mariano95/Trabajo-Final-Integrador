
namespace OyPPTP
{
    partial class IndicarTipoUsuario
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
            this.particular = new System.Windows.Forms.Button();
            this.trabajador = new System.Windows.Forms.Button();
            this.indicar_tipo_usuario_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // particular
            // 
            this.particular.Location = new System.Drawing.Point(303, 176);
            this.particular.Name = "particular";
            this.particular.Size = new System.Drawing.Size(164, 55);
            this.particular.TabIndex = 0;
            this.particular.Text = "Particular";
            this.particular.UseVisualStyleBackColor = true;
            this.particular.Click += new System.EventHandler(this.particular_Click);
            // 
            // trabajador
            // 
            this.trabajador.Location = new System.Drawing.Point(303, 246);
            this.trabajador.Name = "trabajador";
            this.trabajador.Size = new System.Drawing.Size(164, 50);
            this.trabajador.TabIndex = 1;
            this.trabajador.Text = "Trabajador";
            this.trabajador.UseVisualStyleBackColor = true;
            this.trabajador.Click += new System.EventHandler(this.trabajador_Click);
            // 
            // indicar_tipo_usuario_label
            // 
            this.indicar_tipo_usuario_label.AutoSize = true;
            this.indicar_tipo_usuario_label.Location = new System.Drawing.Point(287, 121);
            this.indicar_tipo_usuario_label.Name = "indicar_tipo_usuario_label";
            this.indicar_tipo_usuario_label.Size = new System.Drawing.Size(180, 15);
            this.indicar_tipo_usuario_label.TabIndex = 2;
            this.indicar_tipo_usuario_label.Text = "Indicar tipo de usuario a registrar";
            // 
            // IndicarTipoUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.indicar_tipo_usuario_label);
            this.Controls.Add(this.trabajador);
            this.Controls.Add(this.particular);
            this.Name = "IndicarTipoUsuario";
            this.Text = "IndicarTipoUsuario";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.IndicarTipoUsuario_FormClosed);
            this.Load += new System.EventHandler(this.IndicarTipoUsuario_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button particular;
        private System.Windows.Forms.Button trabajador;
        private System.Windows.Forms.Label indicar_tipo_usuario_label;
    }
}
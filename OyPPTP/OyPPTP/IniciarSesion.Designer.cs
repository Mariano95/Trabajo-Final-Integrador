
namespace OyPPTP
{
    partial class IniciarSesion
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.iniciar_sesion_mail_text = new System.Windows.Forms.TextBox();
            this.iniciar_sesion_contrasena_text = new System.Windows.Forms.MaskedTextBox();
            this.iniciar_sesion_mail_label = new System.Windows.Forms.Label();
            this.iniciar_sesion_contrasena_label = new System.Windows.Forms.Label();
            this.iniciar_sesion = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // iniciar_sesion_mail_text
            // 
            this.iniciar_sesion_mail_text.Location = new System.Drawing.Point(338, 140);
            this.iniciar_sesion_mail_text.Name = "iniciar_sesion_mail_text";
            this.iniciar_sesion_mail_text.Size = new System.Drawing.Size(290, 23);
            this.iniciar_sesion_mail_text.TabIndex = 0;
            // 
            // iniciar_sesion_contrasena_text
            // 
            this.iniciar_sesion_contrasena_text.Location = new System.Drawing.Point(338, 197);
            this.iniciar_sesion_contrasena_text.Name = "iniciar_sesion_contrasena_text";
            this.iniciar_sesion_contrasena_text.Size = new System.Drawing.Size(290, 23);
            this.iniciar_sesion_contrasena_text.TabIndex = 1;
            // 
            // iniciar_sesion_mail_label
            // 
            this.iniciar_sesion_mail_label.AutoSize = true;
            this.iniciar_sesion_mail_label.Location = new System.Drawing.Point(149, 143);
            this.iniciar_sesion_mail_label.Name = "iniciar_sesion_mail_label";
            this.iniciar_sesion_mail_label.Size = new System.Drawing.Size(172, 15);
            this.iniciar_sesion_mail_label.TabIndex = 2;
            this.iniciar_sesion_mail_label.Text = "Dirección de correo electrónico";
            // 
            // iniciar_sesion_contrasena_label
            // 
            this.iniciar_sesion_contrasena_label.AutoSize = true;
            this.iniciar_sesion_contrasena_label.Location = new System.Drawing.Point(149, 200);
            this.iniciar_sesion_contrasena_label.Name = "iniciar_sesion_contrasena_label";
            this.iniciar_sesion_contrasena_label.Size = new System.Drawing.Size(67, 15);
            this.iniciar_sesion_contrasena_label.TabIndex = 3;
            this.iniciar_sesion_contrasena_label.Text = "Contraseña";
            // 
            // iniciar_sesion
            // 
            this.iniciar_sesion.Location = new System.Drawing.Point(338, 273);
            this.iniciar_sesion.Name = "iniciar_sesion";
            this.iniciar_sesion.Size = new System.Drawing.Size(135, 23);
            this.iniciar_sesion.TabIndex = 4;
            this.iniciar_sesion.Text = "Iniciar Sesión";
            this.iniciar_sesion.UseVisualStyleBackColor = true;
            this.iniciar_sesion.Click += new System.EventHandler(this.iniciar_sesion_Click);
            // 
            // IniciarSesion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.iniciar_sesion);
            this.Controls.Add(this.iniciar_sesion_contrasena_label);
            this.Controls.Add(this.iniciar_sesion_mail_label);
            this.Controls.Add(this.iniciar_sesion_contrasena_text);
            this.Controls.Add(this.iniciar_sesion_mail_text);
            this.Name = "IniciarSesion";
            this.Text = "IniciarSesion";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.IniciarSesion_FormClosed);
            this.Load += new System.EventHandler(this.IniciarSesion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox iniciar_sesion_mail_text;
        private System.Windows.Forms.MaskedTextBox iniciar_sesion_contrasena_text;
        private System.Windows.Forms.Label iniciar_sesion_mail_label;
        private System.Windows.Forms.Label iniciar_sesion_contrasena_label;
        private System.Windows.Forms.Button iniciar_sesion;
    }
}


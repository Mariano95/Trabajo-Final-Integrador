
namespace OyPPTP
{
    partial class PreLogin
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
            this.iniciar_sesion = new System.Windows.Forms.Button();
            this.registrar_usuario = new System.Windows.Forms.Button();
            this.olvide_mi_contrasena = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // iniciar_sesion
            // 
            this.iniciar_sesion.Location = new System.Drawing.Point(299, 108);
            this.iniciar_sesion.Name = "iniciar_sesion";
            this.iniciar_sesion.Size = new System.Drawing.Size(198, 54);
            this.iniciar_sesion.TabIndex = 0;
            this.iniciar_sesion.Text = "Iniciar sesión";
            this.iniciar_sesion.UseVisualStyleBackColor = true;
            this.iniciar_sesion.Click += new System.EventHandler(this.iniciar_sesion_Click);
            // 
            // registrar_usuario
            // 
            this.registrar_usuario.Location = new System.Drawing.Point(299, 196);
            this.registrar_usuario.Name = "registrar_usuario";
            this.registrar_usuario.Size = new System.Drawing.Size(198, 54);
            this.registrar_usuario.TabIndex = 1;
            this.registrar_usuario.Text = "Registrar usuario";
            this.registrar_usuario.UseVisualStyleBackColor = true;
            this.registrar_usuario.Click += new System.EventHandler(this.registrar_usuario_Click);
            // 
            // olvide_mi_contrasena
            // 
            this.olvide_mi_contrasena.AutoSize = true;
            this.olvide_mi_contrasena.Location = new System.Drawing.Point(341, 339);
            this.olvide_mi_contrasena.Name = "olvide_mi_contrasena";
            this.olvide_mi_contrasena.Size = new System.Drawing.Size(119, 15);
            this.olvide_mi_contrasena.TabIndex = 2;
            this.olvide_mi_contrasena.TabStop = true;
            this.olvide_mi_contrasena.Text = "Olvidé mi contraseña";
            this.olvide_mi_contrasena.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.olvide_mi_contrasena_LinkClicked);
            // 
            // PreLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.olvide_mi_contrasena);
            this.Controls.Add(this.registrar_usuario);
            this.Controls.Add(this.iniciar_sesion);
            this.Name = "PreLogin";
            this.Text = "PreLogin";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PreLogin_FormClosed);
            this.Load += new System.EventHandler(this.PreLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button iniciar_sesion;
        private System.Windows.Forms.Button registrar_usuario;
        private System.Windows.Forms.LinkLabel olvide_mi_contrasena;
    }
}
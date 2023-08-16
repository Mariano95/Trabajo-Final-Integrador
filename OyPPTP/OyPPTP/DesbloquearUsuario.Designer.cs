
namespace OyPPTP
{
    partial class DesbloquearUsuario
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
            this.desbloquear_usuario_label = new System.Windows.Forms.Label();
            this.mail_usuario_text = new System.Windows.Forms.TextBox();
            this.desbloquear_usuario = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // desbloquear_usuario_label
            // 
            this.desbloquear_usuario_label.AutoSize = true;
            this.desbloquear_usuario_label.Location = new System.Drawing.Point(226, 137);
            this.desbloquear_usuario_label.Name = "desbloquear_usuario_label";
            this.desbloquear_usuario_label.Size = new System.Drawing.Size(362, 15);
            this.desbloquear_usuario_label.TabIndex = 1;
            this.desbloquear_usuario_label.Text = "Ingresá la dirección de correo electrónico del usuario a desbloquear";
            // 
            // mail_usuario_text
            // 
            this.mail_usuario_text.Location = new System.Drawing.Point(278, 183);
            this.mail_usuario_text.Name = "mail_usuario_text";
            this.mail_usuario_text.Size = new System.Drawing.Size(245, 23);
            this.mail_usuario_text.TabIndex = 2;
            // 
            // desbloquear_usuario
            // 
            this.desbloquear_usuario.Location = new System.Drawing.Point(331, 246);
            this.desbloquear_usuario.Name = "desbloquear_usuario";
            this.desbloquear_usuario.Size = new System.Drawing.Size(141, 52);
            this.desbloquear_usuario.TabIndex = 3;
            this.desbloquear_usuario.Text = "Desbloquear usuario";
            this.desbloquear_usuario.UseVisualStyleBackColor = true;
            this.desbloquear_usuario.Click += new System.EventHandler(this.desbloquear_usuario_Click);
            // 
            // DesbloquearUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.desbloquear_usuario);
            this.Controls.Add(this.mail_usuario_text);
            this.Controls.Add(this.desbloquear_usuario_label);
            this.Name = "DesbloquearUsuario";
            this.Text = "DesbloquearUsuario";
            this.Load += new System.EventHandler(this.DesbloquearUsuario_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label desbloquear_usuario_label;
        private System.Windows.Forms.TextBox mail_usuario_text;
        private System.Windows.Forms.Button desbloquear_usuario;
    }
}
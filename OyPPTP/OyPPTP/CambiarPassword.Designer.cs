
namespace OyPPTP
{
    partial class CambiarPassword
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
            this.nueva_contrasena = new System.Windows.Forms.Label();
            this.nueva_contrasena_text = new System.Windows.Forms.TextBox();
            this.actualizar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nueva_contrasena
            // 
            this.nueva_contrasena.AutoSize = true;
            this.nueva_contrasena.Location = new System.Drawing.Point(235, 247);
            this.nueva_contrasena.Name = "nueva_contrasena";
            this.nueva_contrasena.Size = new System.Drawing.Size(102, 15);
            this.nueva_contrasena.TabIndex = 0;
            this.nueva_contrasena.Text = "Nueva contraseña";
            // 
            // nueva_contrasena_text
            // 
            this.nueva_contrasena_text.Location = new System.Drawing.Point(465, 244);
            this.nueva_contrasena_text.Name = "nueva_contrasena_text";
            this.nueva_contrasena_text.PasswordChar = '*';
            this.nueva_contrasena_text.Size = new System.Drawing.Size(141, 23);
            this.nueva_contrasena_text.TabIndex = 1;
            this.nueva_contrasena_text.UseSystemPasswordChar = true;
            // 
            // actualizar
            // 
            this.actualizar.Location = new System.Drawing.Point(372, 295);
            this.actualizar.Name = "actualizar";
            this.actualizar.Size = new System.Drawing.Size(75, 23);
            this.actualizar.TabIndex = 2;
            this.actualizar.Text = "Actualizar";
            this.actualizar.UseVisualStyleBackColor = true;
            this.actualizar.Click += new System.EventHandler(this.actualizar_Click);
            // 
            // CambiarPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 450);
            this.Controls.Add(this.actualizar);
            this.Controls.Add(this.nueva_contrasena_text);
            this.Controls.Add(this.nueva_contrasena);
            this.Name = "CambiarPassword";
            this.Text = "CambiarPassword";
            this.Load += new System.EventHandler(this.CambiarPassword_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nueva_contrasena;
        private System.Windows.Forms.TextBox nueva_contrasena_text;
        private System.Windows.Forms.Button actualizar;
    }
}
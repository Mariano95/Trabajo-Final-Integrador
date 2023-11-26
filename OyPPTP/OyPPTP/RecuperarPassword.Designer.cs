
namespace UI
{
    partial class RecuperarPassword
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
            this.recuperar_password_dni = new System.Windows.Forms.TextBox();
            this.recuperar_password_aceptar = new System.Windows.Forms.Button();
            this.recuperar_password_cancelar = new System.Windows.Forms.Button();
            this.recuperar_password_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // recuperar_password_dni
            // 
            this.recuperar_password_dni.Location = new System.Drawing.Point(259, 206);
            this.recuperar_password_dni.Name = "recuperar_password_dni";
            this.recuperar_password_dni.Size = new System.Drawing.Size(277, 23);
            this.recuperar_password_dni.TabIndex = 0;
            // 
            // recuperar_password_aceptar
            // 
            this.recuperar_password_aceptar.Location = new System.Drawing.Point(259, 297);
            this.recuperar_password_aceptar.Name = "recuperar_password_aceptar";
            this.recuperar_password_aceptar.Size = new System.Drawing.Size(106, 23);
            this.recuperar_password_aceptar.TabIndex = 1;
            this.recuperar_password_aceptar.Text = "Aceptar";
            this.recuperar_password_aceptar.UseVisualStyleBackColor = true;
            this.recuperar_password_aceptar.Click += new System.EventHandler(this.recuperar_password_aceptar_Click);
            // 
            // recuperar_password_cancelar
            // 
            this.recuperar_password_cancelar.Location = new System.Drawing.Point(430, 297);
            this.recuperar_password_cancelar.Name = "recuperar_password_cancelar";
            this.recuperar_password_cancelar.Size = new System.Drawing.Size(106, 23);
            this.recuperar_password_cancelar.TabIndex = 2;
            this.recuperar_password_cancelar.Text = "Cancelar";
            this.recuperar_password_cancelar.UseVisualStyleBackColor = true;
            this.recuperar_password_cancelar.Click += new System.EventHandler(this.recuperar_password_cancelar_Click);
            // 
            // recuperar_password_label
            // 
            this.recuperar_password_label.AutoSize = true;
            this.recuperar_password_label.Location = new System.Drawing.Point(300, 160);
            this.recuperar_password_label.Name = "recuperar_password_label";
            this.recuperar_password_label.Size = new System.Drawing.Size(196, 15);
            this.recuperar_password_label.TabIndex = 3;
            this.recuperar_password_label.Text = "Por favor, escribí tu número de dni. ";
            this.recuperar_password_label.Click += new System.EventHandler(this.label1_Click);
            // 
            // RecuperarPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.recuperar_password_label);
            this.Controls.Add(this.recuperar_password_cancelar);
            this.Controls.Add(this.recuperar_password_aceptar);
            this.Controls.Add(this.recuperar_password_dni);
            this.Name = "RecuperarPassword";
            this.Text = "RecuperarPassword";
            this.Load += new System.EventHandler(this.RecuperarPassword_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox recuperar_password_dni;
        private System.Windows.Forms.Button recuperar_password_aceptar;
        private System.Windows.Forms.Button recuperar_password_cancelar;
        private System.Windows.Forms.Label recuperar_password_label;
    }
}
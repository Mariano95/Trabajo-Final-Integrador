
namespace OyPPTP
{
    partial class RegistrarUsuario
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
            this.ingresar_datos_personales = new System.Windows.Forms.Label();
            this.nombre_text = new System.Windows.Forms.TextBox();
            this.apellido_text = new System.Windows.Forms.TextBox();
            this.domicilio_text = new System.Windows.Forms.TextBox();
            this.email_text = new System.Windows.Forms.TextBox();
            this.contrasena_text = new System.Windows.Forms.TextBox();
            this.dni_text = new System.Windows.Forms.TextBox();
            this.nombre = new System.Windows.Forms.Label();
            this.apellido = new System.Windows.Forms.Label();
            this.dni = new System.Windows.Forms.Label();
            this.domicilio = new System.Windows.Forms.Label();
            this.email = new System.Windows.Forms.Label();
            this.contrasena = new System.Windows.Forms.Label();
            this.continuar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ingresar_datos_personales
            // 
            this.ingresar_datos_personales.AutoSize = true;
            this.ingresar_datos_personales.Location = new System.Drawing.Point(47, 68);
            this.ingresar_datos_personales.Name = "ingresar_datos_personales";
            this.ingresar_datos_personales.Size = new System.Drawing.Size(140, 15);
            this.ingresar_datos_personales.TabIndex = 0;
            this.ingresar_datos_personales.Text = "Ingresar datos personales";
            // 
            // nombre_text
            // 
            this.nombre_text.Location = new System.Drawing.Point(291, 110);
            this.nombre_text.Name = "nombre_text";
            this.nombre_text.Size = new System.Drawing.Size(100, 23);
            this.nombre_text.TabIndex = 1;
            // 
            // apellido_text
            // 
            this.apellido_text.Location = new System.Drawing.Point(291, 157);
            this.apellido_text.Name = "apellido_text";
            this.apellido_text.Size = new System.Drawing.Size(100, 23);
            this.apellido_text.TabIndex = 2;
            // 
            // domicilio_text
            // 
            this.domicilio_text.Location = new System.Drawing.Point(291, 239);
            this.domicilio_text.Name = "domicilio_text";
            this.domicilio_text.Size = new System.Drawing.Size(100, 23);
            this.domicilio_text.TabIndex = 3;
            // 
            // email_text
            // 
            this.email_text.Location = new System.Drawing.Point(291, 282);
            this.email_text.Name = "email_text";
            this.email_text.Size = new System.Drawing.Size(100, 23);
            this.email_text.TabIndex = 4;
            // 
            // contrasena_text
            // 
            this.contrasena_text.Location = new System.Drawing.Point(291, 321);
            this.contrasena_text.Name = "contrasena_text";
            this.contrasena_text.Size = new System.Drawing.Size(100, 23);
            this.contrasena_text.TabIndex = 5;
            // 
            // dni_text
            // 
            this.dni_text.Location = new System.Drawing.Point(291, 198);
            this.dni_text.Name = "dni_text";
            this.dni_text.Size = new System.Drawing.Size(100, 23);
            this.dni_text.TabIndex = 6;
            // 
            // nombre
            // 
            this.nombre.AutoSize = true;
            this.nombre.Location = new System.Drawing.Point(108, 118);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(51, 15);
            this.nombre.TabIndex = 7;
            this.nombre.Text = "Nombre";
            // 
            // apellido
            // 
            this.apellido.AutoSize = true;
            this.apellido.Location = new System.Drawing.Point(108, 162);
            this.apellido.Name = "apellido";
            this.apellido.Size = new System.Drawing.Size(51, 15);
            this.apellido.TabIndex = 8;
            this.apellido.Text = "Apellido";
            // 
            // dni
            // 
            this.dni.AutoSize = true;
            this.dni.Location = new System.Drawing.Point(108, 203);
            this.dni.Name = "dni";
            this.dni.Size = new System.Drawing.Size(27, 15);
            this.dni.TabIndex = 9;
            this.dni.Text = "DNI";
            // 
            // domicilio
            // 
            this.domicilio.AutoSize = true;
            this.domicilio.Location = new System.Drawing.Point(108, 244);
            this.domicilio.Name = "domicilio";
            this.domicilio.Size = new System.Drawing.Size(58, 15);
            this.domicilio.TabIndex = 10;
            this.domicilio.Text = "Domicilio";
            // 
            // email
            // 
            this.email.AutoSize = true;
            this.email.Location = new System.Drawing.Point(108, 290);
            this.email.Name = "email";
            this.email.Size = new System.Drawing.Size(36, 15);
            this.email.TabIndex = 11;
            this.email.Text = "Email";
            // 
            // contrasena
            // 
            this.contrasena.AutoSize = true;
            this.contrasena.Location = new System.Drawing.Point(108, 329);
            this.contrasena.Name = "contrasena";
            this.contrasena.Size = new System.Drawing.Size(67, 15);
            this.contrasena.TabIndex = 12;
            this.contrasena.Text = "Contraseña";
            // 
            // continuar
            // 
            this.continuar.Location = new System.Drawing.Point(47, 391);
            this.continuar.Name = "continuar";
            this.continuar.Size = new System.Drawing.Size(75, 23);
            this.continuar.TabIndex = 13;
            this.continuar.Text = "Continuar";
            this.continuar.UseVisualStyleBackColor = true;
            this.continuar.Click += new System.EventHandler(this.continuar_Click);
            // 
            // RegistrarUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.continuar);
            this.Controls.Add(this.contrasena);
            this.Controls.Add(this.email);
            this.Controls.Add(this.domicilio);
            this.Controls.Add(this.dni);
            this.Controls.Add(this.apellido);
            this.Controls.Add(this.nombre);
            this.Controls.Add(this.dni_text);
            this.Controls.Add(this.contrasena_text);
            this.Controls.Add(this.email_text);
            this.Controls.Add(this.domicilio_text);
            this.Controls.Add(this.apellido_text);
            this.Controls.Add(this.nombre_text);
            this.Controls.Add(this.ingresar_datos_personales);
            this.Name = "RegistrarUsuario";
            this.Text = "RegistrarUsuario";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ingresar_datos_personales;
        private System.Windows.Forms.TextBox nombre_text;
        private System.Windows.Forms.TextBox apellido_text;
        private System.Windows.Forms.TextBox domicilio_text;
        private System.Windows.Forms.TextBox email_text;
        private System.Windows.Forms.TextBox contrasena_text;
        private System.Windows.Forms.TextBox dni_text;
        private System.Windows.Forms.Label nombre;
        private System.Windows.Forms.Label apellido;
        private System.Windows.Forms.Label dni;
        private System.Windows.Forms.Label domicilio;
        private System.Windows.Forms.Label email;
        private System.Windows.Forms.Label contrasena;
        private System.Windows.Forms.Button continuar;
        private System.Windows.Forms.Button ontinuar;
    }
}
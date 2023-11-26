
namespace OyPPTP
{
    partial class CredencialesBD
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
            this.credencialesBD_label = new System.Windows.Forms.Label();
            this.servidor = new System.Windows.Forms.Label();
            this.usuario = new System.Windows.Forms.Label();
            this.contrasena = new System.Windows.Forms.Label();
            this.servidor_text = new System.Windows.Forms.TextBox();
            this.contrasena_text = new System.Windows.Forms.TextBox();
            this.usuario_text = new System.Windows.Forms.TextBox();
            this.conectar = new System.Windows.Forms.Button();
            this.windows_radioButton = new System.Windows.Forms.RadioButton();
            this.sql_radioButton = new System.Windows.Forms.RadioButton();
            this.tipo_autenticacion_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // credencialesBD_label
            // 
            this.credencialesBD_label.AutoSize = true;
            this.credencialesBD_label.Location = new System.Drawing.Point(52, 94);
            this.credencialesBD_label.Name = "credencialesBD_label";
            this.credencialesBD_label.Size = new System.Drawing.Size(739, 15);
            this.credencialesBD_label.TabIndex = 1;
            this.credencialesBD_label.Text = "No se ha podido establecer conexión con la base de datos de manera automática. Po" +
    "r favor, indique a cuál base de datos desea conectarse.";
            // 
            // servidor
            // 
            this.servidor.AutoSize = true;
            this.servidor.Location = new System.Drawing.Point(275, 148);
            this.servidor.Name = "servidor";
            this.servidor.Size = new System.Drawing.Size(50, 15);
            this.servidor.TabIndex = 2;
            this.servidor.Text = "Servidor";
            // 
            // usuario
            // 
            this.usuario.AutoSize = true;
            this.usuario.Location = new System.Drawing.Point(278, 248);
            this.usuario.Name = "usuario";
            this.usuario.Size = new System.Drawing.Size(47, 15);
            this.usuario.TabIndex = 3;
            this.usuario.Text = "Usuario";
            // 
            // contrasena
            // 
            this.contrasena.AutoSize = true;
            this.contrasena.Location = new System.Drawing.Point(275, 307);
            this.contrasena.Name = "contrasena";
            this.contrasena.Size = new System.Drawing.Size(67, 15);
            this.contrasena.TabIndex = 4;
            this.contrasena.Text = "Contraseña";
            // 
            // servidor_text
            // 
            this.servidor_text.Location = new System.Drawing.Point(423, 145);
            this.servidor_text.Name = "servidor_text";
            this.servidor_text.Size = new System.Drawing.Size(137, 23);
            this.servidor_text.TabIndex = 5;
            // 
            // contrasena_text
            // 
            this.contrasena_text.Location = new System.Drawing.Point(423, 304);
            this.contrasena_text.Name = "contrasena_text";
            this.contrasena_text.Size = new System.Drawing.Size(137, 23);
            this.contrasena_text.TabIndex = 6;
            // 
            // usuario_text
            // 
            this.usuario_text.Location = new System.Drawing.Point(423, 248);
            this.usuario_text.Name = "usuario_text";
            this.usuario_text.Size = new System.Drawing.Size(137, 23);
            this.usuario_text.TabIndex = 7;
            // 
            // conectar
            // 
            this.conectar.Location = new System.Drawing.Point(386, 374);
            this.conectar.Name = "conectar";
            this.conectar.Size = new System.Drawing.Size(75, 23);
            this.conectar.TabIndex = 9;
            this.conectar.Text = "Conectar";
            this.conectar.UseVisualStyleBackColor = true;
            this.conectar.Click += new System.EventHandler(this.conectar_Click);
            // 
            // windows_radioButton
            // 
            this.windows_radioButton.AutoSize = true;
            this.windows_radioButton.Location = new System.Drawing.Point(423, 184);
            this.windows_radioButton.Name = "windows_radioButton";
            this.windows_radioButton.Size = new System.Drawing.Size(167, 19);
            this.windows_radioButton.TabIndex = 10;
            this.windows_radioButton.Text = "Autenticación de Windows";
            this.windows_radioButton.UseVisualStyleBackColor = true;
            this.windows_radioButton.CheckedChanged += new System.EventHandler(this.CredencialesBD_CheckedChanged);
            // 
            // sql_radioButton
            // 
            this.sql_radioButton.AutoSize = true;
            this.sql_radioButton.Location = new System.Drawing.Point(423, 209);
            this.sql_radioButton.Name = "sql_radioButton";
            this.sql_radioButton.Size = new System.Drawing.Size(174, 19);
            this.sql_radioButton.TabIndex = 11;
            this.sql_radioButton.TabStop = true;
            this.sql_radioButton.Text = "Autenticación de SQL Server";
            this.sql_radioButton.UseVisualStyleBackColor = true;
            // 
            // tipo_autenticacion_label
            // 
            this.tipo_autenticacion_label.AutoSize = true;
            this.tipo_autenticacion_label.Location = new System.Drawing.Point(278, 198);
            this.tipo_autenticacion_label.Name = "tipo_autenticacion_label";
            this.tipo_autenticacion_label.Size = new System.Drawing.Size(81, 15);
            this.tipo_autenticacion_label.TabIndex = 12;
            this.tipo_autenticacion_label.Text = "Autenticación";
            // 
            // CredencialesBD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 450);
            this.Controls.Add(this.tipo_autenticacion_label);
            this.Controls.Add(this.sql_radioButton);
            this.Controls.Add(this.windows_radioButton);
            this.Controls.Add(this.conectar);
            this.Controls.Add(this.usuario_text);
            this.Controls.Add(this.contrasena_text);
            this.Controls.Add(this.servidor_text);
            this.Controls.Add(this.contrasena);
            this.Controls.Add(this.usuario);
            this.Controls.Add(this.servidor);
            this.Controls.Add(this.credencialesBD_label);
            this.Name = "CredencialesBD";
            this.Text = "CredencialesBD";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CredencialesBD_FormClosed);
            this.Load += new System.EventHandler(this.CredencialesBD_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label credencialesBD_label;
        private System.Windows.Forms.Label servidor;
        private System.Windows.Forms.Label usuario;
        private System.Windows.Forms.Label contrasena;
        private System.Windows.Forms.TextBox servidor_text;
        private System.Windows.Forms.TextBox contrasena_text;
        private System.Windows.Forms.TextBox usuario_text;
        private System.Windows.Forms.Button conectar;
        private System.Windows.Forms.RadioButton windows_radioButton;
        private System.Windows.Forms.RadioButton sql_radioButton;
        private System.Windows.Forms.Label tipo_autenticacion_label;
    }
}
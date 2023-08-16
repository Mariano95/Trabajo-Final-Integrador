
namespace OyPPTP
{
    partial class RestaurarSistema
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
            this.iniciar = new System.Windows.Forms.Button();
            this.cancelar = new System.Windows.Forms.Button();
            this.directorio_del_archivo_de_backup_text = new System.Windows.Forms.TextBox();
            this.directorio_del_archivo_de_backup = new System.Windows.Forms.Label();
            this.restaurar_sistema = new System.Windows.Forms.Label();
            this.nombre_del_archivo_de_backup_text = new System.Windows.Forms.TextBox();
            this.nombre_del_archivo_de_backup = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // iniciar
            // 
            this.iniciar.Location = new System.Drawing.Point(321, 202);
            this.iniciar.Name = "iniciar";
            this.iniciar.Size = new System.Drawing.Size(75, 23);
            this.iniciar.TabIndex = 17;
            this.iniciar.Text = "Iniciar";
            this.iniciar.UseVisualStyleBackColor = true;
            this.iniciar.Click += new System.EventHandler(this.iniciar_Click);
            // 
            // cancelar
            // 
            this.cancelar.Location = new System.Drawing.Point(52, 202);
            this.cancelar.Name = "cancelar";
            this.cancelar.Size = new System.Drawing.Size(75, 23);
            this.cancelar.TabIndex = 16;
            this.cancelar.Text = "Cancelar";
            this.cancelar.UseVisualStyleBackColor = true;
            this.cancelar.Click += new System.EventHandler(this.cancelar_Click);
            // 
            // directorio_del_archivo_de_backup_text
            // 
            this.directorio_del_archivo_de_backup_text.Location = new System.Drawing.Point(236, 77);
            this.directorio_del_archivo_de_backup_text.Name = "directorio_del_archivo_de_backup_text";
            this.directorio_del_archivo_de_backup_text.Size = new System.Drawing.Size(200, 23);
            this.directorio_del_archivo_de_backup_text.TabIndex = 15;
            // 
            // directorio_del_archivo_de_backup
            // 
            this.directorio_del_archivo_de_backup.AutoSize = true;
            this.directorio_del_archivo_de_backup.Location = new System.Drawing.Point(35, 80);
            this.directorio_del_archivo_de_backup.Name = "directorio_del_archivo_de_backup";
            this.directorio_del_archivo_de_backup.Size = new System.Drawing.Size(178, 15);
            this.directorio_del_archivo_de_backup.TabIndex = 12;
            this.directorio_del_archivo_de_backup.Text = "Directorio del archivo de backup";
            // 
            // restaurar_sistema
            // 
            this.restaurar_sistema.AutoSize = true;
            this.restaurar_sistema.Location = new System.Drawing.Point(177, 27);
            this.restaurar_sistema.Name = "restaurar_sistema";
            this.restaurar_sistema.Size = new System.Drawing.Size(99, 15);
            this.restaurar_sistema.TabIndex = 9;
            this.restaurar_sistema.Text = "Restaurar sistema";
            // 
            // nombre_del_archivo_de_backup_text
            // 
            this.nombre_del_archivo_de_backup_text.Location = new System.Drawing.Point(236, 127);
            this.nombre_del_archivo_de_backup_text.Name = "nombre_del_archivo_de_backup_text";
            this.nombre_del_archivo_de_backup_text.Size = new System.Drawing.Size(200, 23);
            this.nombre_del_archivo_de_backup_text.TabIndex = 18;
            // 
            // nombre_del_archivo_de_backup
            // 
            this.nombre_del_archivo_de_backup.AutoSize = true;
            this.nombre_del_archivo_de_backup.Location = new System.Drawing.Point(35, 130);
            this.nombre_del_archivo_de_backup.Name = "nombre_del_archivo_de_backup";
            this.nombre_del_archivo_de_backup.Size = new System.Drawing.Size(170, 15);
            this.nombre_del_archivo_de_backup.TabIndex = 19;
            this.nombre_del_archivo_de_backup.Text = "Nombre del archivo de backup";
            // 
            // RestaurarSistema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 252);
            this.Controls.Add(this.nombre_del_archivo_de_backup);
            this.Controls.Add(this.nombre_del_archivo_de_backup_text);
            this.Controls.Add(this.iniciar);
            this.Controls.Add(this.cancelar);
            this.Controls.Add(this.directorio_del_archivo_de_backup_text);
            this.Controls.Add(this.directorio_del_archivo_de_backup);
            this.Controls.Add(this.restaurar_sistema);
            this.Name = "RestaurarSistema";
            this.Text = "RestaurarSistema";
            this.Load += new System.EventHandler(this.RestaurarSistema_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button iniciar;
        private System.Windows.Forms.Button cancelar;
        private System.Windows.Forms.TextBox directorio_del_archivo_de_backup_text;
        private System.Windows.Forms.Label directorio_del_archivo_de_backup;
        private System.Windows.Forms.Label restaurar_sistema;
        private System.Windows.Forms.TextBox nombre_del_archivo_de_backup_text;
        private System.Windows.Forms.Label nombre_del_archivo_de_backup;
    }
}
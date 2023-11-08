
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
            this.restaurar_sistema = new System.Windows.Forms.Label();
            this.aguarde_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // iniciar
            // 
            this.iniciar.Location = new System.Drawing.Point(187, 83);
            this.iniciar.Name = "iniciar";
            this.iniciar.Size = new System.Drawing.Size(75, 23);
            this.iniciar.TabIndex = 17;
            this.iniciar.Text = "Iniciar";
            this.iniciar.UseVisualStyleBackColor = true;
            this.iniciar.Click += new System.EventHandler(this.iniciar_Click);
            // 
            // cancelar
            // 
            this.cancelar.Location = new System.Drawing.Point(187, 112);
            this.cancelar.Name = "cancelar";
            this.cancelar.Size = new System.Drawing.Size(75, 23);
            this.cancelar.TabIndex = 16;
            this.cancelar.Text = "Cancelar";
            this.cancelar.UseVisualStyleBackColor = true;
            this.cancelar.Click += new System.EventHandler(this.cancelar_Click);
            // 
            // restaurar_sistema
            // 
            this.restaurar_sistema.AutoSize = true;
            this.restaurar_sistema.Location = new System.Drawing.Point(176, 37);
            this.restaurar_sistema.Name = "restaurar_sistema";
            this.restaurar_sistema.Size = new System.Drawing.Size(99, 15);
            this.restaurar_sistema.TabIndex = 9;
            this.restaurar_sistema.Text = "Restaurar sistema";
            // 
            // aguarde_label
            // 
            this.aguarde_label.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.aguarde_label.AutoSize = true;
            this.aguarde_label.Location = new System.Drawing.Point(114, 37);
            this.aguarde_label.Name = "aguarde_label";
            this.aguarde_label.Size = new System.Drawing.Size(226, 15);
            this.aguarde_label.TabIndex = 19;
            this.aguarde_label.Text = "Aguarde mientras se restauran los datos...";
            this.aguarde_label.UseMnemonic = false;
            this.aguarde_label.Visible = false;
            // 
            // RestaurarSistema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 252);
            this.Controls.Add(this.aguarde_label);
            this.Controls.Add(this.iniciar);
            this.Controls.Add(this.cancelar);
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
        private System.Windows.Forms.Label restaurar_sistema;
        private System.Windows.Forms.Label aguarde_label;
    }
}
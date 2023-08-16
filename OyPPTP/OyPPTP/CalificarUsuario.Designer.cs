
namespace OyPPTP
{
    partial class CalificarUsuario
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
            this.components = new System.ComponentModel.Container();
            this.calificar_usuario_label = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.calificar_usuario_numeric = new System.Windows.Forms.NumericUpDown();
            this.guardar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.calificar_usuario_numeric)).BeginInit();
            this.SuspendLayout();
            // 
            // calificar_usuario_label
            // 
            this.calificar_usuario_label.AllowDrop = true;
            this.calificar_usuario_label.AutoSize = true;
            this.calificar_usuario_label.Location = new System.Drawing.Point(18, 67);
            this.calificar_usuario_label.Name = "calificar_usuario_label";
            this.calificar_usuario_label.Size = new System.Drawing.Size(643, 15);
            this.calificar_usuario_label.TabIndex = 0;
            this.calificar_usuario_label.Text = "Expresá tu nivel de satisfacción del 0 al 100, donde 100 representa \'Totalmente s" +
    "atisfecho\' y 0 representa \'Nada satisfecho\'";
            this.calificar_usuario_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // calificar_usuario_numeric
            // 
            this.calificar_usuario_numeric.Location = new System.Drawing.Point(280, 123);
            this.calificar_usuario_numeric.Name = "calificar_usuario_numeric";
            this.calificar_usuario_numeric.Size = new System.Drawing.Size(125, 23);
            this.calificar_usuario_numeric.TabIndex = 2;
            // 
            // guardar
            // 
            this.guardar.Location = new System.Drawing.Point(280, 204);
            this.guardar.Name = "guardar";
            this.guardar.Size = new System.Drawing.Size(125, 44);
            this.guardar.TabIndex = 3;
            this.guardar.Text = "Guardar";
            this.guardar.UseVisualStyleBackColor = true;
            this.guardar.Click += new System.EventHandler(this.guardar_Click);
            // 
            // CalificarUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 373);
            this.Controls.Add(this.guardar);
            this.Controls.Add(this.calificar_usuario_numeric);
            this.Controls.Add(this.calificar_usuario_label);
            this.Name = "CalificarUsuario";
            this.Text = "CalificarUsuario";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.calificar_usuario_numeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label calificar_usuario_label;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.NumericUpDown calificar_usuario_numeric;
        private System.Windows.Forms.Button guardar;
    }
}

namespace OyPPTP
{
    partial class ComentarCitacion
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
            this.comentar_citacion_grilla = new System.Windows.Forms.DataGridView();
            this.comentar_citacion_textbox = new System.Windows.Forms.RichTextBox();
            this.agregar_comentario = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.comentar_citacion_grilla)).BeginInit();
            this.SuspendLayout();
            // 
            // comentar_citacion_grilla
            // 
            this.comentar_citacion_grilla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.comentar_citacion_grilla.Location = new System.Drawing.Point(12, 46);
            this.comentar_citacion_grilla.Name = "comentar_citacion_grilla";
            this.comentar_citacion_grilla.RowTemplate.Height = 25;
            this.comentar_citacion_grilla.Size = new System.Drawing.Size(760, 229);
            this.comentar_citacion_grilla.TabIndex = 0;
            // 
            // comentar_citacion_textbox
            // 
            this.comentar_citacion_textbox.Location = new System.Drawing.Point(12, 301);
            this.comentar_citacion_textbox.Name = "comentar_citacion_textbox";
            this.comentar_citacion_textbox.Size = new System.Drawing.Size(776, 75);
            this.comentar_citacion_textbox.TabIndex = 1;
            this.comentar_citacion_textbox.Text = "";
            // 
            // agregar_comentario
            // 
            this.agregar_comentario.Location = new System.Drawing.Point(311, 391);
            this.agregar_comentario.Name = "agregar_comentario";
            this.agregar_comentario.Size = new System.Drawing.Size(181, 47);
            this.agregar_comentario.TabIndex = 2;
            this.agregar_comentario.Text = "Agregar comentario";
            this.agregar_comentario.UseVisualStyleBackColor = true;
            this.agregar_comentario.Click += new System.EventHandler(this.agregar_comentario_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Historial de comentarios";
            // 
            // ComentarCitacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.agregar_comentario);
            this.Controls.Add(this.comentar_citacion_textbox);
            this.Controls.Add(this.comentar_citacion_grilla);
            this.Name = "ComentarCitacion";
            this.Text = "ComentarCitacion";
            this.Load += new System.EventHandler(this.ComentarCitacion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.comentar_citacion_grilla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RichTextBox comentar_citacion_textbox;
        private System.Windows.Forms.Button agregar_comentario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView comentar_citacion_grilla;
    }
}
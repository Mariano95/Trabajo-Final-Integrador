
namespace OyPPTP
{
    partial class Bitacora
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
            this.grilla_bitacora = new System.Windows.Forms.DataGridView();
            this.cerrar_bitacora = new System.Windows.Forms.Button();
            this.cambiar_filtros = new System.Windows.Forms.Button();
            this.imprimir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grilla_bitacora)).BeginInit();
            this.SuspendLayout();
            // 
            // grilla_bitacora
            // 
            this.grilla_bitacora.AllowUserToAddRows = false;
            this.grilla_bitacora.AllowUserToDeleteRows = false;
            this.grilla_bitacora.AllowUserToOrderColumns = true;
            this.grilla_bitacora.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grilla_bitacora.Location = new System.Drawing.Point(12, 12);
            this.grilla_bitacora.Name = "grilla_bitacora";
            this.grilla_bitacora.ReadOnly = true;
            this.grilla_bitacora.RowTemplate.Height = 25;
            this.grilla_bitacora.Size = new System.Drawing.Size(840, 323);
            this.grilla_bitacora.TabIndex = 0;
            // 
            // cerrar_bitacora
            // 
            this.cerrar_bitacora.Location = new System.Drawing.Point(23, 379);
            this.cerrar_bitacora.Name = "cerrar_bitacora";
            this.cerrar_bitacora.Size = new System.Drawing.Size(137, 39);
            this.cerrar_bitacora.TabIndex = 1;
            this.cerrar_bitacora.Text = "Cerrar bitácora";
            this.cerrar_bitacora.UseVisualStyleBackColor = true;
            this.cerrar_bitacora.Click += new System.EventHandler(this.cerrar_bitacora_Click);
            // 
            // cambiar_filtros
            // 
            this.cambiar_filtros.Location = new System.Drawing.Point(352, 379);
            this.cambiar_filtros.Name = "cambiar_filtros";
            this.cambiar_filtros.Size = new System.Drawing.Size(147, 39);
            this.cambiar_filtros.TabIndex = 2;
            this.cambiar_filtros.Text = "Cambiar filtros";
            this.cambiar_filtros.UseVisualStyleBackColor = true;
            this.cambiar_filtros.Click += new System.EventHandler(this.cambiar_filtros_Click);
            // 
            // imprimir
            // 
            this.imprimir.Location = new System.Drawing.Point(713, 379);
            this.imprimir.Name = "imprimir";
            this.imprimir.Size = new System.Drawing.Size(132, 39);
            this.imprimir.TabIndex = 3;
            this.imprimir.Text = "Imprimir";
            this.imprimir.UseVisualStyleBackColor = true;
            this.imprimir.Click += new System.EventHandler(this.imprimir_Click);
            // 
            // Bitacora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 450);
            this.Controls.Add(this.imprimir);
            this.Controls.Add(this.cambiar_filtros);
            this.Controls.Add(this.cerrar_bitacora);
            this.Controls.Add(this.grilla_bitacora);
            this.Name = "Bitacora";
            this.Text = "Bitacora";
            this.Load += new System.EventHandler(this.Bitacora_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grilla_bitacora)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grilla_bitacora;
        private System.Windows.Forms.Button cerrar_bitacora;
        private System.Windows.Forms.Button cambiar_filtros;
        private System.Windows.Forms.Button imprimir;
    }
}
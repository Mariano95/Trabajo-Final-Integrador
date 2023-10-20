
namespace OyPPTP
{
    partial class PatentesPorGrupo
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
            this.grupo = new System.Windows.Forms.Label();
            this.grupo_combo = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.patentesOtorgadas = new System.Windows.Forms.DataGridView();
            this.quitarPatente = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.otorgarPatente = new System.Windows.Forms.Button();
            this.patentesNoOtorgadas = new System.Windows.Forms.DataGridView();
            this.patentes_no_otorgadas = new System.Windows.Forms.Label();
            this.patentes_otorgadas = new System.Windows.Forms.Label();
            this.volver = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patentesOtorgadas)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patentesNoOtorgadas)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // grupo
            // 
            this.grupo.AutoSize = true;
            this.grupo.Location = new System.Drawing.Point(12, 14);
            this.grupo.Name = "grupo";
            this.grupo.Size = new System.Drawing.Size(40, 15);
            this.grupo.TabIndex = 0;
            this.grupo.Text = "Grupo";
            // 
            // grupo_combo
            // 
            this.grupo_combo.FormattingEnabled = true;
            
            this.grupo_combo.Location = new System.Drawing.Point(119, 6);
            this.grupo_combo.Name = "grupo_combo";
            this.grupo_combo.Size = new System.Drawing.Size(232, 23);
            this.grupo_combo.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.patentesOtorgadas);
            this.panel1.Controls.Add(this.quitarPatente);
            this.panel1.Location = new System.Drawing.Point(44, 117);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(351, 253);
            this.panel1.TabIndex = 12;
            // 
            // patentesOtorgadas
            // 
            this.patentesOtorgadas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.patentesOtorgadas.Location = new System.Drawing.Point(12, 16);
            this.patentesOtorgadas.Name = "patentesOtorgadas";
            this.patentesOtorgadas.RowTemplate.Height = 25;
            this.patentesOtorgadas.Size = new System.Drawing.Size(331, 181);
            this.patentesOtorgadas.TabIndex = 0;
            // 
            // quitarPatente
            // 
            this.quitarPatente.Location = new System.Drawing.Point(229, 203);
            this.quitarPatente.Name = "quitarPatente";
            this.quitarPatente.Size = new System.Drawing.Size(114, 29);
            this.quitarPatente.TabIndex = 9;
            this.quitarPatente.Text = "Quitar patente";
            this.quitarPatente.UseVisualStyleBackColor = true;
            this.quitarPatente.Click += new System.EventHandler(this.quitarPatente_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.otorgarPatente);
            this.panel2.Controls.Add(this.patentesNoOtorgadas);
            this.panel2.Location = new System.Drawing.Point(405, 117);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(351, 253);
            this.panel2.TabIndex = 11;
            // 
            // otorgarPatente
            // 
            this.otorgarPatente.Location = new System.Drawing.Point(226, 203);
            this.otorgarPatente.Name = "otorgarPatente";
            this.otorgarPatente.Size = new System.Drawing.Size(114, 29);
            this.otorgarPatente.TabIndex = 10;
            this.otorgarPatente.Text = "Otorgar patente";
            this.otorgarPatente.UseVisualStyleBackColor = true;
            this.otorgarPatente.Click += new System.EventHandler(this.otorgarPatente_Click);
            // 
            // patentesNoOtorgadas
            // 
            this.patentesNoOtorgadas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.patentesNoOtorgadas.Location = new System.Drawing.Point(9, 16);
            this.patentesNoOtorgadas.Name = "patentesNoOtorgadas";
            this.patentesNoOtorgadas.RowTemplate.Height = 25;
            this.patentesNoOtorgadas.Size = new System.Drawing.Size(331, 181);
            this.patentesNoOtorgadas.TabIndex = 1;
            // 
            // patentes_no_otorgadas
            // 
            this.patentes_no_otorgadas.AutoSize = true;
            this.patentes_no_otorgadas.Location = new System.Drawing.Point(414, 90);
            this.patentes_no_otorgadas.Name = "patentes_no_otorgadas";
            this.patentes_no_otorgadas.Size = new System.Drawing.Size(125, 15);
            this.patentes_no_otorgadas.TabIndex = 10;
            this.patentes_no_otorgadas.Text = "Patentes no otorgadas";
            // 
            // patentes_otorgadas
            // 
            this.patentes_otorgadas.AutoSize = true;
            this.patentes_otorgadas.Location = new System.Drawing.Point(56, 90);
            this.patentes_otorgadas.Name = "patentes_otorgadas";
            this.patentes_otorgadas.Size = new System.Drawing.Size(108, 15);
            this.patentes_otorgadas.TabIndex = 9;
            this.patentes_otorgadas.Text = "Patentes otorgadas";
            // 
            // volver
            // 
            this.volver.Location = new System.Drawing.Point(44, 393);
            this.volver.Name = "volver";
            this.volver.Size = new System.Drawing.Size(100, 33);
            this.volver.TabIndex = 14;
            this.volver.Text = "Volver";
            this.volver.UseVisualStyleBackColor = true;
            this.volver.Click += new System.EventHandler(this.volver_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.grupo);
            this.panel3.Controls.Add(this.grupo_combo);
            this.panel3.Location = new System.Drawing.Point(44, 25);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(712, 45);
            this.panel3.TabIndex = 13;
            // 
            // PatentesPorGrupo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.patentes_no_otorgadas);
            this.Controls.Add(this.patentes_otorgadas);
            this.Controls.Add(this.volver);
            this.Controls.Add(this.panel3);
            this.Name = "PatentesPorGrupo";
            this.Text = "PatentesPorGrupo";
            this.Load += new System.EventHandler(this.PatentesPorGrupo_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.patentesOtorgadas)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.patentesNoOtorgadas)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label grupo;
        private System.Windows.Forms.ComboBox grupo_combo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView patentesOtorgadas;
        private System.Windows.Forms.Button quitarPatente;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button otorgarPatente;
        private System.Windows.Forms.DataGridView patentesNoOtorgadas;
        private System.Windows.Forms.Label patentes_no_otorgadas;
        private System.Windows.Forms.Label patentes_otorgadas;
        private System.Windows.Forms.Button volver;
        private System.Windows.Forms.Panel panel3;
    }
}
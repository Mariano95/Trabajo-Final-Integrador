
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.patentesOtorgadas = new System.Windows.Forms.DataGridView();
            this.quitarPatente = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.otorgarPatente = new System.Windows.Forms.Button();
            this.patentesNoOtorgadas = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patentesOtorgadas)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patentesNoOtorgadas)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Grupo";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Grupo1",
            "Grupo2",
            "Grupo3",
            "Grupo4",
            "Grupo5",
            "Grupo6",
            "Grupo7",
            "Grupo8",
            "Grupo9",
            "Grupo10",
            "Grupo11",
            "Grupo12",
            "Grupo13",
            "Grupo14"});
            this.comboBox1.Location = new System.Drawing.Point(119, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(232, 23);
            this.comboBox1.TabIndex = 1;
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(414, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "Patentes no otorgadas";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "Patentes otorgadas";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(44, 393);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 33);
            this.button1.TabIndex = 14;
            this.button1.Text = "Volver";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.comboBox1);
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
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
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

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView patentesOtorgadas;
        private System.Windows.Forms.Button quitarPatente;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button otorgarPatente;
        private System.Windows.Forms.DataGridView patentesNoOtorgadas;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel3;
    }
}
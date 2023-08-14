
namespace OyPPTP
{
    partial class GruposUsuarios
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
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.eliminarGrupo = new System.Windows.Forms.Button();
            this.miembrosGrupo = new System.Windows.Forms.DataGridView();
            this.quitarMiembro = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.agregarUsuario = new System.Windows.Forms.Button();
            this.otrosUsuarios = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.crearGrupo = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.miembrosGrupo)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.otrosUsuarios)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 34);
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
            this.comboBox1.Location = new System.Drawing.Point(12, 11);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(183, 23);
            this.comboBox1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.quitarMiembro);
            this.panel1.Controls.Add(this.miembrosGrupo);
            this.panel1.Location = new System.Drawing.Point(12, 142);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(377, 214);
            this.panel1.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 15);
            this.label2.TabIndex = 15;
            this.label2.Text = "Miembros del grupo";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(44, 393);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 33);
            this.button1.TabIndex = 20;
            this.button1.Text = "Volver";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.eliminarGrupo);
            this.panel3.Controls.Add(this.comboBox1);
            this.panel3.Location = new System.Drawing.Point(12, 52);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(377, 45);
            this.panel3.TabIndex = 19;
            // 
            // eliminarGrupo
            // 
            this.eliminarGrupo.BackColor = System.Drawing.Color.Red;
            this.eliminarGrupo.ForeColor = System.Drawing.Color.Black;
            this.eliminarGrupo.Location = new System.Drawing.Point(240, 7);
            this.eliminarGrupo.Name = "eliminarGrupo";
            this.eliminarGrupo.Size = new System.Drawing.Size(118, 28);
            this.eliminarGrupo.TabIndex = 2;
            this.eliminarGrupo.Text = "ELIMINAR GRUPO";
            this.eliminarGrupo.UseVisualStyleBackColor = false;
            // 
            // miembrosGrupo
            // 
            this.miembrosGrupo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.miembrosGrupo.Location = new System.Drawing.Point(12, 23);
            this.miembrosGrupo.Name = "miembrosGrupo";
            this.miembrosGrupo.RowTemplate.Height = 25;
            this.miembrosGrupo.Size = new System.Drawing.Size(356, 150);
            this.miembrosGrupo.TabIndex = 0;
            // 
            // quitarMiembro
            // 
            this.quitarMiembro.Location = new System.Drawing.Point(225, 179);
            this.quitarMiembro.Name = "quitarMiembro";
            this.quitarMiembro.Size = new System.Drawing.Size(143, 23);
            this.quitarMiembro.TabIndex = 1;
            this.quitarMiembro.Text = "Quitar del grupo";
            this.quitarMiembro.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.agregarUsuario);
            this.panel2.Controls.Add(this.otrosUsuarios);
            this.panel2.Location = new System.Drawing.Point(409, 142);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(379, 214);
            this.panel2.TabIndex = 19;
            // 
            // agregarUsuario
            // 
            this.agregarUsuario.BackColor = System.Drawing.Color.Lime;
            this.agregarUsuario.Location = new System.Drawing.Point(225, 179);
            this.agregarUsuario.Name = "agregarUsuario";
            this.agregarUsuario.Size = new System.Drawing.Size(143, 23);
            this.agregarUsuario.TabIndex = 2;
            this.agregarUsuario.Text = "Agregar al grupo";
            this.agregarUsuario.UseVisualStyleBackColor = false;
            // 
            // otrosUsuarios
            // 
            this.otrosUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.otrosUsuarios.Location = new System.Drawing.Point(12, 23);
            this.otrosUsuarios.Name = "otrosUsuarios";
            this.otrosUsuarios.RowTemplate.Height = 25;
            this.otrosUsuarios.Size = new System.Drawing.Size(356, 150);
            this.otrosUsuarios.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(421, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 15);
            this.label3.TabIndex = 21;
            this.label3.Text = "Otros usuarios";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.textBox1);
            this.panel4.Controls.Add(this.crearGrupo);
            this.panel4.Location = new System.Drawing.Point(409, 52);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(377, 45);
            this.panel4.TabIndex = 20;
            // 
            // crearGrupo
            // 
            this.crearGrupo.BackColor = System.Drawing.Color.Lime;
            this.crearGrupo.ForeColor = System.Drawing.Color.Black;
            this.crearGrupo.Location = new System.Drawing.Point(240, 7);
            this.crearGrupo.Name = "crearGrupo";
            this.crearGrupo.Size = new System.Drawing.Size(118, 28);
            this.crearGrupo.TabIndex = 2;
            this.crearGrupo.Text = "CREAR GRUPO";
            this.crearGrupo.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(409, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 15);
            this.label4.TabIndex = 22;
            this.label4.Text = "Crear nuevo grupo";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 11);
            this.textBox1.Name = "textBox1";
            this.textBox1.PlaceholderText = "Nombre del grupo";
            this.textBox1.Size = new System.Drawing.Size(159, 23);
            this.textBox1.TabIndex = 3;
            // 
            // GruposUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel3);
            this.Name = "GruposUsuarios";
            this.Text = "GruposUsuarios";
            this.Load += new System.EventHandler(this.GruposUsuarios_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.miembrosGrupo)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.otrosUsuarios)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button quitarMiembro;
        private System.Windows.Forms.DataGridView miembrosGrupo;
        private System.Windows.Forms.Button eliminarGrupo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button agregarUsuario;
        private System.Windows.Forms.DataGridView otrosUsuarios;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button crearGrupo;
        private System.Windows.Forms.Label label4;
    }
}
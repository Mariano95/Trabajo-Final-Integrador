
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
            this.grupo = new System.Windows.Forms.Label();
            this.grupo_combo = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.quitar_del_grupo = new System.Windows.Forms.Button();
            this.miembros_grilla = new System.Windows.Forms.DataGridView();
            this.miembros_del_grupo = new System.Windows.Forms.Label();
            this.volver = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.eliminar_grupo = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.agregar_al_grupo = new System.Windows.Forms.Button();
            this.otros_usuarios_grilla = new System.Windows.Forms.DataGridView();
            this.otros_usuarios = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.crear_nuevo_grupo_text = new System.Windows.Forms.TextBox();
            this.crear_grupo = new System.Windows.Forms.Button();
            this.crear_nuevo_grupo = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.miembros_grilla)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.otros_usuarios_grilla)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // grupo
            // 
            this.grupo.AutoSize = true;
            this.grupo.Location = new System.Drawing.Point(24, 34);
            this.grupo.Name = "grupo";
            this.grupo.Size = new System.Drawing.Size(40, 15);
            this.grupo.TabIndex = 0;
            this.grupo.Text = "Grupo";
            // 
            // grupo_combo
            // 
            this.grupo_combo.FormattingEnabled = true;
            this.grupo_combo.Location = new System.Drawing.Point(12, 11);
            this.grupo_combo.Name = "grupo_combo";
            this.grupo_combo.Size = new System.Drawing.Size(183, 23);
            this.grupo_combo.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.quitar_del_grupo);
            this.panel1.Controls.Add(this.miembros_grilla);
            this.panel1.Location = new System.Drawing.Point(12, 142);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(377, 214);
            this.panel1.TabIndex = 18;
            // 
            // quitar_del_grupo
            // 
            this.quitar_del_grupo.Location = new System.Drawing.Point(225, 179);
            this.quitar_del_grupo.Name = "quitar_del_grupo";
            this.quitar_del_grupo.Size = new System.Drawing.Size(143, 23);
            this.quitar_del_grupo.TabIndex = 1;
            this.quitar_del_grupo.Text = "Quitar del grupo";
            this.quitar_del_grupo.UseVisualStyleBackColor = true;
            this.quitar_del_grupo.Click += new System.EventHandler(this.quitar_del_grupo_Click);
            // 
            // miembros_grilla
            // 
            this.miembros_grilla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.miembros_grilla.Location = new System.Drawing.Point(12, 23);
            this.miembros_grilla.Name = "miembros_grilla";
            this.miembros_grilla.RowTemplate.Height = 25;
            this.miembros_grilla.Size = new System.Drawing.Size(356, 150);
            this.miembros_grilla.TabIndex = 0;
            this.miembros_grilla.MultiSelect = false;
            // 
            // miembros_del_grupo
            // 
            this.miembros_del_grupo.AutoSize = true;
            this.miembros_del_grupo.Location = new System.Drawing.Point(24, 124);
            this.miembros_del_grupo.Name = "miembros_del_grupo";
            this.miembros_del_grupo.Size = new System.Drawing.Size(115, 15);
            this.miembros_del_grupo.TabIndex = 15;
            this.miembros_del_grupo.Text = "Miembros del grupo";
            // 
            // volver
            // 
            this.volver.Location = new System.Drawing.Point(44, 393);
            this.volver.Name = "volver";
            this.volver.Size = new System.Drawing.Size(100, 33);
            this.volver.TabIndex = 20;
            this.volver.Text = "Volver";
            this.volver.UseVisualStyleBackColor = true;
            this.volver.Click += new System.EventHandler(this.volver_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.eliminar_grupo);
            this.panel3.Controls.Add(this.grupo_combo);
            this.panel3.Location = new System.Drawing.Point(12, 52);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(377, 45);
            this.panel3.TabIndex = 19;
            // 
            // eliminar_grupo
            // 
            this.eliminar_grupo.BackColor = System.Drawing.Color.Red;
            this.eliminar_grupo.ForeColor = System.Drawing.Color.Black;
            this.eliminar_grupo.Location = new System.Drawing.Point(240, 7);
            this.eliminar_grupo.Name = "eliminar_grupo";
            this.eliminar_grupo.Size = new System.Drawing.Size(118, 28);
            this.eliminar_grupo.TabIndex = 2;
            this.eliminar_grupo.Text = "ELIMINAR GRUPO";
            this.eliminar_grupo.UseVisualStyleBackColor = false;
            this.eliminar_grupo.Click += new System.EventHandler(this.eliminar_grupo_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.agregar_al_grupo);
            this.panel2.Controls.Add(this.otros_usuarios_grilla);
            this.panel2.Location = new System.Drawing.Point(409, 142);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(379, 214);
            this.panel2.TabIndex = 19;
            // 
            // agregar_al_grupo
            // 
            this.agregar_al_grupo.BackColor = System.Drawing.Color.Lime;
            this.agregar_al_grupo.Location = new System.Drawing.Point(225, 179);
            this.agregar_al_grupo.Name = "agregar_al_grupo";
            this.agregar_al_grupo.Size = new System.Drawing.Size(143, 23);
            this.agregar_al_grupo.TabIndex = 2;
            this.agregar_al_grupo.Text = "Agregar al grupo";
            this.agregar_al_grupo.UseVisualStyleBackColor = false;
            this.agregar_al_grupo.Click += new System.EventHandler(this.agregar_al_grupo_Click);
            // 
            // otros_usuarios_grilla
            // 
            this.otros_usuarios_grilla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.otros_usuarios_grilla.Location = new System.Drawing.Point(12, 23);
            this.otros_usuarios_grilla.Name = "otros_usuarios_grilla";
            this.otros_usuarios_grilla.RowTemplate.Height = 25;
            this.otros_usuarios_grilla.Size = new System.Drawing.Size(356, 150);
            this.otros_usuarios_grilla.TabIndex = 0;
            this.otros_usuarios_grilla.MultiSelect = false;
            // 
            // otros_usuarios
            // 
            this.otros_usuarios.AutoSize = true;
            this.otros_usuarios.Location = new System.Drawing.Point(421, 124);
            this.otros_usuarios.Name = "otros_usuarios";
            this.otros_usuarios.Size = new System.Drawing.Size(83, 15);
            this.otros_usuarios.TabIndex = 21;
            this.otros_usuarios.Text = "Otros usuarios";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.crear_nuevo_grupo_text);
            this.panel4.Controls.Add(this.crear_grupo);
            this.panel4.Location = new System.Drawing.Point(409, 52);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(377, 45);
            this.panel4.TabIndex = 20;
            // 
            // crear_nuevo_grupo_text
            // 
            this.crear_nuevo_grupo_text.Location = new System.Drawing.Point(12, 11);
            this.crear_nuevo_grupo_text.Name = "crear_nuevo_grupo_text";
            this.crear_nuevo_grupo_text.PlaceholderText = "Nombre del grupo";
            this.crear_nuevo_grupo_text.Size = new System.Drawing.Size(159, 23);
            this.crear_nuevo_grupo_text.TabIndex = 3;
            // 
            // crear_grupo
            // 
            this.crear_grupo.BackColor = System.Drawing.Color.Lime;
            this.crear_grupo.ForeColor = System.Drawing.Color.Black;
            this.crear_grupo.Location = new System.Drawing.Point(240, 7);
            this.crear_grupo.Name = "crear_grupo";
            this.crear_grupo.Size = new System.Drawing.Size(118, 28);
            this.crear_grupo.TabIndex = 2;
            this.crear_grupo.Text = "CREAR GRUPO";
            this.crear_grupo.UseVisualStyleBackColor = false;
            this.crear_grupo.Click += new System.EventHandler(this.crear_grupo_Click);
            // 
            // crear_nuevo_grupo
            // 
            this.crear_nuevo_grupo.AutoSize = true;
            this.crear_nuevo_grupo.Location = new System.Drawing.Point(409, 34);
            this.crear_nuevo_grupo.Name = "crear_nuevo_grupo";
            this.crear_nuevo_grupo.Size = new System.Drawing.Size(106, 15);
            this.crear_nuevo_grupo.TabIndex = 22;
            this.crear_nuevo_grupo.Text = "Crear nuevo grupo";
            // 
            // GruposUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.crear_nuevo_grupo);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.otros_usuarios);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.grupo);
            this.Controls.Add(this.miembros_del_grupo);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.volver);
            this.Controls.Add(this.panel3);
            this.Name = "GruposUsuarios";
            this.Text = "GruposUsuarios";
            this.Load += new System.EventHandler(this.GruposUsuarios_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.miembros_grilla)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.otros_usuarios_grilla)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label grupo;
        private System.Windows.Forms.ComboBox grupo_combo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label miembros_del_grupo;
        private System.Windows.Forms.Button volver;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button quitar_del_grupo;
        private System.Windows.Forms.DataGridView miembros_grilla;
        private System.Windows.Forms.Button eliminar_grupo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button agregar_al_grupo;
        private System.Windows.Forms.DataGridView otros_usuarios_grilla;
        private System.Windows.Forms.Label otros_usuarios;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox crear_nuevo_grupo_text;
        private System.Windows.Forms.Button crear_grupo;
        private System.Windows.Forms.Label crear_nuevo_grupo;
    }
}
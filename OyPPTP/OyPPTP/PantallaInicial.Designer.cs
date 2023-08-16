
namespace OyPPTP
{
    partial class PantallaInicial
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.datosPersonalesOption = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarServicios = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarDatosPersonales = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarPassword = new System.Windows.Forms.ToolStripMenuItem();
            this.ocultarUsuario = new System.Windows.Forms.ToolStripMenuItem();
            this.busquedaTrabajadoresOption = new System.Windows.Forms.ToolStripMenuItem();
            this.buscarTrabajadores = new System.Windows.Forms.ToolStripMenuItem();
            this.citacionesOption = new System.Windows.Forms.ToolStripMenuItem();
            this.citacionesRecibidas = new System.Windows.Forms.ToolStripMenuItem();
            this.citacionesEnviadas = new System.Windows.Forms.ToolStripMenuItem();
            this.administracionOption = new System.Windows.Forms.ToolStripMenuItem();
            this.bitacora = new System.Windows.Forms.ToolStripMenuItem();
            this.backup = new System.Windows.Forms.ToolStripMenuItem();
            this.restaurarSistema = new System.Windows.Forms.ToolStripMenuItem();
            this.desbloquearUsuario = new System.Windows.Forms.ToolStripMenuItem();
            this.patentesPorGrupo = new System.Windows.Forms.ToolStripMenuItem();
            this.patentesPorUsuario = new System.Windows.Forms.ToolStripMenuItem();
            this.gruposUsuarios = new System.Windows.Forms.ToolStripMenuItem();
            this.crearUsuarioAdmin = new System.Windows.Forms.ToolStripMenuItem();
            this.opciones = new System.Windows.Forms.ToolStripMenuItem();
            this.cambiarIdioma = new System.Windows.Forms.ToolStripMenuItem();
            this.ingles = new System.Windows.Forms.ToolStripMenuItem();
            this.portugues = new System.Windows.Forms.ToolStripMenuItem();
            this.sesion = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarSesion = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.pantalla_inicial_label_1 = new System.Windows.Forms.Label();
            this.pantalla_inicial_label_2 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.datosPersonalesOption,
            this.busquedaTrabajadoresOption,
            this.citacionesOption,
            this.administracionOption,
            this.opciones,
            this.sesion});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // datosPersonalesOption
            // 
            this.datosPersonalesOption.AccessibleRole = System.Windows.Forms.AccessibleRole.List;
            this.datosPersonalesOption.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modificarServicios,
            this.modificarDatosPersonales,
            this.modificarPassword,
            this.ocultarUsuario});
            this.datosPersonalesOption.Name = "datosPersonalesOption";
            this.datosPersonalesOption.Size = new System.Drawing.Size(108, 20);
            this.datosPersonalesOption.Text = "Datos personales";
            // 
            // modificarServicios
            // 
            this.modificarServicios.Name = "modificarServicios";
            this.modificarServicios.Size = new System.Drawing.Size(216, 22);
            this.modificarServicios.Text = "Modificar servicios";
            this.modificarServicios.Click += new System.EventHandler(this.menuStripOptionClicked);
            // 
            // modificarDatosPersonales
            // 
            this.modificarDatosPersonales.Name = "modificarDatosPersonales";
            this.modificarDatosPersonales.Size = new System.Drawing.Size(216, 22);
            this.modificarDatosPersonales.Text = "Modificar datos personales";
            this.modificarDatosPersonales.Click += new System.EventHandler(this.menuStripOptionClicked);
            // 
            // modificarPassword
            // 
            this.modificarPassword.Name = "modificarPassword";
            this.modificarPassword.Size = new System.Drawing.Size(216, 22);
            this.modificarPassword.Text = "Modificar contraseña";
            this.modificarPassword.Click += new System.EventHandler(this.menuStripOptionClicked);
            // 
            // ocultarUsuario
            // 
            this.ocultarUsuario.Name = "ocultarUsuario";
            this.ocultarUsuario.Size = new System.Drawing.Size(216, 22);
            this.ocultarUsuario.Text = "Ocultar usuario";
            this.ocultarUsuario.Click += new System.EventHandler(this.menuStripOptionClicked);
            // 
            // busquedaTrabajadoresOption
            // 
            this.busquedaTrabajadoresOption.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buscarTrabajadores});
            this.busquedaTrabajadoresOption.Name = "busquedaTrabajadoresOption";
            this.busquedaTrabajadoresOption.Size = new System.Drawing.Size(155, 20);
            this.busquedaTrabajadoresOption.Text = "Búsqueda de trabajadores";
            this.busquedaTrabajadoresOption.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // buscarTrabajadores
            // 
            this.buscarTrabajadores.Name = "buscarTrabajadores";
            this.buscarTrabajadores.Size = new System.Drawing.Size(177, 22);
            this.buscarTrabajadores.Text = "Buscar trabajadores";
            this.buscarTrabajadores.Click += new System.EventHandler(this.menuStripOptionClicked);
            // 
            // citacionesOption
            // 
            this.citacionesOption.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.citacionesRecibidas,
            this.citacionesEnviadas});
            this.citacionesOption.Name = "citacionesOption";
            this.citacionesOption.Size = new System.Drawing.Size(74, 20);
            this.citacionesOption.Text = "Citaciones";
            // 
            // citacionesRecibidas
            // 
            this.citacionesRecibidas.Name = "citacionesRecibidas";
            this.citacionesRecibidas.Size = new System.Drawing.Size(179, 22);
            this.citacionesRecibidas.Text = "Citaciones recibidas";
            this.citacionesRecibidas.Click += new System.EventHandler(this.menuStripOptionClicked);
            // 
            // citacionesEnviadas
            // 
            this.citacionesEnviadas.Name = "citacionesEnviadas";
            this.citacionesEnviadas.Size = new System.Drawing.Size(179, 22);
            this.citacionesEnviadas.Text = "Citaciones enviadas";
            this.citacionesEnviadas.Click += new System.EventHandler(this.menuStripOptionClicked);
            // 
            // administracionOption
            // 
            this.administracionOption.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bitacora,
            this.backup,
            this.restaurarSistema,
            this.desbloquearUsuario,
            this.patentesPorGrupo,
            this.patentesPorUsuario,
            this.gruposUsuarios,
            this.crearUsuarioAdmin});
            this.administracionOption.Name = "administracionOption";
            this.administracionOption.Size = new System.Drawing.Size(100, 20);
            this.administracionOption.Text = "Administración";
            // 
            // bitacora
            // 
            this.bitacora.Name = "bitacora";
            this.bitacora.Size = new System.Drawing.Size(221, 22);
            this.bitacora.Text = "Bitácora";
            this.bitacora.Click += new System.EventHandler(this.menuStripOptionClicked);
            // 
            // backup
            // 
            this.backup.Name = "backup";
            this.backup.Size = new System.Drawing.Size(221, 22);
            this.backup.Text = "Crear backup del sistema";
            this.backup.Click += new System.EventHandler(this.menuStripOptionClicked);
            // 
            // restaurarSistema
            // 
            this.restaurarSistema.Name = "restaurarSistema";
            this.restaurarSistema.Size = new System.Drawing.Size(221, 22);
            this.restaurarSistema.Text = "Restaurar sistema";
            this.restaurarSistema.Click += new System.EventHandler(this.menuStripOptionClicked);
            // 
            // desbloquearUsuario
            // 
            this.desbloquearUsuario.Name = "desbloquearUsuario";
            this.desbloquearUsuario.Size = new System.Drawing.Size(221, 22);
            this.desbloquearUsuario.Text = "Desbloquear usuario";
            this.desbloquearUsuario.Click += new System.EventHandler(this.menuStripOptionClicked);
            // 
            // patentesPorGrupo
            // 
            this.patentesPorGrupo.Name = "patentesPorGrupo";
            this.patentesPorGrupo.Size = new System.Drawing.Size(221, 22);
            this.patentesPorGrupo.Text = "Patentes por grupo";
            this.patentesPorGrupo.Click += new System.EventHandler(this.menuStripOptionClicked);
            // 
            // patentesPorUsuario
            // 
            this.patentesPorUsuario.Name = "patentesPorUsuario";
            this.patentesPorUsuario.Size = new System.Drawing.Size(221, 22);
            this.patentesPorUsuario.Text = "Patentes por usuario";
            this.patentesPorUsuario.Click += new System.EventHandler(this.menuStripOptionClicked);
            // 
            // gruposUsuarios
            // 
            this.gruposUsuarios.Name = "gruposUsuarios";
            this.gruposUsuarios.Size = new System.Drawing.Size(221, 22);
            this.gruposUsuarios.Text = "Grupos de usuarios";
            this.gruposUsuarios.Click += new System.EventHandler(this.menuStripOptionClicked);
            // 
            // crearUsuarioAdmin
            // 
            this.crearUsuarioAdmin.Name = "crearUsuarioAdmin";
            this.crearUsuarioAdmin.Size = new System.Drawing.Size(221, 22);
            this.crearUsuarioAdmin.Text = "Crear usuario administrador";
            this.crearUsuarioAdmin.Click += new System.EventHandler(this.menuStripOptionClicked);
            // 
            // opciones
            // 
            this.opciones.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cambiarIdioma});
            this.opciones.Name = "opciones";
            this.opciones.Size = new System.Drawing.Size(69, 20);
            this.opciones.Text = "Opciones";
            // 
            // cambiarIdioma
            // 
            this.cambiarIdioma.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ingles,
            this.portugues});
            this.cambiarIdioma.Name = "cambiarIdioma";
            this.cambiarIdioma.Size = new System.Drawing.Size(159, 22);
            this.cambiarIdioma.Text = "Cambiar idioma";
            // 
            // ingles
            // 
            this.ingles.Name = "ingles";
            this.ingles.Size = new System.Drawing.Size(128, 22);
            this.ingles.Text = "Inglés";
            this.ingles.Click += new System.EventHandler(this.menuStripOptionClicked);
            // 
            // portugues
            // 
            this.portugues.Name = "portugues";
            this.portugues.Size = new System.Drawing.Size(128, 22);
            this.portugues.Text = "Portugués";
            this.portugues.Click += new System.EventHandler(this.menuStripOptionClicked);
            // 
            // sesion
            // 
            this.sesion.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cerrarSesion});
            this.sesion.Name = "sesion";
            this.sesion.Size = new System.Drawing.Size(53, 20);
            this.sesion.Text = "Sesión";
            // 
            // cerrarSesion
            // 
            this.cerrarSesion.Name = "cerrarSesion";
            this.cerrarSesion.Size = new System.Drawing.Size(142, 22);
            this.cerrarSesion.Text = "Cerrar sesión";
            this.cerrarSesion.Click += new System.EventHandler(this.menuStripOptionClicked);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(32, 19);
            this.toolStripMenuItem1.Text = "toolStripMenuItem1";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem2.Text = "toolStripMenuItem2";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(32, 19);
            this.toolStripMenuItem3.Text = "toolStripMenuItem3";
            // 
            // pantalla_inicial_label_1
            // 
            this.pantalla_inicial_label_1.AutoSize = true;
            this.pantalla_inicial_label_1.Location = new System.Drawing.Point(329, 169);
            this.pantalla_inicial_label_1.Name = "pantalla_inicial_label_1";
            this.pantalla_inicial_label_1.Size = new System.Drawing.Size(146, 15);
            this.pantalla_inicial_label_1.TabIndex = 1;
            this.pantalla_inicial_label_1.Text = "Hola, APELLIDO NOMBRE!";
            // 
            // pantalla_inicial_label_2
            // 
            this.pantalla_inicial_label_2.AutoSize = true;
            this.pantalla_inicial_label_2.Location = new System.Drawing.Point(142, 199);
            this.pantalla_inicial_label_2.Name = "pantalla_inicial_label_2";
            this.pantalla_inicial_label_2.Size = new System.Drawing.Size(530, 15);
            this.pantalla_inicial_label_2.TabIndex = 2;
            this.pantalla_inicial_label_2.Text = "Bienvenido al portal de servicios. Por favor, seleccioná una de las opciones de a" +
    "rriba para continuar.";
            // 
            // PantallaInicial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pantalla_inicial_label_2);
            this.Controls.Add(this.pantalla_inicial_label_1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "PantallaInicial";
            this.Text = "PantallaInicial";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PantallaInicial_FormClosed);
            this.Load += new System.EventHandler(this.PantallaInicial_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem datosPersonalesOption;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem modificarDatosPersonales;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem modificarServicios;
        private System.Windows.Forms.ToolStripMenuItem modificarPassword;
        private System.Windows.Forms.Label pantalla_inicial_label_1;
        private System.Windows.Forms.Label pantalla_inicial_label_2;
        private System.Windows.Forms.ToolStripMenuItem busquedaTrabajadoresOption;
        private System.Windows.Forms.ToolStripMenuItem buscarTrabajadores;
        private System.Windows.Forms.ToolStripMenuItem citacionesOption;
        private System.Windows.Forms.ToolStripMenuItem citacionesRecibidas;
        private System.Windows.Forms.ToolStripMenuItem citacionesEnviadas;
        private System.Windows.Forms.ToolStripMenuItem ocultarUsuario;
        private System.Windows.Forms.ToolStripMenuItem administracionOption;
        private System.Windows.Forms.ToolStripMenuItem bitacora;
        private System.Windows.Forms.ToolStripMenuItem backup;
        private System.Windows.Forms.ToolStripMenuItem restaurarSistema;
        private System.Windows.Forms.ToolStripMenuItem opciones;
        private System.Windows.Forms.ToolStripMenuItem cambiarIdioma;
        private System.Windows.Forms.ToolStripMenuItem ingles;
        private System.Windows.Forms.ToolStripMenuItem portugues;
        private System.Windows.Forms.ToolStripMenuItem sesion;
        private System.Windows.Forms.ToolStripMenuItem cerrarSesion;
        private System.Windows.Forms.ToolStripMenuItem desbloquearUsuario;
        private System.Windows.Forms.ToolStripMenuItem patentesPorGrupo;
        private System.Windows.Forms.ToolStripMenuItem patentesPorUsuario;
        private System.Windows.Forms.ToolStripMenuItem gruposUsuarios;
        private System.Windows.Forms.ToolStripMenuItem crearUsuarioAdmin;
    }
}
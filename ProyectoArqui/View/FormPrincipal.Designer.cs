namespace ProyectoArqui.View
{
    partial class FormPrincipal
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.FileChooser = new System.Windows.Forms.OpenFileDialog();
            this.BotonNuevaSimulacion = new System.Windows.Forms.Button();
            this.TextBoxCantidadProgramas = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BotonAgregarArchivo = new System.Windows.Forms.Button();
            this.panelIniciar = new System.Windows.Forms.Panel();
            this.BotonIniciarSimulacion = new System.Windows.Forms.Button();
            this.GridPaths = new System.Windows.Forms.DataGridView();
            this.Path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelTitulo = new System.Windows.Forms.Label();
            this.panelProcesadores = new System.Windows.Forms.Panel();
            this.labelProcesador2Corriendo = new System.Windows.Forms.Label();
            this.labelProcesador1Corriendo = new System.Windows.Forms.Label();
            this.labelProcesador0Corriendo = new System.Windows.Forms.Label();
            this.labelProcesador2 = new System.Windows.Forms.Label();
            this.labelProcesador1 = new System.Windows.Forms.Label();
            this.gridProcesador2 = new System.Windows.Forms.DataGridView();
            this.Propiedad2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Informacion2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Informacion22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Informacion23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Informacion24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridProcesador1 = new System.Windows.Forms.DataGridView();
            this.Propiedad1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Informacion1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Informacion12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Informacion13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Informacion14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridProcesador0 = new System.Windows.Forms.DataGridView();
            this.Propiedad0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Informacion0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Informacion02 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Informacion03 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Informacion04 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelProcesador0 = new System.Windows.Forms.Label();
            this.panelMemoria = new System.Windows.Forms.Panel();
            this.gridMemoriaCompartida = new System.Windows.Forms.DataGridView();
            this.Direcciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Registros1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Registros2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Registros3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Registros4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Registros5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Registros6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Registros7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Registros8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelMemoriaCompartida = new System.Windows.Forms.Label();
            this.panelIniciar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridPaths)).BeginInit();
            this.panelProcesadores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProcesador2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridProcesador1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridProcesador0)).BeginInit();
            this.panelMemoria.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMemoriaCompartida)).BeginInit();
            this.SuspendLayout();
            // 
            // FileChooser
            // 
            this.FileChooser.FileName = "openFileDialog";
            this.FileChooser.FileOk += new System.ComponentModel.CancelEventHandler(this.FileChooser_FileOk);
            // 
            // BotonNuevaSimulacion
            // 
            this.BotonNuevaSimulacion.Location = new System.Drawing.Point(0, 35);
            this.BotonNuevaSimulacion.Name = "BotonNuevaSimulacion";
            this.BotonNuevaSimulacion.Size = new System.Drawing.Size(100, 30);
            this.BotonNuevaSimulacion.TabIndex = 0;
            this.BotonNuevaSimulacion.Text = "Nueva simulación";
            this.BotonNuevaSimulacion.UseVisualStyleBackColor = true;
            this.BotonNuevaSimulacion.Click += new System.EventHandler(this.BotonNuevaSimulacion_Click);
            // 
            // TextBoxCantidadProgramas
            // 
            this.TextBoxCantidadProgramas.Enabled = false;
            this.TextBoxCantidadProgramas.Location = new System.Drawing.Point(214, 24);
            this.TextBoxCantidadProgramas.Name = "TextBoxCantidadProgramas";
            this.TextBoxCantidadProgramas.Size = new System.Drawing.Size(75, 20);
            this.TextBoxCantidadProgramas.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(117, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Cantidad de hilos:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(116, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Escoger archivos:";
            // 
            // BotonAgregarArchivo
            // 
            this.BotonAgregarArchivo.Enabled = false;
            this.BotonAgregarArchivo.Location = new System.Drawing.Point(214, 55);
            this.BotonAgregarArchivo.Name = "BotonAgregarArchivo";
            this.BotonAgregarArchivo.Size = new System.Drawing.Size(75, 23);
            this.BotonAgregarArchivo.TabIndex = 4;
            this.BotonAgregarArchivo.Text = "Agregar";
            this.BotonAgregarArchivo.UseVisualStyleBackColor = true;
            this.BotonAgregarArchivo.Click += new System.EventHandler(this.BotonAgregarArchivo_Click);
            // 
            // panelIniciar
            // 
            this.panelIniciar.Controls.Add(this.BotonIniciarSimulacion);
            this.panelIniciar.Controls.Add(this.BotonNuevaSimulacion);
            this.panelIniciar.Controls.Add(this.GridPaths);
            this.panelIniciar.Controls.Add(this.label1);
            this.panelIniciar.Controls.Add(this.BotonAgregarArchivo);
            this.panelIniciar.Controls.Add(this.TextBoxCantidadProgramas);
            this.panelIniciar.Controls.Add(this.label2);
            this.panelIniciar.Location = new System.Drawing.Point(18, 60);
            this.panelIniciar.Name = "panelIniciar";
            this.panelIniciar.Size = new System.Drawing.Size(1110, 100);
            this.panelIniciar.TabIndex = 5;
            // 
            // BotonIniciarSimulacion
            // 
            this.BotonIniciarSimulacion.Enabled = false;
            this.BotonIniciarSimulacion.Location = new System.Drawing.Point(1010, 35);
            this.BotonIniciarSimulacion.Name = "BotonIniciarSimulacion";
            this.BotonIniciarSimulacion.Size = new System.Drawing.Size(100, 30);
            this.BotonIniciarSimulacion.TabIndex = 6;
            this.BotonIniciarSimulacion.Text = "Iniciar Simulación";
            this.BotonIniciarSimulacion.UseVisualStyleBackColor = true;
            this.BotonIniciarSimulacion.Click += new System.EventHandler(this.BotonIniciarSimulacion_Click);
            // 
            // GridPaths
            // 
            this.GridPaths.AllowUserToAddRows = false;
            this.GridPaths.AllowUserToDeleteRows = false;
            this.GridPaths.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridPaths.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Path});
            this.GridPaths.Enabled = false;
            this.GridPaths.Location = new System.Drawing.Point(305, 0);
            this.GridPaths.Name = "GridPaths";
            this.GridPaths.ReadOnly = true;
            this.GridPaths.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.GridPaths.Size = new System.Drawing.Size(690, 100);
            this.GridPaths.TabIndex = 5;
            // 
            // Path
            // 
            this.Path.HeaderText = "Path del archivo";
            this.Path.Name = "Path";
            this.Path.ReadOnly = true;
            this.Path.Width = 1000;
            // 
            // labelTitulo
            // 
            this.labelTitulo.AutoSize = true;
            this.labelTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitulo.Location = new System.Drawing.Point(13, 18);
            this.labelTitulo.Name = "labelTitulo";
            this.labelTitulo.Size = new System.Drawing.Size(196, 29);
            this.labelTitulo.TabIndex = 6;
            this.labelTitulo.Text = "Simulador MIPS";
            // 
            // panelProcesadores
            // 
            this.panelProcesadores.Controls.Add(this.labelProcesador2Corriendo);
            this.panelProcesadores.Controls.Add(this.labelProcesador1Corriendo);
            this.panelProcesadores.Controls.Add(this.labelProcesador0Corriendo);
            this.panelProcesadores.Controls.Add(this.labelProcesador2);
            this.panelProcesadores.Controls.Add(this.labelProcesador1);
            this.panelProcesadores.Controls.Add(this.gridProcesador2);
            this.panelProcesadores.Controls.Add(this.gridProcesador1);
            this.panelProcesadores.Controls.Add(this.gridProcesador0);
            this.panelProcesadores.Controls.Add(this.labelProcesador0);
            this.panelProcesadores.Location = new System.Drawing.Point(18, 172);
            this.panelProcesadores.Name = "panelProcesadores";
            this.panelProcesadores.Size = new System.Drawing.Size(1110, 240);
            this.panelProcesadores.TabIndex = 7;
            this.panelProcesadores.Visible = false;
            // 
            // labelProcesador2Corriendo
            // 
            this.labelProcesador2Corriendo.AutoSize = true;
            this.labelProcesador2Corriendo.Location = new System.Drawing.Point(809, 13);
            this.labelProcesador2Corriendo.Name = "labelProcesador2Corriendo";
            this.labelProcesador2Corriendo.Size = new System.Drawing.Size(0, 13);
            this.labelProcesador2Corriendo.TabIndex = 8;
            // 
            // labelProcesador1Corriendo
            // 
            this.labelProcesador1Corriendo.AutoSize = true;
            this.labelProcesador1Corriendo.Location = new System.Drawing.Point(438, 13);
            this.labelProcesador1Corriendo.Name = "labelProcesador1Corriendo";
            this.labelProcesador1Corriendo.Size = new System.Drawing.Size(0, 13);
            this.labelProcesador1Corriendo.TabIndex = 7;
            // 
            // labelProcesador0Corriendo
            // 
            this.labelProcesador0Corriendo.AutoSize = true;
            this.labelProcesador0Corriendo.Location = new System.Drawing.Point(67, 12);
            this.labelProcesador0Corriendo.Name = "labelProcesador0Corriendo";
            this.labelProcesador0Corriendo.Size = new System.Drawing.Size(0, 13);
            this.labelProcesador0Corriendo.TabIndex = 6;
            // 
            // labelProcesador2
            // 
            this.labelProcesador2.AutoSize = true;
            this.labelProcesador2.Location = new System.Drawing.Point(739, 13);
            this.labelProcesador2.Name = "labelProcesador2";
            this.labelProcesador2.Size = new System.Drawing.Size(70, 13);
            this.labelProcesador2.TabIndex = 5;
            this.labelProcesador2.Text = "Procesador 2";
            // 
            // labelProcesador1
            // 
            this.labelProcesador1.AutoSize = true;
            this.labelProcesador1.Location = new System.Drawing.Point(368, 13);
            this.labelProcesador1.Name = "labelProcesador1";
            this.labelProcesador1.Size = new System.Drawing.Size(70, 13);
            this.labelProcesador1.TabIndex = 4;
            this.labelProcesador1.Text = "Procesador 1";
            // 
            // gridProcesador2
            // 
            this.gridProcesador2.AllowUserToAddRows = false;
            this.gridProcesador2.AllowUserToDeleteRows = false;
            this.gridProcesador2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridProcesador2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Propiedad2,
            this.Informacion2,
            this.Informacion22,
            this.Informacion23,
            this.Informacion24});
            this.gridProcesador2.Location = new System.Drawing.Point(742, 28);
            this.gridProcesador2.Name = "gridProcesador2";
            this.gridProcesador2.ReadOnly = true;
            this.gridProcesador2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridProcesador2.Size = new System.Drawing.Size(365, 212);
            this.gridProcesador2.TabIndex = 3;
            // 
            // Propiedad2
            // 
            this.Propiedad2.HeaderText = "";
            this.Propiedad2.Name = "Propiedad2";
            this.Propiedad2.ReadOnly = true;
            // 
            // Informacion2
            // 
            this.Informacion2.HeaderText = "";
            this.Informacion2.Name = "Informacion2";
            this.Informacion2.ReadOnly = true;
            this.Informacion2.Width = 55;
            // 
            // Informacion22
            // 
            this.Informacion22.HeaderText = "";
            this.Informacion22.Name = "Informacion22";
            this.Informacion22.ReadOnly = true;
            this.Informacion22.Width = 55;
            // 
            // Informacion23
            // 
            this.Informacion23.HeaderText = "";
            this.Informacion23.Name = "Informacion23";
            this.Informacion23.ReadOnly = true;
            this.Informacion23.Width = 55;
            // 
            // Informacion24
            // 
            this.Informacion24.HeaderText = "";
            this.Informacion24.Name = "Informacion24";
            this.Informacion24.ReadOnly = true;
            this.Informacion24.Width = 55;
            // 
            // gridProcesador1
            // 
            this.gridProcesador1.AllowUserToAddRows = false;
            this.gridProcesador1.AllowUserToDeleteRows = false;
            this.gridProcesador1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridProcesador1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Propiedad1,
            this.Informacion1,
            this.Informacion12,
            this.Informacion13,
            this.Informacion14});
            this.gridProcesador1.Location = new System.Drawing.Point(371, 28);
            this.gridProcesador1.Name = "gridProcesador1";
            this.gridProcesador1.ReadOnly = true;
            this.gridProcesador1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridProcesador1.Size = new System.Drawing.Size(365, 212);
            this.gridProcesador1.TabIndex = 2;
            // 
            // Propiedad1
            // 
            this.Propiedad1.HeaderText = "";
            this.Propiedad1.Name = "Propiedad1";
            this.Propiedad1.ReadOnly = true;
            // 
            // Informacion1
            // 
            this.Informacion1.HeaderText = "";
            this.Informacion1.Name = "Informacion1";
            this.Informacion1.ReadOnly = true;
            this.Informacion1.Width = 55;
            // 
            // Informacion12
            // 
            this.Informacion12.HeaderText = "";
            this.Informacion12.Name = "Informacion12";
            this.Informacion12.ReadOnly = true;
            this.Informacion12.Width = 55;
            // 
            // Informacion13
            // 
            this.Informacion13.HeaderText = "";
            this.Informacion13.Name = "Informacion13";
            this.Informacion13.ReadOnly = true;
            this.Informacion13.Width = 55;
            // 
            // Informacion14
            // 
            this.Informacion14.HeaderText = "";
            this.Informacion14.Name = "Informacion14";
            this.Informacion14.ReadOnly = true;
            this.Informacion14.Width = 55;
            // 
            // gridProcesador0
            // 
            this.gridProcesador0.AllowUserToAddRows = false;
            this.gridProcesador0.AllowUserToDeleteRows = false;
            this.gridProcesador0.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridProcesador0.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Propiedad0,
            this.Informacion0,
            this.Informacion02,
            this.Informacion03,
            this.Informacion04});
            this.gridProcesador0.Location = new System.Drawing.Point(0, 28);
            this.gridProcesador0.Name = "gridProcesador0";
            this.gridProcesador0.ReadOnly = true;
            this.gridProcesador0.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridProcesador0.Size = new System.Drawing.Size(365, 212);
            this.gridProcesador0.TabIndex = 1;
            // 
            // Propiedad0
            // 
            this.Propiedad0.HeaderText = "";
            this.Propiedad0.Name = "Propiedad0";
            this.Propiedad0.ReadOnly = true;
            // 
            // Informacion0
            // 
            this.Informacion0.HeaderText = "";
            this.Informacion0.Name = "Informacion0";
            this.Informacion0.ReadOnly = true;
            this.Informacion0.Width = 55;
            // 
            // Informacion02
            // 
            this.Informacion02.HeaderText = "";
            this.Informacion02.Name = "Informacion02";
            this.Informacion02.ReadOnly = true;
            this.Informacion02.Width = 55;
            // 
            // Informacion03
            // 
            this.Informacion03.HeaderText = "";
            this.Informacion03.Name = "Informacion03";
            this.Informacion03.ReadOnly = true;
            this.Informacion03.Width = 55;
            // 
            // Informacion04
            // 
            this.Informacion04.HeaderText = "";
            this.Informacion04.Name = "Informacion04";
            this.Informacion04.ReadOnly = true;
            this.Informacion04.Width = 55;
            // 
            // labelProcesador0
            // 
            this.labelProcesador0.AutoSize = true;
            this.labelProcesador0.Location = new System.Drawing.Point(-3, 12);
            this.labelProcesador0.Name = "labelProcesador0";
            this.labelProcesador0.Size = new System.Drawing.Size(70, 13);
            this.labelProcesador0.TabIndex = 0;
            this.labelProcesador0.Text = "Procesador 0";
            // 
            // panelMemoria
            // 
            this.panelMemoria.Controls.Add(this.gridMemoriaCompartida);
            this.panelMemoria.Controls.Add(this.labelMemoriaCompartida);
            this.panelMemoria.Location = new System.Drawing.Point(18, 421);
            this.panelMemoria.Name = "panelMemoria";
            this.panelMemoria.Size = new System.Drawing.Size(1110, 230);
            this.panelMemoria.TabIndex = 8;
            this.panelMemoria.Visible = false;
            // 
            // gridMemoriaCompartida
            // 
            this.gridMemoriaCompartida.AllowUserToAddRows = false;
            this.gridMemoriaCompartida.AllowUserToDeleteRows = false;
            this.gridMemoriaCompartida.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMemoriaCompartida.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Direcciones,
            this.Registros1,
            this.Registros2,
            this.Registros3,
            this.Registros4,
            this.Registros5,
            this.Registros6,
            this.Registros7,
            this.Registros8});
            this.gridMemoriaCompartida.Location = new System.Drawing.Point(0, 16);
            this.gridMemoriaCompartida.Name = "gridMemoriaCompartida";
            this.gridMemoriaCompartida.ReadOnly = true;
            this.gridMemoriaCompartida.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridMemoriaCompartida.Size = new System.Drawing.Size(1107, 213);
            this.gridMemoriaCompartida.TabIndex = 6;
            // 
            // Direcciones
            // 
            this.Direcciones.HeaderText = "Bloques";
            this.Direcciones.Name = "Direcciones";
            this.Direcciones.ReadOnly = true;
            // 
            // Registros1
            // 
            this.Registros1.HeaderText = "";
            this.Registros1.Name = "Registros1";
            this.Registros1.ReadOnly = true;
            this.Registros1.Width = 120;
            // 
            // Registros2
            // 
            this.Registros2.HeaderText = "";
            this.Registros2.Name = "Registros2";
            this.Registros2.ReadOnly = true;
            this.Registros2.Width = 120;
            // 
            // Registros3
            // 
            this.Registros3.HeaderText = "";
            this.Registros3.Name = "Registros3";
            this.Registros3.ReadOnly = true;
            this.Registros3.Width = 120;
            // 
            // Registros4
            // 
            this.Registros4.HeaderText = "";
            this.Registros4.Name = "Registros4";
            this.Registros4.ReadOnly = true;
            this.Registros4.Width = 120;
            // 
            // Registros5
            // 
            this.Registros5.HeaderText = "";
            this.Registros5.Name = "Registros5";
            this.Registros5.ReadOnly = true;
            this.Registros5.Width = 120;
            // 
            // Registros6
            // 
            this.Registros6.HeaderText = "";
            this.Registros6.Name = "Registros6";
            this.Registros6.ReadOnly = true;
            this.Registros6.Width = 120;
            // 
            // Registros7
            // 
            this.Registros7.HeaderText = "";
            this.Registros7.Name = "Registros7";
            this.Registros7.ReadOnly = true;
            this.Registros7.Width = 120;
            // 
            // Registros8
            // 
            this.Registros8.HeaderText = "";
            this.Registros8.Name = "Registros8";
            this.Registros8.ReadOnly = true;
            this.Registros8.Width = 120;
            // 
            // labelMemoriaCompartida
            // 
            this.labelMemoriaCompartida.AutoSize = true;
            this.labelMemoriaCompartida.Location = new System.Drawing.Point(3, 0);
            this.labelMemoriaCompartida.Name = "labelMemoriaCompartida";
            this.labelMemoriaCompartida.Size = new System.Drawing.Size(151, 13);
            this.labelMemoriaCompartida.TabIndex = 6;
            this.labelMemoriaCompartida.Text = "Memoria compartida resultante";
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1154, 662);
            this.Controls.Add(this.panelMemoria);
            this.Controls.Add(this.panelProcesadores);
            this.Controls.Add(this.labelTitulo);
            this.Controls.Add(this.panelIniciar);
            this.Name = "FormPrincipal";
            this.Text = "Proyecto Arquitectura de computadoras - Simulador de procesadores MIPS";
            this.panelIniciar.ResumeLayout(false);
            this.panelIniciar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridPaths)).EndInit();
            this.panelProcesadores.ResumeLayout(false);
            this.panelProcesadores.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProcesador2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridProcesador1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridProcesador0)).EndInit();
            this.panelMemoria.ResumeLayout(false);
            this.panelMemoria.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMemoriaCompartida)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog FileChooser;
        private System.Windows.Forms.Button BotonNuevaSimulacion;
        private System.Windows.Forms.TextBox TextBoxCantidadProgramas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BotonAgregarArchivo;
        private System.Windows.Forms.Panel panelIniciar;
        private System.Windows.Forms.DataGridView GridPaths;
        private System.Windows.Forms.DataGridViewTextBoxColumn Path;
        private System.Windows.Forms.Button BotonIniciarSimulacion;
        private System.Windows.Forms.Label labelTitulo;
        private System.Windows.Forms.Panel panelProcesadores;
        private System.Windows.Forms.Panel panelMemoria;
        private System.Windows.Forms.Label labelProcesador0;
        private System.Windows.Forms.Label labelProcesador2;
        private System.Windows.Forms.Label labelProcesador1;
        private System.Windows.Forms.DataGridView gridProcesador2;
        private System.Windows.Forms.DataGridView gridProcesador1;
        private System.Windows.Forms.DataGridView gridProcesador0;
        private System.Windows.Forms.DataGridView gridMemoriaCompartida;
        private System.Windows.Forms.Label labelMemoriaCompartida;
        private System.Windows.Forms.Label labelProcesador0Corriendo;
        private System.Windows.Forms.Label labelProcesador2Corriendo;
        private System.Windows.Forms.Label labelProcesador1Corriendo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Propiedad2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Informacion2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Informacion22;
        private System.Windows.Forms.DataGridViewTextBoxColumn Informacion23;
        private System.Windows.Forms.DataGridViewTextBoxColumn Informacion24;
        private System.Windows.Forms.DataGridViewTextBoxColumn Propiedad1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Informacion1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Informacion12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Informacion13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Informacion14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Propiedad0;
        private System.Windows.Forms.DataGridViewTextBoxColumn Informacion0;
        private System.Windows.Forms.DataGridViewTextBoxColumn Informacion02;
        private System.Windows.Forms.DataGridViewTextBoxColumn Informacion03;
        private System.Windows.Forms.DataGridViewTextBoxColumn Informacion04;
        private System.Windows.Forms.DataGridViewTextBoxColumn Direcciones;
        private System.Windows.Forms.DataGridViewTextBoxColumn Registros1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Registros2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Registros3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Registros4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Registros5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Registros6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Registros7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Registros8;
    }
}


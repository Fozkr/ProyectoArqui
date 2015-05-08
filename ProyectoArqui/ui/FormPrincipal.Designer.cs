namespace ProyectoArqui
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
            this.labelProcesador0Corriendo = new System.Windows.Forms.Label();
            this.labelProcesador2 = new System.Windows.Forms.Label();
            this.labelProcesador1 = new System.Windows.Forms.Label();
            this.gridProcesador3 = new System.Windows.Forms.DataGridView();
            this.gridProcesador2 = new System.Windows.Forms.DataGridView();
            this.gridProcesador1 = new System.Windows.Forms.DataGridView();
            this.labelProcesador0 = new System.Windows.Forms.Label();
            this.panelMemoria = new System.Windows.Forms.Panel();
            this.gridMemoriaCompartida = new System.Windows.Forms.DataGridView();
            this.labelMemoriaCompartida = new System.Windows.Forms.Label();
            this.labelProcesador1Corriendo = new System.Windows.Forms.Label();
            this.labelProcesador2Corriendo = new System.Windows.Forms.Label();
            this.Propiedad1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Informacion1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Propiedad0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Informacion0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Propiedad2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Informacion2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Registros = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelIniciar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridPaths)).BeginInit();
            this.panelProcesadores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProcesador3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridProcesador2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridProcesador1)).BeginInit();
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
            this.panelProcesadores.Controls.Add(this.gridProcesador3);
            this.panelProcesadores.Controls.Add(this.gridProcesador2);
            this.panelProcesadores.Controls.Add(this.gridProcesador1);
            this.panelProcesadores.Controls.Add(this.labelProcesador0);
            this.panelProcesadores.Location = new System.Drawing.Point(18, 177);
            this.panelProcesadores.Name = "panelProcesadores";
            this.panelProcesadores.Size = new System.Drawing.Size(1110, 240);
            this.panelProcesadores.TabIndex = 7;
            this.panelProcesadores.Visible = false;
            // 
            // labelProcesador0Corriendo
            // 
            this.labelProcesador0Corriendo.AutoSize = true;
            this.labelProcesador0Corriendo.Location = new System.Drawing.Point(3, 13);
            this.labelProcesador0Corriendo.Name = "labelProcesador0Corriendo";
            this.labelProcesador0Corriendo.Size = new System.Drawing.Size(0, 13);
            this.labelProcesador0Corriendo.TabIndex = 6;
            // 
            // labelProcesador2
            // 
            this.labelProcesador2.AutoSize = true;
            this.labelProcesador2.Location = new System.Drawing.Point(739, 0);
            this.labelProcesador2.Name = "labelProcesador2";
            this.labelProcesador2.Size = new System.Drawing.Size(70, 13);
            this.labelProcesador2.TabIndex = 5;
            this.labelProcesador2.Text = "Procesador 2";
            // 
            // labelProcesador1
            // 
            this.labelProcesador1.AutoSize = true;
            this.labelProcesador1.Location = new System.Drawing.Point(371, 0);
            this.labelProcesador1.Name = "labelProcesador1";
            this.labelProcesador1.Size = new System.Drawing.Size(70, 13);
            this.labelProcesador1.TabIndex = 4;
            this.labelProcesador1.Text = "Procesador 1";
            // 
            // gridProcesador3
            // 
            this.gridProcesador3.AllowUserToAddRows = false;
            this.gridProcesador3.AllowUserToDeleteRows = false;
            this.gridProcesador3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridProcesador3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Propiedad2,
            this.Informacion2});
            this.gridProcesador3.Enabled = false;
            this.gridProcesador3.Location = new System.Drawing.Point(742, 28);
            this.gridProcesador3.Name = "gridProcesador3";
            this.gridProcesador3.ReadOnly = true;
            this.gridProcesador3.Size = new System.Drawing.Size(365, 212);
            this.gridProcesador3.TabIndex = 3;
            // 
            // gridProcesador2
            // 
            this.gridProcesador2.AllowUserToAddRows = false;
            this.gridProcesador2.AllowUserToDeleteRows = false;
            this.gridProcesador2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridProcesador2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Propiedad1,
            this.Informacion1});
            this.gridProcesador2.Enabled = false;
            this.gridProcesador2.Location = new System.Drawing.Point(371, 28);
            this.gridProcesador2.Name = "gridProcesador2";
            this.gridProcesador2.ReadOnly = true;
            this.gridProcesador2.Size = new System.Drawing.Size(365, 212);
            this.gridProcesador2.TabIndex = 2;
            // 
            // gridProcesador1
            // 
            this.gridProcesador1.AllowUserToAddRows = false;
            this.gridProcesador1.AllowUserToDeleteRows = false;
            this.gridProcesador1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridProcesador1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Propiedad0,
            this.Informacion0});
            this.gridProcesador1.Enabled = false;
            this.gridProcesador1.Location = new System.Drawing.Point(0, 28);
            this.gridProcesador1.Name = "gridProcesador1";
            this.gridProcesador1.ReadOnly = true;
            this.gridProcesador1.Size = new System.Drawing.Size(365, 212);
            this.gridProcesador1.TabIndex = 1;
            // 
            // labelProcesador0
            // 
            this.labelProcesador0.AutoSize = true;
            this.labelProcesador0.Location = new System.Drawing.Point(3, 0);
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
            this.gridMemoriaCompartida.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMemoriaCompartida.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Registros});
            this.gridMemoriaCompartida.Enabled = false;
            this.gridMemoriaCompartida.Location = new System.Drawing.Point(0, 16);
            this.gridMemoriaCompartida.Name = "gridMemoriaCompartida";
            this.gridMemoriaCompartida.Size = new System.Drawing.Size(1107, 213);
            this.gridMemoriaCompartida.TabIndex = 6;
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
            // labelProcesador1Corriendo
            // 
            this.labelProcesador1Corriendo.AutoSize = true;
            this.labelProcesador1Corriendo.Location = new System.Drawing.Point(371, 13);
            this.labelProcesador1Corriendo.Name = "labelProcesador1Corriendo";
            this.labelProcesador1Corriendo.Size = new System.Drawing.Size(0, 13);
            this.labelProcesador1Corriendo.TabIndex = 7;
            // 
            // labelProcesador2Corriendo
            // 
            this.labelProcesador2Corriendo.AutoSize = true;
            this.labelProcesador2Corriendo.Location = new System.Drawing.Point(739, 13);
            this.labelProcesador2Corriendo.Name = "labelProcesador2Corriendo";
            this.labelProcesador2Corriendo.Size = new System.Drawing.Size(0, 13);
            this.labelProcesador2Corriendo.TabIndex = 8;
            // 
            // Propiedad1
            // 
            this.Propiedad1.HeaderText = "Propiedad";
            this.Propiedad1.Name = "Propiedad1";
            this.Propiedad1.ReadOnly = true;
            // 
            // Informacion1
            // 
            this.Informacion1.HeaderText = "Información";
            this.Informacion1.Name = "Informacion1";
            this.Informacion1.ReadOnly = true;
            this.Informacion1.Width = 220;
            // 
            // Propiedad0
            // 
            this.Propiedad0.HeaderText = "Propiedad";
            this.Propiedad0.Name = "Propiedad0";
            this.Propiedad0.ReadOnly = true;
            // 
            // Informacion0
            // 
            this.Informacion0.HeaderText = "Información";
            this.Informacion0.Name = "Informacion0";
            this.Informacion0.ReadOnly = true;
            this.Informacion0.Width = 220;
            // 
            // Propiedad2
            // 
            this.Propiedad2.HeaderText = "Propiedad";
            this.Propiedad2.Name = "Propiedad2";
            this.Propiedad2.ReadOnly = true;
            // 
            // Informacion2
            // 
            this.Informacion2.HeaderText = "Información";
            this.Informacion2.Name = "Informacion2";
            this.Informacion2.ReadOnly = true;
            this.Informacion2.Width = 220;
            // 
            // Registros
            // 
            this.Registros.HeaderText = "Registros";
            this.Registros.Name = "Registros";
            this.Registros.Width = 1060;
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
            ((System.ComponentModel.ISupportInitialize)(this.gridProcesador3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridProcesador2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridProcesador1)).EndInit();
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
        private System.Windows.Forms.DataGridView gridProcesador3;
        private System.Windows.Forms.DataGridView gridProcesador2;
        private System.Windows.Forms.DataGridView gridProcesador1;
        private System.Windows.Forms.DataGridView gridMemoriaCompartida;
        private System.Windows.Forms.Label labelMemoriaCompartida;
        private System.Windows.Forms.Label labelProcesador0Corriendo;
        private System.Windows.Forms.Label labelProcesador2Corriendo;
        private System.Windows.Forms.Label labelProcesador1Corriendo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Propiedad2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Informacion2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Propiedad1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Informacion1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Propiedad0;
        private System.Windows.Forms.DataGridViewTextBoxColumn Informacion0;
        private System.Windows.Forms.DataGridViewTextBoxColumn Registros;
    }
}


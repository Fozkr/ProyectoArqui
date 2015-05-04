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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.BotonNuevaSimulacion = new System.Windows.Forms.Button();
            this.TextBoxCantidadProgramas = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BotonAgregarArchivo = new System.Windows.Forms.Button();
            this.PanelIniciar = new System.Windows.Forms.Panel();
            this.GridPaths = new System.Windows.Forms.DataGridView();
            this.Path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BotonIniciarSimulacion = new System.Windows.Forms.Button();
            this.PanelIniciar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridPaths)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog";
            // 
            // BotonNuevaSimulacion
            // 
            this.BotonNuevaSimulacion.Location = new System.Drawing.Point(12, 12);
            this.BotonNuevaSimulacion.Name = "BotonNuevaSimulacion";
            this.BotonNuevaSimulacion.Size = new System.Drawing.Size(100, 30);
            this.BotonNuevaSimulacion.TabIndex = 0;
            this.BotonNuevaSimulacion.Text = "Nueva simulación";
            this.BotonNuevaSimulacion.UseVisualStyleBackColor = true;
            // 
            // TextBoxCantidadProgramas
            // 
            this.TextBoxCantidadProgramas.Location = new System.Drawing.Point(128, 3);
            this.TextBoxCantidadProgramas.Name = "TextBoxCantidadProgramas";
            this.TextBoxCantidadProgramas.Size = new System.Drawing.Size(100, 20);
            this.TextBoxCantidadProgramas.TabIndex = 1;
            this.TextBoxCantidadProgramas.TextChanged += new System.EventHandler(this.TextBoxCantidadProgramas_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Cantidad de programas:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(270, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Escoger archivos:";
            // 
            // BotonAgregarArchivo
            // 
            this.BotonAgregarArchivo.Location = new System.Drawing.Point(368, 1);
            this.BotonAgregarArchivo.Name = "BotonAgregarArchivo";
            this.BotonAgregarArchivo.Size = new System.Drawing.Size(75, 23);
            this.BotonAgregarArchivo.TabIndex = 4;
            this.BotonAgregarArchivo.Text = "Agregar";
            this.BotonAgregarArchivo.UseVisualStyleBackColor = true;
            // 
            // PanelIniciar
            // 
            this.PanelIniciar.Controls.Add(this.BotonIniciarSimulacion);
            this.PanelIniciar.Controls.Add(this.GridPaths);
            this.PanelIniciar.Controls.Add(this.label1);
            this.PanelIniciar.Controls.Add(this.BotonAgregarArchivo);
            this.PanelIniciar.Controls.Add(this.TextBoxCantidadProgramas);
            this.PanelIniciar.Controls.Add(this.label2);
            this.PanelIniciar.Location = new System.Drawing.Point(12, 54);
            this.PanelIniciar.Name = "PanelIniciar";
            this.PanelIniciar.Size = new System.Drawing.Size(1130, 106);
            this.PanelIniciar.TabIndex = 5;
            // 
            // GridPaths
            // 
            this.GridPaths.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridPaths.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Path});
            this.GridPaths.Location = new System.Drawing.Point(469, 0);
            this.GridPaths.Name = "GridPaths";
            this.GridPaths.Size = new System.Drawing.Size(661, 106);
            this.GridPaths.TabIndex = 5;
            // 
            // Path
            // 
            this.Path.HeaderText = "Path del archivo";
            this.Path.Name = "Path";
            this.Path.Width = 1000;
            // 
            // BotonIniciarSimulacion
            // 
            this.BotonIniciarSimulacion.Location = new System.Drawing.Point(179, 54);
            this.BotonIniciarSimulacion.Name = "BotonIniciarSimulacion";
            this.BotonIniciarSimulacion.Size = new System.Drawing.Size(100, 23);
            this.BotonIniciarSimulacion.TabIndex = 6;
            this.BotonIniciarSimulacion.Text = "Iniciar Simulación";
            this.BotonIniciarSimulacion.UseVisualStyleBackColor = true;
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1154, 692);
            this.Controls.Add(this.PanelIniciar);
            this.Controls.Add(this.BotonNuevaSimulacion);
            this.Name = "FormPrincipal";
            this.Text = "Proyecto Arquitectura de computadoras - Simulador de procesadores MIPS";
            this.PanelIniciar.ResumeLayout(false);
            this.PanelIniciar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridPaths)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button BotonNuevaSimulacion;
        private System.Windows.Forms.TextBox TextBoxCantidadProgramas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BotonAgregarArchivo;
        private System.Windows.Forms.Panel PanelIniciar;
        private System.Windows.Forms.DataGridView GridPaths;
        private System.Windows.Forms.DataGridViewTextBoxColumn Path;
        private System.Windows.Forms.Button BotonIniciarSimulacion;
    }
}


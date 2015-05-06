using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoArqui
{
    /*
     * 
     */
    public partial class FormPrincipal : Form
    {
        //Atributos
        //Controladora hiloMaestro;

        /*
         * Constructor, inicializa la instancia del hilo maestro.
         */
        public FormPrincipal()
        {
            InitializeComponent();
            //hiloMaestro = new Controladora();
        }

        /*
         * Limpia los campos para comenzar una nueva simulación.
         */
        private void BotonNuevaSimulacion_Click(object sender, EventArgs e)
        {
            TextBoxCantidadProgramas.Text = "";
            GridPaths.Rows.Clear();
            TextBoxCantidadProgramas.Enabled = true;
            BotonAgregarArchivo.Enabled = true;
        }

        /*
         * Cuando escriba la cantidad de programas que serán ejecutados en la simulación, se guardan en el atributo de la clase,
         * más adelante debe bloquearse este textbox, cuando la simulación sea iniciada.
         */
        private void TextBoxCantidadProgramas_TextChanged(object sender, EventArgs e)
        {
            //if(TextBoxCantidadProgramas.TextLength != 0)
            //    hiloMaestro.CantidadProgramas = Convert.ToInt32(TextBoxCantidadProgramas.Text);
        }

        /*
         * Abre el FileChooser para poder escoger un archivo que será agregado al grid.
         */
        private void BotonAgregarArchivo_Click(object sender, EventArgs e)
        {
            FileChooser.Reset();
            FileChooser.ShowDialog();
        }

        /*
         * Al escogerse un archivo, se agrega el path del mismo al grid. También, cuando haya escogido la misma cantidad de archivos
         * que especificó en el textbox, se habilita el botón de iniciar la simulación.
         */
        private void FileChooser_FileOk(object sender, CancelEventArgs e)
        {
            String pathNuevoArchivo = FileChooser.FileName;
            GridPaths.Rows.Add(pathNuevoArchivo);
            if(TextBoxCantidadProgramas.TextLength == GridPaths.Rows.Count)
                BotonIniciarSimulacion.Enabled = true;
        }

        /*
         * Inicia la simulación.
         * Por ahora, y temporalmente, albergará el código para la carga de instrucciones desde los archivos.
         */
        private void BotonIniciarSimulacion_Click(object sender, EventArgs e)
        {
            List<int> instrucciones = new List<int>();      //arreglo general que almacenará todas las instrucciones leídas
            List<int> iniciosProgramas = new List<int>();   //arreglo pequeño que almacena los índices en el anterior donde inicia cada programa
            String pathArchivo = "";            //usada para iterar por los paths
            String instruccionIndividual = "";  //usada para iterar por las líneas de los archivos
            int parteInstruccion = 0;           //usada para iterar por los números enteros en cada línea
            foreach(DataGridViewRow fila in GridPaths.Rows)
            {
                pathArchivo = fila.Cells[0].Value.ToString();
                iniciosProgramas.Add(instrucciones.Count);
                System.IO.TextReader lector = System.IO.File.OpenText(pathArchivo); //abre el archivo para Leer sus líneas una por una
                while ((instruccionIndividual = lector.ReadLine()) != null)
                {
                    string[] partes = instruccionIndividual.Split(' '); //divide cada línea en partes, usando los espacios como token separador
                    for (short i = 0; i < 4; ++i)
                        instrucciones.Add(parteInstruccion = int.Parse(partes[i])); //agrega cada número entero al arreglo
                }
            }
        }
    }
}

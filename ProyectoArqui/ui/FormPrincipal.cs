using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoArqui.simulador;

namespace ProyectoArqui
{
    /*
     * Clase interfaz, obtiene los parámetros del usuario y crea la instancia del hilo maestro para iniciar el programa.
     */
    public partial class FormPrincipal : Form
    {
        //Atributos
        Simulador hiloMaestro;

        /*
         * Constructor, inicializa la instancia del hilo maestro.
         */
        public FormPrincipal()
        {
            InitializeComponent();
            hiloMaestro = new Simulador();
        }

        /*
         * Limpia y habilita los campos para comenzar una nueva simulación.
         */
        private void BotonNuevaSimulacion_Click(object sender, EventArgs e)
        {
            TextBoxCantidadProgramas.Text = "";
            GridPaths.Rows.Clear();
            TextBoxCantidadProgramas.Enabled = true;
            BotonAgregarArchivo.Enabled = true;
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
         * Lee desde los archivos todas las instrucciones para luego enviarlas al simulador.
         */
        private void BotonIniciarSimulacion_Click(object sender, EventArgs e)
        {
            if ((TextBoxCantidadProgramas.TextLength != GridPaths.Rows.Count) || (TextBoxCantidadProgramas.TextLength == 0))
                return; //TODO mostrar un mensaje de error

            //Deshabilitar botones para no entorpecer la interfaz durante la simulación (TODO agregar botón para detenerla)
            BotonNuevaSimulacion.Enabled = false;
            TextBoxCantidadProgramas.Enabled = false;
            BotonAgregarArchivo.Enabled = false;
            BotonIniciarSimulacion.Enabled = false;

            //Lee todas las instrucciones y las guarda en una lista
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

            //Enviar parámetros al simulador e iniciar la simulación
            hiloMaestro.CantidadProgramas = Convert.ToInt16(TextBoxCantidadProgramas.Text); //se asume que sólo escribirán números
            hiloMaestro.ejecutarSimulacion(instrucciones, iniciosProgramas);
        }
    }
}

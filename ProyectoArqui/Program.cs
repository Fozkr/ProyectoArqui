using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoArqui.simulador;
using System.Diagnostics;

namespace ProyectoArqui
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Para probar
            TextWriterTraceListener twtr = new TextWriterTraceListener(System.IO.File.CreateText("Debug.txt"));
            Debug.Listeners.Add(twtr);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormPrincipal());

            // El metodo probar es para verificar la funcionalidad del programa
            // probar();
        }

        static void probar()
        {
            List<int> instrucciones = new List<int>();      //arreglo general que almacenará todas las instrucciones leídas
            List<int> iniciosProgramas = new List<int>();   //arreglo pequeño que almacena los índices en el anterior donde inicia cada programa

            int c = 0;

            string[] archivos = { Properties.Resources.Hilo1, Properties.Resources.Hilo2, Properties.Resources.Hilo3, Properties.Resources.Hilo4 };

            foreach (string archivo in archivos)
            {
                iniciosProgramas.Add(c);
                foreach (string linea in archivo.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None))
                {
                    string[] partes = linea.Split(' '); //divide cada línea en partes, usando los espacios como token separador
                    if (partes.Length == 4)
                    {
                        for (short i = 0; i < 4; ++i)
                        {
                            c++;
                            instrucciones.Add(int.Parse(partes[i])); //agrega cada número entero al arreglo
                        }
                    }
                }

            }

            Simulador programa = new Simulador();
            programa.ejecutarSimulacion(instrucciones, iniciosProgramas);

        }

    }
}

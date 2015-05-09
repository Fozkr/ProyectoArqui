using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.Controller
{
    class Observable
    {

        List<Listener> listeners = new List<Listener>();

        public void AddListener(Listener listener)
        {
            listeners.Add(listener);
        }

        public void fireTickChanged(int newTick)
        {
            foreach (Listener l in listeners)
            {
                l.onTickChanged(newTick);
            }
        }

        public void fireProgramNameChanged(String newName, int idProcesador)
        {
            foreach (Listener l in listeners)
            {
                l.onProgramNameChanged(newName, idProcesador);
            }
        }

        void onProgramCounterChanged(int newPc, int idProcesador)
        {
            foreach (Listener l in listeners)
            {
                l.onProgramCounterChanged(newPc, idProcesador);
            }
        }

        void fireRegistersChanged(int[] nuevosRegistros, int idProcesador)
        {
            foreach (Listener l in listeners)
            {
                l.onRegistersChanged(nuevosRegistros, idProcesador);
            }
        }

        void fireCacheChanged(int[] palabrasCache, int idProcesador)
        {
            foreach (Listener l in listeners)
            {
                l.onCacheChanged(palabrasCache, idProcesador);
            }
        }

        void fireMemoryChanged(int[] palabrasMemoria, int idProcesador)
        {
            foreach (Listener l in listeners)
            {
                l.onMemoryChanged(palabrasMemoria, idProcesador);
            }
        }

        void fireProgramEnded(string nombrePrograma, int[] registrosFinales, int idProcesador)
        {
            foreach (Listener l in listeners)
            {
                l.onProgramEnded(nombrePrograma, registrosFinales, idProcesador);
            }
        }

        public void fireSimulationFinished()
        {
            foreach (Listener l in listeners)
            {
                l.onSimulationFinished();
            }
        }
    }
}

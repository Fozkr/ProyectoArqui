using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.ui
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

        public void fireProgramNameChanged(String newName, int id)
        {
            foreach (Listener l in listeners)
            {
                l.onProgramNameChanged(newName, id);
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

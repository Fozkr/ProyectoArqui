using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.ui
{
    interface Listener
    {
        void onTickChanged(int newTick);
        void onProgramNameChanged(String newName, int id);
        void onSimulationFinished();
    }
}

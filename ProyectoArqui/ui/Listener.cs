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

        void onProgramNameChanged(String newName, int idProcesador);

        void onProgramCounterChanged(int newPc, int idProcesador);

        void onRegistersChanged(int nuevosRegistros[], int idProcesador);

        void onCacheChanged(int palabrasCache[], int idProcesador);

        void onMemoryChanged(int palabrasMemoria[], int idProcesador);

        void onSimulationFinished();

    }
}

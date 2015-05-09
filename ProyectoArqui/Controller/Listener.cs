﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.Controller
{
    interface Listener
    {
        void onTickChanged(int newTick);

        void onProgramChanged(int idProcesador, String nombrePrograma, int ticksReloj, int[] registros, int[] cache);

        void onProgramCounterChanged(int newPc, int idProcesador);

        void onRegistersChanged(int[] nuevosRegistros, int idProcesador);

        void onCacheChanged(int[] palabrasCache, int idProcesador);

        void onMemoryChanged(int[] palabrasMemoria, int idProcesador);

        void onProgramEnded(string nombrePrograma, int[] registrosFinales, int idProcesador);

        void onSimulationFinished();

    }
}

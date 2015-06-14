using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui {

    /// <summary>
    /// Clase donde se almacenan todas las constantes necesarias
    /// para ejecutar la simulacion
    /// </summary>
    abstract class Constantes {

        public const int BytesPorPalabra = 4;
        public const int PalabrasPorBloque = 4;
        public const int BloquesPorMemoria = 8;
        public const int BloquesPorCache = 4;
        public const int RegistrosPorProcesador = 32;

        public const int EsperaOperacionDirectorioLocal = 2;
        public const int EsperaOperacionDirectorioRemoto = 4;
        public const int EsperaEnvioMemoriaLocal = 16;
        public const int EsperaEnvioMemoriaRemota = 32;
        public const int EsperaTraerMemoriaLocal = 16;
        public const int EsperaTraerMemoriaRemota = 32;
        public const int EsperaTraerCacheRemota = 20;

        // Los siguientes atributos se calculan por si en algún momento
        // alguien modifica los datos de arriba.

        public const int PalabrasPorMemoria = PalabrasPorBloque * BloquesPorMemoria; // 4 * 8 = 32
        public const int PalabrasPorCache = PalabrasPorBloque * BloquesPorCache; // 4 * 4 = 16

        public const int BytesPorBloque = BytesPorPalabra * PalabrasPorBloque; // 4 * 4 = 16
        public const int BytesPorMemoria = BytesPorPalabra * PalabrasPorMemoria; // 4 * 32 = 128

        /// <summary>
        /// Enumeración de los estados posibles de un bloque en la Cache
        /// </summary>
        public enum EstadosB {
            Invalido,
            Compartido,
            Modificado
        };

        /// <summary>
        /// Enumeración de los estados posibles de un bloque en el directorio
        /// </summary>
        public enum EstadosD {
            Uncached,
            Compartido,
            Modificado
        };

    }
}

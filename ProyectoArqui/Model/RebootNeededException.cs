using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoArqui.Model {
    class RebootNeededException : Exception {
        public RebootNeededException() : base("Se necesita reinicar la operacion") {
        }

        public RebootNeededException(string message)
            : base(message) {
        }

        public RebootNeededException(string message, Exception inner)
            : base(message, inner) {
        }

    }
}

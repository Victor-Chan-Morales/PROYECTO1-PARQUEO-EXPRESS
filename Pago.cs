using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO1_PARQUEO_EXPRESS
{
    public abstract class Pago
    {
        protected double Monto;
        public abstract void ProcesarPago();
    }
}

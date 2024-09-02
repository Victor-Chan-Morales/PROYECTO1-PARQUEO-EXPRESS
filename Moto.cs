using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO1_PARQUEO_EXPRESS
{
    public class Moto : Vehiculo
    {
        public bool SideCarr { get; set; }
        public Moto(string placa, string color, string marca, int anio, DateTime horaIngreso, string tipoVehiculo, bool sideCar) : base(placa, color, marca, anio, horaIngreso, tipoVehiculo)
        {
            this.SideCarr=sideCar;
        }

        // Mostrar la visibilidad del sideCar
        public string MostrarSideCar(bool sideCar)
        {
            if (sideCar)
            {
                return "Cuenta con sideCar";
            }
            else
            {
                return "No cuenta con sideCar";
            }
        }

        // Método heredado para mostrar información
        public override void MostrarInfo()
        {
            base.MostrarInfo();
            
            Console.WriteLine("    → SIDECAR: "+MostrarSideCar(SideCarr)+"\n");
            
        }
     

    }
}

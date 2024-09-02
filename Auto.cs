using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO1_PARQUEO_EXPRESS
{
    public class Auto : Vehiculo
    {
        public int NumeroPuertas { get; set; }
        public Auto(string placa, string color, string marca, int anio, DateTime horaIngreso, string tipoVehiculo, int numeroPuertas) : base(placa, color, marca, anio, horaIngreso, tipoVehiculo)
        {
            this.NumeroPuertas = numeroPuertas;
        }
        // Método heredado para mostrar información
        public override void MostrarInfo()
        {
            base.MostrarInfo();
            Console.Write($"    → NÚMERO DE PUERTAS: {NumeroPuertas}\n");

        }

    }
        
}

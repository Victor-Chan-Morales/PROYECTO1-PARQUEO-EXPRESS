using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO1_PARQUEO_EXPRESS
{
    public class Camion : Vehiculo
    {
        public string TipoCamion { get; set; }
        public Camion(string placa, string color, string marca, int anio, DateTime horaIngreso, string tipoVehiculo, string tipoCamion) : base(placa, color, marca, anio, horaIngreso, tipoVehiculo)
        {
            this.TipoCamion = tipoCamion;
        }

        public override void MostrarInfo()
        {
            base.MostrarInfo();
            Console.WriteLine($"    → TIPO DE CAMIÓN: {TipoCamion}\n");
        }
    }
}

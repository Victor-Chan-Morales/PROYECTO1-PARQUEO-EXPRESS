using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO1_PARQUEO_EXPRESS
{
    public class Vehiculo
    {
        private string Placa;
        private string Color;
        private string Marca;
        private int Anio;
        private DateTime HoraIngreso;
        private string TipoVehiculo;
        
        // Métodos públicos para acceder a campos privados:
        public string placa
        {
            get { return Placa; }
            set { Placa = value; }
        }
        public string color
        {
            get { return Color; }
            set { Color = value; }
        }
        public string marca
        {
            get { return Marca; }
            set { Marca = value; }
        }
        public DateTime horaIngreso
        {
            get { return HoraIngreso; }
            set { HoraIngreso = value; }
        }
        public string tipoVehiculo
        {
            get { return TipoVehiculo; }
            set { TipoVehiculo = value; }
        }
        public int anio
        {
            get { return Anio; }
            set { Anio = value; }
        }
        
                
        // Constructores de métodos públicos
        public Vehiculo(string placa, string color, string marca, int anio, DateTime horaIngreso, string tipoVehiculo)
        {
            this.placa = placa;
            this.color = color;
            this.marca = marca;
            this.anio = anio;
            this.horaIngreso = horaIngreso;
            this.tipoVehiculo= tipoVehiculo;
        }
        // Método virtual para mostrar la información base
        public virtual void MostrarInfo()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nDATOS:");
            Console.ResetColor();
            Console.WriteLine($"    → PLACA: {Placa}\n    → MARCA: {Marca}\n    → COLOR: {Color}\n    → HORA DE ENTRADA: {HoraIngreso}\n    → TIPO DE VEHÍCULO: {TipoVehiculo}\n    → AÑO: {anio}");
        }
        // Metodo para calcular el costo del parqueo
        public virtual double CalcularCostoParqueo(int horas, double fraccion, int precioHora, int precioFraccion)
        {
            // Valor de la hora = Q10 *** Valor de la fraccion 1 a 30 min = Q5 y de 31 a 59 min = Q10
            int costoHoras = horas * precioHora;
            int precioFrac = 0;
            
            if (fraccion > 0 && fraccion <= 0.5)
            {
                precioFrac += precioFraccion;
            }
            else if (fraccion > 0.5 && fraccion <1 )
            {
                precioFrac += precioHora;
            }
            return costoHoras + precioFrac;
        }
        


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO1_PARQUEO_EXPRESS
{
    public class Tarjeta:Pago
    {
        private string NumeroTarjeta;
        private string NombreTitular;
        private DateTime FechaVencimiento;
        private int Cvv;

        public Tarjeta(double monto, string numeroTarjeta, string nombreTitular, DateTime fechaVencimiento, int cvv)
        {
            this.Monto = monto;
            this.NumeroTarjeta = numeroTarjeta;
            this.NombreTitular = nombreTitular;
            this.FechaVencimiento = fechaVencimiento;
            this.Cvv = cvv;
        }
        public override void ProcesarPago()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=====================");
            Console.WriteLine("║  PARKING EXPRESS  ║");
            Console.WriteLine("=====================\n"); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("PROCESANDO TARJETA");
            for (int i = 0; i < 10; i++)
            {
                Console.Write(".");
                Thread.Sleep(200); // Simula tiempo de carga
            }
            Console.ResetColor();
            if (ValidarDatosTarjeta())
            {
                Console.ForegroundColor= ConsoleColor.DarkGreen;
                Console.WriteLine("\nPago con tarjeta aprobado. Monto: Q" + Monto);
                Console.ResetColor();

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\nError en los detalles de la tarjeta. Por favor, intente nuevamente.");
                Console.ResetColor();
            }
        }
        public bool ValidarDatosTarjeta()
        {
            if (NumeroTarjeta.Length != 16)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\nNúmero de tarjeta inválido.");
                Console.ResetColor();
                return false;
            }
            if (FechaVencimiento <= DateTime.Now)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\nLa tarjeta ha expirado.");
                Console.ResetColor();
                return false;
            }
            if (Cvv < 100 || Cvv > 9999)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\nCVV inválido.");
                Console.ResetColor();
                return false;
            }
            return true;
        }

    }
}

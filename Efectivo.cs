using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO1_PARQUEO_EXPRESS
{
    public class Efectivo : Pago
    {
        private double MontoEntregado;
        public Efectivo(double monto)
        {
            this.Monto = monto;
        }

        public void IniciarMontoEntregado(double montoEntregado)
        {
            this.MontoEntregado = montoEntregado;
        }
        // Método para procesar el pago en efectivo
        public override void ProcesarPago()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=====================");
            Console.WriteLine("║  PARKING EXPRESS  ║");
            Console.WriteLine("=====================\n"); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("PROCESANDO PAGO ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write(".");
                Thread.Sleep(200); // Simula tiempo de carga
            }
            Console.ResetColor();
            if (VerificarMonto(MontoEntregado, Monto))
            {
                double cambio = MontoEntregado - Monto;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\n¡¡¡ PAGO EXITOSO !!!\n"); Console.ResetColor();
                Console.WriteLine($"    Monto entregado: Q{MontoEntregado} ---- Cambio: Q{cambio}");
                if (cambio>0)
                {
                    DesglosarCambio(cambio);
                }
                else
                {
                    Console.WriteLine("\nNo hay cambio...");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n¡No es posible procesar el pago, el Monto entregado por el cliente debe ser mayor o igual al total a pagar"); Console.ResetColor();
            }
        }
        public bool VerificarMonto(double entregado,  double monto)
        {
            if (monto > entregado)
            {
                return false;
            }
            else if (monto <= entregado)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        // Método para calcular el desglose del vuelto
        public void DesglosarCambio(double cambio)
        {
            int[] billetes = { 200, 100, 50, 20, 10, 5, 1 };
            int[] cantidadBilletes = new int[billetes.Length];

            for (int i = 0; i < billetes.Length; i++)
            {
                cantidadBilletes[i] = (int)(cambio / billetes[i]);
                cambio %= billetes[i];
            }

            Console.WriteLine("Cambio desglosado en billetes:");
            for (int i = 0; i < billetes.Length; i++)
            {
                if (cantidadBilletes[i] > 0)
                {
                    Console.WriteLine($"{cantidadBilletes[i]} billete(s) de Q{billetes[i]}");
                }
            }
        }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO1_PARQUEO_EXPRESS
{
    public class Estacionamiento
    {
        // Campo de estacionamiento
        public int EspaciosDisponibles { get; set; }
        List<Vehiculo> listaVehiculos = new List<Vehiculo>();

        // Propiedad pública para acceder al campo privado
        public Estacionamiento(int espaciosDisponibles)
        {
            this.EspaciosDisponibles = espaciosDisponibles;
        }

        // Método para calcular las horas enteras 
        public static int CalcularHoras(DateTime horaEntrada)
        {
            DateTime horaSalida = DateTime.Now;
            TimeSpan totalTiempo =horaSalida- horaEntrada;
            double horasTotal = totalTiempo.TotalHours;
            int redondeo = (int)Math.Floor(horasTotal);
            return redondeo;
        }

        // Método para calcular la fracción de hora
        public static double CalcularFraccion(DateTime horaEntrada)
        {
            DateTime horaSalida = DateTime.Now;
            TimeSpan totalTiempo = horaSalida-horaEntrada;
            double horasTotal = totalTiempo.TotalHours;
            double fraccionHoras = totalTiempo.TotalHours - Math.Floor(totalTiempo.TotalHours);
            return Math.Round(fraccionHoras, 2);
        }

        // MétodO para mostrar los espacios diponibles en el estacionamiento
        public void MostrarEspacios()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=====================");
            Console.WriteLine("║  PARKING EXPRESS  ║");
            Console.WriteLine("=====================\n");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("    → ESPACIOS DISPONIBLES: " + EspaciosDisponibles);
            Console.ResetColor();
        }

        // Método para agregar vehículos a la lista
        public void AgregarVehiculoALista(Vehiculo vehiculoAgreagr)
        {
            if (EspaciosDisponibles > 0)
            {
                listaVehiculos.Add(vehiculoAgreagr);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Se ha agregado exitosamente el vehículo..."); Console.ResetColor();
                EspaciosDisponibles--;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("El estacionamiento no cuenta con espacios disponibles..."); Console.ResetColor();
            }
        }

        // Método para ver los vehículos estacionados
        public void MostrarVehiculos()
        {
            if (listaVehiculos.Count>0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("=====================");
                Console.WriteLine("║  PARKING EXPRESS  ║");
                Console.WriteLine("=====================\n");Console.ResetColor();
                foreach (var grupo in listaVehiculos.GroupBy(v => v.GetType().Name))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"\n===== {grupo.Key} =====");
                    Console.ResetColor();

                    foreach (var vehiculo in grupo)
                    {
                        vehiculo.MostrarInfo();
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("=====================");
                Console.WriteLine("║  PARKING EXPRESS  ║");
                Console.WriteLine("=====================\n"); Console.ResetColor();
                Console.ForegroundColor= ConsoleColor.DarkRed;
                Console.WriteLine("Aún no hay vehículos registrados..."); Console.ResetColor();
            }
        }
        
        // Método para eliminar vehículos
        public void EliminarVehiculo(Vehiculo placaAELiminar)
        {
            listaVehiculos.Remove(placaAELiminar);
            Console.ForegroundColor= ConsoleColor.DarkGreen;
            Console.WriteLine("Se ha eliminado el vehículo del sistema..."); Console.ResetColor();
            EspaciosDisponibles++;
        }

        // Método para procesar el apgo efectivo
        public void ProcesarPagoEfectivo(double monto, Vehiculo eliminar)
        {
            Console.Write("Ingrese el monto entregado por el cliente: Q");
            double montoEntregado = Convert.ToDouble(Console.ReadLine());

            Efectivo pagoEfectivo = new Efectivo(monto);
            pagoEfectivo.IniciarMontoEntregado(montoEntregado);
            if (pagoEfectivo.VerificarMonto(montoEntregado,monto))
            {
                pagoEfectivo.ProcesarPago();
                EliminarVehiculo(eliminar);
                EspaciosDisponibles++;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("El vehículo no se eliminará debido a un fallo en el pago."); Console.ResetColor();
            }
        }

        // Método para procesar el pago con tarjeta
        public void ProcesarPagoConTarjeta(double monto, Vehiculo eliminar)
        {
            Console.WriteLine("Ingrese el número de tarjeta (16 dígitos):");
            string numeroTarjeta = Console.ReadLine();
            Console.WriteLine("Ingrese el nombre del titular de la tarjeta:");
            string nombreTitular = Console.ReadLine();
            Console.WriteLine("Ingrese la fecha de vencimiento (MM/AAAA):");
            DateTime fechaVencimiento = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese el CVV (3 o 4 dígitos):");
            int cvv = Convert.ToInt32(Console.ReadLine());

            Tarjeta pagoTarjeta = new Tarjeta(monto, numeroTarjeta, nombreTitular, fechaVencimiento, cvv);
            if (pagoTarjeta.ValidarDatosTarjeta())
            {
                pagoTarjeta.ProcesarPago();
                EliminarVehiculo(eliminar);
                EspaciosDisponibles++;
                Console.WriteLine("Gracias por usar el servicio de Parking Express...");
            }
            else
            {
                Console.ForegroundColor= ConsoleColor.DarkRed;
                Console.WriteLine("El vehículo no se eliminará debido a un fallo en el pago."); Console.ResetColor();
            }
        }

        // Método para procesar el retiro de un vehículo
        public void RetirarVehiculo(string placa)
        {
            Vehiculo buscarVehiculo=listaVehiculos.Find(x=>x.placa==placa);
            if (buscarVehiculo==null)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n¡¡ Este vehículo no está registrado !!"); Console.ResetColor();
            }
            else
            {
                int calcularHorasEnteras = CalcularHoras(buscarVehiculo.horaIngreso);
                double calcularHorasFraccionarias = CalcularFraccion(buscarVehiculo.horaIngreso);
                double costoParqueo = buscarVehiculo.CalcularCostoParqueo(calcularHorasEnteras, calcularHorasFraccionarias, 10, 5);
                Console.ForegroundColor= ConsoleColor.DarkYellow;
                Console.WriteLine("\nTotal a pagar es de: Q"+costoParqueo); Console.ResetColor();
                Console.WriteLine("Hora de entrada: "+buscarVehiculo.horaIngreso);
                Console.WriteLine("Hora de salida: "+DateTime.Now);
                Console.WriteLine("Total de tiempo: "+(calcularHorasEnteras+calcularHorasFraccionarias)+ " Horas");
                bool confirmarPago = true;
               while (confirmarPago)
                {
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("\nConfirme que desea retirar al vehículo con placas "); Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write("PLACA: " + placa); Console.ResetColor();
                        Console.Write("\nIngrese S para confirmar o N para cancelar: ");
                        string respuesta = Console.ReadLine().ToUpper();
                        if (respuesta == "S")
                        {
                            bool pagar = true;
                            while (pagar)
                            {
                                try
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine("Total a pagar es de: Q" + costoParqueo); Console.ResetColor();
                                    Console.WriteLine("\nSeleccione el método a pagar:\n    → 1. Efectivo\n    → 2. Con tarjeta");
                                    int metodoPago = int.Parse(Console.ReadLine());
                                    switch (metodoPago)
                                    {
                                        case 1:
                                            ProcesarPagoEfectivo(costoParqueo, buscarVehiculo);
                                            EspaciosDisponibles++;
                                            pagar = false;
                                            confirmarPago = false;
                                            break;
                                        case 2:
                                            ProcesarPagoConTarjeta(costoParqueo, buscarVehiculo);
                                            pagar = false;
                                            confirmarPago=false;
                                            break;
                                        default:
                                            Console.WriteLine("Esta opción no está disponible, sleeccione otra..."); Console.ReadKey();
                                            break;
                                    }
                                    Console.ReadKey();
                                    pagar = false;
                                }
                                catch (FormatException)
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine("Error: ha ingresado un caracter inválido, inténte de nuevo..."); Console.ResetColor();
                                    Console.ReadKey();
                                }
                            }
                        }
                        else if (respuesta == "N")
                        {
                            Console.WriteLine("Operación cancelada...");
                            Console.ReadKey();
                            break;
                        }
                    }
                    catch (FormatException)
                    {
                        Console.ForegroundColor= ConsoleColor.DarkRed;
                        Console.WriteLine("Error: ha ingresado un caracter inválido, enter e inténtelo de nuevo..."); Console.ResetColor();
                        Console.ReadKey();
                    }
                }
                
                

            }
        }

        // Método para acceder a esta lista
        public bool VerificarExistencia(string placa)
        {
            Vehiculo verificar=listaVehiculos.Find(x=>x.placa==placa);
            if (verificar==null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool ComprobrarAutos()
        {
            if (listaVehiculos.Count == 0)
            {
                return false ;
            }
            else
            {
                return true;
            }
        }
        public bool CorrobararEspacios()
        {
            if (EspaciosDisponibles>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

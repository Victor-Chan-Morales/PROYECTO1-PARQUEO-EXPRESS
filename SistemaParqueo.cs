using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO1_PARQUEO_EXPRESS
{
    public class SistemaParqueo
    {
        private Estacionamiento parkingExpress;
        public SistemaParqueo (int capacidadParqueo)
        {
            this.parkingExpress = new Estacionamiento(capacidadParqueo);
        }
        // Método para registrar Vehículo
        public void RegistrarVehículo()
        {
           if (parkingExpress.CorrobararEspacios())
            {
                try
                {
                    int tipoVehiculo;
                    do
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Seleccione el típo de vehículo que desea agregar:\n1. Auto\n2. Moto\n3. Camión");
                        tipoVehiculo = int.Parse(Console.ReadLine());
                        if (tipoVehiculo == 1 || tipoVehiculo == 2 || tipoVehiculo == 3)
                        {
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("No disponible..."); Console.ResetColor();
                        }
                    } while (true);
                    Console.ResetColor();
                    Console.Write("Ingrese la placa del vehículo: ");
                    string placaV = Console.ReadLine().ToUpper();
                    if (parkingExpress.VerificarExistencia(placaV))
                    {
                        Console.Write("Ingrese el color del vehículo: ");
                        string colorV = Console.ReadLine().ToUpper();
                        Console.Write("Ingrese la marca del vehículo: ");
                        string marcaV = Console.ReadLine().ToUpper();
                        int anioV;
                        do
                        {
                            Console.Write("Ingrese el año del vehículo: ");
                            anioV = int.Parse(Console.ReadLine());
                            if (anioV > 1900 && anioV < 2026)
                            {
                                break;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("Error: El año debe estar en el rango de 1900 a 2025"); Console.ResetColor();
                            }
                        } while (true);
                        DateTime horaEntradaV = DateTime.Now;
                        switch (tipoVehiculo)
                        {
                            case 1:
                                Console.Write("Ingrese el número de puertas: ");
                                int numeroPuertasV = int.Parse(Console.ReadLine());
                                Vehiculo nuevoAuto = new Auto(placaV, colorV, marcaV, anioV, horaEntradaV, "AUTO", numeroPuertasV);
                                if (ConfirmarOperacion(nuevoAuto))
                                {
                                    parkingExpress.AgregarVehiculoALista(nuevoAuto);
                                }
                                else
                                {
                                    Console.WriteLine("Cancelando operación...");
                                }
                                break;
                            case 2:
                                bool verificarSideCar = false;
                                while (true)
                                {
                                    Console.WriteLine("¿Cuenta con SideCar? S/N");
                                    string sideCar = Console.ReadLine().ToLower();

                                    if (sideCar == "s")
                                    {
                                        verificarSideCar = true;
                                        break;
                                    }
                                    else if (sideCar == "N")
                                    {
                                        verificarSideCar = false;
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Debe ingresar S o N");
                                    }
                                }
                                Vehiculo nuevaMoto = new Moto(placaV, colorV, marcaV, anioV, horaEntradaV, "MOTOCICLETA", verificarSideCar);
                                if (ConfirmarOperacion(nuevaMoto))
                                {
                                    parkingExpress.AgregarVehiculoALista(nuevaMoto);
                                }
                                else
                                {
                                    Console.WriteLine("Cancelando operación...");
                                }
                                break;
                            case 3:
                                string tipoCamion = "";
                                bool menu3 = true;
                                while (menu3)
                                {
                                    Console.WriteLine("Seleccione el tipo de camión: 1. camión de carga, 2. camión de remolque, 3. Tanque, 4. Plataforma");
                                    int opcionCamion = int.Parse(Console.ReadLine());

                                    switch (opcionCamion)
                                    {
                                        case 1:
                                            tipoCamion = "CARGA";
                                            menu3 = false;
                                            break;
                                        case 2:
                                            tipoCamion = "REMOLQUE";
                                            menu3 = false;
                                            break;
                                        case 3:
                                            tipoCamion = "TANQUE";
                                            menu3 = false;
                                            break;
                                        case 4:
                                            tipoCamion = "PLATAFORMA";
                                            menu3 = false;
                                            break;
                                        default:
                                            Console.WriteLine("Opción no válida... seleccione del 1 al 4");
                                            menu3 = true;
                                            break;
                                    }
                                }
                                Vehiculo nuevoCamion = new Camion(placaV, colorV, marcaV, anioV, horaEntradaV, "CAMIÓN", tipoCamion);
                                if (ConfirmarOperacion(nuevoCamion))
                                {
                                    parkingExpress.AgregarVehiculoALista(nuevoCamion);
                                }
                                else
                                {
                                    Console.WriteLine("Cancelando operación...");
                                }
                                break;
                            default:
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("¡¡Esta opción no está disponible!!"); Console.ResetColor();
                                break;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Estas placas ya están registradas...");
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("¡¡ Error: ha ingresado un caracter inválido, intente de nuevo..."); Console.ResetColor();
                }
            }
           else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("¡¡No hay espacios disponibles en este momento!!");
                Console.ResetColor();
            }
        }

        // Método para eliminar vehículo
        public void RetirarVehiculo()
        {
            if (parkingExpress.ComprobrarAutos())
            {
                Console.WriteLine("Ingrese la placa del vehículo:");
                string placaR = Console.ReadLine().ToUpper();
                parkingExpress.RetirarVehiculo(placaR);
            }
            else
            {
                Console.ForegroundColor=ConsoleColor.DarkRed;
                Console.WriteLine("Aún no se ha registrado ningún vehículo, clic para regresar..."); Console.ResetColor();
            }
        }

        // Método para Confirmar agregar/eliminar vehículo
        public bool ConfirmarOperacion(Vehiculo vehiculo)
        {
            Console.ForegroundColor= ConsoleColor.DarkRed;
            Console.WriteLine("\nCONFIRME LOS DATOS DEL VEHÍCULO ANTES DE CONTINUAR: "); Console.ResetColor();
            vehiculo.MostrarInfo();
            Console.ForegroundColor= ConsoleColor.DarkYellow;
            while(true)
            {
                Console.WriteLine("\nPresione S para continuar o N para cancelar..."); Console.ResetColor();
                string confirmacion = Console.ReadLine().ToUpper();
                if (confirmacion == "S")
                {
                    return true;
                    break;
                }
                else if (confirmacion=="N")
                {
                    return false;
                    break;
                }
            }
        }

        // Mostrar menú
        public void MostrarMenu()
        {
            int opcion=0;
            do
            {
                try
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("=====================");
                    Console.WriteLine("║  PARKING EXPRESS  ║");
                    Console.WriteLine("=====================\n"); Console.ResetColor();

                    Console.WriteLine("    1. Registrar Vehículo");
                    Console.WriteLine("    2. Retirar Vehículo");
                    Console.WriteLine("    3. Ver Vehículos Estacionados");
                    Console.WriteLine("    4. Ver Espacios Disponibles");
                    Console.WriteLine("    5. Salir");
                    Console.WriteLine("    Seleccione una opción: ");
                    opcion = Convert.ToInt32(Console.ReadLine());

                    eleccionMenu(opcion);
                    Console.ReadKey();
                }
                catch (FormatException)
                {
                    Console.ForegroundColor=ConsoleColor.DarkRed;
                    Console.WriteLine("ERROR: ha ingresado un caractér inválido, intente de nuevo"); Console.ResetColor();
                    Console.ReadKey();
                }
                catch (DivideByZeroException)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("ERROR: no es posible realizar la operación ya que se indefine una división (n/0)"); Console.ResetColor();
                    Console.ReadKey();
                }
                catch (ArgumentException)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("ERROR: argumento enviado no válido..."); Console.ResetColor();
                    Console.ReadKey();
                }
                catch (InvalidOperationException)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("ERROR: operación inválida en el estado del acutal objeto..."); Console.ResetColor();
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("ERROR: "+ex); Console.ResetColor();
                    Console.ReadKey();
                }
                
            } while (opcion != 5);
        }
        public void eleccionMenu(int opcion)
        {
            switch (opcion)
            {
                case 1:
                    Console.Clear();
                    MostrarEncabezado();
                    RegistrarVehículo();
                    Console.ReadKey();
                    break;
                case 2:
                    Console.Clear();
                    MostrarEncabezado();
                    RetirarVehiculo();
                    Console.ReadKey();
                    break;
                case 3:
                    Console.Clear();
                    parkingExpress.MostrarVehiculos();
                    Console.ReadKey();
                    break;
                case 4:
                    Console.Clear();
                    parkingExpress.MostrarEspacios();
                    Console.ReadKey();
                    break;
                case 5:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\nSaliendo del sistema, clic para cerrar..."); Console.ResetColor();
                    break;
                default:
                    Console.ForegroundColor= ConsoleColor.DarkRed;
                    Console.WriteLine("\nEsta opción no está diponible ingrese una opción válida...");
                    Console.ReadKey();
                    break;
            }
        }
        public void MostrarEncabezado()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=====================");
            Console.WriteLine("║  PARKING EXPRESS  ║");
            Console.WriteLine("=====================\n"); Console.ResetColor();
        }
    }
}

﻿using PROYECTO1_PARQUEO_EXPRESS;

Console.ForegroundColor = ConsoleColor.DarkGray;
Console.WriteLine("=====================");
Console.WriteLine("║  PARKING EXPRESS  ║");
Console.WriteLine("=====================\n"); Console.ResetColor();
Console.ForegroundColor = ConsoleColor.DarkYellow;
Console.Write("Ingresando al programa");
for (int i = 0; i < 10; i++)
{
    Console.Write(".");
    Thread.Sleep(200); // Simula tiempo de carga
}
int espacios = 0;
while (true)
{
    try
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("=====================");
        Console.WriteLine("║  PARKING EXPRESS  ║");
        Console.WriteLine("=====================\n"); Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Clear();
        Console.WriteLine("Bienvenido al sistema de PARKING EXPRESS"); Console.ResetColor();
        Console.Write("Espacios disponibles para hoy: ");
        espacios=int.Parse(Console.ReadLine());
        break;
    }
    catch (FormatException)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("ERROR: ha ingresado un caractér inválido, intente de nuevo"); Console.ResetColor();
        Console.ReadKey();
    }
}

SistemaParqueo parkingExpress=new SistemaParqueo(espacios);
parkingExpress.MostrarMenu();
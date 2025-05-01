using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO;
using System.Threading;
using NetworkStreamNS;
using CarreteraClass;
using VehiculoClass;
using ClienteClass;

namespace Client
{
    class Program
    {
        static TcpClient Client;
        static NetworkStream NS;
        static bool quit = false;
        
        static void Main(string[] args)
        {
            TcpClient cliente = new TcpClient();
            cliente.Connect("127.0.0.1", 10001);

            if (cliente.Connected)
            {
                Console.WriteLine("Cliente: Cliente conectado");

                NetworkStream NS = cliente.GetStream();
                
                // Crear un hilo para la recepción de datos del servidor
                Thread TRecepcionDatos = new Thread(() => RecibirDatosServidor(cliente));
                TRecepcionDatos.Start();

                // Bucle principal del cliente
                while (true)
                {
                    // Lógica del cliente (avance del vehículo, etc.)
                    Vehiculo vehiculo = new Vehiculo();

                    // Avanzar el vehículo
                    for (int i = 0; i < 100; i++)
                    {
                        vehiculo.Pos += vehiculo.Velocidad / 100; // Avanzar la posición del vehículo según su velocidad
                        Console.WriteLine($"Vehículo avanzando: Posición = {vehiculo.Pos}");

                        // Enviar los datos actualizados del vehículo al servidor
                        NetworkStreamClass.EscribirDatosVehiculoNS(NS, vehiculo);
        
                        // Esperar un tiempo para simular el avance del vehículo
                        Thread.Sleep(vehiculo.Velocidad / 100); // El vehículo avanza en función de su velocidad
                    }
                    // Indicar al servidor que el vehículo ha acabado su recorrido
                    vehiculo.Acabado = true;
                    NetworkStreamClass.EscribirDatosVehiculoNS(NS, vehiculo);
                }

                cliente.Close();
            }
            else
            {
                Console.WriteLine("No se pudo conectar al servidor.");
            }

            Console.ReadLine();
        }

        // Método para recibir datos del servidor y mostrarlos por pantalla
        private static void RecibirDatosServidor(TcpClient cliente)
        {
            try
            {
                NetworkStream NS = cliente.GetStream();
                Carretera carretera;

                while (true)
                {
                    // Leer los datos actualizados de la carretera desde el servidor
                    carretera = NetworkStreamClass.LeerDatosCarreteraNS(NS);

                    // Mostrar los vehículos en la carretera recibidos del servidor
                    Console.WriteLine("Vehículos en la carretera recibidos del servidor:");
                    carretera.MostrarBicicletas();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al recibir datos del servidor: " + e.Message);
            }
        }
    }
}
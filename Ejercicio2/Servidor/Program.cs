using System;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Text;
using System.Threading;
using NetworkStreamNS;
using CarreteraClass;
using VehiculoClass;
using ClienteClass;
using System.Net.Quic;
using System.Collections.Generic;

namespace Servidor
{
    
    class Program
    {
        static TcpListener servidor;
        static readonly object locker = new object();
        static List<TcpClient> clientesConectados = new List<TcpClient>();
        static Carretera carretera = new Carretera();
        static int id = 0;
        
        static void Main(string[] args)
        {
            servidor = new TcpListener(IPAddress.Parse("127.0.0.1"), 10001);
            servidor.Start();
            Console.WriteLine("Servidor: Servidor iniciado");

            while (true)
            {
                TcpClient cliente = servidor.AcceptTcpClient();
                NetworkStream NS = cliente.GetStream();
                lock (clientesConectados)
                {
                    clientesConectados.Add(cliente);
                }

                // Crear un hilo para manejar las comunicaciones con el cliente
                Thread TCliente = new Thread(() => GestionarCliente(cliente));
                TCliente.Start();
            }
        }

        // Método para gestionar las comunicaciones con un cliente
        private static void GestionarCliente(TcpClient cliente)
        {
            NetworkStream NS = cliente.GetStream();
            Vehiculo vehiculo;
            bool acabado = false;

            while (!acabado)
            {
                vehiculo = NetworkStreamClass.LeerDatosVehiculoNS(NS); // Leer los datos del vehículo enviado por el cliente
                carretera.ActualizarVehiculo(vehiculo); // Actualizar la posición del vehículo en la carretera

                // Mostrar los vehículos en la carretera
                Console.WriteLine("Vehículos en la carretera:");
                carretera.MostrarBicicletas();

                // Enviar los datos actualizados de la carretera a todos los clientes conectados
                EnviarDatosCarreteraAClientes();

                acabado = vehiculo.Acabado; // Verificar si el vehículo ha acabado su recorrido
            }

            lock (clientesConectados)
            {
                clientesConectados.Remove(cliente);
            }

            cliente.Close();
        }

        // Método para enviar los datos de la carretera a todos los clientes conectados
        private static void EnviarDatosCarreteraAClientes()
        {
            lock (clientesConectados)
            {
                foreach (TcpClient cliente in clientesConectados)
                {
                    NetworkStream NS = cliente.GetStream();
                    NetworkStreamClass.EscribirDatosCarreteraNS(NS, carretera);
                }
            }
        }
    }
}


using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO;
using System.Threading;
using NetworkStreamNS;
using CarreteraClass;
using VehiculoClass;

namespace Client
{
    class Program
    {
        static TcpClient cliente;

        static void Main(string[] args)
        {
            // Crear el cliente TCP
            cliente = new TcpClient();

            cliente.Connect("127.0.0.1", 10001);
            if (cliente.Connected) {
                Console.WriteLine("Cliente: Conectado al servidor.");
            }
        }

    }
}
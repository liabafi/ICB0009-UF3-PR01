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

namespace Servidor
{

    class Program
    {
        static TcpListener servidor;
        
        static void Main(string[] args)
        {
            // Crear el servidor TCP            
            servidor = new TcpListener(IPAddress.Parse("127.0.0.1"), 10001);
        }
    }
}


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
        static NetworkStream NS;
        static bool quit = false;
        

        static void Main(string[] args)
        {
            cliente = new TcpClient();

            try
            {
                cliente.Connect("127.0.0.1", 10001);
                if (cliente.Connected)
                {
                    Console.WriteLine("Cliente: Conectado al servidor.");

                    NS = cliente.GetStream();
                    string mensaje = "";

                    do
                    {
                        if (NS.CanWrite)
                        {
                            mensaje = Console.ReadLine();
                            NetworkStreamClass.EscribirMensajeNetworkStream(NS, mensaje);
                        }
                    } while (mensaje != "quit");
                }
            }
            catch (Exception e )
            {
                Console.WriteLine("Error al conectar con el servidor: {0}", e.Message);
            }
            finally
            {
                NS.Close();
                cliente.Close();
            }
        }
    }
}
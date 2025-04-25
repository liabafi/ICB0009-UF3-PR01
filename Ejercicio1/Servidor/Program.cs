using System;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Text;
using System.Threading;
using NetworkStreamNS;
using VehiculoClass;
using ClienteClass;

namespace Servidor
{

    class Program
    {
        static TcpListener servidor;
        static readonly object locker = new object();
        static int id = 0;
        static ClienteNuevo clienteNuevo = new ClienteNuevo();
        static List<ClienteNuevo> clientesConectados = new List<ClienteNuevo>();
        
        static void Main(string[] args)
        {
            servidor = new TcpListener(IPAddress.Parse("127.0.0.1"), 10001);
            servidor.Start();
            Console.WriteLine("Servidor: Servidor iniciado");

            while (true)
            {
                TcpClient cliente = servidor.AcceptTcpClient();
                Thread clienteThread = new Thread(GestionarCliente);
                clienteThread.Start(cliente);
            }
        }

        private static void GestionarCliente(object clienteObj)
        {
            byte[] bufferLectura = new byte[1024];
            TcpClient cliente = (TcpClient)clienteObj;

            if (cliente.Connected)
            {
                int idCliente;

                lock (locker)
                {
                    id++;
                    Console.WriteLine("Gestionando cliente {0}...", id);
                    idCliente = id;
                }

                NetworkStream NS = cliente.GetStream();
                ClienteNuevo clienteNuevo = new ClienteNuevo(idCliente, NS);

                clientesConectados.Add(clienteNuevo);
                Console.WriteLine("Clientes conectados: " + clientesConectados.Count());

                try 
                {
                    bool quit = false;
                    int mensajeInt;
                    do
                    {
                        string mensaje = NetworkStreamClass.LeerMensajeNetworkStream(NS);

                        if (mensaje == "inicio")
                        {
                            mensajeInt = clienteNuevo.id;
                            mensaje = mensajeInt.ToString();

                            NetworkStreamClass.EscribirMensajeNetworkStream(NS, mensaje);
                        }
                        else if (mensaje == "quit")
                        {
                            quit = true;
                        }
                        else
                        {
                            Console.WriteLine("Cliente: Soy el vehiculo ID = " + mensaje);
                        }
                    } while (!quit);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error al gestionar al cliente: {0}", e.Message);
                }
                finally
                {
                    NS.Close();
                    cliente.Close();
                }
            }
        }
    }
}


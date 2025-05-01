using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO;
using System.Threading;
using NetworkStreamNS;
using CarreteraClass;
using VehiculoClass;
using System.Net.Quic;

namespace ClienteClass;

[Serializable]

public class ClienteNuevo
{
    public int id { get; set; }
    public NetworkStream NS { get; set; }
    private object locker = new object();
    
    public ClienteNuevo()
    {
        
    }
    public ClienteNuevo(int i, NetworkStream NS)
    {
        this.id = i;
        this.NS = NS;
    }

}

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Chess
{
    //the board class is constant
    public class TCPclient
    {
        IPAddress UserIP;
        int serverPort = 0;

        public bool connected = false;


        public TCPclient()
        {
            //UserIP = GetIpAddr();
            //Console.WriteLine(UserIP);
            //ScanForServer();nvhff

        }

        IPAddress GetIpAddr()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }
            return null;
        }
    }
}

//        string ScanForServer()
//        {
//            string[] IPRange = (UserIP.ToString()).Split('.');
//            for (int i = 0; i < 256; i++)
//            {
//                string newIP = IPRange[0] + "." + IPRange[1] + "." + IPRange[2] + "." + i;
//                //Console.WriteLine(newIP);
//                if (PingIP(newIP, serverPort))
//                {
//                    Console.WriteLine(newIP);
//                    return newIP;
//                }
//                else
//                {
//                    Console.WriteLine("no");
//                }
//            }
//            return "no";
//            //IPAddress ScanForServer()
//            //10.120.112.135
//            string newIP = "10.120.112.135";
//                for (int i = 1; i < 500; i++)
//                {
//                    if (PingIP(newIP, i))
//                    {
//                        Console.WriteLine(i);
//                    }
//                    else
//                    {
//                        //Console.WriteLine("no");
//                    }
//                }




//                return null;
//        }

//        bool PingIP(string ip, int port)
//        {
//            var client = new TcpClient();
//            if (!client.ConnectAsync(ip, port).Wait(10))
//            {
//                client.Close();
//                return false;
//            }
//            else
//            {
//                client.Close();
//                return true;
//            }

//        }
//    }
//}

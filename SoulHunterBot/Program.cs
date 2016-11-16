using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SoulHunterBot {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Init");

            Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse("52.7.233.168");
            System.Net.IPEndPoint remoteEP = new IPEndPoint(ipAdd, 10000);
            soc.Connect(remoteEP);

            //Start sending stuf..

            byte[] byData = StringToByteArray(Commands.GetGoldChest);
            soc.Send(byData);

            byte[] buffer = new byte[1024];
            int iRx = soc.Receive(buffer);
            char[] chars = new char[iRx];

            System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
            int charLen = d.GetChars(buffer, 0, iRx, chars, 0);
            System.String recv = new System.String(chars);
            Console.WriteLine(recv);


            Console.WriteLine("Finish");
            Console.ReadKey();
        }

        public static byte[] StringToByteArray(string hex) {
            var symbols = hex.Split(':');
            return
                BitConverter.GetBytes(symbols.Length).Reverse().Concat(
                    symbols.Select(x => Convert.ToByte(x, 16))).ToArray();
        }
    }

    public static class Commands {
        public const string GetGoldChest = "00:11:00:31:38:31:38:32:23:31:38:38:2d:32:38:32:32:33:36:36:61:39:5f:bf:0e:16:ce:52:9e:a9:76:dd:5b:cf:a7:67";
        public const string L1Sweep =      "00:11:00:31:38:31:38:32:23:31:38:38:2d:32:38:32:32:33:36:36:b1:4e:48:51:b1:36:16:dc:35:86:2a:63:c8:21:80:c0:c2:64:30:a6:97:30:4b:58:78:72:e7:c4:62:23:29:b9";
    }
}

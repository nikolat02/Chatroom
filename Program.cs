using ConsoleApplication1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatRoomClient
{
    class Program   //NameSpace ConsoleApplication1 is the ChatRoomClient side of the project
    {
        private static Stream stm;
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.Run(new ClientForm());
            try
            {
                Console.WriteLine("");
                Console.WriteLine("Please enter username");
                User clientUser;
                clientUser = new User(Console.ReadLine());
                TcpClient tcpclnt = new TcpClient();
                Console.WriteLine("Connecting.....");

                tcpclnt.Connect("192.168.101.9", 8001); //192.168.101.9
                // use the ipaddress as in the server program

                Console.WriteLine("Connected");

                stm = tcpclnt.GetStream();
                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(clientUser.UserName);
                stm.Write(ba, 0, ba.Length);

                Console.Write("Enter the string to be transmitted : ");

                String str = Console.ReadLine();
                Thread replyThread = new Thread(ReceiveReplies);
                replyThread.Start();
                while (str != "Exit")
                {

                    ba = asen.GetBytes(str);
                    Console.WriteLine("Transmitting.....");

                    stm.Write(ba, 0, ba.Length); //This writes "the wire"
                    str = Console.ReadLine();
                }

                tcpclnt.Close();
            }

            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }

        private static void ReceiveReplies()
        {
            byte[] bb = new byte[100];
            while(true)
            { 
            int k = stm.Read(bb, 0, 100);

            for (int i = 0; i < k; i++)
                Console.Write(Convert.ToChar(bb[i]));
            }
        }
    }
}

using System.Net;
using System.Net.Sockets;

namespace bai1._1
{
    internal class Program
    {
        static void ProcessMessage(object parm)
        {
            string data;
            int count;
            try
            {
                TcpClient tcpClient = parm as TcpClient;
                Byte[] bytes = new Byte[256];
                NetworkStream stream = tcpClient.GetStream();
                while ((count = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, count);
                    Console.WriteLine($"Received: {data} at {DateTime.Now:t}");
                    data = new string(data.Reverse().ToArray());
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                    stream.Write(msg, 0, msg.Length);
                    Console.WriteLine($"Sent: {data}");
                }
                tcpClient.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void ExecuteServer(string host, int port)
        {

            int Count = 0;
            TcpListener server = null;
            try
            {
                Console.Title = "Server Application";
                IPAddress ip = IPAddress.Parse(host);
                server = new TcpListener(ip, port);
                server.Start();
                Console.WriteLine("Waiting for a connection .. ");
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine($"Number of client connected: {++Count}");
                    Console.WriteLine();
                    Thread thread = new Thread(new ParameterizedThreadStart(ProcessMessage));
                    thread.Start(client);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                server.Stop();
                Console.WriteLine("Server stop");
            }
            Console.Read();
        }

        static void Main(string[] args)
        {
            string host = "10.87.64.48";
            int port = 5000;
            ExecuteServer(host, port);
        }
    }
}

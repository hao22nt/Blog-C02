using System.Net.Sockets;

namespace  bai1._2
{
    internal class Program
    {
        static void ConnectServer(string server, int port)
        {
            string mess, responseData;
            int bytes;
            try
            {
                TcpClient client = new TcpClient(server, port);
                Console.Title = "Client Application";
                NetworkStream stream = null;
                while (true)
                {
                    Console.WriteLine("Input mess");
                    mess = Console.ReadLine();
                    if (mess == null)
                    {
                        break;
                    }
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(mess);
                    stream = client.GetStream();
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine($"Sent: {mess}");
                    data = new Byte[256];
                    bytes = stream.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Console.WriteLine($"Recieved: {responseData}");
                }
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void Main(string[] args)
        {
            string host = "172.20.10.5";
            int port = 5000;
            ConnectServer(host, port);
        }
    }
}


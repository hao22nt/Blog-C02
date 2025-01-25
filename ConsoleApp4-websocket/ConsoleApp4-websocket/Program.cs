using System.Net.Sockets;
using System.Net;
using System.Text;

namespace ConsoleApp4_websocket;

    internal class Program
    {
        static List<TcpClient> clients = new List<TcpClient>();
        static object lockObj = new object();

        static void BroadcastMessage(string message, TcpClient excludeClient)
        {
            lock (lockObj)
            {
                foreach (var client in clients)
                {
                    if (client != excludeClient)
                    {
                        try
                        {
                            NetworkStream stream = client.GetStream();
                            byte[] data = Encoding.UTF8.GetBytes(message);
                            stream.Write(data, 0, data.Length);
                        }
                        catch
                        {
                            clients.Remove(client);
                        }
                    }
                }
            }
        }

        static void HandleClient(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];

                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Received: {message}");
                    BroadcastMessage(message, client);
                }
            }
            catch
            {
                Console.WriteLine("Client disconnected.");
            }
            finally
            {
                lock (lockObj)
                {
                    clients.Remove(client);
                }
                client.Close();
            }
        }

        static void Main(string[] args)
        {
            int port = 9000;
            TcpListener server = new TcpListener(IPAddress.Parse("192.168.228.29"), port);

            try
            {
                server.Start();
                Console.WriteLine($"Server started on port {port}.");

                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    lock (lockObj)
                    {
                        clients.Add(client);
                    }
                    Console.WriteLine("New client connected.");
                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                server.Stop();
            }
        }
    }

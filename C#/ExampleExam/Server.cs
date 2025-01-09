using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ExampleExam
{
    public class Server
    {
        private int PORT = 7531;
        private static PlayGroundRepository _repo = new PlayGroundRepository();
        public Server()
        {
            TcpListener server = new TcpListener(PORT);
            server.Start();
            while (true)
                Task.Run(() =>
                {
                    TcpClient socket = server.AcceptTcpClient();
                    HandleOneClient(socket);
                });
        }
        private void HandleOneClient(TcpClient socket)
        {
            StreamReader sr = new StreamReader(socket.GetStream());
            StreamWriter sw = new StreamWriter(socket.GetStream());
            while (true)
            {
                int age = int.Parse(sr.ReadLine());
                string output = "";
                List<PlayGround> list = _repo.GetAll().Where(elem => elem.MinAge <= age).ToList();
                output += JsonSerializer.Serialize(list);
                sw.WriteLine(output);
                sw.Flush();
                Console.WriteLine(output);
            }


        }
    }


}

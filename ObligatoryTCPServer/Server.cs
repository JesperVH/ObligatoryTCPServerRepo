using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using BookLibrary;
using Newtonsoft.Json;


namespace ObligatoryTCPServer
{
    public class Server
    {
        private static readonly List<Book> books = new List<Book>()
        {
            new Book("Codex Astartes", "Roboute Guilliman", 5917, "9412572156601"),
            new Book("The Kings Ranger", "John Flanagan", 465, "1234567890987")
        };
        public void Start(TcpListener serverSocket)
        {
            //TcpListener socket = new TcpListener("localhost", 4646);
            serverSocket.Start();
            Console.WriteLine("Server started");
        }

        public void DoClient(TcpListener listener)
        {

            TcpClient socket = listener.AcceptTcpClient();

            Console.WriteLine("Server activated");

            Stream ns = socket.GetStream();

            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;

            string line = sr.ReadLine();
            string answer = "";
            while (line != null && line != "")
            {
                string[] lineArray = line.Split(" ");
                string command = lineArray[0];
                string param = null;
                if (lineArray.Length > 1)
                {
                    param = lineArray[1];
                }
                
                switch(command)
                {
                    case "GetAll":
                        sw.WriteLine("GetAll");
                        sw.WriteLine(JsonConvert.SerializeObject(books));
                        break;

                    case "Get":
                        sw.WriteLine("Get");
                        
                        sw.WriteLine(JsonConvert.SerializeObject(books.FindAll(i => i.ISBN == param)));
                            break;

                    case "Save":
                        sw.WriteLine("Save");
                        Book book = JsonConvert.DeserializeObject<Book>(param);
                        books.Add(book);
                        break;

                    default:
                        sw.WriteLine("Not Accepted");
                        break;
                }
                line = sr.ReadLine();


            }
            ns.Close();
            socket.Close();
        }
    }
}

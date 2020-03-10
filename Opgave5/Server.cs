using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Opgave5
{

    public class Server
    {
        private static List<Bog> _bogliste = new List<Bog>()
        {
            new Bog() {Titel = "Programmering", Forfatter = "Claus Hedegaard", Isbn13 = "123456789123", Sidetal = 84},
            new Bog()
            {
                Titel = "Programmering 1.3", Forfatter = "Claus Hedegaard", Isbn13 = "123456789124", Sidetal = 100
            },
            new Bog()
            {
                Titel = "Programmering 1.1", Forfatter = "Claus Hedegaard", Isbn13 = "123456789125", Sidetal = 400
            },
            new Bog()
            {
                Titel = "Programmering 1.2", Forfatter = "Claus Hedegaard", Isbn13 = "123456789126", Sidetal = 900
            },
            new Bog()
            {
                Titel = "Programmering 1.3", Forfatter = "Claus Hedegaard", Isbn13 = "123456789127", Sidetal = 700
            }
        };

        public Server()
        {
            
        }

        public void DoClient(TcpClient socket)
        {
            using (socket)
            {
                NetworkStream ns = socket.GetStream();
                StreamReader sr = new StreamReader(ns);
                StreamWriter sw = new StreamWriter(ns);
                sw.AutoFlush = true;

                string line;
                while (true)
                {
                    line = sr.ReadLine();

                    Console.WriteLine("Client:" + line);

                    string[] stringSplit = line.Split(' ');

                    if (stringSplit[0] == "GetAll")
                    {
                        string bog = JsonConvert.SerializeObject(_bogliste);

                        Console.WriteLine(socket.Client.RemoteEndPoint + ": " + bog);

                        sw.WriteLine(bog);
                    }

                    if (stringSplit[0] == "GetByISBN13")
                    {
                        if (stringSplit.Length == 2)
                        {
                            Bog bogen = _bogliste.Find(bog => bog.Isbn13 == stringSplit[1]);
                            sw.WriteLine(JsonConvert.SerializeObject(bogen));
                        }
                    }

                    if (stringSplit[0] == "Save")
                    {
                        if (stringSplit.Length >= 2)
                        {
                            Bog nyBog = JsonConvert.DeserializeObject<Bog>(stringSplit[1]);
                            sw.WriteLine("Tilføjer ny bog ved navn:" + nyBog.Titel);
                            _bogliste.Add(nyBog);

                        }
                    }
                }
            }
        }

        public void Start()
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback, 4646);
            listener.Start();

            Console.WriteLine("Server startet");

            while (true)
            {
                TcpClient socket = listener.AcceptTcpClient();
                Console.WriteLine("Server er forbundet til en client");

                Task.Run(() =>
                {
                    TcpClient tempsocket = socket;
                    DoClient(tempsocket);
                });
            }
        }

    }
}
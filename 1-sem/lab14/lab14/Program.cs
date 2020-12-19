using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text.Json;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;

namespace lab14
{
    public class GameDescr
    {
        public string ShortDescription { get; set; }
        public string Symbol { get; set; }
        public string Developer { get; set; }
        public GameDescr(string shortDescr, string symbol, string developer)
        {
            ShortDescription = shortDescr;
            Symbol = symbol;
            Developer = developer;
        }
        public GameDescr()
        {

        }
        public void Log()
        {
            Console.WriteLine($"{ShortDescription}\nSymbol: {Symbol}\nDeveloped by: {Developer}");
        }
    }
    [Serializable]
    public class Game
    {
        public string Name { get; set; }
        public string Genre { set; get; }
        public int  Age { get; set; }
        public Game(string name, string genre, int age)
        {
            Name = name;
            Genre = genre;
            Age = age;
        }
        public Game()
        {

        }
        public virtual void Info()
        {
            Console.WriteLine($"Hi, I am a {Name} game. My genre is {Genre}, i am {Age} y.o.");
        }
    }
    [Serializable]
    public class GameWithVersions : Game
    {
        public string[] Versions { get; set; }
        public GameDescr Description { get; set; }
        public GameWithVersions()
        {

        }
        public GameWithVersions(string name, string genre, int age, GameDescr descr, params string[] versions) : base(name, genre, age)
        {
            Versions = versions;
            Description = descr;
        }
        public override void Info()
        {
            Console.WriteLine($"Hi, I am a {Name} game. My genre is {Genre}, i am {Age} y.o.\nMy versions are: {Versions}");
            Console.Write("Description: ");
            Description.Log();
        }
    }
    class Program
    {
        async static Task Main(string[] args)
        {
            Game[] games = new Game[]{
                new Game("Dota 2", "MMORPG", 10),
                new Game("Skyrim", "RPG", 6)
            };


            //BINARY SERIALIZATION
            BinaryFormatter MyFormatter = new BinaryFormatter();//Объект сериализатора

            using (FileStream InputStream = new FileStream("games.dat", FileMode.OpenOrCreate))//Поток для записи в файл
            {
                MyFormatter.Serialize(InputStream, games);//Метод Serialize принимает поток записи и объекты
            }

            using (FileStream Reader = new FileStream("games.dat", FileMode.OpenOrCreate))
            {
                Game[] gamesFromBinary = (Game[])MyFormatter.Deserialize(Reader);//Десериализуем из файла в потоке чтения
                foreach (var game in gamesFromBinary)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Aloha from Binary! ");
                    Console.ResetColor();
                    game.Info();
                }
            }


            //SOAP SERIALIZATION
            SoapFormatter MySoapFormatter = new SoapFormatter();

            using (FileStream SoapSerializer = new FileStream("games-soap.soap", FileMode.OpenOrCreate))
            {
                MySoapFormatter.Serialize(SoapSerializer, games);
            }

            using (FileStream SoapDeserializer = new FileStream("games-soap.soap", FileMode.Open))
            {
                Game[] gamesFromSoap = (Game[])MySoapFormatter.Deserialize(SoapDeserializer);
                foreach (var g in gamesFromSoap)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("Greetings from SOAP! ");
                    Console.ResetColor();
                    g.Info();
                }
            }


            //JSON SERIALIZATION
            GameWithVersions[] gamesJson = new GameWithVersions[] {
                new GameWithVersions("Minecraft", "Sandbox", 10, new GameDescr("Sandbox game", "box of earth", "Notch"), "1.0.0", "1.0.2", "1.1.0", "2.3.5"),
                new GameWithVersions("Terraria", "Platformer", 8, new GameDescr("Flat game", "T", null), "1.0.1", "1.2.0", "1.3.1")
            };

            string json = JsonSerializer.Serialize(gamesJson,
                 new JsonSerializerOptions { IgnoreNullValues = true, WriteIndented = true });//Метод возвращает строку в формате json

            using (StreamWriter JsonInput = new StreamWriter("game.json"))
            {
                JsonInput.WriteLine(json);
            }
            //Console.WriteLine(json);

            GameWithVersions[] gameFromJson;
            using (FileStream JsonReader = new FileStream("game.json", FileMode.Open))
            {
                gameFromJson = await JsonSerializer.DeserializeAsync<GameWithVersions[]>(JsonReader);
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Objects from JSON file");
            Console.ResetColor();
            foreach (var g in gameFromJson)
            {
                g.Info();
                Console.WriteLine();
            }


            //XML SERIALIZATION
            XmlSerializer MyXMLSerializer = new XmlSerializer(typeof(GameWithVersions[]));
            GameWithVersions[] gamesXml = new GameWithVersions[] {
                new GameWithVersions("GTA SA", "Quest", 8, new GameDescr("GTA Game", "Auto", "Rockstar"), "1.0.2", "1.2.2", "1.4.0"),
                new GameWithVersions("Mount&Blade", "Simulator", 7, new GameDescr("Medieval strategy", "Blade", "Paradox"), "1.0.0", "1.2.1", "1.3.0", "4.5.6")
            };
            //serialization
            using (FileStream InputXml = new FileStream("games.xml", FileMode.OpenOrCreate))
            {
                MyXMLSerializer.Serialize(InputXml, gamesXml);
            }

            //deserialization
            GameWithVersions[] gamesFromXml;
            using (FileStream XmlReader = new FileStream("games.xml", FileMode.Open))
            {
                gamesFromXml = (GameWithVersions[])MyXMLSerializer.Deserialize(XmlReader);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Objects from XML file");
            Console.ResetColor();
            foreach (var g in gamesFromXml)
            {
                g.Info();
                Console.WriteLine();
            }

            //XPath
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("XPath ");
            Console.ResetColor();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("games.xml");//Загружаем документ
            XmlElement xmlRoot = xmlDoc.DocumentElement;//получаем корневой узел

            XmlNodeList xmlElements = xmlRoot.SelectNodes("*");//Выбираем все дочерние узлы узла xmlRoot

            Console.WriteLine("Вывод всех игр");
            foreach (XmlNode node in xmlElements)
            {
                Console.WriteLine(node.OuterXml);
                Console.WriteLine();
            }

            Console.WriteLine("Вывод игр, чей разработчик Paradox");
            foreach (XmlNode node in xmlElements)
            {
                XmlNode developerParadox = node.SelectSingleNode("Description[Developer='Paradox']");
                if(developerParadox != null)
                {
                    Console.WriteLine(developerParadox.ParentNode.OuterXml);
                    Console.WriteLine();
                }
            }


            //Linq to XML
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Запросы Linq to XML");
            Console.ResetColor();
            XDocument xDoc = XDocument.Load("games-linq.xml");
            Console.WriteLine("Запрос на поиск разработчика Betheda");
            var query1 = from xNode in xDoc.Element("Games").Elements("Game")
                        where xNode.Element("Description").Element("Developer").Value == "Betheda"
                        select xNode;
            foreach (var item in query1)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Запрос на по возрасту игры 10 лет");
            var query2 = from xNode in xDoc.Element("Games").Elements("Game")
                         where xNode.Element("Age").Value == "10"
                         select xNode;
            foreach (var item in query2)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Запрос по жанру игры RPG");
            var query3 = (from xNode in xDoc.Element("Games").Elements("Game")
                         where xNode.Element("Genre").Value == "RPG"
                         select xNode).Take(1);
            foreach (var item in query3)
            {
                Console.WriteLine(item);
            }


            Console.Read();
        }
    }
}

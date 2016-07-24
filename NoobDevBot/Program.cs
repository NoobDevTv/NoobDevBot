using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace NoobDevBot
{
    class Program
    {
        static TelegramBotClient bot;

        static void Main(string[] args)
        {
            if (!File.Exists("key.txt"))
            {
                Console.WriteLine("Keyfile not Found /n Please pressed Key");
                Console.ReadKey();
                return;
            }

            CommandManager.Initialize();

            using (var reader = new StreamReader(File.OpenRead("key.txt")))
                bot = new TelegramBotClient(reader.ReadLine());



            bot.OnMessage += (s, e) => CommandManager.Throw(e.Message.Text, e);

            bot.StartReceiving();

            Console.ReadKey();
        }
    }
}

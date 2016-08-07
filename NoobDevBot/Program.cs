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
        public static TelegramBotClient Bot;

        static void Main(string[] args)
        {
            if (!File.Exists("key.txt"))
            {
                Console.WriteLine("Keyfile not Found /n Please pressed Key");
                Console.ReadKey();
                return;
            }
            
            DatabaseManager.Initialize();

            using (var reader = new StreamReader("key.txt"))
                Bot = new TelegramBotClient(reader.ReadLine());

            CommandManager.Initialize(Bot);
            
            Bot.OnMessage += (s, e) => CommandManager.DispatchAsync(e.Message.Text.Split().FirstOrDefault(f => f.StartsWith("/")), e);

            Bot.StartReceiving();

            Console.ReadKey();
        }
    }
}

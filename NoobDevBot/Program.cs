using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using File = System.IO.File;

namespace NoobDevBot
{
    class Program
    {

        public static TelegramBotClient Bot;

        static void Main(string[] args)
        {
            AutoResetEvent are = new AutoResetEvent(false);
            Logger.StartLog();

            if (!File.Exists("key.txt"))
            {
                Logger.Log("Keyfile not Found /n Please pressed Key");
                Console.ReadKey();
                return;
            }

            DatabaseManager.Initialize();

            using (var reader = new StreamReader("key.txt"))
                Bot = new TelegramBotClient(reader.ReadLine());

            CommandManager.Initialize(Bot);

            Bot.OnMessage += (s, e) => CommandManager.DispatchAsync(commandFromMessage(e.Message), e);

            Bot.StartReceiving();

            are.WaitOne();
            
            Logger.EndLog();
        }

        static string commandFromMessage(Message message)
        {
            var command = message?.Text?.Split().FirstOrDefault(f => f.StartsWith("/"));

            if (command == null)
                return null;

            var splitcont = command.ToLower().Split('@');

            if (splitcont.Length > 1 && !splitcont.Any(s => s == "noobdev_bot" || s == "noobdevbot"))
                return "null";

            return splitcont.FirstOrDefault();
        }
    }
}

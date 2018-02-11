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
using System.Data.Common;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace NoobDevBot
{
    class Program
    {
        public static BotCommandManager manager;
        public static TelegramBotClient Bot;

        private static void Main(string[] args)
        {
            AutoResetEvent are = new AutoResetEvent(false);
            Logger.StartLog();

            if (!File.Exists("key.txt"))
            {
                Logger.Log("Keyfile not Found /n Please pressed Key");
                Console.ReadKey();
                return;
            }
            string connectionString = "";

            if (File.Exists(@".\database.json"))
            {
                var dbInit = JsonConvert.DeserializeObject<DBSettings>(File.ReadAllText("database.json"));
                var conBuilder = new SqlConnectionStringBuilder
                {
                    DataSource = dbInit.Source,
                    InitialCatalog = dbInit.Database,
                    UserID = dbInit.User,
                    Password = dbInit.Password,
                    ConnectTimeout = dbInit.Timeout,
                    Encrypt = true
                };
                connectionString = conBuilder.ToString();
            }

            DatabaseManager.Initialize(connectionString);

            using (var reader = new StreamReader("key.txt"))
                Bot = new TelegramBotClient(reader.ReadLine());

            manager.Initialize(Bot);

            Bot.OnMessage += (s, e) => manager.DispatchAsync(CommandFromMessage(e.Message), e);
            Bot.OnInlineQuery += (s, e) => { };
            Bot.OnCallbackQuery += (s, e) => manager.DispatchAsync("", e);
            Bot.OnInlineResultChosen += (s, e) => { };

            Bot.StartReceiving();

            are.WaitOne();

            Logger.EndLog();
        }

        private static string CommandFromMessage(Message message)
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

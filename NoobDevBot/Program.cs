using System;
using System.Collections.Generic;
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
            CommandManager.Initialize();
            bot = new TelegramBotClient("232609616:AAGOJkDYQmSmUFhjToCt3JTtRnlWp3-TszE");

            bot.OnMessage += (s, e) => CommandManager.Throw(e.Message.Text, e);

            bot.StartReceiving();
            
            Console.ReadKey();
        }
    }
}

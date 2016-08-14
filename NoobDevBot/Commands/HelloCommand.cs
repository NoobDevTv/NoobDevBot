using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace NoobDevBot.Commands
{
    internal class HelloCommand
        : Command<MessageEventArgs, bool>
    {
        private TelegramBotClient telegramBotClient;
        private long id;

        public HelloCommand(TelegramBotClient telegramBot, long chatId)
        {
            NextFunction = WriteHello;
            telegramBotClient = telegramBot;
            id = chatId;
        }

        public bool WriteHello(MessageEventArgs e)
        {
            Console.WriteLine($"{e.Message.From.Username ?? e.Message.From.FirstName}: {e.Message.Text}");
            telegramBotClient.SendTextMessageAsync(id, $"Hallo {e.Message.From.Username ?? e.Message.From.FirstName}");
            NextFunction = null;
            RaiseFinishEvent(this, e);
            return true;
        }
    }
}

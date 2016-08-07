using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace NoobDevBot.Commands
{
    internal class NextStreamCommand
        : Command<MessageEventArgs, bool>
    {
        private long id;
        private TelegramBotClient telegramBot;

        public NextStreamCommand(TelegramBotClient telegramBot, long id)
        {
            this.telegramBot = telegramBot;
            this.id = id;

            NextFunction = SendNextStream;
        }

        private bool SendNextStream(MessageEventArgs e)
        {
            var stream = DatabaseManager.GetNextStream();
            
            string message;

            if (stream != null)
                message = $"Der Nächste Stream ist: {stream.title} am {stream.start} von {stream.user.name}";
            else
                message = "Leider konnte ich keinen Stream finden :(";
            
            telegramBot.SendTextMessageAsync(id, message);

            NextFunction = null;
            RaiseFinishEvent(this, e);

            return true;
        }
    }
}

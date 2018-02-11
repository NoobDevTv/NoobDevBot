using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace NoobDevBot.Commands
{
    [Command("/nextstream")]
    internal class NextStreamCommand
        : Command<MessageEventArgs, bool>
    {
        private long id;
        private streams nextStream;
        private TelegramBotClient telegramBot;

        public NextStreamCommand(TelegramBotClient telegramBot, long id)
        {
            this.telegramBot = telegramBot;
            this.id = id;
            nextStream = DatabaseManager.NextStream;

            NextFunction = SendNextStream;
        }

        private bool SendNextStream(MessageEventArgs e)
        {
            string message;

            if (nextStream != null)
                message = $"Der Nächste Stream ist: {nextStream.title} am {nextStream.START} von {nextStream.user.name}";
            else
                message = "Leider konnte ich keinen Stream finden :(";
            
            telegramBot.SendTextMessageAsync(id, message);

            NextFunction = null;
            Finished = true;
            RaiseFinishEvent(this, e);

            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace NoobDevBot.Commands
{
    [Command("/getrandomsmiley")]
    internal class GetRandomSmileyCommand
        : Command<MessageEventArgs, bool>
    {
        private long id;
        private TelegramBotClient telegramBot;

        public GetRandomSmileyCommand(TelegramBotClient telegramBot, long id)
        {
            this.telegramBot = telegramBot;
            this.id = id;

            NextFunction = getRandomSmiley;
        }

        private bool getRandomSmiley(MessageEventArgs arg)
        {
            var gen = new Random();

            var id = gen.Next(1, DatabaseManager.GetNumOfSmilies() + 1);
            
            telegramBot.SendTextMessageAsync(this.id, DatabaseManager.GetSmiley(id) );

            return true;
        }
    }
}

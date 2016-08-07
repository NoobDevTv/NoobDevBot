using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace NoobDevBot.Commands
{
    internal class DeleteStreamCommand
        : Command<MessageEventArgs, bool>
    {
        private long id;
        private TelegramBotClient telegramBot;

        public DeleteStreamCommand(TelegramBotClient telegramBot, long id)
        {
            this.telegramBot = telegramBot;
            this.id = id;
        }
    }
}

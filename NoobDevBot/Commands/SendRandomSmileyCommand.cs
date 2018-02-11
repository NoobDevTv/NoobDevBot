using System;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace NoobDevBot.Commands
{
    internal class SendRandomSmileyCommand
        : Command<MessageEventArgs, bool>
    {
        private long id;
        private TelegramBotClient telegramBot;

        public SendRandomSmileyCommand(TelegramBotClient telegramBot, long id)
        {
            this.telegramBot = telegramBot;
            this.id = id;

            NextFunction = SendRandomSmiley;
        }

        private bool SendRandomSmiley(MessageEventArgs arg)
        {
            var smiley = arg.Message.Text.Split().FirstOrDefault(a => a.StartsWith("@")).Substring(1);

            DatabaseManager.InsertNewSmiley(smiley);
            DatabaseManager.Submit();

            return true;
        }
    }
}
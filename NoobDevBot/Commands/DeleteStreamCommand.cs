﻿using System;
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
            AskUser("Bitte gebe die ID des zu löschenden Streams ein");
            NextFunction = DeleteTheStream;
        }

        public bool DeleteTheStream(MessageEventArgs e)
        {
            int id;
             
            if (int.TryParse(e.Message.Text, out id))
            {
                var stream = DatabaseManager.GetStreamById(id);
                if (DatabaseManager.DeleteStream(e.Message.From.Id, id))
                {
                    AskUser($"Stream {stream.title} wurde gelöscht");
                    DatabaseManager.Submit();
                    return true;
                }
            }

            AskUser("Stream wurde nicht gelöscht");

            return false;
        }

        private void AskUser(string text) => telegramBot.SendTextMessageAsync(id, text);
    }
}

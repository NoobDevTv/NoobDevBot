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
    [Command("/deletestream")]
    internal class DeleteStreamCommand
        : Command<MessageEventArgs, bool>
    {
        private long id;
        private TelegramBotClient telegramBot;

        public DeleteStreamCommand(TelegramBotClient telegramBot, long id)
        {
            this.telegramBot = telegramBot;
            this.id = id;
            NextFunction = AskUser;
        }

        public bool AskUser(MessageEventArgs e)
        {
            if (e.Message.Chat.Type == ChatType.Group || e.Message.Chat.Type == ChatType.Supergroup)
            {
                telegramBot.SendTextMessageAsync(e.Message.Chat.Id, "Diese Funktion ist in Gruppen nicht erlaubt");
                return false;
            }

            AskUser("Bitte gebe die ID des zu löschenden Streams ein");
            NextFunction = DeleteTheStream;
            return true;
        }
        
        public bool DeleteTheStream(MessageEventArgs e)
        {
            int tmpId;
             
            if (int.TryParse(e.Message.Text, out tmpId))
            {
                var stream = DatabaseManager.GetStreamById(tmpId);
                if (DatabaseManager.DeleteStream(e.Message.From.Id, tmpId))
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

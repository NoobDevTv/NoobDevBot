using NoobDevBot.Commands;
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
    [Command("/insertstream")]
    internal class InsertStreamCommand
        : Command<MessageEventArgs, bool>
    {
        private TelegramBotClient telegramBotClient;
        private long id;

        private int userId;
        private string streamTitle;
        private DateTime date;

        public InsertStreamCommand(TelegramBotClient telegramBot, long chatId)
        {
            NextFunction = CheckOrInsertUser;
            telegramBotClient = telegramBot;
            id = chatId;
        }

        public bool CheckOrInsertUser(MessageEventArgs e)
        {
            if (e.Message.Chat.Type == ChatType.Group || e.Message.Chat.Type == ChatType.Supergroup)
            {
                telegramBotClient.SendTextMessageAsync(e.Message.Chat.Id,
                    "Diese Funktion ist in Gruppen nicht erlaubt");
                return false;
            }

            var user = DatabaseManager.GetUser(e.Message.From.Id);

            if (!RightManager.CheckRight("insert_allowed", user.id))
            {
                AskUser($"Tut mir leid {e.Message.From.FirstName} du hast leider nicht genügend Rechte");
                return false;
            }

            userId = user.id;

            AskUser("Wie lautet der Titel deines Streams?");

            NextFunction = GetTitleFromUser;

            return true;
        }

        private void AskUser(string text) => telegramBotClient.SendTextMessageAsync(id, text);

        public bool GetTitleFromUser(MessageEventArgs e)
        {
            streamTitle = e.Message.Text;

            AskUser("Wann findet dein Stream statt?");

            NextFunction = GetDateFromUser;

            return true;
        }

        public bool GetDateFromUser(MessageEventArgs e)
        {
            if (DateTime.TryParse(e.Message.Text, out date))
            {
                insertStream(e);
                NextFunction = null;
                return true;
            }

            return false;
        }

        private void insertStream(MessageEventArgs e)
        {
            var stream = DatabaseManager.InsertNewStream(userId, date, streamTitle);

            AskUser($"Dein Stream wurde erfolgreich eingetragen. Die ID ist {stream.id}");

            RaiseFinishEvent(this, e);
            NextFunction = null;
            Finished = true;
        }
    }
}

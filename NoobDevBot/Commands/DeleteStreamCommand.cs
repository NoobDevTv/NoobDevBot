using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace NoobDevBot.Commands
{
    [Command("/deletestream")]
    internal class DeleteStreamCommand
        : Command<MessageEventArgs, CallbackQueryEventArgs, bool>
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

            sendStreams(e);
            NextFunction = AskUser;
            return true;
        }
        
        public bool DeleteTheStream(CallbackQueryEventArgs e)
        {
            int tmpId;
             
            if (int.TryParse(e.CallbackQuery.Data, out tmpId))
            {
                var stream = DatabaseManager.GetStreamById(tmpId);
                if (DatabaseManager.DeleteStream(e.CallbackQuery.From.Id, tmpId))
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

        private void sendStreams(MessageEventArgs arg)
        {
            var mark = new InlineKeyboardMarkup();

            var streams = DatabaseManager.GetUserStreams(arg.Message.From.Id);
            //mark.InlineKeyboard = new InlineKeyboardButton[streams.Count][];

            for (int i = 0; i < streams.Count; i++)
            {
                //mark.InlineKeyboard[i] = new InlineKeyboardButton[1];

                //var button = new InlineKeyboardButton(
                //    $"id:{streams[i].id}, Title: {streams[i].title}, Datum: {streams[i].start}", $"{streams[i].id}");
                //mark.InlineKeyboard[i][0] = button;
            }

            telegramBot.SendTextMessageAsync(id, "Deine Streams:", replyMarkup: mark);
           

            WaitForQuery(DeleteTheStream, id);
        }
    }
}

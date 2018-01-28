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
    [Command("/mystreams", "/ms")]
    public class MyStreamsCommand : Command<MessageEventArgs, CallbackQueryEventArgs, bool>
    {
        private long id;
        private TelegramBotClient telegramBotClient;

        public MyStreamsCommand(TelegramBotClient telegramBot, long chatId)
        {
            NextFunction = getStreams;
            telegramBotClient = telegramBot;
            id = chatId;
        }


        private bool getStreams(MessageEventArgs arg)
        {
            if (arg.Message.Chat.Type == ChatType.Group || arg.Message.Chat.Type == ChatType.Supergroup)
            {
                telegramBotClient.SendTextMessageAsync(arg.Message.Chat.Id,
                    "Diese Funktion ist in Gruppen nicht erlaubt");
                return false;
            }

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

            telegramBotClient.SendTextMessageAsync(id, "Deine Streams:", replyMarkup: mark);

            WaitForQuery(answer, id);

            return true;
        }

        private bool answer(CallbackQueryEventArgs args)
        {
            Console.WriteLine(args.CallbackQuery.Data);
            
            return true;
        }

    }
}

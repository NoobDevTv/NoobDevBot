using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace NoobDevBot
{
    public static class CommandManager
    {
        private static CommandHandler<MessageEventArgs, bool> commandHandler;
        private static TelegramBotClient telegramBot;

        public static void Initialize(TelegramBotClient telegramBot)
        {
            commandHandler = new CommandHandler<MessageEventArgs, bool>();
            CommandManager.telegramBot = telegramBot;

            commandHandler["/hello"] += (e) => hello(e);
            commandHandler["/Hello"] += (e) => hello(e);
            commandHandler["/nextStream"] += (e) => nextStream(e);
            //commandHandler["/insertStream"] += (e) => insertStream(e);
            //commandHandler["/deleteStream"] += (e) => deleteStream(e);
 
        }
        
        public static bool Throw(string commandName, MessageEventArgs e)
        {
            if (commandHandler.CommandExists(commandName ?? ""))
                return commandHandler.Throw(commandName, e);

            return false;
        }

        private static bool deleteStream(MessageEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static bool insertStream(MessageEventArgs e)
        {
            if (!DatabaseManager.UserExists(e.Message.From.Id))
                DatabaseManager.SaveNewUser(e.Message.From, false);
            else
            {
              var user = DatabaseManager.GetUser(e.Message.From.Id);

                if (!user.insertStreamAllowed.Value)
                    return false;
            }

            var array = e.Message.Text.Split(',');

            DateTime dt = new DateTime();
            string title = "";

            foreach (var parameter in array)
            {
                var tmp = parameter.Split('=');
                var command = tmp[0];
                var value = tmp[1];

                switch (command)
                {
                    case "s":
                        var dtArray = value.Split('.');
                        dt = new DateTime(int.Parse(dtArray[2]),int.Parse(dtArray[1]), int.Parse(dtArray[0]));
                        break;
                    case "t":
                        title = value;
                        break;
                    default:
                        
                        break;
                }
            }

            DatabaseManager.InsertNewStream(e.Message.From.Id, dt, title);

            DatabaseManager.Submit();

            return true;
        }

        private static bool nextStream(MessageEventArgs e)
        {
            var stream = DatabaseManager.GetNextStream();

            var message = $"{stream.title} {stream.start} {stream.user.name}";

            telegramBot.SendTextMessageAsync(e.Message.Chat.Id, message);


            return true;
        }

        private static bool hello(MessageEventArgs e)
        {
            Console.WriteLine($"{e.Message.From.Username ?? e.Message.From.FirstName ?? e.Message.Text}: {e.Message.Text}");
            return true;
        }
    }
}

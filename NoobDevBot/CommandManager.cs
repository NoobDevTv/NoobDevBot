using NoobDevBot.Commands;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace NoobDevBot
{
    public static class CommandManager
    {
        private static CommandHandler<MessageEventArgs, bool> commandHandler;
        private static TelegramBotClient telegramBot;
        private static ConcurrentDictionary<User, Func<MessageEventArgs, bool>> commandDictionary;

        public static void Initialize(TelegramBotClient telegramBot)
        {
            commandHandler = new CommandHandler<MessageEventArgs, bool>();
            commandDictionary = new ConcurrentDictionary<User, Func<MessageEventArgs, bool>>();
            CommandManager.telegramBot = telegramBot;

            commandHandler["/hello"] += (e) => hello(e);
            commandHandler["/nextstream"] += (e) => nextStream(e);
            commandHandler["/insertstream"] += (e) => insertStream(e);
            //commandHandler["/deleteStream"] += (e) => deleteStream(e);

        }

        public static bool Dispatch(string commandName, MessageEventArgs e)
        {
            commandName = commandName.ToLower();
            Func<MessageEventArgs, bool> method;


            if (commandDictionary.TryGetValue(e.Message.From, out method))
                    return method(e);
            

            if (commandHandler.CommandExists(commandName ?? ""))
                return commandHandler.Dispatch(commandName, e);

            return false;
        }

        public static async Task<bool> DispatchAsync(string commandName, MessageEventArgs e)
        {
            return Dispatch(commandName, e);
        }

        private static bool deleteStream(MessageEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static bool insertStream(MessageEventArgs e)
        {

            var command = new InsertStreamCommand(telegramBot, e.Message.Chat.Id);
            if (!command.NextFunction(e))
                return false;

            command.FinishEvent += finishedCommand;

            return commandDictionary.TryAdd(e.Message.From, command.NextFunction);
                       
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
            var command = new HelloCommand();

            return command.NextFunction(e);
        }

        private static void finishedCommand(object sender, MessageEventArgs e)
        {
            Func<MessageEventArgs, bool> method;

            commandDictionary.TryRemove(e.Message.From, out method);
            
        }
    }
}

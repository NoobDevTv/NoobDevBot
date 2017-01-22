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
using Telegram.Bot.Types.Enums;
using System.Reflection;

namespace NoobDevBot
{
    public static class CommandManager
    {
        private static CommandHandler<MessageEventArgs, bool> commandHandler;
        private static TelegramBotClient telegramBot;
        private static ConcurrentDictionary<long, Func<MessageEventArgs, bool>> commandDictionary;
        private static ConcurrentDictionary<long, Func<CallbackQueryEventArgs, bool>> waitForInlineQuery;

        public static void Initialize(TelegramBotClient telegramBot)
        {
            commandHandler = new CommandHandler<MessageEventArgs, bool>();
            commandDictionary = new ConcurrentDictionary<long, Func<MessageEventArgs, bool>>();
            waitForInlineQuery = new ConcurrentDictionary<long, Func<CallbackQueryEventArgs, bool>>();

            RightManager.Initialize();
            CommandManager.telegramBot = telegramBot;


            var types = Assembly.GetExecutingAssembly().GetTypes();

            foreach (var item in types)
            {
                var command = item.GetCustomAttribute<CommandAttribute>();
                var rights = item.GetCustomAttributes<CommandRightAttribute>();

                if (command == null)
                    continue;

                commandHandler[command.Name] += (e) => initializeCommand(item, e);

                if (rights != null)
                    foreach (var right in rights)
                        RightManager.Add(right.Tag, right.NeededPower);
            }
        }

        public static bool Dispatch(string commandName, MessageEventArgs e)
        {
            if (commandName != null)
            {
                DatabaseManager.InsertUserIfNotExist(e.Message.From);
                Logger.Log($"User: {e.Message.From.Username ?? e.Message.From.FirstName} try to use {commandName}");
                Console.WriteLine($"User: {e.Message.From.Username ?? e.Message.From.FirstName} try to use {commandName}");
                commandName = commandName.ToLower();

                if (commandHandler.CommandExists(commandName))
                    return commandHandler.Dispatch(commandName, e);
            }
            else
            {
                Func<MessageEventArgs, bool> method;

                if (commandDictionary.TryGetValue(e.Message.Chat.Id, out method))
                    return method(e);
            }

            return false;
        }
        public static bool Dispatch(string commandName, CallbackQueryEventArgs e)
        {
            Func<CallbackQueryEventArgs, bool> method;
            if (waitForInlineQuery.TryRemove(e.CallbackQuery.From.Id, out method))
                return method(e);

            return false;
        }

        public static void DispatchAsync(string commandName, MessageEventArgs e) => Task.Run(() => Dispatch(commandName, e));
        public static void DispatchAsync(string commandName, CallbackQueryEventArgs e) => Task.Run(() => Dispatch(commandName, e));

        public static bool initializeCommand(Type commandType, MessageEventArgs e)
        {
            var command = (ICommand<MessageEventArgs, bool>)Activator.CreateInstance(
                                           commandType, telegramBot, e.Message.Chat.Id);

            command.FinishEvent += finishedCommand;

            commandDictionary.TryAdd(e.Message.Chat.Id, command.Dispatch);

            if (commandType.BaseType.GenericTypeArguments.Count() == 3)
            {
                var tmp = (Command<MessageEventArgs, CallbackQueryEventArgs, bool>)command;
                tmp.WaitForInlineQuery += tmp_WaitForInlineQuery;
                
            }

            return command.Dispatch(e);
        }

        private static void tmp_WaitForInlineQuery(object sender, Func<CallbackQueryEventArgs, bool> method)
        {
            var tmp = (long)sender;
            waitForInlineQuery.TryAdd(tmp, method);
        }

        private static void finishedCommand(object sender, MessageEventArgs e)
        {
            Func<MessageEventArgs, bool> method;

            commandDictionary.TryRemove(e.Message.Chat.Id, out method);

        }
    }
}

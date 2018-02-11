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
using CommandManagementSystem;
using CommandManagementSystem.Attributes;

namespace NoobDevBot
{
    [CommandManager(nameof(BotCommandManager))]
    public class BotCommandManager : CommandManager<string, MessageEventArgs, bool>
    {
        private TelegramBotClient telegramBot;
        private ConcurrentDictionary<long, Func<MessageEventArgs, bool>> commandDictionary;
        private ConcurrentDictionary<long, Func<CallbackQueryEventArgs, bool>> waitForInlineQuery;

        public void Initialize(TelegramBotClient telegramBot)
        {
            commandDictionary = new ConcurrentDictionary<long, Func<MessageEventArgs, bool>>();
            waitForInlineQuery = new ConcurrentDictionary<long, Func<CallbackQueryEventArgs, bool>>();

            RightManager.Initialize();
            this.telegramBot = telegramBot;
            
            var types = Assembly.GetExecutingAssembly().GetTypes();

            foreach (var item in types)
            {
                var command = item.GetCustomAttribute<CommandAttribute>();
                var rights = item.GetCustomAttributes<CommandRightAttribute>();

                if (command == null)
                    continue;

                commandHandler[(string)command.Tag] += (e) => InitializeCommand(item, e);

                if (rights != null)
                    foreach (var right in rights)
                        RightManager.Add(right.Tag, right.NeededPower);
            }
        }

        public override bool Dispatch(string commandName, MessageEventArgs e)
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

                if (commandDictionary.TryGetValue(e.Message.Chat.Id, out Func<MessageEventArgs, bool> method))
                    return method(e);
            }

            return false;
        }
        public bool Dispatch(string commandName, CallbackQueryEventArgs e)
        {
            if (waitForInlineQuery.TryRemove(e.CallbackQuery.From.Id, out Func<CallbackQueryEventArgs, bool> method))
                return method(e);

            return false;
        }

        public override Task<bool> DispatchAsync(string commandName, MessageEventArgs e) => Task.Run(() => Dispatch(commandName, e));
        public void DispatchAsync(string commandName, CallbackQueryEventArgs e) => Task.Run(() => Dispatch(commandName, e));

        public override bool InitializeCommand(Type commandType, MessageEventArgs e)
        {
            var command = (ICommand<MessageEventArgs, bool>)Activator.CreateInstance(
                                           commandType, telegramBot, e.Message.Chat.Id);

            command.FinishEvent += FinishedCommand;

            commandDictionary.TryAdd(e.Message.Chat.Id, command.Dispatch);

            if (commandType.BaseType.GenericTypeArguments.Count() == 3)
            {
                var tmp = (Command<MessageEventArgs, CallbackQueryEventArgs, bool>)command;
                tmp.WaitForInlineQuery += TmpWaitForInlineQuery;
            }

            return command.Dispatch(e);
        }

        private void TmpWaitForInlineQuery(object sender, Func<CallbackQueryEventArgs, bool> method) 
            => waitForInlineQuery.TryAdd((long)sender, method);

        private void FinishedCommand(object sender, MessageEventArgs e) 
            => commandDictionary.TryRemove(key: e.Message.Chat.Id, value: out Func<MessageEventArgs, bool> method);
    }
}

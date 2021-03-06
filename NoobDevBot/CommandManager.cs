﻿using NoobDevBot.Commands;
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

namespace NoobDevBot
{
    public static class CommandManager
    {
        private static CommandHandler<MessageEventArgs, bool> commandHandler;
        private static TelegramBotClient telegramBot;
        private static ConcurrentDictionary<long, Func<MessageEventArgs, bool>> commandDictionary;

        public static void Initialize(TelegramBotClient telegramBot)
        {
            commandHandler = new CommandHandler<MessageEventArgs, bool>();
            commandDictionary = new ConcurrentDictionary<long, Func<MessageEventArgs, bool>>();
            RightManager.Initialize();
            CommandManager.telegramBot = telegramBot;

            commandHandler["/hello"] += (e) => hello(e);
            commandHandler["/nextstream"] += (e) => nextStream(e);
            commandHandler["/insertstream"] += (e) => insertStream(e);
            commandHandler["/deletestream"] += (e) => deleteStream(e);
            commandHandler["/getrandomsmiley"] += (e) => getRandomSmiley(e);
            //commandHandler["/sendsmiley"] += (e) => sendRandomSmiley(e);

            RightManager.Add("insert_allowed", 100);
            RightManager.Add("delete_allowed", 100);
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

        public static async Task<bool> DispatchAsync(string commandName, MessageEventArgs e)
        {
            return await Task.Run(() => Dispatch(commandName, e));
        }

        private static bool deleteStream(MessageEventArgs e)
        {
            if (e.Message.Chat.Type == ChatType.Group || e.Message.Chat.Type == ChatType.Supergroup)
            {
                telegramBot.SendTextMessageAsync(e.Message.Chat.Id, "Diese Funktion ist in Gruppen nicht erlaubt");
                return false;
            }

            var command = new DeleteStreamCommand(telegramBot, e.Message.Chat.Id);


            command.FinishEvent += finishedCommand;

            return commandDictionary.TryAdd(e.Message.Chat.Id, command.Dispatch);
        }

        private static bool insertStream(MessageEventArgs e)
        {
            if (e.Message.Chat.Type == ChatType.Group || e.Message.Chat.Type == ChatType.Supergroup)
            {
                telegramBot.SendTextMessageAsync(e.Message.Chat.Id, "Diese Funktion ist in Gruppen nicht erlaubt");
                return false;
            }

            var command = new InsertStreamCommand(telegramBot, e.Message.Chat.Id);

            command.FinishEvent += finishedCommand;

            command.NextFunction(e);

            return commandDictionary.TryAdd(e.Message.Chat.Id, command.Dispatch);

        }

        private static bool nextStream(MessageEventArgs e)
        {
            var command = new NextStreamCommand(telegramBot, e.Message.Chat.Id);

            return command.Dispatch(e);
        }

        private static bool hello(MessageEventArgs e)
        {
            var command = new HelloCommand(telegramBot, e.Message.Chat.Id);

            return command.Dispatch(e);
        }

        private static bool getRandomSmiley(MessageEventArgs e)
        {
            var command = new GetRandomSmileyCommand(telegramBot, e.Message.Chat.Id);

            return command.Dispatch(e);
        }

        private static bool sendRandomSmiley(MessageEventArgs e)
        {
            var command = new SendRandomSmileyCommand(telegramBot, e.Message.Chat.Id);

            return command.Dispatch(e);
        }

        private static void finishedCommand(object sender, MessageEventArgs e)
        {
            Func<MessageEventArgs, bool> method;

            commandDictionary.TryRemove(e.Message.Chat.Id, out method);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace NoobDevBot
{
    public static class CommandManager
    {
        private static CommandHandler<MessageEventArgs, bool> commandHandler;

        public static void Initialize()
        {
            commandHandler = new CommandHandler<MessageEventArgs, bool>();

            commandHandler["/hello"] += (e) => TestMethod(e);
            commandHandler["/Hello"] += (e) => TestMethod(e);
            commandHandler["/Hello"] += (e) => TestMethod2(e);

        }

        private static bool TestMethod2(MessageEventArgs e)
        {
            Console.WriteLine("Nööp");
            return true;
        }

        public static bool Throw(string commandName, MessageEventArgs e)
        {
            if (commandHandler.CommandExists(commandName ?? ""))
                return commandHandler.Throw(commandName, e);

            return false;
        }

        private static bool TestMethod(MessageEventArgs e)
        {
            Console.WriteLine($"{e.Message.From.Username ?? e.Message.From.FirstName ?? e.Message.Text}: {e.Message.Text}");
            return true;
        }
    }
}

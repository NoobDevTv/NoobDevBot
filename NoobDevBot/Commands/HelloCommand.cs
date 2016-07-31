using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace NoobDevBot.Commands
{
    internal class HelloCommand
        : Command<MessageEventArgs, bool>
    {

        public HelloCommand()
        {
            NextFunction = WriteHello;
        }

        public bool WriteHello(MessageEventArgs e) {
            Console.WriteLine($"{e.Message.From.Username ?? e.Message.From.FirstName ?? e.Message.Text}: {e.Message.Text}");
            return true;
        }
    }
}

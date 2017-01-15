using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobDevBot.Commands
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class CommandRightAttribute : Attribute
    {
        public readonly string Tag;
        public readonly byte NeededPower;

        public CommandRightAttribute(string tag, byte neededPower)
        {
            Tag = tag;
            NeededPower = neededPower;
        }
    }
}

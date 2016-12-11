using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NoobDevBot.Commands
{
    [AttributeUsage(AttributeTargets.Class,Inherited = true)]
    public class CommandAttribute : Attribute
    {
        public readonly string Name;

        public readonly string[] Aliases;

        public  byte NeededPower { get; set; }

        public CommandAttribute(string name, params string[] aliases)
        {
            Name = name;
            Aliases = aliases;
        }
    }
}

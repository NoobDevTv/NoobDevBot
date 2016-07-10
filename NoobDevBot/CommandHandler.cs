using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobDevBot
{
    public class CommandHandler<Tin, Tout>
    {
        private Dictionary<string, Func<Tin, Tout>> mainDictionary;
        
        public CommandHandler()
        {
            mainDictionary = new Dictionary<string, Func<Tin, Tout>>();
        }

        public Func<Tin, Tout> this[string commandName]
        {
            get
            {
                Func<Tin, Tout> value;
                mainDictionary.TryGetValue(commandName, out value);
                return value;
            }
            set
            {
                if (mainDictionary.ContainsKey(commandName))
                    mainDictionary[commandName] = value;
                else
                    mainDictionary.Add(commandName, value);

            }
        }

        public Tout Throw(string commandName, Tin parameter) => mainDictionary[commandName](parameter);

        public bool CommandExists(string commandName) => mainDictionary.ContainsKey(commandName);
    }
}

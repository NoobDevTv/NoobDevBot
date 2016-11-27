using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Installer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start Working");
            NoobBotDatabaseDataContext context = new NoobBotDatabaseDataContext();

            var table = context.GetTable<smilies>();
            string[] content;

            using (var reader = new StreamReader(File.OpenRead("Smileys.txt")))
            {
                content = reader.ReadToEnd().Split(Environment.NewLine.ToArray(),
                    StringSplitOptions.RemoveEmptyEntries);
            }

            Console.WriteLine("Get a Coffee");

            foreach (var item in content)
            {
                table.InsertOnSubmit(new smilies { unicode = item.Substring(2) });
            }

            context.SubmitChanges();
            Console.WriteLine("Please enter enter");
            Console.ReadLine();
        }
    }
}

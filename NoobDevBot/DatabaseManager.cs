using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace NoobDevBot
{
    public static class DatabaseManager
    {
        static NoobBotDatabaseDataContext context;

        public static void Initialize()
        {
            context = new NoobBotDatabaseDataContext();
        }

        public static bool UserExists(int id) => context.GetTable<user>().Any(u => u.id == id);

        public static void SaveNewUser(User user, bool streamInsertAllowed)
        {
            var table = context.GetTable<user>();

            var tempUser = new user
            {
                id = user.Id,
                name = string.IsNullOrWhiteSpace(user.Username) ? user.FirstName : user.Username,
                insertStreamAllowed = streamInsertAllowed
            };

            table.InsertOnSubmit(tempUser);
        }

        public static void InsertNewStream(int user, DateTime dateTime, string name)
        {
            var table = context.GetTable<streams>();

            var tempStream = new streams
            {
                userId = user,
                start = dateTime,
                title = name
            };

            table.InsertOnSubmit(tempStream);
        }

        public static streams GetNextStream()
        {
            var table = context.GetTable<streams>();
            return table.Where(s => s.start > DateTime.UtcNow).OrderBy(s => s.start).First();
        }

        public static user GetUser(int id) => context.GetTable<user>().First(u => u.id == id);

        public static void Submit() => context.SubmitChanges();


    }
}

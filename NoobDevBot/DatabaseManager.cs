using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace NoobDevBot
{
    public static class DatabaseManager
    {
        public static streams NextStream { get; private set; }

        private static NoobBotDatabaseDataContext context;

        public static void Initialize(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                context = new NoobBotDatabaseDataContext();
            else
                context = new NoobBotDatabaseDataContext(connectionString);

            GetNextStream();
        }

        public static bool UserExists(int id) => context.GetTable<user>().Any(u => u.id == id);

        public static user InsertUserIfNotExist(User user)
        {
            if (!TryGetUser(user.Id, out user tmpUser))
            {
                SaveNewUser(user);
                Submit();
            }

            return tmpUser;
        }

        public static void SaveNewUser(User user)
        {
            var table = context.GetTable<user>();

            var tempUser = new user
            {
                id = user.Id,
                name = string.IsNullOrWhiteSpace(user.Username) ? user.FirstName : user.Username,
            };

            table.InsertOnSubmit(tempUser);
        }

        public static streams InsertNewStream(int user, DateTime dateTime, string name)
        {
            var table = context.GetTable<streams>();

            var tempStream = new streams
            {
                userId = user,
                START = dateTime,
                title = name
            };

            table.InsertOnSubmit(tempStream);
            Submit();
            GetNextStream();
            return table.FirstOrDefault(s => s.userId == user && s.START == dateTime && s.title == name);
        }

        internal static void InsertNewSmiley(string smiley)
        {
            //context.GetTable<smilies>().InsertOnSubmit(new smilies { unicode = smiley});
        }

        public static bool DeleteStream(int user, int id)
        {
            var table = context.GetTable<streams>();
            var stream = table.FirstOrDefault(s => s.id == id && s.userId == user);

            if (stream == null)
                return false;

            table.DeleteOnSubmit(stream);
            return true;
        }

        public static void GetNextStream()
        {
            Table<streams> table;
            try
            {
                table = context.GetTable<streams>();
            }
            catch (Exception e)
            {
                Console.WriteLine("Irgend so ein fehler");
                Logger.Log(e.Message, 2);
                return;
            }

            NextStream = table?.Where(s => s.START > DateTime.UtcNow)?.OrderBy(s => s.START)?.FirstOrDefault();
        }

        public static streams GetStreamById(int id)
        {
            Table<streams> table;
            try
            {
                table = context.GetTable<streams>();
            }
            catch (Exception e)
            {
                Console.WriteLine("Irgend so ein fehler");
                Logger.Log(e.Message, 2);
                return null;
            }

            return table?.Where(s => s.id == id)?.FirstOrDefault();
        }

        public static List<streams> GetUserStreams(int id) => GetUser(id).streams.ToList();

        public static user GetUser(int id) => context.GetTable<user>().First(u => u.id == id);

        public static bool TryGetUser(int id, out user user)
        {
            user = context.GetTable<user>().FirstOrDefault(u => u.id == id);

            return user == null ? false : true;
        }

        public static void Submit() => context.SubmitChanges();

        public static List<groups_relation> GetGroups(int user_id) =>
            context.GetTable<groups_relation>().Where(r => r.user_id == user_id).ToList();

        public static byte? GetPower(string id, int reference, bool is_Group) =>
            context.GetTable<rights_relation>().FirstOrDefault(
                r => r.is_group == is_Group && r.reference == reference && r.right_id == id)?.POWER;

        public static string GetSmiley(int id)
        {
            //Von TheBlubb14 
            return char.ConvertFromUtf32(int.Parse(
                context.GetTable<smilies>().FirstOrDefault(s => s.id == id).unicode,
                NumberStyles.HexNumber));
        }

        public static int GetNumOfSmilies() => context.GetTable<smilies>().Count();
    }
}

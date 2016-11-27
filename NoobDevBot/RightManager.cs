using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobDevBot
{
    public static class RightManager
    {
        static Dictionary<string, int> rights;

        public static void Initialize()
        {
            rights = new Dictionary<string, int>();
        }

        public static void Add(string id, byte power) => rights.Add(id, power);

        public static bool CheckRight(string id, int user_id)
        {
            if (!DatabaseManager.UserExists(user_id))
                return false;

            int needed_power;
            if (!rights.TryGetValue(id, out needed_power))
                return false;

            byte? power = DatabaseManager.GetPower(id, user_id, false);

            foreach (var group in DatabaseManager.GetGroups(user_id))
            {
                var p = DatabaseManager.GetPower(id, group.group_id, true);

                if (p > power)
                    power = p;
            }

            return (power ?? 0) >= needed_power ;
        }
    }
}

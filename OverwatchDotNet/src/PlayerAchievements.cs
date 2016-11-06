using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverwatchAPI
{
    public class PlayerAchievements
    {
        public Dictionary<string, bool> General;
        public Dictionary<string, bool> Offense;
        public Dictionary<string, bool> Defense;
        public Dictionary<string, bool> Tank;
        public Dictionary<string, bool> Support;
        public Dictionary<string, bool> Maps;



        public PlayerAchievements()
        {

        }
    }
}

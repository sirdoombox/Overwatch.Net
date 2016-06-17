using AngleSharp;
using AngleSharp.Dom;
using OverwatchDotNet.Internal;
using System.Linq;
using System.Threading.Tasks;
using System;
using OverwatchDotNet.Core;

namespace OverwatchDotNet.OverwatchData
{
    public class CombatStats : IStatModule
    {
        [OverwatchStat("Melee Final Blows")]
        public int MeleeFinalBlows { get; private set; }

        [OverwatchStat("Solo Kills")]
        public int SoloKills { get; private set; }

        [OverwatchStat("Objective Kills")]
        public int ObjectiveKills { get; private set; }

        [OverwatchStat("Final Blows")]
        public int FinalBlows { get; private set; }

        [OverwatchStat("Damage Done")]
        public int DamageDone { get; private set; }

        [OverwatchStat("Eliminations")]
        public int Eliminations { get; private set; }

        [OverwatchStat("Environmental Kills")]
        public int EnvironmentKills { get; private set; }

        [OverwatchStat("Multikills")]
        public int Multikills { get; private set; }
        
        public void PopulateFromTableData(OverwatchDataTable dataTable)
        {
            
        }
    }
}

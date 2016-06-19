using OverwatchAPI.Internal;
using System;
using System.Collections.Generic;

//-- A U T O   G E N E R A T E D --//

namespace OverwatchAPI.Data
{
	public class AllHeroes : IStatGroup
	{
		public CombatStats Combat { get; private set; }
		public AssistsStats Assists { get; private set; }
		public BestStats Best { get; private set; }
		public AverageStats Average { get; private set; }
		public DeathsStats Deaths { get; private set; }
		public MatchAwardsStats MatchAwards { get; private set; }
		public GameStats Game { get; private set; }
		public MiscellaneousStats Miscellaneous { get; private set; }

		public void SendPage(IEnumerable<OverwatchDataTable> tableCollection)
		{
			foreach(var item in tableCollection)
			{
				var prop = GetType().GetProperty(item.Name);
				if (typeof(IStatModule).IsAssignableFrom(prop.GetType()))
				{
					IStatModule statModule = (IStatModule)Activator.CreateInstance(prop.GetType());
					statModule.SendTable(item);
					prop.SetValue(this, statModule);
				}
			}
		}

		public class CombatStats : IStatModule
		{
			public int MeleeFinalBlows { get; private set; }
			public int SoloKills { get; private set; }
			public int ObjectiveKills { get; private set; }
			public int FinalBlows { get; private set; }
			public int DamageDone { get; private set; }
			public int Eliminations { get; private set; }
			public int EnvironmentalKills { get; private set; }
			public int Multikills { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				MeleeFinalBlows = table.Stats["Melee Final Blows"].OWValToInt();
				SoloKills = table.Stats["Solo Kills"].OWValToInt();
				ObjectiveKills = table.Stats["Objective Kills"].OWValToInt();
				FinalBlows = table.Stats["Final Blows"].OWValToInt();
				DamageDone = table.Stats["Damage Done"].OWValToInt();
				Eliminations = table.Stats["Eliminations"].OWValToInt();
				EnvironmentalKills = table.Stats["Environmental Kills"].OWValToInt();
				Multikills = table.Stats["Multikills"].OWValToInt();
			}
		}

		public class AssistsStats : IStatModule
		{
			public int HealingDone { get; private set; }
			public int ReconAssists { get; private set; }
			public int TeleporterPadsDestroyed { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				HealingDone = table.Stats["Healing Done"].OWValToInt();
				ReconAssists = table.Stats["Recon Assists"].OWValToInt();
				TeleporterPadsDestroyed = table.Stats["Teleporter Pads Destroyed"].OWValToInt();
			}
		}

		public class BestStats : IStatModule
		{
			public int EliminationsMostinGame { get; private set; }
			public int FinalBlowsMostinGame { get; private set; }
			public int DamageDoneMostinGame { get; private set; }
			public int HealingDoneMostinGame { get; private set; }
			public int DefensiveAssistsMostinGame { get; private set; }
			public int OffensiveAssistsMostinGame { get; private set; }
			public int ObjectiveKillsMostinGame { get; private set; }
			public TimeSpan ObjectiveTimeMostinGame { get; private set; }
			public int MultikillBest { get; private set; }
			public int SoloKillsMostinGame { get; private set; }
			public TimeSpan TimeSpentonFireMostinGame { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				EliminationsMostinGame = table.Stats["Eliminations - Most in Game"].OWValToInt();
				FinalBlowsMostinGame = table.Stats["Final Blows - Most in Game"].OWValToInt();
				DamageDoneMostinGame = table.Stats["Damage Done - Most in Game"].OWValToInt();
				HealingDoneMostinGame = table.Stats["Healing Done - Most in Game"].OWValToInt();
				DefensiveAssistsMostinGame = table.Stats["Defensive Assists - Most in Game"].OWValToInt();
				OffensiveAssistsMostinGame = table.Stats["Offensive Assists - Most in Game"].OWValToInt();
				ObjectiveKillsMostinGame = table.Stats["Objective Kills - Most in Game"].OWValToInt();
				ObjectiveTimeMostinGame = table.Stats["Objective Time - Most in Game"].OWValToTimeSpan();
				MultikillBest = table.Stats["Multikill - Best"].OWValToInt();
				SoloKillsMostinGame = table.Stats["Solo Kills - Most in Game"].OWValToInt();
				TimeSpentonFireMostinGame = table.Stats["Time Spent on Fire - Most in Game"].OWValToTimeSpan();
			}
		}

		public class AverageStats : IStatModule
		{
			public float MeleeFinalBlowsAverage { get; private set; }
			public float FinalBlowsAverage { get; private set; }
			public TimeSpan TimeSpentonFireAverage { get; private set; }
			public float SoloKillsAverage { get; private set; }
			public TimeSpan ObjectiveTimeAverage { get; private set; }
			public float ObjectiveKillsAverage { get; private set; }
			public int HealingDoneAverage { get; private set; }
			public float DeathsAverage { get; private set; }
			public int DamageDoneAverage { get; private set; }
			public float EliminationsAverage { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				MeleeFinalBlowsAverage = table.Stats["Melee Final Blows - Average"].OWValToFloat();
				FinalBlowsAverage = table.Stats["Final Blows - Average"].OWValToFloat();
				TimeSpentonFireAverage = table.Stats["Time Spent on Fire - Average"].OWValToTimeSpan();
				SoloKillsAverage = table.Stats["Solo Kills - Average"].OWValToFloat();
				ObjectiveTimeAverage = table.Stats["Objective Time - Average"].OWValToTimeSpan();
				ObjectiveKillsAverage = table.Stats["Objective Kills - Average"].OWValToFloat();
				HealingDoneAverage = table.Stats["Healing Done - Average"].OWValToInt();
				DeathsAverage = table.Stats["Deaths - Average"].OWValToFloat();
				DamageDoneAverage = table.Stats["Damage Done - Average"].OWValToInt();
				EliminationsAverage = table.Stats["Eliminations - Average"].OWValToFloat();
			}
		}

		public class DeathsStats : IStatModule
		{
			public int Deaths { get; private set; }
			public int EnvironmentalDeaths { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				Deaths = table.Stats["Deaths"].OWValToInt();
				EnvironmentalDeaths = table.Stats["Environmental Deaths"].OWValToInt();
			}
		}

		public class MatchAwardsStats : IStatModule
		{
			public int Cards { get; private set; }
			public int Medals { get; private set; }
			public int MedalsGold { get; private set; }
			public int MedalsSilver { get; private set; }
			public int MedalsBronze { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				Cards = table.Stats["Cards"].OWValToInt();
				Medals = table.Stats["Medals"].OWValToInt();
				MedalsGold = table.Stats["Medals - Gold"].OWValToInt();
				MedalsSilver = table.Stats["Medals - Silver"].OWValToInt();
				MedalsBronze = table.Stats["Medals - Bronze"].OWValToInt();
			}
		}

		public class GameStats : IStatModule
		{
			public int GamesWon { get; private set; }
			public int GamesPlayed { get; private set; }
			public TimeSpan TimeSpentonFire { get; private set; }
			public TimeSpan ObjectiveTime { get; private set; }
			public int Score { get; private set; }
			public TimeSpan TimePlayed { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				GamesWon = table.Stats["Games Won"].OWValToInt();
				GamesPlayed = table.Stats["Games Played"].OWValToInt();
				TimeSpentonFire = table.Stats["Time Spent on Fire"].OWValToTimeSpan();
				ObjectiveTime = table.Stats["Objective Time"].OWValToTimeSpan();
				Score = table.Stats["Score"].OWValToInt();
				TimePlayed = table.Stats["Time Played"].OWValToTimeSpan();
			}
		}

		public class MiscellaneousStats : IStatModule
		{
			public int MeleeFinalBlowsMostinGame { get; private set; }
			public int DefensiveAssists { get; private set; }
			public int DefensiveAssistsAverage { get; private set; }
			public int OffensiveAssists { get; private set; }
			public int OffensiveAssistsAverage { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				MeleeFinalBlowsMostinGame = table.Stats["Melee Final Blows - Most in Game"].OWValToInt();
				DefensiveAssists = table.Stats["Defensive Assists"].OWValToInt();
				DefensiveAssistsAverage = table.Stats["Defensive Assists - Average"].OWValToInt();
				OffensiveAssists = table.Stats["Offensive Assists"].OWValToInt();
				OffensiveAssistsAverage = table.Stats["Offensive Assists - Average"].OWValToInt();
			}
		}
	}
}
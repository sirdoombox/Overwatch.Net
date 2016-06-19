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
				var prop = GetType().GetProperty(item.Name.Replace(" ", ""));
				if (typeof(IStatModule).IsAssignableFrom(prop.PropertyType))
				{
					IStatModule statModule = (IStatModule)Activator.CreateInstance(prop.PropertyType);
					statModule.SendTable(item);
					prop.SetValue(this, statModule);
				}
			}
		}

		public class CombatStats : IStatModule
		{
			public float MeleeFinalBlows { get; private set; }
			public float SoloKills { get; private set; }
			public float ObjectiveKills { get; private set; }
			public float FinalBlows { get; private set; }
			public float DamageDone { get; private set; }
			public float Eliminations { get; private set; }
			public float EnvironmentalKills { get; private set; }
			public float Multikills { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				MeleeFinalBlows = table.Stats["Melee Final Blows"].OWValToFloat();
				SoloKills = table.Stats["Solo Kills"].OWValToFloat();
				ObjectiveKills = table.Stats["Objective Kills"].OWValToFloat();
				FinalBlows = table.Stats["Final Blows"].OWValToFloat();
				DamageDone = table.Stats["Damage Done"].OWValToFloat();
				Eliminations = table.Stats["Eliminations"].OWValToFloat();
				EnvironmentalKills = table.Stats["Environmental Kills"].OWValToFloat();
				Multikills = table.Stats["Multikills"].OWValToFloat();
			}
		}

		public class AssistsStats : IStatModule
		{
			public float HealingDone { get; private set; }
			public float ReconAssists { get; private set; }
			public float TeleporterPadsDestroyed { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				HealingDone = table.Stats["Healing Done"].OWValToFloat();
				ReconAssists = table.Stats["Recon Assists"].OWValToFloat();
				TeleporterPadsDestroyed = table.Stats["Teleporter Pads Destroyed"].OWValToFloat();
			}
		}

		public class BestStats : IStatModule
		{
			public float EliminationsMostinGame { get; private set; }
			public float FinalBlowsMostinGame { get; private set; }
			public float DamageDoneMostinGame { get; private set; }
			public float HealingDoneMostinGame { get; private set; }
			public float DefensiveAssistsMostinGame { get; private set; }
			public float OffensiveAssistsMostinGame { get; private set; }
			public float ObjectiveKillsMostinGame { get; private set; }
			public TimeSpan ObjectiveTimeMostinGame { get; private set; }
			public float MultikillBest { get; private set; }
			public float SoloKillsMostinGame { get; private set; }
			public TimeSpan TimeSpentonFireMostinGame { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				EliminationsMostinGame = table.Stats["Eliminations - Most in Game"].OWValToFloat();
				FinalBlowsMostinGame = table.Stats["Final Blows - Most in Game"].OWValToFloat();
				DamageDoneMostinGame = table.Stats["Damage Done - Most in Game"].OWValToFloat();
				HealingDoneMostinGame = table.Stats["Healing Done - Most in Game"].OWValToFloat();
				DefensiveAssistsMostinGame = table.Stats["Defensive Assists - Most in Game"].OWValToFloat();
				OffensiveAssistsMostinGame = table.Stats["Offensive Assists - Most in Game"].OWValToFloat();
				ObjectiveKillsMostinGame = table.Stats["Objective Kills - Most in Game"].OWValToFloat();
				ObjectiveTimeMostinGame = table.Stats["Objective Time - Most in Game"].OWValToTimeSpan();
				MultikillBest = table.Stats["Multikill - Best"].OWValToFloat();
				SoloKillsMostinGame = table.Stats["Solo Kills - Most in Game"].OWValToFloat();
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
			public float HealingDoneAverage { get; private set; }
			public float DeathsAverage { get; private set; }
			public float DamageDoneAverage { get; private set; }
			public float EliminationsAverage { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				MeleeFinalBlowsAverage = table.Stats["Melee Final Blows - Average"].OWValToFloat();
				FinalBlowsAverage = table.Stats["Final Blows - Average"].OWValToFloat();
				TimeSpentonFireAverage = table.Stats["Time Spent on Fire - Average"].OWValToTimeSpan();
				SoloKillsAverage = table.Stats["Solo Kills - Average"].OWValToFloat();
				ObjectiveTimeAverage = table.Stats["Objective Time - Average"].OWValToTimeSpan();
				ObjectiveKillsAverage = table.Stats["Objective Kills - Average"].OWValToFloat();
				HealingDoneAverage = table.Stats["Healing Done - Average"].OWValToFloat();
				DeathsAverage = table.Stats["Deaths - Average"].OWValToFloat();
				DamageDoneAverage = table.Stats["Damage Done - Average"].OWValToFloat();
				EliminationsAverage = table.Stats["Eliminations - Average"].OWValToFloat();
			}
		}

		public class DeathsStats : IStatModule
		{
			public float Deaths { get; private set; }
			public float EnvironmentalDeaths { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				Deaths = table.Stats["Deaths"].OWValToFloat();
				EnvironmentalDeaths = table.Stats["Environmental Deaths"].OWValToFloat();
			}
		}

		public class MatchAwardsStats : IStatModule
		{
			public float Cards { get; private set; }
			public float Medals { get; private set; }
			public float MedalsGold { get; private set; }
			public float MedalsSilver { get; private set; }
			public float MedalsBronze { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				Cards = table.Stats["Cards"].OWValToFloat();
				Medals = table.Stats["Medals"].OWValToFloat();
				MedalsGold = table.Stats["Medals - Gold"].OWValToFloat();
				MedalsSilver = table.Stats["Medals - Silver"].OWValToFloat();
				MedalsBronze = table.Stats["Medals - Bronze"].OWValToFloat();
			}
		}

		public class GameStats : IStatModule
		{
			public float GamesWon { get; private set; }
			public float GamesPlayed { get; private set; }
			public TimeSpan TimeSpentonFire { get; private set; }
			public TimeSpan ObjectiveTime { get; private set; }
			public float Score { get; private set; }
			public TimeSpan TimePlayed { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				GamesWon = table.Stats["Games Won"].OWValToFloat();
				GamesPlayed = table.Stats["Games Played"].OWValToFloat();
				TimeSpentonFire = table.Stats["Time Spent on Fire"].OWValToTimeSpan();
				ObjectiveTime = table.Stats["Objective Time"].OWValToTimeSpan();
				Score = table.Stats["Score"].OWValToFloat();
				TimePlayed = table.Stats["Time Played"].OWValToTimeSpan();
			}
		}

		public class MiscellaneousStats : IStatModule
		{
			public float MeleeFinalBlowsMostinGame { get; private set; }
			public float DefensiveAssists { get; private set; }
			public float DefensiveAssistsAverage { get; private set; }
			public float OffensiveAssists { get; private set; }
			public float OffensiveAssistsAverage { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				MeleeFinalBlowsMostinGame = table.Stats["Melee Final Blows - Most in Game"].OWValToFloat();
				DefensiveAssists = table.Stats["Defensive Assists"].OWValToFloat();
				DefensiveAssistsAverage = table.Stats["Defensive Assists - Average"].OWValToFloat();
				OffensiveAssists = table.Stats["Offensive Assists"].OWValToFloat();
				OffensiveAssistsAverage = table.Stats["Offensive Assists - Average"].OWValToFloat();
			}
		}
	}
}
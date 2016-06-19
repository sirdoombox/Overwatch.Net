using OverwatchAPI.Internal;
using System;
using System.Collections.Generic;

//-- A U T O   G E N E R A T E D --//

namespace OverwatchAPI.Data
{
	public class Zenyatta : IStatGroup
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
			public int Eliminations { get; private set; }
			public int ShotsFired { get; private set; }
			public int ShotsHit { get; private set; }
			public int CriticalHits { get; private set; }
			public int DamageDone { get; private set; }
			public int CriticalHitsperMinute { get; private set; }
			public int CriticalHitAccuracy { get; private set; }
			public float EliminationsperLife { get; private set; }
			public int WeaponAccuracy { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				Eliminations = table.Stats["Eliminations"].OWValToInt();
				ShotsFired = table.Stats["Shots Fired"].OWValToInt();
				ShotsHit = table.Stats["Shots Hit"].OWValToInt();
				CriticalHits = table.Stats["Critical Hits"].OWValToInt();
				DamageDone = table.Stats["Damage Done"].OWValToInt();
				CriticalHitsperMinute = table.Stats["Critical Hits per Minute"].OWValToInt();
				CriticalHitAccuracy = table.Stats["Critical Hit Accuracy"].OWValToInt();
				EliminationsperLife = table.Stats["Eliminations per Life"].OWValToFloat();
				WeaponAccuracy = table.Stats["Weapon Accuracy"].OWValToInt();
			}
		}

		public class AssistsStats : IStatModule
		{
			public int HealingDone { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				HealingDone = table.Stats["Healing Done"].OWValToInt();
			}
		}

		public class BestStats : IStatModule
		{
			public int EliminationsMostinLife { get; private set; }
			public int MostScorewithinoneLife { get; private set; }
			public int DamageDoneMostinLife { get; private set; }
			public int HealingDoneMostinLife { get; private set; }
			public int KillStreakBest { get; private set; }
			public int DamageDoneMostinGame { get; private set; }
			public int HealingDoneMostinGame { get; private set; }
			public int EliminationsMostinGame { get; private set; }
			public int ObjectiveTimeMostinGame { get; private set; }
			public int CriticalHitsMostinGame { get; private set; }
			public int CriticalHitsMostinLife { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				EliminationsMostinLife = table.Stats["Eliminations - Most in Life"].OWValToInt();
				MostScorewithinoneLife = table.Stats["Most Score within one Life"].OWValToInt();
				DamageDoneMostinLife = table.Stats["Damage Done - Most in Life"].OWValToInt();
				HealingDoneMostinLife = table.Stats["Healing Done - Most in Life"].OWValToInt();
				KillStreakBest = table.Stats["Kill Streak - Best"].OWValToInt();
				DamageDoneMostinGame = table.Stats["Damage Done - Most in Game"].OWValToInt();
				HealingDoneMostinGame = table.Stats["Healing Done - Most in Game"].OWValToInt();
				EliminationsMostinGame = table.Stats["Eliminations - Most in Game"].OWValToInt();
				ObjectiveTimeMostinGame = table.Stats["Objective Time - Most in Game"].OWValToInt();
				CriticalHitsMostinGame = table.Stats["Critical Hits - Most in Game"].OWValToInt();
				CriticalHitsMostinLife = table.Stats["Critical Hits - Most in Life"].OWValToInt();
			}
		}

		public class AverageStats : IStatModule
		{
			public float DeathsAverage { get; private set; }
			public float ObjectiveTimeAverage { get; private set; }
			public float HealingDoneAverage { get; private set; }
			public float EliminationsAverage { get; private set; }
			public float DamageDoneAverage { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				DeathsAverage = table.Stats["Deaths - Average"].OWValToFloat();
				ObjectiveTimeAverage = table.Stats["Objective Time - Average"].OWValToFloat();
				HealingDoneAverage = table.Stats["Healing Done - Average"].OWValToFloat();
				EliminationsAverage = table.Stats["Eliminations - Average"].OWValToFloat();
				DamageDoneAverage = table.Stats["Damage Done - Average"].OWValToFloat();
			}
		}

		public class DeathsStats : IStatModule
		{
			public int Deaths { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				Deaths = table.Stats["Deaths"].OWValToInt();
			}
		}

		public class MatchAwardsStats : IStatModule
		{
			public int MedalsBronze { get; private set; }
			public int MedalsSilver { get; private set; }
			public int MedalsGold { get; private set; }
			public int Medals { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				MedalsBronze = table.Stats["Medals - Bronze"].OWValToInt();
				MedalsSilver = table.Stats["Medals - Silver"].OWValToInt();
				MedalsGold = table.Stats["Medals - Gold"].OWValToInt();
				Medals = table.Stats["Medals"].OWValToInt();
			}
		}

		public class GameStats : IStatModule
		{
			public TimeSpan TimePlayed { get; private set; }
			public int GamesPlayed { get; private set; }
			public int GamesWon { get; private set; }
			public int Score { get; private set; }
			public int ObjectiveTime { get; private set; }
			public int WinPercentage { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				TimePlayed = table.Stats["Time Played"].OWValToTimeSpan();
				GamesPlayed = table.Stats["Games Played"].OWValToInt();
				GamesWon = table.Stats["Games Won"].OWValToInt();
				Score = table.Stats["Score"].OWValToInt();
				ObjectiveTime = table.Stats["Objective Time"].OWValToInt();
				WinPercentage = table.Stats["Win Percentage"].OWValToInt();
			}
		}

		public class MiscellaneousStats : IStatModule
		{
			public int OffensiveAssists { get; private set; }
			public int OffensiveAssistsMostinGame { get; private set; }
			public int DefensiveAssists { get; private set; }
			public int DefensiveAssistsMostinGame { get; private set; }
			public int HealingDone { get; private set; }
			public int HealingDoneAverage { get; private set; }
			public int DefensiveAssistsAverage { get; private set; }
			public int OffensiveAssistsAverage { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				OffensiveAssists = table.Stats["Offensive Assists"].OWValToInt();
				OffensiveAssistsMostinGame = table.Stats["Offensive Assists - Most in Game"].OWValToInt();
				DefensiveAssists = table.Stats["Defensive Assists"].OWValToInt();
				DefensiveAssistsMostinGame = table.Stats["Defensive Assists - Most in Game"].OWValToInt();
				HealingDone = table.Stats["Healing Done"].OWValToInt();
				HealingDoneAverage = table.Stats["Healing Done - Average"].OWValToInt();
				DefensiveAssistsAverage = table.Stats["Defensive Assists - Average"].OWValToInt();
				OffensiveAssistsAverage = table.Stats["Offensive Assists - Average"].OWValToInt();
			}
		}
	}
}
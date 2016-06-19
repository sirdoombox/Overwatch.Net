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
			public float Eliminations { get; private set; }
			public float ShotsFired { get; private set; }
			public float ShotsHit { get; private set; }
			public float CriticalHits { get; private set; }
			public float DamageDone { get; private set; }
			public float CriticalHitsperMinute { get; private set; }
			public float CriticalHitAccuracy { get; private set; }
			public float EliminationsperLife { get; private set; }
			public float WeaponAccuracy { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				Eliminations = table.Stats["Eliminations"].OWValToFloat();
				ShotsFired = table.Stats["Shots Fired"].OWValToFloat();
				ShotsHit = table.Stats["Shots Hit"].OWValToFloat();
				CriticalHits = table.Stats["Critical Hits"].OWValToFloat();
				DamageDone = table.Stats["Damage Done"].OWValToFloat();
				CriticalHitsperMinute = table.Stats["Critical Hits per Minute"].OWValToFloat();
				CriticalHitAccuracy = table.Stats["Critical Hit Accuracy"].OWValToFloat();
				EliminationsperLife = table.Stats["Eliminations per Life"].OWValToFloat();
				WeaponAccuracy = table.Stats["Weapon Accuracy"].OWValToFloat();
			}
		}

		public class AssistsStats : IStatModule
		{
			public float HealingDone { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				HealingDone = table.Stats["Healing Done"].OWValToFloat();
			}
		}

		public class BestStats : IStatModule
		{
			public float EliminationsMostinLife { get; private set; }
			public float MostScorewithinoneLife { get; private set; }
			public float DamageDoneMostinLife { get; private set; }
			public float HealingDoneMostinLife { get; private set; }
			public float KillStreakBest { get; private set; }
			public float DamageDoneMostinGame { get; private set; }
			public float HealingDoneMostinGame { get; private set; }
			public float EliminationsMostinGame { get; private set; }
			public float ObjectiveTimeMostinGame { get; private set; }
			public float CriticalHitsMostinGame { get; private set; }
			public float CriticalHitsMostinLife { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				EliminationsMostinLife = table.Stats["Eliminations - Most in Life"].OWValToFloat();
				MostScorewithinoneLife = table.Stats["Most Score within one Life"].OWValToFloat();
				DamageDoneMostinLife = table.Stats["Damage Done - Most in Life"].OWValToFloat();
				HealingDoneMostinLife = table.Stats["Healing Done - Most in Life"].OWValToFloat();
				KillStreakBest = table.Stats["Kill Streak - Best"].OWValToFloat();
				DamageDoneMostinGame = table.Stats["Damage Done - Most in Game"].OWValToFloat();
				HealingDoneMostinGame = table.Stats["Healing Done - Most in Game"].OWValToFloat();
				EliminationsMostinGame = table.Stats["Eliminations - Most in Game"].OWValToFloat();
				ObjectiveTimeMostinGame = table.Stats["Objective Time - Most in Game"].OWValToFloat();
				CriticalHitsMostinGame = table.Stats["Critical Hits - Most in Game"].OWValToFloat();
				CriticalHitsMostinLife = table.Stats["Critical Hits - Most in Life"].OWValToFloat();
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
			public float Deaths { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				Deaths = table.Stats["Deaths"].OWValToFloat();
			}
		}

		public class MatchAwardsStats : IStatModule
		{
			public float MedalsBronze { get; private set; }
			public float MedalsSilver { get; private set; }
			public float MedalsGold { get; private set; }
			public float Medals { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				MedalsBronze = table.Stats["Medals - Bronze"].OWValToFloat();
				MedalsSilver = table.Stats["Medals - Silver"].OWValToFloat();
				MedalsGold = table.Stats["Medals - Gold"].OWValToFloat();
				Medals = table.Stats["Medals"].OWValToFloat();
			}
		}

		public class GameStats : IStatModule
		{
			public TimeSpan TimePlayed { get; private set; }
			public float GamesPlayed { get; private set; }
			public float GamesWon { get; private set; }
			public float Score { get; private set; }
			public float ObjectiveTime { get; private set; }
			public float WinPercentage { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				TimePlayed = table.Stats["Time Played"].OWValToTimeSpan();
				GamesPlayed = table.Stats["Games Played"].OWValToFloat();
				GamesWon = table.Stats["Games Won"].OWValToFloat();
				Score = table.Stats["Score"].OWValToFloat();
				ObjectiveTime = table.Stats["Objective Time"].OWValToFloat();
				WinPercentage = table.Stats["Win Percentage"].OWValToFloat();
			}
		}

		public class MiscellaneousStats : IStatModule
		{
			public float OffensiveAssists { get; private set; }
			public float OffensiveAssistsMostinGame { get; private set; }
			public float DefensiveAssists { get; private set; }
			public float DefensiveAssistsMostinGame { get; private set; }
			public float HealingDone { get; private set; }
			public float HealingDoneAverage { get; private set; }
			public float DefensiveAssistsAverage { get; private set; }
			public float OffensiveAssistsAverage { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				OffensiveAssists = table.Stats["Offensive Assists"].OWValToFloat();
				OffensiveAssistsMostinGame = table.Stats["Offensive Assists - Most in Game"].OWValToFloat();
				DefensiveAssists = table.Stats["Defensive Assists"].OWValToFloat();
				DefensiveAssistsMostinGame = table.Stats["Defensive Assists - Most in Game"].OWValToFloat();
				HealingDone = table.Stats["Healing Done"].OWValToFloat();
				HealingDoneAverage = table.Stats["Healing Done - Average"].OWValToFloat();
				DefensiveAssistsAverage = table.Stats["Defensive Assists - Average"].OWValToFloat();
				OffensiveAssistsAverage = table.Stats["Offensive Assists - Average"].OWValToFloat();
			}
		}
	}
}
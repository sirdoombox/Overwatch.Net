using OverwatchAPI.Internal;
using System;
using System.Collections.Generic;

//-- A U T O   G E N E R A T E D --//

namespace OverwatchAPI.Data
{
	public class Zarya : IStatGroup
	{
		public HeroSpecificStats HeroSpecific { get; private set; }
		public CombatStats Combat { get; private set; }
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

		public class HeroSpecificStats : IStatModule
		{
			public int DamageBlocked { get; private set; }
			public int DamageBlockedMostinGame { get; private set; }
			public int LifetimeEnergyAccumulation { get; private set; }
			public int EnergyMaximum { get; private set; }
			public int ProjectedBarriersApplied { get; private set; }
			public int AverageEnergyBestinGame { get; private set; }
			public float ProjectedBarriersAppliedAverage { get; private set; }
			public int DamageBlockedAverage { get; private set; }
			public float LifetimeAverageEnergy { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				DamageBlocked = table.Stats["Damage Blocked"].OWValToInt();
				DamageBlockedMostinGame = table.Stats["Damage Blocked - Most in Game"].OWValToInt();
				LifetimeEnergyAccumulation = table.Stats["Lifetime Energy Accumulation"].OWValToInt();
				EnergyMaximum = table.Stats["Energy Maximum"].OWValToInt();
				ProjectedBarriersApplied = table.Stats["Projected Barriers Applied"].OWValToInt();
				AverageEnergyBestinGame = table.Stats["Average Energy - Best in Game"].OWValToInt();
				ProjectedBarriersAppliedAverage = table.Stats["Projected Barriers Applied - Average"].OWValToFloat();
				DamageBlockedAverage = table.Stats["Damage Blocked - Average"].OWValToInt();
				LifetimeAverageEnergy = table.Stats["Lifetime Average Energy"].OWValToFloat();
			}
		}

		public class CombatStats : IStatModule
		{
			public int Eliminations { get; private set; }
			public int ShotsFired { get; private set; }
			public int ShotsHit { get; private set; }
			public int DamageDone { get; private set; }
			public int EliminationsperLife { get; private set; }
			public int WeaponAccuracy { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				Eliminations = table.Stats["Eliminations"].OWValToInt();
				ShotsFired = table.Stats["Shots Fired"].OWValToInt();
				ShotsHit = table.Stats["Shots Hit"].OWValToInt();
				DamageDone = table.Stats["Damage Done"].OWValToInt();
				EliminationsperLife = table.Stats["Eliminations per Life"].OWValToInt();
				WeaponAccuracy = table.Stats["Weapon Accuracy"].OWValToInt();
			}
		}

		public class BestStats : IStatModule
		{
			public int EliminationsMostinLife { get; private set; }
			public int MostScorewithinoneLife { get; private set; }
			public int DamageDoneMostinLife { get; private set; }
			public int KillStreakBest { get; private set; }
			public int DamageDoneMostinGame { get; private set; }
			public int EliminationsMostinGame { get; private set; }
			public int ObjectiveTimeMostinGame { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				EliminationsMostinLife = table.Stats["Eliminations - Most in Life"].OWValToInt();
				MostScorewithinoneLife = table.Stats["Most Score within one Life"].OWValToInt();
				DamageDoneMostinLife = table.Stats["Damage Done - Most in Life"].OWValToInt();
				KillStreakBest = table.Stats["Kill Streak - Best"].OWValToInt();
				DamageDoneMostinGame = table.Stats["Damage Done - Most in Game"].OWValToInt();
				EliminationsMostinGame = table.Stats["Eliminations - Most in Game"].OWValToInt();
				ObjectiveTimeMostinGame = table.Stats["Objective Time - Most in Game"].OWValToInt();
			}
		}

		public class AverageStats : IStatModule
		{
			public float DeathsAverage { get; private set; }
			public float ObjectiveTimeAverage { get; private set; }
			public float EliminationsAverage { get; private set; }
			public float DamageDoneAverage { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				DeathsAverage = table.Stats["Deaths - Average"].OWValToFloat();
				ObjectiveTimeAverage = table.Stats["Objective Time - Average"].OWValToFloat();
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
			public int Score { get; private set; }
			public int ObjectiveTime { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				TimePlayed = table.Stats["Time Played"].OWValToTimeSpan();
				GamesPlayed = table.Stats["Games Played"].OWValToInt();
				Score = table.Stats["Score"].OWValToInt();
				ObjectiveTime = table.Stats["Objective Time"].OWValToInt();
			}
		}

		public class MiscellaneousStats : IStatModule
		{
			public int ProjectedBarriersAppliedMostinGame { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				ProjectedBarriersAppliedMostinGame = table.Stats["Projected Barriers Applied - Most in Game"].OWValToInt();
			}
		}
	}
}
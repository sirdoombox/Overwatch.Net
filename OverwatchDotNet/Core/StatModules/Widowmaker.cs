using OverwatchAPI.Internal;
using System;
using System.Collections.Generic;

//-- A U T O   G E N E R A T E D --//

namespace OverwatchAPI.Data
{
	public class Widowmaker : IStatGroup
	{
		public HeroSpecificStats HeroSpecific { get; private set; }
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
				if (prop != null && typeof(IStatModule).IsAssignableFrom(prop.PropertyType))
				{
					IStatModule statModule = (IStatModule)Activator.CreateInstance(prop.PropertyType);
					statModule.SendTable(item);
					prop.SetValue(this, statModule);
				}
			}
		}

		public class HeroSpecificStats : IStatModule
		{
			public float VenomMineKills { get; private set; }
			public float ScopedHits { get; private set; }
			public float ScopedShots { get; private set; }
			public float ScopedCriticalHits { get; private set; }
			public float ScopedCriticalHitsMostInGame { get; private set; }
			public float VenomMineKillsMostInGame { get; private set; }
			public float ReconAssistsMostInGame { get; private set; }
			public float ScopedAccuracyBestInGame { get; private set; }
			public float MeleeFinalBlowsMostInGame { get; private set; }
			public float VenomMineKillsAverage { get; private set; }
			public float ScopedCriticalHitsAverage { get; private set; }
			public float ScopedAccuracy { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				if(table.Stats.ContainsKey("Venom Mine Kills"))
					VenomMineKills = table.Stats["Venom Mine Kills"].OWValToFloat();
				else{ VenomMineKills = 0; }
				if(table.Stats.ContainsKey("Scoped Hits"))
					ScopedHits = table.Stats["Scoped Hits"].OWValToFloat();
				else{ ScopedHits = 0; }
				if(table.Stats.ContainsKey("Scoped Shots"))
					ScopedShots = table.Stats["Scoped Shots"].OWValToFloat();
				else{ ScopedShots = 0; }
				if(table.Stats.ContainsKey("Scoped Critical Hits"))
					ScopedCriticalHits = table.Stats["Scoped Critical Hits"].OWValToFloat();
				else{ ScopedCriticalHits = 0; }
				if(table.Stats.ContainsKey("Scoped Critical Hits - Most in Game"))
					ScopedCriticalHitsMostInGame = table.Stats["Scoped Critical Hits - Most in Game"].OWValToFloat();
				else{ ScopedCriticalHitsMostInGame = 0; }
				if(table.Stats.ContainsKey("Venom Mine Kills - Most in Game"))
					VenomMineKillsMostInGame = table.Stats["Venom Mine Kills - Most in Game"].OWValToFloat();
				else{ VenomMineKillsMostInGame = 0; }
				if(table.Stats.ContainsKey("Recon Assists - Most in Game"))
					ReconAssistsMostInGame = table.Stats["Recon Assists - Most in Game"].OWValToFloat();
				else{ ReconAssistsMostInGame = 0; }
				if(table.Stats.ContainsKey("Scoped Accuracy - Best in Game"))
					ScopedAccuracyBestInGame = table.Stats["Scoped Accuracy - Best in Game"].OWValToFloat();
				else{ ScopedAccuracyBestInGame = 0; }
				if(table.Stats.ContainsKey("Melee Final Blows - Most in Game"))
					MeleeFinalBlowsMostInGame = table.Stats["Melee Final Blows - Most in Game"].OWValToFloat();
				else{ MeleeFinalBlowsMostInGame = 0; }
				if(table.Stats.ContainsKey("Venom Mine Kills - Average"))
					VenomMineKillsAverage = table.Stats["Venom Mine Kills - Average"].OWValToFloat();
				else{ VenomMineKillsAverage = 0; }
				if(table.Stats.ContainsKey("Scoped Critical Hits - Average"))
					ScopedCriticalHitsAverage = table.Stats["Scoped Critical Hits - Average"].OWValToFloat();
				else{ ScopedCriticalHitsAverage = 0; }
				if(table.Stats.ContainsKey("Scoped Accuracy"))
					ScopedAccuracy = table.Stats["Scoped Accuracy"].OWValToFloat();
				else{ ScopedAccuracy = 0; }
			}
		}

		public class CombatStats : IStatModule
		{
			public float Eliminations { get; private set; }
			public float FinalBlows { get; private set; }
			public float SoloKills { get; private set; }
			public float ShotsFired { get; private set; }
			public float ShotsHit { get; private set; }
			public float CriticalHits { get; private set; }
			public float DamageDone { get; private set; }
			public float ObjectiveKills { get; private set; }
			public float MeleeFinalBlows { get; private set; }
			public float CriticalHitsPerMinute { get; private set; }
			public float CriticalHitAccuracy { get; private set; }
			public float EliminationsPerLife { get; private set; }
			public float WeaponAccuracy { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				if(table.Stats.ContainsKey("Eliminations"))
					Eliminations = table.Stats["Eliminations"].OWValToFloat();
				else{ Eliminations = 0; }
				if(table.Stats.ContainsKey("Final Blows"))
					FinalBlows = table.Stats["Final Blows"].OWValToFloat();
				else{ FinalBlows = 0; }
				if(table.Stats.ContainsKey("Solo Kills"))
					SoloKills = table.Stats["Solo Kills"].OWValToFloat();
				else{ SoloKills = 0; }
				if(table.Stats.ContainsKey("Shots Fired"))
					ShotsFired = table.Stats["Shots Fired"].OWValToFloat();
				else{ ShotsFired = 0; }
				if(table.Stats.ContainsKey("Shots Hit"))
					ShotsHit = table.Stats["Shots Hit"].OWValToFloat();
				else{ ShotsHit = 0; }
				if(table.Stats.ContainsKey("Critical Hits"))
					CriticalHits = table.Stats["Critical Hits"].OWValToFloat();
				else{ CriticalHits = 0; }
				if(table.Stats.ContainsKey("Damage Done"))
					DamageDone = table.Stats["Damage Done"].OWValToFloat();
				else{ DamageDone = 0; }
				if(table.Stats.ContainsKey("Objective Kills"))
					ObjectiveKills = table.Stats["Objective Kills"].OWValToFloat();
				else{ ObjectiveKills = 0; }
				if(table.Stats.ContainsKey("Melee Final Blows"))
					MeleeFinalBlows = table.Stats["Melee Final Blows"].OWValToFloat();
				else{ MeleeFinalBlows = 0; }
				if(table.Stats.ContainsKey("Critical Hits per Minute"))
					CriticalHitsPerMinute = table.Stats["Critical Hits per Minute"].OWValToFloat();
				else{ CriticalHitsPerMinute = 0; }
				if(table.Stats.ContainsKey("Critical Hit Accuracy"))
					CriticalHitAccuracy = table.Stats["Critical Hit Accuracy"].OWValToFloat();
				else{ CriticalHitAccuracy = 0; }
				if(table.Stats.ContainsKey("Eliminations per Life"))
					EliminationsPerLife = table.Stats["Eliminations per Life"].OWValToFloat();
				else{ EliminationsPerLife = 0; }
				if(table.Stats.ContainsKey("Weapon Accuracy"))
					WeaponAccuracy = table.Stats["Weapon Accuracy"].OWValToFloat();
				else{ WeaponAccuracy = 0; }
			}
		}

		public class AssistsStats : IStatModule
		{
			public float TeleporterPadsDestroyed { get; private set; }
			public float TurretsDestroyed { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				if(table.Stats.ContainsKey("Teleporter Pads Destroyed"))
					TeleporterPadsDestroyed = table.Stats["Teleporter Pads Destroyed"].OWValToFloat();
				else{ TeleporterPadsDestroyed = 0; }
				if(table.Stats.ContainsKey("Turrets Destroyed"))
					TurretsDestroyed = table.Stats["Turrets Destroyed"].OWValToFloat();
				else{ TurretsDestroyed = 0; }
			}
		}

		public class BestStats : IStatModule
		{
			public float EliminationsMostInLife { get; private set; }
			public float MostScoreWithinOneLife { get; private set; }
			public float DamageDoneMostInLife { get; private set; }
			public float WeaponAccuracyBestInGame { get; private set; }
			public float KillStreakBest { get; private set; }
			public float DamageDoneMostInGame { get; private set; }
			public float EliminationsMostInGame { get; private set; }
			public float FinalBlowsMostInGame { get; private set; }
			public float ObjectiveKillsMostInGame { get; private set; }
			public float ObjectiveTimeMostInGame { get; private set; }
			public float SoloKillsMostInGame { get; private set; }
			public float CriticalHitsMostInGame { get; private set; }
			public float CriticalHitsMostInLife { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				if(table.Stats.ContainsKey("Eliminations - Most in Life"))
					EliminationsMostInLife = table.Stats["Eliminations - Most in Life"].OWValToFloat();
				else{ EliminationsMostInLife = 0; }
				if(table.Stats.ContainsKey("Most Score within one Life"))
					MostScoreWithinOneLife = table.Stats["Most Score within one Life"].OWValToFloat();
				else{ MostScoreWithinOneLife = 0; }
				if(table.Stats.ContainsKey("Damage Done - Most in Life"))
					DamageDoneMostInLife = table.Stats["Damage Done - Most in Life"].OWValToFloat();
				else{ DamageDoneMostInLife = 0; }
				if(table.Stats.ContainsKey("Weapon Accuracy - Best in Game"))
					WeaponAccuracyBestInGame = table.Stats["Weapon Accuracy - Best in Game"].OWValToFloat();
				else{ WeaponAccuracyBestInGame = 0; }
				if(table.Stats.ContainsKey("Kill Streak - Best"))
					KillStreakBest = table.Stats["Kill Streak - Best"].OWValToFloat();
				else{ KillStreakBest = 0; }
				if(table.Stats.ContainsKey("Damage Done - Most in Game"))
					DamageDoneMostInGame = table.Stats["Damage Done - Most in Game"].OWValToFloat();
				else{ DamageDoneMostInGame = 0; }
				if(table.Stats.ContainsKey("Eliminations - Most in Game"))
					EliminationsMostInGame = table.Stats["Eliminations - Most in Game"].OWValToFloat();
				else{ EliminationsMostInGame = 0; }
				if(table.Stats.ContainsKey("Final Blows - Most in Game"))
					FinalBlowsMostInGame = table.Stats["Final Blows - Most in Game"].OWValToFloat();
				else{ FinalBlowsMostInGame = 0; }
				if(table.Stats.ContainsKey("Objective Kills - Most in Game"))
					ObjectiveKillsMostInGame = table.Stats["Objective Kills - Most in Game"].OWValToFloat();
				else{ ObjectiveKillsMostInGame = 0; }
				if(table.Stats.ContainsKey("Objective Time - Most in Game"))
					ObjectiveTimeMostInGame = table.Stats["Objective Time - Most in Game"].OWValToFloat();
				else{ ObjectiveTimeMostInGame = 0; }
				if(table.Stats.ContainsKey("Solo Kills - Most in Game"))
					SoloKillsMostInGame = table.Stats["Solo Kills - Most in Game"].OWValToFloat();
				else{ SoloKillsMostInGame = 0; }
				if(table.Stats.ContainsKey("Critical Hits - Most in Game"))
					CriticalHitsMostInGame = table.Stats["Critical Hits - Most in Game"].OWValToFloat();
				else{ CriticalHitsMostInGame = 0; }
				if(table.Stats.ContainsKey("Critical Hits - Most in Life"))
					CriticalHitsMostInLife = table.Stats["Critical Hits - Most in Life"].OWValToFloat();
				else{ CriticalHitsMostInLife = 0; }
			}
		}

		public class AverageStats : IStatModule
		{
			public float DeathsAverage { get; private set; }
			public float SoloKillsAverage { get; private set; }
			public float ObjectiveTimeAverage { get; private set; }
			public float ObjectiveKillsAverage { get; private set; }
			public float FinalBlowsAverage { get; private set; }
			public float EliminationsAverage { get; private set; }
			public float DamageDoneAverage { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				if(table.Stats.ContainsKey("Deaths - Average"))
					DeathsAverage = table.Stats["Deaths - Average"].OWValToFloat();
				else{ DeathsAverage = 0; }
				if(table.Stats.ContainsKey("Solo Kills - Average"))
					SoloKillsAverage = table.Stats["Solo Kills - Average"].OWValToFloat();
				else{ SoloKillsAverage = 0; }
				if(table.Stats.ContainsKey("Objective Time - Average"))
					ObjectiveTimeAverage = table.Stats["Objective Time - Average"].OWValToFloat();
				else{ ObjectiveTimeAverage = 0; }
				if(table.Stats.ContainsKey("Objective Kills - Average"))
					ObjectiveKillsAverage = table.Stats["Objective Kills - Average"].OWValToFloat();
				else{ ObjectiveKillsAverage = 0; }
				if(table.Stats.ContainsKey("Final Blows - Average"))
					FinalBlowsAverage = table.Stats["Final Blows - Average"].OWValToFloat();
				else{ FinalBlowsAverage = 0; }
				if(table.Stats.ContainsKey("Eliminations - Average"))
					EliminationsAverage = table.Stats["Eliminations - Average"].OWValToFloat();
				else{ EliminationsAverage = 0; }
				if(table.Stats.ContainsKey("Damage Done - Average"))
					DamageDoneAverage = table.Stats["Damage Done - Average"].OWValToFloat();
				else{ DamageDoneAverage = 0; }
			}
		}

		public class DeathsStats : IStatModule
		{
			public float Deaths { get; private set; }
			public float EnvironmentalDeaths { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				if(table.Stats.ContainsKey("Deaths"))
					Deaths = table.Stats["Deaths"].OWValToFloat();
				else{ Deaths = 0; }
				if(table.Stats.ContainsKey("Environmental Deaths"))
					EnvironmentalDeaths = table.Stats["Environmental Deaths"].OWValToFloat();
				else{ EnvironmentalDeaths = 0; }
			}
		}

		public class MatchAwardsStats : IStatModule
		{
			public float MedalsBronze { get; private set; }
			public float MedalsSilver { get; private set; }
			public float MedalsGold { get; private set; }
			public float Medals { get; private set; }
			public float Cards { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				if(table.Stats.ContainsKey("Medals - Bronze"))
					MedalsBronze = table.Stats["Medals - Bronze"].OWValToFloat();
				else{ MedalsBronze = 0; }
				if(table.Stats.ContainsKey("Medals - Silver"))
					MedalsSilver = table.Stats["Medals - Silver"].OWValToFloat();
				else{ MedalsSilver = 0; }
				if(table.Stats.ContainsKey("Medals - Gold"))
					MedalsGold = table.Stats["Medals - Gold"].OWValToFloat();
				else{ MedalsGold = 0; }
				if(table.Stats.ContainsKey("Medals"))
					Medals = table.Stats["Medals"].OWValToFloat();
				else{ Medals = 0; }
				if(table.Stats.ContainsKey("Cards"))
					Cards = table.Stats["Cards"].OWValToFloat();
				else{ Cards = 0; }
			}
		}

		public class GameStats : IStatModule
		{
			public TimeSpan TimePlayed { get; private set; }
			public float GamesPlayed { get; private set; }
			public float GamesWon { get; private set; }
			public float Score { get; private set; }
			public float ObjectiveTime { get; private set; }
			public float TimeSpentOnFire { get; private set; }
			public float WinPercentage { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				if(table.Stats.ContainsKey("Time Played"))
					TimePlayed = table.Stats["Time Played"].OWValToTimeSpan();
				else{ TimePlayed = TimeSpan.FromSeconds(0);; }
				if(table.Stats.ContainsKey("Games Played"))
					GamesPlayed = table.Stats["Games Played"].OWValToFloat();
				else{ GamesPlayed = 0; }
				if(table.Stats.ContainsKey("Games Won"))
					GamesWon = table.Stats["Games Won"].OWValToFloat();
				else{ GamesWon = 0; }
				if(table.Stats.ContainsKey("Score"))
					Score = table.Stats["Score"].OWValToFloat();
				else{ Score = 0; }
				if(table.Stats.ContainsKey("Objective Time"))
					ObjectiveTime = table.Stats["Objective Time"].OWValToFloat();
				else{ ObjectiveTime = 0; }
				if(table.Stats.ContainsKey("Time Spent on Fire"))
					TimeSpentOnFire = table.Stats["Time Spent on Fire"].OWValToFloat();
				else{ TimeSpentOnFire = 0; }
				if(table.Stats.ContainsKey("Win Percentage"))
					WinPercentage = table.Stats["Win Percentage"].OWValToFloat();
				else{ WinPercentage = 0; }
			}
		}

		public class MiscellaneousStats : IStatModule
		{
			public float ReconAssistsAverage { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				if(table.Stats.ContainsKey("Recon Assists - Average"))
					ReconAssistsAverage = table.Stats["Recon Assists - Average"].OWValToFloat();
				else{ ReconAssistsAverage = 0; }
			}
		}
	}
}
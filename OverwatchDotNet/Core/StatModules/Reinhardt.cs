using OverwatchAPI.Internal;
using System;
using System.Collections.Generic;

//-- A U T O   G E N E R A T E D --//

namespace OverwatchAPI.Data
{
	public class Reinhardt : IStatGroup
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
			public float DamageBlocked { get; private set; }
			public float DamageBlockedMostInGame { get; private set; }
			public float ChargeKills { get; private set; }
			public float ChargeKillsMostInGame { get; private set; }
			public float FireStrikeKills { get; private set; }
			public float FireStrikeKillsMostInGame { get; private set; }
			public float EarthshatterKills { get; private set; }
			public float EarthshatterKillsMostInGame { get; private set; }
			public float FireStrikeKillsAverage { get; private set; }
			public float EarthshatterKillsAverage { get; private set; }
			public float DamageBlockedAverage { get; private set; }
			public float ChargeKillsAverage { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				if(table.Stats.ContainsKey("Damage Blocked"))
					DamageBlocked = table.Stats["Damage Blocked"].OWValToFloat();
				else{ DamageBlocked = 0; }
				if(table.Stats.ContainsKey("Damage Blocked - Most in Game"))
					DamageBlockedMostInGame = table.Stats["Damage Blocked - Most in Game"].OWValToFloat();
				else{ DamageBlockedMostInGame = 0; }
				if(table.Stats.ContainsKey("Charge Kills"))
					ChargeKills = table.Stats["Charge Kills"].OWValToFloat();
				else{ ChargeKills = 0; }
				if(table.Stats.ContainsKey("Charge Kills - Most in Game"))
					ChargeKillsMostInGame = table.Stats["Charge Kills - Most in Game"].OWValToFloat();
				else{ ChargeKillsMostInGame = 0; }
				if(table.Stats.ContainsKey("Fire Strike Kills"))
					FireStrikeKills = table.Stats["Fire Strike Kills"].OWValToFloat();
				else{ FireStrikeKills = 0; }
				if(table.Stats.ContainsKey("Fire Strike Kills - Most in Game"))
					FireStrikeKillsMostInGame = table.Stats["Fire Strike Kills - Most in Game"].OWValToFloat();
				else{ FireStrikeKillsMostInGame = 0; }
				if(table.Stats.ContainsKey("Earthshatter Kills"))
					EarthshatterKills = table.Stats["Earthshatter Kills"].OWValToFloat();
				else{ EarthshatterKills = 0; }
				if(table.Stats.ContainsKey("Earthshatter Kills - Most in Game"))
					EarthshatterKillsMostInGame = table.Stats["Earthshatter Kills - Most in Game"].OWValToFloat();
				else{ EarthshatterKillsMostInGame = 0; }
				if(table.Stats.ContainsKey("Fire Strike Kills - Average"))
					FireStrikeKillsAverage = table.Stats["Fire Strike Kills - Average"].OWValToFloat();
				else{ FireStrikeKillsAverage = 0; }
				if(table.Stats.ContainsKey("Earthshatter Kills - Average"))
					EarthshatterKillsAverage = table.Stats["Earthshatter Kills - Average"].OWValToFloat();
				else{ EarthshatterKillsAverage = 0; }
				if(table.Stats.ContainsKey("Damage Blocked - Average"))
					DamageBlockedAverage = table.Stats["Damage Blocked - Average"].OWValToFloat();
				else{ DamageBlockedAverage = 0; }
				if(table.Stats.ContainsKey("Charge Kills - Average"))
					ChargeKillsAverage = table.Stats["Charge Kills - Average"].OWValToFloat();
				else{ ChargeKillsAverage = 0; }
			}
		}

		public class CombatStats : IStatModule
		{
			public float Eliminations { get; private set; }
			public float FinalBlows { get; private set; }
			public float SoloKills { get; private set; }
			public float DamageDone { get; private set; }
			public float ObjectiveKills { get; private set; }
			public float Multikills { get; private set; }
			public float EnvironmentalKills { get; private set; }
			public float EliminationsPerLife { get; private set; }

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
				if(table.Stats.ContainsKey("Damage Done"))
					DamageDone = table.Stats["Damage Done"].OWValToFloat();
				else{ DamageDone = 0; }
				if(table.Stats.ContainsKey("Objective Kills"))
					ObjectiveKills = table.Stats["Objective Kills"].OWValToFloat();
				else{ ObjectiveKills = 0; }
				if(table.Stats.ContainsKey("Multikills"))
					Multikills = table.Stats["Multikills"].OWValToFloat();
				else{ Multikills = 0; }
				if(table.Stats.ContainsKey("Environmental Kills"))
					EnvironmentalKills = table.Stats["Environmental Kills"].OWValToFloat();
				else{ EnvironmentalKills = 0; }
				if(table.Stats.ContainsKey("Eliminations per Life"))
					EliminationsPerLife = table.Stats["Eliminations per Life"].OWValToFloat();
				else{ EliminationsPerLife = 0; }
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
			public float DamageDoneMostInLife { get; private set; }
			public float KillStreakBest { get; private set; }
			public float DamageDoneMostInGame { get; private set; }
			public float EliminationsMostInGame { get; private set; }
			public float FinalBlowsMostInGame { get; private set; }
			public float ObjectiveKillsMostInGame { get; private set; }
			public float ObjectiveTimeMostInGame { get; private set; }
			public float SoloKillsMostInGame { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				if(table.Stats.ContainsKey("Eliminations - Most in Life"))
					EliminationsMostInLife = table.Stats["Eliminations - Most in Life"].OWValToFloat();
				else{ EliminationsMostInLife = 0; }
				if(table.Stats.ContainsKey("Damage Done - Most in Life"))
					DamageDoneMostInLife = table.Stats["Damage Done - Most in Life"].OWValToFloat();
				else{ DamageDoneMostInLife = 0; }
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
			public float MultikillBest { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				if(table.Stats.ContainsKey("Multikill - Best"))
					MultikillBest = table.Stats["Multikill - Best"].OWValToFloat();
				else{ MultikillBest = 0; }
			}
		}
	}
}
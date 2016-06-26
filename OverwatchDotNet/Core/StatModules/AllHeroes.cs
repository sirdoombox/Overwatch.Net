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
				if (prop != null && typeof(IStatModule).IsAssignableFrom(prop.PropertyType))
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
				if(table.Stats.ContainsKey("Melee Final Blows"))
					MeleeFinalBlows = table.Stats["Melee Final Blows"].OWValToFloat();
				else{ MeleeFinalBlows = 0; }
				if(table.Stats.ContainsKey("Solo Kills"))
					SoloKills = table.Stats["Solo Kills"].OWValToFloat();
				else{ SoloKills = 0; }
				if(table.Stats.ContainsKey("Objective Kills"))
					ObjectiveKills = table.Stats["Objective Kills"].OWValToFloat();
				else{ ObjectiveKills = 0; }
				if(table.Stats.ContainsKey("Final Blows"))
					FinalBlows = table.Stats["Final Blows"].OWValToFloat();
				else{ FinalBlows = 0; }
				if(table.Stats.ContainsKey("Damage Done"))
					DamageDone = table.Stats["Damage Done"].OWValToFloat();
				else{ DamageDone = 0; }
				if(table.Stats.ContainsKey("Eliminations"))
					Eliminations = table.Stats["Eliminations"].OWValToFloat();
				else{ Eliminations = 0; }
				if(table.Stats.ContainsKey("Environmental Kills"))
					EnvironmentalKills = table.Stats["Environmental Kills"].OWValToFloat();
				else{ EnvironmentalKills = 0; }
				if(table.Stats.ContainsKey("Multikills"))
					Multikills = table.Stats["Multikills"].OWValToFloat();
				else{ Multikills = 0; }
			}
		}

		public class AssistsStats : IStatModule
		{
			public float HealingDone { get; private set; }
			public float ReconAssists { get; private set; }
			public float TeleporterPadsDestroyed { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				if(table.Stats.ContainsKey("Healing Done"))
					HealingDone = table.Stats["Healing Done"].OWValToFloat();
				else{ HealingDone = 0; }
				if(table.Stats.ContainsKey("Recon Assists"))
					ReconAssists = table.Stats["Recon Assists"].OWValToFloat();
				else{ ReconAssists = 0; }
				if(table.Stats.ContainsKey("Teleporter Pads Destroyed"))
					TeleporterPadsDestroyed = table.Stats["Teleporter Pads Destroyed"].OWValToFloat();
				else{ TeleporterPadsDestroyed = 0; }
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
				if(table.Stats.ContainsKey("Eliminations - Most in Game"))
					EliminationsMostinGame = table.Stats["Eliminations - Most in Game"].OWValToFloat();
				else{ EliminationsMostinGame = 0; }
				if(table.Stats.ContainsKey("Final Blows - Most in Game"))
					FinalBlowsMostinGame = table.Stats["Final Blows - Most in Game"].OWValToFloat();
				else{ FinalBlowsMostinGame = 0; }
				if(table.Stats.ContainsKey("Damage Done - Most in Game"))
					DamageDoneMostinGame = table.Stats["Damage Done - Most in Game"].OWValToFloat();
				else{ DamageDoneMostinGame = 0; }
				if(table.Stats.ContainsKey("Healing Done - Most in Game"))
					HealingDoneMostinGame = table.Stats["Healing Done - Most in Game"].OWValToFloat();
				else{ HealingDoneMostinGame = 0; }
				if(table.Stats.ContainsKey("Defensive Assists - Most in Game"))
					DefensiveAssistsMostinGame = table.Stats["Defensive Assists - Most in Game"].OWValToFloat();
				else{ DefensiveAssistsMostinGame = 0; }
				if(table.Stats.ContainsKey("Offensive Assists - Most in Game"))
					OffensiveAssistsMostinGame = table.Stats["Offensive Assists - Most in Game"].OWValToFloat();
				else{ OffensiveAssistsMostinGame = 0; }
				if(table.Stats.ContainsKey("Objective Kills - Most in Game"))
					ObjectiveKillsMostinGame = table.Stats["Objective Kills - Most in Game"].OWValToFloat();
				else{ ObjectiveKillsMostinGame = 0; }
				if(table.Stats.ContainsKey("Objective Time - Most in Game"))
					ObjectiveTimeMostinGame = table.Stats["Objective Time - Most in Game"].OWValToTimeSpan();
				else{ ObjectiveTimeMostinGame = TimeSpan.FromSeconds(0);; }
				if(table.Stats.ContainsKey("Multikill - Best"))
					MultikillBest = table.Stats["Multikill - Best"].OWValToFloat();
				else{ MultikillBest = 0; }
				if(table.Stats.ContainsKey("Solo Kills - Most in Game"))
					SoloKillsMostinGame = table.Stats["Solo Kills - Most in Game"].OWValToFloat();
				else{ SoloKillsMostinGame = 0; }
				if(table.Stats.ContainsKey("Time Spent on Fire - Most in Game"))
					TimeSpentonFireMostinGame = table.Stats["Time Spent on Fire - Most in Game"].OWValToTimeSpan();
				else{ TimeSpentonFireMostinGame = TimeSpan.FromSeconds(0);; }
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
				if(table.Stats.ContainsKey("Melee Final Blows - Average"))
					MeleeFinalBlowsAverage = table.Stats["Melee Final Blows - Average"].OWValToFloat();
				else{ MeleeFinalBlowsAverage = 0; }
				if(table.Stats.ContainsKey("Final Blows - Average"))
					FinalBlowsAverage = table.Stats["Final Blows - Average"].OWValToFloat();
				else{ FinalBlowsAverage = 0; }
				if(table.Stats.ContainsKey("Time Spent on Fire - Average"))
					TimeSpentonFireAverage = table.Stats["Time Spent on Fire - Average"].OWValToTimeSpan();
				else{ TimeSpentonFireAverage = TimeSpan.FromSeconds(0);; }
				if(table.Stats.ContainsKey("Solo Kills - Average"))
					SoloKillsAverage = table.Stats["Solo Kills - Average"].OWValToFloat();
				else{ SoloKillsAverage = 0; }
				if(table.Stats.ContainsKey("Objective Time - Average"))
					ObjectiveTimeAverage = table.Stats["Objective Time - Average"].OWValToTimeSpan();
				else{ ObjectiveTimeAverage = TimeSpan.FromSeconds(0);; }
				if(table.Stats.ContainsKey("Objective Kills - Average"))
					ObjectiveKillsAverage = table.Stats["Objective Kills - Average"].OWValToFloat();
				else{ ObjectiveKillsAverage = 0; }
				if(table.Stats.ContainsKey("Healing Done - Average"))
					HealingDoneAverage = table.Stats["Healing Done - Average"].OWValToFloat();
				else{ HealingDoneAverage = 0; }
				if(table.Stats.ContainsKey("Deaths - Average"))
					DeathsAverage = table.Stats["Deaths - Average"].OWValToFloat();
				else{ DeathsAverage = 0; }
				if(table.Stats.ContainsKey("Damage Done - Average"))
					DamageDoneAverage = table.Stats["Damage Done - Average"].OWValToFloat();
				else{ DamageDoneAverage = 0; }
				if(table.Stats.ContainsKey("Eliminations - Average"))
					EliminationsAverage = table.Stats["Eliminations - Average"].OWValToFloat();
				else{ EliminationsAverage = 0; }
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
			public float Cards { get; private set; }
			public float Medals { get; private set; }
			public float MedalsGold { get; private set; }
			public float MedalsSilver { get; private set; }
			public float MedalsBronze { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				if(table.Stats.ContainsKey("Cards"))
					Cards = table.Stats["Cards"].OWValToFloat();
				else{ Cards = 0; }
				if(table.Stats.ContainsKey("Medals"))
					Medals = table.Stats["Medals"].OWValToFloat();
				else{ Medals = 0; }
				if(table.Stats.ContainsKey("Medals - Gold"))
					MedalsGold = table.Stats["Medals - Gold"].OWValToFloat();
				else{ MedalsGold = 0; }
				if(table.Stats.ContainsKey("Medals - Silver"))
					MedalsSilver = table.Stats["Medals - Silver"].OWValToFloat();
				else{ MedalsSilver = 0; }
				if(table.Stats.ContainsKey("Medals - Bronze"))
					MedalsBronze = table.Stats["Medals - Bronze"].OWValToFloat();
				else{ MedalsBronze = 0; }
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
				if(table.Stats.ContainsKey("Games Won"))
					GamesWon = table.Stats["Games Won"].OWValToFloat();
				else{ GamesWon = 0; }
				if(table.Stats.ContainsKey("Games Played"))
					GamesPlayed = table.Stats["Games Played"].OWValToFloat();
				else{ GamesPlayed = 0; }
				if(table.Stats.ContainsKey("Time Spent on Fire"))
					TimeSpentonFire = table.Stats["Time Spent on Fire"].OWValToTimeSpan();
				else{ TimeSpentonFire = TimeSpan.FromSeconds(0);; }
				if(table.Stats.ContainsKey("Objective Time"))
					ObjectiveTime = table.Stats["Objective Time"].OWValToTimeSpan();
				else{ ObjectiveTime = TimeSpan.FromSeconds(0);; }
				if(table.Stats.ContainsKey("Score"))
					Score = table.Stats["Score"].OWValToFloat();
				else{ Score = 0; }
				if(table.Stats.ContainsKey("Time Played"))
					TimePlayed = table.Stats["Time Played"].OWValToTimeSpan();
				else{ TimePlayed = TimeSpan.FromSeconds(0);; }
			}
		}

		public class MiscellaneousStats : IStatModule
		{
			public float MeleeFinalBlowsMostinGame { get; private set; }
			public float DefensiveAssists { get; private set; }
			public float DefensiveAssistsAverage { get; private set; }
			public float OffensiveAssists { get; private set; }
			public float OffensiveAssistsAverage { get; private set; }
			public float ReconAssistsAverage { get; private set; }
			public float ReconAssistsMostinGame { get; private set; }

			public void SendTable(OverwatchDataTable table)
			{
				if(table.Stats.ContainsKey("Melee Final Blows - Most in Game"))
					MeleeFinalBlowsMostinGame = table.Stats["Melee Final Blows - Most in Game"].OWValToFloat();
				else{ MeleeFinalBlowsMostinGame = 0; }
				if(table.Stats.ContainsKey("Defensive Assists"))
					DefensiveAssists = table.Stats["Defensive Assists"].OWValToFloat();
				else{ DefensiveAssists = 0; }
				if(table.Stats.ContainsKey("Defensive Assists - Average"))
					DefensiveAssistsAverage = table.Stats["Defensive Assists - Average"].OWValToFloat();
				else{ DefensiveAssistsAverage = 0; }
				if(table.Stats.ContainsKey("Offensive Assists"))
					OffensiveAssists = table.Stats["Offensive Assists"].OWValToFloat();
				else{ OffensiveAssists = 0; }
				if(table.Stats.ContainsKey("Offensive Assists - Average"))
					OffensiveAssistsAverage = table.Stats["Offensive Assists - Average"].OWValToFloat();
				else{ OffensiveAssistsAverage = 0; }
				if(table.Stats.ContainsKey("Recon Assists - Average"))
					ReconAssistsAverage = table.Stats["Recon Assists - Average"].OWValToFloat();
				else{ ReconAssistsAverage = 0; }
				if(table.Stats.ContainsKey("Recon Assists - Most in Game"))
					ReconAssistsMostinGame = table.Stats["Recon Assists - Most in Game"].OWValToFloat();
				else{ ReconAssistsMostinGame = 0; }
			}
		}
	}
}
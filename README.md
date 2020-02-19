# Overwatch.Net

An unofficial player stats API for the Blizzard game "Overwatch" - Targetting .NET Standard 2.0.

[![Build status](https://ci.appveyor.com/api/projects/status/github/sirdoombox/overwatch.net?svg=true)](https://ci.appveyor.com/project/sirdoombox/overwatch-net)[![NuGet version](https://badge.fury.io/nu/Overwatch.Net.svg)](https://badge.fury.io/nu/Overwatch.Net)

## What is it?

A simple web scraper that grabs player stats from a users PlayOverwatch.com profile and parses it to be used however you like. It's written entirely in C# using AngleSharp to parse the data from the page. The only limiting factor is the speed at which the PlayOverwatch profile can be loaded.

## Current Features
* Supports PC, Xbox 1 and Playstation 4 players.
* Entirely async
* Serializable - No complex data.
* Platform detection
* Identifies and gracefully handles private profiles - parsing as much data as is available.
* Finding player aliases where a player plays on multiple platforms.

## Planned Features
* None at the moment - Any feature requests will be placed here, however new features do make themselves into the library from time to time.

## Dependencies
* AngleSharp - Used to parse the data from the PlayOverwatch page as no other data source is available from Blizzard.
* Newtonsoft.Json - Used to parse XHR requests for player aliases.

## Usage
```csharp
using(var owClient = new OverwatchClient()) // Initialising without an "OverwatchConfig" will use the Default config.
{
  Player player = await owClient.GetPlayerAsync("SirDoombox#2603");
  var allHeroesHealingDone = player.CasualStats.GetStatExact("All Heroes", "Assists", "Healing Done");
  IEnumerable<Stat> allHealingDoneStats = player.CasualStats.FilterByName("Healing Done");
  foreach(var stat in player.CasualStats)
  {
    string statHeroName = stat.HeroName
    string statName = stat.Name;
    string statCategoryName = stat.CategoryName;
    string statValue = stat.Value;
  }
}
```
NOTE: If you plan on making many requests over the lifetime of your application, I recommend keeping an instance of `OverwatchClient` around and disposing of it explicitly later.

## Contact
If you wish to contact me about contributing to the project, or have any questions / suggestions please feel free to come find me on the [C# discord server.](https://discordapp.com/invite/ccyrDKv "C# Discord") - @Doombox#0661

Please report any issues or bugs that you may find at your earliest convenience so I can get them fixed ASAP.

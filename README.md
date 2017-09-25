# Overwatch.Net

An unofficial player stats API for the Blizzard game "Overwatch" - Targetting .NET Standard 1.1 for maximum compatibility.

[![NuGet version](https://badge.fury.io/nu/Overwatch.Net.svg)](https://badge.fury.io/nu/Overwatch.Net)

## What is it?

A simple web scraper that grabs player stats from a users PlayOverwatch.com profile and parses it to be used however you like. It's written entirely in C# using AngleSharp to parse the data from the page. The only limiting factor is the speed at which the PlayOverwatch profile can be loaded.

## Current Features
* Supports PC, Xbox 1 and Playstation 4 players.
* Entirely async
* Serializable - No complex data.
* Region / Platform detection

## Planned Features
* None at the moment - Any feature requests will be placed here.

## Dependencies
* AngleSharp - Used to parse the data from the PlayOverwatch page as no other data source is available from Blizzard.

## Usage
```csharp
using(var owClient = new OverwatchClient())
{
  Player player = owClient.GetPlayerAsync("SirDoombox#2603");
  // it is possible to get values individually, however it is not recommended.
  double allHeroesHealingDone = player.CasualStats["AllHeroes"]["Assists"]["Healing Done"];
  foreach(var hero in player.CasualStats)
  {
    foreach(var category in hero.Value)
    {
      foreach(var stat in category.Value)
      {
        string name = stat.Key;
        double value = stat.Value;
      }
    }
  }
}
```
NOTE: If you plan on making many requests over the lifetime of your application, I recommend keeping an instance of `OverwatchClient` around and disposing of it explicitly later.

## Contact
If you wish to contact me about contributing to the project, or have any questions / suggestions please feel free to come find me on the [C# discord server.](https://discord.gg/0np62rq4o8GnQO9l "C# Discord") - @Doombox#0661

Please report any issues or bugs that you may find at your earliest convenience so I can get them fixed ASAP.

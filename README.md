# Overwatch.Net

An unofficial player stats API for the Blizzard game "Overwatch".

[![NuGet version](https://badge.fury.io/nu/Overwatch.Net.svg)](https://badge.fury.io/nu/Overwatch.Net)

## What is it?

A simple wrapper that grabs player stats from a users PlayOverwatch.com profile and parses it to be used however you like. It's written entirely in C# using AngleSharp to parse the data from the page. The only limiting factor is the speed at which the PlayOverwatch profile can be loaded.

## Current Features
* Supports PC, Xbox 1 and Playstation 4 players.
* Entirely async operation
* Serializable - No complex data.
* Region detection - Easily find the correct region for a player.
* Platform detection - Detect which platform a player might play on.

## Planned Features
* None at the moment - Any feature requests will be placed here.

## Dependencies
* AngleSharp - Used to parse the data from the PlayOverwatch page as no other data source is available from Blizzard.

## Usage

After you've added the necessary references to your project, using the library is incredibly simple.

The below code will create a new Overwatch player with the given Battletag, it will then detect the players region and update the users stats entirely asynchronously.
```csharp
OverwatchPlayer player = new OverwatchPlayer("SirDoombox#2603");
await player.DetectPlatform();
await player.DetectRegionPC();
await player.UpdateStats();
Double timePlayedInSeconds = player.CasualStats.GetHero("AllHeroes").GetCategory("Game").GetStat("Time Played").Value;
```
You can cut down on some of the requests you need to make (and the time that those requests take up) by specifying the region at creation (if known). This snippet also uses `.GetAwaiter().GetResult()` to make the method run in a synchronous fashion.
```csharp
OverwatchPlayer player = new OverwatchPlayer("SirDoombox#2603", Platform.pc, Region.eu);
player.UpdateStats().GetAwaiter().GetResult();
StatCategory statCategory = player.CasualStats.GetHero("Junkrat").GetCategory("Hero Specific");
````
There are also some helper methods available for use to simplify some common operations
```csharp
bool validTag = OverwatchAPIHelpers.IsValidBattletag("SomePlayer#1234"); // Returns true.
string profileUrl = OverwatchAPIHelpers.ProfileUrl("SomePlayer#1234", Region.eu); // Returns a PlayOverwatch profile URL.
```
Please see the "OverwatchDotNetTestbed" project if you'd like to see an implementation.

## Contact
If you wish to contact me about contributing to the project, or have any questions / suggestions please feel free to come find me on the [C# discord server.](https://discord.gg/0np62rq4o8GnQO9l "C# Discord") - @Doombox#0661

Please report any issues or bugs that you may find at your earliest convenience so I can get them fixed ASAP.

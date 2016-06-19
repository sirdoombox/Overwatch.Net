using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

// -- Incredibly rushed and insanely weak plaintext code generation, uses my Overwatch profile as a base to craft classes from plaintext snippets -- //
// -- "If it ain't broken don't fix it" -- //

namespace OverwatchCodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            GetOverwatchStats("https://playoverwatch.com/en-gb/career/pc/eu/SirDoombox-2603");
            Console.ReadKey();
        }
        static Dictionary<string, string> idDictionary = new Dictionary<string, string>();
        static string filePath = @"D:\Projects\C#\Visual Studio 2015\Projects\OverwatchDotNet\OverwatchDotNet\Core\StatModules";

        static void GetOverwatchStats(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var document = BrowsingContext.New(config).OpenAsync(url).Result;

            foreach (var dropdownitem in document.QuerySelectorAll("select > option"))
            {
                string id = dropdownitem.GetAttribute("value");
                if (id.StartsWith("0x0"))
                {
                    idDictionary.Add(id, ParseClassName(dropdownitem.TextContent));
                }
            }

            foreach (var section in document.QuerySelectorAll("div[data-group-id='stats']"))
            {
                var catId = section.GetAttribute("data-category-id");
                var heroTable = BuildHeroTables(idDictionary[catId], section.QuerySelectorAll($"div[data-category-id='{catId}'] table.data-table"));
                CreateClass(idDictionary[catId], heroTable);
            }
        }

        static IEnumerable<OverwatchDataTable> BuildHeroTables(string heroName, IHtmlCollection<IElement> heroStats)
        {
            foreach (var table in heroStats)
            {
                OverwatchDataTable heroTable = new OverwatchDataTable();
                heroTable.Name = table.QuerySelector("thead").TextContent;
                var heroCard = new Dictionary<string, string>();
                foreach (var row in table.QuerySelectorAll("tbody tr"))
                {
                    if (heroCard.ContainsKey(row.Children[0].TextContent))
                        continue;
                    heroCard.Add(row.Children[0].TextContent, row.Children[1].TextContent);
                }
                heroTable.Stats = heroCard;
                yield return heroTable;
            }
        }

        static void CreateClass(string heroName, IEnumerable<OverwatchDataTable> heroStats)
        {
            string thisPath = $"{filePath}/{heroName}.cs";
            Stopwatch _stopwatch = Stopwatch.StartNew();
            using (TextWriter ts = File.CreateText(thisPath))
            {
                ts.Write(header);
                ts.Write($"\tpublic class {heroName} : IStatGroup\n\t{{\n");
                foreach (var table in heroStats)
                {
                    ts.Write($"\t\tpublic {table.Name.Replace(" ", "")}Stats {table.Name.Replace(" ", "")} {{ get; private set; }}\n");
                }

                ts.Write(statGroupImplement);

                foreach (var table in heroStats)
                {
                    ts.Write($"\n\t\tpublic class {table.Name.Replace(" ", "")}Stats : IStatModule\n\t\t{{\n");
                    foreach (var stat in table.Stats)
                    {
                        ts.Write($"\t\t\tpublic {GetValueType(stat.Value)} {CleansePropertyName(stat.Key)} {{ get; private set; }}\n");
                    }

                    // Implement the SendTables method.
                    ts.Write("\n\t\t\tpublic void SendTable(OverwatchDataTable table)\n\t\t\t{\n");
                    foreach (var row in table.Stats)
                    {
                        ts.Write($"\t\t\t\t{CleansePropertyName(row.Key)} = table.Stats[\"{row.Key}\"]{ExtensionType(row.Value)}\n");
                    }
                    ts.Write("\t\t\t}\n");
                    ts.Write("\t\t}\n");
                }
                ts.Write("\t}\n}");
            }
            _stopwatch.Stop();
            Console.WriteLine($"Finished Generating {heroName} in {_stopwatch.Elapsed}");
        }

        static string ParseClassName(string input)
        {
            if (input.ToLower() == "all heroes")
                return "AllHeroes";
            return input.Replace("ú", "u").Replace(":", "").Replace(" ", "").Replace("ö", "o").Replace(".", "");
        }

        static string CleansePropertyName(string input)
        {
            return input.Replace(" ", "").Replace("-", "");
        }

        static string GetValueType(string inputValue)
        {           
            if (inputValue.Contains(":") || inputValue.ToLower().Contains("hour") || inputValue.ToLower().Contains("minute"))
                return "TimeSpan";
            else
                return "float";
        }

        static string ExtensionType(string inputValue)
        {
            
            if (inputValue.Contains(":") || inputValue.ToLower().Contains("hour") || inputValue.ToLower().Contains("minute"))
                return ".OWValToTimeSpan();";
            else
                return ".OWValToFloat();";
        }

        static string header = "using OverwatchAPI.Internal;\n" +
                               "using System;\n" +
                               "using System.Collections.Generic;\n\n" +
                               "//-- A U T O   G E N E R A T E D --//\n\n" +
                               "namespace OverwatchAPI.Data\n{\n";

        static string statGroupImplement = "\n\t\tpublic void SendPage(IEnumerable<OverwatchDataTable> tableCollection)\n\t\t{\n" +
                                            "\t\t\tforeach(var item in tableCollection)\n\t\t\t{\n" +
                                            "\t\t\t\tvar prop = GetType().GetProperty(item.Name.Replace(\" \", \"\"));\n" +
                                            "\t\t\t\tif (typeof(IStatModule).IsAssignableFrom(prop.PropertyType))\n\t\t\t\t{\n" +
                                            "\t\t\t\t\tIStatModule statModule = (IStatModule)Activator.CreateInstance(prop.PropertyType);\n" +
                                            "\t\t\t\t\tstatModule.SendTable(item);\n" +
                                            "\t\t\t\t\tprop.SetValue(this, statModule);\n\t\t\t\t}\n\t\t\t}\n\t\t}\n";
    }

    public class OverwatchDataTable
    {
        public string Name { get; set; }
        public Dictionary<string, string> Stats { get; set; }
    }
}

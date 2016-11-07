﻿using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OverwatchAPI.intrnl
{
    public static class StaticVars
    {
        public static Regex playerRankImageRegex = new Regex("(0x\\w*)(?=_)", RegexOptions.Compiled);

        // Ranked portrait definitions for the prestige levels.
        public static Dictionary<string, ushort> prestigeDefinitions = new Dictionary<string, ushort>
        {
            {"0x0250000000000918", 0},
            {"0x0250000000000919", 0},
            {"0x025000000000091A", 0},
            {"0x025000000000091B", 0},
            {"0x025000000000091C", 0},
            {"0x025000000000091D", 0},
            {"0x025000000000091E", 0},
            {"0x025000000000091F", 0},
            {"0x0250000000000920", 0},
            {"0x0250000000000921", 0},
            {"0x0250000000000922", 100},
            {"0x0250000000000924", 100},
            {"0x0250000000000925", 100},
            {"0x0250000000000926", 100},
            {"0x025000000000094C", 100},
            {"0x0250000000000927", 100},
            {"0x0250000000000928", 100},
            {"0x0250000000000929", 100},
            {"0x025000000000092B", 100},
            {"0x0250000000000950", 100},
            {"0x025000000000092A", 200},
            {"0x025000000000092C", 200},
            {"0x0250000000000937", 200},
            {"0x025000000000093B", 200},
            {"0x0250000000000933", 200},
            {"0x0250000000000923", 200},
            {"0x0250000000000944", 200},
            {"0x0250000000000948", 200},
            {"0x025000000000093F", 200},
            {"0x0250000000000951", 200},
            {"0x025000000000092D", 300},
            {"0x0250000000000930", 300},
            {"0x0250000000000934", 300},
            {"0x0250000000000938", 300},
            {"0x0250000000000940", 300},
            {"0x0250000000000949", 300},
            {"0x0250000000000952", 300},
            {"0x025000000000094D", 300},
            {"0x0250000000000945", 300},
            {"0x025000000000093C", 300},
            {"0x025000000000092E", 400},
            {"0x0250000000000931", 400},
            {"0x0250000000000935", 400},
            {"0x025000000000093D", 400},
            {"0x0250000000000946", 400},
            {"0x025000000000094A", 400},
            {"0x0250000000000953", 400},
            {"0x025000000000094E", 400},
            {"0x0250000000000939", 400},
            {"0x0250000000000941", 400},
            {"0x025000000000092F", 500},
            {"0x0250000000000932", 500},
            {"0x025000000000093E", 500},
            {"0x0250000000000936", 500},
            {"0x025000000000093A", 500},
            {"0x0250000000000942", 500},
            {"0x0250000000000947", 500},
            {"0x025000000000094F", 500},
            {"0x025000000000094B", 500},
            {"0x0250000000000954", 500}
        };
    }
}

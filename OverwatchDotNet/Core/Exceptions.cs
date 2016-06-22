using System;

namespace OverwatchAPI.Internal
{
    class UserRegionNotDefinedException : Exception
    {
        static string messageString = "OverwatchPlayer Region is not defined. Use 'DetectRegion()' or set the Region in the constructor.";
        public UserRegionNotDefinedException() : base(messageString) { }
    }

    class InvalidBattletagException : Exception
    {
        static string messageString = "OverwatchPlayer's Battletag is not valid - Format is 'User#1234`";
        public InvalidBattletagException() : base(messageString) { }
    }
}

namespace OverwatchDotNet.Internal
{
    public static class Extensions
    {
        public static float OverwatchValueStringToFloat(this string input)
        {
            input = input.Replace(",", "").Replace(":", "");
            return float.Parse(input);
        }
    }
}

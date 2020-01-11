namespace PropertyManagement.Database
{
    public static class ExtensionMethods
    {
        public static string ReplaceUmlauts(this string inputString)
        {
            return inputString.ToLower().Replace("ue", "ü").Replace("ae", "ä").Replace("oe", "ö");
        }
    }
}

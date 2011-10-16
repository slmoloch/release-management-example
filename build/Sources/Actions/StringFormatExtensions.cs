namespace CommitStage
{
    public static class StringFormatExtensions
    {
        public static string FormatInvariant(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
    }
}
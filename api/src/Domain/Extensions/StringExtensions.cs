namespace SuggestionApi.Domain.Extensions
{
    public static class StringExtensions
    {
        public static string SanitizeInput(this string val)
        {
            //We are not striping diacritics, see ADR-007    
            return val.Trim().Replace("\"", "").ToLower();
        }

        public static bool IsValidInput(this string val)
        {
            if (string.IsNullOrEmpty(val) || val == "\"\"" || val == "\"" || val == "\'" || val == "\'\'")
                return false;
            return true;
        }
    }
}
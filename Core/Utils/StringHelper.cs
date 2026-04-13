namespace Core.Utils
{
    public static class StringHelper
    {
        public static List<string> GetSignificantWords(string text, int minLength = 3)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new List<string>();

            var charsToRemove = new[] { ',', '.', ':', ';', '&', '-' };
            string cleanText = text;

            foreach (var c in charsToRemove)
            {
                cleanText = cleanText.Replace(c.ToString(), " ");
            }

            return cleanText
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Where(w => w.Length > minLength)
                .ToList();
        }
    }
}
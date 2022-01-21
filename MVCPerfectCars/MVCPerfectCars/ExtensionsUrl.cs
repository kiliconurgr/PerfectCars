



using System.Linq;
using System.Text.RegularExpressions;

namespace MVCPerfectCars
{
    public static class ExtensionsUrl
    {
        public static string ToSafeUrl(this string Text) => Regex.Replace(string.Concat(Text.Where(p => char.IsLetterOrDigit(p) || char.IsWhiteSpace(p))), @"\s+", "-").ToLower();
    }
}

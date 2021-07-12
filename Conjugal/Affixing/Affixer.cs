using JetBrains.Annotations;

namespace Conjugal.Affixing {
    /// <summary>
    /// <see cref="string"/> extension methods that apply <a href="https://en.wikipedia.org/wiki/Affix">affixes</a> to stems.
    /// </summary>
    [PublicAPI]
    public static class Affixer {
        public static string Suffix(this string stem, string suffix, string joiner = "") {
            return $"{stem}{joiner}{suffix}";
        }

        public static string Prefix(this string stem, string prefix, string joiner = "") {
            return $"{prefix}{joiner}{stem}";
        }

        public static string Infix(this string stem, string infix, int insertionPoint, string joiner = "") {
            return stem.Insert(insertionPoint, infix.Ambifix(joiner));
        }

        public static string Circumfix(this string stem, string prefix, string suffix, string joiner = "") {
            return $"{prefix}{joiner}{stem}{joiner}{suffix}";
        }

        public static string Ambifix(this string stem, string ambifix, string joiner = "") {
            return stem.Circumfix(ambifix, ambifix, joiner);
        }
    }
}
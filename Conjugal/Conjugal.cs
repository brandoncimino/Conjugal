using System;
using System.Reflection;

using Conjugal.Annotations;

using Humanizer;

namespace Conjugal {
    /// <summary>
    /// Gets <see cref="IConjugal"/> values from <see cref="Annotations"/>.
    /// </summary>
    /// <remarks><a href="https://stackoverflow.com/questions/144833/most-useful-attributes">Most Useful Attributes"</a></remarks>
    public static class Conjugal {
        public static bool IsProperNoun(this Type type) {
            return type.GetCustomAttribute(typeof(ProperNounAttribute)) != null;
        }

        public static string Lemma(this Type type) {
            return type.GetAnnotatedLemma() ?? type.Name.Humanize(type.GetLemmaCase());
        }

        private static LetterCasing GetLemmaCase(this Type type) {
            var casing = type.GetCustomAttribute<CasingAttribute>()?.Casing;
            return casing.GetValueOrDefault(type.IsProperNoun() ? LetterCasing.Title : LetterCasing.LowerCase);
        }

        private static string GetAnnotatedLemma(this MemberInfo type) {
            return type.GetCustomAttribute<LemmaAttribute>()?.Lemma;
        }

        private static string GetUncasedLemma(this MemberInfo type) {
            return type.GetCustomAttribute<LemmaAttribute>()?.Lemma ?? type.Name.Humanize();
        }

        public static bool IsCountable(this Type type) {
            return type.GetCustomAttribute(typeof(UncountableAttribute)) == null;
        }

        public static string Pluralize(this Type type, int? count = default) {
            if (!type.IsCountable()) {
                return type.Lemma();
            }

            var attribute = type.GetCustomAttribute(typeof(PluralAttribute)) as PluralAttribute;
            return attribute?.Plural ?? type.Lemma().Pluralize();
        }

        public static string Plural(this Type type) {
            return type.GetCustomAttribute<PluralAttribute>()?.Plural ?? type.Name.Pluralize();
        }
    }
}
using System;
using System.ComponentModel;
using System.Reflection;

using FowlFever.Conjugal.Annotations;

using Humanizer;

using JetBrains.Annotations;

namespace FowlFever.Conjugal {
    /// <summary>
    /// Gets <see cref="IConjugal"/> values from <see cref="Annotations"/>.
    /// </summary>
    /// <remarks><a href="https://stackoverflow.com/questions/144833/most-useful-attributes">Most Useful Attributes"</a></remarks>
    [PublicAPI]
    public static class ConjugalTypeExtensions {
        /// <param name="type">this <see cref="Type"/></param>
        /// <returns>the value of <see cref="ProperNounAttribute"/>.<see cref="ProperNounAttribute.IsProperNoun"/> if set; otherwise, false</returns>
        public static bool IsProperNoun(this Type type) {
            return type.GetCustomAttribute(typeof(ProperNounAttribute)) != null;
        }

        /// <summary>
        /// Gets the <a href="https://en.wikipedia.org/wiki/Lemma_(morphology)">lemma</a> of this class:
        /// <ul>
        /// <li>If <see cref="LemmaAttribute"/> is present, returns <see cref="GetAnnotatedLemma"/></li>
        /// <li>Otherwise, returns <see cref="GetDefaultLemma"/></li>
        /// </ul>
        /// </summary>
        /// <param name="type">this <see cref="Type"/></param>
        /// <returns><see cref="GetAnnotatedLemma"/> or <see cref="GetDefaultLemma"/></returns>
        public static string Lemma(this Type type) {
            return type.GetAnnotatedLemma() ?? GetDefaultLemma(type);
        }

        private static string GetDefaultLemma(Type type) {
            return type.Name.Humanize(type.GetLemmaCase());
        }

        private static LetterCasing GetLemmaCase(this Type type) {
            var casing = type.GetCustomAttribute<PreferredCasing>()?.Casing;
            return casing.GetValueOrDefault(type.IsProperNoun() ? LetterCasing.Title : LetterCasing.LowerCase);
        }

        private static string GetAnnotatedLemma(this MemberInfo type) {
            return type.GetCustomAttribute<LemmaAttribute>()?.Lemma;
        }

        /// <param name="type">this <see cref="Type"/></param>
        /// <returns>the <see cref="CountabilityAttribute"/>.<see cref="CountabilityAttribute.Countability"/> if set; otherwise, <see cref="CountabilityAttribute.Default"/></returns>
        public static Countability Countability(this Type type) {
            return type.GetCustomAttribute<CountabilityAttribute>()?.Countability ?? CountabilityAttribute.Default;
        }

        public static string Singular(this Type type) {
            return type.GetCustomAttribute<SingularAttribute>()?.Singular ?? type.Lemma();
        }

        /// <summary>
        /// Gets <b>either</b> the <see cref="IConjugal.Plural"/> <b>or</b> <see cref="IConjugal.Singular"/> form of the given <see cref="Type"/>,
        /// based on the given <see cref="quantity"/>.
        /// </summary>
        /// <param name="type">this <see cref="Type"/></param>
        /// <param name="quantity">the actual quantity of items, to decide whether the <see cref="IConjugal.Plural"/> or <see cref="IConjugal.Singular"/> should be used. If not set, the <see cref="IConjugal.Plural"/> will be returned</param>
        /// <returns>either the <see cref="IConjugal.Singular"/> or <see cref="IConjugal.Plural"/></returns>
        public static string Pluralize(this Type type, int? quantity = default) {
            var countability = type.Countability();
            return countability switch {
                Conjugal.Countability.Uncountable => type.Singular(),
                Conjugal.Countability.Countable   => quantity.GetValueOrDefault(0) == 1 ? type.Singular() : type.Plural(),
                Conjugal.Countability.Collective  => throw new NotImplementedException($"Not implemented: {Conjugal.Countability.Collective}"),
                _                                 => throw new InvalidEnumArgumentException(nameof(countability), (int) countability, typeof(Countability))
            };
        }

        /// <param name="type"></param>
        /// <returns>the value of the <see cref="PluralAttribute"/>, if set; otherwise, <see cref="InflectorExtensions.Pluralize"/>s the <see cref="Singular"/> form via <see cref="Humanizer"/>.</returns>
        public static string Plural(this Type type) {
            return type.GetCustomAttribute<PluralAttribute>()?.Plural ?? type.Singular().Pluralize();
        }

        /// <param name="type"></param>
        /// <remarks>Not sure when I'd actually use this, to be honest.</remarks>
        /// <returns>the value of the <see cref="NounalVerbAttribute"/>, if set; otherwise <see cref="Lemma"/></returns>
        public static string NounalVerb(this Type type) {
            return type.GetCustomAttribute<NounalVerbAttribute>()?.Verb ?? type.Lemma();
        }

        [CanBeNull]
        public static UnitOfMeasure? UnitOfMeasure(this Type type) {
            return type.GetCustomAttribute<UnitOfMeasureAttribute>()?.UnitOfMeasure;
        }

        public static string Abbreviation(this Type type) {
            return type.GetCustomAttribute<AbbreviationAttribute>()?.SingularAbbreviation ?? type.Lemma();
        }

        public static string PluralAbbreviation(this Type type) {
            return type.GetCustomAttribute<AbbreviationAttribute>()?.PluralAbbreviation ?? type.Abbreviation();
        }

        public static string Abbreviate(this Type type, int? quantity = default) {
            return quantity.GetValueOrDefault(0) == 1 ? type.Abbreviation() : type.PluralAbbreviation();
        }

        public static LetterCasing? PreferredCasing(this Type type) {
            return type.GetCustomAttribute<PreferredCasing>()?.Casing ?? (type.IsProperNoun() ? LetterCasing.Title : default);
        }

        public static string Quantify(this Type type, double quantity) {
            var uom = type.UnitOfMeasure();
            return uom.HasValue ? new QuanticString(quantity, uom.Value) : type.Lemma().ToQuantity(quantity);
        }
    }
}
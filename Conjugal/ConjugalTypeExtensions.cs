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
        /// <li>Otherwise, returns <see cref="GetFallbackLemma"/></li>
        /// </ul>
        /// </summary>
        /// <param name="type">this <see cref="Type"/></param>
        /// <returns><see cref="GetAnnotatedLemma"/> or <see cref="GetFallbackLemma"/></returns>
        public static string Lemma(this Type type) {
            return type.GetAnnotatedLemma() ?? GetFallbackLemma(type);
        }

        /// <summary>
        /// Gets the explicitly annotated lemma
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string GetAnnotatedLemma(this MemberInfo type) {
            return type.GetCustomAttribute<LemmaAttribute>()?.Lemma;
        }

        /// <summary>
        /// The fallback value for <see cref="Lemma"/> when <see cref="GetAnnotatedLemma"/> is not defined
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string GetFallbackLemma(Type type) {
            // Tip: ?? is equivalent to saying ".GetValueOrDefault()", which is equivalent to Java's ".orElse()"
            return type.Name.Humanize(type.PreferredCasing() ?? LetterCasing.LowerCase);
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
        /// <remarks>0 is considered a <b>plural <paramref name="quantity"/>!</b></remarks>
        /// <param name="type">this <see cref="Type"/></param>
        /// <param name="quantity">the actual quantity of items, to decide whether the <see cref="IConjugal.Plural"/> or <see cref="IConjugal.Singular"/> should be used. If not set, the <see cref="IConjugal.Plural"/> will be returned</param>
        /// <returns>either the <see cref="IConjugal.Singular"/> or <see cref="IConjugal.Plural"/></returns>
        /// <seealso cref="PlurableExtensions.Pluralize(FowlFever.Conjugal.IPlurable,System.Nullable{int})"/>
        /// <seealso cref="PlurableExtensions.Pluralize(FowlFever.Conjugal.IPlurable,System.Nullable{double})"/>
        /// <seealso cref="Pluralize(System.Type,System.Nullable{int})"/>
        /// <seealso cref="Pluralize(System.Type,System.Nullable{double})"/>
        public static string Pluralize(this Type type, int? quantity = default) {
            return new Conjugation(type).Pluralize(quantity);
        }

        /// <inheritdoc cref="Pluralize(System.Type,System.Nullable{int})"/>
        public static string Pluralize(this Type type, double? quantity) {
            return new Conjugation(type).Pluralize(quantity);
        }

        /// <param name="type"></param>
        /// <returns>the value of the <see cref="PluralAttribute"/>, if set; otherwise, <see cref="InflectorExtensions.Pluralize"/>s the <see cref="Singular"/> form via <see cref="Humanizer"/>.</returns>
        public static string Plural(this Type type) {
            return type.GetCustomAttribute<PluralAttribute>()?.Plural ?? PluralFromCountability(type);
        }

        /// <summary>
        /// Gets a <see cref="IPlurable.Plural"/> form based on the <see cref="Countability"/>.
        /// Used as a fallback when <see cref="PluralAttribute"/> isn't explicitly set.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        private static string PluralFromCountability(Type type) {
            return type.Singular().PluralFromCountability(type.Countability());
        }

        /// <param name="type"></param>
        /// <remarks>Not sure when I'd actually use this, to be honest.</remarks>
        /// <returns>the value of the <see cref="NounalVerbAttribute"/>, if set; otherwise <see cref="Lemma"/></returns>
        public static string NounalVerb(this Type type) {
            return type.GetCustomAttribute<NounalVerbAttribute>()?.Verb ?? type.Lemma();
        }

        /// <param name="type">this <see cref="Type"/></param>
        /// <returns><see cref="UnitOfMeasureAttribute"/>.<see cref="UnitOfMeasureAttribute.UnitOfMeasure"/>, if present</returns>
        [CanBeNull]
        public static UnitOfMeasure? UnitOfMeasure(this Type type) {
            return type.GetCustomAttribute<UnitOfMeasureAttribute>()?.UnitOfMeasure;
        }

        public static Plurable? Abbreviation(this Type type) {
            return type.GetCustomAttribute<AbbreviationAttribute>()?.Abbreviation;
        }

        public static string Abbreviate(this Type type, int? quantity = default) {
            return type.Abbreviation()?.Pluralize(quantity);
        }

        public static LetterCasing? PreferredCasing(this Type type) {
            return type.GetCustomAttribute<PreferredCasingAttribute>()?.Casing ?? (type.IsProperNoun() ? LetterCasing.Title : default);
        }

        public static QuanticString Quantify(this Type type, int quantity) {
            return new Conjugation(type).Quantify(quantity);
        }

        public static QuanticString Quantify(this Type type, double quantity) {
            return new Conjugation(type).Quantify(quantity);
        }
    }
}
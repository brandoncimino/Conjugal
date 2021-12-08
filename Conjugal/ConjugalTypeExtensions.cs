using System;
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
        /// <returns>the <see cref="CountabilityAttribute"/>.<see cref="CountabilityAttribute.Countability"/>, if set; otherwise, <see cref="Plurable.InferCountability">InferCountability</see>(<see cref="Singular">Singular</see>, <see cref="Plural">Plural</see>)</returns>
        public static Countability Countability(this Type type) {
            return type.GetCustomAttribute<CountabilityAttribute>()?.Countability ?? Plurable.InferCountability(type.Singular(), type.Plural());
        }

        /// <param name="type">this <see cref="Type"/></param>
        /// <returns>the <see cref="SingularAttribute"/>.<see cref="SingularAttribute.Singular"/>, if set; otherwise, the <see cref="Lemma"/></returns>
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
            return type.GetCustomAttribute<PluralAttribute>()?.Plural ?? type.Singular().PluralFromCountability(type.Countability());
        }

        /// <param name="type"></param>
        /// <remarks>Not sure when I'd actually use this, to be honest.</remarks>
        /// <returns>the value of the <see cref="NounalVerbAttribute"/>, if set; otherwise <see cref="Lemma"/></returns>
        public static string NounalVerb(this Type type) {
            return type.GetCustomAttribute<NounalVerbAttribute>()?.Verb ?? type.Lemma();
        }

        /// <param name="type">this <see cref="Type"/></param>
        /// <returns><see cref="UnitOfMeasureAttribute"/>.<see cref="UnitOfMeasureAttribute.UnitOfMeasure"/>, if present</returns>
        public static UnitOfMeasure? UnitOfMeasure(this Type type) {
            return type.GetCustomAttribute<UnitOfMeasureAttribute>()?.UnitOfMeasure;
        }

        /// <param name="type">this <see cref="Type"/></param>
        /// <returns><see cref="AbbreviationAttribute"/>.<see cref="PlurableWrapperAttribute.Value"/></returns>
        public static Plurable? Abbreviation(this Type type) {
            return type.GetCustomAttribute<AbbreviationAttribute>()?.Value;
        }

        /// <param name="type">this <paramref name="type"/></param>
        /// <param name="quantity">a quantity, used to decide between the <see cref="Abbreviation"/>'s <see cref="IPlurable.Singular"/> and <see cref="IPlurable.Plural"/> forms</param>
        /// <returns>this type's pluralized <see cref="Abbreviation"/>, if present; otherwise, <c>null</c></returns>
        [CanBeNull]
        public static string Abbreviate(this Type type, int? quantity = default) {
            return type.Abbreviation()?.Pluralize(quantity);
        }

        /// <param name="type">this <see cref="Type"/></param>
        /// <returns><see cref="PreferredCasingAttribute"/>.<see cref="PreferredCasingAttribute.Casing"/></returns>
        public static LetterCasing? PreferredCasing(this Type type) {
            return type.GetCustomAttribute<PreferredCasingAttribute>()?.Casing ?? (type.IsProperNoun() ? LetterCasing.Title : default);
        }

        public static QuanticString Quantify(this Type type, int quantity) {
            return new Conjugation(type).Quantify(quantity);
        }

        public static QuanticString Quantify(this Type type, double quantity) {
            return new Conjugation(type).Quantify(quantity);
        }

        /// <param name="type"></param>
        /// <returns><see cref="CollectiveNounAttribute"/>.<see cref="PlurableWrapperAttribute.Value"/></returns>
        public static Plurable? CollectiveNoun(this Type type) {
            return type.GetCustomAttribute<CollectiveNounAttribute>()?.Value;
        }
    }
}
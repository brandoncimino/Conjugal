using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

namespace FowlFever.Conjugal.Affixing {
    /// <summary>
    /// <see cref="string"/> extension methods that apply <a href="https://en.wikipedia.org/wiki/Affix">affixes</a> to stems.
    /// </summary>
    [PublicAPI]
    public static class Affixer {
        #region Simple Strings

        /// <summary>
        /// Appends <paramref name="suffix"/> to <paramref name="stem"/>.
        /// </summary>
        /// <param name="stem">the original, "base" <see cref="string"/></param>
        /// <param name="suffix">the string to be appended to <paramref name="stem"/></param>
        /// <param name="joiner">a <see cref="string"/> interposed betwixt <paramref name="stem"/> and <paramref name="suffix"/></param>
        /// <returns>the <see cref="AffixFlavor.Suffix"/>ed <see cref="string"/></returns>
        public static string Suffix(this string? stem, string? suffix, string? joiner = default) {
            return $"{stem}{joiner}{suffix}";
        }

        /// <summary>
        /// Prepends <paramref name="prefix"/> to <paramref name="stem"/>.
        /// </summary>
        /// <param name="stem">the original, "base" <see cref="string"/></param>
        /// <param name="prefix">the string to be prepended to <paramref name="stem"/></param>
        /// <param name="joiner">a <see cref="string"/> interposed betwixt <paramref name="stem"/> and <paramref name="prefix"/></param>
        /// <returns>the <see cref="AffixFlavor.Prefix"/>ed <see cref="string"/></returns>
        public static string Prefix(this string? stem, string? prefix, string? joiner = default) {
            return $"{prefix}{joiner}{stem}";
        }

        /**
         * <inheritdoc cref="AffixFlavor.Infix"/>
         */
        public static string Infix(this string? stem, string? infix, int insertionPoint, string? joiner = default) {
            return stem?.Insert(insertionPoint, infix.Ambifix(joiner)) ?? "";
        }

        /**
         * <inheritdoc cref="AffixFlavor.Circumfix"/>
         */
        public static string Circumfix(this string? stem, string? prefix, string? suffix, string? joiner = default) {
            return $"{prefix}{joiner}{stem}{joiner}{suffix}";
        }

        /**
         * <inheritdoc cref="AffixFlavor.Ambifix"/>
         */
        public static string Ambifix(this string? stem, string? ambifix, string? joiner = default) {
            return stem.Circumfix(ambifix, ambifix, joiner);
        }

        #endregion

        #region IEnumerable<string>

        /**
         * <inheritdoc cref="AffixFlavor.Suffix"/>
         */
        public static IEnumerable<string> Suffix(
            this IEnumerable<string?>? stems,
            string?                    suffix,
            string?                    joiner = default
        ) {
            return stems?.Select(it => it.Suffix(suffix, joiner)) ?? Enumerable.Empty<string>();
        }

        /**
         * <inheritdoc cref="AffixFlavor.Prefix"/>
         */
        public static IEnumerable<string> Prefix(
            this IEnumerable<string?>? stems,
            string?                    prefix,
            string?                    joiner = default
        ) {
            return stems?.Select(it => it.Prefix(prefix, joiner)) ?? Enumerable.Empty<string>();
        }

        #endregion

        #region Affixations

        /// <summary>
        /// Constructs a new <see cref="AffixFlavor.Suffix"/>-flavored <see cref="Affixation"/>
        /// </summary>
        /// <param name="stem">the <see cref="Affixation.Stem"/></param>
        /// <param name="suffix">the <see cref="Affixation.BoundMorpheme"/></param>
        /// <param name="joiner">the <see cref="Affixation.Joiner"/></param>
        /// <returns>a new <see cref="AffixFlavor.Suffix"/>-flavored <see cref="Affixation"/></returns>
        public static Affixation Suffixation(this string stem, string suffix, string joiner = default) {
            return Affixation.Suffixation(stem, suffix, joiner);
        }

        /// <summary>
        /// Constructs a new <see cref="AffixFlavor.Prefix"/>-flavored <see cref="Affixation"/>
        /// </summary>
        /// <param name="stem">the <see cref="Affixation.Stem"/></param>
        /// <param name="prefix">the <see cref="Affixation.BoundMorpheme"/></param>
        /// <param name="joiner">the <see cref="Affixation.Joiner"/></param>
        /// <returns>a new <see cref="AffixFlavor.Prefix"/>-flavored <see cref="Affixation"/></returns>
        public static Affixation Prefixation(this string stem, string prefix, string joiner = "") {
            return Affixation.Prefixation(stem, prefix, joiner);
        }

        /// <summary>
        /// Constructs a new <see cref="AffixFlavor.Infix"/>-flavored <see cref="Affixation"/>
        /// </summary>
        /// <param name="stem">the <see cref="Affixation.Stem"/></param>
        /// <param name="infix">the <see cref="Affixation.BoundMorpheme"/></param>
        /// <param name="insertionPoint">the <see cref="Affixation.Index"/> in the <see cref="Affixation.Stem"/></param>
        /// <param name="joiner">the <see cref="Affixation.Joiner"/></param>
        /// <returns>an <see cref="AffixFlavor.Infix"/>-flavored <see cref="Affixation"/></returns>
        public static Affixation Infixation(this string stem, string infix, int insertionPoint, string joiner = "") {
            return Affixation.Infixation(stem, infix, insertionPoint, joiner);
        }

        /// <summary>
        /// Constructs a new <see cref="AffixFlavor.Circumfix"/>-flavored <see cref="Affixation"/>
        /// </summary>
        /// <param name="stem">the <see cref="Affixation.Stem"/></param>
        /// <param name="prefix">the <see cref="Affixation.BoundMorpheme"/> portion <i>before</i> the <see cref="Affixation.Stem"/></param>
        /// <param name="suffix">the <see cref="Affixation.BoundMorpheme"/> portion <i>after</i> the <see cref="Affixation.Stem"/></param>
        /// <param name="joiner">the <see cref="Affixation.Joiner"/></param>
        /// <returns>a new <see cref="AffixFlavor.Circumfix"/>-flavored <see cref="Affixation"/></returns>
        public static Affixation Circumfixation(this string stem, string prefix, string suffix, string joiner = default) {
            return Affixation.Circumfixation(stem, prefix, suffix, joiner);
        }

        /// <summary>
        /// Constructs a new <see cref="AffixFlavor.Ambifix"/>-flavored <see cref="Affixation"/>
        /// </summary>
        /// <param name="stem">the <see cref="Affixation.Stem"/></param>
        /// <param name="ambifix">the <see cref="Affixation.BoundMorpheme"/></param>
        /// <param name="joiner">the <see cref="Affixation.Joiner"/></param>
        /// <returns>a new <see cref="AffixFlavor.Ambifix"/>-flavored <see cref="Affixation"/></returns>
        public static Affixation Ambifixation(this string stem, string ambifix, string joiner = default) {
            return Affixation.Ambifixation(stem, ambifix, joiner);
        }

        #endregion
    }
}
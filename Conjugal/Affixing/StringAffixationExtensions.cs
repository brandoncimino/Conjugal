using System;

using JetBrains.Annotations;

namespace FowlFever.Conjugal.Affixing {
    /// <summary>
    /// <see cref="string"/> extension methods that apply <a href="https://en.wikipedia.org/wiki/Affix">affixes</a> to stems.
    /// </summary>
    [PublicAPI]
    public static class Affixer {
        #region Simple Strings

        /// <summary>
        /// Appends <paramref name="suffix"/> to <paramref name="stem"/>, returning a <see cref="AffixFlavor.Suffix"/>-flavored <see cref="Affixation"/>
        /// </summary>
        /// <param name="stem">the original, "base" <see cref="string"/> <i>(🏫 <see cref="Affixation.Stem"/>)</i></param>
        /// <param name="suffix">the <see cref="string"/> to be appended to <paramref name="stem"/> <i>(🏫 <see cref="Affixation.BoundMorpheme"/>)</i></param>
        /// <param name="joiner">a <see cref="string"/> interposed betwixt <paramref name="stem"/> and <paramref name="suffix"/> <i>(🏫 <see cref="Affixation.Joiner"/>)</i></param>
        /// <returns>the <see cref="AffixFlavor.Suffix"/>ed <see cref="Affixation.Suffixation"/></returns>
        public static Affixation Suffix(this string? stem, string? suffix, string? joiner = Affixation.DefaultJoiner) {
            return Affixation.Suffixation(stem, suffix, joiner);
        }

        /// <summary>
        /// Prepends <paramref name="prefix"/> to <paramref name="stem"/>.
        /// </summary>
        /// <param name="stem">the original, "base" <see cref="string"/> <i>(🏫 <see cref="Affixation.Stem"/>)</i></param>
        /// <param name="prefix">the <see cref="string"/> to be appended to <paramref name="stem"/> <i>(🏫 <see cref="Affixation.BoundMorpheme"/>)</i></param>
        /// <param name="joiner">a <see cref="string"/> interposed betwixt <paramref name="stem"/> and <paramref name="prefix"/> <i>(🏫 <see cref="Affixation.Joiner"/>)</i></param>
        /// <returns>the <see cref="AffixFlavor.Prefix"/>ed <see cref="Affixation.Prefixation"/></returns>
        public static Affixation Prefix(this string? stem, string? prefix, string? joiner = Affixation.DefaultJoiner) {
            return Affixation.Prefixation(stem, prefix, joiner);
        }

        /**
         * <inheritdoc cref="AffixFlavor.Infix"/>
         */
        [Obsolete("Please use Infixation")]
        public static Affixation Infix(this string? stem, string? infix, int insertionPoint, string? joiner = Affixation.DefaultJoiner) {
            return Affixation.Infixation(stem, infix, insertionPoint, joiner);
        }

        /**
         * <inheritdoc cref="AffixFlavor.Circumfix"/>
         */
        public static Affixation Circumfix(this string? stem, string? prefix, string? suffix, string? joiner = Affixation.DefaultJoiner) {
            return Affixation.Circumfixation(stem, prefix, suffix, joiner);
        }

        /**
         * <inheritdoc cref="AffixFlavor.Ambifix"/>
         */
        public static Affixation Ambifix(this string? stem, string? ambifix, string? joiner = Affixation.DefaultJoiner) {
            return Affixation.Ambifixation(stem, ambifix, joiner);
        }

        #endregion
    }
}
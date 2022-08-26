using System;

using JetBrains.Annotations;

namespace FowlFever.Conjugal.Affixing;

/// <summary>
/// <see cref="string"/> extension methods that apply <a href="https://en.wikipedia.org/wiki/Affix">affixes</a> to stems.
/// </summary>
[PublicAPI]
public static class StringAffixationExtensions {
    #region Simple Strings

    /// <summary>
    /// Appends <paramref name="suffix"/> to <paramref name="stem"/>, returning a <see cref="AffixFlavor.Suffix"/>-flavored <see cref="Affixation"/>
    /// </summary>
    /// <param name="stem">the original, "base" <see cref="string"/> <i>(🏫 <see cref="Affixation.Stem"/>)</i></param>
    /// <param name="suffix">the <see cref="string"/> to be appended to <paramref name="stem"/> <i>(🏫 <see cref="Affixation.BoundMorpheme"/>)</i></param>
    /// <param name="joiner">a <see cref="string"/> interposed betwixt <paramref name="stem"/> and <paramref name="suffix"/> <i>(🏫 <see cref="Affixation.Joiner"/>)</i></param>
    /// <returns>the <see cref="AffixFlavor.Suffix"/>ed <see cref="Affixation.Suffixation"/></returns>
    [Pure]
    public static Affixation Suffix(this string? stem, ReadOnlySpan<char> suffix, ReadOnlySpan<char> joiner = default) {
        return Affixation.Suffixation(stem, suffix, joiner);
    }

    /// <inheritdoc cref="Suffix(string?,System.ReadOnlySpan{char},System.ReadOnlySpan{char})"/>
    [Pure]
    public static Affixation Suffix(this ReadOnlySpan<char> stem, ReadOnlySpan<char> suffix, ReadOnlySpan<char> joiner = default) {
        return Affixation.Suffixation(stem, suffix, joiner);
    }

    /// <summary>
    /// Prepends <paramref name="prefix"/> to <paramref name="stem"/>.
    /// </summary>
    /// <param name="stem">the original, "base" <see cref="string"/> <i>(🏫 <see cref="Affixation.Stem"/>)</i></param>
    /// <param name="prefix">the <see cref="string"/> to be appended to <paramref name="stem"/> <i>(🏫 <see cref="Affixation.BoundMorpheme"/>)</i></param>
    /// <param name="joiner">a <see cref="string"/> interposed betwixt <paramref name="stem"/> and <paramref name="prefix"/> <i>(🏫 <see cref="Affixation.Joiner"/>)</i></param>
    /// <returns>the <see cref="AffixFlavor.Prefix"/>ed <see cref="Affixation.Prefixation"/></returns>
    [Pure]
    public static Affixation Prefix(this string? stem, ReadOnlySpan<char> prefix, ReadOnlySpan<char> joiner = default) {
        return Affixation.Prefixation(stem, prefix, joiner);
    }

    /// <inheritdoc cref="Prefix(string?,System.ReadOnlySpan{char},System.ReadOnlySpan{char})"/>
    [Pure]
    public static Affixation Prefix(this ReadOnlySpan<char> stem, ReadOnlySpan<char> prefix, ReadOnlySpan<char> joiner = default) {
        return Affixation.Prefixation(stem, prefix, joiner);
    }

    /// <inheritdoc cref="AffixFlavor.Infix"/>
    [Pure]
    public static Affixation Infix(this string? stem, ReadOnlySpan<char> infix, Index insertionPoint, ReadOnlySpan<char> joiner = default) {
        return Affixation.Infixation(stem, infix, insertionPoint, joiner);
    }

    /// <inheritdoc cref="AffixFlavor.Infix"/>
    [Pure]
    public static Affixation Infix(this ReadOnlySpan<char> stem, ReadOnlySpan<char> infix, Index insertionPoint, ReadOnlySpan<char> joiner = default) {
        return Affixation.Infixation(stem, infix, insertionPoint, joiner);
    }

    /// <inheritdoc cref="AffixFlavor.Circumfix"/>
    [Pure]
    public static Affixation Circumfix(this string? stem, ReadOnlySpan<char> prefix, ReadOnlySpan<char> suffix, ReadOnlySpan<char> joiner = default) {
        return Affixation.Circumfixation(stem, prefix, suffix, joiner);
    }

    /// <inheritdoc cref="Circumfix(string?,System.ReadOnlySpan{char},System.ReadOnlySpan{char},System.ReadOnlySpan{char})"/>
    [Pure]
    public static Affixation Circumfix(this ReadOnlySpan<char> stem, ReadOnlySpan<char> prefix, ReadOnlySpan<char> suffix, ReadOnlySpan<char> joiner = default) {
        return Affixation.Circumfixation(stem, prefix, suffix, joiner);
    }

    /// <inheritdoc cref="AffixFlavor.Ambifix"/>
    [Pure]
    public static Affixation Ambifix(this string? stem, ReadOnlySpan<char> ambifix, ReadOnlySpan<char> joiner = default) {
        return Affixation.Ambifixation(stem, ambifix, joiner);
    }

    /// <inheritdoc cref="AffixFlavor.Ambifix"/>
    [Pure]
    public static Affixation Ambifix(this ReadOnlySpan<char> stem, ReadOnlySpan<char> ambifix, ReadOnlySpan<char> joiner = default) {
        return Affixation.Ambifixation(stem, ambifix, joiner);
    }

    /// <inheritdoc cref="AffixFlavor.Duplifix"/>
    [Pure]
    public static Affixation Duplifix(this string? stem, ReadOnlySpan<char> joiner = default) {
        return Affixation.Duplifixation(stem, joiner);
    }

    /// <inheritdoc cref="AffixFlavor.Duplifix"/>
    [Pure]
    public static Affixation Duplifix(this ReadOnlySpan<char> stem, ReadOnlySpan<char> joiner = default) {
        return Affixation.Duplifixation(stem, joiner);
    }

    #endregion
}
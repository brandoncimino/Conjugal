using System;

namespace FowlFever.Conjugal.Affixing;

/// <summary>
/// Static factory methods for creating <see cref="AffixRef{T}"/> instances.
/// </summary>
public static class Affix {
    /// <inheritdoc cref="Affixing.Prefix"/>
    public static AffixRef<IPrefix> Prefix(ReadOnlySpan<char> prefix, ReadOnlySpan<char> joiner = default) {
        return new AffixRef<IPrefix>() {
            BoundMorpheme = prefix,
            Joiner        = joiner
        };
    }

    /// <inheritdoc cref="Affixing.Suffix"/>
    public static AffixRef<ISuffix> Suffix(ReadOnlySpan<char> suffix, ReadOnlySpan<char> joiner = default) {
        return new AffixRef<ISuffix>() {
            SuffixMorpheme = suffix,
            Joiner         = joiner,
            InsertionPoint = Index.End,
        };
    }

    /// <inheritdoc cref="Affixing.Infix"/>
    public static AffixRef<IInfix> Infix(ReadOnlySpan<char> infix, Index insertionPoint, ReadOnlySpan<char> joiner = default) {
        return new AffixRef<IInfix>() {
            BoundMorpheme  = infix,
            InsertionPoint = insertionPoint,
            Joiner         = joiner
        };
    }

    /// <inheritdoc cref="Affixing.Circumfix"/>
    public static AffixRef<ICircumfix> Circumfix(ReadOnlySpan<char> prefix, ReadOnlySpan<char> suffix, ReadOnlySpan<char> joiner = default) {
        return new AffixRef<ICircumfix>() {
            BoundMorpheme  = prefix,
            SuffixMorpheme = suffix,
            Joiner         = joiner
        };
    }

    /// <inheritdoc cref="Affixing.Ambifix"/>
    public static AffixRef<IAmbifix> Ambifix(ReadOnlySpan<char> ambifix, ReadOnlySpan<char> joiner = default) => new() {
        BoundMorpheme  = ambifix,
        SuffixMorpheme = ambifix,
        Joiner         = joiner
    };

    /// <inheritdoc cref="Affixing.Duplifix"/>
    public static AffixRef<IDuplifix> Duplifix(ReadOnlySpan<char> joiner) => new() {
        Joiner         = joiner,
        InsertionPoint = Index.End,
    };
}
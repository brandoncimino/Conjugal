using System;

using JetBrains.Annotations;

namespace FowlFever.Conjugal.Affixing;

/// <summary>
/// Holds the 2 <see cref="ReadOnlySpan{T}"/>s that correspond to a <see cref="AffixFlavor.Circumfix"/>'s <see cref="IPrefix.GetPrefix"/> and <see cref="ISuffix.GetSuffix"/>.
/// </summary>
public readonly ref struct CircumSpans {
    /// <inheritdoc cref="IPrefix.GetPrefix"/>
    public ReadOnlySpan<char> Prefix { get; init; }
    /// <inheritdoc cref="ISuffix.GetSuffix"/>
    public ReadOnlySpan<char> Suffix { get; init; }

    /// <param name="prefix"><see cref="Prefix"/></param>
    /// <param name="suffix"><see cref="Suffix"/></param>
    public CircumSpans(ReadOnlySpan<char> prefix, ReadOnlySpan<char> suffix) {
        Prefix = prefix;
        Suffix = suffix;
    }

    /// <param name="prefix"><see cref="Prefix"/></param>
    /// <param name="suffix"><see cref="Suffix"/></param>
    public void Deconstruct(out ReadOnlySpan<char> prefix, out ReadOnlySpan<char> suffix) {
        prefix = Prefix;
        suffix = Suffix;
    }
}

/// <summary>
/// Property accessors for <see cref="AffixRef{TFlavor}"/>, which are based on its <see cref="IAffix{T}"/> type parameter.
/// </summary>
/// <remarks>
/// This allows us to pretend that <see cref="AffixRef{TFlavor}"/> has interfaces and inheritance by using type constraints.
/// </remarks>
public static class AffixRefExtensions {
    #region Properties

    #region BoundMorpheme

    /// <inheritdoc cref="IPrefix.GetPrefix"/>
    [Pure]
    public static ReadOnlySpan<char> GetBoundMorpheme(this AffixRef<IPrefix> prefix) => prefix.BoundMorpheme;

    /// <inheritdoc cref="ISuffix.GetSuffix"/>
    [Pure]
    public static ReadOnlySpan<char> GetBoundMorpheme(this AffixRef<ISuffix> suffix) => suffix.SuffixMorpheme;

    /// <inheritdoc cref="IAffix.BoundMorpheme"/>
    [Pure]
    public static ReadOnlySpan<char> GetBoundMorpheme(this AffixRef<IInfix> infix) => infix.BoundMorpheme;

    /// <inheritdoc cref="IAffix.BoundMorpheme"/>
    [Pure]
    public static ReadOnlySpan<char> GetBoundMorpheme(this AffixRef<IDuplifix> duplifix) => duplifix.SuffixMorpheme;

    /// <param name="circumfix">this <see cref="AffixFlavor.Circumfix"/>-flavored <see cref="AffixRef{TFlavor}"/></param>
    /// <typeparam name="T">an <see cref="ICircumfix"/>-derived type</typeparam>
    /// <returns>(<see cref="IPrefix.GetPrefix"/>, <see cref="ISuffix.GetSuffix"/>)</returns>
    public static CircumSpans GetBoundMorphemes<T>(this AffixRef<T> circumfix) where T : IAffix<T>, IAffix<ICircumfix> => new(circumfix.BoundMorpheme, circumfix.SuffixMorpheme);

    #endregion

    /// <inheritdoc cref="IPrefix.GetPrefix"/>
    [Pure]
    public static ReadOnlySpan<char> GetPrefix<T>(this AffixRef<T> prefix) where T : IAffix<T>, IAffix<IPrefix> => prefix.BoundMorpheme;

    /// <inheritdoc cref="ISuffix.GetSuffix"/>
    [Pure]
    public static ReadOnlySpan<char> GetSuffix<T>(this AffixRef<T> suffix) where T : IAffix<T>, IAffix<ISuffix> => suffix.SuffixMorpheme;

    /// <inheritdoc cref="IInfix.InsertionPoint"/>
    public static Index GetInsertionPoint<T>(this AffixRef<T> infix) where T : IAffix<IInfix>, IAffix<T> => infix.InsertionPoint;

    /// <inheritdoc cref="IAffix.Joiner"/>
    [Pure]
    public static ReadOnlySpan<char> GetJoiner<T>(this AffixRef<T> affixRef) where T : IAffix<T> => affixRef.Joiner;

    #endregion

    #region Conversions

    /// <summary>
    /// Converts this <see cref="AffixRef{TFlavor}"/> to an instance of its <typeparamref name="TFlavor"/> parameter.
    /// </summary>
    /// <param name="affixRef">this <see cref="AffixRef{TFlavor}"/></param>
    /// <typeparam name="TFlavor">a type of <see cref="IAffix"/></typeparam>
    /// <returns>an instance of <typeparamref name="TFlavor"/></returns>
    /// <exception cref="ArgumentException">if we don't know how to derive an instance of <typeparamref name="TFlavor"/></exception>
    private static TFlavor ToAffix<TFlavor>(this AffixRef<TFlavor> affixRef) where TFlavor : IAffix<TFlavor> => throw new InvalidOperationException("This method exists only for us with <inheritdoc>");

    /// <inheritdoc cref="ToAffix{TFlavor}"/>
    [MustUseReturnValue]
    public static IPrefix ToPrefix<T>(this AffixRef<T> affixRef) where T : IAffix<T>, IAffix<IPrefix> => new Prefix(affixRef.BoundMorpheme.ToString(), affixRef.Joiner.ToString());

    /// <inheritdoc cref="ToAffix{TFlavor}"/>
    [MustUseReturnValue]
    public static ISuffix ToSuffix<T>(this AffixRef<T> affixRef) where T : IAffix<T>, IAffix<ISuffix> => new Suffix(affixRef.GetSuffix().ToString(), affixRef.Joiner.ToString());

    /// <inheritdoc cref="ToAffix{TFlavor}"/>
    [MustUseReturnValue]
    public static IInfix ToInfix<T>(this AffixRef<T> affixRef) where T : IAffix<T>, IAffix<IInfix> => new Infix(affixRef.BoundMorpheme.ToString(), affixRef.InsertionPoint, affixRef.Joiner.ToString());

    /// <inheritdoc cref="ToAffix{TFlavor}"/>
    [MustUseReturnValue]
    public static ICircumfix ToCircumfix<T>(this AffixRef<T> affixRef) where T : IAffix<T>, IAffix<ICircumfix> => new Circumfix(affixRef.GetPrefix().ToString(), affixRef.GetSuffix().ToString(), affixRef.Joiner.ToString());

    /// <inheritdoc cref="ToAffix{TFlavor}"/>
    [MustUseReturnValue]
    public static IAmbifix ToAmbifix<T>(this AffixRef<T> affixRef) where T : IAffix<T>, IAffix<IAmbifix> => new Ambifix(affixRef.BoundMorpheme.ToString(), affixRef.Joiner.ToString());

    /// <inheritdoc cref="ToAffix{TFlavor}"/>
    [MustUseReturnValue]
    public static IDuplifix ToDuplifix<T>(this AffixRef<T> affixRef) where T : IAffix<T>, IAffix<IDuplifix> => new Duplifix(affixRef.Joiner.ToString());

    #endregion
}
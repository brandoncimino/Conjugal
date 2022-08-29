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

    /// <summary>
    /// Creates a new <see cref="AffixRef{TFlavor}"/> of <see cref="IPrefix"/> wth a modified <see cref="GetBoundMorpheme(FowlFever.Conjugal.Affixing.AffixRef{FowlFever.Conjugal.Affixing.IPrefix})"/>.
    /// </summary>
    /// <param name="prefix">this <see cref="AffixRef{IPrefix}"/>, which must be of <see cref="IPrefix"/></param>
    /// <param name="newPrefix">the new <see cref="GetBoundMorpheme(FowlFever.Conjugal.Affixing.AffixRef{FowlFever.Conjugal.Affixing.IPrefix})"/></param>
    /// <returns>a new <see cref="AffixRef{TFlavor}"/></returns>
    [Pure]
    public static AffixRef<IPrefix> WithBoundMorpheme(this AffixRef<IPrefix> prefix, ReadOnlySpan<char> newPrefix) => prefix.WithPrefix(newPrefix);


    /// <inheritdoc cref="ISuffix.GetSuffix"/>
    [Pure]
    public static ReadOnlySpan<char> GetBoundMorpheme(this AffixRef<ISuffix> suffix) => suffix.SuffixMorpheme;

    /// <summary>
    /// Creates a new <see cref="AffixRef{TFlavor}"/> of <see cref="ISuffix"/> wth a modified <see cref="GetBoundMorpheme(FowlFever.Conjugal.Affixing.AffixRef{FowlFever.Conjugal.Affixing.ISuffix})"/>.
    /// </summary>
    /// <param name="suffix">this <see cref="AffixRef{IPrefix}"/>, which must be of <see cref="ISuffix"/></param>
    /// <param name="newSuffix">the new <see cref="GetBoundMorpheme(FowlFever.Conjugal.Affixing.AffixRef{FowlFever.Conjugal.Affixing.ISuffix})"/></param>
    /// <returns>a new <see cref="AffixRef{TFlavor}"/></returns>
    [Pure]
    public static AffixRef<ISuffix> WithBoundMorpheme(this AffixRef<ISuffix> suffix, ReadOnlySpan<char> newSuffix) => suffix.WithSuffix(newSuffix);

    /// <inheritdoc cref="IAffix.BoundMorpheme"/>
    [Pure]
    public static ReadOnlySpan<char> GetBoundMorpheme(this AffixRef<IInfix> infix) => infix.BoundMorpheme;

    /// <summary>
    /// Creates a new <see cref="AffixRef{TFlavor}"/> of <see cref="IInfix"/> wth a modified <see cref="GetBoundMorpheme(FowlFever.Conjugal.Affixing.AffixRef{FowlFever.Conjugal.Affixing.IInfix})"/>.
    /// </summary>
    /// <param name="infix">this <see cref="AffixRef{IPrefix}"/>, which must be of <see cref="IInfix"/></param>
    /// <param name="newInfix">the new <see cref="GetBoundMorpheme(FowlFever.Conjugal.Affixing.AffixRef{FowlFever.Conjugal.Affixing.IInfix})"/></param>
    /// <returns>a new <see cref="AffixRef{TFlavor}"/></returns>
    [Pure]
    public static AffixRef<IInfix> WithBoundMorpheme(this AffixRef<IInfix> infix, ReadOnlySpan<char> newInfix) => infix with { BoundMorpheme = newInfix };

    /// <param name="circumfix">this <see cref="AffixFlavor.Circumfix"/>-flavored <see cref="AffixRef{TFlavor}"/></param>
    /// <typeparam name="T">an <see cref="ICircumfix"/>-derived type</typeparam>
    /// <returns>(<see cref="IPrefix.GetPrefix"/>, <see cref="ISuffix.GetSuffix"/>)</returns>
    [Pure]
    public static CircumSpans GetBoundMorphemes<T>(this AffixRef<T> circumfix) where T : IAffix<T>, IAffix<ICircumfix> => new(circumfix.BoundMorpheme, circumfix.SuffixMorpheme);

    #endregion

    /// <inheritdoc cref="IPrefix.GetPrefix"/>
    [Pure]
    public static ReadOnlySpan<char> GetPrefix<T>(this AffixRef<T> prefix) where T : IAffix<T>, IAffix<IPrefix> => prefix.BoundMorpheme;

    /// <summary>
    /// Creates a new <see cref="AffixRef{TFlavor}"/> with a modified <see cref="GetPrefix{T}"/>.
    /// </summary>
    /// <param name="prefix">this <see cref="AffixRef{TFlavor}"/></param>
    /// <param name="newPrefix">the new <see cref="GetPrefix{T}"/></param>
    /// <typeparam name="T">the flavor of the <see cref="AffixRef{TFlavor}"/>, which must be a derivative of <see cref="IPrefix"/></typeparam>
    /// <returns>a new <see cref="AffixRef{TFlavor}"/></returns>
    [Pure]
    public static AffixRef<T> WithPrefix<T>(this AffixRef<T> prefix, ReadOnlySpan<char> newPrefix) where T : IAffix<T>, IAffix<IPrefix> => prefix with { BoundMorpheme = newPrefix };

    /// <inheritdoc cref="ISuffix.GetSuffix"/>
    [Pure]
    public static ReadOnlySpan<char> GetSuffix<T>(this AffixRef<T> suffix) where T : IAffix<T>, IAffix<ISuffix> => suffix.SuffixMorpheme;

    /// <summary>
    /// Creates a new <see cref="AffixRef{TFlavor}"/> with a modified <see cref="GetSuffix{T}"/>.
    /// </summary>
    /// <param name="suffix">this <see cref="AffixRef{TFlavor}"/></param>
    /// <param name="newSuffix">the new <see cref="GetSuffix{T}"/></param>
    /// <typeparam name="T">the flavor of the <see cref="AffixRef{TFlavor}"/>, which must be a derivative of <see cref="ISuffix"/></typeparam>
    /// <returns>a new <see cref="AffixRef{TFlavor}"/></returns>
    [Pure]
    public static AffixRef<T> WithSuffix<T>(this AffixRef<T> suffix, ReadOnlySpan<char> newSuffix) where T : IAffix<T>, IAffix<ISuffix> => suffix with { SuffixMorpheme = newSuffix };

    /// <inheritdoc cref="IInfix.InsertionPoint"/>
    [Pure]
    public static Index GetInsertionPoint<T>(this AffixRef<T> infix) where T : IAffix<T>, IAffix<IInfix> => infix.InsertionPoint;

    /// <summary>
    /// Creates a new <see cref="AffixRef{TFlavor}"/> with an updated <see cref="AffixRef{TFlavor}.InsertionPoint"/>.
    /// </summary>
    /// <param name="infix">this <see cref="AffixRef{TFlavor}"/>, which is of an <see cref="IInfix"/> derivative</param>
    /// <param name="newInsertionPoint">the new <see cref="AffixRef{TFlavor}.InsertionPoint"/></param>
    /// <typeparam name="T">a derivative <see cref="IInfix"/></typeparam>
    /// <returns>a new <see cref="AffixRef{TFlavor}"/></returns>
    [Pure]
    public static AffixRef<T> WithInsertionPoint<T>(this AffixRef<T> infix, Index newInsertionPoint) where T : IAffix<T>, IAffix<IInfix> => infix with { InsertionPoint = newInsertionPoint };

    /// <inheritdoc cref="IAffix.Joiner"/>
    [Pure]
    public static ReadOnlySpan<char> GetJoiner<T>(this AffixRef<T> affixRef) where T : IAffix<T> => affixRef.Joiner;

    /// <summary>
    /// Creates a new <see cref="AffixRef{TFlavor}"/> with an updated <see cref="AffixRef{TFlavor}.Joiner"/>.
    /// </summary>
    /// <param name="affixRef">this <see cref="AffixRef{TFlavor}"/></param>
    /// <param name="newJoiner">the new <see cref="AffixRef{TFlavor}.Joiner"/></param>
    /// <typeparam name="T">the flavor of the <see cref="AffixRef{TFlavor}"/></typeparam>
    /// <returns>a new <see cref="AffixRef{TFlavor}"/></returns>
    [Pure]
    public static AffixRef<T> WithJoiner<T>(this AffixRef<T> affixRef, ReadOnlySpan<char> newJoiner) where T : IAffix<T> => affixRef with { Joiner = newJoiner };

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
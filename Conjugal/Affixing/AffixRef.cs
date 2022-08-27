using System;

namespace FowlFever.Conjugal.Affixing;

/// <inheritdoc cref="IAffix{TFlavor}"/>
/// <remarks>
/// This is a <a href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/struct#ref-struct"><c>ref struct</c></a> equivalent of <see cref="IAffix{TFlavor}"/>.
/// </remarks>
public readonly ref struct AffixRef<TFlavor> where TFlavor : IAffix<TFlavor> {
    /// <summary>
    /// The primary <see cref="BoundMorpheme"/> for this <see cref="AffixRef{TFlavor}"/>, which can appear anywhere <b>except</b> after the <see cref="Affixation.Stem"/> (because that would make it a <see cref="SuffixMorpheme"/> instead).
    /// </summary>
    internal ReadOnlySpan<char> BoundMorpheme { get; init; }

    /// <summary>
    /// The <see cref="BoundMorpheme"/> appearing <b>after</b> the <see cref="Affixation{T}.Stem"/>, if any.
    /// </summary>
    /// <remarks>
    /// Needs to be separate from <see cref="BoundMorpheme"/> because <see cref="AffixFlavor.Circumfix"/> contains both a <see cref="AffixFlavor.Prefix"/> <b>and</b> a <see cref="AffixFlavor.Suffix"/>.
    /// <p/>
    /// Can't be shared with <see cref="BoundMorpheme"/> and split with <see cref="Index"/>, as I had originally intended, because we are likely to receive the two values as separately allocated <see cref="String"/>s and therefore disjointed <see cref="ReadOnlySpan{T}"/>s.
    /// Instead of allocating a new <see cref="string"/> that combines the two, we just take the hit of an extra <see cref="ReadOnlySpan{T}"/>, which is 16 bytes (but won't require any new allocation...?).
    /// </remarks>
    internal ReadOnlySpan<char> SuffixMorpheme { get; init; }

    /// <inheritdoc cref="IAffix.Joiner"/>
    internal ReadOnlySpan<char> Joiner { get; init; }

    /// <summary>
    /// Where in the <see cref="Affixation{T}.Stem"/> the <see cref="BoundMorpheme"/> should be placed.
    /// </summary>
    /// <remarks>
    /// Really only of significance to <see cref="AffixFlavor.Infix"/>es.
    /// </remarks>
    internal Index InsertionPoint { get; init; }

    /// <summary>
    /// The <see cref="AffixFlavor"/> that corresponds to <typeparamref name="TFlavor"/>. 
    /// </summary>
    /// <seealso cref="AffixFlavorExtensions.FromInterface{T}"/>
    public AffixFlavor Flavor => AffixFlavorExtensions.FromInterface<TFlavor>();

    /// <summary>
    /// Applies this affix to a <a href="https://en.wikipedia.org/wiki/Word_stem">word stem</a>, producing an <see cref="Affixation"/>.
    /// </summary>
    /// <param name="stem">the <see cref="Affixation.Stem"/></param>
    /// <returns>a new <see cref="Affixation"/></returns>
    public Affixation WithStem(ReadOnlySpan<char> stem) => Affixation.Of(stem, this);
}
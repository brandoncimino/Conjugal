using System;

namespace FowlFever.Conjugal.Affixing;

/// <inheritdoc cref="Affix{TFlavor}"/>
/// <remarks>
/// This is a <a href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/struct#ref-struct"><c>ref struct</c></a> equivalent of <see cref="Affix{TFlavor}"/>.
/// </remarks>
public readonly ref struct AffixRef<TFlavor> where TFlavor : IAffix<TFlavor> {
    /// <inheritdoc cref="Affix{TFlavor}.BoundMorpheme"/>
    public ReadOnlySpan<char> BoundMorpheme { get; internal init; }

    /// <inheritdoc cref="Affix{TFlavor}.BoundMorpheme2"/>
    public ReadOnlySpan<char> BoundMorpheme2 { get; internal init; }

    /// <inheritdoc cref="Affix{TFlavor}.Joiner"/>
    public ReadOnlySpan<char> Joiner { get; internal init; }

    /// <inheritdoc cref="Affix{TFlavor}.InsertionPoint"/>
    public Index InsertionPoint { get; internal init; }

    /// <inheritdoc cref="Affix{TFlavor}.Flavor"/>
    public AffixFlavor Flavor => AffixFlavorExtensions.FromInterface<TFlavor>();

    /// <inheritdoc cref="Affix{TFlavor}.WithStem"/>
    public Affixation WithStem(ReadOnlySpan<char> stem) => Affixation.Of(stem, this);
}
using System;
using System.Text;

using FowlFever.Conjugal.Internal;

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

    private const string StemIcon        = "🌱";
    private const string ToStringJoiner  = "-";
    private const string PartialStemIcon = "🪵";

    /// <returns>a long-form list of all the properties of this <see cref="AffixRef{TFlavor}"/></returns>
    public string Describe() {
        const int nameWidth  = 14;
        var       headerLine = ToString();
        var       hrule      = new string('—', headerLine.Length);
        var lines = new StringBuilder()
                    .AppendLine(ToString())
                    .AppendLine(hrule)
                    .AppendLine($"{nameof(Flavor),-nameWidth} {"",4} {Flavor}")
                    .AppendLine($"{nameof(BoundMorpheme),-nameWidth} [{BoundMorpheme.Length,2}] {BoundMorpheme.ToString()}")
                    .AppendLine($"{nameof(SuffixMorpheme),-nameWidth} [{SuffixMorpheme.Length,2}] {SuffixMorpheme.ToString()}")
                    .AppendLine($"{nameof(Joiner),-nameWidth} [{Joiner.Length,2}] {Joiner.ToString()}")
                    .AppendLine($"{nameof(InsertionPoint),-nameWidth} {"",4} {InsertionPoint}");
        return lines.ToString();
    }

    /// <inheritdoc />
    public override string ToString() {
        const string baseName   = nameof(AffixRef<TFlavor>);
        const int    diamondLen = 2;
        var          flavorName = typeof(TFlavor).Name;
        var          totalLen   = baseName.Length + diamondLen + flavorName.Length;
        Span<char>   nameSpan   = stackalloc char[totalLen];
        var          namePos    = 0;
        nameSpan.Write(baseName, ref namePos);
        nameSpan[namePos++] = '<';
        nameSpan.Write(flavorName, ref namePos);
        nameSpan[namePos] = '>';

        var msg = Flavor switch {
            AffixFlavor.Prefix    => SpanHelpers.Join(ToStringJoiner, BoundMorpheme,   Joiner, StemIcon),
            AffixFlavor.Suffix    => SpanHelpers.Join(ToStringJoiner, StemIcon,        Joiner, SuffixMorpheme),
            AffixFlavor.Infix     => SpanHelpers.Join(ToStringJoiner, PartialStemIcon, Joiner, BoundMorpheme, Joiner, PartialStemIcon),
            AffixFlavor.Circumfix => SpanHelpers.Join(ToStringJoiner, BoundMorpheme,   Joiner, StemIcon,      Joiner, SuffixMorpheme),
            AffixFlavor.Ambifix   => SpanHelpers.Join(ToStringJoiner, BoundMorpheme,   Joiner, StemIcon,      Joiner, SuffixMorpheme),
            AffixFlavor.Duplifix  => SpanHelpers.Join(ToStringJoiner, StemIcon,        Joiner, StemIcon),
            AffixFlavor.Disfix    => "is not currently supported!",
            AffixFlavor.Transfix  => "is not currently supported!",
            _                     => "was unhandled by any switch branch!",
        };

        return SpanHelpers.Concat(nameSpan, " ", msg);
    }
}
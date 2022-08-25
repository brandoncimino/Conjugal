using System;

using JetBrains.Annotations;

namespace FowlFever.Conjugal.Affixing;

/// <inheritdoc cref="IAffix{TFlavor}"/>
[PublicAPI]
public readonly record struct Affix<TFlavor> : IAffix<TFlavor> where TFlavor : IAffix<TFlavor> {
    /// <inheritdoc cref="IAffix.BoundMorpheme"/>
    public string BoundMorpheme { get; internal init; }
    /// <inheritdoc cref="IAffix.BoundMorpheme2"/>
    public string BoundMorpheme2 { get; internal init; }
    /// <inheritdoc cref="IAffix.Joiner"/>
    public string Joiner { get; internal init; }
    /// <inheritdoc cref="IAffix.InsertionPoint"/>
    public Index InsertionPoint { get; internal init; }

    /// <summary>
    /// The <see cref="AffixFlavor"/> that corresponds to the <typeparamref name="TFlavor"/>.
    /// <p/>
    /// TODO: Future support for <a href="https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/static-virtual-interface-members">static virtual interface members</a> will make it so that this value can be <c>const</c>
    /// </summary>
    public AffixFlavor Flavor => AffixFlavorExtensions.FromInterface<TFlavor>();

    /// <summary>
    /// Converts this <see cref="Affix{TFlavor}"/> to its <c>ref struct</c> equivalent, <see cref="AffixRef{TFlavor}"/>. 
    /// </summary>
    /// <returns>an equivalent <see cref="AffixRef{TFlavor}"/></returns>
    public AffixRef<TFlavor> ToAffixRef() => new() {
        InsertionPoint = InsertionPoint,
        Joiner         = Joiner,
        BoundMorpheme  = BoundMorpheme,
        BoundMorpheme2 = BoundMorpheme2
    };

    /// <summary>
    /// Implicit cast to the <c>ref struct</c> equivalent, <see cref="AffixRef{TFlavor}"/>.
    /// </summary>
    /// <param name="affix">this <see cref="Affix{TFlavor}"/></param>
    /// <returns>an equivalent <see cref="AffixRef{TFlavor}"/></returns>
    public static implicit operator AffixRef<TFlavor>(Affix<TFlavor> affix) => affix.ToAffixRef();

    /// <summary>
    /// Applies this affix to a <a href="https://en.wikipedia.org/wiki/Word_stem">linguistic stem</a>, producing an <see cref="Affixation"/>.
    /// </summary>
    /// <param name="stem">the <see cref="Affixation.Stem"/></param>
    /// <returns>a new <see cref="Affixation"/></returns>
    public Affixation WithStem(ReadOnlySpan<char> stem) => ToAffixRef().WithStem(stem);
}
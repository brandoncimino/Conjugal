using System;
using System.ComponentModel;

using FowlFever.Conjugal.Internal;

using JetBrains.Annotations;

namespace FowlFever.Conjugal.Affixing;

/// <summary>
/// <a href="https://en.wiktionary.org/wiki/affix#English">affix</a> + <a href="https://en.wiktionary.org/wiki/-ation#English">-ation</a>
/// <p/>
/// Represents the act of adding an <a href="https://en.wikipedia.org/wiki/Affix">affix</a> to a word.
/// </summary>
/// <seealso cref="IAffixed"/>
/// <seealso cref="Flavor"/>
[PublicAPI]
public readonly ref partial struct Affixation {
    /// <inheritdoc cref="IAffixed.Stem"/>
    public ReadOnlySpan<char> Stem { get; internal init; }

    /// <inheritdoc cref="AffixRef{TFlavor}.BoundMorpheme"/>
    public ReadOnlySpan<char> BoundMorpheme { get; internal init; }

    /// <inheritdoc cref="AffixRef{TFlavor}.SuffixMorpheme"/>
    public ReadOnlySpan<char> BoundMorpheme2 { get; internal init; }

    /// <inheritdoc cref="AffixRef{TFlavor}.Joiner"/>
    public ReadOnlySpan<char> Joiner { get; internal init; }

    /// <inheritdoc cref="AffixRef{TFlavor}.Flavor"/>
    public AffixFlavor Flavor { get; internal init; }

    /// <inheritdoc cref="AffixRef{TFlavor}.InsertionPoint"/>
    public Index InsertionPoint { get; internal init; }

    /// <inheritdoc />
    public Affixation(AffixFlavor flavor) : this() {
        Flavor = flavor;
    }

    /// <inheritdoc cref="AffixRef{TFlavor}.WithStem"/>
    public static Affixation Of<TFlavor>(ReadOnlySpan<char> stem, AffixRef<TFlavor> affixRef) where TFlavor : IAffix<TFlavor> {
        return new Affixation {
            Flavor         = affixRef.Flavor,
            Stem           = stem,
            BoundMorpheme  = affixRef.BoundMorpheme,
            BoundMorpheme2 = affixRef.SuffixMorpheme,
            Joiner         = affixRef.Joiner,
            InsertionPoint = affixRef.InsertionPoint,
        };
    }

    /// <summary>
    /// Describes what parts of the <see cref="Affixation"/> are available.
    /// </summary>
    private enum PartState : byte {
        /// <summary>
        /// <see cref="Affixation.Stem"/> <see cref="ReadOnlySpan{T}.IsEmpty"/>.
        /// </summary>
        NoStem,
        /// <summary>
        /// <see cref="Affixation.Stem"/> isn't empty, but <b>BOTH</b> <see cref="Affixation.BoundMorpheme"/> <b>AND</b> <see cref="Affixation.BoundMorpheme2"/> are.
        /// </summary>
        NoMorpheme,
        /// <summary>
        /// <see cref="Affixation.Stem"/> and <i><b>at least 1</b></i> of <see cref="Affixation.BoundMorpheme"/> &amp; <see cref="Affixation.BoundMorpheme2"/> are non-<see cref="ReadOnlySpan{T}.IsEmpty"/>.
        /// </summary>
        FullyFormed
    }

    private PartState GetPartState() => this switch {
        { Stem.IsEmpty: true }                                                                          => PartState.NoStem,
        { BoundMorpheme.IsEmpty: true, BoundMorpheme2.IsEmpty: true, Flavor: not AffixFlavor.Duplifix } => PartState.NoMorpheme,
        _                                                                                               => PartState.FullyFormed
    };

    /// <summary>
    /// The total <see cref="ReadOnlySpan{T}.Length"/> of the individual pieces that make up this <see cref="Affixation"/>.
    /// </summary>
    /// <exception cref="NotImplementedException">if this <see cref="Flavor"/> hasn't been implemented</exception>
    /// <exception cref="InvalidEnumArgumentException">if this <see cref="Flavor"/> is unknown</exception>
    public int Length {
        get {
            return GetPartState() switch {
                PartState.NoStem     => 0,
                PartState.NoMorpheme => Stem.Length,
                _                    => GetFullParts().LengthWithJoiner(Joiner)
            };
        }
    }

    /// <returns>the <see cref="ReadOnlySpan{T}"/>s used to <see cref="Render"/> the result when <see cref="GetPartState"/> is <see cref="PartState.Normal"/>.</returns>
    /// <exception cref="NotImplementedException"><see cref="Flavor"/> is <see cref="AffixFlavor.Transfix"/> or <see cref="AffixFlavor.Duplifix"/></exception>
    /// <exception cref="InvalidEnumArgumentException"><see cref="Flavor"/> is unknown</exception>
    private Span3 GetFullParts() {
        return Flavor switch {
            AffixFlavor.Prefix    => new Span3(BoundMorpheme,          Stem),
            AffixFlavor.Suffix    => new Span3(Stem,                   BoundMorpheme2),
            AffixFlavor.Infix     => new Span3(Stem[..InsertionPoint], BoundMorpheme, Stem[InsertionPoint..]),
            AffixFlavor.Circumfix => new Span3(BoundMorpheme,          Stem,          BoundMorpheme2),
            AffixFlavor.Ambifix   => new Span3(BoundMorpheme,          Stem,          BoundMorpheme2),
            AffixFlavor.Duplifix  => new Span3(Stem,                   Stem),
            AffixFlavor.Transfix  => throw Flavor.NotImplementedException(),
            AffixFlavor.Disfix    => throw Flavor.NotImplementedException(),
            _                     => throw new InvalidEnumArgumentException(nameof(Flavor), (int)Flavor, typeof(AffixFlavor))
        };
    }

    #region Rendering to string

    /// <returns>the final result of this <see cref="Affixation"/></returns>
    /// <exception cref="NotImplementedException">if this <see cref="Flavor"/> hasn't been implemented</exception>
    /// <exception cref="InvalidEnumArgumentException">if this <see cref="Flavor"/> is unknown</exception>
    public string Render() {
        return GetPartState() switch {
            PartState.NoStem     => "",
            PartState.NoMorpheme => Stem.ToString(),
            _                    => GetFullParts().JoinString(Joiner)
        };
    }

    /// <returns>individual properties of this <see cref="Affixation"/> for debugging</returns>
    public string Describe() {
        return $"{nameof(Stem)}: '{Stem.ToString()}', {Flavor.ToString()}: '{BoundMorpheme.ToString()}', {nameof(Joiner)}: '{Joiner.ToString()}', {nameof(InsertionPoint)}: '{InsertionPoint.ToString()}'";
    }

    /// <inheritdoc cref="Render"/>
    public override string ToString() => Render();

    /// <summary>
    /// Implicitly <see cref="Render"/>s this <see cref="Affixation"/>.
    /// </summary>
    /// <remarks>
    /// TODO: ref structs like <see cref="Span{T}"/> intentionally <b>don't</b> implicitly become <see cref="string"/>s. Should we maintain that behavior?
    /// </remarks>
    /// <param name="affixation">this <see cref="Affixation"/></param>
    /// <returns><see cref="Render"/></returns>
    public static implicit operator string(Affixation affixation) => affixation.Render();

    #endregion
}
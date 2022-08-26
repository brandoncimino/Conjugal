using System;

using JetBrains.Annotations;

namespace FowlFever.Conjugal.Affixing;

/// <summary>
/// Represents a <see cref="BoundMorpheme"/> that can be attached to a stem via <see cref="Affixation"/>.
/// </summary>
/// <remarks>
/// <see cref="IAffix"/> includes <b>only</b> the "extra" bits, e.g. <i>-ing</i> in <i>genuflect<b>ing</b></i>.
/// The interface <see cref="IAffixed"/> is equivalent to <see cref="IAffix"/> + a <see cref="IAffixed.Stem"/>.
/// </remarks>
/// <seealso cref="IAffixed"/>
[PublicAPI]
public interface IAffix {
    /// <summary>
    /// The kind of <a href="https://en.wikipedia.org/wiki/affix">affix</a> being utilized.
    /// </summary>
    public AffixFlavor AffixFlavor { get; }

    /// <summary>
    /// A <see cref="string"/> interposed betwixt the stem and <see cref="BoundMorpheme"/>.
    /// </summary>
    public string? Joiner { get; }

    /// <summary>
    /// The <a href="https://en.wiktionary.org/wiki/bound_morpheme">bound morpheme</a> used to <see cref="AffixedExtensions.Render"/> the final result.
    /// </summary>
    /// <example>
    /// <i>-ing</i> in <i>genuflecting</i>
    /// </example>
    public string? BoundMorpheme { get; }

    /// <summary>
    /// A second <a href="https://en.wiktionary.org/wiki/bound_morpheme">bound morpheme</a>, used in addition to <see cref="BoundMorpheme"/> for certain <see cref="Affixing.AffixFlavor"/>s, like <see cref="Affixing.AffixFlavor.Circumfix"/>.
    /// </summary>
    /// <example>
    /// (<see cref="BoundMorpheme"/>, <see cref="BoundMorpheme2"/>) = (<a href="https://en.wiktionary.org/wiki/em-_-en#English">em-, -en</a>)
    /// </example>
    public string? BoundMorpheme2 => default;

    /// <summary>
    /// Where in the <a href="https://en.wikipedia.org/wiki/Word_stem">linguistic stem</a> the <see cref="BoundMorpheme"/> will be placed.
    /// </summary>
    public Index InsertionPoint => default;
}

/// <summary>
/// Represents a strongly typed <see cref="IAffix"/>, where the <typeparamref name="TFlavor"/> type parameter determines the <see cref="AffixFlavor"/>.
/// </summary>
/// <typeparam name="TFlavor">a subtype of <see cref="IAffix{TFlavor}"/> corresponding to one of the <see cref="Affixing.AffixFlavor"/>s</typeparam>
public interface IAffix<TFlavor> : IAffix where TFlavor : IAffix<TFlavor> {
    AffixFlavor IAffix.AffixFlavor => AffixFlavorExtensions.FromInterface<TFlavor>();
}

/// <inheritdoc />
public interface ISuffix : IAffix<ISuffix> { }

/// <inheritdoc />
public interface IPrefix : IAffix<IPrefix> { }

/// <inheritdoc />
public interface ICircumfix : IAffix<ICircumfix> { }

/// <inheritdoc />
public interface IDisfix : IAffix<IDisfix> { }

/// <inheritdoc />
public interface IInfix : IAffix<IInfix> { }

/// <inheritdoc />
public interface IAmbifix : IAffix<IAmbifix> { }

/// <inheritdoc />
public interface ITransfix : IAffix<ITransfix> { }

/// <inheritdoc />
public interface IDuplifix : IAffix<IDuplifix> { }
using System;

namespace FowlFever.Conjugal.Affixing;

/// <inheritdoc cref="AffixFlavor.Infix"/>
public interface IInfix : IAffix<IInfix>, IEquatable<IInfix> {
    IInfix IAffix<IInfix>.Self        => this;
    AffixFlavor IAffix.   AffixFlavor => AffixFlavor.Infix;

    /// <summary>
    /// Where in the <see cref="IAffixed.Stem"/> the <see cref="IAffix.BoundMorpheme"/> will occur.
    /// </summary>
    public Index InsertionPoint { get; }
}
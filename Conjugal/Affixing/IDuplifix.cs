using System;

namespace FowlFever.Conjugal.Affixing;

/// <inheritdoc cref="AffixFlavor.Duplifix"/>
public interface IDuplifix : IAffix<IDuplifix>, ISuffix, IEquatable<IDuplifix> {
    IDuplifix IAffix<IDuplifix>.Self          => this;
    AffixFlavor IAffix.         AffixFlavor   => AffixFlavor.Duplifix;
    string IAffix.              BoundMorpheme => throw new InvalidOperationException($"A {GetType().Name} doesn't have a {BoundMorpheme} - it re-uses the {nameof(Affixation.Stem)}!");
    string ISuffix.GetSuffix() => BoundMorpheme;
    bool IEquatable<ISuffix>.Equals(ISuffix? other) => false;
}
using System;

namespace FowlFever.Conjugal.Affixing;

/// <inheritdoc cref="AffixFlavor.Prefix"/>
public interface IPrefix : IAffix<IPrefix>, IEquatable<IPrefix> {
    IPrefix IAffix<IPrefix>.Self => this;

    /// <returns>The <see cref="IAffix.BoundMorpheme"/> that will appear <b>before</b> the <see cref="IAffixed.Stem"/>.</returns>
    public string GetPrefix() => BoundMorpheme;

    AffixFlavor IAffix.AffixFlavor => AffixFlavor.Prefix;
}
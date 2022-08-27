using System;

namespace FowlFever.Conjugal.Affixing;

/// <inheritdoc cref="AffixFlavor.Suffix"/>
public interface ISuffix : IAffix<ISuffix>, IEquatable<ISuffix> {
    ISuffix IAffix<ISuffix>.Self => this;

    /// <returns>The <see cref="IAffix.BoundMorpheme"/> that will appear <b>after</b> the <see cref="IAffixed.Stem"/>.</returns>
    public string GetSuffix() => BoundMorpheme;

    AffixFlavor IAffix.AffixFlavor => AffixFlavor.Suffix;
}
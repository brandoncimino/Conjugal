using System;

namespace FowlFever.Conjugal.Affixing;

/// <inheritdoc cref="AffixFlavor.Circumfix"/>
public interface ICircumfix : IAffix<ICircumfix>, IPrefix, ISuffix, IEquatable<ICircumfix>, IEquatable<IAmbifix> {
    ICircumfix IAffix<ICircumfix>.Self => this;
    /// <inheritdoc cref="IPrefix.GetPrefix"/>
    string Prefix { get; }
    string IPrefix.GetPrefix() => Prefix;

    /// <inheritdoc cref="ISuffix.GetSuffix"/>
    string Suffix { get; }
    string ISuffix.GetSuffix() => Suffix;
    AffixFlavor IAffix.AffixFlavor => AffixFlavor.Circumfix;

    bool IEquatable<IPrefix>.Equals(IPrefix? other) => false;
    bool IEquatable<ISuffix>.Equals(ISuffix? other) => false;
}
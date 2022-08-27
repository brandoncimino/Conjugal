namespace FowlFever.Conjugal.Affixing;

/// <inheritdoc cref="AffixFlavor.Disfix"/>
public interface IDisfix : IAffix<IDisfix> {
    IDisfix IAffix<IDisfix>.Self        => this;
    AffixFlavor IAffix.     AffixFlavor => AffixFlavor.Disfix;
}
namespace FowlFever.Conjugal.Affixing;

/// <inheritdoc cref="AffixFlavor.Transfix"/>
public interface ITransfix : IAffix<ITransfix> {
    ITransfix IAffix<ITransfix>.Self        => this;
    AffixFlavor IAffix.         AffixFlavor => AffixFlavor.Transfix;
}
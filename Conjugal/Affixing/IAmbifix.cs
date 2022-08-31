namespace FowlFever.Conjugal.Affixing;

/// <inheritdoc cref="AffixFlavor.Ambifix"/>
public interface IAmbifix : IAffix<IAmbifix>, ICircumfix {
    IAmbifix IAffix<IAmbifix>.Self   => this;
    string ICircumfix.        Prefix => BoundMorpheme;
    string ICircumfix.        Suffix => BoundMorpheme;
    string IPrefix.GetPrefix() => BoundMorpheme;
    string ISuffix.GetSuffix() => BoundMorpheme;
    AffixFlavor IAffix.AffixFlavor => AffixFlavor.Ambifix;
}
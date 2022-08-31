namespace FowlFever.Conjugal.Affixing;

/// <inheritdoc cref="ICircumfix"/>
public sealed record Circumfix(string Prefix, string Suffix, string Joiner = "") : ICircumfix {
    AffixFlavor IAffix.AffixFlavor   => AffixFlavor.Circumfix;
    string IAffix.     BoundMorpheme => Prefix;

    /// <inheritdoc />
    public bool Equals(ICircumfix? other) => this.EqParts() == other?.EqParts();

    /// <inheritdoc />
    public bool Equals(IAmbifix? other) => this.EqParts() == other?.EqParts();
}
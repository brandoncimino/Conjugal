namespace FowlFever.Conjugal.Affixing;

/// <inheritdoc cref="IAmbifix"/>
public sealed record Ambifix(string BoundMorpheme, string Joiner = "") : IAmbifix {
    AffixFlavor IAffix.AffixFlavor => AffixFlavor.Ambifix;

    /// <inheritdoc />
    public bool Equals(ICircumfix? other) => this.EqParts() == other?.EqParts();

    /// <inheritdoc />
    public bool Equals(IAmbifix? other) => this.EqParts() == other?.EqParts();
}
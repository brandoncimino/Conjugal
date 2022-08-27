namespace FowlFever.Conjugal.Affixing;

/// <inheritdoc cref="ISuffix"/>
public sealed record Suffix(string BoundMorpheme, string Joiner = "") : ISuffix {
    /// <inheritdoc />
    public bool Equals(ISuffix? other) => this.EqParts() == other?.EqParts();
}
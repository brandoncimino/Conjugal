namespace FowlFever.Conjugal.Affixing;

/// <inheritdoc cref="IPrefix"/>
public sealed record Prefix(string BoundMorpheme, string Joiner = "") : IPrefix {
    /// <inheritdoc />
    public bool Equals(IPrefix? other) => this.EqParts() == other?.EqParts();
}
namespace FowlFever.Conjugal.Affixing;

/// <inheritdoc cref="IDuplifix"/>
public sealed record Duplifix(string Joiner = "") : IDuplifix {
    /// <inheritdoc />
    public bool Equals(IDuplifix? other) => this.EqParts() == other?.EqParts();
}
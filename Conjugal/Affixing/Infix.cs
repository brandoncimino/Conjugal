using System;

namespace FowlFever.Conjugal.Affixing;

/// <inheritdoc cref="IInfix"/>
public sealed record Infix(string BoundMorpheme, Index InsertionPoint, string Joiner = "") : IInfix {
    /// <inheritdoc />
    public bool Equals(IInfix? other) => this.EqParts() == other?.EqParts();
}
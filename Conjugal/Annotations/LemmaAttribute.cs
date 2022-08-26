using System;

using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations;

/// <summary>
/// Explicitly defines the <see cref="Conjugation.Lemma"/> of the annotated <see cref="Type"/>.
/// </summary>
/// <remarks><a href="https://en.wikipedia.org/wiki/Lemma_(morphology)">Wikipedia - Lemma (morphology)</a></remarks>
[PublicAPI]
public class LemmaAttribute : ConjugalAttribute {
    /// <inheritdoc cref="LemmaAttribute"/>
    public readonly string Lemma;

    /// <inheritdoc cref="LemmaAttribute"/>
    public LemmaAttribute(string lemma) {
        Lemma = lemma;
    }
}
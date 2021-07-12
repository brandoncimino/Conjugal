using System;

using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations {
    /// <summary>
    /// Explicitly defines the <see cref="Conjugation.Lemma"/> of the annotated <see cref="Type"/>.
    /// </summary>
    /// <remarks><a href="https://en.wikipedia.org/wiki/Lemma_(morphology)">Wikipedia - Lemma (morphology)</a></remarks>
    [PublicAPI]
    public class LemmaAttribute : ConjugalAttribute {
        [NotNull] public readonly string Lemma;

        public LemmaAttribute([NotNull] string lemma) {
            Lemma = lemma;
        }
    }
}
using System;

using Humanizer;

using JetBrains.Annotations;

namespace Conjugal.Annotations
{
    public class LemmaAttribute : Attribute
    {
        [NotNull]
        public readonly string Lemma;

        [NotNull] public readonly LetterCasing? Casing;

        public LemmaAttribute(string lemma)
        {
            Lemma = lemma;
            Casing = null;
        }

        public LemmaAttribute(string lemma, LetterCasing casing ) : this(lemma)
        {
            Casing = casing;
        }
    }
}
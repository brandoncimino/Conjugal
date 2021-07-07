using System;
using System.Diagnostics.CodeAnalysis;
using Humanizer;

using JetBrains.Annotations;

namespace BenthicProfiler.Annotations
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
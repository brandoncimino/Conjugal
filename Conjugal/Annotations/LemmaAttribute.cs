using System;

using Humanizer;

using JetBrains.Annotations;

namespace Conjugal.Annotations {
    public class LemmaAttribute : Attribute {
        [NotNull] public readonly string Lemma;

        [CanBeNull] public readonly LetterCasing? Casing;

        public LemmaAttribute([NotNull] string lemma) {
            Lemma = lemma;
        }

        /// <summary>
        /// Defines a <see cref="LemmaAttribute"/> for this class with the given <see cref="LetterCasing"/>.
        ///
        /// To use the default <see cref="LetterCasing"/>, use the alternate constructor, <see cref="LemmaAttribute(string)"/>.
        /// </summary>
        /// <remarks>
        /// This constructor cannot be combined with <see cref="LemmaAttribute(string)"/> because <see cref="Nullable{T}"/>s cannot be used as attribute parameters.
        /// </remarks>
        /// <param name="lemma"></param>
        /// <param name="casing"></param>
        public LemmaAttribute([NotNull] string lemma, LetterCasing casing) : this(lemma) {
            Casing = casing;
        }
    }
}
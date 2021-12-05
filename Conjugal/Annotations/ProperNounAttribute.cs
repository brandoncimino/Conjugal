using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations {
    /// <summary>
    /// Overrides this class's <see cref="IConjugal.IsProperNoun"/> value.
    /// </summary>
    /// <remarks>
    /// The <see cref="IConjugal.IsProperNoun"/> value is used to determine the <see cref="Humanizer.LetterCasing"/> for this class.
    /// However, <see cref="IConjugal.PreferredCasing"/>, if present, will take precedence.
    /// </remarks>
    /// <seealso cref="IConjugal.IsProperNoun"/>
    /// <seealso cref="IConjugal.PreferredCasing"/>
    [PublicAPI]
    public class ProperNounAttribute : ConjugalAttribute {
        /// <inheritdoc cref="ProperNounAttribute"/>
        public readonly bool IsProperNoun;

        /// <inheritdoc cref="ProperNounAttribute"/>
        public ProperNounAttribute(bool isProperNoun = true) {
            IsProperNoun = isProperNoun;
        }
    }
}
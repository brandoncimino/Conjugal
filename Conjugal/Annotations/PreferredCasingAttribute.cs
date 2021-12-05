using Humanizer;

using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations {
    /// <summary>
    /// Overrides this classes <see cref="IConjugal.PreferredCasing"/>.
    /// </summary>
    /// <remarks>
    /// The <see cref="IConjugal.PreferredCasing"/> will take precedence over a <see cref="LetterCasing"/> inferred from other sources,
    /// such as from <see cref="IConjugal.IsProperNoun"/>.
    /// </remarks>
    [PublicAPI]
    public class PreferredCasingAttribute : ConjugalAttribute {
        /// <inheritdoc cref="PreferredCasingAttribute"/>
        public readonly LetterCasing Casing;

        /// <inheritdoc cref="PreferredCasingAttribute"/>
        public PreferredCasingAttribute(LetterCasing letterCasing) {
            Casing = letterCasing;
        }
    }
}
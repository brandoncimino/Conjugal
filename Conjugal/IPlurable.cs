using JetBrains.Annotations;

namespace FowlFever.Conjugal {
    [PublicAPI]
    public interface IPlurable {
        /// <summary>
        /// The <a href="https://en.wikipedia.org/wiki/Grammatical_number">grammatical number</a> for exactly one entity.
        /// </summary>
        [NotNull]
        public string Singular { get; }
        /// <summary>
        /// The <a href="https://en.wikipedia.org/wiki/Grammatical_number">grammatical number</a> for multiple entities.
        /// </summary>
        /// <remarks><a href="https://en.wikipedia.org/wiki/Plural">Wikipedia - Plural</a></remarks>
        [NotNull]
        public string Plural { get; }
        public Countability Countability { get; }
    }
}
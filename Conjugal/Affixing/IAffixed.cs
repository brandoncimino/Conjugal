using JetBrains.Annotations;

namespace FowlFever.Conjugal.Affixing {
    /// <summary>
    /// Represents a <see cref="string"/> that can be <see cref="AffixedExtensions.Render"/>ed via <see cref="Affixation"/>.
    /// </summary>
    /// <remarks>
    /// Equivalent to an <see cref="IAffix"/> + a <see cref="Stem"/>.
    /// </remarks>
    /// <seealso cref="QuanticString"/>
    [PublicAPI]
    public interface IAffixed : IAffix {
        /// <summary>
        /// The <a href="https://en.wikipedia.org/wiki/Word_stem">linguistic stem</a> used to <see cref="AffixedExtensions.Render"/> the final result.
        /// </summary>
        /// <example>
        /// <i>boob</i> in <i>boobies</i>
        /// </example>
        public string? Stem { get; }
    }
}
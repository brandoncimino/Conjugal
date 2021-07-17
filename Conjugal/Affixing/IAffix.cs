using JetBrains.Annotations;

namespace FowlFever.Conjugal.Affixing {
    /// <summary>
    /// Represents a <see cref="BoundMorpheme"/> that can be attached to a stem via <see cref="Affixation"/>.
    /// </summary>
    /// <remarks>
    /// <see cref="IAffix"/> includes <b>only</b> the "extra" bits, e.g. <i>-ing</i> in <i>genuflect<b>ing</b></i>.
    /// The interface <see cref="IAffixed"/> is equivalent to <see cref="IAffix"/> + a <see cref="IAffixed.Stem"/>.
    /// </remarks>
    /// <seealso cref="IAffixed"/>
    [PublicAPI]
    public interface IAffix {
        /// <summary>
        /// The kind of <a href="https://en.wikipedia.org/wiki/affix">affix</a> being utilized.
        /// </summary>
        public AffixFlavor AffixFlavor { get; }
        /// <summary>
        /// A <see cref="string"/> interposed betwixt the stem and <see cref="BoundMorpheme"/>.
        /// </summary>
        [CanBeNull]
        public string Joiner { get; }
        /// <summary>
        /// The <a href="https://en.wiktionary.org/wiki/bound_morpheme">bound morpheme</a> used to <see cref="AffixedExtensions.Render"/> the final result.
        /// </summary>
        /// <example>
        /// <i>-ing</i> in <i>genuflecting</i>
        /// </example>
        [NotNull]
        public string BoundMorpheme { get; }
    }
}
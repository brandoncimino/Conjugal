using JetBrains.Annotations;

namespace FowlFever.Conjugal.Affixing {
    /// <summary>
    /// Represents a <see cref="string"/> that can be <see cref="Affixed.Render"/>ed via <see cref="Affixation"/>.
    /// </summary>
    /// <seealso cref="QuanticString"/>
    [PublicAPI]
    public interface IAffixed {
        /// <summary>
        /// The kind of <a href="https://en.wikipedia.org/wiki/affix">affix</a> being utilized.
        /// </summary>
        public Affix Affix { get; }
        /// <summary>
        /// A <see cref="string"/> interposed betwixt the <see cref="Stem"/> and <see cref="BoundMorpheme"/>.
        /// </summary>
        public string Joiner { get; }
        /// <summary>
        /// The <a href="https://en.wikipedia.org/wiki/Word_stem">linguistic stem</a> used to <see cref="Affixed.Render"/> the final result.
        /// </summary>
        public string Stem { get; }
        /// <summary>
        /// The <a href="https://en.wiktionary.org/wiki/bound_morpheme">bound morpheme</a> used to <see cref="Affixed.Render"/> the final result.
        /// </summary>
        public string BoundMorpheme { get; }
    }

    /// <summary>
    /// Provides "default implementation" methods for <see cref="IAffixed"/>.
    /// </summary>
    [PublicAPI]
    public static class Affixed {
        /// <summary>
        /// The "default" way to produce a <see cref="string"/> representation of an <see cref="IAffixed"/> object.
        /// </summary>
        /// <param name="affixedThing">the <see cref="IAffixed"/> instance being formatted</param>
        /// <returns>the final <see cref="string"/> representation of the <see cref="IAffixed"/></returns>
        /// <seealso cref="Affixing.Affixation.Render"/>
        public static string Render(this IAffixed affixedThing) {
            return affixedThing.Affixation().Render();
        }

        /// <param name="affixedThing">an <see cref="IAffixed"/> instance, whence forth <see cref="Affixing.Affixation"/> couldst been doed</param>
        /// <returns>a new <see cref="Affixing.Affixation"/> built from the <paramref name="affixedThing"/></returns>
        public static Affixation Affixation(this IAffixed affixedThing) {
            return new Affixation(affixedThing.Affix, affixedThing.Stem, affixedThing.BoundMorpheme, null, affixedThing.Joiner);
        }
    }
}
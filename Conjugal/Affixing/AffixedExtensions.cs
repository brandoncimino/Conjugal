using JetBrains.Annotations;

namespace FowlFever.Conjugal.Affixing {
    /// <summary>
    /// Provides "default implementation" methods for <see cref="IAffixed"/>.
    /// </summary>
    [PublicAPI]
    public static class AffixedExtensions {
        /// <summary>
        /// The "default" way to produce a <see cref="string"/> representation of an <see cref="IAffixed"/> object.
        /// </summary>
        /// <param name="affixedThing">the <see cref="IAffixed"/> instance being formatted</param>
        /// <returns>the final <see cref="string"/> representation of the <see cref="IAffixed"/></returns>
        /// <seealso cref="Affixing.Affixation.Render"/>
        public static string Render(this IAffixed affixedThing) {
            return affixedThing.ToAffixation().Render();
        }

        /// <param name="affixedThing">an <see cref="IAffixed"/> instance, whence forth <see cref="Affixing.Affixation"/> couldst been doed</param>
        /// <returns>a new <see cref="Affixing.Affixation"/> built from the <paramref name="affixedThing"/></returns>
        public static Affixation ToAffixation(this IAffixed affixedThing) {
            return new Affixation() {
                Flavor        = affixedThing.AffixFlavor,
                Stem          = affixedThing.Stem,
                BoundMorpheme = affixedThing.BoundMorpheme,
                Joiner        = affixedThing.Joiner,
            };
        }
    }
}
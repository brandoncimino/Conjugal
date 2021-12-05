using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations {
    /// <summary>
    /// Overrides this class's <see cref="IConjugal.Singular"/> form.
    /// </summary>
    /// <remarks>
    /// This attribute should only be used if the <a href="https://en.wikipedia.org/wiki/Lemma_(morphology)">lemma</a> for this entity is <b>not singular</b>, which is rare.
    /// Otherwise, you should use the <see cref="LemmaAttribute"/>.
    /// </remarks>
    /// <seealso cref="IConjugal.Singular"/>
    /// <seealso cref="IConjugal.Lemma"/>
    [PublicAPI]
    public class SingularAttribute : ConjugalAttribute {
        /// <summary>
        /// The <see cref="IPlurable.Singular"/> form of this class.
        /// </summary>
        [NotNull]
        public readonly string Singular;

        /// <inheritdoc />
        public SingularAttribute([NotNull] string singular) {
            Singular = singular;
        }
    }
}
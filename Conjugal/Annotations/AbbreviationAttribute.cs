using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations {
    /// <summary>
    /// Defines an abbreviated form of <see cref="Abbreviation"/>
    /// </summary>
    [PublicAPI]
    public class AbbreviationAttribute : ConjugalAttribute {
        public readonly Plurable Abbreviation;

        public AbbreviationAttribute(
            [NotNull] string singular,
            [CanBeNull]
            string plural = default
        ) {
            Abbreviation = Plurable.Of(singular, plural);
        }
    }
}
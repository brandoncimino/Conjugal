using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations {
    [PublicAPI]
    public class AbbreviationAttribute : ConjugalAttribute {
        [NotNull] public readonly string SingularAbbreviation;
        [NotNull] public readonly string PluralAbbreviation;

        public AbbreviationAttribute(
            [NotNull] string singular,
            [CanBeNull]
            string plural = default
        ) {
            SingularAbbreviation = singular;
            PluralAbbreviation   = plural ?? singular;
        }
    }
}
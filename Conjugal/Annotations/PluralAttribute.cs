using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations {
    [PublicAPI]
    public class PluralAttribute : ConjugalAttribute {
        public string Plural;

        public PluralAttribute(string plural) {
            Plural = plural;
        }
    }
}
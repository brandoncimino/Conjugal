using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations {
    [PublicAPI]
    public class ProperNounAttribute : ConjugalAttribute {
        public readonly bool IsProperNoun;

        public ProperNounAttribute(bool isProperNoun = true) {
            IsProperNoun = isProperNoun;
        }
    }
}
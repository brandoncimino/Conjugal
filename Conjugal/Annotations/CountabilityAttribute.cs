using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations {
    [PublicAPI]
    public class CountabilityAttribute : ConjugalAttribute {
        public readonly Countability Countability;

        public CountabilityAttribute(Countability countability) {
            Countability = countability;
        }
    }
}
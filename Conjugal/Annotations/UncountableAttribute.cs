using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations {
    [PublicAPI]
    public class UncountableAttribute : CountabilityAttribute {
        public UncountableAttribute() : base(Countability.Uncountable) { }
    }
}
using JetBrains.Annotations;

namespace Conjugal.Annotations {
    [PublicAPI]
    public class UncountableAttribute : CountabilityAttribute {
        public UncountableAttribute() : base(Countability.Uncountable) { }
    }
}
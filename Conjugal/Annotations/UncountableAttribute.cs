using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations {
    /// <summary>
    /// Equivalent to [<see cref="CountabilityAttribute"/>(<see cref="Countability.Uncountable"/>)]
    /// </summary>
    /// <seealso cref="CountabilityAttribute"/>
    /// <seealso cref="CountableAttribute"/>
    [PublicAPI]
    public class UncountableAttribute : CountabilityAttribute {
        public UncountableAttribute() : base(Countability.Uncountable) { }
    }
}
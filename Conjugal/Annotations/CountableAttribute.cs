using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations;

/// <summary>
/// Equivalent to [<see cref="CountabilityAttribute"/>(<see cref="Countability.Countable"/>)]
/// </summary>
/// <seealso cref="CountabilityAttribute"/>
/// <seealso cref="UncountableAttribute"/>
[PublicAPI]
public class CountableAttribute : CountabilityAttribute {
    /// <inheritdoc cref="CountableAttribute"/>
    public CountableAttribute() : base(Countability.Countable) { }
}
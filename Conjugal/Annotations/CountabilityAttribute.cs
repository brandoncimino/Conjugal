using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations;

/// <summary>
/// Explicitly defines this class's <see cref="IPlurable.Countability"/>.
/// </summary>
/// <seealso cref="CountableAttribute"/>
/// <seealso cref="UncountableAttribute"/>
[PublicAPI]
public class CountabilityAttribute : ConjugalAttribute {
    /// <inheritdoc cref="CountabilityAttribute"/>
    public readonly Countability Countability;

    /// <inheritdoc cref="CountabilityAttribute"/>
    public CountabilityAttribute(Countability countability) {
        Countability = countability;
    }
}
using JetBrains.Annotations;

namespace FowlFever.Conjugal;

/// <inheritdoc cref="Countability"/>
[PublicAPI]
public static class CountabilityExtensions {
    /// <summary>
    /// Gets the plural form of <paramref name="singular"/> according to this <see cref="Countability"/>.
    /// </summary>
    /// <remarks>
    /// This is literally the commutative inverse of <see cref="ConjugalStringExtensions.PluralFromCountability"/>.
    /// </remarks>
    /// <param name="countability">this <see cref="Countability"/></param>
    /// <param name="singular">a <see cref="IPlurable.Singular"/> form</param>
    /// <returns>the <see cref="IPlurable.Plural"/> form of the <paramref name="singular"/> input</returns>
    [Pure]
    public static string ApplyToSingular(this Countability countability, string singular) {
        return singular.PluralFromCountability(countability);
    }
}
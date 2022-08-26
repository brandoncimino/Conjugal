using JetBrains.Annotations;

namespace FowlFever.Conjugal;

/// <summary>
/// Extensions for <see cref="IConjugal"/>
/// </summary>
[PublicAPI]
public static class ConjugalExtensions {
    /// <summary>
    /// Returns the <see cref="UnitOfMeasure"/> if explicitly defined; otherwise,
    /// attempts to find a suitable substitute, e.g.:
    /// <ul>
    /// <li>
    /// <see cref="IConjugal.Abbreviation"/>
    /// </li>
    /// <li>
    /// <see cref="PlurableExtensions.ToPlurable"/>
    /// </li>
    /// </ul>
    /// </summary>
    /// <param name="conjugal">this <see cref="IConjugal"/></param>
    /// <returns>something resembling a <see cref="UnitOfMeasure"/></returns>
    public static UnitOfMeasure FallbackUnitOfMeasure(this IConjugal conjugal) {
        return conjugal.UnitOfMeasure ??
               new UnitOfMeasure(conjugal.Abbreviation?.ToPlurable() ?? conjugal.ToPlurable());
    }

    /// <summary>
    /// Constructs a <see cref="QuanticString"/>...?
    /// </summary>
    /// <param name="conjugal"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    public static QuanticString Quantify(this IConjugal conjugal, double quantity) {
        return new QuanticString(quantity, conjugal.FallbackUnitOfMeasure());
    }
}
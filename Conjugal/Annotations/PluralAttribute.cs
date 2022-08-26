using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations;

/// <summary>
/// Explicitly determines this class's <see cref="IPlurable.Plural"/> conjugations.
/// </summary>
/// <seealso cref="IPlurable.Plural"/>
[PublicAPI]
public class PluralAttribute : ConjugalAttribute {
    /// <inheritdoc cref="PluralAttribute"/>
    public string Plural;

    /// <inheritdoc cref="PluralAttribute"/>
    public PluralAttribute(string plural) {
        Plural = plural;
    }
}
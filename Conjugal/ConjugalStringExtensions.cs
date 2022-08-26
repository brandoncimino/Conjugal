using System;
using System.ComponentModel;

using Humanizer;

using JetBrains.Annotations;

namespace FowlFever.Conjugal;

/// <summary>
/// Vanilla <see cref="string"/> extension methods defined by <see cref="Conjugal"/>.
/// </summary>
[PublicAPI]
public static class ConjugalStringExtensions {
    /// <summary>
    /// Derives a <see cref="IPlurable.Plural"/> form using the provided <see cref="Countability"/>:
    /// <ul>
    /// <li><see cref="Countability.Countable"/> -> <see cref="InflectorExtensions.Pluralize"/></li>
    /// <li><see cref="Countability.Uncountable"/> -> <paramref name="singular"/></li>
    /// </ul>
    /// </summary>
    /// <param name="singular">a noun describing exactly one entity</param>
    /// <param name="countability">a <see cref="Countability"/></param>
    /// <returns>a plural form of the <paramref name="singular"/> word</returns>
    /// <exception cref="NotImplementedException">if <see cref="Countability.Collective"/> is provided</exception>
    /// <exception cref="InvalidEnumArgumentException">if an unknown <see cref="Countability"/> value is provided</exception>
    public static string PluralFromCountability(this string singular, Countability countability) {
        return countability switch {
            Countability.Countable   => singular.Pluralize(),
            Countability.Uncountable => singular,
            Countability.Collective  => throw new NotImplementedException($"{countability} is not implemented!"),
            _                        => throw new InvalidEnumArgumentException(nameof(countability), (int)countability, countability.GetType())
        };
    }

    /// <inheritdoc cref="Plurable.Humanized"/>
    /// <seealso cref="Plurable.Humanized"/>
    public static Plurable Plurablize(this string singular, bool isKnownToBeSingular = true) {
        return Plurable.Humanized(singular, isKnownToBeSingular);
    }

    /// <inheritdoc cref="CasingExtensions.ApplyCase"/>
    public static string ApplyCase(this string input, LetterCasing? casing) {
        return casing?.ApplyTo(input) ?? input;
    }
}
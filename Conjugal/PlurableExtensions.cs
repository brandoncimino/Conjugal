using System;

using JetBrains.Annotations;

namespace FowlFever.Conjugal {
    /// <summary>
    /// Extensions for <see cref="IPlurable"/>
    /// </summary>
    [PublicAPI]
    public static class PlurableExtensions {
        /// <summary>
        /// Gets <b>either</b> the <see cref="IConjugal.Plural"/> <b>or</b> <see cref="IConjugal.Singular"/> form of the given <see cref="Type"/>,
        /// based on the given <see cref="quantity"/>.
        /// </summary>
        /// <remarks>0 is considered a <b>plural <paramref name="quantity"/>!</b></remarks>
        /// <param name="plurable">this <see cref="IPlurable"/> instance</param>
        /// <param name="quantity">the actual quantity of items, to decide whether the <see cref="IConjugal.Plural"/> or <see cref="IConjugal.Singular"/> should be used. If not set, the <see cref="IConjugal.Plural"/> will be returned</param>
        /// <returns>either the <see cref="IConjugal.Singular"/> or <see cref="IConjugal.Plural"/></returns>
        /// <seealso cref="Pluralize(FowlFever.Conjugal.IPlurable,System.Nullable{int})"/>
        /// <seealso cref="Pluralize(FowlFever.Conjugal.IPlurable,System.Nullable{double})"/>
        public static string Pluralize(this IPlurable plurable, int? quantity = default) {
            return quantity.GetValueOrDefault(0) == 1 ? plurable.Singular : plurable.Plural;
        }

        /// <inheritdoc cref="Pluralize(FowlFever.Conjugal.IPlurable,System.Nullable{int})"/>
        public static string Pluralize(this IPlurable plurable, double? quantity) {
            return Math.Abs(quantity.GetValueOrDefault(0) - 1) < double.Epsilon ? plurable.Singular : plurable.Plural;
        }

        /// <param name="plurable">this <see cref="IPlurable"/></param>
        /// <returns>this <see cref="IPlurable"/> implementation as a concrete <see cref="Plurable"/>.</returns>
        public static Plurable ToPlurable(this IPlurable plurable) {
            return Plurable.Of(plurable.Singular, plurable.Plural, plurable.Countability);
        }
    }
}
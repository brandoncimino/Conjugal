using System;

using JetBrains.Annotations;

namespace FowlFever.Conjugal.Affixing {
    /// <summary>
    /// Extension methods for <see cref="AffixFlavor"/>.
    /// </summary>
    /// <seealso cref="AffixFlavor"/>
    [PublicAPI]
    public static class AffixFlavorExtensions {
        /// <summary>
        /// Whether this <see cref="Affixation.AffixFlavor"/>
        /// </summary>
        /// <param name="affixFlavor"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static bool RequiresIndex(this AffixFlavor affixFlavor) {
            return affixFlavor switch {
                AffixFlavor.Prefix    => false,
                AffixFlavor.Suffix    => false,
                AffixFlavor.Infix     => true,
                AffixFlavor.Circumfix => true,
                AffixFlavor.Ambifix   => false,
                AffixFlavor.Duplifix  => false,
                AffixFlavor.Transfix  => throw new NotImplementedException($"{affixFlavor} is not implemented by {nameof(RequiresIndex)}!"),
                AffixFlavor.Disfix    => throw new NotImplementedException($"{affixFlavor} is not implemented by {nameof(RequiresIndex)}!"),
                _                     => throw new ArgumentOutOfRangeException(nameof(affixFlavor), affixFlavor, null)
            };
        }

        private static ArgumentOutOfRangeException RequiresIndexException(AffixFlavor flavor, object? index) {
            return new ArgumentOutOfRangeException($"The {nameof(AffixFlavor)} {flavor} requires an {nameof(index)} >= 0, but [{index ?? "⛔"}] was provided!");
        }

        private static ArgumentException NoIndexRequiredException(AffixFlavor flavor, object? index) {
            return new ArgumentException($"The {nameof(AffixFlavor)} {flavor} should NOT have an {nameof(index)}, but [{index ?? "⛔"}] was provided!");
        }

        internal static T ValidateIndex<T>(this AffixFlavor flavor, T? index) {
            return (flavor.RequiresIndex(), index) switch {
                (true, not null) => index,
                (true, null)     => throw RequiresIndexException(flavor, index),
                (false, _)       => throw NoIndexRequiredException(flavor, index)
            };
        }
    }
}
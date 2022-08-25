using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

using JetBrains.Annotations;

namespace FowlFever.Conjugal.Affixing {
    /// <summary>
    /// Extension methods for <see cref="AffixFlavor"/>.
    /// </summary>
    /// <seealso cref="AffixFlavor"/>
    [PublicAPI]
    public static class AffixFlavorExtensions {
        /// <param name="affixFlavor"></param>
        /// <returns><c>true</c>if this <see cref="Affixation.AffixFlavor"/> requires an <see cref="Affixation.InsertionPoint"/>.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static bool RequiresIndex(this AffixFlavor affixFlavor) {
            return affixFlavor switch {
                AffixFlavor.Prefix    => false,
                AffixFlavor.Suffix    => false,
                AffixFlavor.Infix     => true,
                AffixFlavor.Circumfix => true,
                AffixFlavor.Ambifix   => false,
                AffixFlavor.Duplifix  => false,
                AffixFlavor.Transfix  => throw NotImplementedException(affixFlavor),
                AffixFlavor.Disfix    => throw NotImplementedException(affixFlavor),
                _                     => throw new ArgumentOutOfRangeException(nameof(affixFlavor), affixFlavor, null)
            };
        }


        /// <param name="affixFlavor">this <see cref="AffixFlavor"/></param>
        /// <returns>the expected number of <see cref="IAffix.Joiner"/>s in the resulting string</returns>
        /// <exception cref="NotImplementedException">if <see cref="AffixFlavor.Transfix"/> or <see cref="AffixFlavor.Disfix"/> is provided</exception>
        /// <exception cref="ArgumentOutOfRangeException">if an unknown <see cref="AffixFlavor"/> is provided</exception>
        public static int NumberOfJoiners(this AffixFlavor affixFlavor) {
            return affixFlavor switch {
                AffixFlavor.Prefix    => 1,
                AffixFlavor.Suffix    => 1,
                AffixFlavor.Infix     => 2,
                AffixFlavor.Circumfix => 2,
                AffixFlavor.Ambifix   => 2,
                AffixFlavor.Duplifix  => 1,
                AffixFlavor.Transfix  => throw NotImplementedException(affixFlavor),
                AffixFlavor.Disfix    => throw NotImplementedException(affixFlavor),
                _                     => throw new ArgumentOutOfRangeException(nameof(affixFlavor), affixFlavor, null)
            };
        }

        private static NotImplementedException NotImplementedException(AffixFlavor flavor, [CallerMemberName] string caller = "") {
            return new NotImplementedException($"{nameof(AffixFlavor)}.{flavor} is not implemented by {caller}!");
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

        /// <summary>
        /// Converts an <see cref="AffixFlavor"/> to the corresponding <see cref="IAffix"/>-derived interface <see cref="Type"/>.
        /// </summary>
        /// <param name="flavor"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static Type ToInterface(this AffixFlavor flavor) {
            return flavor switch {
                AffixFlavor.Suffix    => typeof(ISuffix),
                AffixFlavor.Prefix    => typeof(IPrefix),
                AffixFlavor.Circumfix => typeof(ICircumfix),
                AffixFlavor.Disfix    => typeof(IDisfix),
                AffixFlavor.Infix     => typeof(IInfix),
                AffixFlavor.Ambifix   => typeof(IAmbifix),
                AffixFlavor.Duplifix  => typeof(IDuplifix),
                AffixFlavor.Transfix  => typeof(ITransfix),
                _                     => throw new ArgumentOutOfRangeException(nameof(flavor), flavor, $"I don't know an {nameof(IAffix)} interface that corresponds to {nameof(AffixFlavor)}.{flavor}!")
            };
        }

        private static IDictionary<Type, AffixFlavor> Interface_To_Flavor = Enum.GetValues(typeof(AffixFlavor))
                                                                                .Cast<AffixFlavor>()
                                                                                .Distinct()
                                                                                .ToDictionary(it => it.ToInterface(), it => it);

        /// <summary>
        /// Retrieves the corresponding <see cref="AffixFlavor"/> for a given <see cref="IAffix"/>-derived type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static AffixFlavor FromInterface<T>() where T : IAffix {
            var type = typeof(T);
            if (type == typeof(IAffix)) {
                throw new ArgumentException($"Cannot get the flavor of the parent {nameof(IAffix)} type! Please supply one of its children, like {nameof(ISuffix)}.");
            }

            if (Interface_To_Flavor.TryGetValue(type, out var flavor)) {
                return flavor;
            }

            throw new ArgumentException($"I don't know what {nameof(AffixFlavor)} {type} has!");
        }
    }
}
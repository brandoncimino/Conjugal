using System;
using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

namespace FowlFever.Conjugal.Affixing;

/// <summary>
/// Extension methods for <see cref="AffixFlavor"/>.
/// </summary>
/// <seealso cref="AffixFlavor"/>
[PublicAPI]
public static class AffixFlavorExtensions {
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
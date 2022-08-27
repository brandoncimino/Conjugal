using System;

namespace FowlFever.Conjugal.Affixing;

/// <summary>
/// Helpers for <see cref="IEquatable{T}"/> implementations.
/// </summary>
internal static class AffixEqualityExtensions {
    public static (AffixFlavor, string, string) EqParts(this IPrefix prefix) => (prefix.AffixFlavor, prefix.BoundMorpheme, prefix.Joiner);
    public static (AffixFlavor, string, string) EqParts(this ISuffix suffix) => (suffix.AffixFlavor, suffix.BoundMorpheme, suffix.Joiner);
    public static (AffixFlavor, string, string) EqParts(this IInfix infix) => (infix.AffixFlavor, infix.BoundMorpheme, infix.Joiner);
    public static (string, string, string) EqParts(this ICircumfix circumfix) => (circumfix.GetPrefix(), circumfix.GetSuffix(), circumfix.Joiner);
    public static (string, string, string) EqParts(this IAmbifix ambifix) => (ambifix.GetPrefix(), ambifix.GetSuffix(), ambifix.Joiner);
    public static (AffixFlavor, string) EqParts(this IDuplifix duplifix) => (duplifix.AffixFlavor, duplifix.Joiner);
}
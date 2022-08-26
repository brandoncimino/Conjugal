using FowlFever.Conjugal.Affixing;

using JetBrains.Annotations;

namespace FowlFever.Conjugal;

/// <summary>
/// Represents the <b>formatting information</b> for a <a href="https://en.wikipedia.org/wiki/Unit_of_measurement">unit of measurement</a>.
/// </summary>
[PublicAPI]
public readonly struct UnitOfMeasure : IAffix, IPlurable {
    public const string DefaultJoiner = " ";

    public readonly Plurable     Name;
    public readonly Plurable?    Symbol;
    private         Plurable     Notation      => Symbol ?? Name;
    public          string       BoundMorpheme => Notation.ToString();
    public          AffixFlavor  AffixFlavor   { get; }
    public          string       Joiner        { get; }
    public          string       Singular      => Notation.Singular;
    public          string       Plural        => Notation.Plural;
    public          Countability Countability  => Notation.Countability;

    public UnitOfMeasure(
        Plurable name,
        Plurable? symbol,
        string? joiner = DefaultJoiner,
        AffixFlavor affixFlavor = AffixFlavor.Suffix
    ) {
        Name        = name;
        Symbol      = symbol;
        Joiner      = joiner;
        AffixFlavor = affixFlavor;
    }

    public UnitOfMeasure(
        IPlurable name,
        IPlurable? symbol,
        string? joiner = DefaultJoiner,
        AffixFlavor affixFlavor = AffixFlavor.Suffix
    ) : this(
        name.ToPlurable(),
        symbol.ToPlurable(),
        joiner,
        affixFlavor
    ) { }

    public UnitOfMeasure(
        Plurable name,
        string joiner = DefaultJoiner,
        AffixFlavor affixFlavor = AffixFlavor.Suffix
    ) : this(
        name: name,
        symbol: default,
        joiner: joiner,
        affixFlavor: affixFlavor
    ) { }

    public UnitOfMeasure(
        IPlurable name,
        string joiner = DefaultJoiner,
        AffixFlavor affixFlavor = AffixFlavor.Suffix
    ) : this(
        name,
        default,
        joiner,
        affixFlavor
    ) { }

    public override string ToString() {
        return $"[{Name} ({Symbol?.ToString() ?? $"no {nameof(Symbol)} defined"})]";
    }

    public QuanticString Quantify(double quantity, int? decimalPlaces = null) {
        return new QuanticString(quantity, this, decimalPlaces);
    }
}
using System;
using System.Globalization;

using FowlFever.Conjugal.Affixing;

using JetBrains.Annotations;

namespace FowlFever.Conjugal {
    /// <summary>
    /// A struct for formatting <a href="https://en.wikipedia.org/wiki/Physical_quantity">physical quantities</a>.
    /// </summary>
    /// <remarks>TODO: maybe use fancy number <see cref="IFormatProvider"/>s instead of <see cref="DecimalPlaces"/>?
    /// </remarks>
    [PublicAPI]
    public readonly struct QuanticString : IAffixed, IPhysicalQuantity {
        public          UnitOfMeasure Unit { get; }
        public readonly double        Quantity;
        public          double        Value         => Quantity;
        public          AffixFlavor   AffixFlavor   => Unit.AffixFlavor;
        public          string        Joiner        => Unit.Joiner;
        public          string        Stem          => (DecimalPlaces.HasValue ? Math.Round(Quantity, DecimalPlaces.Value) : Quantity).ToString(CultureInfo.CurrentCulture);
        public          string        BoundMorpheme => (Unit.Symbol ?? Unit.Name).Pluralize(Quantity);
        public readonly int?          DecimalPlaces;


        public QuanticString(
            double        quantity,
            UnitOfMeasure unit,
            [NonNegativeValue]
            int? decimalPlaces = default
        ) {
            if (decimalPlaces <= 0) {
                throw new ArgumentException("Must be > 0 or null", nameof(decimalPlaces));
            }

            DecimalPlaces = decimalPlaces;
            Quantity      = quantity;
            Unit          = unit;
        }

        public QuanticString(
            double      quantity,
            Plurable    unitName,
            Plurable    unitSymbol,
            string      joiner      = UnitOfMeasure.DefaultJoiner,
            AffixFlavor affixFlavor = AffixFlavor.Suffix,
            [NonNegativeValue]
            int? decimalPlaces = default
        ) : this(
            quantity,
            new UnitOfMeasure(unitName, unitSymbol, joiner, affixFlavor)
        ) { }

        public QuanticString(
            double      quantity,
            IPlurable   unitName,
            IPlurable   unitSymbol,
            string      joiner      = UnitOfMeasure.DefaultJoiner,
            AffixFlavor affixFlavor = AffixFlavor.Suffix,
            [NonNegativeValue]
            int? decimalPlaces = default
        ) : this(
            quantity,
            new UnitOfMeasure(unitName, unitSymbol, joiner, affixFlavor)
        ) { }

        public QuanticString(
            double      quantity,
            Plurable    unitName,
            string      joiner      = UnitOfMeasure.DefaultJoiner,
            AffixFlavor affixFlavor = AffixFlavor.Suffix,
            [NonNegativeValue]
            int? decimalPlaces = default
        ) : this(
            quantity,
            new UnitOfMeasure(unitName, joiner, affixFlavor)
        ) { }

        public QuanticString(
            double      quantity,
            IPlurable   unitName,
            string      joiner      = UnitOfMeasure.DefaultJoiner,
            AffixFlavor affixFlavor = AffixFlavor.Suffix,
            [NonNegativeValue]
            int? decimalPlaces = default
        ) : this(
            quantity,
            new UnitOfMeasure(unitName, joiner, affixFlavor)
        ) { }

        public override string ToString() {
            return AffixedExtensions.Render(this);
        }

        public static implicit operator string(QuanticString quanticString) {
            return quanticString.ToString();
        }
    }
}
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
        public          Affix         Affix         => Unit.Affix;
        public          string        Joiner        => Unit.Joiner;
        public          string        Stem          => (DecimalPlaces.HasValue ? Math.Round(Quantity, DecimalPlaces.Value) : Quantity).ToString(CultureInfo.CurrentCulture);
        public          string        BoundMorpheme => (Unit.Symbol ?? Unit.Name).Pluralize(Quantity);
        public readonly int?          DecimalPlaces;

        #region Unit name + symbol

        #region string joiner

        public QuanticString(
            double quantity,
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
            double quantity,
            Plurable unitName,
            Plurable unitSymbol,
            string joiner = UnitOfMeasure.DefaultJoinerString,
            Affix affix = Affix.Suffix
        ) : this(
            quantity,
            new UnitOfMeasure(unitName, unitSymbol, joiner, affix)
        ) { }

        #endregion

        #region enum joiner

        public QuanticString(
            double quantity,
            Plurable unitName,
            Plurable unitSymbol,
            Joiner joiner,
            Affix affix = Affix.Suffix
        ) : this(
            quantity,
            unitName,
            unitSymbol,
            joiner.AsString(),
            affix
        ) { }

        #endregion

        #endregion

        #region unitNameAndSymbol

        #region string joiner

        public QuanticString(
            double quantity,
            [NotNull] Plurable unitName,
            string joiner = UnitOfMeasure.DefaultJoinerString,
            Affix affix = Affix.Suffix
        ) : this(
            quantity,
            new UnitOfMeasure(name: unitName, joiner: joiner, affix: affix)
        ) { }

        public QuanticString(
            double quantity,
            Plurable unitName,
            Joiner joiner,
            Affix affix = Affix.Suffix
        ) : this(
            quantity,
            unitName,
            joiner.AsString(),
            affix
        ) { }

        #endregion

        #endregion

        public override string ToString() {
            return Affixed.Render(this);
        }

        public static implicit operator string(QuanticString quanticString) {
            return quanticString.ToString();
        }
    }
}
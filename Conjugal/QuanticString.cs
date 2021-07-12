using System.Globalization;

using FowlFever.Conjugal.Affixing;

using JetBrains.Annotations;

namespace FowlFever.Conjugal {
    /// <summary>
    /// A struct for formatting <a href="https://en.wikipedia.org/wiki/Physical_quantity">physical quantities</a>.
    /// </summary>
    [PublicAPI]
    public readonly struct QuanticString : IAffixed, IPhysicalQuantity {
        public          UnitOfMeasure Unit { get; }
        public readonly double        Quantity;
        public          double        Value         => Quantity;
        public          Affix         Affix         => Unit.Affix;
        public          string        Joiner        => Unit.Joiner;
        public          string        Stem          => Quantity.ToString(CultureInfo.CurrentCulture);
        public          string        BoundMorpheme => Unit.Symbol;

        #region Unit name + symbol

        #region string joiner

        public QuanticString(double quantity, UnitOfMeasure unit) {
            Quantity = quantity;
            Unit     = unit;
        }

        public QuanticString(
            double quantity,
            string unitName,
            string unitSymbol,
            string joiner = "",
            Affix affix = Affix.Suffix
        ) : this(
            quantity,
            new UnitOfMeasure(unitName, unitSymbol, joiner, affix)
        ) { }

        #endregion

        #region enum joiner

        public QuanticString(
            double quantity,
            string unitName,
            string unitSymbol,
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
            string unitNameAndSymbol,
            string joiner = "",
            Affix affix = Affix.Suffix
        ) : this(
            quantity,
            new UnitOfMeasure(unitNameAndSymbol, joiner, affix)
        ) { }

        public QuanticString(
            double quantity,
            string unitNameAndSymbol,
            Joiner joiner,
            Affix affix = Affix.Suffix
        ) : this(
            quantity,
            unitNameAndSymbol,
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
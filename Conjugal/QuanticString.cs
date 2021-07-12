using System.Globalization;

using Conjugal.Affixing;

using JetBrains.Annotations;

namespace Conjugal {
    /// <summary>
    /// A struct for formatting <a href="https://en.wikipedia.org/wiki/Physical_quantity">physical quantities</a>.
    /// </summary>
    [PublicAPI]
    public readonly struct QuanticString : IAffixed, IPhysicalQuantity {
        public readonly string Symbol;
        public readonly double Quantity;
        public          double Value         => Quantity;
        public          string Unit          => Symbol;
        public          Affix  Affix         { get; }
        public          string Joiner        { get; }
        public          string Stem          => Quantity.ToString(CultureInfo.CurrentCulture);
        public          string BoundMorpheme => Symbol;

        public QuanticString(string symbol, double quantity, Affix affix = Affix.Suffix, string joiner = "") {
            Symbol   = symbol;
            Quantity = quantity;
            Affix    = affix;
            Joiner   = joiner;
        }

        public QuanticString(
            string symbol,
            double quantity,
            Affix affix = Affix.Suffix,
            Joiner joiner = Affixing.Joiner.None
        ) : this(
            symbol,
            quantity,
            affix,
            joiner.String()
        ) { }

        public override string ToString() {
            return Affixed.Render((IAffixed) this);
        }

        public static implicit operator string(QuanticString quanticString) {
            return quanticString.ToString();
        }
    }
}
using FowlFever.Conjugal.Affixing;

using JetBrains.Annotations;

namespace FowlFever.Conjugal {
    [PublicAPI]
    public readonly struct UnitOfMeasure {
        [NotNull] public readonly string Name;
        [NotNull] public readonly string Symbol;
        public readonly           Affix  Affix;
        public readonly           string Joiner;

        public UnitOfMeasure(
            [NotNull] string name,
            [NotNull] string symbol,
            string joiner = "",
            Affix affix = Affix.Suffix
        ) {
            Name   = name;
            Symbol = symbol;
            Joiner = joiner;
            Affix  = affix;
        }

        public UnitOfMeasure(
            [NotNull] string nameAndSymbol,
            string joiner = "",
            Affix affix = Affix.Suffix
        ) : this(
            nameAndSymbol,
            nameAndSymbol,
            joiner,
            affix
        ) { }

        public UnitOfMeasure(
            [NotNull] string name,
            [NotNull] string symbol,
            Joiner joiner,
            Affix affix = Affix.Suffix
        ) : this(
            name,
            symbol,
            joiner.AsString(),
            affix
        ) { }

        public UnitOfMeasure(
            [NotNull] string nameAndSymbol,
            Joiner joiner,
            Affix affix = Affix.Suffix
        ) : this(
            nameAndSymbol,
            nameAndSymbol,
            joiner.AsString(),
            affix
        ) { }

        public override string ToString() {
            return $"{Name} ({Symbol})";
        }
    }
}
using System;

using FowlFever.Conjugal.Affixing;

using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations {
    [PublicAPI]
    public class UnitOfMeasureAttribute : Attribute {
        public readonly UnitOfMeasure UnitOfMeasure;

        public UnitOfMeasureAttribute(
            [NotNull] string name,
            [NotNull] string symbol,
            Joiner joiner = Joiner.None,
            Affix affix = Affix.Suffix
        ) {
            UnitOfMeasure = new UnitOfMeasure(name, symbol, joiner, affix);
        }

        public UnitOfMeasureAttribute(
            [NotNull] string nameAndSymbol,
            Joiner joiner = Joiner.None,
            Affix affix = Affix.Suffix
        ) : this(
            nameAndSymbol,
            nameAndSymbol,
            joiner,
            affix
        ) { }
    }
}
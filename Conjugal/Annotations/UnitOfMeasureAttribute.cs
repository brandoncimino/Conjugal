using System;

using FowlFever.Conjugal.Affixing;

using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations {
    [PublicAPI]
    public class UnitOfMeasureAttribute : Attribute {
        [NotNull] public readonly string Symbol;
        public readonly           Affix  Affix;
        public readonly           Joiner Joiner;

        public UnitOfMeasureAttribute(string symbol, Affix affix = Affix.Suffix, Joiner joiner = Joiner.None) {
            Symbol = symbol;
            Affix  = affix;
            Joiner = joiner;
        }
    }
}
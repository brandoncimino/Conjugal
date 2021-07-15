using System;

using FowlFever.Conjugal.Affixing;

using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations {
    [PublicAPI]
    public class UnitOfMeasureAttribute : Attribute {
        public readonly UnitOfMeasure UnitOfMeasure;

        public UnitOfMeasureAttribute(
            [NotNull] string name,
            [CanBeNull]
            string symbol = default,
            Joiner joiner = Conjugal.UnitOfMeasure.DefaultJoinerEnum,
            Affix affix = Affix.Suffix,
            [CanBeNull]
            string namePlural = default,
            [CanBeNull]
            string symbolPlural = default
        ) {
            if (!string.IsNullOrEmpty(symbolPlural) && string.IsNullOrEmpty(symbol)) {
                throw new ArgumentException($"A non-empty {nameof(symbolPlural)} was provided ('{symbolPlural}') alongside an empty {nameof(symbol)}!");
            }

            var pluricName = namePlural == null ? Plurable.Of(name) : (name, namePlural);

            // NOTE: this could be combined into nested ternary operations, but that A) is really confusing, and B) causes issues with implicit casting betwixt `Pluric` and `Pluric?`
            Plurable? pluricSymbol = default;
            if (symbol != null) {
                pluricSymbol = symbolPlural == null ? symbol : Plurable.Of(symbol, symbolPlural);
            }

            UnitOfMeasure = new UnitOfMeasure(pluricName, pluricSymbol, joiner, affix);
        }
    }
}
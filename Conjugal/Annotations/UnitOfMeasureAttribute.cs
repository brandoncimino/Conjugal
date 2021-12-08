using System;

using FowlFever.Conjugal.Affixing;

using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations {
    [PublicAPI]
    public class UnitOfMeasureAttribute : Attribute {
        public readonly UnitOfMeasure UnitOfMeasure;

        public UnitOfMeasureAttribute(
            string      name,
            string?     symbol       = default,
            string?     joiner       = UnitOfMeasure.DefaultJoiner,
            AffixFlavor affixFlavor  = AffixFlavor.Suffix,
            string?     namePlural   = default,
            string?     symbolPlural = default
        ) {
            if (!string.IsNullOrEmpty(symbolPlural) && string.IsNullOrEmpty(symbol)) {
                throw new ArgumentException($"A non-empty {nameof(symbolPlural)} was provided ('{symbolPlural}') alongside an empty {nameof(symbol)}!");
            }

            var pluricName = namePlural == null ? Plurable.Uncountable(name) : (name, namePlural);

            // NOTE: this could be combined into nested ternary operations, but that A) is really confusing, and B) causes issues with implicit casting betwixt `Pluric` and `Pluric?`
            Plurable? pluricSymbol = default;
            if (symbol != null) {
                pluricSymbol = symbolPlural == null ? symbol : Plurable.Of(symbol, symbolPlural);
            }

            UnitOfMeasure = new UnitOfMeasure(pluricName, pluricSymbol, joiner, affixFlavor);
        }
    }
}
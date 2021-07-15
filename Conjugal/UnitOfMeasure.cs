using System;

using FowlFever.Conjugal.Affixing;

using JetBrains.Annotations;

namespace FowlFever.Conjugal {
    [PublicAPI]
    public readonly struct UnitOfMeasure {
        public const string DefaultJoinerString = " ";
        public const Joiner DefaultJoinerEnum   = Affixing.Joiner.Space;

        [NotNull] public readonly Plurable Name;
        [CanBeNull]
        public readonly Plurable? Symbol;
        public readonly Affix  Affix;
        public readonly string Joiner;

        public UnitOfMeasure(
            [NotNull] Plurable name,
            [CanBeNull]
            Plurable? symbol,
            [CanBeNull]
            string joiner = DefaultJoinerString,
            Affix affix = Affix.Suffix
        ) {
            Console.WriteLine("Creating a new UoM with:");
            Console.WriteLine($"\tjoiner: [{joiner}]");

            Name   = name;
            Symbol = symbol;
            Joiner = joiner;
            Affix  = affix;
        }

        public UnitOfMeasure(
            [NotNull] Plurable name,
            [CanBeNull]
            Plurable? symbol,
            Joiner joiner = DefaultJoinerEnum,
            Affix affix = Affix.Suffix
        ) : this(
            name: name,
            symbol: symbol,
            joiner: joiner.AsString(),
            affix: affix
        ) { }

        public UnitOfMeasure(
            [NotNull] Plurable name,
            string joiner = DefaultJoinerString,
            Affix affix = Affix.Suffix
        ) : this(
            name: name,
            symbol: default,
            joiner: joiner,
            affix: affix
        ) { }

        public UnitOfMeasure(
            [NotNull] Plurable name,
            Joiner joiner,
            Affix affix = Affix.Suffix
        ) : this(
            name,
            default,
            joiner,
            affix
        ) { }


        // public UnitOfMeasure(
        //     Pluric name,
        //     [CanBeNull]
        //     Pluric? symbol = default,
        //     [CanBeNull]
        //     string joiner = "",
        //     Affix affix = Affix.Suffix
        // ) : this(
        //     (IPluric) name,
        //     symbol,
        //     joiner,
        //     affix
        // ) { }
        //
        // public UnitOfMeasure(
        //     Pluric name,
        //     [CanBeNull]
        //     Pluric? symbol = default,
        //     Joiner joiner = Affixing.Joiner.None,
        //     Affix affix = Affix.Suffix
        // ) : this(
        //     (IPluric) name,
        //     symbol,
        //     joiner,
        //     affix
        // ) { }

        public override string ToString() {
            Console.WriteLine($"joiner: [{Joiner}]");
            Console.WriteLine($"affix: {Affix}");
            return $"[{Name} ({Symbol?.ToString() ?? $"no {nameof(Symbol)} defined"})]";
        }
    }
}
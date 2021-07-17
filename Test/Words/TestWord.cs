using System;
using System.Collections.Generic;

using FowlFever.Conjugal;
using FowlFever.Conjugal.Affixing;
using FowlFever.Conjugal.Annotations;

using Humanizer;

namespace Test.Words {
    public abstract class TestWord : IConjugal {
        public abstract string         Lemma           { get; }
        public abstract string         Singular        { get; }
        public abstract string         Plural          { get; }
        public abstract UnitOfMeasure? UnitOfMeasure   { get; }
        public abstract LetterCasing?  PreferredCasing { get; }
        public abstract Plurable?      Abbreviation    { get; }
        public abstract Countability   Countability    { get; }
        public abstract bool           IsProperNoun    { get; }
        public          string         NounalVerb      => throw new NotImplementedException();
        public abstract string         Quantity0       { get; }
        public abstract string         Quantity1       { get; }
        public abstract string         Quantity2       { get; }

        public static List<TestWord> Words => new() {
            new Die(),
            new Rock(),
            new Sheep(),
            new Xbox(),
            new ImportantThing(),
            new SentientAnimal(),
            new TheMan(),
            new SaveData(),
            new Brother(),
            new MarioBrother(),
            new Junk(),
            new PBJ(),
            new Pants()
        };
    }

    [Abbreviation("dX", "dXs")]
    [CollectiveNoun("roll", "rolls")]
    internal class Die : TestWord {
        public override string         Lemma           { get; } = "die";
        public override bool           IsProperNoun    { get; } = false;
        public override string         Quantity0       { get; } = "0 dXs";
        public override string         Quantity1       { get; } = "1 dX";
        public override string         Quantity2       { get; } = "2 dXs";
        public override string         Singular        => Lemma;
        public override string         Plural          { get; } = "dice";
        public override UnitOfMeasure? UnitOfMeasure   { get; } = default;
        public override LetterCasing?  PreferredCasing { get; } = default;
        public override Plurable?      Abbreviation    { get; } = Plurable.Of("dX", "dXs");
        public override Countability   Countability    { get; } = Countability.Countable;
    }

    [UnitOfMeasure("kg", joiner: Joiner.Space)]
    [CollectiveNoun("boulder")]
    internal class Rock : TestWord {
        public override string         Lemma           { get; } = "rock";
        public override bool           IsProperNoun    { get; } = false;
        public override string         Quantity0       { get; } = "0 kg";
        public override string         Quantity1       { get; } = "1 kg";
        public override string         Quantity2       { get; } = "2 kg";
        public override string         Singular        => Lemma;
        public override string         Plural          { get; } = "rocks";
        public override UnitOfMeasure? UnitOfMeasure   => new UnitOfMeasure("kg");
        public override LetterCasing?  PreferredCasing { get; } = default;
        public override Plurable?      Abbreviation    => default;
        public override Countability   Countability    { get; } = Countability.Countable;
    }

    [Uncountable]
    [UnitOfMeasure("head", "🐑", Joiner.Space)]
    [CollectiveNoun("flock", "flocks")]
    internal class Sheep : TestWord {
        public override string         Lemma           { get; } = "sheep";
        public override bool           IsProperNoun    { get; } = false;
        public override string         Quantity0       { get; } = "0 🐑";
        public override string         Quantity1       { get; } = "1 🐑";
        public override string         Quantity2       { get; } = "2 🐑";
        public override string         Singular        => Lemma;
        public override string         Plural          { get; } = "sheep";
        public override UnitOfMeasure? UnitOfMeasure   { get; } = new UnitOfMeasure("head", "🐑", Joiner.Space);
        public override LetterCasing?  PreferredCasing { get; } = default;
        public override Plurable?      Abbreviation    => default;
        public override Countability   Countability    { get; } = Countability.Uncountable;
    }

    [ProperNoun]
    [Lemma("Animal")]
    internal class SentientAnimal : TestWord {
        public override string         Lemma           { get; } = "Animal";
        public override string         Singular        => Lemma;
        public override string         Plural          { get; } = "Animals";
        public override UnitOfMeasure? UnitOfMeasure   { get; } = default;
        public override LetterCasing?  PreferredCasing { get; } = LetterCasing.Title;
        public override Plurable?      Abbreviation    { get; } = default;
        public override bool           IsProperNoun    { get; } = true;
        public override string         Quantity0       { get; } = "0 Animals";
        public override string         Quantity1       { get; } = "1 Animal";
        public override string         Quantity2       { get; } = "2 Animals";
        public override Countability   Countability    { get; } = Countability.Countable;
    }

    [ProperNoun]
    [Lemma("XBox")]
    [CollectiveNoun("series")]
    internal class Xbox : TestWord {
        public override string         Lemma           { get; } = "XBox";
        public override string         Singular        => Lemma;
        public override string         Plural          { get; } = "XBoxes";
        public override UnitOfMeasure? UnitOfMeasure   { get; } = default;
        public override LetterCasing?  PreferredCasing { get; } = default;
        public override Plurable?      Abbreviation    => default;
        public override bool           IsProperNoun    { get; } = true;
        public override string         Quantity0       { get; } = "0 XBoxes";
        public override string         Quantity1       { get; } = "1 XBox";
        public override string         Quantity2       { get; } = "2 XBoxes";
        public override Countability   Countability    { get; } = Countability.Countable;
    }

    [ProperNoun]
    [Plural("The Mans")]
    [UnitOfMeasure("🕴️", joiner: Joiner.Space)]
    internal class TheMan : TestWord {
        public override string         Lemma           { get; } = "The Man";
        public override string         Singular        => Lemma;
        public override string         Plural          { get; } = "The Mans";
        public override UnitOfMeasure? UnitOfMeasure   { get; } = new UnitOfMeasure("🕴️");
        public override LetterCasing?  PreferredCasing { get; } = LetterCasing.Title;
        public override Plurable?      Abbreviation    { get; } = default;
        public override bool           IsProperNoun    { get; } = true;
        public override string         Quantity0       { get; } = "0 🕴️";
        public override string         Quantity1       { get; } = "1 🕴️";
        public override string         Quantity2       { get; } = "2 🕴️";
        public override Countability   Countability    { get; } = Countability.Countable;
    }

    [PreferredCasing(LetterCasing.Title)]
    [ProperNoun]
    [Plural("Important Stuff")]
    internal class ImportantThing : TestWord {
        public override string         Lemma           { get; } = "Important Thing";
        public override bool           IsProperNoun    { get; } = true;
        public override string         Quantity0       { get; } = "0 Important Stuff";
        public override string         Quantity1       { get; } = "1 Important Thing";
        public override string         Quantity2       { get; } = "2 Important Stuff";
        public override string         Singular        => Lemma;
        public override string         Plural          { get; } = "Important Stuff";
        public override UnitOfMeasure? UnitOfMeasure   { get; } = default;
        public override LetterCasing?  PreferredCasing { get; } = LetterCasing.Title;
        public override Plurable?      Abbreviation    => default;
        public override Countability   Countability    { get; } = Countability.Countable;
    }

    [Lemma("save datum")]
    [CollectiveNoun("memory pack", "memory packs")]
    internal class SaveData : TestWord {
        public override string         Lemma           { get; } = "save datum";
        public override bool           IsProperNoun    { get; } = false;
        public override string         Quantity0       { get; } = "0 save data";
        public override string         Quantity1       { get; } = "1 save datum";
        public override string         Quantity2       { get; } = "2 save data";
        public override string         Singular        => Lemma;
        public override string         Plural          { get; } = "save data";
        public override UnitOfMeasure? UnitOfMeasure   { get; } = default;
        public override LetterCasing?  PreferredCasing { get; } = default;
        public override Plurable?      Abbreviation    => default;
        public override Countability   Countability    { get; } = Countability.Countable;
    }

    [Lemma("peanut butter & jelly")]
    [Abbreviation("PB&J", "PB&Js")]
    // ReSharper disable once InconsistentNaming
    internal class PBJ : TestWord {
        public override string         Singular        { get; } = "peanut butter & jelly";
        public override string         Plural          { get; } = "peanut butter & jellies";
        public override string         Lemma           { get; } = "peanut butter & jelly";
        public override bool           IsProperNoun    { get; } = false;
        public override string         Quantity0       { get; } = "0 PB&Js";
        public override string         Quantity1       { get; } = "1 PB&J";
        public override string         Quantity2       { get; } = "2 PB&Js";
        public override UnitOfMeasure? UnitOfMeasure   { get; } = default;
        public override LetterCasing?  PreferredCasing { get; } = default;
        public override Plurable?      Abbreviation    { get; } = Plurable.Of("PB&J", "PB&Js");
        public override Countability   Countability    { get; } = Countability.Countable;
    }

    [Abbreviation("bro", "bros.")]
    [UnitOfMeasure(name: "bro", namePlural: "bros.", symbol: "👨‍👦", joiner: Joiner.Space)]
    [CollectiveNoun("melee")]
    internal class Brother : TestWord {
        public override string Plural { get; } = "brothers";
        public override UnitOfMeasure? UnitOfMeasure => new UnitOfMeasure(
            Plurable.Of("bro", "bros."),
            Plurable.Uncountable("👨‍👦"),
            Joiner.Space
        );
        public override LetterCasing? PreferredCasing { get; } = default;
        public override Plurable?     Abbreviation    { get; } = Plurable.Of("bro", "bros.");
        public override bool          IsProperNoun    { get; } = false;
        public override string        Quantity0       { get; } = "0 👨‍👦";
        public override string        Quantity1       { get; } = "1 👨‍👦";
        public override string        Quantity2       { get; } = "2 👨‍👦";
        public override string        Lemma           { get; } = "brother";
        public override string        Singular        { get; } = "brother";
        public override Countability  Countability    { get; } = Countability.Countable;
    }

    [PreferredCasing(LetterCasing.Sentence)]
    [ProperNoun]
    [Countability(Countability.Countable)]
    [UnitOfMeasure("🍄")]
    [Abbreviation("Mario bro", "Mario bros.")]
    [CollectiveNoun("super show", Countability.Countable)]
    internal class MarioBrother : TestWord {
        public override string         Plural          { get; } = "Mario brothers";
        public override UnitOfMeasure? UnitOfMeasure   { get; } = new UnitOfMeasure(Plurable.Uncountable("🍄"));
        public override LetterCasing?  PreferredCasing { get; } = LetterCasing.Sentence;
        public override Plurable?      Abbreviation    { get; } = Plurable.Of("Mario bro", "Mario bros.");
        public override bool           IsProperNoun    { get; } = true;
        public override string         Quantity0       { get; } = "0 🍄";
        public override string         Quantity1       { get; } = "1 🍄";
        public override string         Quantity2       { get; } = "2 🍄";
        public override string         Lemma           { get; } = "Mario brother";
        public override string         Singular        { get; } = "Mario brother";
        public override Countability   Countability    { get; } = Countability.Countable;
    }

    [Uncountable]
    public class Junk : TestWord {
        public override string         Plural          { get; } = "junk";
        public override UnitOfMeasure? UnitOfMeasure   { get; } = default;
        public override LetterCasing?  PreferredCasing { get; } = default;
        public override Plurable?      Abbreviation    { get; } = default;
        public override Countability   Countability    { get; } = Countability.Uncountable;
        public override bool           IsProperNoun    { get; } = default;
        public override string         Quantity0       { get; } = "0 junk";
        public override string         Quantity1       { get; } = "1 junk";
        public override string         Quantity2       { get; } = "2 junk";
        public override string         Lemma           { get; } = "junk";
        public override string         Singular        { get; } = "junk";
    }

    [UnitOfMeasure("pair", namePlural: "pairs")]
    [Singular("pair of pants")]
    [Plural("pairs of pants")]
    [CollectiveNoun("hosing", "hosings")]
    public class Pants : TestWord {
        public override string         Lemma           { get; } = "pants";
        public override bool           IsProperNoun    { get; } = false;
        public override string         Quantity0       { get; } = "0 pairs";
        public override string         Quantity1       { get; } = "1 pair";
        public override string         Quantity2       { get; } = "2 pairs";
        public override string         Singular        { get; } = "pair of pants";
        public override string         Plural          { get; } = "pairs of pants";
        public override Countability   Countability    { get; } = Countability.Countable;
        public override UnitOfMeasure? UnitOfMeasure   { get; } = new UnitOfMeasure(Plurable.Of("pair", "pairs"));
        public override LetterCasing?  PreferredCasing { get; } = default;
        public override Plurable?      Abbreviation    { get; } = default;
    }
}
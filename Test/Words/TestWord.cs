using System;
using System.Collections.Generic;

using FowlFever.Conjugal;
using FowlFever.Conjugal.Affixing;
using FowlFever.Conjugal.Annotations;

using Humanizer;

namespace Test.Words {
    public abstract class TestWord : IConjugal {
        public abstract string         Lemma              { get; }
        public abstract string         Singular           { get; }
        public abstract string         Plural             { get; }
        public abstract UnitOfMeasure? UnitOfMeasure      { get; }
        public abstract LetterCasing?  PreferredCasing    { get; }
        public abstract string         Abbreviation       { get; }
        public abstract string         PluralAbbreviation { get; }
        public abstract bool           IsProperNoun       { get; }
        public          string         NounalVerb         => throw new NotImplementedException();

        public static List<TestWord> Words => new() {
            new Die(),
            new Rock(),
            new Sheep(),
            new Xbox(),
            new ImportantThing(),
            new SentientAnimal(),
            new TheMan(),
            new SaveData()
        };
    }

    [Abbreviation("dX", "dXs")]
    internal class Die : TestWord {
        public override string         Lemma              { get; } = "die";
        public override string         PluralAbbreviation { get; } = "dXs";
        public override bool           IsProperNoun       { get; } = false;
        public override string         Singular           => Lemma;
        public override string         Plural             { get; } = "dice";
        public override UnitOfMeasure? UnitOfMeasure      { get; } = default;
        public override LetterCasing?  PreferredCasing    { get; } = default;
        public override string         Abbreviation       { get; } = "dX";
    }

    [UnitOfMeasure("kg")]
    internal class Rock : TestWord {
        public override string         Lemma              { get; } = "rock";
        public override string         PluralAbbreviation => Lemma;
        public override bool           IsProperNoun       { get; } = false;
        public override string         Singular           => Lemma;
        public override string         Plural             { get; } = "rocks";
        public override UnitOfMeasure? UnitOfMeasure      => new UnitOfMeasure("kg");
        public override LetterCasing?  PreferredCasing    { get; } = default;
        public override string         Abbreviation       => Lemma;
    }

    [Uncountable]
    [UnitOfMeasure("head", "🐑", Joiner.Space)]
    internal class Sheep : TestWord {
        public override string         Lemma              { get; } = "sheep";
        public override string         PluralAbbreviation { get; } = "sheep";
        public override bool           IsProperNoun       { get; } = false;
        public override string         Singular           => Lemma;
        public override string         Plural             { get; } = "sheep";
        public override UnitOfMeasure? UnitOfMeasure      { get; } = new UnitOfMeasure("head", "🐑", Joiner.Space);
        public override LetterCasing?  PreferredCasing    { get; } = default;
        public override string         Abbreviation       => Lemma;
    }

    [ProperNoun]
    [Lemma("Animal")]
    internal class SentientAnimal : TestWord {
        public override string         Lemma              { get; } = "Animal";
        public override string         Singular           => Lemma;
        public override string         Plural             { get; } = "Animals";
        public override UnitOfMeasure? UnitOfMeasure      { get; } = default;
        public override LetterCasing?  PreferredCasing    { get; } = LetterCasing.Title;
        public override string         Abbreviation       { get; } = "Animal";
        public override string         PluralAbbreviation { get; } = "Animal";
        public override bool           IsProperNoun       { get; } = true;
    }

    [ProperNoun]
    [Lemma("XBox")]
    internal class Xbox : TestWord {
        public override string         Lemma              { get; } = "XBox";
        public override string         Singular           => Lemma;
        public override string         Plural             { get; } = "XBoxes";
        public override UnitOfMeasure? UnitOfMeasure      { get; } = default;
        public override LetterCasing?  PreferredCasing    { get; } = default;
        public override string         Abbreviation       => Lemma;
        public override string         PluralAbbreviation => Lemma;
        public override bool           IsProperNoun       { get; } = true;
    }

    [ProperNoun]
    [Plural("The Mans")]
    [UnitOfMeasure("🕴️", Joiner.Space)]
    internal class TheMan : TestWord {
        public override string         Lemma              { get; } = "The Man";
        public override string         Singular           => Lemma;
        public override string         Plural             { get; } = "The Mans";
        public override UnitOfMeasure? UnitOfMeasure      { get; } = new UnitOfMeasure("🕴️");
        public override LetterCasing?  PreferredCasing    { get; } = LetterCasing.Title;
        public override string         Abbreviation       { get; } = "The Man";
        public override string         PluralAbbreviation { get; } = "The Man";
        public override bool           IsProperNoun       { get; } = true;
    }

    [PreferredCasing(LetterCasing.Title)]
    internal class ImportantThing : TestWord {
        public override string         Lemma              { get; } = "Important Thing";
        public override string         PluralAbbreviation => Lemma;
        public override bool           IsProperNoun       { get; } = true;
        public override string         Singular           => Lemma;
        public override string         Plural             { get; } = "Important Things";
        public override UnitOfMeasure? UnitOfMeasure      { get; } = default;
        public override LetterCasing?  PreferredCasing    { get; } = LetterCasing.Title;
        public override string         Abbreviation       => Lemma;
    }

    [Lemma("save datum")]
    internal class SaveData : TestWord {
        public override string         Lemma              { get; } = "save datum";
        public override string         PluralAbbreviation => Lemma;
        public override bool           IsProperNoun       { get; } = false;
        public override string         Singular           => Lemma;
        public override string         Plural             { get; } = "save data";
        public override UnitOfMeasure? UnitOfMeasure      { get; } = default;
        public override LetterCasing?  PreferredCasing    { get; } = default;
        public override string         Abbreviation       => Lemma;
    }

    [Lemma("peanut butter & jelly")]
    [Abbreviation("PB&J", "PB&Js")]
    internal class PBJ : TestWord {
        public override string         Singular           { get; } = "peanut butter & jelly";
        public override string         Plural             { get; } = "peanut butter & jellies";
        public override string         Lemma              { get; } = "peanut butter & jelly";
        public override string         PluralAbbreviation { get; } = "PB&Js";
        public override bool           IsProperNoun       { get; } = false;
        public override UnitOfMeasure? UnitOfMeasure      { get; } = default;
        public override LetterCasing?  PreferredCasing    { get; } = default;
        public override string         Abbreviation       { get; } = "PB&J";
    }

    [Abbreviation("bro", "bros.")]
    [UnitOfMeasure("bros.", "👨‍👦", joiner: Joiner.Space)]
    internal class Brother : TestWord {
        public override string         Plural             { get; } = "brothers";
        public override UnitOfMeasure? UnitOfMeasure      { get; } = new UnitOfMeasure("bros.", "👨‍👦", Joiner.Space);
        public override LetterCasing?  PreferredCasing    { get; } = default;
        public override string         Abbreviation       { get; } = "bro";
        public override string         PluralAbbreviation { get; } = "bros.";
        public override bool           IsProperNoun       { get; } = false;
        public override string         Lemma              { get; } = "brother";
        public override string         Singular           { get; } = "brother";
    }

    [PreferredCasing(LetterCasing.Sentence)]
    [ProperNoun]
    [Countability(Countability.Countable)]
    [UnitOfMeasure("🍄")]
    [Abbreviation("Mario bro", "Mario bros.")]
    internal class MarioBrother : TestWord {
        public override string         Plural             { get; } = "Mario brothers";
        public override UnitOfMeasure? UnitOfMeasure      { get; } = new UnitOfMeasure("🍄");
        public override LetterCasing?  PreferredCasing    { get; } = LetterCasing.Sentence;
        public override string         Abbreviation       { get; } = "Mario bro";
        public override string         PluralAbbreviation { get; } = "Mario bros.";
        public override bool           IsProperNoun       { get; } = true;
        public override string         Lemma              { get; } = "Mario brother";
        public override string         Singular           { get; } = "Mario brother";
    }
}
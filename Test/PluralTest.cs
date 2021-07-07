using System;

using BenthicProfiler.Annotations;

using Humanizer;

using NUnit.Framework;

namespace Test {
    public class PluralTest {
        public abstract class TestWord {
            public abstract string ExpectedLemma  { get; }
            public abstract string ExpectedPlural { get; }

            public virtual bool IsProperNoun { get; } = false;
        }

        // [Plural("Dice")]
        private class Die : TestWord {
            public override string ExpectedLemma  { get; } = "die";
            public override string ExpectedPlural { get; } = "dice";
        }

        private class Rock : TestWord {
            public override string ExpectedLemma  { get; } = "rock";
            public override string ExpectedPlural { get; } = "rocks";
        }

        [Uncountable]
        private class Sheep : TestWord {
            public override string ExpectedLemma  { get; } = "sheep";
            public override string ExpectedPlural { get; } = "sheep";
        }

        [ProperNoun]
        [Lemma("Animal")]
        private class SentientAnimal : TestWord {
            public override string ExpectedLemma  { get; } = "Animal";
            public override string ExpectedPlural { get; } = "Animals";
            public override bool   IsProperNoun   { get; } = true;
        }

        [ProperNoun]
        [Lemma("XBox")]
        private class Xbox : TestWord {
            public override string ExpectedLemma  { get; } = "XBox";
            public override string ExpectedPlural { get; } = "XBoxes";
            public override bool   IsProperNoun   { get; } = true;
        }

        [ProperNoun]
        [Plural("The Mans")]
        private class TheMan : TestWord {
            public override string ExpectedLemma  { get; } = "The Man";
            public override string ExpectedPlural { get; } = "The Mans";
            public override bool   IsProperNoun   { get; } = true;
        }

        [Casing(LetterCasing.Title)]
        private class ImportantThing : TestWord {
            public override string ExpectedLemma  { get; } = "Important Thing";
            public override string ExpectedPlural { get; } = "Important Things";
        }

        private class SaveData : TestWord {
            public override string ExpectedLemma  { get; } = "save datum";
            public override string ExpectedPlural { get; } = "sava data";
        }

        private static TestWord[] Words = {
            new Die(),
            new Rock(),
            new Sheep(),
            new SentientAnimal(),
            new Xbox(),
            new TheMan(),
            new ImportantThing(),
            new SaveData(),
        };

        [Test]
        public void ProperNoun(
            [ValueSource(nameof(Words))]
            TestWord word
        ) {
            Assert.That(word.GetType().IsProperNoun, Is.EqualTo(word.IsProperNoun));
        }

        [Test]
        public void Lemma(
            [ValueSource(nameof(Words))]
            TestWord word
        ) {
            Assert.That(word.GetType().Lemma, Is.EqualTo(word.ExpectedLemma));
        }

        [Test]
        public void Plural(
            [ValueSource(nameof(Words))]
            TestWord word
        ) {
            Assert.That(word.GetType().Pluralize(), Is.EqualTo(word.ExpectedPlural));
        }
    }
}
using System.Collections.Generic;

using FowlFever.Conjugal;

using NUnit.Framework;

using Test.Words;

namespace Test {
    public class ConjugalTests {
        private static List<TestWord> Words => TestWord.Words;

        [Test]
        public void ProperNoun(
            [ValueSource(nameof(Words))]
            TestWord word
        ) {
            Assert.That(word.GetType().IsProperNoun, Is.EqualTo(word.IsProperNoun), word.GetType().ToString);
        }

        [Test]
        public void Lemma(
            [ValueSource(nameof(Words))]
            TestWord word
        ) {
            Assert.That(word.GetType().Lemma, Is.EqualTo(word.Lemma));
        }

        [Test]
        public void Singular(
            [ValueSource(nameof(Words))]
            TestWord word
        ) {
            Assert.That(word.GetType().Singular, Is.EqualTo(word.Singular));
        }

        [Test]
        public void Plural(
            [ValueSource(nameof(Words))]
            TestWord word
        ) {
            Assert.That(word.GetType().Pluralize(), Is.EqualTo(word.Plural));
        }

        [Test]
        public void Countability(
            [ValueSource(nameof(Words))]
            TestWord word
        ) {
            Assert.That(word.GetType().Countability(), Is.EqualTo(word.Countability), word.GetType().ToString);
        }

        [Test]
        public void Abbreviation(
            [ValueSource(nameof(Words))]
            TestWord word
        ) {
            Assert.That(word.GetType().Abbreviation(), Is.EqualTo(word.Abbreviation), word.GetType().ToString);
        }

        [Test]
        public void Abbreviate(
            [ValueSource(nameof(Words))]
            TestWord word
        ) {
            Assert.Multiple(
                () => {
                    Assert.That(word.GetType().Abbreviate(0), Is.EqualTo(word.Abbreviation?.Plural),   word.GetType().ToString);
                    Assert.That(word.GetType().Abbreviate(1), Is.EqualTo(word.Abbreviation?.Singular), word.GetType().ToString);
                    Assert.That(word.GetType().Abbreviate(2), Is.EqualTo(word.Abbreviation?.Plural),   word.GetType().ToString);
                }
            );
        }

        [Test]
        public void Pluralize(
            [ValueSource(nameof(Words))]
            TestWord word
        ) {
            Assert.Multiple(
                () => {
                    Assert.That(word.GetType().Pluralize(0), Is.EqualTo(word.Plural),   word.GetType().ToString);
                    Assert.That(word.GetType().Pluralize(1), Is.EqualTo(word.Singular), word.GetType().ToString);
                    Assert.That(word.GetType().Pluralize(2), Is.EqualTo(word.Plural),   word.GetType().ToString);
                }
            );
        }

        [Test]
        public void Quantify(
            [ValueSource(nameof(Words))]
            TestWord word
        ) {
            Assert.Multiple(
                () => {
                    Assert.That(word.GetType().Quantify(0).ToString, Is.EqualTo(word.Quantity0), word.GetType().ToString);
                    Assert.That(word.GetType().Quantify(1).ToString, Is.EqualTo(word.Quantity1), word.GetType().ToString);
                    Assert.That(word.GetType().Quantify(2).ToString, Is.EqualTo(word.Quantity2), word.GetType().ToString);
                }
            );
        }
    }
}
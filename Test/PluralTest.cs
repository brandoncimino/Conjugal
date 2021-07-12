using System.Collections.Generic;

using FowlFever.Conjugal;

using NUnit.Framework;

using Test.Words;

namespace Test {
    public class PluralTest {
        private static List<TestWord> Words => TestWord.Words;

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
            Assert.That(word.GetType().Lemma, Is.EqualTo(word.Lemma));
        }

        [Test]
        public void Singular(
            [ValueSource(nameof(Words))]
            TestWord word
        ) {
            Assert.That(word.GetType().Singular, Is.EqualTo(word.Lemma));
        }

        [Test]
        public void Plural(
            [ValueSource(nameof(Words))]
            TestWord word
        ) {
            Assert.That(word.GetType().Pluralize(), Is.EqualTo(word.Plural));
        }
    }
}
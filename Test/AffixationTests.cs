using System;

using FowlFever.Conjugal.Affixing;

using NUnit.Framework;

namespace Test {
    public class AffixationTests {
        [Test]
        [TestCase(null, null, null, "")]
        public void Prefix(string root, string prefix, string joiner, string expected) {
            var prefixed = root.Prefix(prefix, joiner);
            Assert.That(prefixed.Render, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(null, null, null, "")]
        public void Suffix(string root, string suffix, string joiner, string expected) {
            var suffixed = root.Suffix(suffix, joiner);
            Assert.That(suffixed.Render, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(null, null, 0, null, "")]
        public void Infix(string root, string infix, int insertionPoint, string joiner, string expected) {
            var infixed = root.Infix(infix, insertionPoint, joiner);
            Assert.That(infixed.Render, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(null, null, null, "")]
        public void Ambifix(string root, string ambifix, string joiner, string expected) {
            var ambifixed = root.Ambifix(ambifix, joiner);
            Assert.That(ambifixed.Render, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(null, null, null, "")]
        public void Circumfix(string root, string circumfix, string joiner, string expected) {
            var circumfixed = root.Circumfix(circumfix, joiner);
            Assert.That(circumfixed.Render, Is.EqualTo(expected));
        }

        [Test]
        public void EmptyMorphemeMeansNoJoiner([Values] AffixFlavor flavor, [Values("", null)] string? boundMorpheme) {
            const string stem = "STEM";
            Assert.That(new Affixation(flavor) { Stem = stem, BoundMorpheme = boundMorpheme, Joiner = "JOINER" }.Render, Is.EqualTo(stem));
        }

        [Test]
        public void EmptyStemMeansEmptyResult([Values] AffixFlavor flavor, [Values("", null)] string? stem) {
            const string boundMorpheme = "BOUND_MORPHEME";
            Assert.That(new Affixation(flavor) { Stem = stem, BoundMorpheme = boundMorpheme, Joiner = "JOINER" }.Render, Is.Empty);
        }

        [Test]
        public void DefaultAffix() {
            Affixation affixation = default;
            Assert.That(affixation.Render, Is.EqualTo(""));
        }

        [Test]
        public void FlavoredNullMemberAffix([Values] AffixFlavor flavor) {
            try {
                var affixation = new Affixation { AffixFlavor = flavor };
                Assert.That(affixation.Render, Is.EqualTo(""));
            }
            catch (NotImplementedException e) {
                throw new IgnoreException($"{flavor} is not fully implemented: {e.Message}", e);
            }
        }
    }
}
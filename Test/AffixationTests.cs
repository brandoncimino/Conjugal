using System;

using FowlFever.Conjugal.Affixing;

using NUnit.Framework;

namespace Test {
    public class AffixationTests {
        [TestCase("abc",  "",     "yolo", "abc")]
        [TestCase("yolo", "swag", "#",    "swag#yolo")]
        public void Prefix(string stem, string prefix, string joiner, string expected) {
            var prefixed = stem.Prefix(prefix, joiner);
            Assert.That(prefixed.Render(),                  Is.EqualTo(expected));
            Assert.That(prefixed.Stem.ToString(),           Is.EqualTo(stem));
            Assert.That(prefixed.BoundMorpheme.ToString(),  Is.EqualTo(prefix));
            Assert.That(prefixed.BoundMorpheme2.IsEmpty,    Is.True, "BoundMorpheme2 should be empty");
            Assert.That(prefixed.BoundMorpheme2.ToString(), Is.EqualTo(""));
            Assert.That(prefixed.Length,                    Is.EqualTo(expected.Length));
            Assert.That(prefixed.InsertionPoint,            Is.EqualTo(Index.Start));
        }

        [TestCase("yolo", "swag", "#", "yolo#swag")]
        public void Suffix(string stem, string suffix, string joiner, string expected) {
            var suffixed = stem.Suffix(suffix, joiner);
            Assert.That(suffixed.Render(), Is.EqualTo(expected));
        }

        [TestCase("yolo", "swag", 2, "-", "yo-swag-lo")]
        public void Infix(string stem, string infix, int insertionPoint, string joiner, string expected) {
            var infixed = stem.Infix(infix, insertionPoint, joiner);
            Assert.That(infixed.Render(), Is.EqualTo(expected));
        }

        [TestCase("yolo", "swag", "-", "swag-yolo-swag")]
        public void Ambifix(string stem, string ambifix, string joiner, string expected) {
            var ambifixed = stem.Ambifix(ambifix, joiner);
            Assert.That(ambifixed.Render(), Is.EqualTo(expected));
        }

        [TestCase("yolo", "swag", "#", "-", "swag-yolo-#")]
        public void Circumfix(string stem, string prefix, string suffix, string joiner, string expected) {
            var circumfixed = stem.Circumfix(prefix, suffix, joiner);
            Assert.That(circumfixed.Render(), Is.EqualTo(expected));
        }

        [TestCase("yolo", "-", "yolo-yolo")]
        public void Duplifix(string stem, string joiner, string expected) {
            var duplifixed = stem.Duplifix(joiner);
            Assert.That(duplifixed.Render(), Is.EqualTo(expected));
        }

        [Test]
        public void EmptyMorphemeMeansNoJoiner([Values] AffixFlavor flavor, [Values("", null)] string? boundMorpheme) {
            const string stem = "STEM";
            Assume.That(flavor, Is.Not.AnyOf(AffixFlavor.Disfix, AffixFlavor.Transfix), $"{flavor} is not implemented!");
            Assert.That(new Affixation(flavor) { Stem = stem, BoundMorpheme = boundMorpheme, Joiner = "JOINER" }.Render(), Is.EqualTo(stem));
        }

        [Test]
        public void EmptyStemMeansEmptyResult([Values] AffixFlavor flavor, [Values("", null)] string? stem) {
            const string boundMorpheme = "BOUND_MORPHEME";
            Assert.That(new Affixation(flavor) { Stem = stem, BoundMorpheme = boundMorpheme, Joiner = "JOINER" }.Render(), Is.Empty);
        }

        [Test]
        public void DefaultAffix() {
            Affixation affixation = default;
            Assert.That(affixation.Render(), Is.EqualTo(""));
        }

        [Test]
        public void FlavoredNullMemberAffix([Values] AffixFlavor flavor) {
            try {
                var affixation = new Affixation { Flavor = flavor };
                Assert.That(affixation.Render(), Is.EqualTo(""));
            }
            catch (NotImplementedException e) {
                throw new IgnoreException($"{flavor} is not fully implemented: {e.Message}", e);
            }
        }
    }
}
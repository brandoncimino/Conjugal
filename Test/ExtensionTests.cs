using System;

using FowlFever.Conjugal.Affixing;

using NUnit.Framework;

namespace Test {
    public class ExtensionTests {
        [Test]
        [TestCase(null, null, null, "")]
        public void Prefix(string root, string prefix, string joiner, string expected) {
            var prefixed = root.Prefix(prefix, joiner);
            Assert.That(prefixed, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(null, null, null, "")]
        public void Suffix(string root, string suffix, string joiner, string expected) {
            var suffixed = root.Suffix(suffix, joiner);
            Assert.That(suffixed, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(null, null, 0, null, "")]
        public void Infix(string root, string infix, int insertionPoint, string joiner, string expected) {
            var infixed = root.Infix(infix, insertionPoint, joiner);
            Assert.That(infixed, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(null, null, null, "")]
        public void Ambifix(string root, string ambifix, string joiner, string expected) {
            var ambifixed = root.Ambifix(ambifix, joiner);
            Assert.That(ambifixed, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(null, null, null, "")]
        public void Circumfix(string root, string circumfix, string joiner, string expected) {
            var circumfixed = root.Circumfix(circumfix, joiner);
            Assert.That(circumfixed, Is.EqualTo(expected));
        }

        [Test]
        public void NullableAffix([Values] AffixFlavor flavor) {
            const string expected = "";

            try {
                int? index      = flavor.RequiresIndex() ? 0 : null;
                var  affixation = new Affixation(flavor, null, null, index, null);
                Assert.That(affixation.Render(), Is.EqualTo(expected));
            }
            catch (NotImplementedException e) {
                throw new IgnoreException($"{flavor} is not fully implemented: {e.Message}", e);
            }
        }
    }
}
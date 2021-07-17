using FowlFever.Conjugal;

using NUnit.Framework;

namespace Test {
    public class PlurableTests {
        [Test]
        [TestCase("box",  "boxes")]
        [TestCase("egg",  "eggs")]
        [TestCase("yolo", "yolos")]
        [TestCase("x",    "xes")]
        [TestCase("ox",   "oxen")]
        [TestCase("5",    "5s")]
        public void ExplicitlyCountable(string singular, string plural) {
            Plurable plurable = Plurable.Of(singular, Countability.Countable);

            Assert.Multiple(
                () => {
                    Assert.That(plurable, Has.Property(nameof(plurable.Plural)).EqualTo(plural));
                    Assert.That(plurable, Has.Property(nameof(plurable.Singular)).EqualTo(singular));
                    Assert.That(plurable, Has.Property(nameof(plurable.Countability)).EqualTo(Countability.Countable));
                }
            );
        }

        [TestCase("box")]
        [TestCase("egg")]
        [TestCase("ox")]
        [TestCase("datum")]
        [TestCase("x")]
        public void UncountableCast(string singular) {
            Plurable plurable = singular;
            Assert.Multiple(
                () => {
                    Assert.That(plurable, Has.Property(nameof(plurable.Singular)).EqualTo(singular));
                    Assert.That(plurable, Has.Property(nameof(plurable.Plural)).EqualTo(singular));
                    Assert.That(plurable, Has.Property(nameof(plurable.Countability)).EqualTo(Countability.Uncountable));
                }
            );
        }

        [TestCase("sheep", "sheep", Countability.Uncountable)]
        [TestCase("a",     "a",     Countability.Uncountable)]
        [TestCase("x",     "y",     Countability.Countable)]
        [TestCase("yolo",  "swag",  Countability.Countable)]
        public void InferredCountability(string singular, string plural, Countability expectedCountability) {
            Plurable plurable = Plurable.Of(singular, plural);
            Assert.Multiple(
                () => {
                    Assert.That(plurable, Has.Property(nameof(plurable.Singular)).EqualTo(singular));
                    Assert.That(plurable, Has.Property(nameof(plurable.Plural)).EqualTo(plural));
                    Assert.That(plurable, Has.Property(nameof(plurable.Countability)).EqualTo(expectedCountability));
                }
            );
        }
    }
}
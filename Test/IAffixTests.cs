using System;

using FowlFever.Conjugal.Affixing;

using NUnit.Framework;

namespace Test;

public class IAffixTests {
    [Test]
    [TestCase("pre", "post")]
    public void ICircumfix_Properties(string prefix, string suffix) {
        ICircumfix circumfix = new Circumfix(prefix, suffix);
        Console.WriteLine(circumfix.GetPrefix());
        Assert.That(circumfix.GetPrefix(),   Is.EqualTo(prefix));
        Assert.That(circumfix.GetSuffix(),   Is.EqualTo(suffix));
        Assert.That(circumfix.Prefix,        Is.EqualTo(prefix));
        Assert.That(circumfix.Suffix,        Is.EqualTo(suffix));
        Assert.That(circumfix.BoundMorpheme, Is.EqualTo(prefix));
    }

    [Test]
    [TestCase("yolo")]
    public void IAmbifix_Properties(string boundMorpheme) {
        IAmbifix ambifix = new Ambifix(boundMorpheme);
        Assert.That(ambifix.GetPrefix(),   Is.EqualTo(boundMorpheme));
        Assert.That(ambifix.GetSuffix(),   Is.EqualTo(boundMorpheme));
        Assert.That(ambifix.Prefix,        Is.EqualTo(boundMorpheme));
        Assert.That(ambifix.Suffix,        Is.EqualTo(boundMorpheme));
        Assert.That(ambifix.BoundMorpheme, Is.EqualTo(boundMorpheme));
    }
}
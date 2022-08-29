using System;

using FowlFever.Conjugal.Affixing;

using NUnit.Framework;

namespace Test;

public class AffixRef_WithStem_Tests {
    [TestCase("a", "b", "-", "b-a")]
    [TestCase("a", "",  "-", "a")]
    [TestCase("",  "b", "-", "")]
    [TestCase("a", "b", "",  "ba")]
    public void IPrefix_WithStem(string stem, string prefix, string joiner, string expected) {
        var prefixRef   = Affix.Prefix(prefix, joiner);
        var prefixation = prefixRef.WithStem(stem);
        Assert.That(prefixation.ToString(), Is.EqualTo(expected));
    }

    [TestCase("a", "b", "-", "a-b")]
    [TestCase("a", "",  "-", "a")]
    [TestCase("",  "b", "-", "")]
    [TestCase("a", "b", "",  "ab")]
    public void ISuffix_WithStem(string stem, string suffix, string joiner, string expected) {
        var suffixRef   = Affix.Suffix(suffix, joiner);
        var suffixation = suffixRef.WithStem(stem);
        Assert.That(suffixation.ToString(), Is.EqualTo(expected));
    }

    [TestCase("yolo",  "swag", 2, "-", "yo-swag-lo")]
    [TestCase("yolo",  "",     2, "-", "yolo")]
    [TestCase("house", "iz",   1, "",  "hizouse")]
    public void IInfix_WithStem(string stem, string infix, int insertionPoint, string joiner, string expected) {
        var infixRef   = Affix.Infix(infix, insertionPoint, joiner);
        var infixation = infixRef.WithStem(stem);
        Assert.That(infixation.ToString(), Is.EqualTo(expected));
    }

    [TestCase("yolo", "a", "b", "-", "a-yolo-b")]
    public void ICircumfix_WithStem(string stem, string prefix, string suffix, string joiner, string expected) {
        var circumRef      = Affix.Circumfix(prefix, suffix, joiner);
        var circumfixation = circumRef.WithStem(stem);
        Assert.That(circumfixation.ToString(), Is.EqualTo(expected));
    }

    [TestCase("yolo", "swag", "-", "swag-yolo-swag")]
    public void IAmbifix_WithStem(string stem, string ambifix, string joiner, string expected) {
        var ambifixRef   = Affix.Ambifix(ambifix, joiner);
        var ambifixation = ambifixRef.WithStem(stem);
        Assert.That(ambifixation.ToString(), Is.EqualTo(expected));
    }

    [TestCase("yolo", "-", "yolo-yolo")]
    public void IDuplifix_WithStem(string stem, string joiner, string expected) {
        var duplifixRef = Affix.Duplifix(joiner);
        Console.WriteLine(duplifixRef.Describe());
        Console.WriteLine();
        Console.WriteLine(duplifixRef.Highlighted());
        Console.WriteLine();
        var duplifixation = duplifixRef.WithStem(stem);
        Console.WriteLine(duplifixation.Describe());
        Assert.That(duplifixation.ToString(), Is.EqualTo(expected));
    }
}
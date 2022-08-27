using System;

using FowlFever.Conjugal.Affixing;

using NUnit.Framework;

namespace Test;

public class AffixRef_PropertyExtension_Tests {
    #region Property Extensions

    [Test]
    public void AffixRef_ISuffix_PropertyExtensions() {
        var suffix = Affix.Suffix(Str);
        Console.WriteLine(suffix.Describe());

        Req.Equal(suffix.GetSuffix(),        Str);
        Req.Equal(suffix.GetBoundMorpheme(), Str);
    }

    [Test]
    public void AffixRef_ICircumfix_PropertyExtensions() {
        const string prefix = "PREFIX";
        const string suffix = "SUFFIX";

        var circumfix = Affix.Circumfix(prefix, suffix, Jt);
        var (pref, suff) = circumfix.GetBoundMorphemes();
        Console.WriteLine(circumfix.Describe());

        Req.Equal(circumfix.GetPrefix(), prefix);
        Req.Equal(circumfix.GetSuffix(), suffix);
        Req.Equal(pref,                  prefix);
        Req.Equal(suff,                  suffix);
    }

    [Test]
    public void AffixRef_IAmbifix_PropertyExtensions() {
        var ambifix = Affix.Ambifix(Str);
        var (pref, suff) = ambifix.GetBoundMorphemes();
        Console.WriteLine(ambifix.Describe());

        Req.Equal(ambifix.GetPrefix(), Str);
        Req.Equal(ambifix.GetSuffix(), Str);
        Req.Equal(pref,                Str);
        Req.Equal(suff,                Str);
    }

    [Test]
    public void AffixRef_IPrefix_PropertyExtensions() {
        var prefix = Affix.Prefix(Str);
        Console.WriteLine(prefix.Describe());

        Req.Equal(prefix.GetPrefix(),        Str);
        Req.Equal(prefix.GetBoundMorpheme(), Str);
        Req.Equal(prefix.GetJoiner(),        default);
    }

    #endregion

    #region Conversions

    private const string Str = "yolo";
    private const string Jt  = "JOINER";

    [Test]
    public void IPrefix_To_Prefix() {
        var prefixRef = Affix.Prefix(Str, Jt);
        var prefix    = prefixRef.ToPrefix();

        var expected = new Prefix(Str, Jt);
        Assert.That(prefix, Is.EqualTo(expected));
    }

    [Test]
    public void ISuffix_To_Suffix() {
        var suffixRef = Affix.Suffix(Str, Jt);
        var suffix    = suffixRef.ToSuffix();

        var expected = new Suffix(Str, Jt);
        Assert.That(suffix, Is.EqualTo(expected));
    }

    [Test]
    public void IInfix_To_Infix() {
        var infixRef = Affix.Infix(Str, ^3, Jt);
        var infix    = infixRef.ToInfix();

        var expected = new Infix(Str, ^3, Jt);
        Assert.That(infix, Is.EqualTo(expected));
    }

    [Test]
    public void ICircumfix_To_Circumfix() {
        var circumfixRef = Affix.Circumfix(Str, "SWAG", Jt);
        var circumfix    = circumfixRef.ToCircumfix();

        var expected = new Circumfix(Str, "SWAG", Jt);
        Assert.That(circumfix, Is.EqualTo(expected));
    }

    [Test]
    public void IAmbifix_To_Ambifix() {
        var ambifixRef = Affix.Ambifix(Str, Jt);
        Console.WriteLine(ambifixRef.Describe());
        var ambifix = ambifixRef.ToAmbifix();
        Console.WriteLine(ambifix.ToString());

        var expected = new Ambifix(Str, Jt);
        Assert.That(ambifix, Is.EqualTo(expected));
    }

    [Test]
    public void IDuplifix_To_Duplifix() {
        var duplifixRef = Affix.Duplifix(Jt);
        var duplifix    = duplifixRef.ToDuplifix();

        var expected = new Duplifix(Jt);
        Assert.That(duplifix, Is.EqualTo(expected));
    }

    #endregion
}
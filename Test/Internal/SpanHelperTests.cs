using System;
using System.Linq;

using FowlFever.Conjugal.Internal;

using NUnit.Framework;

namespace Test.Internal;

public class SpanHelperTests {
    [TestCase("a", "bcd", "xy", "")]
    public void MultiLength(string a, string b, string c, string d) {
        var expected = a.Length + b.Length + c.Length + d.Length;
        var actual   = SpanHelpers.Length<char>(a, b, c, d);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("--", "a", "bcd", "xy", "")]
    public void MultiLength_Joiner(string joiner, string a, string b, string c, string d) {
        var nonEmpty = new[] { a, b, c, d }.Where(it => string.IsNullOrEmpty(it) == false);
        var expected = string.Join(joiner, nonEmpty).Length;
        var actual   = SpanHelpers.LengthWithJoiner<char>(joiner, a, b, c, d);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    [TestCase("yolo", "#swag")]
    public void Write_Simple(string a, string b) {
        Span<char> span = stackalloc char[a.Length + b.Length];
        var        pos  = 0;

        span.Write(a, ref pos)
            .Write(b, ref pos);

        var spanStr = span.ToString();
        Assert.That(spanStr, Is.EqualTo(a + b));
    }

    [TestCase("--", "a", "bcd", "xz")]
    public void WriteJoin_NonEmpty(string joiner, string a, string b, string c) {
        var expected = string.Join(joiner, a, b, c);

        var actual = SpanHelpers.Join(joiner, a, b, c);

        Assert.That(actual, Is.EqualTo(expected));
    }
}
using System;
using System.Runtime.CompilerServices;

using NUnit.Framework;

namespace Test;

public static class Req {
    public static void True(bool result, [CallerArgumentExpression("result")] string? expression = default) {
        Assert.That(result, Is.True, expression);
    }

    public static void Equal<T>(ReadOnlySpan<T> actual, ReadOnlySpan<T> expected, [CallerArgumentExpression("actual")] string? _actual = default, [CallerArgumentExpression("expected")] string? _expected = default) {
        Assert.That(actual.ToArray(), Is.EqualTo(expected.ToArray()), $"{_actual} == {_expected}");
    }

    public static void Equal<T>(T actual, T expected, [CallerArgumentExpression("actual")] string? _actual = default, [CallerArgumentExpression("expected")] string? _expected = default) {
        Assert.That(actual, Is.EqualTo(expected), $"{_actual} == {_expected}");
    }
}
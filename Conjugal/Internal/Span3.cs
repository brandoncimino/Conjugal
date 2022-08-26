using System;

namespace FowlFever.Conjugal.Internal;

/// <summary>
/// A helper to group together multiple <see cref="ReadOnlySpan{T}"/>s.
/// </summary>
internal readonly ref struct Span3 {
    private ReadOnlySpan<char> A { get; }
    private ReadOnlySpan<char> B { get; }
    private ReadOnlySpan<char> C { get; }

    public Span3(
        ReadOnlySpan<char> a = default,
        ReadOnlySpan<char> b = default,
        ReadOnlySpan<char> c = default
    ) {
        A = a;
        B = b;
        C = c;
    }

    public int LengthWithJoiner(ReadOnlySpan<char> joiner) => SpanHelpers.LengthWithJoiner(joiner, A, B, C);
    public string JoinString(ReadOnlySpan<char> joiner = default) => SpanHelpers.Join(joiner, A, B, C);
}
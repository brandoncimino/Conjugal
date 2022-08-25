using System;

namespace FowlFever.Conjugal;

internal static class SpanHelpers {
    /// <summary>
    /// Helps you "fill" a <see cref="Span{T}"/> with stuff.
    /// </summary>
    /// <param name="span"></param>
    /// <param name="toAppend"></param>
    /// <param name="position"></param>
    /// <param name="shouldValidatePosition"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Span<T> Write<T>(this Span<T> span, ReadOnlySpan<T> toAppend, ref int position, bool shouldValidatePosition = false) {
        if (shouldValidatePosition) {
            span.RequireIndex(position)
                .RequireIndex(position + toAppend.Length);
        }

        foreach (var c in toAppend) {
            span[position] =  c;
            position       += 1;
        }

        return span;
    }

    public static int Length<T>(
        ReadOnlySpan<T> a,
        ReadOnlySpan<T> b,
        ReadOnlySpan<T> c = default,
        ReadOnlySpan<T> d = default,
        ReadOnlySpan<T> e = default,
        ReadOnlySpan<T> f = default,
        ReadOnlySpan<T> g = default,
        ReadOnlySpan<T> h = default
    ) {
        return a.Length + b.Length + c.Length + d.Length + e.Length + f.Length + g.Length + h.Length;
    }

    private static bool ContainsIndex<T>(this Span<T> span, int index) => index >= 0 && index < span.Length;

    private static Span<T> RequireIndex<T>(this Span<T> span, int index) {
        if (span.ContainsIndex(index) == false) {
            throw new IndexOutOfRangeException($"Index {index} is out-of-bounds for a {nameof(Span<T>)}.{nameof(Span<T>.Length)} of {span.Length}!");
        }

        return span;
    }

    public static string Concat(ReadOnlySpan<char> a, ReadOnlySpan<char> b, ReadOnlySpan<char> c = default, ReadOnlySpan<char> d = default, ReadOnlySpan<char> e = default, ReadOnlySpan<char> f = default) {
        Span<char> span = stackalloc char[Length(a, b, c, d, e, f)];
        span = span.Fill(a, b, c, d, e, f);
        return new string(span);
    }

    /// <summary>
    /// Combines a bunch of <see cref="ReadOnlySpan{T}"/>s into one pre-allocated <see cref="Span{T}"/>.
    /// </summary>
    /// <param name="destination">the <see cref="Span{T}"/> that will contain all of the stuff</param>
    /// <param name="a">stuff</param>
    /// <param name="b">stuff</param>
    /// <param name="c">stuff</param>
    /// <param name="d">stuff</param>
    /// <param name="e">stuff</param>
    /// <param name="f">stuff</param>
    /// <param name="requireExactSize">if <c>true</c>, then the <paramref name="destination"/> <b>must</b> be the exact same size as all of the stuff</param>
    /// <typeparam name="T">the type of the stuff</typeparam>
    /// <returns>the <paramref name="destination"/> <see cref="Span{T}"/>, for method chaining</returns>
    /// <exception cref="ArgumentException">if <paramref name="requireExactSize"/> == <c>true</c> and the <paramref name="destination"/> isn't <b>exactly</b> the right size</exception>
    public static Span<T> Fill<T>(
        this Span<T> destination,
        ReadOnlySpan<T> a,
        ReadOnlySpan<T> b,
        ReadOnlySpan<T> c = default,
        ReadOnlySpan<T> d = default,
        ReadOnlySpan<T> e = default,
        ReadOnlySpan<T> f = default,
        bool requireExactSize = false
    ) {
        if (requireExactSize) {
            var totalLength = Length(a, b, c, d, e, f);

            if (destination.Length != totalLength) {
                throw new ArgumentException($"The destination Span<{typeof(T).Name}>.Length {destination.Length} doesn't match the total number of entries, {totalLength}!");
            }
        }

        var pos = 0;
        return destination.Write(a, ref pos)
                          .Write(b, ref pos)
                          .Write(c, ref pos)
                          .Write(d, ref pos)
                          .Write(e, ref pos)
                          .Write(f, ref pos);
    }
}
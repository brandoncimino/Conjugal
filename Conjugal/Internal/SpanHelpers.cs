using System;

namespace FowlFever.Conjugal.Internal;

internal static class SpanHelpers {
    private static Span<T> RequireIndex<T>(this Span<T> span, int index) {
        if ((index >= 0 && index < span.Length) == false) {
            throw new IndexOutOfRangeException($"Index {index} is out-of-bounds for a {nameof(Span<T>)}.{nameof(Span<T>.Length)} of {span.Length}!");
        }

        return span;
    }

    #region Write

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
        if (toAppend.IsEmpty) {
            return span;
        }

        // if (span.IsEmpty) {
        //     throw new ArgumentException($"Destination Span<{typeof(T).Name}> is empty, so we can't write to it!");
        // }

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

    /// <summary>
    /// Equivalent to <see cref="Write{T}"/> but with an optional <paramref name="joiner"/> applied if <paramref name="position"/> &gt; 0 and <see cref="source"/> is not <see cref="ReadOnlySpan{T}.Empty"/>.
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="source"></param>
    /// <param name="joiner"></param>
    /// <param name="position"></param>
    /// <param name="shouldValidatePosition"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Span<T> WriteJoin<T>(this Span<T> destination, ReadOnlySpan<T> source, ReadOnlySpan<T> joiner, ref int position, bool shouldValidatePosition = false) {
        if (source.IsEmpty) {
            return destination;
        }

        if (position > 0) {
            destination.Write(joiner, ref position, shouldValidatePosition);
        }

        return destination.Write(source, ref position, shouldValidatePosition);
    }

    #endregion

    #region Length

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

    public static int LengthWithJoiner<T>(
        ReadOnlySpan<T> joiner,
        ReadOnlySpan<T> a,
        ReadOnlySpan<T> b,
        ReadOnlySpan<T> c = default,
        ReadOnlySpan<T> d = default,
        ReadOnlySpan<T> e = default,
        ReadOnlySpan<T> f = default,
        ReadOnlySpan<T> g = default,
        ReadOnlySpan<T> h = default
    ) {
        static void AddLength(ref int soFar, int next, int joiner) {
            if (next == 0) {
                return;
            }

            if (soFar == 0) {
                soFar = next;
                return;
            }

            soFar += next + joiner;
        }

        if (joiner.Length == 0) {
            return Length(a, b, c, d, e, f, g, h);
        }

        var len = 0;

        AddLength(ref len, a.Length, joiner.Length);
        AddLength(ref len, b.Length, joiner.Length);
        AddLength(ref len, c.Length, joiner.Length);
        AddLength(ref len, d.Length, joiner.Length);
        AddLength(ref len, e.Length, joiner.Length);
        AddLength(ref len, f.Length, joiner.Length);
        AddLength(ref len, g.Length, joiner.Length);
        AddLength(ref len, h.Length, joiner.Length);

        return len;
    }

    #endregion

    #region String creation

    public static string Concat(
        ReadOnlySpan<char> a,
        ReadOnlySpan<char> b,
        ReadOnlySpan<char> c = default,
        ReadOnlySpan<char> d = default,
        ReadOnlySpan<char> e = default,
        ReadOnlySpan<char> f = default,
        ReadOnlySpan<char> g = default,
        ReadOnlySpan<char> h = default
    ) {
        var        length = Length(a, b, c, d, e, f, g, h);
        Span<char> span   = stackalloc char[length];
        span = span.Fill(a, b, c, d, e, f, g, h);
        return new string(span);
    }

    public static string Join(
        ReadOnlySpan<char> joiner,
        ReadOnlySpan<char> a,
        ReadOnlySpan<char> b,
        ReadOnlySpan<char> c = default,
        ReadOnlySpan<char> d = default,
        ReadOnlySpan<char> e = default,
        ReadOnlySpan<char> f = default,
        ReadOnlySpan<char> g = default,
        ReadOnlySpan<char> h = default
    ) {
        if (joiner.IsEmpty) {
            return Concat(a, b, c, d, e, f, g, h);
        }

        var        length = LengthWithJoiner(joiner, a, b, c, d, e, f, g, h);
        Span<char> span   = stackalloc char[length];
        span = span.FillJoin(joiner, a, b, c, d, e, f, g, h);
        return new string(span);
    }

    #endregion

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
    /// <param name="g">stuff</param>
    /// <param name="h">stuff</param>
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
        ReadOnlySpan<T> g = default,
        ReadOnlySpan<T> h = default,
        bool requireExactSize = false
    ) {
        if (requireExactSize) {
            var totalLength = Length(a, b, c, d, e, f, g, h);

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
                          .Write(f, ref pos)
                          .Write(g, ref pos)
                          .Write(h, ref pos);
    }

    /// <inheritdoc cref="Fill{T}"/>
    public static Span<T> FillJoin<T>(
        this Span<T> destination,
        ReadOnlySpan<T> joiner,
        ReadOnlySpan<T> a,
        ReadOnlySpan<T> b,
        ReadOnlySpan<T> c = default,
        ReadOnlySpan<T> d = default,
        ReadOnlySpan<T> e = default,
        ReadOnlySpan<T> f = default,
        ReadOnlySpan<T> g = default,
        ReadOnlySpan<T> h = default,
        bool requireExactSize = false
    ) {
        if (joiner.IsEmpty) {
            return destination.Fill(a, b, c, d, e, f, g, h, requireExactSize);
        }

        if (requireExactSize) {
            var totalLength = LengthWithJoiner(joiner, a, b, c, d, e, f, g, h);
            if (destination.Length != totalLength) {
                throw new ArgumentException($"The destination Span<{typeof(T).Name}>.Length {destination.Length} doesn't match the total number of entries, {totalLength}!");
            }
        }

        var pos = 0;
        return destination.WriteJoin(a, joiner, ref pos)
                          .WriteJoin(b, joiner, ref pos)
                          .WriteJoin(c, joiner, ref pos)
                          .WriteJoin(d, joiner, ref pos)
                          .WriteJoin(e, joiner, ref pos)
                          .WriteJoin(f, joiner, ref pos)
                          .WriteJoin(g, joiner, ref pos)
                          .WriteJoin(h, joiner, ref pos);
    }
}
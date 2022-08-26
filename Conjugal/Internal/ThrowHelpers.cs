using System;
using System.Runtime.CompilerServices;

namespace FowlFever.Conjugal.Internal;

internal static class ThrowHelpers {
    public static NotImplementedException NotImplementedException<T>(this T value, [CallerMemberName] string? caller = default) {
        return new NotImplementedException($"{typeof(T).Name}.{value} is not implemented by {caller}!");
    }

    public static NotSupportedException NotSupportedException<T>(this T value, [CallerMemberName] string? caller = default) {
        return new NotSupportedException($"{typeof(T).Name}.{value} is not supported by {caller}!");
    }
}
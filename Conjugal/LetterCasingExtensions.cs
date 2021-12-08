using Humanizer;

namespace FowlFever.Conjugal {
    /// <inheritdoc cref="LetterCasing"/>
    public static class LetterCasingExtensions {
        /// <summary>
        /// Equivalent to <see cref="CasingExtensions.ApplyCase"/> with the parameter order swapped.
        /// </summary>
        /// <param name="casing">this <see cref="LetterCasing"/></param>
        /// <param name="str">the <see cref="string"/> to which we will <see cref="CasingExtensions.ApplyCase"/></param>
        /// <returns>a new <see cref="string"/> in this <see cref="LetterCasing"/></returns>
        public static string ApplyTo(this LetterCasing casing, string str) {
            return str.ApplyCase(casing);
        }
    }
}
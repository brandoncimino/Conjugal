using System;

using JetBrains.Annotations;

namespace FowlFever.Conjugal.Affixing {
    /// <summary>
    /// Extension methods for <see cref="IAffix"/>.
    /// </summary>
    [PublicAPI]
    public static class AffixExtensions {
        /// <param name="affix">this <see cref="IAffix"/></param>
        /// <param name="stem">the word that will be affixed</param>
        /// <returns>an <see cref="Affixation"/> combining this <see cref="IAffix"/> and a <paramref name="stem"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Affixation AffixTo(this IAffix affix, [CanBeNull] string stem) {
            if (affix == null) {
                throw new ArgumentNullException(nameof(affix));
            }

            return new Affixation(affix, stem);
        }
    }
}
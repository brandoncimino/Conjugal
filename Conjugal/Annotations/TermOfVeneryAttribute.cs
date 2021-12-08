using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations {
    /// <inheritdoc/>
    /// <summary>
    /// The onanistic alias of <see cref="T:FowlFever.Conjugal.Annotations.CollectiveNounAttribute"/>.
    /// </summary>
    /// <seealso cref="T:FowlFever.Conjugal.Annotations.CollectiveNounAttribute"/>
    [PublicAPI]
    public class TermOfVeneryAttribute : CollectiveNounAttribute {
        /// <inheritdoc/>
        public TermOfVeneryAttribute(string singular, Countability countability) : base(singular, countability) { }

        /// <inheritdoc/>
        public TermOfVeneryAttribute(string singularAndPlural) : base(singularAndPlural) { }

        /// <inheritdoc/>
        public TermOfVeneryAttribute(string singular, string plural) : base(singular, plural) { }

        /// <inheritdoc/>
        public TermOfVeneryAttribute(string singular, string plural, Countability countability) : base(singular, plural, countability) { }
    }
}
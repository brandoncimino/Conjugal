using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations {
    /// <summary>
    /// The onanistic alias of <see cref="CollectiveNounAttribute"/>.
    /// </summary>
    /// <seealso cref="CollectiveNounAttribute"/>
    [PublicAPI]
    public class TermOfVeneryAttribute : CollectiveNounAttribute {
        public TermOfVeneryAttribute(string singular, Countability countability) : base(singular, countability) { }
        public TermOfVeneryAttribute(string singularAndPlural) : base(singularAndPlural) { }
        public TermOfVeneryAttribute(string singular, string plural) : base(singular, plural) { }
        public TermOfVeneryAttribute(string singular, string plural, Countability countability) : base(singular, plural, countability) { }
    }
}
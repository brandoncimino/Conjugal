using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations {
    /// <summary>
    /// The onanistic alias of <see cref="CollectiveNounAttribute"/>.
    /// </summary>
    /// <seealso cref="CollectiveNounAttribute"/>
    [PublicAPI]
    public class TermOfVeneryAttribute : CollectiveNounAttribute {
        public TermOfVeneryAttribute([NotNull] string singular, Countability countability) : base(singular, countability) { }
        public TermOfVeneryAttribute([NotNull] string singularAndPlural) : base(singularAndPlural) { }
        public TermOfVeneryAttribute([NotNull] string singular, [NotNull] string plural) : base(singular, plural) { }
        public TermOfVeneryAttribute([NotNull] string singular, [NotNull] string plural, Countability countability) : base(singular, plural, countability) { }
    }
}
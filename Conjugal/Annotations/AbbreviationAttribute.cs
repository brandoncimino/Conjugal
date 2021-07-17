using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations {
    /// <summary>
    /// Defines an abbreviated form of a <see cref="System.Type"/>'s <see cref="LemmaAttribute"/>.
    /// </summary>
    [PublicAPI]
    public class AbbreviationAttribute : PlurableWrapperAttribute {
        public AbbreviationAttribute([NotNull] string singular, Countability countability) : base(singular, countability) { }
        public AbbreviationAttribute([NotNull] string singularAndPlural) : base(singularAndPlural) { }
        public AbbreviationAttribute([NotNull] string singular, [NotNull] string plural) : base(singular, plural) { }
        public AbbreviationAttribute([NotNull] string singular, [NotNull] string plural, Countability countability) : base(singular, plural, countability) { }
    }
}
using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations {
    /// <summary>
    /// Defines an abbreviated form of a <see cref="System.Type"/>'s <see cref="LemmaAttribute"/>.
    /// </summary>
    [PublicAPI]
    public class AbbreviationAttribute : PlurableWrapperAttribute {
        public AbbreviationAttribute(string singular, Countability countability) : base(singular, countability) { }
        public AbbreviationAttribute(string singularAndPlural) : base(singularAndPlural) { }
        public AbbreviationAttribute(string singular, string plural) : base(singular, plural) { }
        public AbbreviationAttribute(string singular, string plural, Countability countability) : base(singular, plural, countability) { }
    }
}
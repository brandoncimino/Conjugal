using Humanizer;

using JetBrains.Annotations;

namespace Conjugal.Annotations {
    [PublicAPI]
    public class CasingAttribute : ConjugalAttribute {
        public readonly LetterCasing Casing;

        public CasingAttribute(LetterCasing letterCasing) {
            Casing = letterCasing;
        }
    }
}
using Humanizer;

using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations {
    [PublicAPI]
    public class PreferredCasingAttribute : ConjugalAttribute {
        public readonly LetterCasing Casing;

        public PreferredCasingAttribute(LetterCasing letterCasing) {
            Casing = letterCasing;
        }
    }
}
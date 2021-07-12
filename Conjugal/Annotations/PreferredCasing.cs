using Humanizer;

using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations {
    [PublicAPI]
    public class PreferredCasing : ConjugalAttribute {
        public readonly LetterCasing Casing;

        public PreferredCasing(LetterCasing letterCasing) {
            Casing = letterCasing;
        }
    }
}
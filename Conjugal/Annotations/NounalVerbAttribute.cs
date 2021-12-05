using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations {
    [PublicAPI]
    public class NounalVerbAttribute : ConjugalAttribute {
        public readonly string Verb;

        public NounalVerbAttribute(string verb) {
            Verb = verb;
        }
    }
}
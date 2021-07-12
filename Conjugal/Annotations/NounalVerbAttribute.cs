using JetBrains.Annotations;

namespace Conjugal.Annotations {
    [PublicAPI]
    public class NounalVerbAttribute : ConjugalAttribute {
        [NotNull] public readonly string Verb;

        public NounalVerbAttribute([NotNull] string verb) {
            Verb = verb;
        }
    }
}
using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations;

[PublicAPI("Experimental")]
public class NounalVerbAttribute : ConjugalAttribute {
    public readonly string Verb;

    public NounalVerbAttribute(string verb) {
        Verb = verb;
    }
}
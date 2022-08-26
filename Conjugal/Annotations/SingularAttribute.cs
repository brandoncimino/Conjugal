using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations;

/// <summary>
/// Overrides this class's <see cref="IConjugal.Singular"/> form.
/// </summary>
/// <remarks>
/// This attribute should only be used for <a href="https://en.wiktionary.org/wiki/Category:English_pluralia_tantum">pluralia tantum</a>,
/// i.e., words whose <a href="https://en.wikipedia.org/wiki/Lemma_(morphology)">lemma</a> is <b>not singular</b>
/// (see: <a href="https://en.wiktionary.org/wiki/pants">pants</a>, <a href="https://en.wiktionary.org/wiki/bristols">bristols</a>).
/// Otherwise, you should use the <see cref="LemmaAttribute"/>.
/// </remarks>
/// <example>
/// The word <a href="https://en.wiktionary.org/wiki/pants">pants</a> does not have a singular form.
/// Therefore, the <see cref="LemmaAttribute"/> should be set to <c>"pants"</c>, while the <see cref="SingularAttribute"/>
/// should be set to <c>"pair of pants"</c>.
/// </example>
/// <seealso cref="IConjugal.Singular"/>
/// <seealso cref="IConjugal.Lemma"/>
[PublicAPI]
public class SingularAttribute : ConjugalAttribute {
    /// <summary>
    /// The <see cref="IPlurable.Singular"/> form of this class.
    /// </summary>
    public readonly string Singular;

    /// <inheritdoc cref="SingularAttribute"/>
    public SingularAttribute(string singular) {
        Singular = singular;
    }
}
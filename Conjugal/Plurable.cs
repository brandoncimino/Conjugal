using FowlFever.Conjugal.Affixing;

using Humanizer;

using JetBrains.Annotations;

namespace FowlFever.Conjugal;

/// <summary>
/// A simple struct implementing <see cref="IPlurable"/>.
/// </summary>
/// <remarks>📎 An alternative name could be "IPluric"</remarks>
[PublicAPI]
public readonly struct Plurable : IPlurable {
    /// <inheritdoc />
    public string Singular { get; }
    /// <inheritdoc />
    public string Plural { get; }
    /// <inheritdoc />
    public Countability Countability { get; }

    /// <summary>
    /// Constructs a new <see cref="Plurable"/> with the given values.
    /// </summary>
    /// <param name="singular">the <see cref="Singular"/> form</param>
    /// <param name="plural">the <see cref="Plural"/> form</param>
    /// <param name="countability">the <see cref="Countability"/>. If omitted, then we <see cref="InferCountability"/> instead</param>
    public Plurable(
        string singular,
        string plural,
        Countability? countability = default
    ) {
        Singular     = singular;
        Plural       = plural;
        Countability = countability ?? InferCountability(Singular, Plural);
    }

    /// <summary>
    /// Constructs a new <see cref="Plurable"/>, inferring the <see cref="Plural"/> form via <see cref="ConjugalStringExtensions.PluralFromCountability"/>.
    /// </summary>
    /// <param name="singular">the <see cref="Singular"/> form</param>
    /// <param name="countability">the <see cref="Countability"/> to use with <see cref="ConjugalStringExtensions.PluralFromCountability"/></param>
    public Plurable(
        string singular,
        Countability countability
    ) {
        Singular     = singular;
        Countability = countability;
        Plural       = singular.PluralFromCountability(Countability);
    }

    /// <summary>
    /// Infers a <see cref="Conjugal.Countability"/> value based on the given <see cref="Singular"/> and <see cref="Plural"/> forms.
    /// </summary>
    /// <remarks>Intended as a fallback for when a <see cref="Plurable"/> is constructed without an explicit <see cref="Countability"/></remarks>
    /// <param name="singular">the <see cref="Singular"/> form</param>
    /// <param name="plural">the <see cref="Plural"/> form</param>
    /// <returns>an inferred <see cref="Conjugal.Countability"/></returns>
    public static Countability InferCountability(string singular, string plural) {
        return singular == plural ? Countability.Uncountable : Countability.Countable;
    }

    /// <inheritdoc />
    public override string ToString() {
        return $"({Singular}, {Plural})";
    }

    /// <summary>
    /// Implicitly casts a single <see cref="string"/> into an <see cref="Conjugal.Countability.Uncountable"/> <see cref="Plurable"/>.
    /// </summary>
    /// <param name="word">a single <see cref="string"/></param>
    /// <returns>an <see cref="Conjugal.Countability.Uncountable"/> <see cref="Plurable"/></returns>
    public static implicit operator Plurable(string word) {
        return Uncountable(word);
    }

    /// <summary>
    /// A <see cref="AffixFlavor.Duplifix">fancy-schmancy</see> way to construct a <see cref="Plurable"/> from a tuple
    /// </summary>
    /// <example>
    /// This allows users to pass values as either <see cref="Conjugal.Countability.Uncountable"/> strings or <see cref="Plurable"/> instances without
    /// interrupting the code with constructors or <see cref="Uncountable"/>.
    /// <br/>
    /// <code>
    /// new UnitOfMeasure("quid");                // 0 quid     1 quid      2 quid
    /// new UnitOfMeasure(("fathom", "fathoms")); // 0 fathoms  1 fathom    60_000 fathoms
    /// Brother.Abbreviation = ("bro", "bros.");  // 0 bros.    1 bro       4 bros.
    /// </code>
    /// </example>
    /// <param name="singularAndPlural">a tuple containing both the <see cref="Singular"/> and <see cref="Plural"/> words, <b>in that order</b></param>
    /// <returns></returns>
    public static implicit operator Plurable((string, string) singularAndPlural) {
        var (singular, plural) = singularAndPlural;
        return Of(singular, plural);
    }

    /// <summary>
    /// Constructs a new <see cref="Conjugal.Countability.Uncountable"/> <see cref="Plurable"/> (i.e. the <see cref="Singular"/> and <see cref="Plural"/> forms are the same).
    /// </summary>
    /// <param name="singularAndPlural">the <see cref="string"/> used as <b>both</b> the <see cref="Singular"/> and <see cref="Plural"/> forms</param>
    /// <returns>a new <see cref="Plurable"/> instance</returns>
    public static Plurable Uncountable(string singularAndPlural) {
        return new Plurable(singularAndPlural, singularAndPlural, Countability.Uncountable);
    }

    /// <summary>
    /// Constructs a <a href="https://en.wikipedia.org/wiki/Syntactic_sugar">sugary</a> new <see cref="Plurable"/>.
    /// </summary>
    /// <param name="singular">the <see cref="Singular"/> form</param>
    /// <param name="plural">the <see cref="Plural"/> form</param>
    /// <param name="countability">an explicit <see cref="Conjugal.Countability"/>.
    /// <br/>If omitted, we <see cref="InferCountability"/> instead.</param>
    /// <returns>a new <see cref="Plurable"/> instance</returns>
    public static Plurable Of(
        string singular,
        string plural,
        Countability? countability = default
    ) {
        return new Plurable(singular, plural, countability);
    }

    /// <summary>
    /// Constructs a new <see cref="Plurable"/>, where the <see cref="Plural"/> form is inferred via <see cref="ConjugalStringExtensions.PluralFromCountability"/>.
    /// </summary>
    /// <param name="singular">the <see cref="Singular"/> form</param>
    /// <param name="countability">a <see cref="Countability"/></param>
    /// <returns>a new <see cref="Plurable"/> instance</returns>
    public static Plurable Of(
        string singular,
        Countability countability
    ) {
        return new Plurable(singular, countability);
    }

    /// <summary>
    /// Returns a <see cref="Plurable"/> using a given <paramref name="singular"/> and <see cref="Humanizer"/>'s <see cref="InflectorExtensions.Pluralize"/>.
    /// </summary>
    /// <param name="singular">the original word being pluralized (which is usually singular)</param>
    /// <param name="isKnownToBeSingular">passed as the <paramref name="isKnownToBeSingular"/> parameter in <see cref="InflectorExtensions.Pluralize"/></param>
    /// <returns>a new <see cref="Plurable"/></returns>
    public static Plurable Humanized(string singular, bool isKnownToBeSingular = true) {
        return new Plurable(singular, singular.Pluralize(isKnownToBeSingular));
    }
}
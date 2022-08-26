using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations;

/// <summary>
/// A base class for <see cref="ConjugalAttribute"/>s that wrap a single <see cref="Plurable"/> <see cref="Value"/>.
/// Provides constructors that mimic <see cref="Plurable"/>'s.
/// </summary>
/// <seealso cref="AbbreviationAttribute"/>
/// <seealso cref="TermOfVeneryAttribute"/>
[PublicAPI]
public abstract class PlurableWrapperAttribute : ConjugalAttribute {
    /// <summary>
    /// The <see cref="Plurable"/> that this <see cref="PlurableWrapperAttribute"/> contains.
    /// </summary>
    public Plurable Value { get; }

    private PlurableWrapperAttribute(Plurable plurable) {
        Value = plurable;
    }

    /// <summary>
    /// Constructs a new <see cref="PlurableWrapperAttribute"/> using:
    /// <ul>
    /// <li>An explicit <see cref="IPlurable.Singular"/> form</li>
    /// <li>A <see cref="IPlurable.Plural"/> derived from a <see cref="Countability"/></li>
    /// </ul>
    /// </summary>
    /// <param name="singular">the <see cref="IPlurable.Singular"/> form</param>
    /// <param name="countability">a <see cref="Countability"/>, from which the <see cref="IPlurable.Plural"/> will be derived</param>
    /// <seealso cref="Plurable.Of(string,FowlFever.Conjugal.Countability)"/>
    protected PlurableWrapperAttribute(string singular, Countability countability) : this(
        Plurable.Of(singular, countability)
    ) { }

    /// <summary>
    /// Constructs a new <see cref="PlurableWrapperAttribute"/> wrapping an <see cref="Plurable.Uncountable"/> <see cref="IPlurable"/>.
    /// </summary>
    /// <param name="singularAndPlural">the <see cref="string"/> used as <b>both</b> the <see cref="IPlurable.Singular"/> and <see cref="IPlurable.Plural"/> forms</param>
    protected PlurableWrapperAttribute(string singularAndPlural) :
        this(Plurable.Uncountable(singularAndPlural)) { }

    /// <summary>
    /// Constructs a new <see cref="PlurableWrapperAttribute"/> wrapping an explicitly defined <see cref="IPlurable.Singular"/> and <see cref="IPlurable.Plural"/>.
    /// </summary>
    /// <param name="singular">the <see cref="IPlurable.Singular"/></param>
    /// <param name="plural">the <see cref="IPlurable.Plural"/></param>
    protected PlurableWrapperAttribute(string singular, string plural) :
        this(Plurable.Of(singular, plural)) { }

    /// <summary>
    /// Constructs a <a href="https://en.wikipedia.org/wiki/Syntactic_sugar">sugary</a> new <see cref="PlurableWrapperAttribute"/>.
    /// </summary>
    /// <param name="singular">the <see cref="IPlurable.Singular"/> form</param>
    /// <param name="plural">the <see cref="IPlurable.Plural"/> form</param>
    /// <param name="countability">an explicit <see cref="Conjugal.Countability"/>.
    /// <br/>If omitted, we <see cref="Plurable.InferCountability"/> instead.</param>
    protected PlurableWrapperAttribute(string singular, string plural, Countability countability) {
        Value = Plurable.Of(singular, plural, countability);
    }
}
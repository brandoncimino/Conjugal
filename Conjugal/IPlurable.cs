using JetBrains.Annotations;

namespace FowlFever.Conjugal;

/// <summary>
/// Joins together a <see cref="Singular"/> and a <see cref="Plural"/> form explicitly.
/// </summary>
[PublicAPI]
public interface IPlurable {
    /// <summary>
    /// The <a href="https://en.wikipedia.org/wiki/Grammatical_number">grammatical number</a> for exactly one entity.
    /// </summary>
    public string Singular { get; }

    /// <summary>
    /// The <a href="https://en.wikipedia.org/wiki/Grammatical_number">grammatical number</a> for multiple entities.
    /// </summary>
    /// <remarks><a href="https://en.wikipedia.org/wiki/Plural">Wikipedia - Plural</a></remarks>
    public string Plural { get; }

    /// <summary>
    /// Describes how this <see cref="IPlurable"/>'s <see cref="Plural"/> is derived from its <see cref="Singular"/>, i.e.,
    /// how it is affected by <a href="https://en.wikipedia.org/wiki/Grammatical_number">grammatical number</a>.
    /// </summary>
    public Countability Countability { get; }
}
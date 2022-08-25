namespace FowlFever.Conjugal.Nymy;

/// <summary>
/// Represents <a href="https://en.wikipedia.org/wiki/Meronymy_and_holonymy">meronymy and holonymy</a>, aka "part" and "whole".
/// </summary>
/// <remarks>
/// Not to be confused with <a href="https://en.wikipedia.org/wiki/Mereology">mereology</a> to be confused with <a href="https://en.wikipedia.org/wiki/Mariology">Mariology</a>, the study of <a href="https://en.wikipedia.org/wiki/Mario">Mario</a>. 
/// </remarks>
public enum Meronymy {
    /// <summary>
    /// Represents a <b>part</b> of something else.
    /// </summary>
    Meronym,
    /// <summary>
    /// Represents a something that has parts.
    /// <p/>
    /// TODO: Does "🧙 is a <see cref="Holonym"/> of 🔮" imply that 🧙 is composed <b>exclusively</b> of 🔮s?
    /// TODO: Does "🧙‍ is a <see cref="Holonym"/> of 🔮" imply that 🧙 is a "complete whole"?
    /// </summary>
    Holonym
}
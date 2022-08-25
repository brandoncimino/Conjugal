namespace FowlFever.Conjugal.Nymy;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Ontology">ontologic</a> relationship of <a href="https://en.wikipedia.org/wiki/Hyponymy_and_hypernymy">hyponymy and hypernymy</a>.
/// </summary>
/// <remarks>
/// According to <a href="https://en.wikipedia.org/wiki/Hyponymy_and_hypernymy#Etymology">Wikipedia</a>, "hyponymy" is the "neutral term" for both <a href="https://en.wikipedia.org/wiki/Hyponymy_and_hypernymy">hyponymy and hypernymy</a>. 
/// </remarks>
public enum Hyponymy {
    /// <summary>
    /// Something that is <b>more specific</b> than something else.
    /// </summary>
    /// <example>
    /// <ul>
    /// <li>"square" is a <see cref="Hyponym"/> of "rectangle".</li>
    /// <li>"red", "blue", and "green" are <see cref="Hyponym"/>s of "color".</li>
    /// </ul>
    /// </example>
    Hyponym,
    /// <summary>
    /// Something that is <b>less specific</b> than something else.
    /// </summary>
    /// <example>
    /// <ul>
    /// <li>"rectangle" is a <see cref="Hypernym"/> of "square".</li>
    /// <li>"color", "primary color", and "<a href="https://en.wikipedia.org/wiki/Primary_color#Additive_mixing_of_light">additive color</a>" are <see cref="Hypernym"/>s of "red", "green", and "blue".</li>
    /// </ul>
    /// </example>
    Hypernym
}
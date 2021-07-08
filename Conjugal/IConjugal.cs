namespace Conjugal {
    /// <summary>
    /// Indicates that this object can be <a href="https://en.wikipedia.org/wiki/Inflection">inflected</a>.
    /// </summary>
    /// <remarks>
    /// I realize that <a href="https://en.wikipedia.org/wiki/Inflection">inflection</a> is is a superset of <a href="https://en.wikipedia.org/wiki/Grammatical_conjugation">conjugation</a>
    /// and <a href="https://en.wikipedia.org/wiki/Declension">declension</a>, and therefore a more traditionally accurate name would be
    /// <a href="https://en.wiktionary.org/wiki/inflective#Adjective"><c>IInflective</c></a>. I also realize that <a href="https://en.wiktionary.org/wiki/conjugatable">"conjugatable"</a> is the traditionally correct term, rather than <a href="https://en.wiktionary.org/wiki/conjugal#Adjective">"conjugal"</a>.
    /// I disagree.
    ///
    /// Reasons include:
    /// <ul>
    /// <li><c>IInflective</c> would start with two <c>I</c>s.</li>
    /// <li>"Conjugal" sounds much better than "conjugatable".</li>
    /// <li>"Conjugatable" isn't long enough to be fun to say.</li>
    /// <li><a href="https://en.wikipedia.org/wiki/Conjugation">"Conjugation"</a> is a superset of both <a href="https://en.wikipedia.org/wiki/Inflection">inflection</a> <i>and</i> <a href="https://en.wikipedia.org/wiki/Grammatical_conjugation">grammatical conjugation</a>. (This makes conjugation a "set that contains itself," but for some reason I can't find the correct term for that...)</li>
    /// <li><a href="https://en.wikipedia.org/wiki/Inflection">Inflection</a> is <a href="https://en.wikipedia.org/wiki/Jargon">jargon</a>; <a href="https://en.wikipedia.org/wiki/Conjugation">conjugation</a> is a <a href="https://en.wikipedia.org/wiki/Concept">concept</a>.</li>
    /// <li>"Conjugal" is a <a href="https://en.wiktionary.org/wiki/double_entendre#Noun">double entendre</a>.</li>
    /// </ul>
    /// </remarks>
    public interface IConjugal {
        public string Singular     { get; }
        public string Plural       { get; }
        public string Lemma        { get; }
        public bool   IsCountable  { get; }
        public bool   IsProperNoun { get; }
    }
}
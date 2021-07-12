namespace FowlFever.Conjugal {
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
        /// The canonical form of this entity.
        /// </summary>
        /// <remarks>
        /// Usually singular, but not always.
        /// </remarks>
        public string Lemma { get; }
        /// <summary>
        /// Whether this entity is a <a href="https://en.wikipedia.org/wiki/Count_noun">countable noun</a>.
        /// </summary>
        public bool IsCountable { get; }
        /// <summary>
        /// Whether this entity is important enough to be capitalized; usually because it is a specific instance of a <a href="https://en.wikipedia.org/wiki/Class_(philosophy)">class</a>.
        /// </summary>
        /// <remarks>
        /// <a href="https://en.wikipedia.org/wiki/Proper_and_common_nouns">Wikipedia - Proper and common nouns</a>
        /// </remarks>
        public bool IsProperNoun { get; }
        /// <summary>
        /// A noun used as a verb.
        /// </summary>
        /// <example>
        /// <ul>
        /// <b><a href="https://en.wiktionary.org/wiki/polymorphism">Polymorphic</a></b>
        /// <li>
        /// <a href="https://en.wiktionary.org/wiki/friend#Noun">friend (noun)</a> <i>(a being treated with affection)</i>
        /// </li>
        /// <li>
        /// <a href="https://en.wiktionary.org/wiki/friend#Verb">friend (verb)</a> <i>(to consider something as a <a href="https://en.wiktionary.org/wiki/friend#Noun">friend (noun)</a>)</i>
        /// </li>
        /// </ul>
        /// <ul>
        /// <b><a href="https://en.wiktionary.org/wiki/locative">Locative</a></b>
        /// <li>
        /// <a href="https://en.wiktionary.org/wiki/bed#Noun">bed (noun)</a> <i>(a place for sleeping)</i>
        /// </li>
        /// <li>
        /// <a href="https://en.wiktionary.org/wiki/bed#Verb">bed (verb)</a> <i>(to <a href="https://en.wiktionary.org/wiki/canoodle">canoodle</a> in a <a href="https://en.wiktionary.org/wiki/bed#Noun">bed (noun)</a>)</i>
        /// </li>
        /// </ul>
        /// <ul>
        /// <b><a href="https://en.wiktionary.org/wiki/instrumental#English">Instrumental</a></b>
        /// <li>
        /// <a href="https://en.wiktionary.org/wiki/peg#Noun">peg (noun)></a> <i>(a cylindrical object used to fasten or as a bearing between objects)</i>
        /// </li>
        /// <li>
        /// <a href="https://en.wiktionary.org/wiki/peg#Verb">peg (verb)</a> <i>(to fasten with a <a href="https://en.wiktionary.org/wiki/dildo">peg (noun)</a>)</i>
        /// </li>
        /// </ul>
        /// </example>
        public string NounalVerb { get; }
    }
}
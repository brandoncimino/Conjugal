using System;
using System.ComponentModel;

using JetBrains.Annotations;

namespace FowlFever.Conjugal.Affixing {
    /// <summary>
    /// <a href="https://en.wiktionary.org/wiki/affix#English">affix</a> + <a href="https://en.wiktionary.org/wiki/-ation#English">-ation</a>
    /// <p/>
    /// Represents the act of adding an <a href="https://en.wikipedia.org/wiki/Affix">affix</a> to a word.
    /// </summary>
    /// <seealso cref="IAffixed"/>
    /// <see cref="Affixing.Affix"/>
    [PublicAPI]
    public readonly struct Affixation : IAffixed {
        public string Stem          { get; }
        public string BoundMorpheme { get; }
        public string Joiner        { get; }
        public Affix  Affix         { get; }
        /// <summary>
        /// An extra, optional <see cref="int"/> used by some <see cref="Affixing.Affix"/> types:
        /// <ul>
        /// <li><see cref="Affixing.Affix.Circumfix"/>: Split a single <see cref="BoundMorpheme"/> into multiple parts.</li>
        /// <li><see cref="Affixing.Affix.Infix"/>: Determine where the <see cref="BoundMorpheme"/> should be <see cref="string.Insert"/>ed into the <see cref="Stem"/>.</li>
        /// </ul>
        /// <b>NOTE:</b>
        /// <br/>
        /// For other <see cref="Affixing.Affix"/> types, <see cref="Index"/> <b>must not be set</b>.
        /// </summary>
        internal int? Index { get; }

        /// <summary>
        /// Constructs a new <see cref="Affixation"/> with the given parameters.
        /// </summary>
        /// <remarks>Please construct <see cref="Affixation"/> instances via the static factory methods: <see cref="Suffixation(string,string,string)"/>, <see cref="Ambifixation(string,string,string)"/>, etc.</remarks>
        /// <exception cref="Index">If an <see cref="ArgumentException"/> is needed but missing <b>or</b> missing but needed.</exception>
        internal Affixation(Affix affix, string stem, string boundMorpheme, int? index, string joiner = "") {
            if (MustBeIndexed(affix) != index.HasValue) {
                var problem = index.HasValue ? $"must not have an {nameof(index)} specified" : $"requires you to specify an {nameof(index)}";
                throw new ArgumentException($"{nameof(global::FowlFever.Conjugal.Affixing.Affix)} {nameof(affix)} {affix} {problem} when constructing an {nameof(Affixation)}!");
            }

            Stem          = stem;
            BoundMorpheme = boundMorpheme;
            Joiner        = joiner;
            Affix         = affix;
            Index         = index;
        }

        internal Affixation(
            Affix affix,
            string stem,
            string boundMorpheme,
            string joiner = ""
        ) : this(
            affix,
            stem,
            boundMorpheme,
            default,
            joiner
        ) { }

        /// <summary>
        /// Constructs a new <see cref="Affixation"/> instance with a <see cref="Affixing.Affix.Suffix"/>.
        /// </summary>
        /// <param name="stem">the linguistic <see cref="Stem"/></param>
        /// <param name="suffix">the <see cref="BoundMorpheme"/> appended to the <see cref="Stem"/></param>
        /// <param name="joiner">the <see cref="Joiner"/> interposed betwixt the <see cref="Stem"/> and <see cref="BoundMorpheme"/>. Defaults to <see cref="string.Empty">""</see>.</param>
        /// <returns>a new <see cref="Affixation"/></returns>
        public static Affixation Suffixation(string stem, string suffix, string joiner = "") {
            return new Affixation(Affix.Suffix, stem, suffix, joiner);
        }

        /// <inheritdoc cref="Suffixation(string,string,string)"/>
        public static Affixation Suffixation(string stem, string suffix, Joiner joiner = Affixing.Joiner.None) {
            return Suffixation(stem, suffix, joiner.String());
        }

        /// <summary>
        /// Constructs a new <see cref="Affixation"/> with a <see cref="Affixing.Affix.Prefix"/>.
        /// </summary>
        /// <param name="stem">the linguistic <see cref="Stem"/></param>
        /// <param name="prefix">the <see cref="BoundMorpheme"/> appended to the <see cref="Stem"/></param>
        /// <param name="joiner">the <see cref="Joiner"/> interposed betwixt the <see cref="Stem"/> and <see cref="BoundMorpheme"/></param>
        /// <returns>a new <see cref="Affixation"/></returns>
        public static Affixation Prefixation(string stem, string prefix, string joiner = "") {
            return new Affixation(Affix.Prefix, stem, prefix, joiner);
        }

        /// <inheritdoc cref="Prefixation(string,string,string)"/>
        public static Affixation Prefixation(string stem, string prefix, Joiner joiner = Affixing.Joiner.None) {
            return Prefixation(stem, prefix, joiner.String());
        }

        /// <summary>
        /// Constructs a new <see cref="Affixation"/> with an <see cref="Affixing.Affix.Infix"/>.
        /// </summary>
        /// <param name="stem">the linguistic <see cref="Stem"/> that the <see cref="BoundMorpheme"/> will be <see cref="string.Insert">inserted</see> into</param>
        /// <param name="infix">the <see cref="BoundMorpheme"/> that will be <see cref="string.Insert">inserted</see> into the <see cref="Stem"/></param>
        /// <param name="index">the <see cref="string"/> index where the <see cref="string.Insert"/>ion will take place</param>
        /// <param name="joiner">the <see cref="Joiner"/> interposed betwixt the <see cref="Stem"/> and <see cref="BoundMorpheme"/>. Defaults to <see cref="string.Empty">""</see>.</param>
        /// <returns>a new <see cref="Affixation"/></returns>
        public static Affixation Infixation(string stem, string infix, int index, string joiner) {
            return new Affixation(Affix.Infix, stem, infix, index, joiner);
        }

        /// <inheritdoc cref="Infixation(string,string,int,string)"/>
        public static Affixation Infixation(string stem, string infix, int index, Joiner joiner = Affixing.Joiner.None) {
            return Infixation(stem, infix, index, joiner.String());
        }

        /// <summary>
        /// Constructs a new <see cref="Affixation"/> with a <see cref="Affixing.Affix.Circumfix"/>.
        /// </summary>
        /// <remarks>
        /// Both <see cref="Circumfixation(string,string,string,string)"/> and <see cref="Ambifixation(string,string,string)"/> split the <see cref="BoundMorpheme"/> into two parts that bookend the <see cref="Stem"/>.
        /// <ul>If the parts of the <see cref="BoundMorpheme"/> are:
        /// <li><b>unique</b>, use <see cref="Circumfixation(string,string,string,string)">Circumfixation</see>: <a href="https://en.wiktionary.org/wiki/em-_-en"><i>em- -en</i></a> in <b>"embooben"</b> <i>(to endow)</i></li>
        /// <li><b>identical</b>, use <see cref="Ambifixation(string,string,string)">Ambifixation</see>: <a href="https://en.wiktionary.org/wiki/en-_-en"><i>en- -en</i></a> in <b>"enchicken"</b>" <i>(to impart the features of a juvenile bird)</i></li>
        /// </ul>
        /// </remarks>
        /// <param name="stem">the linguistic <see cref="Stem"/> that the <see cref="BoundMorpheme"/> will surround</param>
        /// <param name="prefix">the part of the <see cref="BoundMorpheme"/> that will <b>precede</b> the <see cref="Stem"/>(i.e. "em" in "embooben")</param>
        /// <param name="suffix">the part of the <see cref="BoundMorpheme"/> that will <b>succede</b> the <see cref="Stem"/>(i.e. "en" in "embooben")</param>
        /// <param name="joiner">the <see cref="Joiner"/> interposed betwixt the <see cref="BoundMorpheme"/> and the <see cref="Stem"/>. Defaults to <see cref="string.Empty">""</see></param>
        /// <returns>a new <see cref="Affixation"/></returns>
        public static Affixation Circumfixation(string stem, string prefix, string suffix, string joiner) {
            return new Affixation(Affix.Circumfix, stem, prefix + suffix, prefix.Length, joiner);
        }

        /// <inheritdoc cref="Circumfixation(string,string,string,string)"/>
        public static Affixation Circumfixation(string stem, string prefix, string suffix, Joiner joiner = Affixing.Joiner.None) {
            return Circumfixation(stem, prefix, suffix, joiner.String());
        }

        /// <summary>
        /// Constructs a new <see cref="Affixation"/> with an <see cref="Affixing.Affix.Ambifix"/>.
        /// </summary>
        /// <remarks><inheritdoc cref="Circumfixation(string,string,string,string)"/></remarks>
        /// <param name="stem">the linguistic <see cref="Stem"/> that the <see cref="BoundMorpheme"/> will surround</param>
        /// <param name="ambifix">the <see cref="BoundMorpheme"/> that will appear on <b>BOTH SIDES</b> of the <see cref="Stem"/></param>
        /// <param name="joiner">the <see cref="Joiner"/> interposed betwixt the <see cref="BoundMorpheme"/> and the <see cref="Stem"/>. Defaults to <see cref="string.Empty">""</see></param>
        /// <returns></returns>
        public static Affixation Ambifixation(string stem, string ambifix, string joiner) {
            return new Affixation(Affix.Ambifix, stem, ambifix, joiner);
        }

        /// <inheritdoc cref="Ambifixation(string,string,string)"/>
        public static Affixation Ambifixation(string stem, string ambifix, Joiner joiner = Affixing.Joiner.None) {
            return Ambifixation(stem, ambifix, joiner.String());
        }

        private int GetIndex() {
            return Index ?? throw new NullReferenceException($"{nameof(Infixation)} requires an {nameof(Index)}!");
        }

        /// <summary>
        /// The prefixial <see cref="BoundMorpheme"/> for use with <see cref="Circumfixation(string,string,string,string)"/>.
        /// </summary>
        /// <remarks>Fun fact: "circum" is a <a href="https://en.wikipedia.org/wiki/Libfix">libfix</a>!</remarks>
        private string CircumPrefix => BoundMorpheme.Substring(0, GetIndex());
        /// <summary>
        /// The suffixial <see cref="BoundMorpheme"/> for use with <see cref="Circumfixation(string,string,string,string)"/>.
        /// </summary>
        /// <remarks>Fun fact: "circum" is a <a href="https://en.wikipedia.org/wiki/Libfix">libfix</a>!</remarks>
        private string CircumSuffix => BoundMorpheme.Substring(GetIndex(), BoundMorpheme.Length);

        public string Render() {
            return Affix switch {
                Affix.Prefix    => Stem.Prefix(BoundMorpheme, Joiner),
                Affix.Suffix    => Stem.Suffix(BoundMorpheme, Joiner),
                Affix.Infix     => Stem.Infix(BoundMorpheme, GetIndex(), Joiner),
                Affix.Circumfix => Stem.Circumfix(CircumPrefix, CircumSuffix, Joiner),
                Affix.Ambifix   => Stem.Ambifix(BoundMorpheme),
                Affix.Duplifix  => throw new NotImplementedException($"{Affix} is not supported!"),
                Affix.Transfix  => throw new NotImplementedException($"{Affix} is not supported!"),
                Affix.Disfix    => throw new NotImplementedException($"{Affix} is not supported!"),
                _               => throw new InvalidEnumArgumentException(nameof(Affix), (int) Affix, typeof(Affix))
            };
        }

        /// <summary>
        /// Whether this <see cref="Affixing.Affix"/>
        /// </summary>
        /// <param name="affix"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static bool MustBeIndexed(Affix affix) {
            return affix switch {
                Affix.Prefix    => false,
                Affix.Suffix    => false,
                Affix.Infix     => true,
                Affix.Circumfix => true,
                Affix.Ambifix   => true,
                Affix.Duplifix  => false,
                Affix.Transfix  => throw new NotImplementedException($"{affix} is not implemented by {nameof(MustBeIndexed)}!"),
                Affix.Disfix    => throw new NotImplementedException($"{affix} is not implemented by {nameof(MustBeIndexed)}!"),
                _               => throw new ArgumentOutOfRangeException(nameof(affix), affix, null)
            };
        }
    }
}
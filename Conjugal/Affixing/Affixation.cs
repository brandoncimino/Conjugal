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
    /// <see cref="AffixFlavor"/>
    [PublicAPI]
    public readonly struct Affixation : IAffixed {
        public string      Stem          { get; }
        public string      BoundMorpheme { get; }
        public string      Joiner        { get; }
        public AffixFlavor AffixFlavor   { get; }
        /// <summary>
        /// An extra, optional <see cref="int"/> used by some <see cref="AffixFlavor"/> types:
        /// <ul>
        /// <li><see cref="Affixing.AffixFlavor.Circumfix"/>: Split a single <see cref="BoundMorpheme"/> into multiple parts.</li>
        /// <li><see cref="Affixing.AffixFlavor.Infix"/>: Determine where the <see cref="BoundMorpheme"/> should be <see cref="string.Insert"/>ed into the <see cref="Stem"/>.</li>
        /// </ul>
        /// <b>NOTE:</b>
        /// <br/>
        /// For other <see cref="AffixFlavor"/> types, <see cref="Index"/> <b>must not be set</b>.
        /// </summary>
        internal int Index => AffixFlavor.GetIndex(_index);
        private readonly int? _index;

        /// <summary>
        /// Constructs a new <see cref="Affixation"/> with the given parameters.
        /// </summary>
        /// <remarks>Please construct <see cref="Affixation"/> instances via the static factory methods: <see cref="Suffixation(string,string,string)"/>, <see cref="Ambifixation(string,string,string)"/>, etc.</remarks>
        /// <exception cref="Index">If an <see cref="ArgumentException"/> is needed but missing <b>or</b> missing but needed.</exception>
        internal Affixation(AffixFlavor affixFlavor, string stem, string boundMorpheme, int? index, string joiner = "") {
            if (AffixFlavorExtensions.RequiresIndex(affixFlavor) != index.HasValue) {
                var problem = index.HasValue ? $"must not have an {nameof(index)} specified" : $"requires you to specify an {nameof(index)}";
                throw new ArgumentException($"{nameof(global::FowlFever.Conjugal.Affixing.AffixFlavor)} {nameof(affixFlavor)} {affixFlavor} {problem} when constructing an {nameof(Affixation)}!");
            }

            Stem          = stem;
            BoundMorpheme = boundMorpheme;
            Joiner        = joiner;
            AffixFlavor   = affixFlavor;
            _index        = index;
        }

        internal Affixation(
            AffixFlavor affixFlavor,
            string stem,
            string boundMorpheme,
            string joiner = ""
        ) : this(
            affixFlavor,
            stem,
            boundMorpheme,
            default,
            joiner
        ) { }

        internal Affixation(
            [NotNull] IAffix affix,
            [NotNull] string stem
        ) : this(
            affix.AffixFlavor,
            stem,
            affix.BoundMorpheme,
            default,
            affix.Joiner
        ) { }

        /// <summary>
        /// Constructs a new <see cref="Affixation"/> instance with a <see cref="Affixing.AffixFlavor.Suffix"/>.
        /// </summary>
        /// <param name="stem">the linguistic <see cref="Stem"/></param>
        /// <param name="suffix">the <see cref="BoundMorpheme"/> appended to the <see cref="Stem"/></param>
        /// <param name="joiner">the <see cref="Joiner"/> interposed betwixt the <see cref="Stem"/> and <see cref="BoundMorpheme"/>. Defaults to <see cref="string.Empty">""</see>.</param>
        /// <returns>a new <see cref="Affixation"/></returns>
        public static Affixation Suffixation(string stem, string suffix, string joiner = "") {
            return new Affixation(AffixFlavor.Suffix, stem, suffix, joiner);
        }

        /// <summary>
        /// Constructs a new <see cref="Affixation"/> with a <see cref="Affixing.AffixFlavor.Prefix"/>.
        /// </summary>
        /// <param name="stem">the linguistic <see cref="Stem"/></param>
        /// <param name="prefix">the <see cref="BoundMorpheme"/> appended to the <see cref="Stem"/></param>
        /// <param name="joiner">the <see cref="Joiner"/> interposed betwixt the <see cref="Stem"/> and <see cref="BoundMorpheme"/></param>
        /// <returns>a new <see cref="Affixation"/></returns>
        public static Affixation Prefixation(string stem, string prefix, string joiner = "") {
            return new Affixation(AffixFlavor.Prefix, stem, prefix, joiner);
        }

        /// <summary>
        /// Constructs a new <see cref="Affixation"/> with an <see cref="Affixing.AffixFlavor.Infix"/>.
        /// </summary>
        /// <param name="stem">the linguistic <see cref="Stem"/> that the <see cref="BoundMorpheme"/> will be <see cref="string.Insert">inserted</see> into</param>
        /// <param name="infix">the <see cref="BoundMorpheme"/> that will be <see cref="string.Insert">inserted</see> into the <see cref="Stem"/></param>
        /// <param name="index">the <see cref="string"/> index where the <see cref="string.Insert"/>ion will take place</param>
        /// <param name="joiner">the <see cref="Joiner"/> interposed betwixt the <see cref="Stem"/> and <see cref="BoundMorpheme"/>. Defaults to <see cref="string.Empty">""</see>.</param>
        /// <returns>a new <see cref="Affixation"/></returns>
        public static Affixation Infixation(string stem, string infix, int index, string joiner) {
            return new Affixation(AffixFlavor.Infix, stem, infix, index, joiner);
        }

        /// <summary>
        /// Constructs a new <see cref="Affixation"/> with a <see cref="Affixing.AffixFlavor.Circumfix"/>.
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
            return new Affixation(AffixFlavor.Circumfix, stem, prefix + suffix, prefix.Length, joiner);
        }

        /// <summary>
        /// Constructs a new <see cref="Affixation"/> with an <see cref="Affixing.AffixFlavor.Ambifix"/>.
        /// </summary>
        /// <remarks><inheritdoc cref="Circumfixation(string,string,string,string)"/></remarks>
        /// <param name="stem">the linguistic <see cref="Stem"/> that the <see cref="BoundMorpheme"/> will surround</param>
        /// <param name="ambifix">the <see cref="BoundMorpheme"/> that will appear on <b>BOTH SIDES</b> of the <see cref="Stem"/></param>
        /// <param name="joiner">the <see cref="Joiner"/> interposed betwixt the <see cref="BoundMorpheme"/> and the <see cref="Stem"/>. Defaults to <see cref="string.Empty">""</see></param>
        /// <returns></returns>
        public static Affixation Ambifixation(string stem, string ambifix, string joiner) {
            return new Affixation(AffixFlavor.Ambifix, stem, ambifix, joiner);
        }

        /// <summary>
        /// The prefixial <see cref="BoundMorpheme"/> for use with <see cref="Circumfixation(string,string,string,string)"/>.
        /// </summary>
        /// <remarks>Fun fact: "circum" is a <a href="https://en.wikipedia.org/wiki/Libfix">libfix</a>!</remarks>
        private string CircumPrefix => BoundMorpheme.Substring(0, Index);

        /// <summary>
        /// The suffixial <see cref="BoundMorpheme"/> for use with <see cref="Circumfixation(string,string,string,string)"/>.
        /// </summary>
        /// <remarks>Fun fact: "circum" is a <a href="https://en.wikipedia.org/wiki/Libfix">libfix</a>!</remarks>
        private string CircumSuffix => BoundMorpheme.Substring(Index, BoundMorpheme.Length);

        public string Render() {
            return AffixFlavor switch {
                AffixFlavor.Prefix    => $"{BoundMorpheme}{Joiner}{Stem}",
                AffixFlavor.Suffix    => $"{Stem}{Joiner}{BoundMorpheme}",
                AffixFlavor.Infix     => Stem.Insert(Index, BoundMorpheme.Ambifix(Joiner)),
                AffixFlavor.Circumfix => $"{CircumPrefix}{Joiner}{Stem}{Joiner}{CircumSuffix}",
                AffixFlavor.Ambifix   => Stem.Circumfix(BoundMorpheme, BoundMorpheme, ""),
                AffixFlavor.Duplifix  => throw new NotImplementedException($"{AffixFlavor} is not supported!"),
                AffixFlavor.Transfix  => throw new NotImplementedException($"{AffixFlavor} is not supported!"),
                AffixFlavor.Disfix    => throw new NotImplementedException($"{AffixFlavor} is not supported!"),
                _                     => throw new InvalidEnumArgumentException(nameof(AffixFlavor), (int) AffixFlavor, typeof(AffixFlavor))
            };
        }

        public string Describe() {
            return $"{nameof(Stem)}: '{Stem}', {AffixFlavor}: '{BoundMorpheme}', {nameof(Joiner)}: '{Joiner}', {nameof(Index)}: '{Index}'";
        }

        public override string ToString() {
            return Render();
        }

        public static implicit operator string(Affixation affixation) {
            return affixation.Render();
        }
    }
}
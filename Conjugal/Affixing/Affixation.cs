using System;
using System.ComponentModel;
using System.Text;

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
        /// <inheritdoc />
        public string? Stem { get; init; }
        /// <inheritdoc />
        public string? BoundMorpheme { get; init; }
        /// <inheritdoc />
        public string? Joiner { get; init; }
        /// <inheritdoc />
        public AffixFlavor AffixFlavor { get; init; }
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
        internal Index Index {
            get => AffixFlavor.ValidateIndex(_index);
            init => _index = AffixFlavor.ValidateIndex(value);
        }

        /// <inheritdoc />
        public Affixation(AffixFlavor flavor) : this() {
            AffixFlavor = flavor;
        }

        private readonly Index _index;

        internal const string DefaultJoiner = "";

        /// <summary>
        /// Constructs a new <see cref="Affixation"/> instance with a <see cref="Affixing.AffixFlavor.Suffix"/>.
        /// </summary>
        /// <param name="stem">the linguistic <see cref="Stem"/></param>
        /// <param name="suffix">the <see cref="BoundMorpheme"/> appended to the <see cref="Stem"/></param>
        /// <param name="joiner">the <see cref="Joiner"/> interposed betwixt the <see cref="Stem"/> and <see cref="BoundMorpheme"/>. Defaults to <see cref="string.Empty">""</see>.</param>
        /// <returns>a new <see cref="Affixation"/></returns>
        public static Affixation Suffixation(string? stem, string? suffix, string? joiner = DefaultJoiner) {
            return new Affixation {
                AffixFlavor   = AffixFlavor.Suffix,
                Stem          = stem,
                BoundMorpheme = suffix,
                Joiner        = joiner,
            };
        }

        /// <summary>
        /// Constructs a new <see cref="Affixation"/> with a <see cref="Affixing.AffixFlavor.Prefix"/>.
        /// </summary>
        /// <param name="stem">the linguistic <see cref="Stem"/></param>
        /// <param name="prefix">the <see cref="BoundMorpheme"/> appended to the <see cref="Stem"/></param>
        /// <param name="joiner">the <see cref="Joiner"/> interposed betwixt the <see cref="Stem"/> and <see cref="BoundMorpheme"/></param>
        /// <returns>a new <see cref="Affixation"/></returns>
        public static Affixation Prefixation(string? stem, string? prefix, string? joiner = DefaultJoiner) {
            return new Affixation {
                AffixFlavor   = AffixFlavor.Prefix,
                Stem          = stem,
                BoundMorpheme = prefix,
                Joiner        = joiner,
            };
        }

        /// <summary>
        /// Constructs a new <see cref="Affixation"/> with an <see cref="Affixing.AffixFlavor.Infix"/>.
        /// </summary>
        /// <param name="stem">the linguistic <see cref="Stem"/> that the <see cref="BoundMorpheme"/> will be <see cref="string.Insert">inserted</see> into</param>
        /// <param name="infix">the <see cref="BoundMorpheme"/> that will be <see cref="string.Insert">inserted</see> into the <see cref="Stem"/></param>
        /// <param name="insertionPoint">the <see cref="string"/> index where the <see cref="string.Insert"/>ion will take place</param>
        /// <param name="joiner">the <see cref="Joiner"/> interposed betwixt the <see cref="Stem"/> and <see cref="BoundMorpheme"/>. Defaults to <see cref="string.Empty">""</see>.</param>
        /// <returns>a new <see cref="Affixation"/></returns>
        public static Affixation Infixation(string? stem, string? infix, Index insertionPoint, string? joiner = DefaultJoiner) {
            return new Affixation {
                AffixFlavor   = AffixFlavor.Infix,
                Stem          = stem,
                BoundMorpheme = infix,
                Index         = insertionPoint,
                Joiner        = joiner,
            };
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
        public static Affixation Circumfixation(string? stem, string? prefix, string? suffix, string? joiner = DefaultJoiner) {
            return new Affixation() {
                AffixFlavor   = AffixFlavor.Circumfix,
                Stem          = stem,
                BoundMorpheme = prefix + suffix,
                Index         = prefix?.Length ?? 0,
                Joiner        = joiner,
            };
        }

        /// <summary>
        /// Constructs a new <see cref="Affixation"/> with an <see cref="Affixing.AffixFlavor.Ambifix"/>.
        /// </summary>
        /// <remarks><inheritdoc cref="Circumfixation(string,string,string,string)"/></remarks>
        /// <param name="stem">the linguistic <see cref="Stem"/> that the <see cref="BoundMorpheme"/> will surround</param>
        /// <param name="ambifix">the <see cref="BoundMorpheme"/> that will appear on <b>BOTH SIDES</b> of the <see cref="Stem"/></param>
        /// <param name="joiner">the <see cref="Joiner"/> interposed betwixt the <see cref="BoundMorpheme"/> and the <see cref="Stem"/>. Defaults to <see cref="string.Empty">""</see></param>
        /// <returns></returns>
        public static Affixation Ambifixation(string? stem, string? ambifix, string? joiner = DefaultJoiner) {
            return new Affixation {
                AffixFlavor   = AffixFlavor.Ambifix,
                Stem          = stem,
                BoundMorpheme = ambifix,
                Joiner        = joiner,
            };
        }

        /// <returns>the final result of this <see cref="Affixation"/></returns>
        /// <exception cref="NotImplementedException">if this <see cref="AffixFlavor"/> hasn't been implemented</exception>
        /// <exception cref="InvalidEnumArgumentException">if this <see cref="AffixFlavor"/> is unknown</exception>
        public string Render() {
            return this switch {
                { Stem.Length: > 0, BoundMorpheme.Length: > 0 } => _build(),
                { Stem: not null }                              => Stem,
                _                                               => "",
            };
        }

        private string _build() {
            return AffixFlavor switch {
                AffixFlavor.Prefix    => $"{BoundMorpheme}{Joiner}{Stem}",
                AffixFlavor.Suffix    => $"{Stem}{Joiner}{BoundMorpheme}",
                AffixFlavor.Infix     => Stem?.Insert(Index.GetOffset(Stem.Length), $"{Joiner}{BoundMorpheme}{Joiner}") ?? "",
                AffixFlavor.Circumfix => _BuildCircumfix(),
                AffixFlavor.Ambifix   => $"{BoundMorpheme}{Joiner}{Stem}{Joiner}{BoundMorpheme}",
                AffixFlavor.Duplifix  => throw new NotImplementedException($"{AffixFlavor} is not supported!"),
                AffixFlavor.Transfix  => throw new NotImplementedException($"{AffixFlavor} is not supported!"),
                AffixFlavor.Disfix    => throw new NotImplementedException($"{AffixFlavor} is not supported!"),
                _                     => throw new InvalidEnumArgumentException(nameof(AffixFlavor), (int)AffixFlavor, typeof(AffixFlavor))
            };
        }

        private string _BuildCircumfix() {
            var sb = new StringBuilder();

            if (BoundMorpheme?[..Index] is { Length: > 0 } circumPrefix) {
                sb.Append(circumPrefix);
                sb.Append(Joiner);
            }

            sb.Append(Stem);

            // ReSharper disable once InvertIf
            if (BoundMorpheme?[Index..] is { Length: > 0 } circumSuffix) {
                sb.Append(Joiner);
                sb.Append(circumSuffix);
            }

            return sb.ToString();
        }

        /// <returns>individual properties of this <see cref="Affixation"/> for debugging</returns>
        public string Describe() {
            return $"{nameof(Stem)}: '{Stem}', {AffixFlavor}: '{BoundMorpheme}', {nameof(Joiner)}: '{Joiner}', {nameof(Index)}: '{Index}'";
        }

        /// <returns><see cref="Render"/></returns>
        public override string ToString() {
            return Render();
        }

        /// <summary>
        /// Implicitly <see cref="Render"/>s this <see cref="Affixation"/>.
        /// </summary>
        /// <param name="affixation">this <see cref="Affixation"/></param>
        /// <returns><see cref="Render"/></returns>
        public static implicit operator string(Affixation affixation) {
            return affixation.Render();
        }
    }
}
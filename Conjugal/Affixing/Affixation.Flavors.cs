using System;

namespace FowlFever.Conjugal.Affixing;

public readonly ref partial struct Affixation {
    #region Flavors

    /// <summary>
    /// Constructs a new <see cref="Affixation"/> instance with a <see cref="Affixing.AffixFlavor.Suffix"/>.
    /// </summary>
    /// <param name="stem">the linguistic <see cref="Stem"/></param>
    /// <param name="suffix">the <see cref="BoundMorpheme"/> appended to the <see cref="Stem"/></param>
    /// <param name="joiner">the <see cref="Joiner"/> interposed betwixt the <see cref="Stem"/> and <see cref="BoundMorpheme"/>.</param>
    /// <returns>a new <see cref="Affixation"/></returns>
    public static Affixation Suffixation(ReadOnlySpan<char> stem, ReadOnlySpan<char> suffix, ReadOnlySpan<char> joiner = default) {
        return new Affixation {
            AffixFlavor    = AffixFlavor.Suffix,
            Stem           = stem,
            BoundMorpheme  = suffix,
            Joiner         = joiner,
            InsertionPoint = Index.End,
        };
    }

    /// <summary>
    /// Constructs a new <see cref="Affixation"/> with a <see cref="Affixing.AffixFlavor.Prefix"/>.
    /// </summary>
    /// <param name="stem">the linguistic <see cref="Stem"/></param>
    /// <param name="prefix">the <see cref="BoundMorpheme"/> appended to the <see cref="Stem"/></param>
    /// <param name="joiner">the <see cref="Joiner"/> interposed betwixt the <see cref="Stem"/> and <see cref="BoundMorpheme"/></param>
    /// <returns>a new <see cref="Affixation"/></returns>
    public static Affixation Prefixation(ReadOnlySpan<char> stem, ReadOnlySpan<char> prefix, ReadOnlySpan<char> joiner = default) {
        return new Affixation {
            AffixFlavor    = AffixFlavor.Prefix,
            Stem           = stem,
            BoundMorpheme  = prefix,
            Joiner         = joiner,
            InsertionPoint = Index.Start,
        };
    }

    /// <summary>
    /// Constructs a new <see cref="Affixation"/> with an <see cref="Affixing.AffixFlavor.Infix"/>.
    /// </summary>
    /// <param name="stem">the linguistic <see cref="Stem"/> that the <see cref="BoundMorpheme"/> will be <see cref="string.Insert">inserted</see> into</param>
    /// <param name="infix">the <see cref="BoundMorpheme"/> that will be <see cref="string.Insert">inserted</see> into the <see cref="Stem"/></param>
    /// <param name="insertionPoint">the <see cref="string"/> index where the <see cref="string.Insert"/>ion will take place</param>
    /// <param name="joiner">the <see cref="Joiner"/> interposed betwixt the <see cref="Stem"/> and <see cref="BoundMorpheme"/>.</param>
    /// <returns>a new <see cref="Affixation"/></returns>
    public static Affixation Infixation(ReadOnlySpan<char> stem, ReadOnlySpan<char> infix, Index insertionPoint, ReadOnlySpan<char> joiner = default) {
        return new Affixation {
            AffixFlavor    = AffixFlavor.Infix,
            Stem           = stem,
            BoundMorpheme  = infix,
            Joiner         = joiner,
            InsertionPoint = insertionPoint,
        };
    }

    /// <summary>
    /// Constructs a new <see cref="Affixation"/> with a <see cref="Affixing.AffixFlavor.Circumfix"/>.
    /// </summary>
    /// <remarks>
    /// Both <see cref="Circumfixation"/> and <see cref="Ambifixation"/> split the <see cref="BoundMorpheme"/> into two parts that bookend the <see cref="Stem"/>.
    /// <ul>If the parts of the <see cref="BoundMorpheme"/> are:
    /// <li><b>unique</b>, use <b><see cref="Circumfixation">Circumfixation</see>:</b> <a href="https://en.wiktionary.org/wiki/em-_-en"><i>em- -en</i></a> in <b>"embooben"</b> <i>(to endow with assets)</i></li>
    /// <li><b>identical</b>, use <b><see cref="Ambifixation">Ambifixation</see>:</b> <a href="https://en.wiktionary.org/wiki/en-_-en"><i>en- -en</i></a> in <b>"enchicken"</b>" <i>(to impart the features of poultry)</i></li>
    /// </ul>
    /// </remarks>
    /// <param name="stem">the linguistic <see cref="Stem"/> that the <see cref="BoundMorpheme"/>s will surround</param>
    /// <param name="prefix">the <see cref="BoundMorpheme"/> that will <b>precede</b> the <see cref="Stem"/> (i.e. "em" in "embooben")</param>
    /// <param name="suffix">the <see cref="BoundMorpheme2"/> that will <b>succede</b> the <see cref="Stem"/> (i.e. "en" in "embooben")</param>
    /// <param name="joiner">the <see cref="Joiner"/> interposed betwixt the <see cref="BoundMorpheme"/> and the <see cref="Stem"/></param>
    /// <returns>a new <see cref="Affixation"/></returns>
    public static Affixation Circumfixation(ReadOnlySpan<char> stem, ReadOnlySpan<char> prefix, ReadOnlySpan<char> suffix, ReadOnlySpan<char> joiner = default) {
        return new Affixation {
            AffixFlavor    = AffixFlavor.Circumfix,
            Stem           = stem,
            BoundMorpheme  = prefix,
            BoundMorpheme2 = suffix,
            Joiner         = joiner,
        };
    }

    /// <summary>
    /// Constructs a new <see cref="Affixation"/> with an <see cref="Affixing.AffixFlavor.Ambifix"/>.
    /// </summary>
    /// <remarks><inheritdoc cref="Circumfixation"/></remarks>
    /// <param name="stem">the linguistic <see cref="Stem"/> that the <see cref="BoundMorpheme"/> will surround</param>
    /// <param name="ambifix">the <see cref="BoundMorpheme"/> that will appear on <b>BOTH SIDES</b> of the <see cref="Stem"/></param>
    /// <param name="joiner">the <see cref="Joiner"/> interposed betwixt the <see cref="BoundMorpheme"/> and the <see cref="Stem"/></param>
    /// <returns>a new <see cref="Affixation"/></returns>
    public static Affixation Ambifixation(ReadOnlySpan<char> stem, ReadOnlySpan<char> ambifix, ReadOnlySpan<char> joiner = default) {
        return new Affixation {
            AffixFlavor    = AffixFlavor.Ambifix,
            Stem           = stem,
            BoundMorpheme  = ambifix,
            BoundMorpheme2 = ambifix,
            Joiner         = joiner,
        };
    }

    /// <summary>
    /// Constructs a new <see cref="Affixing.AffixFlavor.Duplifix"/>-flavored <see cref="Affixation"/>, which repeats the <paramref name="stem"/> with an optional <paramref name="joiner"/>.
    /// </summary>
    /// <param name="stem">the linguistic <see cref="Stem"/> that will be repeated</param>
    /// <param name="joiner">the <see cref="BoundMorpheme"/> that will be interposed betwixt the repetitions of the <paramref name="stem"/></param>
    /// <returns>a new <see cref="Affixation"/></returns>
    public static Affixation Duplifixation(ReadOnlySpan<char> stem, ReadOnlySpan<char> joiner = default) {
        return new Affixation {
            AffixFlavor    = AffixFlavor.Duplifix,
            Stem           = stem,
            BoundMorpheme  = stem,
            Joiner         = joiner,
            InsertionPoint = Index.End,
        };
    }

    #endregion
}
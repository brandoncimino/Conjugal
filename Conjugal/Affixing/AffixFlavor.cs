using JetBrains.Annotations;

namespace FowlFever.Conjugal.Affixing {
    /// <summary>
    /// Enumerates different kinds of <a href="https://en.wikipedia.org/wiki/Affix">affixes</a>.
    /// </summary>
    /// <remarks>
    /// <ul>
    /// <li>
    /// <a href="https://en.wikipedia.org/wiki/Simulfix">Simulfix</a> is <a href="https://en.wikipedia.org/wiki/Vandalism_on_Wikipedia">Wikipedia vandalism</a>.
    /// </li>
    /// <li>
    /// <a href="https://en.wikipedia.org/wiki/Suprafix">Suprafix</a> is incorrect.
    /// </li>
    /// </ul>
    /// </remarks>
    [PublicAPI]
    public enum AffixFlavor {
        /// <summary>
        /// Appears <b>before</b> the stem.
        /// </summary>
        /// <remarks><a href="https://en.wikipedia.org/wiki/Prefix">Wikipedia - Prefix</a></remarks>
        /// <example>
        /// <a href="https://en.wiktionary.org/wiki/re-#English">re-</a> + <a href="https://en.wiktionary.org/wiki/fettle#Verb">fettle</a> = <a href="https://en.wiktionary.org/wiki/refettle#English">refettle</a> <i>(to fettle again)</i>
        /// </example>
        Prefix,
        /// <summary>
        /// Appears <b>after</b> the stem.
        /// </summary>
        /// <remarks><a href="https://en.wikipedia.org/wiki/Suffix">Wikipedia - Suffix</a></remarks>
        /// <example>
        /// <a href="https://en.wiktionary.org/wiki/funny#English">funny</a> + <a href="https://en.wiktionary.org/wiki/-ly#Etymology_2">-ly</a> = <a href="https://en.wiktionary.org/wiki/funnily">funnily</a> <i>(occuring in a jocular manner)</i>
        /// </example>
        Suffix,
        /// <summary>
        /// Appears <b>inside</b> the stem. Often interchangeable with <a href="https://en.wikipedia.org/wiki/Tmesis">tmesis</a>.
        /// </summary>
        /// <remarks><a href="https://en.wikipedia.org/wiki/Infix">Wikipedia - Infix</a></remarks>
        /// <example>
        /// <a href="https://en.wiktionary.org/wiki/metric_ton">metric ton</a> + <a href="https://en.wiktionary.org/wiki/fuck#English">fuck</a> = <a href="https://en.wiktionary.org/wiki/fuckton">metric fuck-ton</a> <i>(as differentiated from a <a href="https://en.wiktionary.org/wiki/long_ton">long fuck-ton</a>)</i>
        /// </example>
        Infix,
        /// <summary>
        /// <b>Surrounds</b> the stem.
        /// </summary>
        /// <remarks>
        /// <a href="https://en.wikipedia.org/wiki/Circumfix">Wikipedia - Circumfix</a>
        /// </remarks>
        /// <example>
        /// <a href="https://en.wiktionary.org/wiki/boob#Noun_2">boob</a> + <a href="https://en.wiktionary.org/wiki/em-_-en#English">em- -en</a> = <a href="https://en.wiktionary.org/wiki/endow">embooben</a> <i>(to provide with assets)</i>
        /// </example>
        Circumfix,
        /// <summary>
        /// <b>Surrounds</b> the stem, where both the preceding and succeding strings are <b>identical</b>.
        /// </summary>
        /// <remarks>
        /// <a href="https://en.wiktionary.org/wiki/ambifix">Wiktionary - Ambifix</a></remarks>
        /// <example>
        /// <a href="https://en.wiktionary.org/wiki/cold#English">cold</a> + <a href="https://en.wiktionary.org/wiki/en-_-en#English">en- -en</a> = <a href="https://en.wiktionary.org/wiki/encolden#English">encolden</a> <i>(to approach <a href="https://en.wiktionary.org/wiki/aloof">maximum aloofness</a>)</i>
        /// </example>
        /// <seealso cref="Circumfix"/>
        Ambifix,
        /// <summary>
        /// <b>Duplicates</b> the stem, possibly with <a href="https://en.wiktionary.org/wiki/ablaut">ablaution</a>.
        /// </summary>
        /// <remarks>
        /// <a href="https://en.wikipedia.org/wiki/duplifix">Wikipedia - Duplifix</a>
        /// <p/>
        /// Not to be confused with "<a href="https://en.wiktionary.org/wiki/like_like">like, <i><b>like</b></i> like</a>" <i>(<a href="https://en.wikipedia.org/wiki/Contrastive_focus_reduplication">contrastive focus reduplication</a>)</i>.
        /// </remarks>
        /// <example>
        /// <ul>
        /// <li><b>Exact:</b> <a href="https://en.wiktionary.org/wiki/boo-boo">boo-boo</a> <i>(a minor injury)</i></li>
        /// <li><b><a href="https://en.wiktionary.org/wiki/ablaut">Ablaut:</a></b> <a href="https://en.wiktionary.org/wiki/flim-flam#English">flimflam</a> <i>(<a href="https://en.wiktionary.org/wiki/gobbledygook">gobbledygook</a>, <a href="https://en.wiktionary.org/wiki/falderal">falderal</a>, <a href="https://en.wiktionary.org/wiki/galimatias">galimatias</a>, <a href="https://en.wiktionary.org/wiki/bupkis">bupkis</a>, <a href="https://en.wiktionary.org/wiki/clamjamfrey">clamjamfrey</a>)</i></li>
        /// <li><b>Rhyming:</b> <a href="https://en.wiktionary.org/wiki/honeybunny#English">honeybunny</a> <i>(a <a href="https://en.wiktionary.org/wiki/lepus#Latin">lepus</a> <a href="https://en.wikipedia.org/wiki/Supersaturation">supersaturated</a> with <a href="https://en.wikipedia.org/wiki/Monosaccharide">monosaccharides</a>)</i></li>
        /// </ul>
        /// </example>
        Duplifix,
        /// <summary>
        /// Alternates between chunks of the stem and root. Supposedly doesn't exist in English; but I bet it does.
        /// </summary>
        /// <remarks><a href="https://en.wikipedia.org/wiki/Transfix">Wikipedia - Transfix</a></remarks>
        /// <example>
        /// stem + root = st<i>ro</i>em<i>ot</i>
        /// </example>
        Transfix,
        /// <summary>
        /// <b>Removed</b> from the stem.
        /// </summary>
        /// <remarks><a href="https://en.wikipedia.org/wiki/Disfix">Wikipedia - Disfix</a></remarks>
        Disfix,
    }
}
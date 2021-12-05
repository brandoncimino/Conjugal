using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations {
    /// <summary>
    /// Denotes a <a href="https://en.wikipedia.org/wiki/Collective_noun#Terms_of_venery">term of venery</a>, i.e., <a href="https://archive.org/details/bokeofsaintalban00bernuoft/page/114/mode/2up?view=theater">the compaynys of beestys and fowlys</a>.
    /// </summary>
    /// <example>
    /// From the <a href="https://en.wikipedia.org/wiki/Book_of_Saint_Albans#Hunting">Book of Saint Albans</a>'s <a href="https://archive.org/details/bokeofsaintalban00bernuoft/page/114/mode/2up?view=theater">the compaynys of beestys and fowlys</a>:
    /// <ul>
    /// <li>A superfluity of nuns</li>
    /// <li>a feast of brewers</li>
    /// <li>a goring of butchers</li>
    /// <li>an observance of hermits</li>
    /// <li>a school of clerks</li>
    /// <li>a doctrine of doctors</li>
    /// <li>a tabernacle of bakers</li>
    /// <li>a prudence of vicars</li>
    /// <li>a state of princes</li>
    /// <li>a congregation of people</li>
    /// <li>a diligence of messengers</li>
    /// <li>a discretion of priests</li>
    /// <li>an execution of officers</li>
    /// <li>an eloquence of lawyers</li>
    /// <li>a drunkenship of cobblers</li>
    /// <li>a proud showing of tailors</li>
    /// <li>a skulk of thieves</li>
    /// <li>an unkindness of ravens</li>
    /// <li>a clattering of choughs</li>
    /// <li>a murmuration of starlings</li>
    /// <li>a charm of goldfinches</li>
    /// </ul>
    /// See also: <a href="https://www.merriam-webster.com/words-at-play/a-drudge-of-lexicographers-presents-collective-nouns/geese-ducks-swans-and-their-location">A Drudge of Lexicographers Presents: Collective Nouns</a>
    /// </example>
    /// <seealso cref="Countability.Collective"/>
    [PublicAPI]
    public class CollectiveNounAttribute : PlurableWrapperAttribute {
        public CollectiveNounAttribute(string singular, Countability countability) : base(singular, countability) { }
        public CollectiveNounAttribute(string singularAndPlural) : base(singularAndPlural) { }
        public CollectiveNounAttribute(string singular, string plural) : base(singular, plural) { }
        public CollectiveNounAttribute(string singular, string plural, Countability countability) : base(singular, plural, countability) { }
    }
}
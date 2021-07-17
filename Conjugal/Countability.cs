namespace FowlFever.Conjugal {
    /// <summary>
    /// Enumerates types of <a href="https://en.wikipedia.org/wiki/Count_noun">grammatic countability</a>.
    /// </summary>
    public enum Countability {
        /// <remarks><a href="https://en.wikipedia.org/wiki/Count_noun">Wikipedia - Count noun</a></remarks>
        Countable,
        /// <remarks>Also known as a <a href="https://en.wikipedia.org/wiki/Mass_noun">mass noun</a>.</remarks>
        Uncountable,
        /// <remarks><a href="https://en.wikipedia.org/wiki/Collective_noun">Wikipedia - Collective noun</a></remarks>
        /// <summary>
        ///     TODO: Wikipedia groups this with <see cref="Countable"/> and <see cref="Uncountable"/>, but does it make sense to include it as a value of <see cref="Countability"/>? Wikipedia does note British vs. Freedom english often consider <see cref="Collective"/> nouns to have different <see cref="Countability"/> from one another...
        /// </summary>
        /// <seealso cref="Conjugal.Annotations.CollectiveNounAttribute"/>
        Collective,
    }
}
using System;

using Humanizer;

using JetBrains.Annotations;

namespace FowlFever.Conjugal {
    /// <summary>
    /// Grabs all of the <see cref="IConjugal"/> information from the <see cref="ConjugalType"/>'s <see cref="FowlFever.Conjugal.Annotations.ConjugalAttribute"/>s.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [PublicAPI]
    public readonly struct Conjugation<T> : IConjugal {
        public Type           ConjugalType       => typeof(T);
        public string         Singular           => ConjugalType.Singular();
        public string         Plural             => ConjugalType.Plural();
        public string         Lemma              => ConjugalType.Lemma();
        public bool           IsProperNoun       => ConjugalType.IsProperNoun();
        public string         NounalVerb         => ConjugalType.NounalVerb();
        public UnitOfMeasure? UnitOfMeasure      => ConjugalType.UnitOfMeasure();
        public LetterCasing?  PreferredCasing    => ConjugalType.PreferredCasing();
        public string         Abbreviation       => ConjugalType.Abbreviation();
        public string         PluralAbbreviation => ConjugalType.PluralAbbreviation();
    }
}
using System;

using JetBrains.Annotations;

namespace FowlFever.Conjugal {
    /// <summary>
    /// Grabs all of the <see cref="Conjugal.IConjugal"/> information from the <see cref="ConjugalType"/>'s <see cref="Conjugal."/>s.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [PublicAPI]
    public readonly struct Conjugation<T> : IConjugal {
        public Type   ConjugalType => typeof(T);
        public string Singular     => throw new NotImplementedException();
        public string Plural       => ConjugalType.Plural();
        public string Lemma        => ConjugalType.Lemma();
        public bool   IsCountable  => ConjugalType.IsCountable();
        public bool   IsProperNoun => ConjugalType.IsProperNoun();
        public string NounalVerb   => ConjugalType.NounalVerb();
    }

    public readonly struct Conjugation : IConjugal {
        public string Singular     { get; }
        public string Plural       { get; }
        public string Lemma        { get; }
        public bool   IsCountable  { get; }
        public bool   IsProperNoun { get; }
        public string NounalVerb   { get; }

        public Conjugation(
            string singular = default,
            string plural = default,
            string lemma = default,
            bool isCountable = default,
            bool isProperNoun = default,
            string nounalVerb = default
        ) {
            Singular     = singular;
            Lemma        = lemma;
            IsCountable  = isCountable;
            IsProperNoun = isProperNoun;
            Plural       = plural;
            NounalVerb   = nounalVerb;
        }
    }
}
using System;
using System.ComponentModel;
using System.Reflection;

using FowlFever.Conjugal.Annotations;

using Humanizer;

using JetBrains.Annotations;

namespace FowlFever.Conjugal {
    /// <summary>
    /// Grabs all of the <see cref="IConjugal"/> information from the <see cref="ConjugalType"/>'s <see cref="FowlFever.Conjugal.Annotations.ConjugalAttribute"/>s.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [PublicAPI]
    public readonly struct Conjugation<T> : IConjugal {
        public Type   ConjugalType => typeof(T);
        public string Singular     => ConjugalType.GetCustomAttribute<SingularAttribute>()?.Singular ?? ConjugalType.Lemma();
        public string Plural       => ConjugalType.GetCustomAttribute<PluralAttribute>()?.Plural ?? PluralFromCountability();

        private string PluralFromCountability() {
            return Countability switch {
                Countability.Countable   => Singular.Pluralize(),
                Countability.Uncountable => Singular,
                Countability.Collective  => throw new NotImplementedException($"Not implemented: {Countability.Collective}"),
                _                        => throw new InvalidEnumArgumentException(nameof(Countability), (int) Countability, typeof(Countability))
            };
        }

        public string         Lemma           => ConjugalType.Lemma();
        public bool           IsProperNoun    => ConjugalType.IsProperNoun();
        public string         NounalVerb      => ConjugalType.NounalVerb();
        public UnitOfMeasure? UnitOfMeasure   => ConjugalType.UnitOfMeasure();
        public LetterCasing?  PreferredCasing => ConjugalType.PreferredCasing();
        public Plurable?      Abbreviation    => ConjugalType.Abbreviation();
        public Countability   Countability    => ConjugalType.Countability();
    }

    [PublicAPI]
    public readonly struct Conjugation : IConjugal {
        public readonly Type           ConjugalType;
        public          string         Singular        => ConjugalType.Singular();
        public          string         Plural          => ConjugalType.Plural();
        public          string         Lemma           => ConjugalType.Lemma();
        public          bool           IsProperNoun    => ConjugalType.IsProperNoun();
        public          string         NounalVerb      => ConjugalType.NounalVerb();
        public          UnitOfMeasure? UnitOfMeasure   => ConjugalType.UnitOfMeasure();
        public          LetterCasing?  PreferredCasing => ConjugalType.PreferredCasing();
        public          Plurable?      Abbreviation    => ConjugalType.Abbreviation();
        public          Countability   Countability    => ConjugalType.Countability();

        public Conjugation(Type type) {
            ConjugalType = type;
        }
    }
}
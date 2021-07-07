using System;
using System.Reflection;
using Humanizer;

namespace BenthicProfiler.Annotations
{
    public readonly struct Conjugation
    {
        public readonly bool Countable;
        public readonly bool IsProperNoun;
        public readonly string Lemma;
        public readonly Type ConjugalType;
        public readonly string Plural;

        public Conjugation(Type conjugalType)
        {
            ConjugalType = conjugalType;
            Lemma = conjugalType.Lemma();
            Countable = conjugalType.IsCountable();
            IsProperNoun = conjugalType.IsProperNoun();
            Plural = conjugalType.Plural();
        }

        public string Pluralize(int? count = default, Pluralizer? pluralizer = default)
        {
            if (count == 0 || count == null || !Countable)
            {
                return Lemma;
            }

            return pluralizer switch
            {
                Pluralizer.Annotation => ConjugalType.GetCustomAttribute<PluralAttribute>()?.Plural,
                Pluralizer.Humanizer => Lemma.Pluralize(),
                _ => ConjugalType.Pluralize()
            };
        }
    }
}
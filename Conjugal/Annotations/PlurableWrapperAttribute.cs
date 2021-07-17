using JetBrains.Annotations;

namespace FowlFever.Conjugal.Annotations {
    /// <summary>
    /// A base class for <see cref="ConjugalAttribute"/>s that wrap a single <see cref="Plurable"/> <see cref="Value"/>.
    /// Provides constructors that mimic <see cref="Plurable"/>'s.
    /// </summary>
    [PublicAPI]
    public abstract class PlurableWrapperAttribute : ConjugalAttribute {
        public Plurable Value { get; }

        protected PlurableWrapperAttribute([NotNull] string singular, Countability countability) {
            Value = Plurable.Of(singular, countability);
        }

        protected PlurableWrapperAttribute([NotNull] string singularAndPlural) {
            Value = Plurable.Uncountable(singularAndPlural);
        }

        protected PlurableWrapperAttribute([NotNull] string singular, [NotNull] string plural) {
            Value = Plurable.Of(singular, plural);
        }

        protected PlurableWrapperAttribute([NotNull] string singular, [NotNull] string plural, Countability countability) {
            Value = Plurable.Of(singular, plural, countability);
        }
    }
}
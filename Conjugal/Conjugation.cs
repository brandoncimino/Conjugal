using System;

using Humanizer;

using JetBrains.Annotations;

namespace FowlFever.Conjugal;

/// <inheritdoc />
/// <summary>
/// Grabs all of the <see cref="T:FowlFever.Conjugal.IConjugal" /> information from the <see cref="F:FowlFever.Conjugal.Conjugation`1.ConjugalType" />'s <see cref="T:FowlFever.Conjugal.Annotations.ConjugalAttribute" />s.
/// </summary>
/// <remarks>
/// This is a <c>struct</c> instead of a <c>class</c> to sort of "fake" instantiating it:
/// all of the instance members refer to the static <see cref="ConjugalType"/> / <see cref="TypelessConjugation"/>,
/// so there's no need to "really" instantiate it.
/// Instead, we can use <c>default</c> for everything.
/// </remarks>
/// <typeparam name="T">the <see cref="T:System.Type" /> being conjugated</typeparam>
[PublicAPI]
public readonly struct Conjugation<T> : IConjugal {
    /// <summary>
    /// This <see cref="Conjugation"/>'s <typeparamref name="T"/> <see cref="Type"/>.
    /// </summary>
    public static Type ConjugalType = typeof(T);

    /// <summary>
    /// A <see cref="Lazy{T}"/> <see cref="Conjugation"/> of this <see cref="ConjugalType"/>
    /// </summary>
    /// <remarks><see cref="Lazy{T}"/> probably isn't necessary, but it probably doesn't <i>hurt</i>, right?</remarks>
    // ReSharper disable once StaticMemberInGenericType
    private static Lazy<Conjugation> TypelessConjugation = new Lazy<Conjugation>(() => new Conjugation(ConjugalType));

#pragma warning disable 1591
    public string         Singular        => TypelessConjugation.Value.Singular;
    public string         Plural          => TypelessConjugation.Value.Plural;
    public string         Lemma           => TypelessConjugation.Value.Lemma;
    public bool           IsProperNoun    => TypelessConjugation.Value.IsProperNoun;
    public string         NounalVerb      => TypelessConjugation.Value.NounalVerb;
    public UnitOfMeasure? UnitOfMeasure   => TypelessConjugation.Value.UnitOfMeasure;
    public LetterCasing?  PreferredCasing => TypelessConjugation.Value.PreferredCasing;
    public Plurable?      Abbreviation    => TypelessConjugation.Value.Abbreviation;
    public Countability   Countability    => TypelessConjugation.Value.Countability;
#pragma warning restore 1591
}

/// <inheritdoc />
/// <summary>
/// Contains all of the <see cref="IConjugal"/> forms of a <see cref="Type"/>.
/// </summary>
[PublicAPI]
public class Conjugation : IConjugal {
    /// <summary>
    /// The <see cref="Type"/> that was conjugated.
    /// </summary>
    public readonly Type ConjugalType;

#pragma warning disable 1591
    public string         Singular        { get; }
    public string         Plural          { get; }
    public string         Lemma           { get; }
    public bool           IsProperNoun    { get; }
    public string         NounalVerb      { get; }
    public UnitOfMeasure? UnitOfMeasure   { get; }
    public LetterCasing?  PreferredCasing { get; }
    public Plurable?      Abbreviation    { get; }
    public Countability   Countability    { get; }
#pragma warning restore 1591

    /// <inheritdoc cref="Conjugation"/>
    /// <exception cref="ArgumentNullException">if <paramref name="conjugalType"/> is <c>null</c></exception>
    public Conjugation(Type conjugalType) {
        ConjugalType    = conjugalType ?? throw new ArgumentNullException(nameof(conjugalType));
        Singular        = ConjugalType.Singular();
        Plural          = ConjugalType.Plural();
        Lemma           = ConjugalType.Lemma();
        IsProperNoun    = ConjugalType.IsProperNoun();
        NounalVerb      = ConjugalType.NounalVerb();
        UnitOfMeasure   = ConjugalType.UnitOfMeasure();
        PreferredCasing = ConjugalType.PreferredCasing();
        Abbreviation    = ConjugalType.Abbreviation();
        Countability    = ConjugalType.Countability() ?? Plurable.InferCountability(Singular, Plural);
    }
}
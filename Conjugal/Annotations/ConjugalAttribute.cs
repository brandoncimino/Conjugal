using System;

namespace FowlFever.Conjugal.Annotations;

/// <summary>
/// The base class of all <see cref="Annotations"/>.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
public abstract class ConjugalAttribute : Attribute { }
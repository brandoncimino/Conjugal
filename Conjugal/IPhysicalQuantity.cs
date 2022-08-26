using JetBrains.Annotations;

namespace FowlFever.Conjugal;

/// <summary>
/// An extremely simple interface for representing <a href="https://en.wikipedia.org/wiki/Physical_quantity">physical quantities</a>,
/// which are <a href="https://en.wikipedia.org/wiki/Quantity">quantities</a> paired with <a href="https://en.wikipedia.org/wiki/Unit_of_measurement">units of measure</a>.
/// </summary>
[PublicAPI]
public interface IPhysicalQuantity<out TValue> {
    public TValue        Value { get; }
    public UnitOfMeasure Unit  { get; }
}

/// <inheritdoc cref="IPhysicalQuantity{TValue}"/>
[PublicAPI]
public interface IPhysicalQuantity : IPhysicalQuantity<double> { }
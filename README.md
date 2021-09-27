# Conjugal

> Structs and annotations for producing nice word conjugations and other linguistic metadata, such as [abbreviations](Conjugal/Annotations/AbbreviationAttribute.cs), [units of measure](Conjugal/Annotations/UnitOfMeasureAttribute.cs), and [terms of venery](Conjugal/Annotations/CollectiveNounAttribute.cs).

## [Annotations](Conjugal/Annotations)

These annotations can be placed on a class to define explicit conjugation rules such as [\[Plural\]](Conjugal/Annotations/PluralAttribute.cs).

## Structs

Structs that group together logical components of strings for formatting.

#### Example: [Plurable](Conjugal/Plurable.cs)

```c#
class Animal {
    Plurable Name;
}

var ox = new Animal(){
    Name = Plurable.of("ox", "oxen");
}

Console.WriteLine(ox.Name.Singular);    // "ox"
Console.WriteLine(ox.Name.Plural);      // "oxen"
Console.WriteLine(ox.Pluralize(0);      // "oxen"
Console.WriteLine(ox.Pluralize(1);      // "ox"
Console.WriteLine(ox.Pluralize(2);      // "oxen"
```

When possible, these are designed to be snuck in with regular-old ``string``s as unobtrusively as possible:

[Conjugal/Plurable.cs](Conjugal/Plurable.cs)
```c#
Plurable word = "egg";              // new Plural("egg","egg")
Plurable word = ("egg", "eggz");    // new Plural("egg", "eggz")
```

[Conjugal/UnitOfMeasure.cs](Conjugal/UnitOfMeasure.cs)
```c#
UnitOfMeasure kilograms = "kg";
UnitOfMeasure pounds = ("lb", "lbs");

Console.WriteLine(kilograms.Quantify(1));       // 1 kg
Console.WriteLine(kilograms.Quantify(5.6));     // 5.6 kg
Console.WriteLine(pounds.Quantify(1));          // 1 lb
Console.WriteLine(pounds.Quantify(Math.pi, 5)   // 3.14159 lbs
```
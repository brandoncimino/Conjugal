# Changelog
All notable changes to this project will be documented in this file.

- The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).
- This project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).
- Dates are in `YYYY-MM-DD` format.

## Unreleased

### Changed
- `string.{affix}()` extension methods now return `Affixation` instances _(which can be implicitly cast into `string`s)_
- Removed `string.{affix}ation()` extension methods (since they're now redundant with the `string.{affix}()` methods)
- `Affixation.Index` from `int` -> `System.Index`

### Fixed
- `Joiner`s should never be left dangling

## [1.0.0] - 2021-12-08

### Added

- Convenience extensions for `Countability` and `LetterCasing`
- `StringTokenFormatter` as a nuget dependency (note: it is not used for anything yet)

### Changed

- `ConjugalTypeExtensions.Countability` now returns `null Countability?` when the `[Countability]` attribute isn't found (instead of defaulting to `Countability.Countable`)
- Updated C# `LangVersion` to `latest`
- Replaced `[CanBeNull]` and `[NotNull]` annotations with `?` suffixes

### Fixed

- Letter casing!
  - Casing properly defaults to `LetterCasing.Lowercase` *only* when a noun's `Lemma` is derived by `Humanizer`

## [0.2.1] - 2021-09-26

### Added

- A [README.md](README.md) equivalent of [README.adoc](README.adoc)

## [0.2.0] - 2021-09-26

### Added

- `IEnumerable<string>` extensions for `Prefix` and `Suffix`
- Lots of nullability annotations

### Fixed

- Proper `null` handling for `Affixation` instances
- Proper `null` handling for `StringAffixationExtensions`

## [0.1.2] - 2021-09-04

### Added

- This [CHANGELOG.md](./CHANGELOG.md) file
- Methods to create `Plurable` instances using `Humanizer`:
  - `Plurable.Humanized`
  - `ConjugalStringExtensions.Plurablize`
- Assorted missing javadocs and nullability attributes
- Included javadocs in the build output
- "javadocs" is a nice word, and I like it

## [0.1.1]

> Initial Release!
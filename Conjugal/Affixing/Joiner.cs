using System;

using JetBrains.Annotations;

namespace FowlFever.Conjugal.Affixing {
    /// <summary>
    /// Common strings used to join <a href="https://en.wiktionary.org/wiki/morpheme">morphemes</a>.
    /// </summary>
    /// <remarks>
    /// The <a href="https://en.wikipedia.org/wiki/morpheme">Wikipedia article on morphemes</a> is absolute garbage.
    /// <br/>Please use the <a href="https://en.wiktionary.org/wiki/morpheme">Wiktionary page</a> instead.
    /// </remarks>
    [PublicAPI]
    public enum Joiner {
        None,
        Space,
        Hyphen,
        Apostrophe,
        Period,
        Slash,
    }

    /// <summary>
    /// Extension methods provide "properties" and "instance methods" for <see cref="Joiner"/> enum values.
    /// </summary>
    [PublicAPI]
    public static class JoinerExtensions {
        public static string String(this Joiner joiner) {
            return joiner switch {
                Joiner.None       => "",
                Joiner.Hyphen     => "-",
                Joiner.Space      => " ",
                Joiner.Apostrophe => "'",
                Joiner.Period     => ".",
                Joiner.Slash      => "/",
                _                 => throw new ArgumentOutOfRangeException(nameof(joiner), joiner, null)
            };
        }
    }
}
using System;
using Humanizer;

namespace BenthicProfiler.Annotations
{
    public class CasingAttribute : Attribute
    {
        public readonly LetterCasing Casing;

        public CasingAttribute(LetterCasing letterCasing)
        {
            Casing = letterCasing;
        }
    }
}
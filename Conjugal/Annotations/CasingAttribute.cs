using System;

using Humanizer;

namespace Conjugal.Annotations
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
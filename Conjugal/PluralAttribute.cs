using System;

namespace BenthicProfiler.Annotations
{
    public class PluralAttribute : Attribute
    {
        public string Plural;

        public PluralAttribute(string plural)
        {
            Plural = plural;
        }
    }
}
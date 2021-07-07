using System;

namespace Conjugal.Annotations
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
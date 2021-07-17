namespace FowlFever.Conjugal.Affixing {
    public static class AffixExtensions {
        public static Affixation AffixTo(this IAffix affix, string stem) {
            return new Affixation(affix, stem);
        }
    }
}
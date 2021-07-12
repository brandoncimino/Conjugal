using FowlFever.Conjugal.Affixing;
using FowlFever.Conjugal.Annotations;

namespace Test {
    public class Valuables {
        public abstract class Valuable {
            public abstract string ExpectedQuantified(int count);
        }

        [UnitOfMeasure("¢", Affix.Suffix)]
        public class Penny : Valuable {
            public override string ExpectedQuantified(int count) {
                return $"{count}¢";
            }
        }

        [UnitOfMeasure("${}")]
        public class Dollar : Valuable {
            public override string ExpectedQuantified(int count) {
                throw new System.NotImplementedException();
            }
        }

        [UnitOfMeasure("oz t")]
        public class Gold : Valuable {
            public override string ExpectedQuantified(int count) {
                throw new System.NotImplementedException();
            }
        }

        public class Gem : Valuable {
            public override string ExpectedQuantified(int count) {
                return count.ToString();
            }
        }
    }
}
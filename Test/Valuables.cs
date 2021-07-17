using FowlFever.Conjugal.Affixing;
using FowlFever.Conjugal.Annotations;

namespace Test {
    public class Valuables {
        public abstract class Valuable {
            public abstract string ExpectedQuantified(int count);
        }

        [UnitOfMeasure("cents", "¢")]
        public class Penny : Valuable {
            public override string ExpectedQuantified(int count) {
                return $"{count}¢";
            }
        }

        [UnitOfMeasure("dollars", "$", affixFlavor: AffixFlavor.Prefix)]
        public class Dollar : Valuable {
            public override string ExpectedQuantified(int count) {
                throw new System.NotImplementedException();
            }
        }

        [UnitOfMeasure("troy ounces", "oz t")]
        public class Gold : Valuable {
            public override string ExpectedQuantified(int count) {
                throw new System.NotImplementedException();
            }
        }

        [UnitOfMeasure("karats", "kt")]
        public class Gem : Valuable {
            public override string ExpectedQuantified(int count) {
                return count.ToString();
            }
        }
    }
}
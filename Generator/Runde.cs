
namespace Volleyball {
    namespace Generator {
        public class Runde {
            public Opponent a, b;
            public int schiri;

            public Runde(Opponent a, Opponent b, int schiri) {
                this.a = a;
                this.b = b;
                this.schiri = schiri;
            }
        }
    }
}

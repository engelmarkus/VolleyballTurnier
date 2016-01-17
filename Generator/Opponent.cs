
namespace Volleyball {
    namespace Generator {
        /// Three teams forming one opponent.
        public class Opponent : System.Tuple<int, int, int> {
            public Opponent(int a, int b, int c)
                : base(a, b, c) {

            }
        }
    }
}

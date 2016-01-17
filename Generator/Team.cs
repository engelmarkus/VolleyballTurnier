
namespace Volleyball {
    namespace Generator {
        /// Two teams that have played together.
        public class Team : System.Tuple<int, int> {
            public Team(int a, int b)
                : base(a, b) {

            }
        }
    }
}

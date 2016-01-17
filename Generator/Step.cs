using System.Collections.Generic;

namespace Volleyball {
    namespace Generator {
        /// Stores the state of the Generator after each step, i. e. after two rounds have been
        /// successfully generated / all teams are assigned once.
        class Step {
            public List<Team> usedCombis;
            public List<Opponent> opponents;
            public List<int> schiris;

            public Step() {
                usedCombis = new List<Team>();
                opponents = new List<Opponent>();
                schiris = new List<int>();
            }

            public Step(Step s) {
                usedCombis = new List<Team>(s.usedCombis);
                opponents = new List<Opponent>(s.opponents);
                schiris = new List<int>(s.schiris);
            }
        }
    }
}

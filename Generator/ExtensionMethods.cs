using System;
using System.Collections.Generic;

namespace Volleyball {
    namespace Generator {
        public static class ExtensionMethods {
            private static Random rnd = new Random();

            /// Picks elements out of a list randomly; are not distinct; doesn't remove them
            public static IEnumerable<int> Randomly(this List<int> urne) {
                while (true) {
                    yield return urne[rnd.Next(urne.Count)];
                }
            }
        }
    }
}

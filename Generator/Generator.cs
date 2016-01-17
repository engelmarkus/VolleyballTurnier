using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Volleyball {
    namespace Generator {
        // TODO: Man muss noch aufpassen, wenn es 42 Teams sind, dann muss man die Schiris
        // vorher ziehen, damit danach noch 6 Gegner rauskommen, also zwei volle Runden!
        // Bei 36 Teams muss man die Schiris danach auslosen, weil sonst nur 30 Teams für
        // die Gegner übrig bleiben, und damit nur 5 Spiele zustandekommen!
        // => 36 Teams ist buggy, manche Teams haben wahrscheinlich Überschneidungen!
        // Im Wesentlichen muss man immer unterscheiden zwischen Teamanzahlen, die sich
        // gerade bzw. ungerade durch sechs teilen lassen.

        public class Generator {
            public Generator(int numTeams) {
                this.numTeams = numTeams;
                this.steps = new List<Step>();
                this.currentStep = new Step();
                this.steps.Add(new Step(currentStep));
            }

            private List<int> generateNewUrn() {
                return Enumerable.Range(1, numTeams).ToList();
            }

            private int numTeams;
            private List<Step> steps;
            private Step currentStep;            

            /// Checks whether two teams have already played together.
            private bool havePlayedTogether(int team1, int team2) {
                return team1 == team2 || currentStep.usedCombis.Contains(new Tuple<int, int>(team1, team2)) || currentStep.usedCombis.Contains(new Tuple<int, int>(team2, team1));
            }

            private List<int> drawSchiris(List<int> urn) {
                return (from x in urn.Randomly()
                        where !currentStep.schiris.Contains(x)
                        select x).Distinct().Take(6).ToList();
            }

            private Opponent drawOpponent(List<int> urn) {
                // Versuchen, einen Gegner zu erstellen
                // Dazu für x, y und z jeweils 5 Elemente ziehen und schauen, ob
                // sie sich zu einem gültigen Gegner kombinieren lassen.
                // Wenns nicht geht, null zurückgeben, sodass wir einen Backtrack
                // machen können.
                return (from x in urn.Randomly().Take(5)
                        from y in urn.Randomly().Take(5)
                        from z in urn.Randomly().Take(5)
                        where
                        !havePlayedTogether(x, y) &&
                        !havePlayedTogether(x, z) &&
                        !havePlayedTogether(y, z)
                        select new Opponent(x, y, z)
                       ).FirstOrDefault();
            }

            // Prüft, ob eine Überschneidung drin vorkommt, also ob ein Team zur selben Zeit
            // auf mehr als einem Feld stehen muss
            private bool uebersch(List<Opponent> opps) {
                var liste = new List<int>();

                foreach (var o in opps) {
                    liste.Add(o.Item1);
                    liste.Add(o.Item2);
                    liste.Add(o.Item3);
                }

                return liste.Count != liste.Distinct().Count();
            }

            private bool calculateStep(bool lastStep) {
                var urn = generateNewUrn();

                if (urn.Count == 42) {
                    var s = drawSchiris(urn);
                    currentStep.schiris.AddRange(s);
                    urn = urn.Except(s).ToList();
                }
                else {
                    currentStep.schiris.AddRange(new[] { 0, 0, 0, 0, 0, 0 });
                }
                
                // HACK: In the last step, remove all Teams that have never been Schiri, so that
                // they won't get a sixth game assigned.
                if (lastStep) {
                    urn = urn.Where(x => currentStep.schiris.Contains(x)).ToList();
                }

                while (urn.Count > 0) {
                    var g = drawOpponent(urn);

                    if (g == null) {
                        // geht irgendwie ned, do backtrack.
                        return false;
                    }


                    // Wir haben drei Leute, die wir jetzt in die benutzte Liste einfügen und dann
                    // generieren wir daraus einen neuen Gegner
                    currentStep.usedCombis.Add(new Team(g.Item1, g.Item2));
                    currentStep.usedCombis.Add(new Team(g.Item1, g.Item3));
                    currentStep.usedCombis.Add(new Team(g.Item2, g.Item3));

                    currentStep.opponents.Add(g);

                    // Unbedingt jetzt die Leute aus der Urne entfernen...
                    urn.Remove(g.Item1);
                    urn.Remove(g.Item2);
                    urn.Remove(g.Item3);
                }

                // Step completed successfully.
                return true;
            }

            public void generate() {
                /*
                42 Teams
                Jedes 5 Spiele
                Also hat man 42 * 5 Teams, die man verteilen muss
                An einem Spiel nehmen 6 Teams teil, also hat man insgesamt
                42 * 5 / 6 = 35 Spiele, die ausgetragen werden müssen.

                Bei 36 Teams:
                36 * 5 / 6 = 30 Spiele.
                */
                while (currentStep.opponents.Count < numTeams / 3 * 5) {
                    // HACK: Wenn man bei 42 Teams die letzten erzeugt, dann braucht man nur
                    // 10, nicht 12 Spiele. Deswegen der Methode das hier sagen.
                    var r = calculateStep(currentStep.opponents.Count == 60);

                    if (r == true) {
                        // Step was successful, so do next one.
                        steps.Add(new Step(currentStep));
                        Debug.WriteLine("Step successful");
                    }
                    else {
                        // do a backtrack one level.
                        currentStep = new Step(steps.Last());
                        Debug.WriteLine("Backtrack");
                    }
                }
            }

            public List<Spielfeld> sort() {
                // Wir müssen die Gegnerliste, die da rausgekommen ist, jetzt nur noch in der richtigen
                // Reihenfolge auf die einzelnen Spielfelder bzw. Runden verteilen, damit ein Team nicht
                // gleichzeitig auf mehreren Feldern stehen muss.
                // Gegner  1,  2; Schiri 1 => Feld 1, Runde 1;
                // Gegner  3,  4; Schiri 2 => Feld 2, Runde 1;
                // Gegner  5,  6; Schiri 3 => Feld 3, Runde 1;
                // Gegner  7,  8; Schiri 4 => Feld 1, Runde 2;
                // Gegner  9, 10; Schiri 5 => Feld 2, Runde 2;
                // Gegner 11, 12; Schiri 6 => Feld 3, Runde 2;
                // ...

                var feld1 = new Spielfeld();
                var feld2 = new Spielfeld();
                var feld3 = new Spielfeld();

                while (currentStep.opponents.Count != 0) {
                    feld1.runden.Add(new Runde(currentStep.opponents[0], currentStep.opponents[1], currentStep.schiris[0]));
                    feld2.runden.Add(new Runde(currentStep.opponents[2], currentStep.opponents[3], currentStep.schiris[1]));

                    if (currentStep.opponents.Count > 4) {
                        feld3.runden.Add(new Runde(currentStep.opponents[4], currentStep.opponents[5], currentStep.schiris[2]));
                        currentStep.opponents.RemoveRange(0, 6);
                        currentStep.schiris.RemoveRange(0, 3);
                    }
                    else {
                        currentStep.opponents.RemoveRange(0, 4);
                        currentStep.schiris.RemoveRange(0, 2);
                    }
                }

                return new List<Spielfeld> { feld1, feld2, feld3 };
            }
        }
    }
}

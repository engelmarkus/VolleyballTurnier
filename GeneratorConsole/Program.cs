using System;
using System.IO;
using Volleyball.Generator;

namespace GeneratorConsole {
    class Program {
        static void Main(string[] args) {
            var g = new Generator(int.Parse(args[0]));
            g.generate();

            var felder = g.sort();

            var datei = new StreamWriter("tabelle.csv");

            foreach (var f in felder) {
                foreach (var r in f.runden) {
                    datei.WriteLine(string.Format("{0};{1};{2};:;{3};{4};{5};  ;{6}", r.a.Item1, r.a.Item2, r.a.Item3, r.b.Item1, r.b.Item2, r.b.Item3, r.schiri));
                }

                datei.WriteLine();
            }

            datei.Close();
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using Volleyball.Generator;
using Excel = Microsoft.Office.Interop.Excel;

namespace Auswertung {
    public partial class AddInRibbon {
        private void AddInRibbon_Load(object sender, RibbonUIEventArgs e) {
        }

        private void generateButton_Click(object sender, RibbonControlEventArgs e) {
            var g = new Generator(int.Parse(numTeamsBox.Text));
            g.generate();

            var felder = g.sort();

            Excel.Window window = e.Control.Context;
            Excel.Worksheet activeWorksheet = (Excel.Worksheet)window.Application.ActiveSheet;

            var feld1 = activeWorksheet.Range["C2", "O2"];
            var aktuellesFeld = feld1;

            for (var i = 0; i < felder.Count; ++i) {
                for (var j = 0; j < felder[i].runden.Count; ++j) {
                    var f = felder[i];
                    var r = f.runden[j];

                    string data = string.Format("{0};{1};{2};:;{3};{4};{5};;;:;;;{6}", r.a.Item1, r.a.Item2, r.a.Item3, r.b.Item1, r.b.Item2, r.b.Item3, r.schiri);

                    aktuellesFeld.Value2 = new dynamic[] {
                        r.a.Item1, r.a.Item2, r.a.Item3,
                        ":",
                        r.b.Item1, r.b.Item2, r.b.Item3,
                        "", "", ":", "", "",
                        r.schiri };

                    aktuellesFeld = aktuellesFeld.Offset[1, 0];
                }

                aktuellesFeld = feld1.Offset[(i + 1) * 13, 0];
            }
        }
    }
}

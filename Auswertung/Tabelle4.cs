﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.Office.Tools.Excel;
using Microsoft.VisualStudio.Tools.Applications.Runtime;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;

namespace Auswertung
{
    public partial class Tabelle4
    {
        private void Tabelle4_Startup(object sender, System.EventArgs e)
        {
        }

        private void Tabelle4_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region Vom VSTO-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für Designerunterstützung -
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(Tabelle4_Startup);
            this.Shutdown += new System.EventHandler(Tabelle4_Shutdown);
        }

        #endregion

    }
}

namespace Auswertung {
    partial class AddInRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public AddInRibbon()
            : base(Globals.Factory.GetRibbonFactory()) {
            InitializeComponent();
        }

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">"true", wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls "false".</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für Designerunterstützung -
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent() {
            Microsoft.Office.Tools.Ribbon.RibbonDropDownItem ribbonDropDownItemImpl1 = this.Factory.CreateRibbonDropDownItem();
            Microsoft.Office.Tools.Ribbon.RibbonDropDownItem ribbonDropDownItemImpl2 = this.Factory.CreateRibbonDropDownItem();
            this.tab1 = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.numTeamsBox = this.Factory.CreateRibbonComboBox();
            this.generateButton = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.group1.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.group1);
            this.tab1.Label = "TabAddIns";
            this.tab1.Name = "tab1";
            // 
            // group1
            // 
            this.group1.Items.Add(this.numTeamsBox);
            this.group1.Items.Add(this.generateButton);
            this.group1.Label = "Volleyball";
            this.group1.Name = "group1";
            // 
            // numTeamsBox
            // 
            ribbonDropDownItemImpl1.Label = "36";
            ribbonDropDownItemImpl2.Label = "42";
            this.numTeamsBox.Items.Add(ribbonDropDownItemImpl1);
            this.numTeamsBox.Items.Add(ribbonDropDownItemImpl2);
            this.numTeamsBox.Label = "Anzahl der Teams";
            this.numTeamsBox.Name = "numTeamsBox";
            // 
            // generateButton
            // 
            this.generateButton.Label = "Spiele generieren";
            this.generateButton.Name = "generateButton";
            this.generateButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.generateButton_Click);
            // 
            // AddInRibbon
            // 
            this.Name = "AddInRibbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.AddInRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonComboBox numTeamsBox;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton generateButton;
    }

    partial class ThisRibbonCollection {
        internal AddInRibbon AddInRibbon {
            get {
                return this.GetRibbon<AddInRibbon>();
            }
        }
    }
}

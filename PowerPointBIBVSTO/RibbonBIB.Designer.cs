namespace PowerPointBIBVSTO
{
    partial class RibbonBIB : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        private System.ComponentModel.IContainer components = null;

        public RibbonBIB() : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Ribbon

        private void InitializeComponent()
        {
            Microsoft.Office.Tools.Ribbon.RibbonDialogLauncher ribbonDialogLauncherImpl1 = this.Factory.CreateRibbonDialogLauncher();
            Microsoft.Office.Tools.Ribbon.RibbonDropDownItem ribbonDropDownItemImpl1 = this.Factory.CreateRibbonDropDownItem();
            Microsoft.Office.Tools.Ribbon.RibbonDropDownItem ribbonDropDownItemImpl2 = this.Factory.CreateRibbonDropDownItem();
            Microsoft.Office.Tools.Ribbon.RibbonDropDownItem ribbonDropDownItemImpl3 = this.Factory.CreateRibbonDropDownItem();
            Microsoft.Office.Tools.Ribbon.RibbonDropDownItem ribbonDropDownItemImpl4 = this.Factory.CreateRibbonDropDownItem();
            this.tabBIB = this.Factory.CreateRibbonTab();
            this.grpComites = this.Factory.CreateRibbonGroup();
            this.sbComites = this.Factory.CreateRibbonSplitButton();
            this.btnNE = this.Factory.CreateRibbonButton();
            this.btnCred = this.Factory.CreateRibbonButton();
            this.btnApetito = this.Factory.CreateRibbonButton();
            this.btnBiblioteca = this.Factory.CreateRibbonButton();
            this.btnWebPanel = this.Factory.CreateRibbonButton();
            this.Herramientas = this.Factory.CreateRibbonGroup();
            this.btnGetSize = this.Factory.CreateRibbonButton();
            this.btnSetSize = this.Factory.CreateRibbonButton();
            this.btnActualizarToC = this.Factory.CreateRibbonButton();
            this.group3 = this.Factory.CreateRibbonGroup();
            this.IAButton = this.Factory.CreateRibbonButton();
            this.PegarRespuesta = this.Factory.CreateRibbonButton();
            this.grpTablas = this.Factory.CreateRibbonGroup();
            this.drpPosition = this.Factory.CreateRibbonDropDown();
            this.btnTablas = this.Factory.CreateRibbonMenu();
            this.btn4x4 = this.Factory.CreateRibbonButton();
            this.btn5x5 = this.Factory.CreateRibbonButton();
            this.btn6x6 = this.Factory.CreateRibbonButton();
            this.btn3x3 = this.Factory.CreateRibbonButton();
            this.checkRedondos = this.Factory.CreateRibbonCheckBox();
            this.editDistancia = this.Factory.CreateRibbonEditBox();
            this.tabBIB.SuspendLayout();
            this.grpComites.SuspendLayout();
            this.Herramientas.SuspendLayout();
            this.group3.SuspendLayout();
            this.grpTablas.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabBIB
            // 
            this.tabBIB.Groups.Add(this.grpComites);
            this.tabBIB.Groups.Add(this.Herramientas);
            this.tabBIB.Groups.Add(this.group3);
            this.tabBIB.Groups.Add(this.grpTablas);
            this.tabBIB.Label = "BIB";
            this.tabBIB.Name = "tabBIB";
            // 
            // grpComites
            // 
            this.grpComites.Items.Add(this.sbComites);
            this.grpComites.Items.Add(this.btnBiblioteca);
            this.grpComites.Items.Add(this.btnWebPanel);
            this.grpComites.Label = "Presentaciones";
            this.grpComites.Name = "grpComites";
            // 
            // sbComites
            // 
            this.sbComites.ImageName = "UpgradePresentation";
            this.sbComites.Items.Add(this.btnNE);
            this.sbComites.Items.Add(this.btnCred);
            this.sbComites.Items.Add(this.btnApetito);
            this.sbComites.Label = "Comités";
            this.sbComites.Name = "sbComites";
            this.sbComites.OfficeImageId = "UpgradePresentation";
            // 
            // btnNE
            // 
            this.btnNE.ImageName = "AllCategories";
            this.btnNE.Label = "Negocios Estructurados";
            this.btnNE.Name = "btnNE";
            this.btnNE.OfficeImageId = "AllCategories";
            this.btnNE.ShowImage = true;
            this.btnNE.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnNE_Click);
            // 
            // btnCred
            // 
            this.btnCred.ImageName = "AllCategories";
            this.btnCred.Label = "Crédito";
            this.btnCred.Name = "btnCred";
            this.btnCred.OfficeImageId = "AllCategories";
            this.btnCred.ShowImage = true;
            this.btnCred.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnCred_Click);
            // 
            // btnApetito
            // 
            this.btnApetito.ImageName = "AllCategories";
            this.btnApetito.Label = "Apetito";
            this.btnApetito.Name = "btnApetito";
            this.btnApetito.OfficeImageId = "AllCategories";
            this.btnApetito.ShowImage = true;
            this.btnApetito.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnApetito_Click);
            // 
            // btnBiblioteca
            // 
            this.btnBiblioteca.ImageName = "FileOpenRecentFile";
            this.btnBiblioteca.Label = "Biblioteca de Slides";
            this.btnBiblioteca.Name = "btnBiblioteca";
            this.btnBiblioteca.OfficeImageId = "FileOpenRecentFile";
            this.btnBiblioteca.ShowImage = true;
            this.btnBiblioteca.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnBiblioteca_Click);
            // 
            // btnWebPanel
            // 
            this.btnWebPanel.ImageName = "FlagToday";
            this.btnWebPanel.Label = "Validación";
            this.btnWebPanel.Name = "btnWebPanel";
            this.btnWebPanel.OfficeImageId = "FlagToday";
            this.btnWebPanel.ShowImage = true;
            this.btnWebPanel.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnWebPanel_Click);
            // 
            // Herramientas
            // 
            this.Herramientas.Items.Add(this.btnGetSize);
            this.Herramientas.Items.Add(this.btnSetSize);
            this.Herramientas.Items.Add(this.btnActualizarToC);
            this.Herramientas.Label = "Herramientas";
            this.Herramientas.Name = "Herramientas";
            // 
            // btnGetSize
            // 
            this.btnGetSize.ImageName = "SizeAndPositionWindow";
            this.btnGetSize.Label = "Get Size";
            this.btnGetSize.Name = "btnGetSize";
            this.btnGetSize.OfficeImageId = "SizeAndPositionWindow";
            this.btnGetSize.ShowImage = true;
            this.btnGetSize.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnGetSize_Click);
            // 
            // btnSetSize
            // 
            this.btnSetSize.ImageName = "SizeAndPositionWindow1";
            this.btnSetSize.Label = "Set Size";
            this.btnSetSize.Name = "btnSetSize";
            this.btnSetSize.OfficeImageId = "SizeAndPositionWindow";
            this.btnSetSize.ShowImage = true;
            this.btnSetSize.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSetSize_Click);
            // 
            // btnActualizarToC
            // 
            this.btnActualizarToC.ImageName = "TableOfContentsGallery";
            this.btnActualizarToC.Label = "Actualizar ToC";
            this.btnActualizarToC.Name = "btnActualizarToC";
            this.btnActualizarToC.OfficeImageId = "TableOfContentsGallery";
            this.btnActualizarToC.ShowImage = true;
            this.btnActualizarToC.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnActualizarToC_Click);
            // 
            // group3
            // 
            this.group3.Items.Add(this.IAButton);
            this.group3.Items.Add(this.PegarRespuesta);
            this.group3.Label = "IA";
            this.group3.Name = "group3";
            // 
            // IAButton
            // 
            this.IAButton.ImageName = "PersonaMenuStartAudioConference";
            this.IAButton.Label = "IA Chat";
            this.IAButton.Name = "IAButton";
            this.IAButton.OfficeImageId = "PersonaMenuStartAudioConference";
            this.IAButton.ShowImage = true;
            this.IAButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.IAButton_Click);
            // 
            // PegarRespuesta
            // 
            this.PegarRespuesta.ImageName = "PasteDestinationStyle";
            this.PegarRespuesta.Label = "Pegar Respuesta";
            this.PegarRespuesta.Name = "PegarRespuesta";
            this.PegarRespuesta.OfficeImageId = "PasteDestinationStyle";
            this.PegarRespuesta.ShowImage = true;
            this.PegarRespuesta.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnInsertarRespuesta);
            // 
            // grpTablas
            // 
            this.grpTablas.DialogLauncher = ribbonDialogLauncherImpl1;
            this.grpTablas.Items.Add(this.drpPosition);
            this.grpTablas.Items.Add(this.editDistancia);
            this.grpTablas.Items.Add(this.btnTablas);
            this.grpTablas.Items.Add(this.checkRedondos);
            this.grpTablas.Label = "Tablas BIB";
            this.grpTablas.Name = "grpTablas";
            // 
            // drpPosition
            // 
            ribbonDropDownItemImpl1.Label = "Top Left";
            ribbonDropDownItemImpl1.OfficeImageId = "ControlsGalleryClassic";
            ribbonDropDownItemImpl1.Tag = "TopLeft";
            ribbonDropDownItemImpl2.Label = "Top Right";
            ribbonDropDownItemImpl2.OfficeImageId = "ControlsGalleryClassic";
            ribbonDropDownItemImpl2.Tag = "TopRight";
            ribbonDropDownItemImpl3.Label = "Bottom Left";
            ribbonDropDownItemImpl3.OfficeImageId = "ControlsGalleryClassic";
            ribbonDropDownItemImpl3.Tag = "BottomLeft";
            ribbonDropDownItemImpl4.Label = "Bottom Right";
            ribbonDropDownItemImpl4.OfficeImageId = "ControlsGalleryClassic";
            ribbonDropDownItemImpl4.Tag = "BottomRight";
            this.drpPosition.Items.Add(ribbonDropDownItemImpl1);
            this.drpPosition.Items.Add(ribbonDropDownItemImpl2);
            this.drpPosition.Items.Add(ribbonDropDownItemImpl3);
            this.drpPosition.Items.Add(ribbonDropDownItemImpl4);
            this.drpPosition.Label = "Posición";
            this.drpPosition.Name = "drpPosition";
            this.drpPosition.OfficeImageId = "ChoiceGroup";
            this.drpPosition.ShowImage = true;
            this.drpPosition.SelectionChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.drpPosition_SelectionChanged);
            // 
            // btnTablas
            // 
            this.btnTablas.ImageName = "TableAutoFormat";
            this.btnTablas.Items.Add(this.btn4x4);
            this.btnTablas.Items.Add(this.btn5x5);
            this.btnTablas.Items.Add(this.btn6x6);
            this.btnTablas.Items.Add(this.btn3x3);
            this.btnTablas.Label = "Tablas BIB";
            this.btnTablas.Name = "btnTablas";
            this.btnTablas.OfficeImageId = "TableAutoFormat";
            this.btnTablas.ShowImage = true;
            // 
            // btn4x4
            // 
            this.btn4x4.ImageName = "AutoAlignAndSpace";
            this.btn4x4.Label = "4x4";
            this.btn4x4.Name = "btn4x4";
            this.btn4x4.OfficeImageId = "AutoAlignAndSpace";
            this.btn4x4.ShowImage = true;
            this.btn4x4.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn4x4_Click);
            // 
            // btn5x5
            // 
            this.btn5x5.ImageName = "AutoAlignAndSpace";
            this.btn5x5.Label = "5x5";
            this.btn5x5.Name = "btn5x5";
            this.btn5x5.OfficeImageId = "AutoAlignAndSpace";
            this.btn5x5.ShowImage = true;
            this.btn5x5.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn5x5_Click);
            // 
            // btn6x6
            // 
            this.btn6x6.ImageName = "AutoAlignAndSpace";
            this.btn6x6.Label = "6x6";
            this.btn6x6.Name = "btn6x6";
            this.btn6x6.OfficeImageId = "AutoAlignAndSpace";
            this.btn6x6.ShowImage = true;
            this.btn6x6.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn6x6_Click);
            // 
            // btn3x3
            // 
            this.btn3x3.ImageName = "AutoAlignAndSpace";
            this.btn3x3.Label = "3x3";
            this.btn3x3.Name = "btn3x3";
            this.btn3x3.OfficeImageId = "AutoAlignAndSpace";
            this.btn3x3.ShowImage = true;
            this.btn3x3.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn3x3_Click);
            // 
            // checkRedondos
            // 
            this.checkRedondos.Label = "Bordes Redondos";
            this.checkRedondos.Name = "checkRedondos";
            this.checkRedondos.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.checkRedondos_Click);
            // 
            // editDistancia
            // 
            this.editDistancia.ImageName = "GroupField";
            this.editDistancia.Label = "Distancia";
            this.editDistancia.MaxLength = 3;
            this.editDistancia.Name = "editDistancia";
            this.editDistancia.OfficeImageId = "GroupField";
            this.editDistancia.ShowImage = true;
            this.editDistancia.Text = "0.3";
            this.editDistancia.TextChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.editDistancia_TextChanged);
            // 
            // RibbonBIB
            // 
            this.Name = "RibbonBIB";
            this.RibbonType = "Microsoft.PowerPoint.Presentation";
            this.Tabs.Add(this.tabBIB);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.RibbonBIB_Load);
            this.tabBIB.ResumeLayout(false);
            this.tabBIB.PerformLayout();
            this.grpComites.ResumeLayout(false);
            this.grpComites.PerformLayout();
            this.Herramientas.ResumeLayout(false);
            this.Herramientas.PerformLayout();
            this.group3.ResumeLayout(false);
            this.group3.PerformLayout();
            this.grpTablas.ResumeLayout(false);
            this.grpTablas.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tabBIB;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpComites;
        internal Microsoft.Office.Tools.Ribbon.RibbonSplitButton sbComites;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnNE;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnCred;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnApetito;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnBiblioteca;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnWebPanel;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Herramientas;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnGetSize;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSetSize;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnActualizarToC;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group3;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton IAButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton PegarRespuesta;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpTablas;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu btnTablas;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn4x4;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn5x5;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox checkRedondos;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn6x6;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn3x3;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDown drpPosition;
        internal Microsoft.Office.Tools.Ribbon.RibbonEditBox editDistancia;
    }

    partial class ThisRibbonCollection
    {
        internal RibbonBIB RibbonBIB => this.GetRibbon<RibbonBIB>();
    }
}

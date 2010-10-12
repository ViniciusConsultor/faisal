<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PurchaseWarehouseForm
  Inherits QuickBaseForms.ParentToolbarForm

  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    If disposing AndAlso components IsNot Nothing Then
      components.Dispose()
    End If
    MyBase.Dispose(disposing)
  End Sub

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
    Dim DefaultFocusIndicatorRenderer1 As FarPoint.Win.Spread.DefaultFocusIndicatorRenderer = New FarPoint.Win.Spread.DefaultFocusIndicatorRenderer
    Dim DefaultScrollBarRenderer1 As FarPoint.Win.Spread.DefaultScrollBarRenderer = New FarPoint.Win.Spread.DefaultScrollBarRenderer
    Dim DefaultScrollBarRenderer2 As FarPoint.Win.Spread.DefaultScrollBarRenderer = New FarPoint.Win.Spread.DefaultScrollBarRenderer
    Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
    Dim DateButton1 As Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton = New Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton
    Dim DateButton2 As Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton = New Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton
    Dim DefaultScrollBarRenderer3 As FarPoint.Win.Spread.DefaultScrollBarRenderer = New FarPoint.Win.Spread.DefaultScrollBarRenderer
    Dim DefaultScrollBarRenderer4 As FarPoint.Win.Spread.DefaultScrollBarRenderer = New FarPoint.Win.Spread.DefaultScrollBarRenderer
    Me.StockBarSpread = New QuickBusinessControls.ItemSummaryBar
    Me.StockBarSpread_Sheet1 = New QuickBusinessControls.ItemSpreadView
    Me.Quick_Label4 = New QuickControls.Quick_Label
    Me.TotalQtyLabel = New QuickControls.Quick_Label
    Me.Quick_Label2 = New QuickControls.Quick_Label
    Me.TotalAmountLabel = New QuickControls.Quick_Label
    Me.grpMasterInformation = New QuickControls.Quick_GroupBox
    Me.PartyLabel = New QuickControls.Quick_Label
    Me.PartyComboBox = New QuickBusinessControls.PartyComboBox
    Me.RemarksLabel = New QuickControls.Quick_Label
    Me.RemarksTextBox = New QuickControls.Quick_TextBox
    Me.uccSaleDate = New Infragistics.Win.UltraWinSchedule.UltraCalendarCombo
    Me.PurchaseDateLabel = New QuickControls.Quick_Label
    Me.PurchaseNoLabel = New QuickControls.Quick_Label
    Me.PurchaseNoTextBox = New QuickControls.Quick_TextBox
    Me.PurchaseDetailSpread = New QuickBusinessControls.ItemSpread
    Me.PurchaseDetailSpread_Sheet1 = New QuickBusinessControls.ItemSpreadView
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.StockBarSpread, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.StockBarSpread_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.grpMasterInformation.SuspendLayout()
    CType(Me.PartyComboBox, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.uccSaleDate, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.PurchaseDetailSpread, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.PurchaseDetailSpread_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'StockBarSpread
    '
    Me.StockBarSpread.AccessibleDescription = "StockBar1"
    Me.StockBarSpread.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.StockBarSpread.FocusRenderer = DefaultFocusIndicatorRenderer1
    Me.StockBarSpread.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
    Me.StockBarSpread.HorizontalScrollBar.Name = ""
    Me.StockBarSpread.HorizontalScrollBar.Renderer = DefaultScrollBarRenderer1
    Me.StockBarSpread.HorizontalScrollBar.TabIndex = 2
    Me.StockBarSpread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
    Me.StockBarSpread.Location = New System.Drawing.Point(8, 132)
    Me.StockBarSpread.Name = "StockBarSpread"
    Me.StockBarSpread.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.StockBarSpread_Sheet1})
    Me.StockBarSpread.Size = New System.Drawing.Size(784, 84)
    Me.StockBarSpread.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Classic
    Me.StockBarSpread.TabIndex = 1
    Me.StockBarSpread.TabStop = False
    Me.StockBarSpread.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
    Me.StockBarSpread.VerticalScrollBar.Name = ""
    Me.StockBarSpread.VerticalScrollBar.Renderer = DefaultScrollBarRenderer2
    Me.StockBarSpread.VerticalScrollBar.TabIndex = 3
    Me.StockBarSpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never
    Me.StockBarSpread.VisualStyles = FarPoint.Win.VisualStyles.Off
    '
    'StockBarSpread_Sheet1
    '
    Me.StockBarSpread_Sheet1.Reset()
    Me.StockBarSpread_Sheet1.SheetName = "Sheet1"
    'Formulas and custom names must be loaded with R1C1 reference style
    Me.StockBarSpread_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
    Me.StockBarSpread_Sheet1.ColumnHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
    Me.StockBarSpread_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault"
    Me.StockBarSpread_Sheet1.RowHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
    Me.StockBarSpread_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderDefault"
    Me.StockBarSpread_Sheet1.SheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
    Me.StockBarSpread_Sheet1.SheetCornerStyle.Parent = "CornerDefault"
    Me.StockBarSpread_Sheet1.ShowTotalsRow = True
    Me.StockBarSpread_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
    '
    'Quick_Label4
    '
    Me.Quick_Label4.AllowClearValue = False
    Me.Quick_Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Quick_Label4.AutoSize = True
    Me.Quick_Label4.DefaultValue = ""
    Me.Quick_Label4.Location = New System.Drawing.Point(324, 440)
    Me.Quick_Label4.Name = "Quick_Label4"
    Me.Quick_Label4.Size = New System.Drawing.Size(49, 13)
    Me.Quick_Label4.TabIndex = 3
    Me.Quick_Label4.Text = "Quantity:"
    '
    'TotalQtyLabel
    '
    Me.TotalQtyLabel.AllowClearValue = False
    Me.TotalQtyLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TotalQtyLabel.DefaultValue = ""
    Me.TotalQtyLabel.Font = New System.Drawing.Font("Courier New", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.TotalQtyLabel.Location = New System.Drawing.Point(376, 432)
    Me.TotalQtyLabel.Name = "TotalQtyLabel"
    Me.TotalQtyLabel.Size = New System.Drawing.Size(144, 28)
    Me.TotalQtyLabel.TabIndex = 4
    Me.TotalQtyLabel.Text = "0"
    Me.TotalQtyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'Quick_Label2
    '
    Me.Quick_Label2.AllowClearValue = False
    Me.Quick_Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Quick_Label2.AutoSize = True
    Me.Quick_Label2.DefaultValue = ""
    Me.Quick_Label2.Location = New System.Drawing.Point(524, 440)
    Me.Quick_Label2.Name = "Quick_Label2"
    Me.Quick_Label2.Size = New System.Drawing.Size(46, 13)
    Me.Quick_Label2.TabIndex = 5
    Me.Quick_Label2.Text = "Amount:"
    '
    'TotalAmountLabel
    '
    Me.TotalAmountLabel.AllowClearValue = False
    Me.TotalAmountLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TotalAmountLabel.DefaultValue = ""
    Me.TotalAmountLabel.Font = New System.Drawing.Font("Courier New", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.TotalAmountLabel.Location = New System.Drawing.Point(576, 432)
    Me.TotalAmountLabel.Name = "TotalAmountLabel"
    Me.TotalAmountLabel.Size = New System.Drawing.Size(214, 28)
    Me.TotalAmountLabel.TabIndex = 6
    Me.TotalAmountLabel.Text = "0"
    Me.TotalAmountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '
    'grpMasterInformation
    '
    Me.grpMasterInformation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpMasterInformation.Controls.Add(Me.PartyLabel)
    Me.grpMasterInformation.Controls.Add(Me.PartyComboBox)
    Me.grpMasterInformation.Controls.Add(Me.RemarksLabel)
    Me.grpMasterInformation.Controls.Add(Me.RemarksTextBox)
    Me.grpMasterInformation.Controls.Add(Me.uccSaleDate)
    Me.grpMasterInformation.Controls.Add(Me.PurchaseDateLabel)
    Me.grpMasterInformation.Controls.Add(Me.PurchaseNoLabel)
    Me.grpMasterInformation.Controls.Add(Me.PurchaseNoTextBox)
    Me.grpMasterInformation.Location = New System.Drawing.Point(8, 48)
    Me.grpMasterInformation.Name = "grpMasterInformation"
    Me.grpMasterInformation.Size = New System.Drawing.Size(784, 76)
    Me.grpMasterInformation.TabIndex = 0
    Me.grpMasterInformation.TabStop = False
    '
    'PartyLabel
    '
    Me.PartyLabel.AllowClearValue = False
    Me.PartyLabel.AutoSize = True
    Me.PartyLabel.DefaultValue = ""
    Me.PartyLabel.Location = New System.Drawing.Point(304, 24)
    Me.PartyLabel.Name = "PartyLabel"
    Me.PartyLabel.Size = New System.Drawing.Size(34, 13)
    Me.PartyLabel.TabIndex = 6
    Me.PartyLabel.Text = "Party:"
    '
    'PartyComboBox
    '
    Me.PartyComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.PartyComboBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
    Appearance1.BackColor = System.Drawing.SystemColors.Window
    Appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption
    Me.PartyComboBox.DisplayLayout.Appearance = Appearance1
    Me.PartyComboBox.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
    Me.PartyComboBox.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
    Appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder
    Appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark
    Appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
    Appearance2.BorderColor = System.Drawing.SystemColors.Window
    Me.PartyComboBox.DisplayLayout.GroupByBox.Appearance = Appearance2
    Appearance3.ForeColor = System.Drawing.SystemColors.GrayText
    Me.PartyComboBox.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance3
    Me.PartyComboBox.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
    Appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight
    Appearance4.BackColor2 = System.Drawing.SystemColors.Control
    Appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
    Appearance4.ForeColor = System.Drawing.SystemColors.GrayText
    Me.PartyComboBox.DisplayLayout.GroupByBox.PromptAppearance = Appearance4
    Me.PartyComboBox.DisplayLayout.MaxColScrollRegions = 1
    Me.PartyComboBox.DisplayLayout.MaxRowScrollRegions = 1
    Appearance5.BackColor = System.Drawing.SystemColors.Window
    Appearance5.ForeColor = System.Drawing.SystemColors.ControlText
    Me.PartyComboBox.DisplayLayout.Override.ActiveCellAppearance = Appearance5
    Appearance6.BackColor = System.Drawing.SystemColors.Highlight
    Appearance6.ForeColor = System.Drawing.SystemColors.HighlightText
    Me.PartyComboBox.DisplayLayout.Override.ActiveRowAppearance = Appearance6
    Me.PartyComboBox.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
    Me.PartyComboBox.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
    Appearance7.BackColor = System.Drawing.SystemColors.Window
    Me.PartyComboBox.DisplayLayout.Override.CardAreaAppearance = Appearance7
    Appearance8.BorderColor = System.Drawing.Color.Silver
    Appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
    Me.PartyComboBox.DisplayLayout.Override.CellAppearance = Appearance8
    Me.PartyComboBox.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
    Me.PartyComboBox.DisplayLayout.Override.CellPadding = 0
    Appearance9.BackColor = System.Drawing.SystemColors.Control
    Appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark
    Appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
    Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
    Appearance9.BorderColor = System.Drawing.SystemColors.Window
    Me.PartyComboBox.DisplayLayout.Override.GroupByRowAppearance = Appearance9
    Appearance10.TextHAlignAsString = "Left"
    Me.PartyComboBox.DisplayLayout.Override.HeaderAppearance = Appearance10
    Me.PartyComboBox.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
    Me.PartyComboBox.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
    Appearance11.BackColor = System.Drawing.SystemColors.Window
    Appearance11.BorderColor = System.Drawing.Color.Silver
    Me.PartyComboBox.DisplayLayout.Override.RowAppearance = Appearance11
    Me.PartyComboBox.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
    Appearance12.BackColor = System.Drawing.SystemColors.ControlLight
    Me.PartyComboBox.DisplayLayout.Override.TemplateAddRowAppearance = Appearance12
    Me.PartyComboBox.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
    Me.PartyComboBox.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
    Me.PartyComboBox.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
    Me.PartyComboBox.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.[Default]
    Me.PartyComboBox.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList
    Me.PartyComboBox.DropDownWidth = 432
    Me.PartyComboBox.EntityType = QuickLibrary.Constants.EntityTypes.SalesMan
    Me.PartyComboBox.EntryMode = QuickControls.Quick_UltraComboBox.EntryModes.SelectionFromList
    Me.PartyComboBox.IsMandatory = False
    Me.PartyComboBox.IsReadonlyForExistingRecord = False
    Me.PartyComboBox.IsReadonlyForNewRecord = False
    Me.PartyComboBox.Location = New System.Drawing.Point(344, 20)
    Me.PartyComboBox.Name = "PartyComboBox"
    Me.PartyComboBox.PartyCode = ""
    Me.PartyComboBox.PartyID = 0
    Me.PartyComboBox.Size = New System.Drawing.Size(432, 22)
    Me.PartyComboBox.TabIndex = 7
    '
    'RemarksLabel
    '
    Me.RemarksLabel.AllowClearValue = False
    Me.RemarksLabel.AutoSize = True
    Me.RemarksLabel.DefaultValue = ""
    Me.RemarksLabel.Location = New System.Drawing.Point(8, 52)
    Me.RemarksLabel.Name = "RemarksLabel"
    Me.RemarksLabel.Size = New System.Drawing.Size(52, 13)
    Me.RemarksLabel.TabIndex = 16
    Me.RemarksLabel.Text = "Remarks:"
    '
    'RemarksTextBox
    '
    Me.RemarksTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.RemarksTextBox.DefaultValue = ""
    Me.RemarksTextBox.IntegerNumber = 0
    Me.RemarksTextBox.IsMandatory = False
    Me.RemarksTextBox.IsReadonlyForExistingRecord = False
    Me.RemarksTextBox.IsReadonlyForNewRecord = False
    Me.RemarksTextBox.Location = New System.Drawing.Point(72, 48)
    Me.RemarksTextBox.Name = "RemarksTextBox"
    Me.RemarksTextBox.PercentNumber = 0
    Me.RemarksTextBox.Size = New System.Drawing.Size(704, 20)
    Me.RemarksTextBox.TabIndex = 17
    Me.RemarksTextBox.Text = "0"
    Me.RemarksTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'uccSaleDate
    '
    Me.uccSaleDate.BackColor = System.Drawing.SystemColors.Window
    Me.uccSaleDate.DateButtons.Add(DateButton1)
    Me.uccSaleDate.DateButtons.Add(DateButton2)
    Me.uccSaleDate.Location = New System.Drawing.Point(208, 19)
    Me.uccSaleDate.Name = "uccSaleDate"
    Me.uccSaleDate.NonAutoSizeHeight = 21
    Me.uccSaleDate.Size = New System.Drawing.Size(92, 21)
    Me.uccSaleDate.TabIndex = 3
    Me.uccSaleDate.Value = New Date(2008, 1, 27, 0, 0, 0, 0)
    '
    'PurchaseDateLabel
    '
    Me.PurchaseDateLabel.AllowClearValue = False
    Me.PurchaseDateLabel.AutoSize = True
    Me.PurchaseDateLabel.DefaultValue = ""
    Me.PurchaseDateLabel.Location = New System.Drawing.Point(168, 23)
    Me.PurchaseDateLabel.Name = "PurchaseDateLabel"
    Me.PurchaseDateLabel.Size = New System.Drawing.Size(33, 13)
    Me.PurchaseDateLabel.TabIndex = 2
    Me.PurchaseDateLabel.Text = "Date:"
    '
    'PurchaseNoLabel
    '
    Me.PurchaseNoLabel.AllowClearValue = False
    Me.PurchaseNoLabel.AutoSize = True
    Me.PurchaseNoLabel.DefaultValue = ""
    Me.PurchaseNoLabel.Location = New System.Drawing.Point(8, 23)
    Me.PurchaseNoLabel.Name = "PurchaseNoLabel"
    Me.PurchaseNoLabel.Size = New System.Drawing.Size(60, 13)
    Me.PurchaseNoLabel.TabIndex = 0
    Me.PurchaseNoLabel.Text = "Stock In #:"
    '
    'PurchaseNoTextBox
    '
    Me.PurchaseNoTextBox.DefaultValue = ""
    Me.PurchaseNoTextBox.IntegerNumber = 0
    Me.PurchaseNoTextBox.IsMandatory = False
    Me.PurchaseNoTextBox.IsReadonlyForExistingRecord = False
    Me.PurchaseNoTextBox.IsReadonlyForNewRecord = False
    Me.PurchaseNoTextBox.Location = New System.Drawing.Point(72, 19)
    Me.PurchaseNoTextBox.Name = "PurchaseNoTextBox"
    Me.PurchaseNoTextBox.PercentNumber = 0
    Me.PurchaseNoTextBox.ReadOnly = True
    Me.PurchaseNoTextBox.Size = New System.Drawing.Size(92, 20)
    Me.PurchaseNoTextBox.TabIndex = 1
    Me.PurchaseNoTextBox.TabStop = False
    Me.PurchaseNoTextBox.Text = "0"
    Me.PurchaseNoTextBox.TextBoxType = QuickControls.Quick_TextBox.TextBoxTypes.IntegerNumberOrPercentageNumber
    '
    'PurchaseDetailSpread
    '
    Me.PurchaseDetailSpread.AccessibleDescription = "Quick_Spread"
    Me.PurchaseDetailSpread.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.PurchaseDetailSpread.AutoNewRow = True
    Me.PurchaseDetailSpread.BackColor = System.Drawing.SystemColors.Control
    Me.PurchaseDetailSpread.EditModePermanent = True
    Me.PurchaseDetailSpread.EditModeReplace = True
    Me.PurchaseDetailSpread.FocusRenderer = DefaultFocusIndicatorRenderer1
    Me.PurchaseDetailSpread.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
    Me.PurchaseDetailSpread.HorizontalScrollBar.Name = ""
    Me.PurchaseDetailSpread.HorizontalScrollBar.Renderer = DefaultScrollBarRenderer3
    Me.PurchaseDetailSpread.HorizontalScrollBar.TabIndex = 2
    Me.PurchaseDetailSpread.ItemCodeFirstColumnIndex = -1
    Me.PurchaseDetailSpread.ItemDescColumnIndex = -1
    Me.PurchaseDetailSpread.ItemIDColumnIndex = -1
    Me.PurchaseDetailSpread.ItemQtyFirstColumnIndex = -1
    Me.PurchaseDetailSpread.ItemRateFirstColumnIndex = -1
    Me.PurchaseDetailSpread.ItemSheetView = Nothing
    Me.PurchaseDetailSpread.ItemSizeColumnIndex = -1
    Me.PurchaseDetailSpread.ItemSummaryBarObject = Nothing
    Me.PurchaseDetailSpread.Location = New System.Drawing.Point(8, 224)
    Me.PurchaseDetailSpread.LoginInfoObject = Nothing
    Me.PurchaseDetailSpread.Name = "PurchaseDetailSpread"
    Me.PurchaseDetailSpread.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.PurchaseDetailSpread_Sheet1})
    Me.PurchaseDetailSpread.Size = New System.Drawing.Size(783, 200)
    Me.PurchaseDetailSpread.SizesHorizontal = False
    Me.PurchaseDetailSpread.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Classic
    Me.PurchaseDetailSpread.TabIndex = 2
    Me.PurchaseDetailSpread.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
    Me.PurchaseDetailSpread.VerticalScrollBar.Name = ""
    Me.PurchaseDetailSpread.VerticalScrollBar.Renderer = DefaultScrollBarRenderer4
    Me.PurchaseDetailSpread.VerticalScrollBar.TabIndex = 3
    Me.PurchaseDetailSpread.VisualStyles = FarPoint.Win.VisualStyles.Off
    '
    'PurchaseDetailSpread_Sheet1
    '
    Me.PurchaseDetailSpread_Sheet1.Reset()
    Me.PurchaseDetailSpread_Sheet1.SheetName = "Sheet1"
    'Formulas and custom names must be loaded with R1C1 reference style
    Me.PurchaseDetailSpread_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
    PurchaseDetailSpread_Sheet1.ColumnCount = 256
    PurchaseDetailSpread_Sheet1.RowCount = 1
    Me.PurchaseDetailSpread_Sheet1.ColumnHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
    Me.PurchaseDetailSpread_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault"
    Me.PurchaseDetailSpread_Sheet1.RowHeader.Columns.Default.Resizable = False
    Me.PurchaseDetailSpread_Sheet1.RowHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.Red
    Me.PurchaseDetailSpread_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderDefault"
    Me.PurchaseDetailSpread_Sheet1.SheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red
    Me.PurchaseDetailSpread_Sheet1.SheetCornerStyle.Parent = "CornerDefault"
    Me.PurchaseDetailSpread_Sheet1.ShowTotalsRow = True
    Me.PurchaseDetailSpread_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
    '
    'PurchaseWarehouseForm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(799, 486)
    Me.Controls.Add(Me.StockBarSpread)
    Me.Controls.Add(Me.Quick_Label4)
    Me.Controls.Add(Me.TotalQtyLabel)
    Me.Controls.Add(Me.Quick_Label2)
    Me.Controls.Add(Me.TotalAmountLabel)
    Me.Controls.Add(Me.grpMasterInformation)
    Me.Controls.Add(Me.PurchaseDetailSpread)
    Me.KeyPreview = True
    Me.Name = "PurchaseWarehouseForm"
    Me.Text = "Stock In"
    Me.Controls.SetChildIndex(Me.PurchaseDetailSpread, 0)
    Me.Controls.SetChildIndex(Me.grpMasterInformation, 0)
    Me.Controls.SetChildIndex(Me.TotalAmountLabel, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label2, 0)
    Me.Controls.SetChildIndex(Me.TotalQtyLabel, 0)
    Me.Controls.SetChildIndex(Me.Quick_Label4, 0)
    Me.Controls.SetChildIndex(Me.StockBarSpread, 0)
    CType(Me.FormDataSet, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.StockBarSpread, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.StockBarSpread_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.grpMasterInformation.ResumeLayout(False)
    Me.grpMasterInformation.PerformLayout()
    CType(Me.PartyComboBox, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.uccSaleDate, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.PurchaseDetailSpread, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.PurchaseDetailSpread_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents grpMasterInformation As QuickControls.Quick_GroupBox
  Friend WithEvents RemarksLabel As QuickControls.Quick_Label
  Friend WithEvents RemarksTextBox As QuickControls.Quick_TextBox
  Friend WithEvents uccSaleDate As Infragistics.Win.UltraWinSchedule.UltraCalendarCombo
  Friend WithEvents PurchaseDateLabel As QuickControls.Quick_Label
  Friend WithEvents PurchaseNoLabel As QuickControls.Quick_Label
  Friend WithEvents PurchaseNoTextBox As QuickControls.Quick_TextBox
  Friend WithEvents PurchaseDetailSpread_Sheet1 As QuickBusinessControls.ItemSpreadView
  Friend WithEvents PartyLabel As QuickControls.Quick_Label
  Friend WithEvents PartyComboBox As QuickBusinessControls.PartyComboBox
  Friend WithEvents TotalAmountLabel As QuickControls.Quick_Label
  Friend WithEvents Quick_Label2 As QuickControls.Quick_Label
  Friend WithEvents Quick_Label4 As QuickControls.Quick_Label
  Friend WithEvents TotalQtyLabel As QuickControls.Quick_Label
  'Friend WithEvents StockBar1 As QuickBusinessControls.ItemSummaryBar
  Friend WithEvents StockBarSpread_Sheet1 As QuickBusinessControls.ItemSpreadView
  Friend WithEvents PurchaseDetailSpread As QuickBusinessControls.ItemSpread
  Friend WithEvents StockBarSpread As QuickBusinessControls.ItemSummaryBar
End Class

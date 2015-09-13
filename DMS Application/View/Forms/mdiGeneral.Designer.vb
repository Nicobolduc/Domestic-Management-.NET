<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class mdiGeneral
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(mdiGeneral))
        Me.mnuMain = New System.Windows.Forms.MenuStrip()
        Me.mnUsager = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuiOptions = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuGestion = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuiBudget = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuiGrocery = New System.Windows.Forms.ToolStripMenuItem()
        Me.RepasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFinance = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuiExpense = New System.Windows.Forms.ToolStripMenuItem()
        Me.TypeDeDépenseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.RevenuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuProduit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuiProducts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuiProductType = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuiProductCategory = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuiBrand = New System.Windows.Forms.ToolStripMenuItem()
        Me.EntitéeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuiCompany = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuWindows = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTests = New System.Windows.Forms.ToolStripMenuItem()
        Me.statusBar = New System.Windows.Forms.StatusStrip()
        Me.lblStatusUser = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblStatusBD = New System.Windows.Forms.ToolStripStatusLabel()
        Me.mnuMain.SuspendLayout()
        Me.statusBar.SuspendLayout()
        Me.SuspendLayout()
        '
        'mnuMain
        '
        Me.mnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnUsager, Me.mnuGestion, Me.mnuFinance, Me.mnuProduit, Me.EntitéeToolStripMenuItem, Me.mnuWindows, Me.mnuTests})
        Me.mnuMain.Location = New System.Drawing.Point(0, 0)
        Me.mnuMain.MdiWindowListItem = Me.mnuWindows
        Me.mnuMain.Name = "mnuMain"
        Me.mnuMain.Size = New System.Drawing.Size(1008, 24)
        Me.mnuMain.TabIndex = 0
        Me.mnuMain.Text = "mnuMain"
        '
        'mnUsager
        '
        Me.mnUsager.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuiOptions})
        Me.mnUsager.Name = "mnUsager"
        Me.mnUsager.Size = New System.Drawing.Size(55, 20)
        Me.mnUsager.Text = "Usager"
        '
        'mnuiOptions
        '
        Me.mnuiOptions.Name = "mnuiOptions"
        Me.mnuiOptions.Size = New System.Drawing.Size(116, 22)
        Me.mnuiOptions.Text = "Options"
        '
        'mnuGestion
        '
        Me.mnuGestion.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuiBudget, Me.mnuiGrocery, Me.RepasToolStripMenuItem})
        Me.mnuGestion.Name = "mnuGestion"
        Me.mnuGestion.Size = New System.Drawing.Size(59, 20)
        Me.mnuGestion.Text = "Gestion"
        '
        'mnuiBudget
        '
        Me.mnuiBudget.Name = "mnuiBudget"
        Me.mnuiBudget.Size = New System.Drawing.Size(115, 22)
        Me.mnuiBudget.Text = "Budget"
        '
        'mnuiGrocery
        '
        Me.mnuiGrocery.Name = "mnuiGrocery"
        Me.mnuiGrocery.Size = New System.Drawing.Size(115, 22)
        Me.mnuiGrocery.Text = "Épicerie"
        '
        'RepasToolStripMenuItem
        '
        Me.RepasToolStripMenuItem.Name = "RepasToolStripMenuItem"
        Me.RepasToolStripMenuItem.Size = New System.Drawing.Size(115, 22)
        Me.RepasToolStripMenuItem.Text = "Repas"
        '
        'mnuFinance
        '
        Me.mnuFinance.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuiExpense, Me.TypeDeDépenseToolStripMenuItem, Me.ToolStripSeparator1, Me.RevenuToolStripMenuItem})
        Me.mnuFinance.Name = "mnuFinance"
        Me.mnuFinance.Size = New System.Drawing.Size(60, 20)
        Me.mnuFinance.Text = "Finance"
        '
        'mnuiExpense
        '
        Me.mnuiExpense.Name = "mnuiExpense"
        Me.mnuiExpense.Size = New System.Drawing.Size(163, 22)
        Me.mnuiExpense.Text = "Dépense"
        '
        'TypeDeDépenseToolStripMenuItem
        '
        Me.TypeDeDépenseToolStripMenuItem.Name = "TypeDeDépenseToolStripMenuItem"
        Me.TypeDeDépenseToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.TypeDeDépenseToolStripMenuItem.Text = "Type de dépense"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(160, 6)
        '
        'RevenuToolStripMenuItem
        '
        Me.RevenuToolStripMenuItem.Name = "RevenuToolStripMenuItem"
        Me.RevenuToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.RevenuToolStripMenuItem.Text = "Revenu"
        '
        'mnuProduit
        '
        Me.mnuProduit.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuiProducts, Me.mnuiProductType, Me.mnuiProductCategory, Me.mnuiBrand})
        Me.mnuProduit.Name = "mnuProduit"
        Me.mnuProduit.Size = New System.Drawing.Size(58, 20)
        Me.mnuProduit.Text = "Produit"
        '
        'mnuiProducts
        '
        Me.mnuiProducts.Name = "mnuiProducts"
        Me.mnuiProducts.Size = New System.Drawing.Size(179, 22)
        Me.mnuiProducts.Text = "Produit"
        '
        'mnuiProductType
        '
        Me.mnuiProductType.Name = "mnuiProductType"
        Me.mnuiProductType.Size = New System.Drawing.Size(179, 22)
        Me.mnuiProductType.Text = "Type de produit"
        '
        'mnuiProductCategory
        '
        Me.mnuiProductCategory.Name = "mnuiProductCategory"
        Me.mnuiProductCategory.Size = New System.Drawing.Size(179, 22)
        Me.mnuiProductCategory.Text = "Catégorie de poduit"
        '
        'mnuiBrand
        '
        Me.mnuiBrand.Name = "mnuiBrand"
        Me.mnuiBrand.Size = New System.Drawing.Size(179, 22)
        Me.mnuiBrand.Text = "Marque de produit"
        '
        'EntitéeToolStripMenuItem
        '
        Me.EntitéeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuiCompany})
        Me.EntitéeToolStripMenuItem.Name = "EntitéeToolStripMenuItem"
        Me.EntitéeToolStripMenuItem.Size = New System.Drawing.Size(55, 20)
        Me.EntitéeToolStripMenuItem.Text = "Entitée"
        '
        'mnuiCompany
        '
        Me.mnuiCompany.Name = "mnuiCompany"
        Me.mnuiCompany.Size = New System.Drawing.Size(136, 22)
        Me.mnuiCompany.Text = "Compagnie"
        '
        'mnuWindows
        '
        Me.mnuWindows.Name = "mnuWindows"
        Me.mnuWindows.Size = New System.Drawing.Size(63, 20)
        Me.mnuWindows.Text = "Fenêtres"
        '
        'mnuTests
        '
        Me.mnuTests.Name = "mnuTests"
        Me.mnuTests.Size = New System.Drawing.Size(46, 20)
        Me.mnuTests.Text = "Tests"
        '
        'statusBar
        '
        Me.statusBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatusUser, Me.lblStatusBD})
        Me.statusBar.Location = New System.Drawing.Point(0, 705)
        Me.statusBar.Name = "statusBar"
        Me.statusBar.Size = New System.Drawing.Size(1008, 25)
        Me.statusBar.TabIndex = 2
        Me.statusBar.Text = "StatusStrip1"
        '
        'lblStatusUser
        '
        Me.lblStatusUser.AutoSize = False
        Me.lblStatusUser.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusUser.Name = "lblStatusUser"
        Me.lblStatusUser.Size = New System.Drawing.Size(300, 20)
        Me.lblStatusUser.Text = "Utilisateur: "
        Me.lblStatusUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblStatusBD
        '
        Me.lblStatusBD.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusBD.Name = "lblStatusBD"
        Me.lblStatusBD.Size = New System.Drawing.Size(30, 20)
        Me.lblStatusBD.Text = "BD:"
        '
        'mdiGeneral
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 730)
        Me.Controls.Add(Me.statusBar)
        Me.Controls.Add(Me.mnuMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.mnuMain
        Me.Name = "mdiGeneral"
        Me.Text = "Gestion des taches domestiques"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.mnuMain.ResumeLayout(False)
        Me.mnuMain.PerformLayout()
        Me.statusBar.ResumeLayout(False)
        Me.statusBar.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents mnuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuGestion As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuiBudget As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents statusBar As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatusUser As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblStatusBD As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents mnuFinance As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuiExpense As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnUsager As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuProduit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuiProducts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuiOptions As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuiProductType As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuiProductCategory As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuiBrand As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuWindows As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EntitéeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuiCompany As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuiGrocery As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTests As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TypeDeDépenseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents RevenuToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RepasToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class

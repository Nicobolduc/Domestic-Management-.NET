Option Strict Off
Option Explicit On
Imports System.Collections.Generic
<System.Runtime.InteropServices.ProgId("clsTTVSFlex_NET.clsTTVSFlex_Net")> Public Class clsTTVSFlex_Net
    Implements IDisposable

    Private Const mstrCLASS_NAME As String = "clsTTVSFlex_Net"

    Private WithEvents mobjGRID As TT3DLL.ctlTTGrid
    Private mcTTAPP As clsTTAPP
    Private WithEvents mcmdADD As System.Windows.Forms.Button
    Private WithEvents mcmdDelete As System.Windows.Forms.Button

    Private Const mintPrinterNotAvail_msg As Integer = 900439

    'UPGRADE_NOTE: mintKeyAddLine was changed from a Constant to a Variable. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="C54B49D7-5804-4D48-834B-B3D81E4C2F13"'
    Private mintKeyAddLine As Short = System.Windows.Forms.Keys.F2
    'UPGRADE_NOTE: mintKeyDelLine was changed from a Constant to a Variable. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="C54B49D7-5804-4D48-834B-B3D81E4C2F13"'
    Private mintKeyDelLine As Short = System.Windows.Forms.Keys.F3

    Public Event ValideGridRules(ByRef Valide As Boolean)
    Public Event SaveGridData(ByRef Valide As Boolean)
    Public Event BeforeClickAdd(ByRef Cancel As Boolean)
    Public Event BeforeClickDelete(ByRef Cancel As Boolean)
    Public Event ClickAdd()
    Public Event ClickDelete()
    Public Event SetDisplay()

    Private Const mintNOMBRE_COLS_tab As Short = 4
    Private Const mintTAB_Caption As Short = 1
    Private Const mintTAB_Champ As Short = 2
    Private Const mintTAB_Type As Short = 3
    Private Const mintTAB_Width As Short = 4

    Public Const mintMaxLargeurGrille_Print As Integer = 1025

    Public blnInsertUnderCurrentRow As Boolean
    Public mstrReportName As String = gvbnullstring
    Private mintActionCol As Short
    Private mblnAllowPrint As Boolean
    Private mblnAllowEditUpdate As Boolean
    Private mrecRecord As ADODB.Recordset
    Private mDataTable As System.Data.DataTable
    Private mDataView As System.Data.DataView
    Private mstrFilter As String = gvbnullstring
    Private mvarColonnes As Object
    Private mblnDisconnectedDataSource As Boolean = False
    Private mblnIsInCodeGrid As Boolean
    Private mChangedColumnSize As System.Collections.Generic.Dictionary(Of Integer, Integer)

    Public mfrmFormParent As Form

    Public mdictControlLieeGrille As Dictionary(Of Integer, Control)

    Public Const TTCM_GRID_BACK_COLOR_ALT As Integer = &HF3F3F3

    ''' <summary>
    ''' Définit si l'on redomensionne les colonnes visibles de la grille
    ''' </summary>
    ''' <remarks></remarks>
    Private mblnResizeColumn As Boolean

    ''' <summary>
    ''' Taille originale de la grille
    ''' </summary>
    ''' <remarks></remarks>
    Public mOrigGridSize As Size

    ''' <summary>
    ''' Garde en mémoire l'ancienne taille de la grille.
    ''' </summary>
    ''' <remarks></remarks>
    Private mAncienneGridSize As Size

    'Private mMouseWheel As TT3DLL.clsGridMouseWheel
    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Class_Terminate_Renamed()
    End Sub

    Public ReadOnly Property DisconnectedDataSource() As Boolean
        Get
            Return Me.mblnDisconnectedDataSource
        End Get
    End Property
    Public Property TailleOrigineGrid() As Size
        Get
            Return Me.mOrigGridSize
        End Get
        Set(ByVal value As Size)
            Me.mOrigGridSize = value
        End Set
    End Property
    Public Property AncienneTailleGrid() As Size
        Get
            Return Me.mAncienneGridSize
        End Get
        Set(ByVal value As Size)
            Me.mAncienneGridSize = value
        End Set
    End Property
    Public ReadOnly Property Grid() As ctlTTGrid
        Get
            Return Me.mobjGRID
        End Get
    End Property
    ''' <summary>
    ''' Initialise les objets dans le contrôle
    ''' </summary>
    ''' <param name="vcTTApp"></param>
    ''' <param name="robjGrid"></param>
    ''' <param name="rcmdAdd"></param>
    ''' <param name="rcmdDelete"></param>
    ''' <param name="vintActionCol"></param>
    ''' <param name="vblnAllowPrint"></param>
    ''' <param name="vblnAllowEditUpdate"></param>
    ''' <param name="vblnKeepOriginalSettings"></param>
    ''' <param name="intNbLigneToScroll"></param>
    ''' <param name="rfrmFormOptional"></param>
    ''' <param name="vblnKeepDisconnectedDataSource"></param>
    ''' <param name="vfctXLSBeforeExportHander"></param>
    ''' <param name="vblnResizeColumn">Définit si l'on resize les colonnes de la grille </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function bln_Init(ByVal vcTTApp As clsTTAPP, _
                             ByRef robjGrid As TT3DLL.ctlTTGrid, _
                             Optional ByRef rcmdAdd As Object = Nothing, _
                             Optional ByRef rcmdDelete As Object = Nothing, _
                             Optional ByVal vintActionCol As Short = clsConstante.TTGridCol.TTGC_MODE, _
                             Optional ByVal vblnAllowPrint As Boolean = True, _
                             Optional ByVal vblnAllowEditUpdate As Boolean = True, _
                             Optional ByVal vblnKeepOriginalSettings As Boolean = False, _
                             Optional ByVal intNbLigneToScroll As Integer = 10, _
                             Optional ByRef rfrmFormOptional As Form = Nothing, _
                             Optional ByVal vblnKeepDisconnectedDataSource As Boolean = False, _
                             Optional ByVal vfctXLSBeforeExportHander As clsTTExcel.BeforeExportXLSHandler = Nothing, _
                             Optional ByVal vblnResizeColumn As Boolean = False) As Boolean
        'On Error GoTo Error_bln_Init

        Const strFCT_NAME As String = "bln_Init"
        Dim blnReturn As Boolean
        Dim strForeColor As String = gvbNullstring
        Dim strBackColor As String = gvbNullstring
        mdictControlLieeGrille = New Dictionary(Of Integer, Control)
        'mMouseWheel = New TT3DLL.clsGridMouseWheel
        'mMouseWheel.fbln_Init(robjGrid)
        'mMouseWheel.mintNbLigneToScroll = intNbLigneToScroll

        'robjGrid.ScrollTrack = True

        blnReturn = True

        If Not IsNothing(mobjGRID) Then
            mobjGRID.DataSource = Nothing
        End If

        If Not mblnIsInCodeGrid Then
            mstrReportName = robjGrid.Parent.Text
        End If

        mintActionCol = vintActionCol
        Me.mblnDisconnectedDataSource = vblnKeepDisconnectedDataSource

        If Not mblnIsInCodeGrid Then
            'Pour les cas ou la form parent est pas accessible...
            If Not rfrmFormOptional Is Nothing Then
                mfrmFormParent = rfrmFormOptional
            Else
                mfrmFormParent = robjGrid.FindForm
            End If

            RegisterFlexGrid_Net(mfrmFormParent, Me)
        End If

        'UPGRADE_NOTE: Object mobjGRID may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mobjGRID = Nothing
        mcTTAPP = vcTTApp
        mblnAllowPrint = vblnAllowPrint
        mblnAllowEditUpdate = vblnAllowEditUpdate

        'UPGRADE_WARNING: TypeName has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        If TypeOf robjGrid Is TT3DLL.ctlTTGrid Then
            On Error Resume Next

            'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.Rows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            robjGrid.Rows = 1
            '!!!!!MP: ne plus re-activer cette ligne, cause des bugs dans l'application
            'robjGrid.Cols = 1
            robjGrid.FixedRows = 1

            'On Error Resume Next
            If robjGrid.Cols = 0 Then robjGrid.Cols = 1
            robjGrid.FixedCols = 1
            'On Error GoTo Error_bln_Init

            'MP: non... ca enleve tout meme la fixed row
            'robjGrid.Clear()
            robjGrid.DataSource = Nothing
            mobjGRID = robjGrid
            mAncienneGridSize = mobjGRID.ClientSize
        Else
            ' Do nothing
        End If

        'UPGRADE_WARNING: TypeName has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        If TypeName(rcmdAdd) = "Button" Then
            mcmdADD = rcmdAdd
        Else
            'Do nothing
        End If

        'UPGRADE_WARNING: TypeName has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        If TypeName(rcmdDelete) = "Button" Then
            mcmdDelete = rcmdDelete
        Else
            'Do nothing
        End If


        robjGrid.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromTop
        'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.AutoSearchDelay. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        robjGrid.AutoSearchDelay = 10

        If robjGrid.SelectionMode = C1.Win.C1FlexGrid.Classic.SelModeSettings.flexSelectionByRow Then
            robjGrid.AllowSelection = False
            robjGrid.AllowBigSelection = False
            robjGrid.SelectionMode = C1.Win.C1FlexGrid.Classic.SelModeSettings.flexSelectionByRow
        End If

        robjGrid.EditOptions = C1.Win.C1FlexGrid.EditFlags.ExitOnLeftRightKeys

        'copier dans le OCX pour les grilles de ttcbo et ttadr, différence pour le FocusRect
        If Not vblnKeepOriginalSettings Then

            robjGrid.BackColorSel = System.Drawing.ColorTranslator.FromOle(TT3DLL.clsConstante.TTColorMode.TTCM_GRID_BACK_SEL)
            robjGrid.ForeColorSel = System.Drawing.ColorTranslator.FromOle(TT3DLL.clsConstante.TTColorMode.TTCM_GRID_FORE_SEL)

            'robjGrid.BACKCOLORSEL = SystemColors.Highlight
            'robjGrid.ForeColorSel = SystemColors.HighlightText

            robjGrid.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.Inset

            pfblnGrd_GetSelectedColors(robjGrid)

            'robjGrid.BackColorSel = &HFFC0C0

            'Sinon, ça jure un peu avec les autres controles
            'robjGrid.FontName = "Arial"
            'robjGrid.FontSize = 8

        End If

        'Les lignes avec une trop petite police rapetissent les lignes un peut trop et coupent les check box
        robjGrid.RowHeightMin = 16


        ' Assigner le vcTTApp à la grille.
        If TypeName(robjGrid) = "ctlTTGrid" Then
            DirectCast(robjGrid, Object).cTTApp = vcTTApp
        End If

        ' Assigner l'export vers Excel.
        mobjGRID.ExportToExcel = vfctXLSBeforeExportHander

        RaiseEvent SetDisplay()

        mChangedColumnSize = New Dictionary(Of Integer, Integer)
        mblnResizeColumn = vblnResizeColumn

        If mblnResizeColumn Then
            fblnResizeGrid()
        End If

        TailleOrigineGrid = AncienneTailleGrid

Exit_bln_Init:
        bln_Init = blnReturn
        Exit Function

Error_bln_Init:
        blnReturn = False
        Call GerrLogError(Err, mstrCLASS_NAME, strFCT_NAME)
        'Resume Exit_bln_Init
    End Function


    Public Function bln_RowAdd(Optional ByVal vstrToAdd As String = "", Optional ByVal vintPosition As Integer = 0) As Boolean
        On Error GoTo Error_bln_RowAdd
        Const strFCT_NAME As String = "bln_RowAdd"
        Dim blnReturn As Boolean
        Dim lngCount As Integer
        Dim cRow As DataRow

        '!!EF : Si la ligne s'ajoute au début et qu'elle devrait s'ajouter à la fin, vérifier que la table n'a pas de sort
        '!!EF : Le sort pourrait se rajouter par le code et cela dérangerait la fonctionnalité de cette fonction

        If vintPosition = 0 Then
            mobjGRID.AddItem(vbTab & clsConstante.TTGridAction.TTGA_INSERT & vbTab & vstrToAdd)
            mobjGRID.Row = mobjGRID.Rows - 1
        Else
            If (Not Me.DisconnectedDataSource) And (TypeOf mobjGRID.DataSource Is System.Data.DataTable) Then
                cRow = mobjGRID.DataSource.NewRow
                mobjGRID.DataSource.Rows.InsertAt(cRow, vintPosition - 1)
            Else
                mobjGRID.AddItem(vbTab & clsConstante.TTGridAction.TTGA_INSERT & vbTab & vstrToAdd, vintPosition)
            End If

            mobjGRID.Row = vintPosition

            mobjGRID.TextMatrix(mobjGRID.Row, 1) = TT3DLL.clsConstante.TTGridAction.TTGA_INSERT
        End If

        blnReturn = fblnGrid_SetRowColor(mobjGRID, mobjGRID.Row, clsConstante.TTColorMode.TTCM_INSERT, mintActionCol)


Exit_bln_RowAdd:
        cRow = Nothing
        bln_RowAdd = blnReturn
        Exit Function

Error_bln_RowAdd:
        blnReturn = False
        Call GerrLogError(Err, mstrCLASS_NAME, strFCT_NAME)
        Resume Exit_bln_RowAdd
    End Function

    Public Function bln_RowDelete(ByVal vlngRow As Integer) As Boolean
        On Error GoTo Error_bln_RowDelete
        Const strFCT_NAME As String = "bln_RowDelete"
        Dim blnReturn As Boolean
        Dim intColor As clsConstante.TTColorMode
        Dim intAction As clsConstante.TTGridAction
        Dim blnRemove As Boolean

        blnReturn = True
        If vlngRow <> 0 Then
            Select Case getTextMatrix(vlngRow, mintActionCol)
                Case CStr(clsConstante.TTGridAction.TTGA_INSERT)
                    intAction = clsConstante.TTGridAction.TTGA_NOACTION
                    intColor = clsConstante.TTColorMode.TTCM_DELETE
                    blnRemove = True

                Case CStr(clsConstante.TTGridAction.TTGA_DELETE)
                    intAction = clsConstante.TTGridAction.TTGA_UPDATE
                    intColor = clsConstante.TTColorMode.TTCM_UPDATE

                Case CStr(clsConstante.TTGridAction.TTGA_LOADED), CStr(clsConstante.TTGridAction.TTGA_UPDATE)
                    intAction = clsConstante.TTGridAction.TTGA_DELETE
                    intColor = clsConstante.TTColorMode.TTCM_DELETE

                Case CStr(clsConstante.TTGridAction.TTGA_NOACTION)
                    intAction = clsConstante.TTGridAction.TTGA_INSERT
                    intColor = clsConstante.TTColorMode.TTCM_INSERT
            End Select

            setTextMatrix(vlngRow, mintActionCol, intAction)
            blnReturn = fblnGrid_SetRowColor(mobjGRID, vlngRow, intColor, mintActionCol)

            If blnRemove Then
                Call mobjGRID.RemoveItem(vlngRow)
            Else
                'Do nothing
            End If
        Else
            'Do nothing
        End If


Exit_bln_RowDelete:
        bln_RowDelete = blnReturn
        Exit Function

Error_bln_RowDelete:
        blnReturn = False
        Call GerrLogError(Err, mstrCLASS_NAME, strFCT_NAME)
        Resume Exit_bln_RowDelete
    End Function

    Public Function getTextMatrix(ByVal row As Integer, ByVal col As Integer) As String
        Return mobjGRID.get_TextMatrix(row, col)
    End Function

    Public Sub setTextMatrix(ByVal row As Integer, ByVal col As Integer, ByVal val As String)
        mobjGRID.set_TextMatrix(row, col, val)
    End Sub

    Public Function bln_SetRowToLoad(ByVal vlngRow As Integer) As Boolean
        On Error GoTo Error_bln_SetRowToLoad
        Const strFCT_NAME As String = "bln_SetRowToLoad"
        Dim blnReturn As Boolean

        setTextMatrix(vlngRow, mintActionCol, clsConstante.TTGridAction.TTGA_LOADED)

        blnReturn = fblnGrid_SetRowColor(mobjGRID, vlngRow, clsConstante.TTColorMode.TTCM_NO_ACTION, mintActionCol)


Exit_bln_SetRowToLoad:
        bln_SetRowToLoad = blnReturn
        Exit Function

Error_bln_SetRowToLoad:
        blnReturn = False
        Call GerrLogError(Err, mstrCLASS_NAME, strFCT_NAME)
        Resume Exit_bln_SetRowToLoad
    End Function

    Public Function bln_SetRowAction(ByVal vlngRow As Integer, ByVal mintAction As clsConstante.TTGridAction, ByVal mintColor As clsConstante.TTColorMode) As Boolean
        On Error GoTo Error_bln_SetRowAction
        Const strFCT_NAME As String = "bln_SetRowAction"
        Dim blnReturn As Boolean

        setTextMatrix(vlngRow, mintActionCol, mintAction)
        blnReturn = fblnGrid_SetRowColor(mobjGRID, vlngRow, mintColor, mintActionCol)


Exit_bln_SetRowAction:
        bln_SetRowAction = blnReturn
        Exit Function

Error_bln_SetRowAction:
        blnReturn = False
        Call GerrLogError(Err, mstrCLASS_NAME, strFCT_NAME)
        Resume Exit_bln_SetRowAction
    End Function

    Public Function bln_RowUpdate(ByVal vlngRow As Integer) As Boolean
        On Error GoTo Error_bln_RowUpdate
        Const strFCT_NAME As String = "bln_RowUpdate"
        Dim blnReturn As Boolean

        blnReturn = True

        If vlngRow > 0 Then
            If getTextMatrix(vlngRow, mintActionCol) = CStr(clsConstante.TTGridAction.TTGA_INSERT) Then
                ' Do nothing
            Else
                setTextMatrix(vlngRow, mintActionCol, clsConstante.TTGridAction.TTGA_UPDATE)
                blnReturn = fblnGrid_SetRowColor(mobjGRID, vlngRow, clsConstante.TTColorMode.TTCM_UPDATE, mintActionCol)
            End If
        Else
            ' Do nothing
        End If


Exit_bln_RowUpdate:
        bln_RowUpdate = blnReturn
        Exit Function

Error_bln_RowUpdate:
        blnReturn = False
        Call GerrLogError(Err, mstrCLASS_NAME, strFCT_NAME)
        Resume Exit_bln_RowUpdate
    End Function

    Public Function bln_Fill(ByVal vstrSQL As String) As Boolean
        On Error GoTo Error_bln_Fill
        Const strFCT_NAME As String = "bln_Fill"
        Dim blnReturn As Boolean
        Dim intScrollBarsStatus As Short
        Dim myDataTable As New System.Data.DataTable

        intScrollBarsStatus = mobjGRID.ScrollBars
        mobjGRID.ScrollBars = ScrollBars.None

        Select Case False
            Case pfblnSetSQL_ActionCol(vstrSQL)
            Case fblnRecord_SELECT_Net(vstrSQL, myDataTable, mcTTAPP.mcnAdoConnection_Net)
            Case bln_Fill_WithDataTable(myDataTable)
            Case Else
                blnReturn = True
        End Select

        'If blnReturn And mblnResizeColumn Then
        'GarderDonnéesPourResize()
        'End If

Exit_bln_Fill:
        myDataTable = Nothing
        mobjGRID.ScrollBars = intScrollBarsStatus
        bln_Fill = blnReturn
        Exit Function

Error_bln_Fill:
        blnReturn = False
        Call GerrLogError(Err, mstrCLASS_NAME, strFCT_NAME)
        Resume Exit_bln_Fill
    End Function

    Public Function bln_PrintGrid(ByVal vblnShowPrintDialog As Boolean, _
                                  ByVal vintLeftRightMargin As Integer, _
                                  ByVal vintTopBottomMargin As Integer) As Boolean
        On Error GoTo Error_bln_PrintGrid
        Const strFCT_NAME As String = "bln_PrintGrid"
        Dim blnReturn As Boolean = True
        Dim strTitre As String = gvbNullstring

        strTitre = mobjGRID.Parent.Text

        Select Case False
            Case mobjGRID.Rows > 1
            Case Else
                mobjGRID.PrintGrid(gstrEXEName & " " & strTitre & " (" & VB6.Format(Now, gstrPCShortDateFormat & " HH:mm") & ")", vblnShowPrintDialog, 2, vintLeftRightMargin, vintTopBottomMargin)
        End Select

Exit_bln_PrintGrid:
        bln_PrintGrid = blnReturn
        Exit Function

Error_bln_PrintGrid:
        blnReturn = False
        Call GerrLogError(Err, mstrCLASS_NAME, strFCT_NAME)
        Resume Exit_bln_PrintGrid
    End Function

    Public Function bln_Fill_NoAction(ByVal vstrSQL As String) As Boolean
        On Error GoTo Error_bln_Fill_NoAction
        Const strFCT_NAME As String = "bln_Fill_NoAction"
        Dim blnReturn As Boolean
        Dim intScrollBarsStatus As Short
        Dim recRecordTemp As New ADODB.Recordset
        Dim myDataTable As New System.Data.DataTable
        Dim intCpt As Integer

        intScrollBarsStatus = mobjGRID.ScrollBars
        mobjGRID.ScrollBars = ScrollBars.None


        Select Case False
            Case fblnRecord_SELECT_Net(vstrSQL, myDataTable, mcTTAPP.mcnAdoConnection_Net)
            Case bln_Fill_WithDataTable(myDataTable)
            Case Else
                blnReturn = True
        End Select

        If mChangedColumnSize.Count > 0 Then
            For Each Col As KeyValuePair(Of Integer, Integer) In mChangedColumnSize
                mobjGRID.ColWidth(Col.Key) = Col.Value
            Next
        Else
            'Do nothing
        End If

        If blnReturn And mblnResizeColumn Then
            GarderDonnéesPourResize()
        End If

Exit_bln_Fill_NoAction:
        'mChangedColumnSize.Clear()
        myDataTable = Nothing
        mobjGRID.ScrollBars = intScrollBarsStatus
        bln_Fill_NoAction = blnReturn
        Exit Function

Error_bln_Fill_NoAction:
        blnReturn = False
        Call GerrLogError(Err, mstrCLASS_NAME, strFCT_NAME)
        Resume Exit_bln_Fill_NoAction
    End Function

    Public Function bln_Fill_WithRecordset(ByVal vrecRecord As ADODB.Recordset) As Boolean
        On Error GoTo Error_bln_Fill_WithRecordset
        Const strFCT_NAME As String = "bln_Fill_WithRecordset"
        Dim blnReturn As Boolean
        Dim intScrollBarsStatus As Short
        Dim recRecordTemp As New ADODB.Recordset
        Dim intCpt As Integer

        Dim myDA As New System.Data.OleDb.OleDbDataAdapter()
        Dim myDS As System.Data.DataSet = New System.Data.DataSet()
        Dim myDT As System.Data.DataTable = New System.Data.DataTable
        Dim recRecord As New ADODB.Recordset


        If Not mblnIsInCodeGrid Then
            If mcTTAPP Is Nothing Then
                mcTTAPP = New TT3DLL.clsTTAPP
            End If
            Call mcTTAPP.bln_FormStopUpdate(mfrmFormParent)
        End If

        mobjGRID.Redraw = C1.Win.C1FlexGrid.Classic.RedrawSettings.flexRDNone
        mobjGRID.Visible = False

        intScrollBarsStatus = mobjGRID.ScrollBars
        mobjGRID.ScrollBars = ScrollBars.None

        'On clone le recordset, pcq il revient magané du GridFill ...
        Call fblnCloneRecordSet(vrecRecord, recRecordTemp)

        Select Case False
            Case fblnGrid_FillWithRecordset_Net(mobjGRID, recRecordTemp, Me.DisconnectedDataSource)
            Case Else
                mrecRecord = vrecRecord

                Call mcTTAPP.bln_RecordsetClone(vrecRecord, recRecord)
                myDA.Fill(myDS, recRecord, "MyTable")
                myDT = myDS.Tables(0)
                mDataTable = myDT

                'For intCpt = 1 To mobjGRID.Cols - 1
                '    mobjGRID.set_ColDataType(intCpt, GetType(Object))
                'Next

                RaiseEvent SetDisplay()

                Select Case False
                    Case pfblnGRID_SetDataCol()
                    Case pfblnGRID_GetRecordInfo()
                    Case Else
                        blnReturn = True
                End Select
        End Select

        If blnReturn And mblnResizeColumn Then
            GarderDonnéesPourResize()
        End If

Exit_bln_Fill_WithRecordset:
        mobjGRID.Redraw = C1.Win.C1FlexGrid.Classic.RedrawSettings.flexRDBuffered
        mobjGRID.Visible = True
        If Not mblnIsInCodeGrid Then
            Call mcTTAPP.bln_FormAllowUpdate()
        End If

        recRecordTemp = Nothing
        mobjGRID.ScrollBars = intScrollBarsStatus
        bln_Fill_WithRecordset = blnReturn
        Exit Function

Error_bln_Fill_WithRecordset:
        blnReturn = False
        Call GerrLogError(Err, mstrCLASS_NAME, strFCT_NAME)
        Resume Exit_bln_Fill_WithRecordset
    End Function


    Public Function bln_SetRowColor(ByVal vlngRow As Integer, ByVal vlngColor As clsConstante.TTColorMode) As Boolean
        On Error GoTo Error_bln_SetRowColor
        Const strFCT_NAME As String = "bln_SetRowColor"
        Dim blnReturn As Boolean

        blnReturn = fblnGrid_SetRowColor(mobjGRID, vlngRow, vlngColor, mintActionCol)


Exit_bln_SetRowColor:
        bln_SetRowColor = blnReturn
        Exit Function

Error_bln_SetRowColor:
        blnReturn = False
        Call GerrLogError(Err, mstrCLASS_NAME, strFCT_NAME)
        Resume Exit_bln_SetRowColor
    End Function

    Public Function bln_SetComboCol(ByVal vintCol As Short, ByVal vstrSQL As String, Optional ByRef blnAllowEmpty As Boolean = True) As Boolean
        On Error GoTo Error_bln_SetComboCol
        Const strFCT_NAME As String = "bln_SetComboCol"
        Dim blnReturn As Boolean
        Dim recRec As New ADODB.Recordset
        Dim strADD As String = gvbNullstring

        blnReturn = fblnRecord_SELECT(vstrSQL, recRec, mcTTAPP.mcnAdoConnection)

        If blnAllowEmpty Then
            strADD = "#0;    |"
        Else
            strADD = gvbNullstring
        End If

        '"#1;Full time|#23;Part time|#65;Contractor|#78;Intern|#0;Other"
        Do While Not recRec.EOF
            strADD = strADD & "#" & recRec.Fields(0).Value & ";" & recRec.Fields(1).Value & "|"
            recRec.MoveNext()
        Loop

        mobjGRID.set_ColComboList(vintCol, strADD)


Exit_bln_SetComboCol:
        'UPGRADE_NOTE: Object recRec may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        recRec = Nothing
        bln_SetComboCol = blnReturn
        Exit Function

Error_bln_SetComboCol:
        blnReturn = False
        Call GerrLogError(Err, mstrCLASS_NAME, strFCT_NAME)
        Resume Exit_bln_SetComboCol
    End Function

    'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Terminate_Renamed()
        'UPGRADE_NOTE: Object mcmdADD may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mcmdADD = Nothing
        'UPGRADE_NOTE: Object mcmdDelete may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mcmdDelete = Nothing
        'UPGRADE_NOTE: Object mobjGRID may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'

        If (Not mobjGRID Is Nothing) Then
            mobjGRID.ExportToExcel = Nothing
        End If
        mobjGRID = Nothing

        'UPGRADE_NOTE: Object mrecRecord may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mrecRecord = Nothing

        'UPGRADE_NOTE: Object mcTTAPP may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mcTTAPP = Nothing

        mfrmFormParent = Nothing

        mChangedColumnSize = Nothing
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub

    Private Sub mcmdADD_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mcmdADD.Click
        Dim blnCancel As Boolean
        Dim intCptCol As Short

        blnCancel = False
        RaiseEvent BeforeClickAdd(blnCancel)

        If Not blnCancel Then
            If Not blnInsertUnderCurrentRow Then
                'mobjGRID.AddItem("")
                bln_RowAdd()
                mobjGRID.Row = mobjGRID.Rows - 1
            Else
                'mobjGRID.AddItem("", mobjGRID.Row + 1)
                bln_RowAdd("", mobjGRID.Row + 1)
            End If

            Call fblnGrid_SetRowColor(mobjGRID, mobjGRID.Row, clsConstante.TTColorMode.TTCM_INSERT, mintActionCol)
            setTextMatrix(mobjGRID.Row, mintActionCol, clsConstante.TTGridAction.TTGA_INSERT)

            'Trouve la premiere colonne visible
            Call psGRD_GetFirstVisibleCol(intCptCol)
            intCptCol = IIf(intCptCol = 0, 1, intCptCol)

            Call mobjGRID.ShowCell(mobjGRID.Row, intCptCol)
            Call mobjGRID.Select(mobjGRID.Row, intCptCol)

            RaiseEvent ClickAdd()
        Else
            'do nothing:ajout de ligne cancellé
        End If
    End Sub
    Private Sub psGRD_GetFirstVisibleCol(ByRef rintCol As Short)
        Dim intCpt As Short

        For intCpt = 1 To mobjGRID.Cols - 1
            If mobjGRID.get_ColHidden(intCpt) = True Then
                'prochaine
            Else
                rintCol = intCpt
                Exit For
            End If
        Next intCpt
    End Sub

    Private Sub mcmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mcmdDelete.Click
        Dim blnCancel As Boolean

        If mobjGRID.Row > 0 And mobjGRID.Rows > 1 Then
            blnCancel = False
            RaiseEvent BeforeClickDelete(blnCancel)

            If Not blnCancel Then
                Select Case True
                    Case getTextMatrix(mobjGRID.Row, mintActionCol) = gvbNullstring
                        Call mobjGRID.RemoveItem(mobjGRID.Row)

                    Case getTextMatrix(mobjGRID.Row, mintActionCol) = CStr(clsConstante.TTGridAction.TTGA_INSERT)
                        Call mobjGRID.RemoveItem(mobjGRID.Row)

                    Case getTextMatrix(mobjGRID.Row, mintActionCol) = CStr(clsConstante.TTGridAction.TTGA_LOADED)
                        Call fblnGrid_SetRowColor(mobjGRID, mobjGRID.Row, clsConstante.TTColorMode.TTCM_DELETE, mintActionCol)
                        setTextMatrix(mobjGRID.Row, mintActionCol, clsConstante.TTGridAction.TTGA_DELETE)

                    Case getTextMatrix(mobjGRID.Row, mintActionCol) = CStr(clsConstante.TTGridAction.TTGA_DELETE)
                        Call fblnGrid_SetRowColor(mobjGRID, mobjGRID.Row, clsConstante.TTColorMode.TTCM_UPDATE, mintActionCol)
                        setTextMatrix(mobjGRID.Row, mintActionCol, clsConstante.TTGridAction.TTGA_UPDATE)

                    Case getTextMatrix(mobjGRID.Row, mintActionCol) = CStr(clsConstante.TTGridAction.TTGA_UPDATE)
                        Call fblnGrid_SetRowColor(mobjGRID, mobjGRID.Row, clsConstante.TTColorMode.TTCM_DELETE, mintActionCol)
                        setTextMatrix(mobjGRID.Row, mintActionCol, clsConstante.TTGridAction.TTGA_DELETE)

                End Select
                RaiseEvent ClickDelete()
            Else
                'do nothing: delete de ligne cancellé
            End If
        Else
            'Pas de linge dans la grille
            'Do nothing
        End If
    End Sub

    Private Sub mobjGRID_AfterEdit(ByVal eventSender As System.Object, ByVal eventArgs As C1.Win.C1FlexGrid.RowColEventArgs) Handles mobjGRID.AfterEdit
        If mblnAllowEditUpdate Then
            Select Case True
                Case getTextMatrix(mobjGRID.Row, mintActionCol) = CStr(clsConstante.TTGridAction.TTGA_INSERT)
                    'Do nothing
                Case getTextMatrix(mobjGRID.Row, mintActionCol) = CStr(clsConstante.TTGridAction.TTGA_LOADED)
                    Call fblnGrid_SetRowColor(mobjGRID, mobjGRID.Row, clsConstante.TTColorMode.TTCM_UPDATE, mintActionCol)
                    setTextMatrix(mobjGRID.Row, mintActionCol, clsConstante.TTGridAction.TTGA_UPDATE)
            End Select
        Else
            'do nothing
        End If
    End Sub

    Private Sub mobjGRID_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mobjGRID.Leave
        'On appelle le after edit dans le lost focus de la grille pour cette raison...
        'Disons que l'on est en train d'editer dans la grille, et qu'on switch d'application,
        'et qu'on revient, la grille est encore en edit mode (le focus avec le curseur
        'dans une cellule n'est plus là toutefois) mais si on pese sur appliquer (disons),
        'le after edit se declenchera pas, c'est pour cette raison que l'after edit est appele ici aussi...

        ' Si on est dans une grid en édition et quitte l'application, EditWindows lance une erreur.
        On Error GoTo mobjGRID_Leave_Error

        If mobjGRID.EditWindow <> 0 Then
            Call mobjGRID_AfterEdit(mobjGRID, New C1.Win.C1FlexGrid.RowColEventArgs(mobjGRID.Row, mobjGRID.Col))
        Else
            'do nothing
        End If

mobjGRID_Leave_End:
        Exit Sub

mobjGRID_Leave_Error:
        Resume mobjGRID_Leave_End

    End Sub

    '****************************************************************
    '* Nom de la fonction   : bln_ValideRules
    '*
    '*               Cree   : 24-10-2001   lamorin
    '*            Modifie   : **-**-****  ***
    '*
    '*                But   :
    '*
    '* Parametre(s):
    '*              IN :
    '*
    '*              OUT:    True, si succes. False, si erreur
    '*
    '****************************************************************
    Public Function bln_ValideRules() As Boolean
        On Error GoTo Error_bln_ValideRules
        Const strFCT_NAME As String = "bln_ValideRules"
        Dim blnReturn As Boolean

        RaiseEvent ValideGridRules(blnReturn)


Exit_bln_ValideRules:
        bln_ValideRules = blnReturn
        Exit Function

Error_bln_ValideRules:
        blnReturn = False
        Call GerrLogError(Err, mstrCLASS_NAME, strFCT_NAME)
        Resume Exit_bln_ValideRules
    End Function

    '****************************************************************
    '* Nom de la fonction   : bln_SaveData
    '*
    '*               Cree   : 24-10-2001   lamorin
    '*            Modifie   : **-**-****  ***
    '*
    '*                But   :
    '*
    '* Parametre(s):
    '*              IN :
    '*
    '*              OUT:    True, si succes. False, si erreur
    '*
    '****************************************************************
    Public Function bln_SaveData() As Boolean
        On Error GoTo Error_bln_SaveData
        Const strFCT_NAME As String = "bln_SaveData"
        Dim blnReturn As Boolean

        RaiseEvent SaveGridData(blnReturn)

Exit_bln_SaveData:
        bln_SaveData = blnReturn
        Exit Function

Error_bln_SaveData:
        blnReturn = False
        Call GerrLogError(Err, mstrCLASS_NAME, strFCT_NAME)
        Resume Exit_bln_SaveData

    End Function

    '****************************************************************
    '* Nom de la fonction   : pfblnSetSQL_ActionCol
    '*
    '*               Cree   : 01-04-2002   lamorin
    '*            Modifie   : **-**-****  ***
    '*
    '*                But   :
    '*
    '* Parametre(s):
    '*              IN :
    '*
    '*              OUT:    True, si succes. False, si erreur
    '*
    '****************************************************************
    Private Function pfblnSetSQL_ActionCol(ByRef vstrSQL As String) As Boolean
        On Error GoTo Error_pfblnSetSQL_ActionCol
        Const strFCT_NAME As String = "pfblnSetSQL_ActionCol"
        Dim blnReturn As Boolean
        Dim strDebut As String = gvbNullstring
        Dim intDebut As Short
        Dim intBoucle As Short

        blnReturn = True

        If mintActionCol = clsConstante.TTGridCol.TTGC_MODE Then
            vstrSQL = Replace(vstrSQL, "Select ", "Select " & clsConstante.TTGridAction.TTGA_LOADED & " as Action ,", , , CompareMethod.Text)
        Else
            For intBoucle = 1 To mintActionCol - 1
                intDebut = InStr(intDebut + 1, vstrSQL, ",", CompareMethod.Text)
            Next
            strDebut = Mid(vstrSQL, 1, intDebut + 1)
            vstrSQL = Replace(vstrSQL, strDebut, strDebut & clsConstante.TTGridAction.TTGA_LOADED & " as Action ,", , , CompareMethod.Text)
        End If


Exit_pfblnSetSQL_ActionCol:
        pfblnSetSQL_ActionCol = blnReturn
        Exit Function

Error_pfblnSetSQL_ActionCol:
        blnReturn = False
        Call GerrLogError(Err, mstrCLASS_NAME, strFCT_NAME)
        Resume Exit_pfblnSetSQL_ActionCol
    End Function

    Private Sub mobjGRID_AfterMoveColumn(ByVal eventSender As System.Object, ByVal eventArgs As C1.Win.C1FlexGrid.DragRowColEventArgs) Handles mobjGRID.AfterDragColumn
        Call pfblnGRID_GetRecordInfo()
    End Sub

    Private Sub mobjGRID_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles mobjGRID.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.Shift
        Select Case KeyCode
            Case mintKeyAddLine
                If Not mcmdADD Is Nothing Then
                    If mcmdADD.Enabled Then
                        Call mcmdADD_Click(mcmdADD, New System.EventArgs())
                    Else
                        'do nohthing
                    End If
                End If

            Case mintKeyDelLine
                If Not mcmdDelete Is Nothing Then
                    If mcmdDelete.Enabled Then
                        Call mcmdDelete_Click(mcmdDelete, New System.EventArgs())
                    Else
                        'do nothing
                    End If
                End If

        End Select

    End Sub

    Private Sub mobjGRID_MouseDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles mobjGRID.MouseDown
        On Error GoTo Error_mobjGRID_MouseDown
        Const strFCT_NAME As String = "mobjGRID_MouseDown"
        Dim blnToprint As Boolean
        Dim frmTemp As New frmTTPrintGrid
        Dim strTitre As String = gvbNullstring
        Dim intLargeurGrille As Integer = 0

        strTitre = mobjGRID.Parent.Text
        Select Case False
            Case mobjGRID.Rows > 1, mblnAllowPrint
                'Case eventArgs.button = VB6.MouseButtonConstants.RightButton
            Case eventArgs.Button = MouseButtons.Right
            Case frmTemp.bln_Show(mcTTAPP, strTitre, blnToprint)
            Case blnToprint
            Case pfblnAjusteLargeurGrille(intLargeurGrille, mobjGRID, False)
            Case Else
                mobjGRID.PrintGrid(gstrEXEName & " " & strTitre & " (" & VB6.Format(Now, gstrPCShortDateFormat & " HH:mm") & ")", Nothing, 2, 300, 300)
                mobjGRID.PrintGrid(gstrEXEName & " " & strTitre & " (" & VB6.Format(Now, gstrPCShortDateFormat & " HH:mm") & ")", Nothing, 2, 0, 0)
                Call pfblnAjusteLargeurGrille(intLargeurGrille, mobjGRID, True)
        End Select

        'UPGRADE_NOTE: Object frmTemp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        frmTemp = Nothing

Exit_mobjGRID_MouseDown:
        Exit Sub

Error_mobjGRID_MouseDown:
        If Err.Number = 57 Then
            Call mcTTAPP.bln_ShowMSG(mintPrinterNotAvail_msg, MsgBoxStyle.Information)
        End If
        Resume Exit_mobjGRID_MouseDown
    End Sub


    '****************************************************************
    '* Nom de la fonction   : bln_ValidationError
    '*
    '*               Cree   : 31-03-2004  mpelletier
    '*            Modifie   : **-**-****  ***
    '*
    '*                But   :
    '*
    '* Parametre(s):
    '*              IN :
    '*
    '*              OUT:    True, si succes. False, si erreur
    '*
    '****************************************************************
    Public Function bln_ValidationError(ByVal vintMsg As Long, ByVal vlngRow As Integer, ByVal vintCol As Short) As Boolean
        On Error GoTo Error_bln_ValidationError
        Const strFCT_NAME As String = "bln_ValidationError"
        Dim blnReturn As Boolean

        blnReturn = True

        Call mcTTAPP.bln_ShowMSG(vintMsg, MsgBoxStyle.Information)
        Call bln_SelectAndShowLine(vlngRow, vintCol)

Exit_bln_ValidationError:
        bln_ValidationError = blnReturn
        Exit Function

Error_bln_ValidationError:
        blnReturn = False
        Call GerrLogError(Err, mstrCLASS_NAME, strFCT_NAME)
        Resume Exit_bln_ValidationError
    End Function

    '****************************************************************
    '* Nom de la fonction   : bln_SelectAndShowLine
    '*
    '*               Cree   : 31-03-2004  mpelletier
    '*            Modifie   : **-**-****  ***
    '*
    '*                But   :
    '*
    '* Parametre(s):
    '*              IN :
    '*
    '*              OUT:    True, si succes. False, si erreur
    '*
    '****************************************************************
    Public Function bln_SelectAndShowLine(ByVal vlngRow As Integer, Optional ByVal vintCol As Short = 1) As Boolean
        On Error GoTo Error_bln_SelectAndShowLine
        Const strFCT_NAME As String = "bln_SelectAndShowLine"
        Dim blnReturn As Boolean

        blnReturn = True

        If (mobjGRID.get_RowIsVisible(vlngRow)) Then
            mobjGRID.Select(vlngRow, vintCol, False)
        Else
            mobjGRID.Select(vlngRow, vintCol)
            mobjGRID.ShowCell(vlngRow, vintCol)
        End If

Exit_bln_SelectAndShowLine:
        bln_SelectAndShowLine = blnReturn
        Exit Function

Error_bln_SelectAndShowLine:
        blnReturn = False
        Call GerrLogError(Err, mstrCLASS_NAME, strFCT_NAME)
        Resume Exit_bln_SelectAndShowLine
    End Function
    ReadOnly Property Cols() As Short
        Get
            Cols = mobjGRID.Cols
        End Get
    End Property
    ReadOnly Property ADORecord() As ADODB.Recordset
        Get
            ADORecord = mrecRecord
        End Get
    End Property

    Property DataTable() As System.Data.DataTable
        Get
            DataTable = mDataTable
        End Get

        Set(ByVal vDataTable As System.Data.DataTable)
            mDataTable = vDataTable
        End Set

    End Property

    Property Filter() As String
        Get
            Filter = mstrFilter
        End Get

        Set(ByVal vstrFilter As String)
            Dim myDataView As System.Data.DataView

            myDataView = New System.Data.DataView(mDataTable, vstrFilter, gvbNullstring, DataViewRowState.CurrentRows)

            mstrFilter = vstrFilter

            Call fblnGrid_FillWithDataView_Net(mobjGRID, myDataView, mblnDisconnectedDataSource)

            RaiseEvent SetDisplay()
            If mblnResizeColumn Then
                GarderDonnéesPourResize()
            End If
        End Set

    End Property

    ReadOnly Property ColDataType(ByVal vlngCol As Integer) As Integer
        Get
            'UPGRADE_WARNING: Couldn't resolve default property of object mvarColonnes(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            ColDataType = mvarColonnes(vlngCol, mintTAB_Type)
        End Get
    End Property

    ReadOnly Property ColName(ByVal vlngCol As Integer) As String
        Get
            'UPGRADE_WARNING: Couldn't resolve default property of object mvarColonnes(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            ColName = mvarColonnes(vlngCol, mintTAB_Caption)
        End Get
    End Property

    ReadOnly Property ColField(ByVal vlngCol As Integer) As String
        Get
            'UPGRADE_WARNING: Couldn't resolve default property of object mvarColonnes(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            ColField = mvarColonnes(vlngCol, mintTAB_Champ)
        End Get
    End Property
    ReadOnly Property ColWidth(ByVal vlngCol As Integer) As Integer
        Get
            'UPGRADE_WARNING: Couldn't resolve default property of object mvarColonnes(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            ColWidth = mvarColonnes(vlngCol, mintTAB_Width)
        End Get
    End Property

    Private Function pfblnGRID_GetRecordInfo() As Boolean
        On Error GoTo Error_pfblnGRID_GetRecordInfo
        Const strFCT_NAME As String = "pfblnGRID_GetRecordInfo"

        Dim blnReturn As Boolean
        Dim intCol As Short
        Dim intCount As Short
        Dim strFieldType As String = gvbNullstring
        Dim strFieldName As String = gvbNullstring
        Dim strColFormat As String = gvbNullstring

        blnReturn = True

        'Tableau de la grille avec le nom et le type de donnee ratache au caption
        'Le nombre de colone - la colone grise de la grille (Fixed row)
        ReDim mvarColonnes(mobjGRID.Cols - 1, mintNOMBRE_COLS_tab)

        '1-nom de colone '2-nom de  champ '3-Type de champ

        intCount = 1

        'Boucle dans la colone de la grille pour avoir le caption de chaque colone
        'L'index du recordset commence a 0 et la grille 1
        'Donc intCount-1 pour l'index du recordset = intCount index de la colone de la grille

        For intCol = 1 To mobjGRID.Cols - 1
            'UPGRADE_WARNING: Couldn't resolve default property of object mobjGRID.Cell(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            strFieldName = mobjGRID.Cell(CellPropertySettings.flexcpData, 0, intCol, 0, intCol)

            'UPGRADE_WARNING: Couldn't resolve default property of object mvarColonnes(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            mvarColonnes(intCount, mintTAB_Caption) = getTextMatrix(0, intCol)
            'UPGRADE_WARNING: Couldn't resolve default property of object mvarColonnes(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            mvarColonnes(intCount, mintTAB_Champ) = strFieldName
            'UPGRADE_WARNING: Couldn't resolve default property of object mvarColonnes(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            mvarColonnes(intCount, mintTAB_Width) = IIf(mobjGRID.get_ColHidden(intCol) = True, 0, mobjGRID.get_ColWidth(intCol))

            strColFormat = mobjGRID.ColFormat(intCol)

            'Get field type

            Select Case True

                Case mDataTable.Columns(strFieldName).DataType.Name = GetType(Integer).Name
                    mvarColonnes(intCount, mintTAB_Type) = ADODB.DataTypeEnum.adInteger

                Case mDataTable.Columns(strFieldName).DataType.Name = GetType(Boolean).Name
                    mvarColonnes(intCount, mintTAB_Type) = ADODB.DataTypeEnum.adInteger

                Case mDataTable.Columns(strFieldName).DataType.Name = GetType(Byte).Name
                    mvarColonnes(intCount, mintTAB_Type) = ADODB.DataTypeEnum.adInteger

                Case mDataTable.Columns(strFieldName).DataType.Name = GetType(String).Name
                    mvarColonnes(intCount, mintTAB_Type) = ADODB.DataTypeEnum.adVarChar

                Case mDataTable.Columns(strFieldName).DataType.Name = GetType(Char).Name
                    mvarColonnes(intCount, mintTAB_Type) = ADODB.DataTypeEnum.adVarChar

                Case mDataTable.Columns(strFieldName).DataType.Name = GetType(Double).Name
                    mvarColonnes(intCount, mintTAB_Type) = ADODB.DataTypeEnum.adDouble

                Case mDataTable.Columns(strFieldName).DataType.Name = GetType(Decimal).Name
                    mvarColonnes(intCount, mintTAB_Type) = ADODB.DataTypeEnum.adDouble

                Case mDataTable.Columns(strFieldName).DataType.Name = GetType(Date).Name
                    mvarColonnes(intCount, mintTAB_Type) = ADODB.DataTypeEnum.adDate

                Case mDataTable.Columns(strFieldName).DataType.Name = GetType(DateTime).Name
                    mvarColonnes(intCount, mintTAB_Type) = adDateTime

                Case Else
                    ' Invalide Type
                    'UPGRADE_WARNING: Couldn't resolve default property of object mvarColonnes(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    mvarColonnes(intCount, mintTAB_Type) = "Not Valide"

            End Select


            '    Case ADODB.DataTypeEnum.adInteger, ADODB.DataTypeEnum.adSmallInt, ADODB.DataTypeEnum.adUnsignedTinyInt
            '        'UPGRADE_WARNING: Couldn't resolve default property of object mvarColonnes(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            '        mvarColonnes(intCount, mintTAB_Type) = ADODB.DataTypeEnum.adInteger

            '    Case ADODB.DataTypeEnum.adCurrency, ADODB.DataTypeEnum.adDouble, ADODB.DataTypeEnum.adNumeric
            '        'UPGRADE_WARNING: Couldn't resolve default property of object mvarColonnes(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            '        mvarColonnes(intCount, mintTAB_Type) = ADODB.DataTypeEnum.adDouble

            '    Case ADODB.DataTypeEnum.adChar, ADODB.DataTypeEnum.adVarChar
            '        'UPGRADE_WARNING: Couldn't resolve default property of object mvarColonnes(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            '        mvarColonnes(intCount, mintTAB_Type) = ADODB.DataTypeEnum.adVarChar

            '    Case ADODB.DataTypeEnum.adDate
            '        'UPGRADE_WARNING: Couldn't resolve default property of object mvarColonnes(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            '        mvarColonnes(intCount, mintTAB_Type) = ADODB.DataTypeEnum.adDate
            '        If Me.DisconnectedDataSource Then
            '            mobjGRID.set_ColDataType(intCol, GetType(DateTime))
            '        End If

            '    Case adDateTime
            '        'UPGRADE_WARNING: Couldn't resolve default property of object mvarColonnes(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            '        mvarColonnes(intCount, mintTAB_Type) = adDateTime
            '        If Me.DisconnectedDataSource Then
            '            mobjGRID.set_ColDataType(intCol, GetType(DateTime))
            '        End If

            '    Case ADODB.DataTypeEnum.adDBTimeStamp
            '        'UPGRADE_WARNING: Couldn't resolve default property of object mvarColonnes(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            '        mvarColonnes(intCount, mintTAB_Type) = adDateTime
            '        If Me.DisconnectedDataSource Then
            '            mobjGRID.set_ColDataType(intCol, GetType(DateTime))
            '        End If
            '    Case Else
            '        'Invalide type
            '        'UPGRADE_WARNING: Couldn't resolve default property of object mvarColonnes(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            '        mvarColonnes(intCount, mintTAB_Type) = "Not Valide"
            'End Select

            If strColFormat <> gvbNullstring Then
                mobjGRID.ColFormat(intCol) = strColFormat
            End If

            intCount = intCount + 1
        Next intCol

Exit_pfblnGRID_GetRecordInfo:
        pfblnGRID_GetRecordInfo = blnReturn
        Exit Function

Error_pfblnGRID_GetRecordInfo:
        blnReturn = False
        Call GerrLogError(Err, mstrCLASS_NAME, strFCT_NAME)
        Resume Exit_pfblnGRID_GetRecordInfo
    End Function

    '****************************************************************
    '* Nom de la fonction   : pfblnGRID_SetDataCol
    '*
    '*               Cree   : 30-12-2003   mpelletier
    '*            Modifie   : **-**-****  ***
    '*
    '*                But   :
    '*
    '* Parametre(s):
    '*              IN :
    '*
    '*              OUT:    True, si succes. False, si erreur
    '*
    '****************************************************************
    Private Function pfblnGRID_SetDataCol() As Boolean
        On Error GoTo Error_pfblnGRID_SetDataCol
        Const strFCT_NAME As String = "pfblnGRID_SetDataCol"
        Dim blnReturn As Boolean
        Dim intCpt As Short

        blnReturn = True

        'MP: on a besoin de cette fonction puisque quand on bouge les colonnes, les positions du recordset ne changent pas...

        'On met le nom du champ correspondant dans le data de la colonne.
        'Puisqu'on bouge les colonnes, il faut avoir un moyen de se rappeler ce que chaque colonne represente.

        'If mobjGRID.Cols = mrecRecord.Fields.Count + 1 Then
        If mobjGRID.Cols = mDataTable.Columns.Count + 1 Then
            For intCpt = 0 To mobjGRID.Cols - 2
                'UPGRADE_WARNING: Couldn't resolve default property of object mobjGRID.Cell(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                '                mobjGRID.Cell(CellPropertySettings.flexcpData, 0, intCpt, 0, intCpt) = mrecRecord.Fields(intCpt - 1).Name
                mobjGRID.Cell(CellPropertySettings.flexcpData, 0, intCpt + 1, 0, intCpt + 1) = mDataTable.Columns(intCpt).ColumnName
            Next intCpt
        Else
            'Debalancement entre le caption et le recordset
            If mcTTAPP.DebugMode <> clsConstante.TTDEBUG_MODE.TTDM_NODEBUG Then
                'Call MsgBox("AVERTISSEMENT: le caption et le recordset n'ont pas le même nombre de colonnes." & vbCrLf & vbCrLf & "Caption:" & vbCrLf & mobjGRID.FormatString, MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
                Call MsgBox("AVERTISSEMENT: le caption et le recordset n'ont pas le même nombre de colonnes." & vbCrLf & vbCrLf & "Caption:" & vbCrLf, MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
            End If

            blnReturn = False
        End If

Exit_pfblnGRID_SetDataCol:
        pfblnGRID_SetDataCol = blnReturn
        Exit Function

Error_pfblnGRID_SetDataCol:
        blnReturn = False
        Call mcTTAPP.bln_LogError(Err, mstrCLASS_NAME, strFCT_NAME)
        Resume Exit_pfblnGRID_SetDataCol
    End Function
    Public Function fblnGrid_SetRowColor(ByRef objGrid As TT3DLL.ctlTTGrid, ByVal vlngRow As Integer, ByVal vlngColor As Integer, Optional ByVal vintStartCol As Short = 0) As Boolean
        On Error GoTo Error_fblnGrid_SetRowColor
        Const strFCT_NAME As String = "fblnGrid_SetRowColor"
        Dim blnReturn As Boolean
        Dim lngCount As Integer
        Dim lngStart As Integer

        blnReturn = True
        'UPGRADE_WARNING: Couldn't resolve default property of object objGrid.FixedCols. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        lngStart = objGrid.FixedCols

        objGrid.Cell(CellPropertySettings.flexcpBackColor, vlngRow, vintStartCol, vlngRow, objGrid.Cols - 1) = vlngColor

        'UPGRADE_WARNING: Couldn't resolve default property of object objGrid.Cols. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        'For lngCount = vintStartCol To objGrid.Cols - 1
        'UPGRADE_WARNING: Couldn't resolve default property of object objGrid.Cell. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        'objGrid.Cell(CellPropertySettings.flexcpBackColor, vlngRow, lngCount, vlngRow, lngCount) = vlngColor

        'Next lngCount

        'Couleur colone 0
        If vlngColor = clsConstante.TTColorMode.TTCM_NO_ACTION Then
            'UPGRADE_WARNING: Couldn't resolve default property of object objGrid.Cell. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            objGrid.Cell(CellPropertySettings.flexcpBackColor, vlngRow, 0, vlngRow, 0) = System.Drawing.ColorTranslator.ToOle(System.Drawing.SystemColors.Control)
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object objGrid.Cell. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"
            objGrid.Cell(CellPropertySettings.flexcpBackColor, vlngRow, 0, vlngRow, 0) = vlngColor
        End If


Exit_fblnGrid_SetRowColor:
        fblnGrid_SetRowColor = blnReturn
        Exit Function

Error_fblnGrid_SetRowColor:
        blnReturn = False
        Call GerrLogError(Err, mstrCLASS_NAME, strFCT_NAME)
        Resume Exit_fblnGrid_SetRowColor
    End Function

    '****************************************************************
    '* Nom de la fonction   : fblnGRID_SetAltColor
    '*
    '*               Cree   : 24-08-2005   mpelletier
    '*            Modifie   : **-**-****  ***
    '*
    '*                But   :
    '*
    '* Parametre(s):
    '*              IN :
    '*c
    '*              OUT:    True, si succes. False, si erreur
    '*
    '****************************************************************
    Public Function fblnGRID_SetAltColor(ByVal vgrdTemp As TT3DLL.ctlTTGrid, ByVal vintCol2SwitchLine As Short) As Boolean
        On Error GoTo Error_fblnGRID_SetAltColor
        Const strFCT_NAME As String = "fblnGRID_SetAltColor"
        Dim blnReturn As Boolean
        Dim lngCpt As Integer
        Dim strValue As String = gvbNullstring
        Dim lngCptDepart As Integer
        Dim lngCurrColor As Integer

        'NOTE: Normalement on appelle cette fonction sur le load de la grille ET sur l'after sort

        blnReturn = True

        lngCurrColor = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)

        If vgrdTemp.Rows - 1 > 0 Then
            For lngCpt = 1 To vgrdTemp.Rows - 1
                If strValue = gvbNullstring Then
                    lngCptDepart = lngCpt
                    strValue = vgrdTemp.TextMatrix(lngCpt, vintCol2SwitchLine)
                Else
                    If strValue <> vgrdTemp.TextMatrix(lngCpt, vintCol2SwitchLine) Or lngCpt = vgrdTemp.Rows - 1 Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object vgrdTemp.Cell(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        vgrdTemp.Cell(CellPropertySettings.flexcpBackColor, lngCptDepart, 1, lngCpt - 1, vgrdTemp.Cols - 1) = lngCurrColor

                        If lngCurrColor = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White) Then
                            lngCurrColor = TTCM_GRID_BACK_COLOR_ALT
                        Else
                            lngCurrColor = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)
                        End If

                        lngCptDepart = lngCpt
                        strValue = vgrdTemp.TextMatrix(lngCpt, vintCol2SwitchLine)

                        'Si a la derniere ligne...
                        If vgrdTemp.Rows - 1 = lngCpt Then
                            'Si la ligne precedente est differente, nouvelle couleur
                            If strValue <> vgrdTemp.TextMatrix(lngCpt - 1, vintCol2SwitchLine) Then
                                'UPGRADE_WARNING: Couldn't resolve default property of object vgrdTemp.Cell(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                                vgrdTemp.Cell(CellPropertySettings.flexcpBackColor, vgrdTemp.Rows - 1, 1, vgrdTemp.Rows - 1, vgrdTemp.Cols - 1) = lngCurrColor
                            Else
                                'Sinon meme couleur que la ligne precedente
                                'UPGRADE_WARNING: Couldn't resolve default property of object vgrdTemp.Cell(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                                vgrdTemp.Cell(CellPropertySettings.flexcpBackColor, vgrdTemp.Rows - 1, 1, vgrdTemp.Rows - 1, vgrdTemp.Cols - 1) = vgrdTemp.Cell(CellPropertySettings.flexcpBackColor, lngCpt - 1, 1, Nothing, Nothing)
                            End If
                        Else
                            'do nothing
                        End If
                    Else
                        'Continue
                    End If
                End If
            Next lngCpt
        Else
            'do nothing
        End If

Exit_fblnGRID_SetAltColor:
        fblnGRID_SetAltColor = blnReturn
        Exit Function

Error_fblnGRID_SetAltColor:
        blnReturn = False
        Call GerrLogError(Err, mstrCLASS_NAME, strFCT_NAME)
        Resume Exit_fblnGRID_SetAltColor
    End Function

    Public Function bln_Fill_WithDataTable(ByVal vdataTable As System.Data.DataTable) As Boolean
        On Error GoTo Error_bln_Fill_WithDataTable
        Const strFCT_NAME As String = "bln_Fill_WithDataTable"
        Dim blnReturn As Boolean
        Dim intScrollBarsStatus As Short
        Dim recRecordTemp As New ADODB.Recordset
        Dim intCpt As Integer

        If Not mblnIsInCodeGrid Then
            Call mcTTAPP.bln_FormStopUpdate(mfrmFormParent)
        End If

        mobjGRID.Redraw = C1.Win.C1FlexGrid.Classic.RedrawSettings.flexRDNone
        If mobjGRID.IsAccessible Then
            mobjGRID.Visible = False
        End If

        intScrollBarsStatus = mobjGRID.ScrollBars
        mobjGRID.ScrollBars = ScrollBars.None

        Select Case False
            Case fblnGrid_FillWithDataTable_Net(mobjGRID, vdataTable, Me.DisconnectedDataSource)
            Case Else

                mDataTable = vdataTable

                RaiseEvent SetDisplay()
                blnReturn = True

                Select Case False
                    Case pfblnGRID_SetDataCol()
                    Case pfblnGRID_GetRecordInfo()
                    Case Else
                        blnReturn = True
                End Select
        End Select

        If mChangedColumnSize.Count > 0 Then
            For Each Col As KeyValuePair(Of Integer, Integer) In mChangedColumnSize
                mobjGRID.ColWidth(Col.Key) = Col.Value
            Next
        Else
            'Do nothing
        End If

        If blnReturn And mblnResizeColumn Then
            GarderDonnéesPourResize()
            fblnResizeGrid()
        End If

Exit_bln_Fill_WithDataTable:
        mobjGRID.Redraw = C1.Win.C1FlexGrid.Classic.RedrawSettings.flexRDBuffered
        mobjGRID.Visible = True
        If Not mblnIsInCodeGrid Then
            Call mcTTAPP.bln_FormAllowUpdate()
        End If

        recRecordTemp = Nothing
        mobjGRID.ScrollBars = intScrollBarsStatus
        bln_Fill_WithDataTable = blnReturn
        Exit Function

Error_bln_Fill_WithDataTable:
        blnReturn = False
        Call GerrLogError(Err, mstrCLASS_NAME, strFCT_NAME)
        Resume Exit_bln_Fill_WithDataTable
    End Function

    Public Function bln_Fill_WithListe(ByVal vListe As clsListe(Of IItem)) As Boolean
        On Error GoTo Error_bln_Fill_WithListe
        Const strFCT_NAME As String = "bln_Fill_WithListe"
        Dim blnReturn As Boolean
        Dim intScrollBarsStatus As Short
        Dim recRecordTemp As New ADODB.Recordset
        Dim intCpt As Integer

        If Not mblnIsInCodeGrid Then
            Call mcTTAPP.bln_FormStopUpdate(mfrmFormParent)
        End If

        mobjGRID.Redraw = C1.Win.C1FlexGrid.Classic.RedrawSettings.flexRDNone
        If mobjGRID.IsAccessible Then
            mobjGRID.Visible = False
        End If

        intScrollBarsStatus = mobjGRID.ScrollBars
        mobjGRID.ScrollBars = ScrollBars.None

        Select Case False
            Case fblnGrid_FillWithListe_Net(mobjGRID, vListe)
            Case Else

                'mDataTable = vdataTable

                RaiseEvent SetDisplay()
                blnReturn = True

                Select Case False
                    Case pfblnGRID_SetDataCol()
                    Case pfblnGRID_GetRecordInfo()
                    Case Else
                        blnReturn = True
                End Select
        End Select

        If mChangedColumnSize.Count > 0 Then
            For Each Col As KeyValuePair(Of Integer, Integer) In mChangedColumnSize
                mobjGRID.ColWidth(Col.Key) = Col.Value
            Next
        Else
            'Do nothing
        End If

        If blnReturn And mblnResizeColumn Then
            GarderDonnéesPourResize()
        End If

Exit_bln_Fill_WithListe:
        mobjGRID.Redraw = C1.Win.C1FlexGrid.Classic.RedrawSettings.flexRDBuffered
        mobjGRID.Visible = True
        If Not mblnIsInCodeGrid Then
            Call mcTTAPP.bln_FormAllowUpdate()
        End If

        recRecordTemp = Nothing
        mobjGRID.ScrollBars = intScrollBarsStatus
        bln_Fill_WithListe = blnReturn
        Exit Function

Error_bln_Fill_WithListe:
        blnReturn = False
        Call GerrLogError(Err, mstrCLASS_NAME, strFCT_NAME)
        Resume Exit_bln_Fill_WithListe
    End Function

    '*************************************************************
    '*
    '* Name:        bln_CreateAndLinkGridObject
    '*
    '* Par:        	Michael,  2011-04-06
    '*
    '* Description: 
    '*
    '* IN:
    '* OUT: False si erreur, True si OK
    '*
    '*************************************************************
    Public Function bln_CreateAndLinkGridObject(ByVal vcTTApp As clsTTAPP, ByRef robjGrid As TT3DLL.ctlTTGrid) As Boolean
        On Error GoTo Error_bln_CreateAndLinkGridObject
        Const strFCT_NAME As String = "bln_CreateAndLinkGridObject"

        Dim blnReturn As Boolean

        blnReturn = True

        robjGrid = New TT3DLL.ctlTTGrid

        'vblnIsInCodeGrid est pour les grilles qu'on declare dans le code mais qu'on affiche pas
        mblnIsInCodeGrid = True

        robjGrid.Rows = 1
        robjGrid.FixedRows = 1
        robjGrid.Cols = 1
        robjGrid.FixedCols = 1

        blnReturn = bln_Init(vcTTApp, robjGrid, , , , , , , , , True)


Exit_bln_CreateAndLinkGridObject:
        bln_CreateAndLinkGridObject = blnReturn
        Exit Function

Error_bln_CreateAndLinkGridObject:
        blnReturn = False
        Call vcTTApp.bln_LogError(Err, mstrCLASS_NAME, strFCT_NAME)
        Resume Exit_bln_CreateAndLinkGridObject
    End Function

    '*************************************************************
    '*
    '* Name:        pfblnGrd_GetSelectedColors
    '*
    '* Par:         Nicolas,  2011-04-15
    '*
    '* Description:
    '*
    '* IN:
    '* OUT: False si erreur, True si OK
    '*
    '*************************************************************
    Private Function pfblnGrd_GetSelectedColors(ByRef robjGrid As TT3DLL.ctlTTGrid) As Boolean
        On Error GoTo Error_pfblnGrd_GetSelectedColors
        Const strFCT_NAME As String = "pfblnGrd_GetSelectedColors"
        Dim blnReturn As Boolean = True
        Dim strForeColor As String = gvbNullstring
        Dim strBackColor As String = gvbNullstring

        Select Case False
            Case mcTTAPP.bln_GetSysParam("Grid_SelectedForeColor", strForeColor)
            Case mcTTAPP.bln_GetSysParam("Grid_SelectedBackColor", strBackColor)
            Case Else
                If Trim(strForeColor) <> gvbNullstring Then
                    robjGrid.ForeColorSel = System.Drawing.ColorTranslator.FromHtml(strForeColor)
                End If

                If Trim(strBackColor) <> gvbNullstring Then
                    robjGrid.BackColorSel = System.Drawing.ColorTranslator.FromHtml(strBackColor)
                End If
        End Select

Exit_pfblnGrd_GetSelectedColors:
        pfblnGrd_GetSelectedColors = blnReturn
        Exit Function
Error_pfblnGrd_GetSelectedColors:
        blnReturn = False
        Resume Exit_pfblnGrd_GetSelectedColors
    End Function

    '****************************************************************
    '* Nom de la fonction   : pfblnAjusteLargeurGrille
    '*
    '*               Cree   : 05-05-2011  Eric
    '*            Modifie   : **-**-****  ***
    '*
    '*                But   :
    '*
    '* Parametre(s):
    '*              IN :
    '*
    '*              OUT:    True, si succes. False, si erreur
    '*
    '****************************************************************
    Private Function pfblnAjusteLargeurGrille(ByRef rintLargeurOri As Integer, _
                                              ByRef robjGrid As TT3DLL.ctlTTGrid, _
                                              ByVal vblnRestoreDefault As Boolean) As Boolean
        On Error GoTo Error_pfblnAjusteLargeurGrille
        Const strFCT_NAME As String = "pfblnAjusteLargeurGrille"
        Dim blnReturn As Boolean
        Dim intCpt As Integer
        Dim intDiff As Integer
        Dim intLargeur As Integer
        Dim dblRapport As Double

        blnReturn = True
        intLargeur = 0

        If Not vblnRestoreDefault Then

            If robjGrid.Cols > 0 Then

                For intCpt = 0 To robjGrid.Cols - 1

                    If robjGrid.ColHidden(intCpt) = False Then

                        intLargeur = intLargeur + robjGrid.ColWidth(intCpt)

                    End If

                Next

                If intLargeur <= mintMaxLargeurGrille_Print Then
                    'Si la grille est plus petite, on va élargir la dernière colonne
                    intDiff = mintMaxLargeurGrille_Print - intLargeur
                    rintLargeurOri = robjGrid.ColWidth(robjGrid.Cols - 1)
                    robjGrid.ColWidth(robjGrid.Cols - 1) = rintLargeurOri + intDiff
                Else
                    'On prend le plancher, on ajoute 1, la grille doit rentrer dans ce facteur x la largeur.
                    dblRapport = Math.Floor(intLargeur / mintMaxLargeurGrille_Print) + 1
                    intDiff = (dblRapport * mintMaxLargeurGrille_Print) - intLargeur
                    rintLargeurOri = robjGrid.ColWidth(robjGrid.Cols - 1)
                    robjGrid.ColWidth(robjGrid.Cols - 1) = rintLargeurOri + (intDiff - rintLargeurOri)

                End If

            Else
                'do nothing
            End If

        Else
            robjGrid.ColWidth(robjGrid.Cols - 1) = rintLargeurOri
        End If

Exit_pfblnAjusteLargeurGrille:
        pfblnAjusteLargeurGrille = blnReturn
        Exit Function

Error_pfblnAjusteLargeurGrille:
        blnReturn = False
        Call GerrLogError(Err, mstrCLASS_NAME, strFCT_NAME)
        Resume Exit_pfblnAjusteLargeurGrille
    End Function

    Private Sub mobjGRID_AfterResizeColumn(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles mobjGRID.AfterResizeColumn
        If mChangedColumnSize.ContainsKey(e.Col) Then
            mChangedColumnSize.Remove(e.Col)
            mChangedColumnSize.Add(e.Col, sender.colWidth(e.Col))
        Else
            mChangedColumnSize.Add(e.Col, sender.colWidth(e.Col))
        End If
    End Sub

    'Public Sub LoadControlLierGrille(ByRef listControl As Dictionary(Of Integer, Control))
    '    mdictControlLieeGrille = listControl
    'End Sub

    '****************************************************************
    '* Nom de la fonction   : LoadControlLierGrille
    '*
    '*               Cree   : 17-04-2015  Michel Pelland
    '*            Modifie   : **-**-****  ***
    '*
    '*                But   :
    '*
    '* Parametre(s):
    '*              IN : ParamArray -> No colonne, Control, No colonne, Control....
    '*
    '*              OUT:    True, si succes. False, si erreur
    '*
    '****************************************************************
    Public Function pfblnLoadControlLierGrille(ByVal ParamArray listControl() As Object) As Boolean
        On Error GoTo Error_pfblnLoadControlLierGrille
        Const strFCT_NAME As String = "pfblnLoadControlLierGrille"
        Dim blnReturn As Boolean = True
        Dim lngCptrCol As Integer
        Dim lngBoundField As Integer

        blnReturn = True
        mdictControlLieeGrille = New Dictionary(Of Integer, Control)

        If UBound(listControl) <> 0 Then
            If (UBound(listControl) + 1) Mod 2 <> 0 Then
                'Nombre d'argument non conforme dans le BoundList
                blnReturn = False
            Else
                'Arguments are OK
            End If
        Else
            'il doit y avoir des elements dans le ParamArray
            blnReturn = False
        End If

        If blnReturn Then
            For lngCptrCol = 0 To UBound(listControl) Step 2
                lngBoundField = lngCptrCol + 1
                If IsReference(listControl(lngBoundField)) Then
                    Select Case TypeName(listControl(lngBoundField))
                        Case "TextBox"
                            If TypeName(listControl(lngCptrCol)).Equals("Integer") Then
                                mdictControlLieeGrille.Add(listControl(lngCptrCol), listControl(lngBoundField))
                            End If
                        Case Else
                            'do nothing
                    End Select
                Else
                    blnReturn = False
                End If
            Next
        Else
            blnReturn = False
        End If

Exit_pfblnLoadControlLierGrille:
        pfblnLoadControlLierGrille = blnReturn
        Exit Function

Error_pfblnLoadControlLierGrille:
        blnReturn = False
        Call GerrLogError(Err, mstrCLASS_NAME, strFCT_NAME)
        Resume Exit_pfblnLoadControlLierGrille
    End Function


    '****************************************************************
    '* Nom de la fonction   : ResizeGrid
    '*
    '*               Cree   : 17-04-2015  Olivier Boyer
    '*            Modifie   : **-**-****  ***
    '*
    '*                Description: Fonction qui redimensionnent la largeur des colonnes visible de la grille lié
    '*                              à un clsTTVSFlex_net proportionellement à la différence de taille entre la taille d'origine de 
    '*                              la grille et la nouvelle taille.
    '*
    '* Parametre(s):
    '*              IN : 
    '*
    '*              OUT:    
    '*
    '****************************************************************
    Public Function fblnResizeGrid() As Boolean
        'On Error GoTo Error_ResizeGrid
        'Const strFCT_NAME As String = "ResizeGrid"
        Dim blnReturn As Boolean = True
        If Me IsNot Nothing Then
            If Me.Grid IsNot Nothing Then
                'Me.Grid.SuspendLayout()
                If (Me.AncienneTailleGrid.Width > 0 And Me.AncienneTailleGrid.Width <> Me.Grid.ClientSize.Width) Then
                    Dim cptVisible As Integer = 0
                    Dim diffClientSize As Integer

                    ' TODO : Voir pour opimiser. Mettre dans le load impossible -> Possibilité d'ajout de colonnes visibles après le load (Nécéssite gestion supplémentaire).
                    For Each colonne As C1.Win.C1FlexGrid.Column In Me.Grid.ColumnCollection
                        If colonne.Visible Then
                            cptVisible += 1
                        Else ' nothing
                        End If
                    Next

                    Dim blnPremiereCol As Boolean = True
                    diffClientSize = Math.Truncate(((Me.Grid.ClientSize.Width - Me.AncienneTailleGrid.Width) / (cptVisible - 1)))

                    Dim intCptVisiblePourControl As Integer = 0
                    Dim intPositionColonne As Integer = Me.Grid.Location.X

                    For Each colonne As C1.Win.C1FlexGrid.Column In Me.Grid.ColumnCollection
                        If blnPremiereCol Then
                            blnPremiereCol = False
                            intPositionColonne += colonne.Width
                        Else
                            If colonne.Visible Then
                                intCptVisiblePourControl += 1
                                If diffClientSize <> 0 Then
                                    colonne.Width += diffClientSize

                                    'Dim listTemp As List(Of KeyValuePair(Of Integer, Control)) = mdictControlLieeGrille.ToList.FindAll(Function(x) x.Key = intCptVisiblePourControl)

                                    'For Each element As KeyValuePair(Of Integer, Control) In listTemp
                                    '    SyncLock element.Value
                                    '        element.Value.Width = colonne.Width - 3
                                    '        element.Value.Location = New Point(intPositionColonne, element.Value.Location.Y)
                                    '    End SyncLock
                                    'Next

                                    Dim listTemp As List(Of KeyValuePair(Of Integer, Control)) = mdictControlLieeGrille.ToList.FindAll(Function(x) x.Key = intCptVisiblePourControl)

                                    For Each element As KeyValuePair(Of Integer, Control) In listTemp
                                        SyncLock element.Value
                                            If Not element.Value.Text.Equals("") Then
                                                element.Value.Visible = True
                                                element.Value.Width = colonne.Width - 3
                                                element.Value.Location = New Point(intPositionColonne, Me.Grid.Location.Y + Me.Grid.Height + 2)
                                            Else


                                                element.Value.Visible = False
                                            End If
                                        End SyncLock
                                    Next


                                    intPositionColonne += colonne.Width
                                End If
                            Else
                            End If
                        End If
                    Next
                    mobjGRID.ScrollPosition = New Point(0, 0)
                Else ' Nothing
                End If
                Me.AncienneTailleGrid = Me.Grid.ClientSize
                'Me.Grid.ResumeLayout()
            End If
        End If

Exit_ResizeGrid:
        fblnResizeGrid = blnReturn
        Exit Function

        'Error_ResizeGrid:
        '        blnReturn = False
        '        Call GerrLogError(Err, mstrCLASS_NAME, strFCT_NAME)
        '        Resume Exit_ResizeGrid
    End Function

    '****************************************************************
    '* Nom de la fonction   : fblnPlacerTextBoxTotal
    '*
    '*               Cree   : 17-04-2015  Olivier Boyer
    '*            Modifie   : **-**-****  ***
    '*
    '*                Description: Replace les textbox enregistrés 
    '*
    '* Parametre(s):
    '*              IN : 
    '*
    '*              OUT:    
    '*
    '****************************************************************
    Public Sub fblnPlacerTextBoxTotal()
        Dim intPositionColonne As Integer = Me.Grid.Location.X
        Dim blnPremiereCol As Boolean = True
        Dim intCptVisiblePourControl As Integer = 0

        For Each colonne As C1.Win.C1FlexGrid.Column In Me.Grid.ColumnCollection
            If blnPremiereCol Then
                blnPremiereCol = False
                intPositionColonne += colonne.Width
            Else
                If colonne.Visible Then
                    intCptVisiblePourControl += 1

                    Dim listTemp As List(Of KeyValuePair(Of Integer, Control)) = mdictControlLieeGrille.ToList.FindAll(Function(x) x.Key = intCptVisiblePourControl)

                    For Each element As KeyValuePair(Of Integer, Control) In listTemp
                        SyncLock element.Value
                            If Not element.Value.Text.Equals("") Then
                                element.Value.Visible = True
                                element.Value.Width = colonne.Width - 3
                                element.Value.Location = New Point(intPositionColonne, Me.Grid.Location.Y + Me.Grid.Height + 2)
                            Else
                                element.Value.Visible = False
                            End If
                        End SyncLock
                    Next

                    intPositionColonne += colonne.Width
                End If
            End If
        Next
    End Sub

    Private Sub AfterResize() Handles mobjGRID.AfterResizeColumn
        fblnPlacerTextBoxTotal()
    End Sub


    Private Sub mobjGRID_resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mobjGRID.Resize

        If mblnResizeColumn Then
            fblnResizeGrid()
        Else 'nothing
        End If
    End Sub

    ''' <summary>
    ''' Ensemble des opérations à faire dans une fonction fill pour la fonction resize
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GarderDonnéesPourResize()
        AncienneTailleGrid = TailleOrigineGrid
    End Sub
End Class
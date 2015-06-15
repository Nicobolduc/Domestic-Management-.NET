<System.ComponentModel.ToolboxItem(True), System.ComponentModel.DesignTimeVisible(True)> _
Public Class ctlTTGrid
    Inherits C1.Win.C1FlexGrid.Classic.C1FlexGridClassic
    Implements System.ComponentModel.IComponent
    Implements IDisposable

    ' Conserver une référence vers l'objet global en cours.
    Private mcTTapp As TT3DLL.clsTTAPP

    Private components As System.ComponentModel.IContainer
    Friend WithEvents imgList As System.Windows.Forms.ImageList
    'Couleurs VB6
    Public Const vbBlack As Long = &H80000008

    Private Const mintExportExcelQuestion_msg As Integer = 900307

    
    Private mExportToExcelHandler As clsTTExcel.BeforeExportXLSHandler

    ' Récupérer ou définir la delegate pour l'export vers Excel.
    Public Property ExportToExcel() As clsTTExcel.BeforeExportXLSHandler
        Get
            Return Me.mExportToExcelHandler
        End Get
        Set(ByVal value As clsTTExcel.BeforeExportXLSHandler)
            Me.mExportToExcelHandler = value
        End Set
    End Property

    Public Property RowHidden(ByVal vintRow As Integer) As Boolean
        Get
            Return MyBase.get_RowHidden(vintRow)
        End Get
        Set(ByVal value As Boolean)
            MyBase.set_RowHidden(vintRow, value)
        End Set
    End Property

    Public Property cTTApp() As TT3DLL.clsTTAPP
        Get
            Return Me.mcTTapp
        End Get
        Set(ByVal value As TT3DLL.clsTTAPP)
            Me.mcTTapp = value
        End Set
    End Property

    Public Function bln_ExportToExcel(Optional ByVal vfctXLSBeforeExportHander As clsTTExcel.BeforeExportXLSHandler = Nothing) As Boolean
        Call pfblnExportToExcel()

        bln_ExportToExcel = True
    End Function

    Public Overloads Property Cell(ByVal Setting As C1.Win.C1FlexGrid.Classic.CellPropertySettings, Optional ByVal Row1 As String = "", Optional ByVal Col1 As String = "", Optional ByVal Row2 As String = "", Optional ByVal Col2 As String = "") As Object
        Get
            'Cell(Setting As CellPropertySettings, [R1 As Long], [C1 As Long], [R2 As Long], [C2 As Long]) [ = Value ]
            'MP: quand on specifie pas row ou col, on doit passer "" a la fonction master et non nothing ou 

            'If Setting = C1.Win.C1FlexGrid.Classic.CellPropertySettings.flexcpPicture Then

            '    Me.Select(Row1, Col1)

            '    Return Me.CellPicture
            'Else
            '    Return Me.get_Cell(Setting, Row1, Col1, Row2, Col2)
            'End If

            If Row2 = gvbNullstring And Col2 = gvbNullstring Then
                'Row1 = 3
                'Col1 = 4
                Return Me.get_Cell(Setting, Row1, Col1)
            Else
                'Si un des 2 derniers n'est pas la, on presume que c'est la meme chose que le premier parametre
                'ex.: cell(1,10,,20) --> (1,10,1,20)
                Return Me.get_Cell(Setting, Row1, Col1, IIf(Row2 = gvbNullstring, Row1, Row2), IIf(Col2 = gvbNullstring, Col1, Col2))
            End If

        End Get

        Set(ByVal value As Object)
            Dim objCellStyle As C1.Win.C1FlexGrid.CellStyle
            Dim objCellRange As C1.Win.C1FlexGrid.CellRange
            Dim strKey As String = gvbnullstring

            Select Case Setting
                Case C1.Win.C1FlexGrid.Classic.CellPropertySettings.flexcpBackColor, _
                    C1.Win.C1FlexGrid.Classic.CellPropertySettings.flexcpForeColor

                    If Not TypeOf (value) Is System.Drawing.Color Then
                        value = System.Drawing.ColorTranslator.FromOle(value)
                    End If
            End Select

            If (Setting = C1.Win.C1FlexGrid.Classic.CellPropertySettings.flexcpBackColor) Then
                strKey = String.Format("{0}_{1}", Setting.ToString(), value.ToString())
                If (Me.Styles.Contains(strKey)) Then
                    objCellStyle = Me.Styles(strKey)
                Else
                    objCellStyle = Me.Styles.Add(strKey, "Normal")
                End If

                objCellStyle.BackColor = value
                objCellRange = Me.GetCellRange(Row1, Col1, IIf(Row2 = gvbNullstring, Row1, Row2), IIf(Col2 = gvbNullstring, Col1, Col2))
                objCellRange.Style = objCellStyle
            Else
                If Row2 = gvbNullstring And Col2 = gvbNullstring Then
                    Me.set_Cell(Setting, Row1, Col1, value)
                Else
                    Me.set_Cell(Setting, Row1, Col1, IIf(Row2 = gvbNullstring, Row1, Row2), IIf(Col2 = gvbNullstring, Col1, Col2), value)
                End If
            End If

            objCellRange = Nothing
            objCellStyle = Nothing
        End Set
    End Property


    Public Property TextMatrix(ByVal vlngRow As Integer, ByVal vlngCol As Integer) As String
        Get
            Dim objTest As String = Nothing
            If (vlngRow <= Me.Rows - 1 And vlngCol <= Me.Cols - 1) Then
                objTest = Me.get_TextMatrix(vlngRow, vlngCol)
            End If

            Select Case False
                Case vlngRow > 0
                Case vlngCol <= Me.Cols - 1
                Case Not (IsNothing(Me.get_ColDataType(vlngCol)))
                Case UCase(Me.get_ColDataType(vlngCol).Name) = "BOOLEAN"
                Case Else
                    objTest = IIf(objTest = "True" Or objTest = "1", "1", "0")
            End Select

            If objTest Is Nothing Then
                objTest = gvbNullstring
            Else
                'do nothing
            End If

            Return objTest
        End Get
        Set(ByVal value As String)
            If (vlngRow <= Me.Rows - 1 And vlngCol <= Me.Cols - 1) Then

                If Not (IsNothing(Me.get_ColDataType(vlngCol))) Then
                    If (UCase(Me.get_ColDataType(vlngCol).Name) = "DATE") Or (UCase(Me.get_ColDataType(vlngCol).Name) = "DATETIME") Then
                        Me.set_TextMatrix(vlngRow, vlngCol, IIf(value <> gvbNullstring, value, Nothing))
                    Else
                        Me.set_TextMatrix(vlngRow, vlngCol, value)
                    End If
                Else
                    Me.set_TextMatrix(vlngRow, vlngCol, value)
                End If

                'If Not Me.get_ColDataType(vlngCol).Equals(Nothing) Then
                '    If value = gvbNullstring And (UCase(Me.get_ColDataType(vlngCol).Name) = "DATE" Or UCase(Me.get_ColDataType(vlngCol).Name) = "DATETIME") Then
                'Me.set_TextMatrix(vlngRow, vlngCol, Nothing)
                'End If

            End If
        End Set
    End Property
    Public Property FormatString() As String
        Get
            If Not mcTTapp Is Nothing Then
                Return mcTTapp.str_GetCaption(Me.Tag, mcTTapp.cUser.Langue, Nothing)
            Else
                Return gvbNullstring
            End If
        End Get
        Set(ByVal value As String)
            Dim intCpt As Integer
            Dim varCol() As String
            Dim strTemp As String = gvbNullstring
            Dim s As Size
            Dim intStartPosition As Integer = 0

            varCol = Split(value, "|")

            Me.Cols = UBound(varCol) + 1

            Me.set_ColWidth(0, 5)

            For intCpt = 1 To UBound(varCol)
                If Len(value) > 0 Then
                    strTemp = Mid(varCol(intCpt), 1, 1)
                Else
                    strTemp = gvbNullstring
                End If

                intStartPosition = 2
                Select Case strTemp
                    Case "<"
                        Me.set_FixedAlignment(intCpt, AlignmentSettings.flexAlignLeftCenter)
                        Me.set_ColAlignment(intCpt, AlignmentSettings.flexAlignLeftCenter)
                    Case ">"
                        Me.set_FixedAlignment(intCpt, AlignmentSettings.flexAlignRightCenter)
                        Me.set_ColAlignment(intCpt, AlignmentSettings.flexAlignRightCenter)
                    Case "^"
                        Me.set_FixedAlignment(intCpt, AlignmentSettings.flexAlignCenterCenter)
                        Me.set_ColAlignment(intCpt, AlignmentSettings.flexAlignCenterCenter)
                    Case Else
                        intStartPosition = 1
                        'do nothing
                End Select

                If Len(varCol(intCpt)) > 1 Then
                    Me.TextMatrix(0, intCpt) = Mid(varCol(intCpt), intStartPosition)
                    s = TextRenderer.MeasureText(Me.TextMatrix(0, intCpt), Me.Font)
                    Me.set_ColWidth(intCpt, s.Width)
                Else
                    Me.TextMatrix(0, intCpt) = gvbNullstring
                    Me.set_ColWidth(intCpt, 5)
                End If
            Next

            'Dim grdOld As New VSFlex7.VSFlexGrid

            'Le position du premier pipe (0) represente avant le pipe, on ne l'utilise pas

            'grdOld.FormatString = value

            'grdOld.Cols = UBound(Split(value, "|")) + 1

            'Me.Cols = grdOld.Cols

            'For intCpt = 0 To grdOld.Cols - 1
            '    Me.set_ColWidth(intCpt, flngConvertTwipsToPixel(grdOld.ColWidth(intCpt)))
            '    Me.set_FixedAlignment(intCpt, grdOld.ColAlignment(intCpt))
            '    Me.set_ColAlignment(intCpt, grdOld.ColAlignment(intCpt))
            '    Me.TextMatrix(0, intCpt) = IIf(grdOld.TextMatrix(0, intCpt) = Nothing, gvbNullstring, grdOld.TextMatrix(0, intCpt))
            'Next

            'grdOld = Nothing

            s = Nothing
        End Set
    End Property
    'Public Shadows ReadOnly Property FindRow(ByVal Item As Object, Optional ByVal Row As String = "", Optional ByVal Col As String = "", Optional ByVal CaseSensitive As Boolean = True, Optional ByVal FullMatch As Boolean = True, Optional ByVal vblnCallBack As Boolean = True) As Long
    '    Get
    '        'FindRow(Item As Variant, [ Row As Long ], [ Col As Long ], [ CaseSensitive As Boolean ], [ FullMatch As Boolean])

    '        'MP: quand on specifie pas row ou col, on doit passer "" a la fonction master et non nothing ou 0
    '        'MP: CaseSensitive et FullMatch sont les defauts dans la documentation de VsFlexGrid

    '        Return Me.FindRow(Item, Row, Col, CaseSensitive, FullMatch, False)


    '    End Get
    'End Property

    Public Property ColHidden(ByVal vlngCol As Integer) As Boolean
        Get
            Return Me.get_ColHidden(vlngCol)
        End Get
        Set(ByVal value As Boolean)
            Me.set_ColHidden(vlngCol, value)
        End Set
    End Property

    Public Property ColWidth(ByVal vlngCol As Integer) As Integer
        Get
            Return Me.get_ColWidth(vlngCol)
        End Get
        Set(ByVal value As Integer)
            Me.set_ColWidth(vlngCol, value)
        End Set
    End Property

    Public Property ColFormat(ByVal vlngCol As Integer) As String
        Get
            Return Me.get_ColFormat(vlngCol)
        End Get
        Set(ByVal value As String)
            Me.set_ColFormat(vlngCol, value)
        End Set
    End Property

    Public Property ColDataType(ByVal vlngCol As Integer) As System.Type
        Get
            Return Me.get_ColDataType(vlngCol)
        End Get
        Set(ByVal value As System.Type)
            Me.set_ColDataType(vlngCol, value)
        End Set
    End Property

    Public Property ColAlignment(ByVal vlngCol As Integer) As C1.Win.C1FlexGrid.Classic.AlignmentSettings
        Get
            Return Me.get_ColAlignment(vlngCol)
        End Get
        Set(ByVal value As C1.Win.C1FlexGrid.Classic.AlignmentSettings)
            Me.set_ColAlignment(vlngCol, value)
        End Set
    End Property

    Public Shadows Event [DblClick](ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Shadows Event [AfterSort](ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.SortColEventArgs)

    Public Sub New()
        AddHandler MyBase.DoubleClick, AddressOf Grid_DblClick
        AddHandler MyBase.AfterSort, AddressOf Grid_AfterSort
    End Sub

    Public Sub Grid_AfterSort(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.SortColEventArgs)
        If Me.Rows > 1 And Me.Row > 0 And Me.Row <= Me.Rows Then
            Me.Select(Me.Row, e.Col, True)
        End If
        RaiseEvent AfterSort(sender, e)
    End Sub

    Public Sub Grid_DblClick(ByVal sender As Object, ByVal e As System.EventArgs)


        Select Case True
            Case Me.MouseCol = 0 And Me.MouseRow = 0
                Call pfblnExportToExcel()
               

            Case Me.MouseCol <> 0 And Me.MouseRow <> 0
                ' L'évnement double-click ne sera plus déclenché si on double click dans le header de row ou de colonne
                RaiseEvent DblClick(sender, e)
        End Select
    End Sub
    Public Function pfblnExportToExcel() As Boolean
        Dim intAnswer As Integer

        ' Si on a défini l'évênement ExportToExcell, et l'utilisateur double-click sur le premier coin de la grille (haut-gauche, intersection des header de row et de col
        '  on propose à l'utilisateur d'exporter la grille sous Excell.

        Select Case False
            Case Not Me.mcTTapp Is Nothing
            Case Me.mcTTapp.bln_ShowAnswerMSG(mintExportExcelQuestion_msg, vbQuestion + vbYesNo, intAnswer)
            Case intAnswer = vbYes
            Case Else
                Me.mcTTapp.bln_TransfertGridToExcel(Me, , Me.ExportToExcel)
        End Select


        pfblnExportToExcel = True
    End Function
    Public Shadows Sub set_RowPosition(ByVal vintOldPosition As Integer, ByVal vintNewPosition As Integer)
        Dim myDataRow As DataRow
        Dim myDataRowNew As DataRow
        Dim blnAPasserDansDataTable As Boolean

        If TypeOf (DataSource) Is DataTable Then
            If DataSource.Rows.Count > 0 And vintOldPosition > 0 And vintNewPosition > 0 Then
                myDataRow = DataSource.Rows(vintOldPosition - 1)
                myDataRowNew = DataSource.NewRow
                myDataRowNew.ItemArray = myDataRow.ItemArray
                DataSource.Rows.Remove(myDataRow)
                DataSource.Rows.InsertAt(myDataRowNew, vintNewPosition - 1)
                blnAPasserDansDataTable = True
            End If
        End If

        'myDataTable = DataSource
        'myDataRow = myDataTable.Rows(vintOldPosition - 1)
        'myDataTable.Rows.RemoveAt(vintOldPosition - 1)
        'myDataTable.Rows.InsertAt(myDataRow, vintNewPosition - 1)
        'DataSource = myDataTable
        If Not blnAPasserDansDataTable Then
            MyBase.set_RowPosition(vintOldPosition, vintNewPosition)
        End If

        MyBase.Row = vintNewPosition
        myDataRow = Nothing
        myDataRowNew = Nothing
    End Sub

    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ctlTTGrid))
        Me.imgList = New System.Windows.Forms.ImageList(Me.components)
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'imgList
        '
        Me.imgList.ImageStream = CType(resources.GetObject("imgList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgList.TransparentColor = System.Drawing.Color.Transparent
        Me.imgList.Images.SetKeyName(0, "PrintExcel")
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class

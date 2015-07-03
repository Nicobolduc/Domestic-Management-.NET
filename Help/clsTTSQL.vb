Public Sub CreateFormFromString(ByVal eventArgs As TT3OCX.ctlTTListeGenTool.OpenFormEventArgs)
        Dim strFormName As String = gvbNullString
        Dim frmTemp As Object = Nothing

        strFormName = System.Reflection.Assembly.GetEntryAssembly.GetName.Name & "." & eventArgs.vstrFormToOpen
        frmTemp = System.Reflection.Assembly.GetEntryAssembly.CreateInstance(strFormName, True)

       

    End Sub



Locale = GetUserDefaultLCID()
            iRet1 = GetLocaleInfo(Locale, LOCALE_SSHORTDATE, lpLCDataVar, 0)
            Symbol = New String(Chr(0), iRet1)

            iRet2 = GetLocaleInfo(Locale, LOCALE_SSHORTDATE, Symbol, iRet1)
            Pos = InStr(Symbol, Chr(0))

            If Pos > 0 Then
                Symbol = Left(Symbol, Pos - 1)
                'FORMAT DU SHORT DATE DU PC
                gstrPCShortDateFormat = Symbol
            End If
'GET PC INFO
	Declare Function GetLocaleInfo Lib "kernel32"  Alias "GetLocaleInfoA"(ByVal Locale As Integer, ByVal LCType As Integer, ByVal lpLCData As String, ByVal cchData As Integer) As Integer



Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("clsTTSQL_NET.clsTTSQL")> Public Class clsTTSQL
    Implements System.Collections.IEnumerable

    Private Const mstrCLASS_NAME As String = "clsTTSQL"

    Public mcnAdoConnection As ADODB.Connection

    Private mCol As Collection
    Private mblnInit As Boolean
    Private mblnRefresh As Boolean
    Private mblnTimeStamp_OK As Boolean
    Private mblnTimeZoneValid As Boolean
    Private mstrItemAppelant As String = gvbnullstring
    Private mstrTransactionSQL As String = gvbnullstring
    Private mlngUserToLog As Long
    Private mintTransactionCount As Integer = 0
    Private mintTimeZoneHeure As Integer
    Private mintTimeZoneMinute As Integer

    Public TransactionStarted As Boolean
    Public SilentMode As Boolean = False

    Public Event ErreurTimeStamp()
    Public Event ErreurSave()
    Public Event ErreurSQL(ByRef lngErrNumber As Integer)

    Public Property UserToLog() As Long
        Get
            UserToLog = mlngUserToLog
        End Get

        Set(ByVal value As Long)
            mlngUserToLog = value
        End Set

    End Property
    Private Function Add(ByRef strField As String, ByRef vValue As Object, ByRef intDataType As clsConstante.TTFieldType, Optional ByRef sKey As String = "") As clsRecord
        Dim objNewMember As New clsRecord

        objNewMember.strField = strField
        'UPGRADE_WARNING: Couldn't resolve default property of object vValue. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        objNewMember.varValue = vValue
        objNewMember.intDataType = intDataType
        If Len(sKey) = 0 Then
            mCol.Add(objNewMember)
        Else
            mCol.Add(objNewMember, sKey)
        End If

        Add = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing

    End Function

    Private ReadOnly Property Item(ByVal vntIndexKey As Object) As clsRecord
        Get
            'used when referencing an element in the collection
            'vntIndexKey contains either the Index or Key to the collection,
            'this is why it is declared as a Variant
            'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
            Item = mCol.Item(vntIndexKey)
        End Get
    End Property

    Public ReadOnly Property Count() As Integer
        Get
            'used when retrieving the number of elements in the
            'collection. Syntax: Debug.Print x.Count
            Count = mCol.Count()
        End Get
    End Property

    'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
    'Public ReadOnly Property NewEnum() As stdole.IUnknown
    'Get
    'this property allows you to enumerate
    'this collection with the For...Each syntax
    'NewEnum = mCol._NewEnum
    'End Get
    'End Property

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
        GetEnumerator = mCol.GetEnumerator
    End Function

    ReadOnly Property FieldsCount() As Short
        Get
            FieldsCount = Me.Count
        End Get
    End Property

    ReadOnly Property FieldCheck(ByVal vstrFieldName As String) As Boolean
        Get
            Dim intCpt As Short

            FieldCheck = False

            For intCpt = 1 To Me.Count
                If UCase(Item(intCpt).strField) = UCase(vstrFieldName) Then
                    FieldCheck = True
                    Exit For
                Else
                    'do nothing
                End If
            Next intCpt

        End Get
    End Property

    ReadOnly Property FieldValue(ByVal vstrFieldName_Index As String, Optional ByVal vblnIsIndex As Boolean = False) As Object
        Get
            Dim intCpt As Short

            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            'UPGRADE_WARNING: Couldn't resolve default property of object FieldValue. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            FieldValue = System.DBNull.Value

            If vblnIsIndex Then
                If Val(vstrFieldName_Index) <= Me.Count Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object Item().varValue. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object FieldValue. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    FieldValue = Item(Val(vstrFieldName_Index)).varValue
                Else
                    'error
                End If
            Else
                For intCpt = 1 To Me.Count
                    If UCase(Item(intCpt).strField) = UCase(vstrFieldName_Index) Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object Item().varValue. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        'UPGRADE_WARNING: Couldn't resolve default property of object FieldValue. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        FieldValue = Item(intCpt).varValue
                        Exit For
                    Else
                        'do nothing
                    End If
                Next intCpt
            End If

        End Get
    End Property
    ReadOnly Property FieldDataType(ByVal vstrFieldName_Index As String, Optional ByVal vblnIsIndex As Boolean = False) As Object
        Get
            Dim intCpt As Short

            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            'UPGRADE_WARNING: Couldn't resolve default property of object FieldDataType. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            FieldDataType = System.DBNull.Value

            If vblnIsIndex Then
                If Val(vstrFieldName_Index) <= Me.Count Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object FieldDataType. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    FieldDataType = Item(Val(vstrFieldName_Index)).intDataType
                Else
                    'error
                End If
            Else
                For intCpt = 1 To Me.Count
                    If UCase(Item(intCpt).strField) = UCase(vstrFieldName_Index) Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object FieldDataType. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        FieldDataType = Item(intCpt).intDataType
                        Exit For
                    Else
                        'do nothing
                    End If
                Next intCpt
            End If

        End Get
    End Property

    ReadOnly Property FieldName(ByVal vintIndex As Short) As String
        Get
            If vintIndex <= Me.Count Then
                FieldName = Item(vintIndex).strField
            Else
                FieldName = gvbNullstring
            End If
        End Get
    End Property

    Public Sub Remove(ByRef vntIndexKey As Object)
        'used when removing an element from the collection
        'vntIndexKey contains either the Index or Key, which is why
        'it is declared as a Variant
        'Syntax: x.Remove(xyz)
        mCol.Remove(vntIndexKey)
    End Sub

    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        'creates the collection when this class is created
        mCol = New Collection

        'Par defaut, utilise la connection du dll.
        'UPGRADE_NOTE: Object mcnAdoConnection may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mcnAdoConnection = Nothing
        mcnAdoConnection = gcnADOConnect

        TransactionStarted = False
        mstrTransactionSQL = gvbNullstring

        If mcnAdoConnection Is Nothing Then
            mblnInit = False
        Else
            mblnInit = True
        End If

        mstrItemAppelant = gvbNullstring
    End Sub
    Private Sub Class_Initialize_Renamed(ByRef rcnAdoConnection As ADODB.Connection)
        'creates the collection when this class is created
        mCol = New Collection

        'Par defaut, utilise la connection du dll.
        'UPGRADE_NOTE: Object mcnAdoConnection may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mcnAdoConnection = Nothing
        mcnAdoConnection = rcnAdoConnection

        TransactionStarted = False
        mstrTransactionSQL = gvbNullstring

        If mcnAdoConnection Is Nothing Then
            mblnInit = False
        Else
            mblnInit = True
        End If

        mstrItemAppelant = gvbNullstring
    End Sub

    Private Sub Class_Initialize_Renamed(ByVal vblnInitializeConnection As Boolean)
        'creates the collection when this class is created
        mCol = New Collection

        'Par defaut, utilise la connection du dll.
        'UPGRADE_NOTE: Object mcnAdoConnection may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mcnAdoConnection = Nothing

        If vblnInitializeConnection Then
            mcnAdoConnection = gcnADOConnect
        Else
            mcnAdoConnection = New ADODB.Connection
        End If

        TransactionStarted = False
        mstrTransactionSQL = gvbNullstring

        If mcnAdoConnection Is Nothing Then
            mblnInit = False
        Else
            mblnInit = True
        End If

        mstrItemAppelant = gvbNullstring
    End Sub

    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub
    Public Sub New(ByRef rcnAdoConnection As ADODB.Connection)
        MyBase.New()
        Class_Initialize_Renamed(rcnAdoConnection)
    End Sub

    Public Sub New(ByVal vblnInitializeConnection As Boolean)
        MyBase.New()
        Class_Initialize_Renamed(vblnInitializeConnection)
    End Sub

    'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Terminate_Renamed()
        'on appel le endtrans au cas ou une transaction serait ouverte
        Call bln_ADOEndTrans(False)
        'destroys collection when this class is terminated
        'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mCol = Nothing
        'UPGRADE_NOTE: Object mcnAdoConnection may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mcnAdoConnection = Nothing
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub

    'New colRecord
    Public Function bln_Refresh(Optional ByVal vlngEntNRI As Integer = 0) As Boolean
        On Error GoTo Error_bln_Refresh
        Const strFCT_NAME As String = "bln_Refresh"
        Dim blnReturn As Boolean
        Dim strColumnName As String = String.Empty
        Dim strSQL As String = gvbNullString
        Dim recRecord As New ADODB.Recordset

        If mblnInit Then
            blnReturn = True
            mblnRefresh = True
            mCol = New Collection
            mblnTimeZoneValid = False

            Select Case False
                ' Déterminer si utilise décalage horaire.
                Case vlngEntNRI <> 0
                Case Else

                    ' Détermienr si c'est un entrepôt ou un fuseau horaire.
                    If (vlngEntNRI > 0) Then
                        blnReturn = False
                        Select Case False
                            Case Me.bln_ADOSingleLookUp("syscolumns inner join sysobjects on syscolumns.id = sysobjects.id and sysobjects.name = 'Entrepot' and sysobjects.xtype = 'U'", "syscolumns.name", strColumnName, "syscolumns.name = 'Ent_DiffHr'")
                            Case strColumnName.Length > 0
                                blnReturn = True
                            Case bln_ADOSelect("SELECT Ent_DiffHr, Ent_DiffMin FROM Entrepot WHERE Ent_NRI = " & vlngEntNRI, recRecord)
                            Case Else
                                mblnTimeZoneValid = True
                                blnReturn = True
                        End Select
                    Else
                        blnReturn = False
                        Select Case False
                            Case Me.bln_ADOSingleLookUp("syscolumns inner join sysobjects on syscolumns.id = sysobjects.id and sysobjects.name = 'FuseauHoraire' and sysobjects.xtype = 'U'", "syscolumns.name", strColumnName, "syscolumns.name = 'FuH_DB_DiffHr'")
                            Case strColumnName.Length > 0
                                blnReturn = True
                            Case bln_ADOSelect("SELECT isnull(FuH_DB_DiffHr, 0) as Ent_DiffHr, isnull(FuH_DB_DiffMin, 0) as Ent_DiffMin FROM FuseauHoraire WHERE FuH_NRI = " & vlngEntNRI, recRecord)
                            Case Else
                                mblnTimeZoneValid = True
                                blnReturn = True
                        End Select
                    End If

                    If blnReturn And mblnTimeZoneValid And Not recRecord.EOF Then
                        mintTimeZoneHeure = recRecord("Ent_DiffHr").Value
                        mintTimeZoneMinute = recRecord("Ent_DiffMin").Value
                    End If
            End Select
        Else
            blnReturn = False
            mblnRefresh = False
        End If


Exit_bln_Refresh:
        recRecord = Nothing
        bln_Refresh = blnReturn
        Exit Function

Error_bln_Refresh:
        blnReturn = False
        If Not SilentMode Then
            Call GerrLogError(Err, mstrItemAppelant & ":" & mstrCLASS_NAME, strFCT_NAME)
        End If
        Resume Exit_bln_Refresh
    End Function

    Public Function bln_ChangeConnection(ByVal vcnConnect As ADODB.Connection) As Boolean
        On Error GoTo Error_bln_ChangeConnection
        Const strFCT_NAME As String = "bln_ChangeConnection"
        Dim blnReturn As Boolean

        blnReturn = True

        mcnAdoConnection = vcnConnect

Exit_bln_ChangeConnection:
        bln_ChangeConnection = blnReturn
        Exit Function

Error_bln_ChangeConnection:
        blnReturn = False
        If Not SilentMode Then
            Call GerrLogError(Err, mstrItemAppelant & ":" & mstrCLASS_NAME, strFCT_NAME)
        End If
        Resume Exit_bln_ChangeConnection
    End Function

    Public Function bln_AddField(ByVal vstrFieldName As String, ByVal vValue As Object, ByVal vintFieldType As clsConstante.TTFieldType, Optional ByVal vblnRequired As Boolean = False) As Boolean
        On Error GoTo Error_bln_AddField
        Const strFCT_NAME As String = "bln_AddField"
        Dim blnReturn As Booleanhsjsjsjsjs

        If mblnRefresh = True Then
            If IsNothing(vValue) Then
                vValue = System.DBNull.Value
            Else

                If TypeOf vValue Is ADODB.Field Then
                    vValue = DirectCast(vValue, ADODB.Field).Value
                End If

                Select Case TypeName(vValue)
                    Case "CheckState"
                        If vValue = Windows.Forms.CheckState.Checked Then
                            vValue = 1
                        Else
                            vValue = 0
                        End If
                    Case "CheckBox"
                        vValue = IIf(vValue.checkstate = CheckState.Checked, 1, 0)
                    Case "TextBox"
                        vValue = vValue.text
                    Case Else
                        'do nothing
                End Select
            End If

            If vblnRequired And (IsDBNull(vValue) Or vValue.ToString() = gvbNullstring) Then
                blnReturn = False

            ElseIf Not vblnRequired And (IsDBNull(vValue) Or vValue.ToString() = gvbNullstring) Then
                vValue = System.DBNull.Value
                blnReturn = True
            Else
                'VAlide le type
                Select Case vintFieldType
                    Case clsConstante.TTFieldType.TTFT_BOOLEAN, clsConstante.TTFieldType.TTFT_INTEGER, clsConstante.TTFieldType.TTFT_FLAOT, clsConstante.TTFieldType.TTFT_NRI, clsConstante.TTFieldType.TTFT_TINYINT
                        blnReturn = IsNumeric(vValue)

                    Case clsConstante.TTFieldType.TTFT_DATETIME, clsConstante.TTFieldType.TTFT_DATE, clsConstante.TTFieldType.TTFT_DATETIME_SEC
                        blnReturn = IsDate(vValue)

                        If blnReturn And mblnTimeZoneValid Then
                            vValue = pfstrSetDateToTimeZone(vValue)
                        End If

                    Case clsConstante.TTFieldType.TTFT_VARCHAR, clsConstante.TTFieldType.TTFT_NVARCHAR
                        vValue = Trim(vValue)

                        blnReturn = True

                    Case Else
                        'Do nothing
                End Select
            End If
        Else
            blnReturn = False
        End If

        If blnReturn Then
            Call Add(vstrFieldName, vValue, vintFieldType)
        End If


Exit_bln_AddField:
        bln_AddField = blnReturn
        Exit Function

Error_bln_AddField:
        blnReturn = False
        If Not SilentMode Then
            Call GerrLogError(Err, mstrItemAppelant & ":" & mstrCLASS_NAME, strFCT_NAME)
        End If
        Resume Exit_bln_AddField
    End Function

    Private Sub psSQLfor_INSERT(ByRef strSQL As String, ByVal vlngEntNRI As Long, Optional ByVal vstrNRIField As String = gvbNullstring, Optional ByVal vlngNewItemNRI As Long = 0)
        Dim strSQLINTO As String = gvbnullstring
        Dim strSQLVALUES As String = gvbnullstring
        Dim intField As Short
        Dim vValue As Object

        If vstrNRIField <> gvbNullstring Then
            strSQLINTO = " (" & vstrNRIField & ", "
            strSQLVALUES = " VALUES ( " & vlngNewItemNRI & ", "
        Else
            strSQLINTO = " ("
            strSQLVALUES = " VALUES ( "
        End If

        For intField = 1 To Me.Count

            strSQLINTO = strSQLINTO & " " & Item(intField).strField & ","
            'UPGRADE_WARNING: Couldn't resolve default property of object vValue. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            vValue = Nothing

            Call fblnSQL_GetRealValue(Item(intField).intDataType, Item(intField).varValue, vValue, vlngEntNRI, mcnAdoConnection)

            'UPGRADE_WARNING: Couldn't resolve default property of object vValue. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            strSQLVALUES = strSQLVALUES & " " & vValue & ","

        Next intField

        strSQLINTO = Mid(strSQLINTO, 1, Len(strSQLINTO) - 1) & " )"
        strSQLVALUES = Mid(strSQLVALUES, 1, Len(strSQLVALUES) - 1) & " )"
        strSQL = strSQLINTO & vbCrLf & strSQLVALUES & vbCrLf

    End Sub

    Private Sub psSQLfor_UPDATE(ByRef rstrSQL As String, ByVal vlngEntNRI As Long)
        Dim strSQLVALUES As String = gvbNullstring
        Dim intField As Short
        Dim vValue As Object = Nothing
        Dim strField As String = gvbNullstring

        strSQLVALUES = gvbNullstring

        For intField = 1 To Me.Count
            Call fblnSQL_GetRealValue(Item(intField).intDataType, Item(intField).varValue, vValue, vlngEntNRI, mcnAdoConnection)

            strField = Item(intField).strField
            'UPGRADE_WARNING: Couldn't resolve default property of object vValue. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            strSQLVALUES = strSQLVALUES & vbCrLf & " " & strField & " = " & vValue & ","

        Next intField

        strSQLVALUES = Mid(strSQLVALUES, 1, Len(strSQLVALUES) - 1)
        rstrSQL = strSQLVALUES

    End Sub

    Public Function bln_ADOSelect(ByVal vstrSQL As String, ByRef rrecRecord As ADODB.Recordset, Optional ByVal vlngMaxRow As Integer = 0) As Boolean
        On Error GoTo Error_bln_ADOSelect
        Const strFCT_NAME As String = "bln_ADOSelect"
        Dim blnReturn As Boolean
        Dim recRecord As ADODB.Recordset = Nothing
        Dim lngError As Integer
        Dim intCount As Short

        rrecRecord = New ADODB.Recordset

        If mblnInit Then
            '2008-06-20 : Keep trace of SQL strings of the transaction
            If Not SilentMode Then
                Call pfblnKeepTrace(vstrSQL)
            End If

            On Error Resume Next
            intCount = 1
            Do While fblnCheckExecute(Err.Number, intCount, mcnAdoConnection)
                recRecord = New ADODB.Recordset
                recRecord.CursorLocation = ADODB.CursorLocationEnum.adUseClient
                recRecord.MaxRecords = vlngMaxRow
                recRecord.Open(vstrSQL, mcnAdoConnection, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)

                intCount = intCount + 1
            Loop

            If Err.Number <> 0 Then
                GoTo Error_bln_ADOSelect
            Else
                'Do nothing
            End If

            On Error GoTo Error_bln_ADOSelect
            'UPGRADE_NOTE: Object recRecord.ActiveConnection may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            recRecord.ActiveConnection = Nothing
            'UPGRADE_NOTE: Object rrecRecord may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            rrecRecord = Nothing
            rrecRecord = recRecord
            blnReturn = True
        Else
            blnReturn = False
        End If


Exit_bln_ADOSelect:
        bln_ADOSelect = blnReturn
        'UPGRADE_NOTE: Object recRecord may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        recRecord = Nothing
        Exit Function

Error_bln_ADOSelect:
        blnReturn = False
        lngError = Err.Number
        If Not SilentMode Then
            Call GerrLogError(Err, mstrItemAppelant & ":" & mstrCLASS_NAME, strFCT_NAME & vbCrLf & vstrSQL)
            RaiseEvent ErreurSQL(lngError)
        End If

        'Resume Exit_bln_ADOSelect
        GoTo Exit_bln_ADOSelect
    End Function

    Public Function bln_ADOInsert(ByVal vstrTABLE As String, Optional ByRef rlngNewNRI As Integer = 0, Optional ByVal vlngEntNRi As Long = 0) As Boolean
        On Error GoTo Error_bln_ADOInsert
        Const strFCT_NAME As String = "bln_ADOInsert"
        Dim blnReturn As Boolean
        Dim strSQL As String = String.Empty
        Dim intCount As Short
        Dim recRecord As ADODB.Recordset = Nothing

        If mblnRefresh And mblnInit Then
            Call psSQLfor_INSERT(strSQL, vlngEntNRi)

            'Call(fblnTTLog(Me, clsConstante.TTFormMode.TTFM_INSERT, vstrTABLE, gvbNullstring, mlngUserToLog)) TEST

            strSQL = " INSERT INTO " & vstrTABLE & "  " & strSQL
            'strSQL = "SET NOCOUNT ON " & strSQL & " SELECT @@IDENTITY as NewNRI " & " SET NOCOUNT OFF"
            strSQL = "SET NOCOUNT ON " & strSQL & " SELECT SCOPE_IDENTITY() as NewNRI " & " SET NOCOUNT OFF"


            '2008-06-20 : Keep trace of SQL strings of the transaction
            If Not SilentMode Then
                Call pfblnKeepTrace(strSQL)
            End If

            On Error Resume Next
            intCount = 1
            Do While fblnCheckExecute(Err.Number, intCount, mcnAdoConnection)
                recRecord = New ADODB.Recordset
                recRecord.Open(strSQL, mcnAdoConnection)
                intCount = intCount + 1
            Loop

            If Err.Number <> 0 Then
                GoTo Error_bln_ADOInsert
            Else
                'Do nothing
            End If

            On Error GoTo Error_bln_ADOInsert

            If Not recRecord.EOF Then
                'verification dans le cas d'un ajout dans une table sans cle primaire

                rlngNewNRI = IIf(Not IsDBNull(recRecord.Fields("NewNRI").Value), recRecord.Fields("NewNRI").Value, 0)
                fblnTTLog(Me, clsConstante.TTFormMode.TTFM_INSERT, vstrTABLE, gvbNullstring, mlngUserToLog, rlngNewNRI) 'TEST

                blnReturn = True
            Else
                rlngNewNRI = 0
                blnReturn = False
            End If
        Else
            blnReturn = False
        End If

        mblnRefresh = False

Exit_bln_ADOInsert:
        bln_ADOInsert = blnReturn
        'UPGRADE_NOTE: Object recRecord may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        recRecord = Nothing
        Exit Function

Error_bln_ADOInsert:
        blnReturn = False
        If Not SilentMode Then
            Call GerrLogError(Err, mstrItemAppelant & ":" & mstrCLASS_NAME, strFCT_NAME)
        End If

        'Resume Exit_bln_ADOInsert
        GoTo Exit_bln_ADOInsert
    End Function


    '****************************************************************
    '* Nom de la fonction   : bln_ExecuteSPOutParam
    '*
    '*               Cree   : 26-06-2008  Nicolas
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
    Public Function bln_ADOExecuteSPOutParam(ByVal vstrSPSQL As String, ByVal vstrOutParamName As String, ByRef rstrReturn As String) As Boolean
        On Error GoTo Error_bln_ExecuteSPOutParam
        Const strFCT_NAME As String = "bln_ExecuteSPOutParam"
        Dim blnReturn As Boolean
        Dim strSQL As String = gvbNullstring
        Dim recRecord As ADODB.Recordset

        blnReturn = True

        recRecord = New ADODB.Recordset

        strSQL = " DECLARE " & vstrOutParamName & " VARCHAR(8000)" & vbCrLf
        strSQL = strSQL & " EXEC " & vstrSPSQL & vbCrLf
        strSQL = strSQL & " SELECT " & vstrOutParamName & vbCrLf

        blnReturn = fblnRecord_SELECT(strSQL, recRecord, mcnAdoConnection)

        If blnReturn And Not recRecord.EOF Then
            rstrReturn = recRecord.Fields(0).Value
        End If

Exit_bln_ADOExecuteSPOutParam:
        bln_ADOExecuteSPOutParam = blnReturn
        Exit Function
Error_bln_ExecuteSPOutParam:
        blnReturn = False
        If Not SilentMode Then
            Call GerrLogError(Err, mstrCLASS_NAME, strFCT_NAME)
        End If

        Resume Exit_bln_ADOExecuteSPOutParam
    End Function

    Public Function bln_ADOUpdate(ByVal vstrTABLE As String, Optional ByVal vstrWHere As String = "", Optional ByRef rlngRowAffected As Integer = 0, Optional ByVal vlngEntNRI As Long = 0) As Boolean
        On Error GoTo Error_bln_ADOUpdate
        Const strFCT_NAME As String = "bln_ADOUpdate"
        Dim blnReturn As Boolean
        Dim strSQL As String = gvbNullstring
        Dim lngError As Integer
        Dim intCount As Short

        If mblnRefresh And mblnInit Then
            'Pour appliquer le TimeZone, on devrait savoir c'est quel entrepôt
            If vlngEntNRI = 0 Then
                vlngEntNRI = glngEnt_NRI
            Else
                'Do nothing
            End If

            Call psSQLfor_UPDATE(strSQL, vlngEntNRI)

            Call fblnTTLog(Me, clsConstante.TTFormMode.TTFM_UPDATE, vstrTABLE, vstrWHere, mlngUserToLog)

            strSQL = " UPDATE " & vstrTABLE & "  SET " & strSQL

            If vstrWHere <> gvbNullstring Then
                strSQL = strSQL & vbCrLf & " WHERE " & vstrWHere
            Else
                'do nothing
            End If

            '2008-06-20 : Keep trace of SQL strings of the transaction
            Call pfblnKeepTrace(strSQL)

            On Error Resume Next
            intCount = 1
            Do While fblnCheckExecute(Err.Number, intCount, mcnAdoConnection)
                Call mcnAdoConnection.Execute(strSQL, rlngRowAffected)
                intCount = intCount + 1
            Loop

            If Err.Number <> 0 Then
                GoTo Error_bln_ADOUpdate
            Else
                'Do nothing
            End If

            blnReturn = True
        Else
            blnReturn = False
        End If

        mblnRefresh = False

Exit_bln_ADOUpdate:
        bln_ADOUpdate = blnReturn
        Exit Function

Error_bln_ADOUpdate:
        blnReturn = False
        lngError = Err.Number
        If Not SilentMode Then
            Call GerrLogError(Err, mstrItemAppelant & ":" & mstrCLASS_NAME, strFCT_NAME)
            RaiseEvent ErreurSQL(lngError)
        End If

        'Resume Exit_bln_ADOUpdate
        GoTo Exit_bln_ADOUpdate
    End Function

    Public Function bln_ADODelete(ByVal vstrTABLE As String, ByVal vstrWHere As String, Optional ByRef rlngRowAffected As Integer = 0) As Boolean
        On Error GoTo Error_bln_ADODelete
        Const strFCT_NAME As String = "bln_ADODelete"
        Dim blnReturn As Boolean
        Dim strSQL As String = gvbNullstring
        Dim intCount As Short

        If mblnInit Then
            Call fblnTTLog(Me, clsConstante.TTFormMode.TTFM_DELETE, vstrTABLE, vstrWHere, mlngUserToLog)

            strSQL = "DELETE FROM " & vstrTABLE
            If vstrWHere <> gvbNullstring Then
                strSQL = strSQL & " WHERE " & vstrWHere
            Else
                'Do nothing
            End If

            '2008-06-20 : Keep trace of SQL strings of the transaction
            If Not SilentMode Then
                Call pfblnKeepTrace(strSQL)
            End If

            On Error Resume Next
            intCount = 1
            Do While fblnCheckExecute(Err.Number, intCount, mcnAdoConnection)
                Call mcnAdoConnection.Execute(strSQL, rlngRowAffected)
                intCount = intCount + 1
            Loop

            If Err.Number <> 0 Then
                GoTo Error_bln_ADODelete
            Else
                'Do nothing
            End If

            blnReturn = True
        Else
            blnReturn = False
        End If


Exit_bln_ADODelete:
        bln_ADODelete = blnReturn
        Exit Function

Error_bln_ADODelete:
        blnReturn = False
        If Not SilentMode Then
            Call GerrLogError(Err, mstrItemAppelant & ":" & mstrCLASS_NAME, strFCT_NAME)
        End If

        'Resume Exit_bln_ADODelete
        GoTo Exit_bln_ADODelete
    End Function

    Public Function bln_ADOEndTrans(ByVal vblnCanCommit As Boolean, Optional ByVal vblnCloseCurrentTransOnly As Boolean = False, Optional ByVal vblnSilentMode As Boolean = False) As Boolean
        On Error GoTo Error_bln_ADOEndTrans
        Const strFCT_NAME As String = "bln_ADOEndTrans"
        Dim blnReturn As Boolean

        blnReturn = True

        'MP: Le close current est utilise dans l'architecture de Dev1. On met la variable pour compatibilité.

        If TransactionStarted And Not (mcnAdoConnection Is Nothing) Then
            Do While mintTransactionCount > 0
                If vblnCanCommit And mblnTimeStamp_OK Then
                    mcnAdoConnection.CommitTrans()
                Else
                    mcnAdoConnection.RollbackTrans()
                    'Write into Log file the SQL strings executed during the transaction
                    If Not vblnSilentMode And Not SilentMode And TransactionStarted And (mstrTransactionSQL <> gvbNullstring) And gintSQLTRACE = 1 Then
                        Call GerrLogError(Err, mstrItemAppelant & ":" & mstrCLASS_NAME, strFCT_NAME & vbCrLf & mstrTransactionSQL)
                    Else
                        'Do nothing
                    End If
                End If

                mintTransactionCount = mintTransactionCount - 1
                gintTransactionCountGlobal = gintTransactionCountGlobal - 1

                TransactionStarted = False
                mstrTransactionSQL = gvbNullstring

                If mintTransactionCount = 0 Then

                    Select Case False
                        Case mblnTimeStamp_OK
                            RaiseEvent ErreurTimeStamp()
                        Case vblnCanCommit
                            If Not vblnSilentMode And Not SilentMode Then
                                RaiseEvent ErreurSave()
                            End If
                        Case Else
                            'Do nothing
                    End Select
                Else
                    'do nothing
                End If
            Loop
        Else
            ' Do nothing : Pas de transaction à terminer!
        End If


Exit_bln_ADOEndTrans:
        bln_ADOEndTrans = blnReturn
        Exit Function

Error_bln_ADOEndTrans:
        blnReturn = False
        If Not SilentMode Then
            Call GerrLogError(Err, mstrItemAppelant & ":" & mstrCLASS_NAME, strFCT_NAME)
        End If

        Resume Exit_bln_ADOEndTrans
    End Function

    Public Function bln_ADOBeginTrans(ByVal vstrItemAppelant As String) As Boolean
        On Error GoTo Error_bln_ADOBeginTrans
        Const strFCT_NAME As String = "bln_ADOBeginTrans"
        Dim blnReturn As Boolean

        blnReturn = True

        If Not mcnAdoConnection Is Nothing Then
            mblnTimeStamp_OK = True
            mcnAdoConnection.BeginTrans()
            TransactionStarted = True
            mstrTransactionSQL = gvbNullstring

            mintTransactionCount = mintTransactionCount + 1
            gintTransactionCountGlobal = gintTransactionCountGlobal + 1

            mstrItemAppelant = vstrItemAppelant
        Else
            'do nothing
        End If


Exit_bln_ADOBeginTrans:
        bln_ADOBeginTrans = blnReturn
        Exit Function

Error_bln_ADOBeginTrans:
        blnReturn = False
        If Not SilentMode Then
            Call GerrLogError(Err, mstrItemAppelant & ":" & mstrCLASS_NAME, strFCT_NAME)
        End If

        Resume Exit_bln_ADOBeginTrans
    End Function

    'UPGRADE_WARNING: ParamArray vvarTableExceptionList was changed from ByRef to ByVal. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="93C6A0DC-8C99-429A-8696-35FC4DCEFCCC"'
    Public Function bln_ADOCheckRefIntegrity(ByVal vstrForeignKeyName As String, ByVal vlngForeignKeyValue As Integer, ByRef rblnCanDelete As Boolean, ByVal ParamArray vvarTableExceptionList() As Object) As Boolean
        On Error GoTo Error_bln_ADOCheckRefIntegrity
        Const strFCT_NAME As String = "bln_ADOCheckRefIntegrity"
        Dim blnReturn As Boolean
        Dim varParam As Object

        'UPGRADE_WARNING: Couldn't resolve default property of object varParam. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        varParam = vvarTableExceptionList
        blnReturn = False
        Select Case False
            Case pfblnCheckRefIntegrity(vstrForeignKeyName, vlngForeignKeyValue, rblnCanDelete, varParam)
            Case Else
                blnReturn = True
        End Select


Exit_bln_ADOCheckRefIntegrity:
        bln_ADOCheckRefIntegrity = blnReturn
        Exit Function

Error_bln_ADOCheckRefIntegrity:
        blnReturn = False
        If Not SilentMode Then
            Call GerrLogError(Err, mstrItemAppelant & ":" & mstrCLASS_NAME, strFCT_NAME)
        End If

        Resume Exit_bln_ADOCheckRefIntegrity
    End Function

    Public Function bln_ADOSingleLookUp(ByVal vstrTABLE As String, ByVal vstrField As String, ByRef rstrReturnval As String, Optional ByVal vstrWHere As String = "") As Boolean
        On Error GoTo Err_bln_ADOSingleLookUp
        Const strFCT_NAME As String = "bln_ADOSingleLookUp"
        Dim blnReturn As Boolean

        blnReturn = pfblnSingleLookUp(vstrTABLE, vstrField, rstrReturnval, vstrWHere)


Exit_bln_ADOSingleLookUp:
        bln_ADOSingleLookUp = blnReturn
        Exit Function

Err_bln_ADOSingleLookUp:
        blnReturn = False
        If Not SilentMode Then
            Call GerrLogError(Err, mstrItemAppelant & ":" & mstrCLASS_NAME, strFCT_NAME)
        End If

        Resume Exit_bln_ADOSingleLookUp
    End Function

    Public Function bln_ADOValideTS(ByVal vstrTABLE As String, ByVal vstrFieldNRI As String, ByVal vlngItemNRI As Integer, ByVal vstrFieldTS As String, ByVal vlngItemTS As Integer, ByRef rblnValide As Boolean) As Boolean
        On Error GoTo Error_bln_ADOValideTS
        Const strFCT_NAME As String = "bln_ADOValideTS "
        Dim blnReturn As Boolean

        rblnValide = False
        blnReturn = pfblnValideTS(vstrTABLE, vstrFieldNRI, vlngItemNRI, vstrFieldTS, vlngItemTS, rblnValide)

        If mblnTimeStamp_OK Then
            mblnTimeStamp_OK = rblnValide
        Else
            'Do nothing
        End If


Exit_bln_ADOValideTS:
        bln_ADOValideTS = blnReturn
        Exit Function

Error_bln_ADOValideTS:
        blnReturn = False
        If Not SilentMode Then
            Call GerrLogError(Err, mstrItemAppelant & ":" & mstrCLASS_NAME, strFCT_NAME)
        End If

        Resume Exit_bln_ADOValideTS
    End Function

    '****************************************************************
    '* Nom de la fonction   : bln_ADOExecute
    '*
    '*               Cree   : 18-10-2001   lamorin
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
    Public Function bln_ADOExecute(ByVal vstrSQL As String, Optional ByRef rlngRowAffected As Integer = 0, Optional ByRef vRecRec As ADODB.Recordset = Nothing, Optional ByVal vblnByPassMsg As Boolean = False) As Boolean
        On Error GoTo Error_bln_ADOExecute
        Const strFCT_NAME As String = "bln_ADOExecute"
        Dim blnReturn As Boolean
        Dim lngError As Integer
        Dim intCount As Short

        '2008-06-20 : Keep trace of SQL strings of the transaction
        If Not SilentMode Then
            Call pfblnKeepTrace(vstrSQL)
        End If

        On Error Resume Next
        intCount = 1
        Do While fblnCheckExecute(Err.Number, intCount, mcnAdoConnection)
            vRecRec = New ADODB.Recordset
            vRecRec = mcnAdoConnection.Execute(vstrSQL, rlngRowAffected)
            intCount = intCount + 1
        Loop

        If Not vblnByPassMsg Then
            If Err.Number <> 0 Then
                GoTo Error_bln_ADOExecute
            Else
                'Do nothing
            End If
        Else
            'Ce paramètre est utilisé pour la facturation d'entreposage, on ne veut pas de message provenant des storeprocedure à cause des prints dans les storeprocedure
        End If

        blnReturn = True

Exit_bln_ADOExecute:
        bln_ADOExecute = blnReturn
        Exit Function

Error_bln_ADOExecute:
        blnReturn = False
        lngError = Err.Number
        If Not SilentMode Then
            Call GerrLogError(Err, mstrItemAppelant & ":" & mstrCLASS_NAME, strFCT_NAME)
            RaiseEvent ErreurSQL(lngError)
        End If

        'Resume Exit_bln_ADOExecute
        GoTo Exit_bln_ADOExecute
    End Function

    Private Function pfblnCheckRefIntegrity(ByVal vstrForeignKeyName As String, ByVal vlngForeignKeyValue As Integer, ByRef rblnValide As Boolean, Optional ByRef vvarTableExceptionList As Object = Nothing) As Boolean
        On Error GoTo Error_pfblnCheckRefIntegrity
        Const strFCT_NAME As String = "pfblnCheckRefIntegrity"
        Dim blnReturn As Boolean
        Dim intCptr As Short
        Dim strReturn As String = gvbNullstring
        Dim strSQL As String = gvbNullstring
        Dim recTables As New ADODB.Recordset
        Dim strExeptTableList As String = gvbNullstring

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        blnReturn = True
        rblnValide = True
        strExeptTableList = gvbNullstring

        For intCptr = 0 To UBound(vvarTableExceptionList)
            'UPGRADE_WARNING: Couldn't resolve default property of object vvarTableExceptionList(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            strExeptTableList = strExeptTableList & "'" & vvarTableExceptionList(intCptr) & "',"
        Next

        'trim last comma
        strExeptTableList = Left(strExeptTableList, Len(strExeptTableList) - 1)

        strSQL = " SELECT DISTINCT  NomTable, NomCleReel " & vbCrLf
        strSQL = strSQL & " FROM   TTCheckIntegrity " & vbCrLf
        strSQL = strSQL & " WHERE      NomTable NOT IN ( " & strExeptTableList & ")" & vbCrLf
        strSQL = strSQL & " AND NomCle = '" & vstrForeignKeyName & "'"

        'Appel sa propre fonction
        Call bln_ADOSelect(strSQL, recTables)

        Do While Not recTables.EOF
            strSQL = gvbNullstring
            blnReturn = bln_ADOSingleLookUp(recTables.Fields("NomTable").Value, recTables.Fields("NomCleReel").Value, strReturn, recTables.Fields("NomCleReel").Value & " = " & vlngForeignKeyValue)

            If blnReturn Then
                If strReturn <> gvbNullstring Then
                    rblnValide = False
                    Exit Do
                Else
                    'Valid for this table
                End If
            Else
                rblnValide = False
                Exit Do
            End If
            recTables.MoveNext()
        Loop


Exit_pfblnCheckRefIntegrity:
        'UPGRADE_ISSUE: Unable to determine which constant to upgrade vbNormal to. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3B44E51-B5F1-4FD7-AA29-CAD31B71F487"'
        'UPGRADE_ISSUE: Screen property Screen.MousePointer does not support custom mousepointers. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="45116EAB-7060-405E-8ABE-9DBB40DC2E86"'
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'UPGRADE_NOTE: Object recTables may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        recTables = Nothing
        pfblnCheckRefIntegrity = blnReturn
        Exit Function

Error_pfblnCheckRefIntegrity:
        blnReturn = CBool(gvbNullstring)
        If Not SilentMode Then
            Call GerrLogError(Err, mstrItemAppelant & ":" & mstrCLASS_NAME, strFCT_NAME)
        End If

        Resume Exit_pfblnCheckRefIntegrity
    End Function

    Private Function pfblnSingleLookUp(ByVal vstrTABLE As String, ByVal vstrField As String, ByRef rstrReturnval As String, Optional ByVal vstrWHere As String = "") As Boolean
        On Error GoTo Error_pfblnSingleLookUp
        Const strFCT_NAME As String = "pfblnSingleLookUp"
        Dim blnReturn As Boolean
        Dim rRsRecord As New ADODB.Recordset
        Dim strSQL As String = gvbNullstring
        Dim intCount As Short

        blnReturn = True
        strSQL = gvbNullstring

        strSQL = "SELECT " & vstrField & " FROM " & vstrTABLE

        If vstrWHere <> gvbNullstring Then
            strSQL = strSQL & " WHERE " & vstrWHere
        Else
            'Pas de clause Where
        End If

        '2008-06-20 : Keep trace of SQL strings of the transaction
        Call pfblnKeepTrace(strSQL)

        On Error Resume Next
        intCount = 1
        Do While fblnCheckExecute(Err.Number, intCount, mcnAdoConnection)
            rRsRecord = New ADODB.Recordset
            rRsRecord.Open(strSQL, mcnAdoConnection, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockReadOnly)
            intCount = intCount + 1
        Loop

        If Err.Number <> 0 Then
            GoTo Error_pfblnSingleLookUp
        Else
            'Do nothing
        End If

        On Error GoTo Error_pfblnSingleLookUp

        If Not rRsRecord.EOF Then
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            If Not IsDBNull(rRsRecord.Fields(0).Value) Then
                rstrReturnval = rRsRecord.Fields(0).Value
            Else
                rstrReturnval = gvbNullstring
            End If
        Else
            rstrReturnval = gvbNullstring
        End If


Exit_pfblnSingleLookUp:
        'UPGRADE_NOTE: Object rRsRecord may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        rRsRecord = Nothing
        pfblnSingleLookUp = blnReturn
        Exit Function

Error_pfblnSingleLookUp:
        blnReturn = False
        If Not SilentMode Then
            Call GerrLogError(Err, mstrItemAppelant & ":" & mstrCLASS_NAME, strFCT_NAME & vbCrLf & strSQL)
        End If

        'Resume Exit_pfblnSingleLookUp
        GoTo Exit_pfblnSingleLookUp
    End Function

    Private Function pfblnValideTS(ByVal vstrTABLE As String, ByVal vtrFieldNRI As String, ByVal vlngItemNRI As Integer, ByVal vstrFieldTS As String, ByVal vlngItemTS As Integer, ByRef rblnValide As Boolean) As Boolean
        On Error GoTo Error_pfblnValideTS
        Const strFCT_NAME As String = "pfblnValideTS "
        Dim blnReturn As Boolean
        Dim strSQL As String = gvbNullstring
        Dim lngOK As Integer
        Dim strTemp As String = gvbNullstring

        rblnValide = False
        blnReturn = True

        'MP : on peut diffficilement et aleatoirement se baser sur le row affected. Si on a des triggers, le output peut afficher plusieurs rows affected.
        'Donc, on lookup au debut, si ca matche, on update le TS

        Call bln_ADOSingleLookUp(vstrTABLE, vtrFieldNRI, strTemp, vtrFieldNRI & " = " & vlngItemNRI & " AND " & vstrFieldTS & " = " & vlngItemTS)

        If strTemp <> "" Then
            strSQL = "SET NOCOUNT OFF UPDATE " & vstrTABLE & " SET " & vbCrLf
            strSQL = strSQL & vstrFieldTS & " = " & vlngItemTS + 1 & vbCrLf
            strSQL = strSQL & " WHERE " & vtrFieldNRI & " = " & vlngItemNRI & vbCrLf
            strSQL = strSQL & " AND " & vstrFieldTS & " = " & vlngItemTS & vbCrLf

            blnReturn = bln_ADOExecute(strSQL, lngOK)

        End If

        lngOK = IIf(strTemp <> "" And blnReturn, 1, 0)

        If lngOK = 1 Then
            rblnValide = True
        Else
            rblnValide = False

            If Not SilentMode Then
                If TransactionStarted Then
                    '2008-06-20 : Keep trace of SQL strings of the transaction
                    Call pfblnKeepTrace(strSQL)
                Else
                    Call GerrLogError(Err, mstrItemAppelant & ":" & mstrCLASS_NAME & ": Check of TimeStamp failed for table " & vstrTABLE & " NRI:" & vlngItemNRI, strFCT_NAME)
                End If
            End If

        End If

Exit_pfblnValideTS:
        pfblnValideTS = blnReturn
        Exit Function

Error_pfblnValideTS:
        blnReturn = False
        If Not SilentMode Then
            Call GerrLogError(Err, mstrItemAppelant & ":" & mstrCLASS_NAME, strFCT_NAME)
        End If

        Resume Exit_pfblnValideTS
    End Function

    '****************************************************************
    '* Nom de la fonction   : bln_SetAdoConnection
    '*
    '*               Cree   : 02-02-2000  LAM
    '*            Modifie   : **-**-****  ***
    '*
    '*                But   : Connect to DataBase
    '*
    '* Parametre(s):
    '*              IN :
    '*
    '*              OUT:    True, si succes. False, si erreur
    '*
    '****************************************************************
    Public Function bln_SetAdoConnection(ByVal vstrUser As String, ByVal vstrPW As String, ByVal vstrDatabase As String, ByVal vstrServer As String) As Boolean
        On Error GoTo Error_bln_SetAdoConnection
        Const strFCT_NAME As String = "bln_SetAdoConnection"
        Dim blnReturn As Boolean
        Dim strConnectString As String = gvbNullstring

        blnReturn = True

        mcnAdoConnection = New ADODB.Connection

        mcnAdoConnection.Provider = "SQLOLEDB.1"
        strConnectString = "Persist Security Info=False;User ID=" & vstrUser & ";PWD=" & vstrPW & ";Initial Catalog=" & vstrDatabase & ";Data Source=" & vstrServer
        mcnAdoConnection.ConnectionString = strConnectString
        mcnAdoConnection.CursorLocation = ADODB.CursorLocationEnum.adUseClient
        mcnAdoConnection.IsolationLevel = ADODB.IsolationLevelEnum.adXactReadCommitted
        mcnAdoConnection.Open()
        mcnAdoConnection.CommandTimeout = 180

        mblnInit = True
        blnReturn = True


Exit_bln_SetAdoConnection:
        bln_SetAdoConnection = blnReturn
        Exit Function

Error_bln_SetAdoConnection:
        blnReturn = False
        If Not SilentMode Then
            Call GerrLogError(Err, mstrItemAppelant & ":" & mstrCLASS_NAME, strFCT_NAME)
        End If

        Resume Exit_bln_SetAdoConnection
    End Function

    '****************************************************************
    '* Nom de la fonction   : bln_AddFieldIfMissing
    '*
    '*               Cree   : 29-11-2007  ¶émi
    '*            Modifie   : **-**-****  ***
    '*
    '*                But   : même principe que AddField mais ajoute seulement si le champ n'est pas déjà dans la classe
    '*
    '* Parametre(s):
    '*              IN :
    '*
    '*              OUT:    True, si succes. False, si erreur
    '*
    '****************************************************************
    Public Function bln_AddFieldIfMissing(ByVal vstrFieldName As String, ByVal vValue As Object, ByVal vintFieldType As clsConstante.TTFieldType, Optional ByVal vblnRequired As Boolean = False) As Boolean
        On Error GoTo Error_bln_AddFieldIfMissing
        Const strFCT_NAME As String = "bln_AddFieldIfMissing"
        Dim blnReturn As Boolean

        blnReturn = True

        If Not FieldCheck(vstrFieldName) Then
            If mblnRefresh = True Then
                If IsNothing(vValue) Then
                    vValue = System.DBNull.Value
                Else
                    Select Case TypeName(vValue)
                        Case "CheckState"
                            If vValue = Windows.Forms.CheckState.Checked Then
                                vValue = 1
                            Else
                                vValue = 0
                            End If
                        Case "CheckBox"
                            vValue = IIf(vValue.checkstate.checked, 1, 0)
                        Case "TextBox"
                            vValue = vValue.text
                        Case Else
                            'do nothing
                    End Select
                End If

                If vblnRequired And (IsDBNull(vValue) Or vValue.ToString() = gvbNullstring) Then
                    blnReturn = False

                ElseIf Not vblnRequired And (IsDBNull(vValue) Or vValue.ToString() = gvbNullstring) Then
                    vValue = System.DBNull.Value
                    blnReturn = True

                Else
                    'VAlide le type
                    Select Case vintFieldType
                        Case clsConstante.TTFieldType.TTFT_BOOLEAN, clsConstante.TTFieldType.TTFT_INTEGER, clsConstante.TTFieldType.TTFT_FLAOT, clsConstante.TTFieldType.TTFT_NRI, clsConstante.TTFieldType.TTFT_TINYINT
                            blnReturn = IsNumeric(vValue)
                        Case clsConstante.TTFieldType.TTFT_DATETIME, clsConstante.TTFieldType.TTFT_DATE, clsConstante.TTFieldType.TTFT_DATETIME_SEC
                            blnReturn = IsDate(vValue)
                        Case clsConstante.TTFieldType.TTFT_VARCHAR
                            'UPGRADE_WARNING: Couldn't resolve default property of object vValue. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                            vValue = Trim(vValue)
                            blnReturn = True
                        Case Else
                            'Do nothing
                    End Select
                End If
            Else
                blnReturn = False
            End If

            If blnReturn Then
                Call Add(vstrFieldName, vValue, vintFieldType)
            End If
        Else
            'do nothing
        End If

Exit_bln_AddFieldIfMissing:
        bln_AddFieldIfMissing = blnReturn
        Exit Function
Error_bln_AddFieldIfMissing:
        blnReturn = False
        If Not SilentMode Then
            Call GerrLogError(Err, mstrItemAppelant & ":" & mstrCLASS_NAME, strFCT_NAME)
        End If

        Resume Exit_bln_AddFieldIfMissing
    End Function

    '****************************************************************
    '* Nom de la fonction   : pfblnKeepTrace
    '*
    '*               Cree   : 07-08-2001  Rani
    '*            Modifie   : **-**-****  ****
    '*
    '*                But   :
    '*
    '* Parametre(s):
    '*              IN :
    '*
    '*              OUT:    True, si succes. False, si erreur
    '*
    '****************************************************************
    Private Function pfblnKeepTrace(ByVal vstrSQL As String) As Boolean
        On Error GoTo Error_pfblnKeepTrace
        Const strFCT_NAME As String = "pfblnKeepTrace"
        Dim blnReturn As Boolean

        blnReturn = True

        Select Case False
            Case vstrSQL <> gvbNullstring
            Case gintSQLTRACE = 1
            Case TransactionStarted
                mstrTransactionSQL = vstrSQL
            Case Else
                'SQLs de toute la transaction jusqu'au DeadLock ou Erreur
                mstrTransactionSQL = mstrTransactionSQL & IIf(mstrTransactionSQL <> gvbNullstring, vbCrLf, gvbNullstring)
                mstrTransactionSQL = mstrTransactionSQL & vstrSQL
        End Select

Exit_pfblnKeepTrace:
        pfblnKeepTrace = blnReturn
        Exit Function

Error_pfblnKeepTrace:
        blnReturn = False
        Call GerrLogError(Err, mstrItemAppelant & ":" & mstrCLASS_NAME, strFCT_NAME)
        Resume Exit_pfblnKeepTrace
    End Function

    Public Function bln_ADO_NewNRI(ByVal vstrTable As String, Optional ByRef rlngNewNRI As Long = 0) As Boolean
        On Error GoTo Error_bln_ADO_NewNRI
        Const strFCT_NAME As String = "bln_ADO_NewNRI"
        Dim blnReturn As Boolean
        Dim strSQL As String = gvbNullstring
        Dim recRecord As New ADODB.Recordset

        '        strSQL = "exec sp_pkeys " & fstrSQLFix(vstrTable)
        Select Case vstrTable
            Case "Produit"
                strSQL = "SELECT MAX(Pro_NRI) FROM Produit"
            Case Else
                'Do nothing
        End Select

        If strSQL <> gvbNullstring Then
            Select Case False
                Case bln_ADOSelect(strSQL, recRecord)
                Case Not recRecord.EOF
                    blnReturn = True
                Case Not IsDBNull(recRecord(0).Value)
                    blnReturn = True
                Case Else
                    rlngNewNRI = recRecord(0).Value
                    blnReturn = True
            End Select
        Else
            blnReturn = True
        End If

Exit_bln_ADO_NewNRI:
        recRecord = Nothing
        bln_ADO_NewNRI = blnReturn
        Exit Function

Error_bln_ADO_NewNRI:
        blnReturn = False
        GerrLogError(Err, mstrCLASS_NAME, strFCT_NAME)
        Resume Exit_bln_ADO_NewNRI
    End Function

    Private Function pfstrSetDateToTimeZone(ByVal vstrDate As String) As String
        On Error GoTo Error_pfstrSetDateToTimeZone
        Const strFCT_NAME As String = "pfstrSetDateToTimeZone"

        Dim strReturn As String = gvbNullstring

        strReturn = gvbNullstring

        If vstrDate <> gvbNullstring Then
            If mintTimeZoneHeure = 0 And mintTimeZoneMinute = 0 Then
                strReturn = vstrDate
            Else
                strReturn = DateAdd("h", -1 * mintTimeZoneHeure, vstrDate)
                strReturn = DateAdd("n", -1 * mintTimeZoneMinute, strReturn)
            End If
        Else
            strReturn = vstrDate
        End If

Exit_pfstrSetDateToTimeZone:
        pfstrSetDateToTimeZone = strReturn
        Exit Function

Error_pfstrSetDateToTimeZone:
        strReturn = gvbNullstring
        Call GerrLogError(Err, mstrItemAppelant & ":" & mstrCLASS_NAME, strFCT_NAME)
        Resume Exit_pfstrSetDateToTimeZone

    End Function

    Public Function bln_ADOInsertNoIdentity(ByVal vstrTABLE As String, Optional ByRef rlngNewNRI As Integer = 0, Optional ByVal vlngEntNRi As Long = 0) As Boolean
        On Error GoTo Error_bln_ADOInsertNoIdentity
        Const strFCT_NAME As String = "bln_ADOInsertNoIdentity"
        Dim blnReturn As Boolean
        Dim strSQL As String = String.Empty
        Dim intCount As Short
        Dim recRecord As ADODB.Recordset = Nothing

        Dim vlngNRIField As Long = Nothing
        Dim vlngNewItemNRI As Long = Nothing

        If mblnRefresh And mblnInit Then
            Call psSQLfor_INSERT(strSQL, vlngEntNRi)

            Call fblnTTLog(Me, clsConstante.TTFormMode.TTFM_INSERT, vstrTABLE, gvbNullstring, mlngUserToLog)

            strSQL = "SET NOCOUNT ON " & " INSERT INTO " & vstrTABLE & "  " & strSQL & " SET NOCOUNT OFF"
            
            '2008-06-20 : Keep trace of SQL strings of the transaction
            If Not SilentMode Then
                Call pfblnKeepTrace(strSQL)
            End If

            On Error Resume Next
            intCount = 1
            Do While fblnCheckExecute(Err.Number, intCount, mcnAdoConnection)
                recRecord = New ADODB.Recordset
                recRecord.Open(strSQL, mcnAdoConnection)
                intCount = intCount + 1
            Loop

            If Err.Number <> 0 Then
                GoTo Error_bln_ADOInsertNoIdentity
            Else
                'Do nothing
                blnReturn = True
            End If

            On Error GoTo Error_bln_ADOInsertNoIdentity

        Else
            blnReturn = False
        End If

        mblnRefresh = False

Exit_bln_ADOInsertNoIdentity:
        bln_ADOInsertNoIdentity = blnReturn
        Exit Function

Error_bln_ADOInsertNoIdentity:
        blnReturn = False
        If Not SilentMode Then
            Call GerrLogError(Err, mstrItemAppelant & ":" & mstrCLASS_NAME, strFCT_NAME)
        End If
        'Resume Exit_bln_ADOInsertNoIdentity
        GoTo Exit_bln_ADOInsertNoIdentity
    End Function

End Class

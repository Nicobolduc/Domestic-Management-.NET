Imports MySql.Data.MySqlClient
Imports DMS_Application.Model

Public NotInheritable Class AppController

    'Private class members
    Private mdiGeneral As mdiGeneral
    Private mcMySQLConnection As MySqlConnection
    Private mcErrorsLog As ErrorsLogController
    Private mcUser As User
    Private mcStringCleaner As System.Text.RegularExpressions.Regex = New System.Text.RegularExpressions.Regex("'", System.Text.RegularExpressions.RegexOptions.Compiled Or System.Text.RegularExpressions.RegexOptions.CultureInvariant Or System.Text.RegularExpressions.RegexOptions.IgnoreCase)
    'Private Shared ReadOnly _myUniqueInstance As New Lazy(Of AppController)(Function() New AppController(), System.Threading.LazyThreadSafetyMode.ExecutionAndPublication)
    Private Shared _myUniqueInstance As AppController


#Region "Shared Functions /Subs"

    Public Shared ReadOnly Property GetAppController As AppController
        Get
            If _myUniqueInstance Is Nothing Then
                _myUniqueInstance = New AppController()
            End If

            Return _myUniqueInstance
        End Get
    End Property

#End Region

#Region "Properties"

    Public ReadOnly Property MySQLConnection As MySqlConnection
        Get
            Return mcMySQLConnection
        End Get
    End Property

    Public ReadOnly Property cErrorsLog As ErrorsLogController
        Get
            Return Me.mcErrorsLog
        End Get
    End Property

    Public ReadOnly Property cUser As User
        Get
            Return Me.mcUser
        End Get
    End Property

    Public ReadOnly Property str_GetUserDateFormat As String
        Get
            Select Case mcUser.GetLanguage
                Case CInt(mConstants.Language.FRENCH_QC)
                    Return "dd/MM/yyyy"

                Case CInt(mConstants.Language.ENGLISH_CA)
                    Return "mm/dd/yyyy"

                Case Else
                    Return System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern()

            End Select
        End Get
    End Property

    Public ReadOnly Property str_GetUserDateTimeFormat As String
        Get
            Select Case mcUser.GetLanguage
                Case CInt(mConstants.Language.FRENCH_QC)
                    Return "dd/MM/yyyy HH:mm:ss"

                Case CInt(mConstants.Language.ENGLISH_CA)
                    Return "mm/dd/yyyy HH:mm:ss"

                Case Else
                    Return System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern & " " & System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.ShortTimePattern

            End Select
        End Get
    End Property

    Public ReadOnly Property str_GetServerTimeFormat As String
        Get
            Return "HH:mm:ss"
        End Get
    End Property

    Public ReadOnly Property str_GetServerDateFormat() As String
        Get
            Return "yyyy-MM-dd"
        End Get
    End Property

    Public ReadOnly Property str_GetServerDateTimeFormat() As String
        Get
            Return "yyyy-MM-dd HH:mm:ss"
        End Get
    End Property

    Public ReadOnly Property GetCoreModelController As DMS_Application.CoreModelController.CoreModelController
        Get
            Return CoreModelController.CoreModelController.GetCoreModelController
        End Get
    End Property

#End Region

#Region "Constructors"

    Private Sub New()

        mcErrorsLog = New ErrorsLogController

        mcUser = New User

        blnOpenMySQLConnection()

        mdiGeneral = New mdiGeneral
    End Sub

#End Region

#Region "Functions / Subs"

    Private Function blnOpenMySQLConnection() As Boolean
        Dim blnValidReturn As Boolean

        mcMySQLConnection = New MySqlConnection

        'mcMySQLConnection.ConnectionString = "Persist Security Info=False;server=192.168.1.107;Port=3306;userid=Nicolas;password=nicolas;database=dms_tests"
        mcMySQLConnection.ConnectionString = "server=127.0.0.1;Port=3306;userid=root;database=dms_tests" 'MultipleActiveResultSets=true

        Try
            mcMySQLConnection.Open()

            blnValidReturn = True

        Catch ex As MySqlException
            blnValidReturn = False
            MessageBox.Show("La connexion au serveur a échouée.")
            Me.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            mcMySQLConnection.Dispose()
#If Debug = 0 Then
            Application.Exit()
#End If
        End Try

        Return blnValidReturn
    End Function

    Public Function bln_CTLBindCaption(ByRef rControl As System.Windows.Forms.Control) As Boolean
        Dim blnValidReturn As Boolean
        Dim strCaption As String = String.Empty

        Try
            Select Case rControl.GetType
                Case GetType(System.Windows.Forms.DataGridView)

            End Select

            MySQLController.str_ADOSingleLookUp("ApC_Text", "AppCaption", "ApC_ID = " & CStr(rControl.Tag))

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Public Function str_GetCaption(ByVal intCaptionID As Integer, ByVal intLanguage As Short) As String
        Dim blnValidReturn As Boolean
        Dim strCaption As String = String.Empty

        Try

            strCaption = MySQLController.str_ADOSingleLookUp("ApC_Text", "AppCaption", "ApC_No = " & intCaptionID.ToString & " AND ApL_ID = " & intLanguage)

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return strCaption
    End Function

    Public Function str_FixStringForSQL(ByVal vstrStringToFix As String) As String

        Return "'" & mcStringCleaner.Replace(vstrStringToFix, "''") & "'"

    End Function

    Public Function str_FixDateForSQL(ByVal vstrDateToFix As Date) As String

        Return str_FixStringForSQL(Format(vstrDateToFix, str_GetServerDateTimeFormat))

    End Function

    Public Function str_SetDateToMidnightServerFormat(ByVal vdtDateToSet As String) As Date

        Return CDate(Format(CDate(Format(CType(vdtDateToSet, Date), str_GetServerDateFormat) & " 00:00:00"), str_GetServerDateTimeFormat))

    End Function

    Public Sub ShowMessage(ByVal vintCaption_ID As Integer, Optional ByVal vmsgType As MsgBoxStyle = MsgBoxStyle.Information)
        Dim strMessage As String = String.Empty

        Try
            strMessage = gcAppController.str_GetCaption(vintCaption_ID, mcUser.GetLanguage)

            MsgBox(strMessage, vmsgType)

        Catch ex As Exception
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

    Public Sub DisableAllFormControls(Optional ByRef rForm As System.Windows.Forms.Form = Nothing, Optional ByRef rTabPage As TabPage = Nothing, Optional ByRef rControl As Control = Nothing)
        Dim controlCollection As System.Windows.Forms.Control.ControlCollection

        Try
            If Not rControl Is Nothing Then
                controlCollection = rControl.Controls
            Else
                controlCollection = rForm.Controls
            End If

            For Each objControl As Control In controlCollection

                Select Case objControl.GetType.Name
                    Case "Button", "TextBox", "CheckBox", "RadioButton", "DateTimePicker", "ListView", "ComboBox"
                        objControl.Enabled = False

                    Case "GroupBox"
                        DisableAllFormControls(Nothing, Nothing, objControl)

                    Case "DataGridView"
                        DirectCast(objControl, DataGridView).ReadOnly = True

                    Case "GridControl"
                        DirectCast(objControl, GridControl).BrowseOnly = True

                    Case "TabControl"
                        For Each tp As TabPage In DirectCast(objControl, TabControl).TabPages
                            DisableAllFormControls(Nothing, tp)
                        Next

                    Case Else
                        'Do Nothing

                End Select

            Next objControl

        Catch ex As Exception
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

    Public Sub EmptyAllFormControls(Optional ByRef rForm As System.Windows.Forms.Form = Nothing, Optional ByRef rTabPage As TabPage = Nothing, Optional ByRef rControl As Control = Nothing)
        Dim controlCollection As System.Windows.Forms.Control.ControlCollection

        Try
            If Not rControl Is Nothing Then
                controlCollection = rControl.Controls
            Else
                controlCollection = rForm.Controls
            End If

            For Each objControl As Control In controlCollection

                Select Case objControl.GetType.Name
                    Case "TextBox"
                        objControl.Text = String.Empty

                    Case "CheckBox", "RadioButton"
                        DirectCast(objControl, CheckBox).Checked = False

                    Case "ComboBox"
                        DirectCast(objControl, ComboBox).DataSource = Nothing
                        DirectCast(objControl, ComboBox).Items.Clear()

                    Case "GroupBox"
                        EmptyAllFormControls(Nothing, Nothing, objControl)

                    Case "TabControl"
                        For Each tp As TabPage In DirectCast(objControl, TabControl).TabPages
                            EmptyAllFormControls(Nothing, tp)
                        Next

                    Case Else
                        'Do Nothing

                End Select

            Next objControl

        Catch ex As Exception
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

    Protected Overrides Sub Finalize()
        mcMySQLConnection.Close()
        mcMySQLConnection.Dispose()
        MyBase.Finalize()
    End Sub

#End Region


End Class

Imports MySql.Data.MySqlClient

Public Class clsApplication

    'Private class members
    Private mdiGeneral As mdiGeneral
    Private mcMySQLConnection As MySqlConnection
    Private mcErrorsLog As clsErrorsLog
    Private mcUser As clsUser
    Private mcStringCleaner As System.Text.RegularExpressions.Regex = New System.Text.RegularExpressions.Regex("'", System.Text.RegularExpressions.RegexOptions.Compiled Or System.Text.RegularExpressions.RegexOptions.CultureInvariant Or System.Text.RegularExpressions.RegexOptions.IgnoreCase)


#Region "Properties"

    Public ReadOnly Property MySQLConnection As MySqlConnection
        Get
            Return mcMySQLConnection
        End Get
    End Property

    Public ReadOnly Property cErrorsLog As clsErrorsLog
        Get
            Return Me.mcErrorsLog
        End Get
    End Property

    Public ReadOnly Property cUser As clsUser
        Get
            Return Me.mcUser
        End Get
    End Property

    Public ReadOnly Property str_GetPCDateFormat As String
        Get
            Return "dd-MMMM-yyyy"
        End Get
    End Property

    Public ReadOnly Property str_GetPCDateTimeFormat As String
        Get
            Return "dd-MMMM-yyyy hh:mm"
        End Get
    End Property

#End Region


#Region "Constructors"

    Public Sub New()

        mcErrorsLog = New clsErrorsLog

        mcUser = New clsUser

        blnSetMySQLConnection()

        mdiGeneral = New mdiGeneral
    End Sub

#End Region


#Region "Functions / Subs"

    Private Function blnSetMySQLConnection() As Boolean
        Dim blnReturn As Boolean

        mcMySQLConnection = New MySqlConnection

        'mcMySQLConnection.ConnectionString = "Persist Security Info=False;server=192.168.1.112;Port=3306;userid=nicolas;password=root;database=dms_test"
        mcMySQLConnection.ConnectionString = "server=127.0.0.1;Port=3306;userid=root;database=dms_tests"

        Try
            mcMySQLConnection.Open()

            blnReturn = True

        Catch ex As MySqlException
            blnReturn = False
            MessageBox.Show("La connexion au serveur a échouée.")
            mcMySQLConnection.Dispose()
        End Try

        Return blnReturn
    End Function

    Public Function str_GetServerDateTimeFormat() As String
        Dim strFormat As String = vbNullString

        Try
            strFormat = "yyyy-MM-dd hh:mm:ss"

        Catch ex As MySqlException
            gcApplication.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return strFormat
    End Function

    Public Function bln_CTLBindCaption(ByRef rControl As System.Windows.Forms.Control) As Boolean
        Dim blnReturn As Boolean
        Dim strCaption As String = vbNullString

        Try
            Select Case rControl.GetType
                Case GetType(System.Windows.Forms.DataGridView)

            End Select

            mSQL.str_ADOSingleLookUp("ApC_Text", "AppCaption", "ApC_ID = " & CStr(rControl.Tag))

        Catch ex As Exception
            blnReturn = False
            gcApplication.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Public Function str_GetCaption(ByVal intCaptionID As Integer, ByVal intLanguage As Short) As String
        Dim blnReturn As Boolean
        Dim strCaption As String = vbNullString

        Try

            strCaption = mSQL.str_ADOSingleLookUp("ApC_Text", "AppCaption", "ApC_No = " & intCaptionID.ToString & " AND ApL_ID = " & intLanguage)

        Catch ex As Exception
            blnReturn = False
            gcApplication.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return strCaption
    End Function

    Public Function str_FixStringForSQL(ByVal vstrStringToFix As String) As String

        Return "'" & mcStringCleaner.Replace(vstrStringToFix, "''") & "'"

    End Function

    Public Sub ShowMessage(ByVal vintCaption_ID As Integer, ByVal vmsgType As MsgBoxStyle)
        Dim strMessage As String = vbNullString

        Try
            strMessage = gcApplication.str_GetCaption(vintCaption_ID, mcUser.GetLanguage)

            MsgBox(strMessage, vmsgType)

        Catch ex As Exception
            gcApplication.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

    Public Sub DisableAllControls(Optional ByRef rForm As System.Windows.Forms.Form = Nothing, Optional ByRef rTabPage As TabPage = Nothing, Optional ByRef rControl As Control = Nothing)
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
                        DisableAllControls(Nothing, Nothing, objControl)

                    Case "DataGridView"
                        DirectCast(objControl, DataGridView).ReadOnly = True

                    Case "TabControl"
                        For Each tp As TabPage In DirectCast(objControl, TabControl).TabPages
                            DisableAllControls(Nothing, tp)
                        Next

                    Case Else
                        'Do Nothing

                End Select

            Next objControl

        Catch ex As Exception
            gcApplication.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

    Public Sub ClearAllControls(Optional ByRef rForm As System.Windows.Forms.Form = Nothing, Optional ByRef rTabPage As TabPage = Nothing, Optional ByRef rControl As Control = Nothing)
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
                        objControl.Text = vbNullString

                    Case "CheckBox", "RadioButton"
                        DirectCast(objControl, CheckBox).Checked = False

                    Case "ComboBox"
                        DirectCast(objControl, ComboBox).DataSource = Nothing
                        DirectCast(objControl, ComboBox).Items.Clear()

                    Case "GroupBox"
                        ClearAllControls(Nothing, Nothing, objControl)

                    Case "TabControl"
                        For Each tp As TabPage In DirectCast(objControl, TabControl).TabPages
                            ClearAllControls(Nothing, tp)
                        Next

                    Case Else
                        'Do Nothing

                End Select

            Next objControl

        Catch ex As Exception
            gcApplication.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

#End Region

End Class

Imports MySql.Data.MySqlClient

Public Class clsApplication

    Private mdiGeneral As mdiGeneral
    Private mcMySQLConnection As MySqlConnection
    Private mcErrorsLog As clsErrorsLog
    Private mcUser As clsUser

    Private mcStringCleaner As System.Text.RegularExpressions.Regex = New System.Text.RegularExpressions.Regex("'", System.Text.RegularExpressions.RegexOptions.Compiled Or System.Text.RegularExpressions.RegexOptions.CultureInvariant Or System.Text.RegularExpressions.RegexOptions.IgnoreCase)

    Public ReadOnly Property cMySQLConnection As MySqlConnection
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

    Public ReadOnly Property getDateFormat As String
        Get
            Return "dd-MMMM-yyyy"
        End Get
    End Property

    Public ReadOnly Property getDateTimeFormat As String
        Get
            Return "dd-MMMM-yyyy hh:mm"
        End Get
    End Property


    Public Sub New()

        mcErrorsLog = New clsErrorsLog

        mcUser = New clsUser

        blnSetMySQLConnection()

        mdiGeneral = New mdiGeneral
    End Sub

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
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
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
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return strCaption
    End Function

    Public Function str_FixStringForSQL(ByVal vstrStringToFix As String) As String

        Return "'" & mcStringCleaner.Replace(vstrStringToFix, "''") & "'"

    End Function

    Public Function bln_ShowMessage(ByVal vintCaption_ID As Integer, ByVal vmsgType As MsgBoxStyle) As Boolean
        Dim blnReturn As Boolean
        Dim strMessage As String = vbNullString

        Try
            strMessage = gcApp.str_GetCaption(vintCaption_ID, mcUser.GetLanguage)

            MsgBox(strMessage, vmsgType)

            blnReturn = True

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Public Function bln_DisableAllFormsControls(Optional ByRef rForm As System.Windows.Forms.Form = Nothing, Optional ByRef rTabPage As TabPage = Nothing, Optional ByRef rControl As Control = Nothing) As Boolean
        Dim blnReturn As Boolean
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
                        blnReturn = objControl.Enabled = False

                    Case "GroupBox"
                        blnReturn = bln_DisableAllFormsControls(Nothing, Nothing, objControl)

                    Case "TabControl"
                        For Each tp As TabPage In DirectCast(objControl, TabControl).TabPages
                            blnReturn = bln_DisableAllFormsControls(Nothing, tp)
                        Next

                    Case Else
                        'Do Nothing

                End Select

                If Not blnReturn Then Exit For
            Next objControl

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function
End Class

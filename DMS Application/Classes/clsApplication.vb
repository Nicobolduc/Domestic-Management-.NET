Imports MySql.Data.MySqlClient

Public Class clsApplication

    Private mdiGeneral As mdiGeneral
    Private mcMySQLConnection As MySqlConnection
    Private mcErrorsLog As clsErrorsLog

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

    Private Function bln_CTLBindCaption(ByRef rControl As System.Windows.Forms.Control) As Boolean
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

End Class

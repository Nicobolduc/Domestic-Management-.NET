Imports System.IO
Imports System.Text


<CLSCompliant(True)> _
Public Class ErrorsLogController

#Region "Functions / Subs"

    Public Sub WriteToErrorLog(ByVal strErrorMessage As String, ByVal strStackTrace As String, ByVal strTitle As String)
        Dim myFileStream As FileStream = New FileStream(Application.StartupPath & "\Log.txt", FileMode.Append, FileAccess.Write)
        Dim myStreamWriter As StreamWriter = New StreamWriter(myFileStream)
        Dim strMessageToShow As String = String.Empty

        If Not System.IO.Directory.Exists(Application.StartupPath & "\") Then
            System.IO.Directory.CreateDirectory(Application.StartupPath & "\")
        End If

        myStreamWriter.Write("Title: " & strTitle & vbCrLf)
        myStreamWriter.Write("Message: " & strErrorMessage & vbCrLf)
        myStreamWriter.Write("StackTrace: " & strStackTrace & vbCrLf)
        myStreamWriter.Write("Date/Time: " & DateTime.Now.ToString() & vbCrLf)
        myStreamWriter.Write("======================== END TRACE ========================" & vbCrLf)

        myStreamWriter.Close()
        myFileStream.Close()

#If DEBUG Then
        strMessageToShow = strTitle & vbCrLf
        strMessageToShow = strMessageToShow & strErrorMessage & vbCrLf
        strMessageToShow = strMessageToShow & strStackTrace & vbCrLf

        MsgBox(strMessageToShow, MsgBoxStyle.Critical, "An error occurred")
#End If

    End Sub

#End Region

End Class

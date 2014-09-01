Imports System.IO
Imports System.Text


<CLSCompliant(True)> _
Public Class clsErrorsLog

#Region "Functions / Subs"

    Public Sub WriteToErrorLog(ByVal strErrorMessage As String, ByVal strStackTrace As String, ByVal strTitle As String)
        Dim myFileStream As FileStream = New FileStream(Application.StartupPath & "\Log.txt", FileMode.Append, FileAccess.Write)
        Dim myStreamWriter As StreamWriter = New StreamWriter(myFileStream)

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

    End Sub

#End Region
    
End Class

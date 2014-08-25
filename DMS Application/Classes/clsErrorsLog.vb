Imports System.IO
Imports System.Text


<CLSCompliant(True)> _
Public Class clsErrorsLog

    Public Sub WriteToErrorLog(ByVal strErrorMessage As String, ByVal strStackTrace As String, ByVal strTitle As String)

        If Not System.IO.Directory.Exists(Application.StartupPath & "\") Then
            System.IO.Directory.CreateDirectory(Application.StartupPath & "\")
        Else
            'Do nothing
        End If


        'Dim cFS As FileStream = New FileStream(Application.StartupPath & "\ErrorsLog\Log.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite)
        'Dim cSW As StreamWriter = New StreamWriter(cFS)

        'cSW.Close()
        'cFS.Close()

        Dim cFile As FileStream = New FileStream(Application.StartupPath & "\Log.txt", FileMode.Append, FileAccess.Write)
        Dim cWriter As StreamWriter = New StreamWriter(cFile)

        cWriter.Write("Title: " & strTitle & vbCrLf)
        cWriter.Write("Message: " & strErrorMessage & vbCrLf)
        cWriter.Write("StackTrace: " & strStackTrace & vbCrLf)
        cWriter.Write("Date/Time: " & DateTime.Now.ToString() & vbCrLf)
        cWriter.Write("======================== END TRACE ========================" & vbCrLf)

        cWriter.Close()
        cFile.Close()

    End Sub

End Class

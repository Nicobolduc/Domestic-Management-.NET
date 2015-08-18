Imports System.Threading
Imports System.Globalization

Module mMain


    Public gcAppCtrl As AppController

    Sub main()

        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-CA")

        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-CA")

        gcAppCtrl = AppController.GetAppController

    End Sub

    'Public Sub CreateFormFromString(ByVal vstrFormToOpenName As String)
    '    Dim strFormName As String = String.Empty
    '    Dim frmTemp As System.Windows.Forms.Form = Nothing

    '    strFormName = System.Reflection.Assembly.GetEntryAssembly.GetName.Name & "." & vstrFormToOpenName
    '    'frmTemp = System.Reflection.Assembly.GetEntryAssembly.CreateInstance(strFormName, True)
    '    frmTemp = New DirectCast(strFormName,System.Windows.Forms.Form)
    '    DirectCast(frmTemp, System.Windows.Forms.Form).Show()

    'End Sub

End Module

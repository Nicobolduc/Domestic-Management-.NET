﻿Module mMain


    Public gcApplication As clsApplication

    Public gstrCurrencyFormat As String = "##,0.00$"

    Sub main()

        gcApplication = New clsApplication

    End Sub

    'Public Sub CreateFormFromString(ByVal vstrFormToOpenName As String)
    '    Dim strFormName As String = vbNullString
    '    Dim frmTemp As System.Windows.Forms.Form = Nothing

    '    strFormName = System.Reflection.Assembly.GetEntryAssembly.GetName.Name & "." & vstrFormToOpenName
    '    'frmTemp = System.Reflection.Assembly.GetEntryAssembly.CreateInstance(strFormName, True)
    '    frmTemp = New DirectCast(strFormName,System.Windows.Forms.Form)
    '    DirectCast(frmTemp, System.Windows.Forms.Form).Show()

    'End Sub

End Module

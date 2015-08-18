Public Class Template

    'Public members

    'Private members

    'Private class members


#Region "Properties"



#End Region


#Region "Constructors"



#End Region


#Region "Shared Functions /Subs"



#End Region


#Region "Functions / Subs"



#End Region


#Region "Private events"



#End Region

    Private Function X() As Boolean
        Dim blnValidReturn As Boolean

        Try

        Catch ex As Exception
            blnValidReturn = False
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function


End Class

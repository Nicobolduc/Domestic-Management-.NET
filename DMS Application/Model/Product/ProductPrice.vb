Public Class ProductPrice

    'Private members
    Private _intProductPrice_ID As Integer
    Private _intCompanySeller_ID As Integer
    Private _intProductBrand_ID As Integer
    Private _intProduct_ID As Integer
    Private _dblPrice As Double

    Private _intDMLCommand As mConstants.Form_Modes

    'Private class members
    Private mcSQL As MySQLController

#Region "Properties"

    Public Property DLMCommand As mConstants.Form_Modes
        Get
            Return _intDMLCommand
        End Get
        Set(ByVal value As mConstants.Form_Modes)
            _intDMLCommand = value
        End Set
    End Property

    Public Property ProductPrice_ID As Integer
        Get
            Return _intProductPrice_ID
        End Get
        Set(ByVal value As Integer)
            _intProductPrice_ID = value
        End Set
    End Property


    Public Property CompanySeller_ID As Integer
        Get
            Return _intCompanySeller_ID
        End Get

        Set(ByVal value As Integer)
            _intCompanySeller_ID = value
        End Set
    End Property

    Public Property ProductBrand_ID As Integer
        Get
            Return _intProductBrand_ID
        End Get

        Set(ByVal value As Integer)
            _intProductBrand_ID = value
        End Set
    End Property

    Public Property Product_ID As Integer
        Get
            Return _intProduct_ID
        End Get

        Set(ByVal value As Integer)
            _intProduct_ID = value
        End Set
    End Property

    Public Property Price As Double
        Get
            Return _dblPrice
        End Get

        Set(ByVal value As Double)
            _dblPrice = value
        End Set
    End Property

    Public WriteOnly Property SetMySQL As MySQLController
        Set(ByVal value As MySQLController)
            mcSQL = value
        End Set
    End Property

#End Region


#Region "Constructors"



#End Region


#Region "Functions / Subs"

    Public Function blnProductPrice_Save() As Boolean
        Dim blnValidReturn As Boolean = True

        Try
            Select Case _intDMLCommand
                Case Form_Modes.INSERT_MODE
                    blnValidReturn = blnProductPrice_Insert()

                Case Form_Modes.UPDATE_MODE
                    blnValidReturn = blnProductPrice_Update()

                Case Form_Modes.DELETE_MODE
                    blnValidReturn = blnProductPrice_Delete()

                Case Else
                    blnValidReturn = True

            End Select

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnProductPrice_AddFields() As Boolean
        Dim blnValidReturn As Boolean

        Try
            Select Case False
                Case mcSQL.bln_RefreshFields
                Case mcSQL.bln_AddField("Cy_ID_Seller", _intCompanySeller_ID, MySQLController.MySQL_FieldTypes.ID_TYPE)
                Case mcSQL.bln_AddField("ProB_ID", _intProductBrand_ID, MySQLController.MySQL_FieldTypes.ID_TYPE)
                Case mcSQL.bln_AddField("Pro_ID", _intProduct_ID, MySQLController.MySQL_FieldTypes.ID_TYPE)
                Case mcSQL.bln_AddField("ProP_Price", _dblPrice, MySQLController.MySQL_FieldTypes.DOUBLE_TYPE)
                Case Else
                    blnValidReturn = True
            End Select

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnProductPrice_Insert() As Boolean
        Dim blnReturn As Boolean

        Try
            Select Case False
                Case blnProductPrice_AddFields()
                Case mcSQL.bln_ADOInsert("ProductPrice", _intProductPrice_ID)
                Case _intProductPrice_ID > 0
                Case Else
                    blnReturn = True
            End Select

        Catch ex As Exception
            blnReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Function blnProductPrice_Update() As Boolean
        Dim blnReturn As Boolean

        Try
            Select Case False
                Case blnProductPrice_AddFields()
                Case mcSQL.bln_ADOUpdate("ProductPrice", "ProP_ID = " & _intProductPrice_ID)
                Case Else
                    blnReturn = True
            End Select

        Catch ex As Exception
            blnReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Function blnProductPrice_Delete() As Boolean
        Dim blnReturn As Boolean

        Try
            Select Case False
                Case mcSQL.bln_ADODelete("ProductPrice", "ProP_ID = " & _intProductPrice_ID)
                Case Else
                    blnReturn = True
            End Select

        Catch ex As Exception
            blnReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

#End Region

End Class

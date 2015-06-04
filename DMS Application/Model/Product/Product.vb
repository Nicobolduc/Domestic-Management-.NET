Namespace Model

    Public Class Product

        'Private members
        Private _intProduct_ID As Integer
        Private _strProduct_Name As String = vbNullString
        Private _intProduct_Type_ID As Integer
        Private _intProduct_Category_ID As Integer
        Private _blnProduct_IsTaxable As Boolean


#Region "Properties"

        Public ReadOnly Property ID As Integer
            Get
                Return _intProduct_ID
            End Get
        End Property

        Public Property Name As String
            Get
                Return _strProduct_Name
            End Get

            Set(value As String)

                If Not value = String.Empty Then
                    _strProduct_Name = value
                Else
                    'Non valide
                End If
            End Set
        End Property

        Public Property Type_ID As Integer
            Get
                Return _intProduct_Type_ID
            End Get

            Set(value As Integer)
                If value > 0 Then
                    _intProduct_Type_ID = value
                Else
                    'Non valide
                End If
            End Set
        End Property

        Public Property Category_ID As Integer
            Get
                Return _intProduct_Category_ID
            End Get

            Set(value As Integer)
                _intProduct_Category_ID = value
            End Set
        End Property

        Public Property isTaxable As Boolean
            Get
                Return _blnProduct_IsTaxable
            End Get

            Set(value As Boolean)
                _blnProduct_IsTaxable = value
            End Set
        End Property

#End Region


#Region "Constructors"

        Public Sub New()

        End Sub

#End Region


#Region "Functions / Subs"

        Public Function blnLoadProduct(ByVal vintProduct_ID As Integer) As Boolean
            Dim blnReturn As Boolean
            Dim strSQL As String = vbNullString
            Dim mySQLReader As MySqlDataReader = Nothing
            Dim lol As Product
            _intProduct_ID = vintProduct_ID

            Try
                strSQL = strSQL & " SELECT Product.Pro_Name, " & vbCrLf
                strSQL = strSQL & "        Product.ProT_ID, " & vbCrLf
                strSQL = strSQL & "        Product.ProC_ID, " & vbCrLf
                strSQL = strSQL & "        Product.Pro_Taxable " & vbCrLf
                strSQL = strSQL & " FROM Product " & vbCrLf
                strSQL = strSQL & " WHERE Product.Pro_ID = " & _intProduct_ID & vbCrLf

                mySQLReader = MySQLController.ADOSelect(strSQL)

                mySQLReader.Read()

                _strProduct_Name = mySQLReader.Item("Pro_Name").ToString

                _blnProduct_IsTaxable = CBool(mySQLReader.Item("Pro_Taxable"))

                _intProduct_Type_ID = CInt(mySQLReader.Item("ProT_ID"))

                If Not IsDBNull(mySQLReader.Item("ProC_ID")) Then
                    _intProduct_Category_ID = CInt(mySQLReader.Item("ProC_ID"))
                Else
                    _intProduct_Category_ID = 0
                End If

            Catch ex As Exception
                blnReturn = False
                gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            Finally
                If Not IsNothing(mySQLReader) Then
                    mySQLReader.Close()
                    mySQLReader.Dispose()
                End If
            End Try

            Return blnReturn
        End Function

#End Region

    End Class

End Namespace
Imports System.ComponentModel


Public Class ctlFormController
    Inherits System.Windows.Forms.UserControl
    Implements INotifyPropertyChanged


    'Private members
    Private mintItem_ID As Integer
    Private mintFormMode As mConstants.Form_Mode
    Private mblnChangeMade As Boolean
    Private mblnFormIsLoading As Boolean
    Private mblnShowButtonQuitOnly As Boolean

    Private WithEvents mfrmParent As System.Windows.Forms.Form

    'Public Events
    Public Event BeNotify(ByVal eventArgs As BeNotifyEventArgs)
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Public Event SetReadRights()
    Public Event LoadData(ByVal eventArgs As LoadDataEventArgs)
    Public Event ValidateForm(ByVal eventArgs As ValidateFormEventArgs)
    Public Event SaveData(ByVal eventArgs As SaveDataEventArgs)


#Region "Properties"

    <Browsable(False)>
    Public Property Item_ID As Integer
        Get
            Return mintItem_ID
        End Get
        Set(ByVal value As Integer)
            mintItem_ID = value
        End Set
    End Property

    <Browsable(False)>
    Public Property FormMode As mConstants.Form_Mode
        Get
            Return mintFormMode
        End Get
        Set(ByVal value As mConstants.Form_Mode)
            mintFormMode = value
            SetVisualStyle()
        End Set
    End Property

    Public WriteOnly Property ChangeMade As Boolean
        Set(ByVal value As Boolean)
            If Not FormIsLoading Then
                mblnChangeMade = value
            Else
                mblnChangeMade = False
            End If

            SetVisualStyle()
        End Set
    End Property

    <Browsable(False)>
    Public Property FormIsLoading As Boolean
        Get
            Return mblnFormIsLoading
        End Get
        Set(ByVal value As Boolean)
            mblnFormIsLoading = value

            If Not mfrmParent Is Nothing And mblnFormIsLoading Then
                Me.Cursor = Cursors.WaitCursor
                mfrmParent.SuspendLayout()
            ElseIf Not mfrmParent Is Nothing Then
                Me.Cursor = Cursors.Default
                mfrmParent.ResumeLayout()
            End If
        End Set
    End Property

    <Browsable(True)>
    Public Property ShowButtonQuitOnly As Boolean
        Get
            Return mblnShowButtonQuitOnly
        End Get
        Set(ByVal value As Boolean)
            mblnShowButtonQuitOnly = value

            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("ShowButtonQuitOnly"))
        End Set
    End Property

#End Region


#Region "Functions / Subs"

    Public Sub ShowForm(ByVal vintFormMode As mConstants.Form_Mode, Optional ByRef rintItem_ID As Integer = 0, Optional ByVal vblnIsModal As Boolean = False)

        mintFormMode = vintFormMode
        mintItem_ID = rintItem_ID

        Try
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            mfrmParent = MyBase.FindForm()

            SetVisualStyle()

            LoadFormData()

            If Not vblnIsModal Then
                mfrmParent.MdiParent = My.Forms.mdiGeneral

                mfrmParent.Show()
            Else
                mfrmParent.MdiParent = Nothing
                mfrmParent.ShowInTaskbar = False

                Me.Cursor = System.Windows.Forms.Cursors.Default

                mfrmParent.ShowDialog()
            End If

            rintItem_ID = mintItem_ID

        Catch ex As Exception
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try

    End Sub

    Private Sub SetVisualStyle()
        Select Case mintFormMode
            Case mConstants.Form_Mode.INSERT_MODE
                btnApply.Text = "Enregistrer"
                imgFormMode.Image = My.Resources.Add

            Case mConstants.Form_Mode.UPDATE_MODE
                btnApply.Text = "Appliquer"
                imgFormMode.Image = My.Resources.Update

        End Select

        Select Case mintFormMode
            Case mConstants.Form_Mode.INSERT_MODE, mConstants.Form_Mode.UPDATE_MODE
                If mblnChangeMade Then
                    btnApply.Enabled = True
                    btnCancel.Enabled = True
                    btnQuit.Enabled = False
                Else
                    btnApply.Enabled = False
                    btnCancel.Enabled = False
                    btnQuit.Enabled = True
                End If

            Case mConstants.Form_Mode.CONSULT_MODE
                imgFormMode.Image = My.Resources.Consult

            Case mConstants.Form_Mode.DELETE_MODE
                btnApply.Enabled = True
                btnCancel.Enabled = False
                btnQuit.Enabled = True

                btnApply.Text = "Supprimer"
                imgFormMode.Image = My.Resources.Delete

        End Select
    End Sub

    Public Sub LoadFormData()
        FormIsLoading = True

        ChangeMade = False

        RaiseEvent LoadData(New LoadDataEventArgs(mintItem_ID))

        RaiseEvent SetReadRights()

        FormIsLoading = False
    End Sub

    Private Sub SetControlsVisility()
        If mblnShowButtonQuitOnly Then

            Me.Width = btnQuit.Width + 10
            imgFormMode.Visible = False
        Else
            Me.Width = 324
            imgFormMode.Visible = True
        End If
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        Me.Dispose()
    End Sub

#End Region


#Region "Privates events"

    Private Sub btnApply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Dim saveEvent As New SaveDataEventArgs
        Dim validationEvent As New ValidateFormEventArgs

        Me.Cursor = Cursors.WaitCursor

        RaiseEvent ValidateForm(validationEvent)

        If validationEvent.IsValid Then

            RaiseEvent SaveData(saveEvent)

            If saveEvent.SaveSuccessful Then

                ChangeMade = False

                Select Case mintFormMode
                    Case mConstants.Form_Mode.INSERT_MODE
                        FormMode = mConstants.Form_Mode.UPDATE_MODE
                        LoadFormData()

                    Case mConstants.Form_Mode.UPDATE_MODE
                        LoadFormData()

                    Case mConstants.Form_Mode.DELETE_MODE
                        mfrmParent.Close()

                End Select
            Else
                gcAppController.ShowMessage(mConstants.Error_Message.ERROR_SAVE_MSG, MsgBoxStyle.Critical)
            End If

        End If

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        gcAppController.EmptyAllFormControls(mfrmParent)
        LoadFormData()
    End Sub

    Private Sub btnQuit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuit.Click
        mfrmParent.Close()
        Me.Dispose()
    End Sub

    Private Sub ctlFormControler_LocationChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LocationChanged
        If Not mfrmParent Is Nothing And Not FormIsLoading Then
            mfrmParent.ResumeLayout()
        End If
    End Sub

    Private Sub ctlFormControler_Move(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Move
        If Not mfrmParent Is Nothing And Not FormIsLoading Then
            mfrmParent.SuspendLayout()
        End If
    End Sub

    Private Sub ctlFormControler_PropertyChanged() Handles Me.PropertyChanged
        SetControlsVisility()
    End Sub

    Private Sub mfrmParent_ResizeBegin(ByVal sender As Object, ByVal e As System.EventArgs) Handles mfrmParent.ResizeBegin
        mfrmParent.SuspendLayout()
    End Sub

    Private Sub mfrmParent_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles mfrmParent.ResizeEnd
        mfrmParent.ResumeLayout()
    End Sub

#End Region

End Class

#Region "Custom events"

Public Class LoadDataEventArgs
    Inherits System.EventArgs

    Private mintItem_ID As Integer

    Public ReadOnly Property Item_ID As Integer
        Get
            Return mintItem_ID
        End Get
    End Property

    Public Sub New(ByVal vintItem_ID As Integer)
        mintItem_ID = vintItem_ID
    End Sub

End Class

Public Class SaveDataEventArgs
    Inherits System.EventArgs

    Private mblnSaveSuccessful As Boolean

    Public Property SaveSuccessful As Boolean
        Get
            Return mblnSaveSuccessful
        End Get
        Set(ByVal value As Boolean)
            mblnSaveSuccessful = value
        End Set
    End Property

End Class

Public Class ValidateFormEventArgs
    Inherits System.EventArgs

    Private mblnIsValid As Boolean

    Public Property IsValid As Boolean
        Get
            Return mblnIsValid
        End Get
        Set(ByVal value As Boolean)
            mblnIsValid = value
        End Set
    End Property

End Class

Public Class BeNotifyEventArgs
    Inherits System.EventArgs

    Private _lstReceivedValues As List(Of Object)

    Public Property LstReceivedValues As List(Of Object)
        Get
            Return New List(Of Object)
        End Get
        Set(ByVal value As List(Of Object))
            _lstReceivedValues = value
        End Set
    End Property

End Class

#End Region

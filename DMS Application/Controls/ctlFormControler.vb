Imports System.ComponentModel


Public Class ctlFormControler
    Inherits System.Windows.Forms.UserControl
    Implements INotifyPropertyChanged


    'Private members
    Private mintItem_ID As Integer
    Private mintFormMode As clsConstants.Form_Modes
    Private mblnChangeMade As Boolean
    Private mblnFormIsLoading As Boolean
    Private mblnShowButtonQuitOnly As Boolean

    Private WithEvents mfrmParent As System.Windows.Forms.Form

    'Public Events
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Public Event SetReadRights()
    Public Event LoadData(ByVal eventArgs As LoadDataEventArgs)
    Public Event ValidateRules(ByVal eventArgs As ValidateRulesEventArgs)
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
    Public Property FormMode As clsConstants.Form_Modes
        Get
            Return mintFormMode
        End Get
        Set(ByVal value As clsConstants.Form_Modes)
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

    Public Sub ShowForm(ByVal vintFormMode As clsConstants.Form_Modes, Optional ByRef rintItem_ID As Integer = 0, Optional ByVal vblnIsModal As Boolean = False)

        mintFormMode = vintFormMode
        mintItem_ID = rintItem_ID

        Try
            mfrmParent = MyBase.FindForm()

            SetVisualStyle()

            LoadFormData()

            If Not vblnIsModal Then
                mfrmParent.MdiParent = My.Forms.mdiGeneral

                mfrmParent.Show()
            Else
                mfrmParent.MdiParent = Nothing

                mfrmParent.ShowDialog()
            End If

            rintItem_ID = mintItem_ID

        Catch ex As Exception
            gcApplication.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

    Private Sub SetVisualStyle()
        Select Case mintFormMode
            Case clsConstants.Form_Modes.INSERT_MODE
                btnApply.Text = "Enregistrer"
                imgFormMode.Image = My.Resources.Add

            Case clsConstants.Form_Modes.UPDATE_MODE
                btnApply.Text = "Appliquer"
                imgFormMode.Image = My.Resources.Update

        End Select

        Select Case mintFormMode
            Case clsConstants.Form_Modes.INSERT_MODE, clsConstants.Form_Modes.UPDATE_MODE
                If mblnChangeMade Then
                    btnApply.Enabled = True
                    btnCancel.Enabled = True
                    btnQuit.Enabled = False
                Else
                    btnApply.Enabled = False
                    btnCancel.Enabled = False
                    btnQuit.Enabled = True
                End If

            Case clsConstants.Form_Modes.CONSULT_MODE
                imgFormMode.Image = My.Resources.Consult

            Case clsConstants.Form_Modes.DELETE_MODE
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
        Dim validationEvent As New ValidateRulesEventArgs

        Me.Cursor = Cursors.WaitCursor

        RaiseEvent ValidateRules(validationEvent)

        If validationEvent.IsValid Then

            RaiseEvent SaveData(saveEvent)

            If saveEvent.SaveSuccessful Then

                ChangeMade = False

                Select Case mintFormMode
                    Case clsConstants.Form_Modes.INSERT_MODE
                        FormMode = clsConstants.Form_Modes.UPDATE_MODE
                        LoadFormData()

                    Case clsConstants.Form_Modes.UPDATE_MODE
                        LoadFormData()

                    Case clsConstants.Form_Modes.DELETE_MODE
                        mfrmParent.Close()

                End Select
            Else
                gcApplication.ShowMessage(clsConstants.Error_Messages.ERROR_SAVE_MSG, MsgBoxStyle.Critical)
            End If

        End If

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        gcApplication.ClearAllControls(mfrmParent)
        LoadFormData()
    End Sub

    Private Sub btnQuit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuit.Click
        mfrmParent.Close()
        Me.Dispose()
    End Sub

    Private Sub ctlFormControler_PropertyChanged() Handles Me.PropertyChanged
        SetControlsVisility()
    End Sub

#End Region
    
    Private Sub mfrmParent_ResizeBegin(ByVal sender As Object, ByVal e As System.EventArgs) Handles mfrmParent.ResizeBegin
        mfrmParent.SuspendLayout()
    End Sub

    Private Sub mfrmParent_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles mfrmParent.ResizeEnd
        mfrmParent.ResumeLayout()
    End Sub
End Class

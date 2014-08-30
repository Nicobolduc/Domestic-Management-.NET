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
            SetButtonsReadRights()
        End Set
    End Property

    Public WriteOnly Property ChangeMade As Boolean
        Set(ByVal value As Boolean)
            mblnChangeMade = value

            SetButtonsReadRights()
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



    Public Function bln_ShowForm(ByVal vintFormMode As clsConstants.Form_Modes, Optional ByVal vintItem_ID As Integer = 0, Optional ByVal vblnIsModal As Boolean = False) As Boolean
        Dim blnReturn As Boolean

        mintFormMode = vintFormMode
        mintItem_ID = vintItem_ID

        Try
            mfrmParent = MyBase.FindForm()

            SetButtonsReadRights()

            LoadFormData

            If Not vblnIsModal Then
                mfrmParent.MdiParent = My.Forms.mdiGeneral

                mfrmParent.Show()
            Else
                mfrmParent.MdiParent = Nothing

                mfrmParent.ShowDialog()
            End If

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Sub SetButtonsReadRights()
        Select Case mintFormMode
            Case clsConstants.Form_Modes.INSERT
                btnApply.Text = "Enregistrer"
                imgFormMode.Image = My.Resources.Add

            Case clsConstants.Form_Modes.UPDATE
                btnApply.Text = "Appliquer"
                imgFormMode.Image = My.Resources.Update

        End Select

        Select Case mintFormMode
            Case clsConstants.Form_Modes.INSERT, clsConstants.Form_Modes.UPDATE
                If mblnChangeMade Then
                    btnApply.Enabled = True
                    btnCancel.Enabled = True
                    btnQuit.Enabled = False
                Else
                    btnApply.Enabled = False
                    btnCancel.Enabled = False
                    btnQuit.Enabled = True
                End If

            Case clsConstants.Form_Modes.DELETE
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

        RaiseEvent SetReadRights()

        RaiseEvent LoadData(New LoadDataEventArgs(mintItem_ID))

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
                    Case clsConstants.Form_Modes.INSERT
                        FormMode = clsConstants.Form_Modes.UPDATE
                        LoadFormData()

                    Case clsConstants.Form_Modes.UPDATE
                        LoadFormData()

                    Case clsConstants.Form_Modes.DELETE
                        mfrmParent.Close()

                End Select
            Else
                gcApp.bln_ShowMessage(clsConstants.Error_Messages.ERROR_SAVE_MSG, MsgBoxStyle.Critical)
            End If
        Else
            'Do nothing
        End If

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        LoadFormData()
    End Sub

    Private Sub btnQuit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuit.Click
        mfrmParent.Close()
        Me.Dispose()
    End Sub

    Private Sub ctlFormControler_PropertyChanged() Handles Me.PropertyChanged
        SetControlsVisility()
    End Sub

End Class

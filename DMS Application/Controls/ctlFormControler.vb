Imports System.ComponentModel


Public Class ctlFormControler
    Inherits System.Windows.Forms.UserControl
    Implements INotifyPropertyChanged

    'Private members
    Private mintItem_ID As Integer
    Private mintFormMode As clsConstants.Form_Modes
    Private mblnChangeMade As Boolean
    Private mblnShowButtonQuitOnly As Boolean

    Private WithEvents mfrmParent As System.Windows.Forms.Form

    'Public members
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public Event SetReadRights()
    Public Event LoadData(ByVal eventArgs As LoadDataEventArgs)
    Public Event SaveData(ByVal eventArgs As SaveDataEventArgs)

#Region "Properties"

    Public ReadOnly Property GetItem_ID As Integer
        Get
            Return mintItem_ID
        End Get
    End Property

    Public ReadOnly Property GetFormMode As clsConstants.Form_Modes
        Get
            Return mintFormMode
        End Get
    End Property

    Public WriteOnly Property ChangeMade As Boolean
        Set(ByVal value As Boolean)
            mblnChangeMade = value

            SetButtonsReadRights()
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

            Select Case mintFormMode
                Case clsConstants.Form_Modes.INSERT
                    btnApply.Text = "Enregistrer"
                    imgFormMode.Image = My.Resources.Add

                Case clsConstants.Form_Modes.UPDATE
                    btnApply.Text = "Appliquer"
                    imgFormMode.Image = My.Resources.Update

                Case clsConstants.Form_Modes.DELETE
                    btnApply.Text = "Supprimer"
                    imgFormMode.Image = My.Resources.Delete

            End Select

            RaiseEvent SetReadRights()

            RaiseEvent LoadData(New LoadDataEventArgs(mintItem_ID))

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
            Case clsConstants.Form_Modes.INSERT, clsConstants.Form_Modes.UPDATE
                If mblnChangeMade Then
                    btnApply.Enabled = True
                    btnCancel.Enabled = True
                Else
                    btnApply.Enabled = False
                    btnCancel.Enabled = False
                End If

        End Select
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
    End Sub

    Private Sub btnApply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Dim saveEvent As New SaveDataEventArgs

        RaiseEvent SaveData(saveEvent)

        If saveEvent.SaveSuccessful Then
            mfrmParent.Close()
        Else
            gcApp.bln_ShowMessage(clsConstants.Messages.ERROR_SAVE_MSG, MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

    End Sub

    Private Sub btnQuit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuit.Click
        mfrmParent.Close()
        Me.Dispose()
    End Sub

    Private Sub ctlFormControler_PropertyChanged() Handles Me.PropertyChanged
        SetControlsVisility()
    End Sub
End Class

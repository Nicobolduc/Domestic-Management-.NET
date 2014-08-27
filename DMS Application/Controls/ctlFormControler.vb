

Public Class ctlFormControler
    Inherits System.Windows.Forms.UserControl

    Private mintItem_ID As Integer
    Private mintFormMode As clsConstants.Form_Modes
    Private mblnChangeMade As Boolean
    Private mblnShowButtonQuitOnly As Boolean

    Private WithEvents mfrmParent As System.Windows.Forms.Form

    Public Event SetReadRights()
    Public Event LoadData(ByVal eventArgs As LoadDataEventArgs)
    Public Event SaveData(ByVal eventArgs As SaveDataEventArgs)

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

    Public WriteOnly Property ShowButtonQuitOnly As Boolean
        Set(ByVal value As Boolean)
            mblnShowButtonQuitOnly = value
        End Set
    End Property

    Public Function bln_ShowForm(ByVal vintFormMode As clsConstants.Form_Modes, Optional ByVal vintItem_ID As Integer = 0, Optional ByVal vblnIsModal As Boolean = False) As Boolean
        Dim blnReturn As Boolean

        mintFormMode = vintFormMode
        mintItem_ID = vintItem_ID

        Try
            mfrmParent = MyBase.FindForm()

            Select Case mintFormMode
                Case clsConstants.Form_Modes.INSERT
                    btnApply.Text = "Enregistrer"

                Case clsConstants.Form_Modes.UPDATE
                    btnApply.Text = "Appliquer"

                Case clsConstants.Form_Modes.DELETE
                    btnApply.Text = "Supprimer"

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

    Private Function bln_DisableAllFormsControls(Optional ByRef rTabPage As TabPage = Nothing) As Boolean
        Dim blnReturn As Boolean
        Dim controlCollection As System.Windows.Forms.Control.ControlCollection

        Try
            If Not rTabPage Is Nothing Then
                controlCollection = rTabPage.Controls
            Else
                controlCollection = Me.Controls
            End If

            'For Each objControl As Object In controlCollection
            '    On Error Resume Next

            '    Select Case objControl.GetType.Name
            '        Case "Button", "TextBox", "CheckBox", "RadioButton", "ctlTTDateTimePicker", "ctlTTTextCombo_3PL", "ListView", "ComboBox"
            '            If objControl.name <> "_cmdCompagnie_9" And objControl.name <> "_cmdCompagnie_10" And objControl.name <> "_cmdCompagnie_11" And objControl.name <> "_cmdCompagnie_15" Then
            '                blnReturn = gcTTAPP.bln_CTLDisabled(objControl)
            '            End If

            '        Case "GroupBox"
            '            blnReturn = bln_DisableAllFormsControls(objControl)

            '        Case "ctlTTSSTab"
            '            For Each tp As TabPage In DirectCast(objControl, TabPage).TabPageControlCollection
            '                blnReturn = pfblnDisableControlsOnLoad(tp)
            '            Next

            '        Case Else
            '            'Do Nothing
            '    End Select

            '    If Not blnReturn Then Exit For
            'Next objControl

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

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

End Class

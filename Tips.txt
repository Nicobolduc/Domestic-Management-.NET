



Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("ctlTTFormDetail_NET.ctlTTFormDetail")> Public Class ctlTTFormDetail
	Inherits System.Windows.Forms.UserControl
	Public Event EnabledChange()
	Private Const mstrCONTROL_NAME As String = "CONTROL DETAIL "
	Private Const mlngMinLenght As Integer = 5800
    Private Const mintMaxItem As Short = 32767

    Private Const mintLoading_cap As Long = 900305
	
    Private mstrCAPTION As String = gvbNullString
    Private mstrIcon As String = gvbNullString
    Private mstrTable As String = gvbNullString
    Private mstrFieldNRI As String = gvbNullString

    Private mintVisibleMode As Short
    Private mintModeDepart As Short

    Private mlngRight As Short

    Private mlngCurrentNRI As Integer
    Private mlngTTLG_NRI As Integer

    Private mcListeNRI As TT3DLL.clsTTItemNRI
    Private mcUserRight As TT3DLL.clsTTUserRight
    Private mcAppForm As clsAppForm
    Private mcTTMDI As ctlTTFormMDI
    Private mcSQL As TT3DLL.clsTTSQL

    Private mblnMenuAuto As Boolean


    Private WithEvents mfrmParent As System.Windows.Forms.Form

    Private mToolTip As ToolTip

    Private mintFormMode As Short
    Private mblnInitialized As Boolean
    Private mblnLoading As Boolean
    Private mblnModeChange As Boolean
    Private mblnChangeMade As Boolean
    Private mblnLoadDataValide As Boolean

    Private mblnDoingSHOW As Boolean

    Private mblnScreenShown As Boolean

    Private mrecRecordLinked As System.Data.DataTable

    Public mblnIsDuplicateMode As Boolean

    'Loading Event
    Public Event FillControl(ByVal Sender As System.Object, ByRef e As FillControlEventArgs)
    Public Event SetReadRight(ByVal Sender As System.Object, ByVal e As SetReadRightEventArgs)
    Public Event LoadItemData(ByVal Sender As System.Object, ByVal e As LoadItemDataEventArgs)
    Public Event BeforeNRIValidation(ByVal Sender As System.Object, ByVal e As BeforeNRIValidationEventArgs)
    Public Event ApplyOptions(ByVal Sender As System.Object, ByVal e As ApplyOptionsEventArgs)
    Public Event AfterOpen(ByVal Sender As System.Object, ByVal e As AfterOpenEventArgs)
    'Saving Event
    Public Event ValideRules(ByVal Sender As System.Object, ByRef e As ValideRulesEventArgs)
    Public Event SaveData(ByVal Sender As System.Object, ByRef e As SaveDataEventArgs)

    Public Event BeforeChangeMode(ByVal Sender As System.Object, ByVal e As BeforeChangeModeEventArgs)
    'Comunication event
    Public Event BeNotify(ByVal Sender As System.Object, ByVal e As BeNotifyEventArgs)
    Public Event NotifyCaller(ByVal Sender As System.Object, ByVal e As NotifyCallerEventArgs)


    ReadOnly Property hWindow() As Integer
        Get
            hWindow = Handle.ToInt32
        End Get
    End Property

    ReadOnly Property ParentName() As Object
        Get
            'UPGRADE_WARNING: Couldn't resolve default property of object ParentName. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            ParentName = mcAppForm.ParentName
        End Get
    End Property


    Property ListMRI() As Integer
        Get
            ListMRI = mlngTTLG_NRI
        End Get
        Set(ByVal Value As Integer)
            If Not IsNothing(mcAppForm) Then
                Call pfblnChangeCurrentList(Value)
            End If
        End Set
    End Property

    WriteOnly Property VisibleMode() As Short
        Set(ByVal Value As Short)
            mintVisibleMode = Value
            Call pfblnVisibleMode()
        End Set
    End Property


    '****** BEGIN PUBLIC PROPERTY *******
    WriteOnly Property ChangeMade() As Boolean
        Set(ByVal Value As Boolean)

            If mblnDoingSHOW Then
                'do nothing
            Else
                If mintFormMode = TT3DLL.clsConstante.TTFormMode.TTFM_CONSULTE Then
                    'Change not valide
                    mblnChangeMade = False
                Else
                    If mblnChangeMade <> Value Then
                        mblnChangeMade = Value
                        Call psChangeMade()
                    Else
                        'Do nothing
                    End If
                End If

            End If
        End Set
    End Property

    'Permet de geler control
    Public Shadows WriteOnly Property Enabled() As Boolean
        Set(ByVal Value As Boolean)
            'mettre les doit de l'usager
            Call psSetControlRight()
            MyBase.Enabled = Value
            RaiseEvent EnabledChange()
        End Set
    End Property

    'Permet de changer le fromMode du control
    Public Property FormMode() As Short
        Get
            FormMode = mintFormMode
        End Get
        Set(ByVal Value As Short)
            'MP: le form mode est appele sur le NEW des forms...
            If mcAppForm Is Nothing Then
                'do nothing
            Else
                Call pfblnSetMode(Value)
            End If
        End Set
    End Property

    Public ReadOnly Property CurrentIndex() As Integer
        Get
            Return mcListeNRI.lng_CurrentIndex
        End Get
    End Property


    Public Property ItemNRI() As Integer
        Get

            If Not mcListeNRI Is Nothing Then
                If mcListeNRI.Count > 0 Then
                    ItemNRI = mcListeNRI((mcListeNRI.lng_CurrentIndex)).lng_ItemNRI
                Else
                    ItemNRI = 0
                End If
            Else
                ItemNRI = 0
            End If

        End Get
        Set(ByVal Value As Integer)
            Dim lngCount As Integer
            Dim blnFound As Boolean
            Dim blnTry As Boolean

            blnTry = False

            If Not IsNothing(mcAppForm) Then
                If mcListeNRI.Count >= mcListeNRI.lng_CurrentIndex Then
                    If Value = mcListeNRI((mcListeNRI.lng_CurrentIndex)).lng_ItemNRI Then
                        scrTool.Value = mcListeNRI.lng_CurrentIndex
                        Me.FormMode = TT3DLL.clsConstante.TTFormMode.TTFM_CONSULTE
                    Else
                        blnTry = True
                    End If
                Else
                    blnTry = True
                End If

                If blnTry Then
                    blnFound = False

                    For lngCount = 1 To mcListeNRI.Count
                        If Value = mcListeNRI(lngCount).lng_ItemNRI Then
                            mcListeNRI.lng_CurrentIndex = lngCount
                            scrTool.Value = lngCount
                            blnFound = True
                            Exit For
                        End If
                    Next lngCount

                    If Not blnFound Then
                        'Ajoute le NRI
                        Call fblnAddItemNRI(Value)
                    End If
                End If
            Else
                'do nothing
            End If

        End Set
    End Property


    'Public Function bln_Show(ByVal vlngTTLG_NRI As Integer, ByVal vcListeNRI As TT3DLL.clsTTItemNRI, ByRef rTTAppForm As Object, ByVal vintFormMode As TT3DLL.clsConstante.TTFormMode, ByVal vlngHwndParent As Integer, Optional ByRef vblnDisableParent As Boolean = True, Optional ByRef rblnUserRightOK As Boolean = False, Optional ByVal vblnIsContainedInMDI As Boolean = True, Optional ByVal vrecRecordToLink As ADODB.Recordset = Nothing) As Boolean
    Public Function bln_Show(ByVal vlngTTLG_NRI As Integer, ByVal vcListeNRI As TT3DLL.clsTTItemNRI, ByRef rTTAppForm As Object, ByVal vintFormMode As TT3DLL.clsConstante.TTFormMode, ByVal vlngHwndParent As Integer, Optional ByRef vblnDisableParent As Boolean = True, Optional ByRef rblnUserRightOK As Boolean = False, Optional ByVal vblnIsContainedInMDI As Boolean = True, Optional ByVal vrecRecordToLink As System.Data.DataTable = Nothing, Optional ByVal vblnIsModal As Boolean = False) As Boolean
        On Error GoTo Error_bln_Show
        Const strFCT_NAME As String = "bln_Show"
        Dim blnReturn As Boolean
        Dim blnValide As Boolean
        Dim blnMenuValide As Boolean
        Dim blnCancel As Boolean
        Dim objFill As New FillControlEventArgs(blnValide)
        Dim objBeforeNRI As New BeforeNRIValidationEventArgs(blnCancel, 0)
        Dim returnValue As IntPtr
        Dim b As Bitmap
        Dim sizSizeOri As Size
        Dim sizSizeStart As Size
        Dim strTitle As String = gvbNullString
        Dim strTitleOri As String = gvbNullString

        'Init
        blnReturn = True
        mblnModeChange = True
        mblnDoingSHOW = True
        mblnLoading = True
        mblnScreenShown = False

        mToolTip = New ToolTip

        mrecRecordLinked = vrecRecordToLink

        If mrecRecordLinked Is Nothing Then
            mnuRemplirListe.Visible = False
        End If

        'Control AppConnect qui gere toute les formes ouverte de l'application
        mcTTMDI = rTTAppForm

        ' Classe NRI DE LA LISTE GENERICK
        mcSQL = New TT3DLL.clsTTSQL
        mlngTTLG_NRI = vlngTTLG_NRI

        Call pfblnColNRI_ProtectOverFlow(vcListeNRI, vintFormMode)
        mcListeNRI = vcListeNRI 'COLLECTION DES ITEMS NRI

        mintModeDepart = vintFormMode
        mintFormMode = vintFormMode 'MODE DE LA FORME

        'Forme parent du control
        mfrmParent = MyBase.FindForm


        ' Faire en sorte que si la form est plus grande que le desktop du user, de mettre la form scrollable et pleine largeur.
        'If (Screen.PrimaryScreen.Bounds.Width <= gintSmallScreenWidth Or Screen.PrimaryScreen.Bounds.Height <= gintSmallScreenHeight) Then
        '    If (mfrmParent.Bounds.Width >= gintSmallScreenWidth Or mfrmParent.Bounds.Height >= gintSmallScreenHeight) Then
        '        mfrmParent.AutoScroll = True
        '        mfrmParent.Dock = DockStyle.Fill
        '    End If
        'End If

        If (mfrmParent.Bounds.Width >= Screen.PrimaryScreen.Bounds.Width Or mfrmParent.Bounds.Height >= Screen.PrimaryScreen.Bounds.Height) Then
            mfrmParent.AutoScroll = True
            mfrmParent.Dock = DockStyle.Fill
        End If

        'MP: doit etre fait en premier et on doit pas faire du non visible a visible pcq le handle change...
        'MP: deplace plus loin... fait afficher la form
        'mfrmParent.Show()

        mfrmParent.KeyPreview = True

        If vblnIsContainedInMDI Then
            mfrmParent.MdiParent = rTTAppForm.mfrmMDI
        End If

        'Class Form  qui enregitre la fome dans la collection des formes
        ' et qui gere la relation parent enfant
        mcAppForm = New clsAppForm
        'MP: Deplace plus loin... affiche la form...
        'mcAppForm.MyHandle = mfrmParent.Handle.ToInt32
        mcAppForm.MyName = mfrmParent.Name
        mcAppForm.ParentHandle = vlngHwndParent
        mcAppForm.mblnDisableParent = vblnDisableParent
        mcAppForm.AppControl = Me
        ' Call mcTTMDI.mcTTAPP.smousewheel_Hook(mcAppForm.MyHandle)

        If mlngTTLG_NRI = TT3DLL.clsConstante.TTLISTEGEN.TTLG_NOLISTE Then
            mlngRight = 15
        Else
            'Gere les droits de l'usager sur le controle
            mlngRight = mcTTMDI.mcTTAPP.cUser.lngGetListeUserRight(mlngTTLG_NRI, mcTTMDI.mcTTAPP.mcnAdoConnection)
        End If

        mcUserRight = New TT3DLL.clsTTUserRight
        mcUserRight.UserRight = mlngRight

        rblnUserRightOK = False

        Call pfblnUserHasRights(blnMenuValide)

        If blnMenuValide Then
            rblnUserRightOK = True
            'Enregistre la form qui contient le control

            Call mcTTMDI.mcTTAPP.bln_BindCaption(mfrmParent, , , mToolTip)

            'Deplace dans le bind caption
            'Call bln_SetControlCaption

            If mlngTTLG_NRI <> TT3DLL.clsConstante.TTLISTEGEN.TTLG_NOLISTE Then
                Call pfblnGetFormDetail()
                mfrmParent.Text = mstrCAPTION
                If Not mcTTMDI.mcTTAPP.imgICON Is Nothing Then
                    On Error Resume Next
                    b = mcTTMDI.mcTTAPP.imgICON.Images.Item(UCase(mstrIcon))
                    returnValue = b.GetHicon

                    mfrmParent.Icon = Icon.FromHandle(returnValue)
                    On Error GoTo Error_bln_Show
                Else
                    'Do nothing
                End If
            Else
                'pas d'information dans liste Gen
                'Do nothing
            End If

            'Empecher le flicker un peu...
            sizSizeOri = mfrmParent.Size
            mfrmParent.Size = New Size(sizSizeOri.Width, 1)

            If Not vblnIsModal Then
                mfrmParent.Show()
            End If

            strTitle = mfrmParent.Text
            mfrmParent.Text = strTitle & " (" & mcTTMDI.mcTTAPP.str_GetCaption(mintLoading_cap, mcTTMDI.mcTTAPP.cUser.Langue) & ")"
            strTitleOri = mfrmParent.Text

            sizSizeStart = mfrmParent.Size

            RaiseEvent FillControl(Me, objFill)
            If objFill.Valide Then
                If mintFormMode = TT3DLL.clsConstante.TTFormMode.TTFM_INSERT Then
                    RaiseEvent BeforeNRIValidation(Me, objBeforeNRI)
                Else
                    objBeforeNRI.vlngNRI = mcListeNRI((mcListeNRI.lng_CurrentIndex)).lng_ItemNRI
                    RaiseEvent BeforeNRIValidation(Me, objBeforeNRI)
                End If

                If Not blnCancel Then
                    blnReturn = pfblnSetMode(mintFormMode)

                    If blnReturn Then
                        If Not mfrmParent Is Nothing Then
                            mblnDoingSHOW = False

                            'mfrmParent.Show()

                            mfrmParent.Left = 0
                            mfrmParent.Top = 0

                            If mfrmParent.Size <> sizSizeStart Then
                                'Size change lors du load de form (comme la liste gen)

                                'On remet les originaux sur les valeurs encore pareilles comme a l origine
                                Select Case True
                                    Case sizSizeStart.Width = mfrmParent.Size.Width
                                        mfrmParent.Size = New Size(sizSizeOri.Width, mfrmParent.Size.Height)
                                    Case sizSizeStart.Height = mfrmParent.Size.Height
                                        mfrmParent.Size = New Size(mfrmParent.Size.Width, sizSizeOri.Height)
                                    Case Else
                                        'impossible
                                End Select
                            Else
                                mfrmParent.Size = sizSizeOri
                            End If

                            If strTitleOri <> mfrmParent.Text Then
                                'do nothing:change au runtime de la form
                            Else
                                mfrmParent.Text = strTitle
                            End If

                            'EF:Ouverture en modal
                            If vblnIsModal Then
                                If mblnLoadDataValide Then
                                    'EF:Ces deux paramètres doivent être à False pour qu'on puisse ouvrir la fenêtre modalement, sinon VB n'aime pas ça
                                    mfrmParent.Visible = False
                                    mfrmParent.MdiParent = Nothing

                                    'EF:La forme n'apparait que sous la forme de la bar titre, il faut lui redonner sa hauteur
                                    mfrmParent.Size = New Size(mfrmParent.Size.Width, sizSizeOri.Height)

                                    'EF:Afin de simuler un vrai formulaire modal
                                    mfrmParent.MinimizeBox = False
                                    mfrmParent.MaximizeBox = False
                                    mfrmParent.ShowInTaskbar = False
                                    'Quand c'est modal, il faut dire que le loading est fini
                                    mblnLoading = False
                                    If mblnIsDuplicateMode Then
                                        ChangeMade = True
                                    End If
                                    RaiseEvent AfterOpen(Me, Nothing)
                                    mfrmParent.ShowDialog()
                                Else
                                    mcTTMDI.mcTTAPP.bln_ShowMSG(TT3DLL.clsConstante.TTMESSAGE.TTMSG_ERRORLOADINGFORM, MsgBoxStyle.Critical)
                                End If
                            End If

                            mcAppForm.MyHandle = mfrmParent.Handle.ToInt32
                            Call mcTTMDI.fbln_Register(mcAppForm)

                            'En modal (Public), est appelé avant le Show.  Fait pour pouvoir activé l'appliquer dès l'ouverture d'un écran
                            If Not vblnIsModal Then
                                RaiseEvent AfterOpen(Me, Nothing)
                            End If

                            mblnScreenShown = True
                        Else
                            'do nothing:form unloadee
                        End If
                    Else
                        'do nothing
                    End If
                Else
                    'le message est appele dans l'evenement de la form appelee
                    TT3DLL.clsFonction.Unload(MyBase.FindForm)
                End If
            Else
                MsgBox("FillControl Error", MsgBoxStyle.Information)
                TT3DLL.clsFonction.Unload(MyBase.FindForm)
                blnReturn = False
            End If
        Else
            blnReturn = False
        End If


Exit_bln_Show:
        returnValue = Nothing
        b = Nothing
        sizSizeOri = Nothing
        sizSizeStart = Nothing
        objFill = Nothing
        objBeforeNRI = Nothing
        mblnDoingSHOW = False
        mblnLoading = False
        bln_Show = blnReturn
        Exit Function

Error_bln_Show:
        blnReturn = False
        Call mcTTMDI.mcTTAPP.bln_LogError(Err, mstrCONTROL_NAME, strFCT_NAME)
        Resume Exit_bln_Show
    End Function

    Public Function bln_SetControlCaption() As Boolean
        On Error GoTo Error_bln_SetControlCaption
        Const strFCT_NAME As String = "bln_SetControlCaption"
        Dim blnReturn As Boolean

        blnReturn = False
        Select Case False
            Case pfblnSetControlCaption()
            Case Else
                blnReturn = True
        End Select

Exit_bln_SetControlCaption:
        bln_SetControlCaption = blnReturn
        Exit Function
Error_bln_SetControlCaption:
        blnReturn = False
        Call mcTTMDI.mcTTAPP.bln_LogError(Err, mstrCONTROL_NAME, strFCT_NAME)
        Resume Exit_bln_SetControlCaption
    End Function

    Private Sub cmdForm_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdForm.Click
        Dim Index As Short = cmdForm.GetIndex(eventSender)
        Dim blnValide As Boolean
        Dim intNew_Mode As TT3DLL.clsConstante.TTFormMode
        Dim objValide As New ValideRulesEventArgs(blnValide)
        Dim objSave As New SaveDataEventArgs(blnValide)

        Select Case Index
            Case TT3DLL.clsConstante.TTCommand.TTCMD_APPLIQUER

                RaiseEvent ValideRules(Me, objValide)

                If objValide.Valide Then

                    objSave.Valide = False

                    RaiseEvent SaveData(Index, objSave)

                    If objSave.Valide Then

                        mblnIsDuplicateMode = False ' Si on etait en mode Dupliquer, et bien on ne l'est plus

                        If FormMode = TT3DLL.clsConstante.TTFormMode.TTFM_DELETE Then Call pfblnDeleteITEM()

                        mblnLoading = True

                        Call pfblnGetUserMode(intNew_Mode)

                        If mcListeNRI.Count = 0 And (Not mcUserRight.Ajouter) Then

                            If (mcAppForm.ParentHandle <> 0) Then
                                Call EnableWindow(mcAppForm.ParentHandle, 1)
                            End If

                            TT3DLL.clsFonction.Unload(MyBase.FindForm)
                        Else
                            Call pfblnSetMode(intNew_Mode)
                            mblnLoading = False
                            Me.ChangeMade = False
                        End If
                    Else
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If

            Case TT3DLL.clsConstante.TTCommand.TTCMD_ANNULER
                Call psAnnuler_Click()

            Case TT3DLL.clsConstante.TTCommand.TTCMD_QUITTER
                If (mcAppForm.ParentHandle <> 0) Then
                    Call EnableWindow(mcAppForm.ParentHandle, 1)
                End If

                TT3DLL.clsFonction.Unload(MyBase.FindForm)
                Exit Sub

            Case Else
                'Do nothing
        End Select

        objValide = Nothing
        objSave = Nothing
    End Sub

    Private Sub imgImage_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles imgImage.Click
        RaiseEvent BeforeChangeMode(Me, Nothing)
        Call psSetControlRight()
        'UPGRADE_ISSUE: UserControl method ctlTTFormDetail.PopupMenu was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
        Call TT3DLL.clsFonction.PopupMenu(mnuFormMode)
    End Sub

    Private Sub mfrmParent_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles mfrmParent.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        '123
        Select Case KeyCode
            Case System.Windows.Forms.Keys.F11
                Call mcTTMDI.mcTTAPP.bln_PrintForm(mfrmParent)

            Case System.Windows.Forms.Keys.F12
                Call mcTTMDI.mcTTAPP.bln_PrintScreen()
        End Select

    End Sub

    Public Sub mnuAuto_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuAuto.Click
        If mblnMenuAuto Then
            mblnMenuAuto = False
        Else
            mblnMenuAuto = True
        End If
        mnuAuto.Checked = mblnMenuAuto
    End Sub

    Public Sub mnuMode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuMode.Click
        Dim Index As Short = mnuMode.GetIndex(eventSender)
        Call pfblnSetMode(Index)
    End Sub
    Private Sub ctlTTFormDetail_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        MyBase.Height = VB6.TwipsToPixelsY(615)

        If VB6.PixelsToTwipsX(MyBase.Width) < mlngMinLenght Then MyBase.Width = VB6.TwipsToPixelsX(mlngMinLenght)

        If cmdForm.Count > 0 Then
            cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_QUITTER).Left = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(MyBase.Width) - VB6.PixelsToTwipsX(cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_QUITTER).Width))
            cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_ANNULER).Left = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(MyBase.Width) - (2 * VB6.PixelsToTwipsX(cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_QUITTER).Width)) - 25)
            cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_APPLIQUER).Left = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(MyBase.Width) - (3 * VB6.PixelsToTwipsX(cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_QUITTER).Width)) - 60)
        End If
    End Sub


    Private Function pfblnSetCaption() As Boolean
        On Error GoTo Error_pfblnSetCaption
        Const strFCT_NAME As String = "pfblnSetCaption "
        Dim intCount As Short
        Dim blnReturn As Boolean
        Dim intPositionStart As Short
        Dim intPositionEnd As Short

        blnReturn = True

        intPositionStart = 1

        For intCount = 0 To cmdForm.Count - 1
            intPositionEnd = InStr(intPositionStart, mstrCAPTION, "|", CompareMethod.Text)
            If intPositionEnd = 0 Then intPositionEnd = Len(mstrCAPTION) + 1
            cmdForm(intCount).Text = Mid(mstrCAPTION, intPositionStart, intPositionEnd - intPositionStart)
            intPositionStart = intPositionEnd + 1
        Next intCount

Exit_pfblnSetCaption:
        pfblnSetCaption = blnReturn
        Exit Function

Error_pfblnSetCaption:
        blnReturn = False
        Call mcTTMDI.mcTTAPP.bln_LogError(Err, mstrCONTROL_NAME, strFCT_NAME)
        Resume Exit_pfblnSetCaption

    End Function

    Private Function pfblnInitScrollBar() As Boolean
        On Error GoTo Error_pfblnInitScrollBar
        Const strFCT_NAME As String = "pfblnInitScrollBar"
        Dim blnReturn As Boolean

        blnReturn = True
        mblnInitialized = True

        'Tableau des nri
        If mcListeNRI.Count > 0 Then
            scrTool.Minimum = 1
            scrTool.Maximum = (mcListeNRI.Count + scrTool.LargeChange - 1)
            scrTool.Value = mcListeNRI.lng_CurrentIndex
        Else
            scrTool.Minimum = 0
            scrTool.Maximum = (0 + scrTool.LargeChange - 1)
            scrTool.Value = 0
            mlngCurrentNRI = 0
            mcListeNRI.lng_CurrentIndex = 0
        End If

Exit_pfblnInitScrollBar:
        pfblnInitScrollBar = blnReturn
        Exit Function

Error_pfblnInitScrollBar:
        blnReturn = False
        Call mcTTMDI.mcTTAPP.bln_LogError(Err, mstrCONTROL_NAME, strFCT_NAME)
        Resume Exit_pfblnInitScrollBar
    End Function


    'UPGRADE_NOTE: scrTool.Change was changed from an event to a procedure. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="4E2DC008-5EDA-4547-8317-C9316952674F"'
    'UPGRADE_WARNING: HScrollBar event scrTool.Change has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
    Private Sub scrTool_Change(ByVal newScrollValue As Integer)

        If mblnLoading Then
            'Do nothing
        Else
            mblnDoingSHOW = True
            If newScrollValue > 0 Then
                mcListeNRI.lng_CurrentIndex = newScrollValue
                mblnLoading = True
                If mintModeDepart = TT3DLL.clsConstante.TTFormMode.TTFM_CONSULTE Then
                    Call pfblnSetMode(mintModeDepart)
                Else
                    If mcUserRight.Modifier Then
                        Call pfblnSetMode(TT3DLL.clsConstante.TTFormMode.TTFM_UPDATE)
                    Else
                        Call pfblnSetMode(TT3DLL.clsConstante.TTFormMode.TTFM_CONSULTE)
                    End If
                End If
                mblnLoading = False
            Else
                'Do nothing
            End If

            mblnDoingSHOW = False
            Call psSetLabelPosition()
        End If
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub


    Private Sub psSetControlRight()

        mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_INSERT).Enabled = mcUserRight.Ajouter
        mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_UPDATE).Enabled = mcUserRight.Modifier
        mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_DELETE).Enabled = mcUserRight.Retier
        mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_CONSULTE).Enabled = mcUserRight.Consulter

        If mcListeNRI.Count = 0 Then
            mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_UPDATE).Enabled = False
            mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_DELETE).Enabled = False
            mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_CONSULTE).Enabled = False
        End If

        If mcUserRight.Ajouter = False And mintFormMode = TT3DLL.clsConstante.TTFormMode.TTFM_INSERT Then
            mintFormMode = TT3DLL.clsConstante.TTFormMode.TTFM_CONSULTE
            mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_INSERT).Enabled = False
            mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_CONSULTE).Enabled = True
        Else
            'do nothing
        End If

    End Sub

    Private Sub psSetLabelPosition()
        Dim strPosition As String = gvbNullString

        strPosition = scrTool.Value & "/" & (scrTool.Maximum - scrTool.LargeChange + 1)

        If Len(strPosition) > 10 Then
            lblPosition.Text = CStr(scrTool.Value)
        Else
            lblPosition.Text = strPosition
        End If
    End Sub


    Private Sub psChangeMade()

        If mintFormMode <> TT3DLL.clsConstante.TTFormMode.TTFM_DELETE Then

            If mblnChangeMade Then
                cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_APPLIQUER).Enabled = True
                cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_ANNULER).Enabled = True
                cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_QUITTER).Enabled = False
                scrTool.Enabled = False

                'On ne désactive plus les menus car si on modifie un
                'caption d'un choix du menu alors qu'il est désactivé
                'et que ce choix est sélectionné, on perd le crochet et
                'il ne revient que lorsqu'on re set le caption
                'NE JAMAIS DÉSACTIVÉ UN CHOIX DE MENU COCHÉ
                '            mnuMode(TTFM_INSERT).Enabled = False
                '            mnuMode(TTFM_UPDATE).Enabled = False
                '            mnuMode(TTFM_DELETE).Enabled = False
                '            mnuMode(TTFM_CONSULTE).Enabled = False
            Else
                cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_APPLIQUER).Enabled = False
                cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_ANNULER).Enabled = True
                cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_QUITTER).Enabled = True
                If mintFormMode <> TT3DLL.clsConstante.TTFormMode.TTFM_INSERT Then
                    scrTool.Enabled = True
                Else
                    scrTool.Enabled = False
                End If

                Call psSetControlRight()
            End If
        Else
            'CANGEMENT IMPOSSIBLE
        End If

    End Sub


    'On peut annuler en tout temp, et pas besion de
    'De valider si on gadre pas les modiffication,
    'Tombe en consulter
    Private Sub psAnnuler_Click()
        mblnChangeMade = False

        'Si plus que 1 enregistrement
        If mcListeNRI.Count > 0 Then
            If mcUserRight.Modifier Then
                Call pfblnSetMode(TT3DLL.clsConstante.TTFormMode.TTFM_UPDATE)
            Else
                Call pfblnSetMode(TT3DLL.clsConstante.TTFormMode.TTFM_CONSULTE)
            End If
        Else
            'MP: aurait du etre la. Le cas: si on ouvre la fenetre en Insert avec la collection
            'vide, si on fait annuler, ca marche pas et on peut pas sortir del'ecran par quitter...
            If mcUserRight.Ajouter Then
                Call pfblnSetMode(TT3DLL.clsConstante.TTFormMode.TTFM_INSERT)
            End If
        End If
    End Sub

    'Vas chercher le Caption et Icon dans la Table TTListeGen
    Private Function pfblnGetFormDetail() As Boolean
        On Error GoTo Error_pfblnGetFormDetail
        Const strFCT_NAME As String = "pfblnGetFormDetail"
        Dim blnReturn As Boolean
        Dim strSQL As String = gvbNullString
        Dim recRec As ADODB.Recordset = Nothing

        strSQL = " SELECT  TTAC_NRI_FORM, TTLG_IconKey, TTLG_Table, TTLG_Prefix"
        strSQL = strSQL & " From TTListeGen"
        strSQL = strSQL & " WHERE TTLG_NRI = " & mlngTTLG_NRI

        blnReturn = False
        Select Case False
            Case mcSQL.bln_Refresh
            Case mcSQL.bln_ADOSelect(strSQL, recRec)
            Case Not recRec.EOF
            Case Else
                mstrCAPTION = mcTTMDI.mcTTAPP.str_GetCaption(CInt(recRec.Fields("TTAC_NRI_FORM").Value), mcTTMDI.mcTTAPP.cUser.Langue)
                mstrIcon = recRec.Fields("TTLG_IconKey").Value
                mstrTable = recRec.Fields("TTLG_Table").Value
                mstrFieldNRI = recRec.Fields("TTLG_Prefix").Value & "_NRI"
                blnReturn = True
        End Select

Exit_pfblnGetFormDetail:
        'UPGRADE_NOTE: Object recRec may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        recRec = Nothing
        pfblnGetFormDetail = blnReturn
        Exit Function

Error_pfblnGetFormDetail:
        blnReturn = False
        Call mcTTMDI.mcTTAPP.bln_LogError(Err, mstrCONTROL_NAME, strFCT_NAME)
        Resume Exit_pfblnGetFormDetail
    End Function


    'UPGRADE_WARNING: ParamArray vParam was changed from ByRef to ByVal. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="93C6A0DC-8C99-429A-8696-35FC4DCEFCCC"'
    Public Function fblnNotifyCaller(ByVal ParamArray vParam() As Object) As Boolean
        Const strFCT_NAME As String = "fblnUnregister"
        On Error GoTo Err_fblnUnregister
        Dim blnReturn As Boolean
        blnReturn = True

        blnReturn = mcTTMDI.fbln_NotifyCaller(mcAppForm, vParam)

Exit_fblnUnregister:
        fblnNotifyCaller = blnReturn
        Exit Function

Err_fblnUnregister:
        blnReturn = False
        Call mcTTMDI.mcTTAPP.bln_LogError(Err, mstrCONTROL_NAME, strFCT_NAME)
        Resume Exit_fblnUnregister
    End Function


    Friend Function fblnBeNotify(ByVal vstrCaller As String, ByRef vParam As Object) As Boolean
        Const strFCT_NAME As String = "fblnBeNotify"
        On Error GoTo Err_fblnBeNotify
        Dim blnReturn As Boolean
        blnReturn = True

        RaiseEvent BeNotify(Me, New BeNotifyEventArgs(vstrCaller, vParam))

Exit_fblnBeNotify:
        fblnBeNotify = blnReturn
        Exit Function

Err_fblnBeNotify:
        blnReturn = False
        Call mcTTMDI.mcTTAPP.bln_LogError(Err, mstrCONTROL_NAME, strFCT_NAME)
        Resume Exit_fblnBeNotify

    End Function


    Public Function fblnAddItemNRI(ByVal vlngItenNRI As Integer) As Boolean
        Const strFCT_NAME As String = "fblnAddItemNRI"
        On Error GoTo Err_fblnAddItemNRI
        Dim blnReturn As Boolean
        Dim intCount As Short

        blnReturn = True

        If vlngItenNRI <> 0 Then
            mcListeNRI.Add(vlngItenNRI)
            mcListeNRI.lng_CurrentIndex = mcListeNRI.Count
            'MP: a moins d'indication contraire, cette ligne cause probleme quand
            'on doit faire une sauvegarde sans passer par le bouton appliquer, exemple, on doit sauver
            'depuis un autre bouton. Si dans ce cas, on est en mode insert, qu'on switch manuellement en mode update
            'on ne peut plus scroller avec les fleches (les chiffres descendent, mais on change pas d'item).

            'mblnLoading = True
        End If

Exit_fblnAddItemNRI:
        fblnAddItemNRI = blnReturn
        Exit Function

Err_fblnAddItemNRI:
        blnReturn = False
        Call mcTTMDI.mcTTAPP.bln_LogError(Err, mstrCONTROL_NAME, strFCT_NAME)
        Resume Exit_fblnAddItemNRI
    End Function

    Private Function pfblnDeleteITEM() As Boolean
        Const strFCT_NAME As String = "pfblnDeleteITEM"
        On Error GoTo Err_pfblnDeleteITEM
        Dim blnReturn As Boolean
        Dim lngCurrentIndex As Integer

        blnReturn = True


        lngCurrentIndex = mcListeNRI.lng_CurrentIndex - 1
        'Enleve l'index courrant
        Call mcListeNRI.Remove((mcListeNRI.lng_CurrentIndex))
        'Se repositionne sur l'Enregistrement precedant
        mcListeNRI.lng_CurrentIndex = lngCurrentIndex

        If mcListeNRI.lng_CurrentIndex = 0 And mcListeNRI.Count > 0 Then
            mcListeNRI.lng_CurrentIndex = 1
        Else
            'Do Nothing
        End If

Exit_pfblnDeleteITEM:
        pfblnDeleteITEM = blnReturn
        Exit Function

Err_pfblnDeleteITEM:
        blnReturn = False
        Call mcTTMDI.mcTTAPP.bln_LogError(Err, mstrCONTROL_NAME, strFCT_NAME)
        Resume Exit_pfblnDeleteITEM
    End Function


    Private Function pfblnVisibleMode() As Boolean
        On Error GoTo Error_pfblnVisibleMode
        Const strFCT_NAME As String = "pfblnVisibleMode"
        Dim blnReturn As Boolean
        Dim cMode As New TT3DLL.clsTTUserRight

        blnReturn = True
        cMode.UserRight = mintVisibleMode

        If Not mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_INSERT).Checked Then mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_INSERT).Visible = cMode.Ajouter
        If Not mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_UPDATE).Checked Then mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_UPDATE).Visible = cMode.Modifier
        If Not mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_DELETE).Checked Then mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_DELETE).Visible = cMode.Retier
        If Not mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_CONSULTE).Checked Then mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_CONSULTE).Visible = cMode.Consulter

Exit_pfblnVisibleMode:
        'UPGRADE_NOTE: Object cMode may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        cMode = Nothing
        pfblnVisibleMode = blnReturn
        Exit Function

Error_pfblnVisibleMode:
        blnReturn = False
        Call mcTTMDI.mcTTAPP.bln_LogError(Err, mstrCONTROL_NAME, strFCT_NAME)
        Resume Exit_pfblnVisibleMode
    End Function


    Public Sub Quit()
        mfrmParent.Size = New Size(mfrmParent.Width, 1)
        mfrmParent.Font = Nothing
        Call psClearFont(mfrmParent.Controls)

        '  Call mcTTMDI.mcTTAPP.smousewheel_unHook(mcAppForm.MyHandle)
        If mblnScreenShown Then
            Call EnableWindow(mcAppForm.ParentHandle, 1)
            Call fblnForm_AllowUpdating()
            RaiseEvent NotifyCaller(Me, New NotifyCallerEventArgs(mcAppForm.ParentName))
        End If

    End Sub


    'UPGRADE_ISSUE: UserControl event UserControl.Hide was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="ABD9AF39-7E24-4AFF-AD8D-3675C1AA3054"'
    Private Sub UserControl_Hide()
        Call pfblnUserControl_End()
    End Sub
    Private Sub UserControl_Terminate()
        Call pfblnUserControl_End()

        Me.Font = Nothing
        Frame1.Font = Nothing
        lblPosition.Font = Nothing
        MainMenu1.Font = Nothing
        mnuAuto.Font = Nothing
        mnuFormMode.Font = Nothing
        mnuRemplirListe.Font = Nothing

        Me.components = Nothing
    End Sub
    Private Function pfblnUserControl_End() As Boolean

        If Not mcTTMDI Is Nothing Then
            Call mcTTMDI.fbln_Unregister(mcAppForm)

            mToolTip.Dispose()
            mToolTip = Nothing

            'UPGRADE_NOTE: Object mcAppForm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            mcAppForm = Nothing
            'UPGRADE_NOTE: Object mcTTMDI may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            mcTTMDI = Nothing
            'UPGRADE_NOTE: Object mfrmParent may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            mfrmParent = Nothing
            'UPGRADE_NOTE: Object mcListeNRI may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            mcListeNRI = Nothing
            'UPGRADE_NOTE: Object mcUserRight may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            mcUserRight = Nothing
            mrecRecordLinked = Nothing
        End If

    End Function
    Private Sub psClearFont(ByRef rctlControl As System.Windows.Forms.Control.ControlCollection)
        Dim ctlControl As System.Windows.Forms.Control

        For Each ctlControl In rctlControl
            On Error Resume Next
            ctlControl.Font = Nothing

            If DirectCast(ctlControl, Control).HasChildren Then
                Call psClearFont(ctlControl.Controls)
            End If
        Next ctlControl

        ctlControl = Nothing
    End Sub

    '****************************************************************
    '* Nom de la fonction   : fblnRemoveCurrentItem
    '*
    '*               Cree   : 19-10-2001   lamorin
    '*            Modifie   : **-**-****  ***
    '*
    '*                But   :
    '*
    '* Parametre(s):
    '*              IN :
    '*
    '*              OUT:    True, si succes. False, si erreur
    '*
    '****************************************************************
    Public Function fblnRemoveCurrentItem() As Boolean
        On Error GoTo Error_fblnRemoveCurrentItem
        Const strFCT_NAME As String = "fblnRemoveCurrentItem"
        Dim blnReturn As Boolean
        Dim intNew_Mode As TT3DLL.clsConstante.TTFormMode

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        blnReturn = pfblnDeleteITEM()

Exit_fblnRemoveCurrentItem:
        'UPGRADE_ISSUE: Unable to determine which constant to upgrade vbNormal to. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3B44E51-B5F1-4FD7-AA29-CAD31B71F487"'
        'UPGRADE_ISSUE: Screen property Screen.MousePointer does not support custom mousepointers. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="45116EAB-7060-405E-8ABE-9DBB40DC2E86"'
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        fblnRemoveCurrentItem = blnReturn
        Exit Function

Error_fblnRemoveCurrentItem:
        blnReturn = False
        Call mcTTMDI.mcTTAPP.bln_LogError(Err, mstrCONTROL_NAME, strFCT_NAME)
        Resume Exit_fblnRemoveCurrentItem
    End Function

    '****************************************************************
    '* Nom de la fonction   : pfblnSetMode
    '*
    '*               Cree   : 22-10-2001   lamorin
    '*            Modifie   : **-**-****  ***
    '*
    '*                But   :
    '*
    '* Parametre(s):
    '*              IN :
    '*
    '*              OUT:    True, si succes. False, si erreur
    '*
    '****************************************************************
    Private Function pfblnSetMode(ByVal vlngMode As TT3DLL.clsConstante.TTFormMode) As Boolean
        On Error GoTo Error_pfblnSetMode
        Const strFCT_NAME As String = "pfblnSetMode"
        Dim blnReturn As Boolean
        Dim objLoadDataEventArgs As TT3OCX.ctlTTFormDetail.LoadItemDataEventArgs

        blnReturn = True

        mblnModeChange = True
        Me.ChangeMade = False

        mintFormMode = vlngMode
        Call fblnForm_StopUpdating(mfrmParent)

        'Si mode Ajouter
        If mintFormMode = TT3DLL.clsConstante.TTFormMode.TTFM_INSERT Then
            scrTool.Enabled = False
            mlngCurrentNRI = 0
            cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_APPLIQUER).Enabled = False
            cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_ANNULER).Enabled = True
            cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_QUITTER).Enabled = True

            mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_INSERT).Enabled = True
            mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_UPDATE).Enabled = False
            mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_DELETE).Enabled = False
            mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_CONSULTE).Enabled = False
        Else
            'Valide le NRI COURRANT
            If pfblnValideNRI() Then
                Select Case mintFormMode
                    Case TT3DLL.clsConstante.TTFormMode.TTFM_UPDATE
                        scrTool.Enabled = True
                        cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_ANNULER).Enabled = True
                        cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_APPLIQUER).Enabled = False
                        cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_QUITTER).Enabled = True

                    Case TT3DLL.clsConstante.TTFormMode.TTFM_DELETE
                        scrTool.Enabled = False
                        cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_ANNULER).Enabled = True
                        cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_APPLIQUER).Enabled = True
                        cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_QUITTER).Enabled = False

                    Case TT3DLL.clsConstante.TTFormMode.TTFM_CONSULTE
                        scrTool.Enabled = True
                        cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_ANNULER).Enabled = False
                        cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_APPLIQUER).Enabled = False
                        cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_QUITTER).Enabled = True

                    Case TT3DLL.clsConstante.TTFormMode.TTFM_INSERT
                        scrTool.Enabled = False
                        cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_APPLIQUER).Enabled = False
                        cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_ANNULER).Enabled = True
                        cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_QUITTER).Enabled = True

                        mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_INSERT).Enabled = True
                        mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_UPDATE).Enabled = False
                        mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_DELETE).Enabled = False
                        mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_CONSULTE).Enabled = False

                    Case Else
                        If mintFormMode = 16 Then 'mMain  gintModeDupliquer = 16
                            mintFormMode = 0
                            mblnIsDuplicateMode = True
                            scrTool.Enabled = False
                            cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_APPLIQUER).Enabled = False
                            cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_ANNULER).Enabled = True
                            cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_QUITTER).Enabled = True

                            mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_INSERT).Enabled = True
                            mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_UPDATE).Enabled = False
                            mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_DELETE).Enabled = False
                            mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_CONSULTE).Enabled = False
                        Else
                            'Do nothing
                        End If

                End Select
            Else
                blnReturn = False
            End If 'Fin si Valide le NRI COURRANT

        End If 'Fin si mode Ajouter

        If blnReturn Then
            Call psSetControlRight()
            Call pfblnInitScrollBar()
            Call psSetLabelPosition()

            mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_INSERT).Checked = False
            mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_UPDATE).Checked = False
            mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_DELETE).Checked = False
            mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_CONSULTE).Checked = False

            'mnuMode(mintFormMode).Checked = True
            'UPGRADE_WARNING: Lower bound of collection imgList.ListImages has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
            imgImage.Image = imgList.Images.Item(mintFormMode + 1 - 1)


            If mblnModeChange Then
                RaiseEvent SetReadRight(Me, New SetReadRightEventArgs(mintFormMode))
                mblnModeChange = False
            End If

            'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            mblnLoadDataValide = False
            objLoadDataEventArgs = New LoadItemDataEventArgs(mlngCurrentNRI)
            RaiseEvent LoadItemData(Me, objLoadDataEventArgs)
            mblnLoadDataValide = objLoadDataEventArgs.Valide
            'Applique les options de chacune des applications (key generator)
            RaiseEvent ApplyOptions(Me, Nothing)
        Else
            'do nothing
        End If

Exit_pfblnSetMode:
        Call fblnForm_AllowUpdating()
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        pfblnSetMode = blnReturn
        Exit Function

Error_pfblnSetMode:
        blnReturn = False
        Call mcTTMDI.mcTTAPP.bln_LogError(Err, mstrCONTROL_NAME, strFCT_NAME)
        Resume Exit_pfblnSetMode
    End Function

    '****************************************************************
    '* Nom de la fonction   : pfblnSetControlCaption
    '*
    '*               Cree   : 22-10-2001   lamorin
    '*            Modifie   : **-**-****  ***
    '*
    '*                But   :
    '*
    '* Parametre(s):
    '*              IN :
    '*
    '*              OUT:    True, si succes. False, si erreur
    '*
    '****************************************************************
    Private Function pfblnSetControlCaption() As Boolean
        On Error GoTo Error_pfblnSetControlCaption
        Const strFCT_NAME As String = "pfblnSetControlCaption"
        Dim blnReturn As Boolean
        Dim lngLangue As Integer

        blnReturn = True
        lngLangue = mcTTMDI.mcTTAPP.cUser.Langue

        cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_ANNULER).Text = mcTTMDI.mcTTAPP.str_GetCaption(CInt(cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_ANNULER).Tag), lngLangue)
        cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_APPLIQUER).Text = mcTTMDI.mcTTAPP.str_GetCaption(CInt(cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_APPLIQUER).Tag), lngLangue)
        cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_QUITTER).Text = mcTTMDI.mcTTAPP.str_GetCaption(CInt(cmdForm(TT3DLL.clsConstante.TTCommand.TTCMD_QUITTER).Tag), lngLangue)

        mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_INSERT).Text = mcTTMDI.mcTTAPP.str_GetCaption(TT3DLL.clsConstante.TTCAPTION.TTCAP_AJOUTER, lngLangue)
        mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_UPDATE).Text = mcTTMDI.mcTTAPP.str_GetCaption(TT3DLL.clsConstante.TTCAPTION.TTCAP_MODIFIER, lngLangue)
        mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_DELETE).Text = mcTTMDI.mcTTAPP.str_GetCaption(TT3DLL.clsConstante.TTCAPTION.TTCAP_RETIRER, lngLangue)
        mnuMode(TT3DLL.clsConstante.TTFormMode.TTFM_CONSULTE).Text = mcTTMDI.mcTTAPP.str_GetCaption(TT3DLL.clsConstante.TTCAPTION.TTCAP_CONSULTER, lngLangue)
        mnuAuto.Text = mcTTMDI.mcTTAPP.str_GetCaption(TT3DLL.clsConstante.TTCAPTION.TTCAP_AUTOMATIQUE, lngLangue)

        mnuRemplirListe.Text = mcTTMDI.mcTTAPP.str_GetCaption(mnuRemplirListe.Tag, lngLangue)


Exit_pfblnSetControlCaption:
        pfblnSetControlCaption = blnReturn
        Exit Function

Error_pfblnSetControlCaption:
        blnReturn = False
        Call mcTTMDI.mcTTAPP.bln_LogError(Err, mstrCONTROL_NAME, strFCT_NAME)
        Resume Exit_pfblnSetControlCaption
    End Function


    Private Function pfblnValideNRI() As Boolean
        On Error GoTo Error_pfblnValideNRI
        Const strFCT_NAME As String = "pfblnValideNRI"
        Dim blnReturn As Boolean
        Dim strReturnNRI As String = gvbNullString
        Dim lngNRI As Integer
        Dim blnNRIChange As Boolean

        blnNRIChange = False

        If mlngTTLG_NRI <> TT3DLL.clsConstante.TTLISTEGEN.TTLG_NOLISTE Then
            Do
                If mcListeNRI.Count > 0 Then
                    lngNRI = mcListeNRI((mcListeNRI.lng_CurrentIndex)).lng_ItemNRI
                    strReturnNRI = vbNullString
                    Select Case False
                        Case mcListeNRI.Count <> 0
                            mlngCurrentNRI = 0
                            mcListeNRI.lng_CurrentIndex = 0
                            strReturnNRI = CStr(0)
                        Case mcSQL.bln_Refresh
                        Case mcSQL.bln_ADOSingleLookUp(mstrTable, mstrFieldNRI, strReturnNRI, mstrFieldNRI & "=" & lngNRI)
                        Case strReturnNRI <> vbNullString
                            Select Case mintFormMode
                                Case TT3DLL.clsConstante.TTFormMode.TTFM_DELETE, TT3DLL.clsConstante.TTFormMode.TTFM_UPDATE
                                    mintFormMode = TT3DLL.clsConstante.TTFormMode.TTFM_CONSULTE
                                    mblnModeChange = True
                                    Call mcTTMDI.mcTTAPP.bln_ShowMSG(TT3DLL.clsConstante.TTMESSAGE.TTMSG_NOMOREVALIDE, MsgBoxStyle.Exclamation)
                                Case Else ' TTFM_CONSULTE
                                    'Do nothing
                            End Select
                            blnNRIChange = True
                            blnReturn = pfblnDeleteITEM()

                        Case Else
                            mlngCurrentNRI = mcListeNRI((mcListeNRI.lng_CurrentIndex)).lng_ItemNRI
                            blnReturn = True
                    End Select
                Else
                    If mcUserRight.Ajouter Then
                        mintFormMode = TT3DLL.clsConstante.TTFormMode.TTFM_INSERT
                        mblnModeChange = True
                        mlngCurrentNRI = 0
                        mcListeNRI.lng_CurrentIndex = 0
                        blnReturn = True
                    Else
                        'UPGRADE_WARNING: Control property UserControl.Parent was upgraded to UserControl.FindForm which has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"'
                        'UPGRADE_ISSUE: Unload UserControl.Parent was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="875EBAD7-D704-4539-9969-BC7DBDAA62A2"'
                        TT3DLL.clsFonction.Unload(MyBase.FindForm)
                        blnReturn = False
                    End If

                    Exit Do
                End If
            Loop While strReturnNRI = vbNullString And blnReturn
        Else
            'no liste
            mlngCurrentNRI = mcListeNRI((mcListeNRI.lng_CurrentIndex)).lng_ItemNRI
            blnReturn = True
        End If


Exit_pfblnValideNRI:
        pfblnValideNRI = blnReturn
        Exit Function

Error_pfblnValideNRI:
        blnReturn = False
        Call mcTTMDI.mcTTAPP.bln_LogError(Err, mstrCONTROL_NAME, strFCT_NAME)
        Resume Exit_pfblnValideNRI
    End Function

    '****************************************************************
    '* Nom de la fonction   : pfblnGetUserMode
    '*
    '*               Cree   : 30-09-2002   Alexis
    '*            Modifie   : **-**-****  ***
    '*
    '*                But   :
    '*
    '* Parametre(s):
    '*              IN :
    '*
    '*              OUT:    True, si succes. False, si erreur
    '*
    '****************************************************************
    Private Function pfblnGetUserMode(ByRef rintNEW_MODE As TT3DLL.clsConstante.TTFormMode) As Boolean
        On Error GoTo Error_pfblnGetUserMode
        Const strFCT_NAME As String = "pfblnGetUserMode"
        Dim blnReturn As Boolean

        blnReturn = True

        'Valide les droit de l'usager
        Select Case True
            Case Not mblnMenuAuto
                If mcUserRight.Ajouter = True And mcListeNRI.Count = 0 Then
                    rintNEW_MODE = TT3DLL.clsConstante.TTFormMode.TTFM_INSERT
                Else
                    If mintModeDepart = TT3DLL.clsConstante.TTFormMode.TTFM_CONSULTE Then
                        rintNEW_MODE = mintModeDepart
                    Else
                        If mcUserRight.Modifier Then
                            rintNEW_MODE = TT3DLL.clsConstante.TTFormMode.TTFM_UPDATE
                        Else
                            rintNEW_MODE = TT3DLL.clsConstante.TTFormMode.TTFM_CONSULTE
                        End If
                    End If
                End If
            Case Not mcUserRight.Ajouter And mcListeNRI.Count = 0
                If mintModeDepart = TT3DLL.clsConstante.TTFormMode.TTFM_CONSULTE Then
                    rintNEW_MODE = mintModeDepart
                Else
                    If mcUserRight.Modifier Then
                        rintNEW_MODE = TT3DLL.clsConstante.TTFormMode.TTFM_UPDATE
                    Else
                        rintNEW_MODE = TT3DLL.clsConstante.TTFormMode.TTFM_CONSULTE
                    End If
                End If

            Case Else
                rintNEW_MODE = mintFormMode

        End Select


Exit_pfblnGetUserMode:
        pfblnGetUserMode = blnReturn
        Exit Function

Error_pfblnGetUserMode:
        blnReturn = False
        Call mcTTMDI.mcTTAPP.bln_LogError(Err, mstrCONTROL_NAME, strFCT_NAME)
        Resume Exit_pfblnGetUserMode
    End Function


    '****************************************************************
    '* Nom de la fonction   : pfblnColNRI_ProtectOverFlow
    '*
    '*               Cree   : 07-08-2001
    '*            Modifie   : **-**-****  ***
    '*
    '*                But   :
    '*
    '* Parametre(s):
    '*              IN :
    '*
    '*              OUT:    True, si succes. False, si erreur
    '*
    '****************************************************************
    Private Function pfblnColNRI_ProtectOverFlow(ByRef rcNRI As TT3DLL.clsTTItemNRI, ByVal vintFormMode As Short) As Boolean
        On Error GoTo Error_pfblnColNRI_ProtectOverFlow
        Const strFCT_NAME As String = "pfblnColNRI_ProtectOverFlow"
        Dim blnReturn As Boolean
        Dim lngNRI As Integer

        blnReturn = True

        'On protege de l'overflow. Si la collection contient plus de 32000 items, la propriete max
        'dans VB cree un erreur partout. Donc si le cas survient, on va chercher le NRI et on refait la collection.

        If rcNRI.Count >= mintMaxItem Then
            If vintFormMode = TT3DLL.clsConstante.TTFormMode.TTFM_INSERT Then
                lngNRI = 0
            Else
                lngNRI = rcNRI((rcNRI.lng_CurrentIndex)).lng_ItemNRI
            End If

            rcNRI = New TT3DLL.clsTTItemNRI

            If lngNRI <> 0 Then
                rcNRI.Add(lngNRI, "" & lngNRI)
                rcNRI.lng_CurrentIndex = 1
            Else
                rcNRI.lng_CurrentIndex = 0
            End If
        Else
            'do nothing
        End If

Exit_pfblnColNRI_ProtectOverFlow:
        pfblnColNRI_ProtectOverFlow = blnReturn
        Exit Function

Error_pfblnColNRI_ProtectOverFlow:
        blnReturn = False
        Call mcTTMDI.mcTTAPP.bln_LogError(Err, mstrCONTROL_NAME, strFCT_NAME)
        Resume Exit_pfblnColNRI_ProtectOverFlow
    End Function

    '****************************************************************
    '* Nom de la fonction   : pfblnChangeCurrentList
    '*
    '*               Cree   : 07-09-2006  ¶émi
    '*            Modifie   : **-**-****  ***
    '*
    '*                But   :
    '*
    '* Parametre(s):
    '*              IN :
    '*
    '*              OUT:    True, si succes. False, si erreur
    '*
    '****************************************************************
    Private Function pfblnChangeCurrentList(ByVal vlngListNRI As Integer) As Boolean
        On Error GoTo Error_pfblnChangeCurrentList
        Const strFCT_NAME As String = "pfblnChangeCurrentList"
        Dim blnReturn As Boolean
        Dim blnMenuValide As Boolean

        blnReturn = True

        If mintFormMode = TT3DLL.clsConstante.TTFormMode.TTFM_INSERT Then
            mlngTTLG_NRI = vlngListNRI

            mcListeNRI = New TT3DLL.clsTTItemNRI

            blnReturn = False
            Select Case False
                Case pfblnUserHasRights(blnMenuValide)
                Case blnMenuValide
                Case pfblnGetFormDetail()
                Case Else
                    blnReturn = True
            End Select

            If blnReturn Then
                mfrmParent.Text = mstrCAPTION
                If Not mcTTMDI.mcTTAPP.imgICON Is Nothing Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object mcTTMDI.mcTTAPP.imgICON.ListImages. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    mfrmParent.Icon = New IconConverter().ConvertFrom(mcTTMDI.mcTTAPP.imgICON.Images.Item(UCase(mstrIcon)))
                Else
                    'Do nothing
                End If

                Call psSetControlRight()

                blnReturn = pfblnInitScrollBar()

                If blnReturn Then
                    Call psSetLabelPosition()
                Else
                    'do nothing
                End If
            Else
                'do nothing
            End If
        Else
            'do nothing : supporte seulement le mode insert
        End If

Exit_pfblnChangeCurrentList:
        pfblnChangeCurrentList = blnReturn
        Exit Function
Error_pfblnChangeCurrentList:
        blnReturn = False
        Call mcTTMDI.mcTTAPP.bln_LogError(Err, mstrCONTROL_NAME, strFCT_NAME)
        Resume Exit_pfblnChangeCurrentList
    End Function

    '****************************************************************
    '* Nom de la fonction   : pfblnUserHasRights
    '*
    '*               Cree   : 07-09-2006  ¶émi
    '*            Modifie   : **-**-****  ***
    '*
    '*                But   :
    '*
    '* Parametre(s):
    '*              IN :
    '*
    '*              OUT:    True, si succes. False, si erreur
    '*
    '****************************************************************
    Private Function pfblnUserHasRights(ByRef rblnMenuValide As Boolean) As Boolean
        On Error GoTo Error_pfblnUserHasRights
        Const strFCT_NAME As String = "pfblnUserHasRights"
        Dim blnReturn As Boolean

        blnReturn = True

        rblnMenuValide = False
        Select Case True
            'Aucun item dans la collection...
            Case mcListeNRI.Count = 0
                Select Case True
                    'on veut le mode insert, mais on a pas les droits: on peut pas...
                    Case mintFormMode = TT3DLL.clsConstante.TTFormMode.TTFM_INSERT And Not mcUserRight.Ajouter
                        Call mcTTMDI.mcTTAPP.bln_ShowMSG(TT3DLL.clsConstante.TTMESSAGE.TTMSG_NORIGHTTOOPEN, MsgBoxStyle.Information)
                        'UPGRADE_WARNING: Control property UserControl.Parent was upgraded to UserControl.FindForm which has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"'
                        'UPGRADE_ISSUE: Unload UserControl.Parent was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="875EBAD7-D704-4539-9969-BC7DBDAA62A2"'
                        TT3DLL.clsFonction.Unload(MyBase.FindForm)

                        'un mode different d'insert, il faut qu'on ait les droits d'inserer
                    Case mintFormMode <> TT3DLL.clsConstante.TTFormMode.TTFM_INSERT
                        If mcUserRight.Ajouter Then
                            mintFormMode = TT3DLL.clsConstante.TTFormMode.TTFM_INSERT
                            rblnMenuValide = True
                        Else
                            ' J'ai pas les droits pis j'ai pas d'items!!
                            Call mcTTMDI.mcTTAPP.bln_ShowMSG(TT3DLL.clsConstante.TTMESSAGE.TTMSG_NORIGHTTOOPEN, MsgBoxStyle.Information)
                            'UPGRADE_WARNING: Control property UserControl.Parent was upgraded to UserControl.FindForm which has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"'
                            'UPGRADE_ISSUE: Unload UserControl.Parent was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="875EBAD7-D704-4539-9969-BC7DBDAA62A2"'
                            TT3DLL.clsFonction.Unload(MyBase.FindForm)
                        End If

                    Case Else
                        rblnMenuValide = True
                End Select

                'On entre dans un mode auquel on a pas droit, il faut avoir les droits de consulter
            Case (Not mcUserRight.Modifier And mintFormMode = TT3DLL.clsConstante.TTFormMode.TTFM_UPDATE) Or (Not mcUserRight.Ajouter And mintFormMode = TT3DLL.clsConstante.TTFormMode.TTFM_INSERT) Or (Not mcUserRight.Retier And mintFormMode = TT3DLL.clsConstante.TTFormMode.TTFM_DELETE)

                If mcUserRight.Consulter Then
                    mintFormMode = TT3DLL.clsConstante.TTFormMode.TTFM_CONSULTE
                    rblnMenuValide = True
                Else
                    ' J'ai pas les droits de consulter ou de modifier!!
                    Call mcTTMDI.mcTTAPP.bln_ShowMSG(TT3DLL.clsConstante.TTMESSAGE.TTMSG_NORIGHTTOOPEN, MsgBoxStyle.Information)
                    'UPGRADE_WARNING: Control property UserControl.Parent was upgraded to UserControl.FindForm which has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"'
                    'UPGRADE_ISSUE: Unload UserControl.Parent was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="875EBAD7-D704-4539-9969-BC7DBDAA62A2"'
                    TT3DLL.clsFonction.Unload(MyBase.FindForm)
                End If

            Case Else
                rblnMenuValide = True
        End Select

Exit_pfblnUserHasRights:
        pfblnUserHasRights = blnReturn
        Exit Function
Error_pfblnUserHasRights:
        blnReturn = False
        Call mcTTMDI.mcTTAPP.bln_LogError(Err, mstrCONTROL_NAME, strFCT_NAME)
        Resume Exit_pfblnUserHasRights
    End Function
    Private Sub scrTool_Scroll(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.ScrollEventArgs) Handles scrTool.Scroll
        Select Case eventArgs.Type
            Case System.Windows.Forms.ScrollEventType.EndScroll
                scrTool_Change(eventArgs.NewValue)
        End Select
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub MainMenu1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles MainMenu1.ItemClicked

    End Sub

    Private Sub _cmdForm_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _cmdForm_2.Click

    End Sub



    Private Sub _mnuMode_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _mnuMode_2.Click

    End Sub

    Private Sub mnuRemplirListe_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuRemplirListe.Click
        Dim lngCurrNRI As Long

        lngCurrNRI = ItemNRI

        If Not mrecRecordLinked Is Nothing Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            mcListeNRI.bln_BuildNRIListe(mrecRecordLinked, mrecRecordLinked.Columns.Count - 1, lngCurrNRI)

            Call pfblnColNRI_ProtectOverFlow(mcListeNRI, FormMode)

            Call pfblnInitScrollBar()
            Call psSetLabelPosition()

            mnuRemplirListe.Visible = False
        End If


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Arrow
    End Sub
End Class





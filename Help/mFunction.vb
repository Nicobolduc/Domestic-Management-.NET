Option Strict Off
Option Explicit On

Imports System.Data.SqlClient

Module mFunction


	Private Const mstrModule_NAME As String = "mFunction"

    Private Const mintNote_cap As Integer = 900456
    Private Const mlngDeadLock_Error As Integer = -2147467259
    Private Const mlngInvalidConnection_Error As Integer = 3709
    Private mcStringCleaner As System.Text.RegularExpressions.Regex = New System.Text.RegularExpressions.Regex("'", System.Text.RegularExpressions.RegexOptions.Compiled Or System.Text.RegularExpressions.RegexOptions.CultureInvariant Or System.Text.RegularExpressions.RegexOptions.IgnoreCase)
	
	'********************************************************
	'* Fonction commune au OCX                              *
	'*                                                      *
	'********************************************************
	'*                                                      *
	'*  Par:            LAM                                 *
	'*  Date:           14-09-2000                          *
	'*  Modification:   **-**-****                          *
	'********************************************************

	'Cette fonction retourne le mot de passe emcryter
    Public Function fstrEncrypt(ByVal vstrPassWord As String) As String
        On Error GoTo Err_fstrEncrypt
        Const strFCT_NAME As String = "fstrEncrypt"
        Dim intRnd As Short
        Dim strINT_PW As String = gvbNullstring 'MOT DE PASSE EN INTEGER
        Dim intCount As Short
        Dim strRND As New VB6.FixedLengthString(2)

        'Nombre choisie au hasard
        intRnd = Int((99 * Rnd()) + 1)
        strRND.Value = Right("00" & intRnd, 2)

        'Boucle sur les lettre sur PW
        For intCount = 1 To Len(vstrPassWord)
            strINT_PW = strINT_PW & Asc(Mid(UCase(vstrPassWord), intCount, 1))
        Next intCount

        'multiplie par le facteur et le concatene "00"
        strINT_PW = (CDbl(strINT_PW) * intRnd) & strRND.Value


Exit_fstrEncrypt:
        fstrEncrypt = strINT_PW
        Exit Function

Err_fstrEncrypt:
        fstrEncrypt = gvbNullstring
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_fstrEncrypt
    End Function

    Public Function fstrDecrypt(ByVal vstrText As String) As String
        On Error GoTo Err_fstrDecrypt
        Const strFCT_NAME As String = "fstrDecrypt"
        Dim intfacteur As Short
        Dim strPassword As String = gvbNullstring
        Dim intCount As Short

        intfacteur = CShort(Right(vstrText, 2))

        '2 dernier caractere = facteur  multiplicateur
        vstrText = Mid(vstrText, 1, Len(vstrText) - 2)

        vstrText = CStr(CDbl(vstrText) / intfacteur)

        For intCount = 1 To Len(vstrText) Step 2
            strPassword = strPassword & Chr(CInt(Mid(vstrText, intCount, 2)))
        Next intCount


Exit_fstrDecrypt:
        fstrDecrypt = strPassword
        Exit Function

Err_fstrDecrypt:
        fstrDecrypt = gvbNullstring
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_fstrDecrypt
    End Function

    Public Function flngConvertTwipsToPixel(ByVal vlngTwips As Long) As Long
        On Error GoTo Err_flngConvertTwipsToPixel
        Const strFCT_NAME As String = "fstrDecrypt"

        Dim lngPixel As Long

        lngPixel = Math.Ceiling(vlngTwips / 20)


Exit_flngConvertTwipsToPixel:
        flngConvertTwipsToPixel = lngPixel
        Exit Function

Err_flngConvertTwipsToPixel:
        flngConvertTwipsToPixel = 0
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_flngConvertTwipsToPixel
    End Function
    Public Function fstrSQLFix(ByVal vstrString As String) As String
        On Error GoTo Err_fstrSQLFix
        Const strFCT_NAME As String = "fstrSQLFix"
        Dim blnReturn As Boolean
        Dim lngIndex As Integer
        Dim strReturn As String = gvbNullstring
        Dim strCar As String = gvbNullstring

        If vstrString Is Nothing Then
            vstrString = String.Empty
        End If        
        strReturn = "'" & mcStringCleaner.Replace(vstrString, "''") & "'"

Exit_fstrSQLFix:
        fstrSQLFix = strReturn
        Exit Function

Err_fstrSQLFix:
        strReturn = vstrString
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_fstrSQLFix
    End Function

    '***********************************************************
    '*  Procedure Name : fblnCheckRegionalSettings
    '*
    '*  Created        : 06-02-2001   LAM-KF
    '*  Modified       : yyyy/mm/dd    XX
    '*
    '*  Parameters     :
    '*  [IN]
    '*
    '*  Return value   : if Success TRUE otherwise FALSE
    '*
    '*  Description    : Get entry from ini
    '***********************************************************
    Public Function fblnCheckRegionalSettings(ByRef rblnFormatValid As Boolean) As Boolean
        On Error GoTo Err_fblnCheckRegionalSettings
        Const strFCT_NAME As String = "fblnCheckRegionalSettings"
        Dim blnReturn As Boolean
        Dim Symbol As String = gvbNullstring
        Dim iRet1 As Integer
        Dim iRet2 As Integer
        Dim lpLCDataVar As String = gvbNullstring
        Dim Pos As Short
        Dim Locale As Integer
        Dim strTemp As String = gvbNullstring

        blnReturn = True

        If gblnLogirackPublic Then
            rblnFormatValid = True

            Locale = GetUserDefaultLCID()
            iRet1 = GetLocaleInfo(Locale, LOCALE_SSHORTDATE, lpLCDataVar, 0)
            Symbol = New String(Chr(0), iRet1)

            iRet2 = GetLocaleInfo(Locale, LOCALE_SSHORTDATE, Symbol, iRet1)
            Pos = InStr(Symbol, Chr(0))

            If Pos > 0 Then
                Symbol = Left(Symbol, Pos - 1)

                Select Case True
                    'POUR WINDOWS ANGLAIS
                    Case UCase(Left(Symbol, 1)) = "Y"
                        If InStr(1, UCase(Symbol), "YYYY", CompareMethod.Text) > 0 Then
                            'do nothing
                        Else
                            Symbol = "yyyy" & Replace(Symbol, "y", "")
                        End If

                    Case UCase(Right(Symbol, 1)) = "Y"
                        If InStr(1, UCase(Symbol), "YYYY", CompareMethod.Text) > 0 Then
                            'do nothing
                        Else
                            Symbol = Replace(Symbol, "y", "") & "yyyy"
                        End If

                        'POUR WINDOWS FRANCAIS
                    Case UCase(Left(Symbol, 1)) = "A"
                        If InStr(1, UCase(Symbol), "AAAA", CompareMethod.Text) > 0 Then
                            'do nothing
                        Else
                            Symbol = "aaaa" & Replace(Symbol, "a", "")
                        End If

                    Case UCase(Right(Symbol, 1)) = "A"
                        If InStr(1, UCase(Symbol), "AAAA", CompareMethod.Text) > 0 Then
                            'do nothing
                        Else
                            Symbol = Replace(Symbol, "a", "") & "aaaa"
                        End If

                    Case Else
                        'do nothing : langue de windows non supporté
                End Select

                gstrPCShortDateFormat = Symbol
                '        If Symbol <> gstrWinDateFormat Then
                '
                '            ' Handle error
                '            rblnFormatValid = False
                '            Call gcTTApp.bln_ShowMsg(mintSHORT_DATE_INVALID, vbCritical, gstrWinDateFormat)
                '        Else
                '            'Date format is valid
                '            rblnFormatValid = True
                '        End If
            End If


        Else
            rblnFormatValid = True

            Locale = GetUserDefaultLCID()
            iRet1 = GetLocaleInfo(Locale, LOCALE_SSHORTDATE, lpLCDataVar, 0)
            Symbol = New String(Chr(0), iRet1)

            iRet2 = GetLocaleInfo(Locale, LOCALE_SSHORTDATE, Symbol, iRet1)
            Pos = InStr(Symbol, Chr(0))

            If Pos > 0 Then
                Symbol = Left(Symbol, Pos - 1)
                'FORMAT DU SHORT DATE DU PC
                gstrPCShortDateFormat = Symbol
            End If

        End If



Exit_fblnCheckRegionalSettings:
        fblnCheckRegionalSettings = blnReturn
        Exit Function

Err_fblnCheckRegionalSettings:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_fblnCheckRegionalSettings
    End Function
    Public Function fblnGetDefaultBackColor(ByVal vintColorMode As Integer, ByRef rcColor As Color) As Boolean
        On Error GoTo Error_fblnSetBackColor
        Const strFCT_NAME As String = "fblnSetBackColor"

        Dim blnReturn As Boolean

        Select Case vintColorMode
            Case clsConstante.COLOR_MODE.COLORMODE_WHITE
                rcColor = Color.White
            Case Else
                rcColor = System.Drawing.SystemColors.Control
        End Select


Exit_fblnSetBackColor:
        fblnGetDefaultBackColor = blnReturn
        Exit Function

Error_fblnSetBackColor:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_fblnSetBackColor
    End Function
    '****************************************************************
    '* Nom de la fonction   : fblnBindCaption
    '*
    '*               Cree   : 02-02-2000  LAM
    '*            Modifie   : **-**-****  ***
    '*
    '*                But   : Load Caption of a form
    '*
    '* Parametre(s):
    '*              IN :
    '*
    '*              OUT:    True, si succes. False, si erreur
    '*
    '****************************************************************
    Public Function fblnBindCaption(ByRef rfrmForm As Object, ByVal vlngUserLangue As Integer, Optional ByVal vlngUserGroupeNRI As Integer = 0, Optional ByVal vblnShowNRI As Boolean = False, Optional ByRef rToolTip As ToolTip = Nothing, Optional ByVal vintColorMode As Integer = clsConstante.COLOR_MODE.COLORMODE_DEFAULT) As Boolean
        On Error GoTo Error_fblnBindCaption
        Const strFCT_NAME As String = "fblnBindCaption"
        Dim blnReturn As Boolean
        Dim ctlControl As System.Windows.Forms.Control
        Dim objTemp As Object = Nothing
        Dim strListeBadCap As String = gvbNullstring
        Dim strTemp As String = gvbNullstring
        Dim strBackColor As String = gvbNullString
        Dim cColor As Object = Nothing

        blnReturn = True

        'Si on est en mode defaut, pas besoin de placer toutes les couleurs... les labels comme les tool tips deviennent gris aussi (exemple)
        If vintColorMode <> clsConstante.COLOR_MODE.COLORMODE_DEFAULT Then
            Call fblnGetDefaultBackColor(vintColorMode, cColor)
            rfrmForm.backcolor = cColor
        Else
            cColor = Nothing
        End If

        'Tous les controls de la forme
        For Each ctlControl In rfrmForm.controls
            strTemp = gvbNullstring

            objTemp = ctlControl
            blnReturn = fblnControl_BindCaption(objTemp, vlngUserLangue, vlngUserGroupeNRI, vblnShowNRI, strTemp, rToolTip, cColor)

            If strTemp <> gvbNullstring Then strListeBadCap = strListeBadCap & strTemp & vbCrLf

            'UPGRADE_NOTE: Object objTemp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            objTemp = Nothing
        Next ctlControl

        'UPGRADE_WARNING: Couldn't resolve default property of object rfrmForm.Tag. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        If CStr(rfrmForm.Tag) <> gvbNullstring Then
            'UPGRADE_WARNING: Couldn't resolve default property of object rfrmForm.Caption. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            'UPGRADE_WARNING: Couldn't resolve default property of object rfrmForm.Tag. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            rfrmForm.Text = fstrGetCaption(rfrmForm.Tag, vlngUserLangue, vblnShowNRI)
            'UPGRADE_WARNING: Couldn't resolve default property of object rfrmForm.Caption. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            'UPGRADE_WARNING: Couldn't resolve default property of object rfrmForm.Tag. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            If rfrmForm.Text = gstrBAD_CAPTION Then strListeBadCap = strListeBadCap & "Me;" & rfrmForm.Tag
        End If

        If strListeBadCap <> gvbNullstring Then
            Call MsgBox("CAPTIONS NON TROUVES" & vbCrLf & vbCrLf & strListeBadCap, MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, My.Application.Info.AssemblyName)
        Else
            'do nothing
        End If

Exit_fblnBindCaption:
        cColor = Nothing
        ctlControl = Nothing
        objTemp = Nothing
        fblnBindCaption = blnReturn
        Exit Function

Error_fblnBindCaption:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_fblnBindCaption
    End Function
    '*************************************************************
    '*
    '* Name:        fblnLook_SetButtonStyle
    '*
    '* Par:        	Michael,  XXXX-XX-XX
    '*
    '* Description: 
    '*
    '* IN:
    '* OUT: False si erreur, True si OK
    '*
    '*************************************************************
    Public Function fblnLook_SetStyle(ByRef robjControl As Object) As Boolean
        On Error GoTo Error_fblnLook_SetStyle
        Const strFCT_NAME As String = "fblnLook_SetStyle"

        Dim blnReturn As Boolean

        blnReturn = True

        If gblnNewAppearance Then
            Select Case TypeName(robjControl)
                Case "Button"
                    'Dim o As button
                    robjControl.FlatAppearance.BorderColor = Drawing.SystemColors.ActiveBorder
                    robjControl.FlatStyle = FlatStyle.Flat
                    robjControl.FlatAppearance.MouseOverBackColor = Drawing.SystemColors.GradientInactiveCaption
                    robjControl.FlatAppearance.MouseDownBackColor = Drawing.SystemColors.GradientActiveCaption

                Case "ctlTTGrid"
                    'Dim o As ctlTTGrid
                    robjControl.VisualStyle = VisualStyle.System



                Case Else
                    'do nothing
            End Select
        End If

Exit_fblnLook_SetStyle:
        fblnLook_SetStyle = blnReturn
        Exit Function

Error_fblnLook_SetStyle:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_fblnLook_SetStyle
    End Function

    '****************************************************************
    '* Nom de la fonction   : fblnControl_BindCaption
    '*
    '*               Cree   : 10-01-2002   lamorin
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
    Function fblnControl_BindCaption(ByRef robjControl As Object, ByVal vlngUserLangue As Integer, Optional ByVal vlngUserGroupeNRI As Integer = 0, Optional ByVal vblnShowNRI As Boolean = False, Optional ByRef rstrBadCaptions As String = "", Optional ByRef rToolTip As ToolTip = Nothing, Optional ByRef rcBackColor As Object = Nothing) As Boolean
        On Error GoTo Error_fblnControl_BindCaption
        Const strFCT_NAME As String = "fblnControl_BindCaption"
        Dim blnReturn As Boolean
        Dim intCpt As Integer
        Dim intCpt2 As Integer
        Dim objChildControl As Object
        Dim strCapID As String = gvbNullstring
        Dim strCaption As String = gvbNullstring

        blnReturn = True

        rstrBadCaptions = gvbNullstring
        'UPGRADE_WARNING: TypeName has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        Select Case TypeName(robjControl)

            'Set Caption & ToolTipText

            Case "ctlTTCombo", "ctlTTTextCombo_3PL", "ctlTTFilter_3PL"
                blnReturn = pfblnTTCombo_SetCaption(robjControl, vlngUserLangue, vblnShowNRI, rstrBadCaptions)
                'BLOC 1

            Case "Frame", "CheckBox", "OptionButton", "RadioButton", "GroupBox", "Panel", "TableLayoutPanel"
                If Not IsNothing(rcBackColor) Then
                    robjControl.backColor = rcBackColor
                End If

                blnReturn = pfblnControl_SetCaption(robjControl, vlngUserLangue, vblnShowNRI, rstrBadCaptions, rToolTip)

                If DirectCast(robjControl, Control).HasChildren Then
                    For Each objChildControl In DirectCast(robjControl, Control).Controls
                        fblnControl_BindCaption(objChildControl, vlngUserLangue, vlngUserGroupeNRI, vblnShowNRI, rstrBadCaptions, rToolTip, rcBackColor)
                    Next
                End If

            Case "Button"
                'UPGRADE_WARNING: Couldn't resolve default property of object robjControl. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                blnReturn = pfblnControl_SetCaption(robjControl, vlngUserLangue, vblnShowNRI, rstrBadCaptions, rToolTip)

                If DirectCast(robjControl, Control).HasChildren Then
                    For Each objChildControl In DirectCast(robjControl, Control).Controls
                        fblnControl_BindCaption(objChildControl, vlngUserLangue, vlngUserGroupeNRI, vblnShowNRI, rstrBadCaptions, rToolTip)
                    Next
                End If

                Call fblnLook_SetStyle(robjControl)


            Case "Label"
                If Not IsNothing(rcBackColor) Then
                    robjControl.backColor = rcBackColor
                End If

                blnReturn = pfblnControl_SetCaption(robjControl, vlngUserLangue, vblnShowNRI, rstrBadCaptions, rToolTip)

                'Set FormatString
            Case "MSHFlexGrid", "MsFlexGrid", "VSFlexGrid", "AxVSFlexGrid", "ctlTTGrid"
                blnReturn = pfblnGrid_SetCaption(robjControl, vlngUserLangue, vblnShowNRI, rstrBadCaptions)

                Call fblnLook_SetStyle(robjControl)

            Case "TextBox"
                'UPGRADE_WARNING: Couldn't resolve default property of object robjControl. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                blnReturn = pfblnTextBox_SetCaption(robjControl, vlngUserLangue, vblnShowNRI, rstrBadCaptions)

            Case "Menu" 'MENU
                blnReturn = pfblnMenu_SetCaption(robjControl, vlngUserLangue, vblnShowNRI, rstrBadCaptions)

            Case "MenuStrip"
                For intCpt = 0 To robjControl.Items.Count - 1
                    blnReturn = pfblnMenu_SetCaption(robjControl.items(intCpt), vlngUserLangue, vblnShowNRI, rstrBadCaptions)

                    For intCpt2 = 0 To robjControl.items(intCpt).dropdownitems.count - 1
                        Call fblnControl_BindCaption(robjControl.items(intCpt).dropdownitems(intCpt2), vlngUserLangue, vlngUserGroupeNRI, vblnShowNRI, rstrBadCaptions, rToolTip)
                    Next intCpt2
                Next intCpt

            Case "ToolStripMenuItem"
                blnReturn = pfblnMenu_SetCaption(robjControl, vlngUserLangue, vblnShowNRI, rstrBadCaptions)

                For intCpt = 0 To robjControl.dropdownitems.count - 1
                    Call fblnControl_BindCaption(robjControl.dropdownitems(intCpt), vlngUserLangue, vlngUserGroupeNRI, vblnShowNRI, rstrBadCaptions, rToolTip)
                Next

            Case "TabControl", "SSTab", "ctlTTSSTab"
                If Not IsNothing(rcBackColor) Then
                    robjControl.backColor = rcBackColor
                End If

                blnReturn = pfblnSSTab_SetCaption(robjControl, vlngUserLangue, vblnShowNRI, rstrBadCaptions)

                If DirectCast(robjControl, Control).HasChildren Then
                    For Each objChildControl In DirectCast(robjControl, Control).Controls
                        fblnControl_BindCaption(objChildControl, vlngUserLangue, vlngUserGroupeNRI, vblnShowNRI, rstrBadCaptions, rToolTip, rcBackColor)
                    Next
                End If

            Case "TabPage"
                If Not IsNothing(rcBackColor) Then
                    robjControl.backColor = rcBackColor
                End If

                If DirectCast(robjControl, Control).HasChildren Then
                    For Each objChildControl In DirectCast(robjControl, Control).Controls
                        fblnControl_BindCaption(objChildControl, vlngUserLangue, vlngUserGroupeNRI, vblnShowNRI, rstrBadCaptions, rToolTip, rcBackColor)
                    Next
                End If

            Case "ctlMultiLangue"
                'do nothing

            Case "Toolbar"
                'UPGRADE_WARNING: Couldn't resolve default property of object pfblnToolBar_SetCaption(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                blnReturn = pfblnToolBar_SetCaption(robjControl, vlngUserLangue, vlngUserGroupeNRI, vblnShowNRI, rstrBadCaptions)

            Case "ToolStrip"
                If gblnLogirackPublic Then
                    For intCpt = 0 To robjControl.Items.Count - 1

                        If robjControl.Items(intCpt).Tag <> gvbNullstring Then
                            Select Case UCase(Left(robjControl.Items(intCpt).Tag, 1))
                                Case "M"
                                    blnReturn = fblnSingleLookUp("TTMenu", "TTAC_NRI", strCapID, gcnADOConnect, "TTM_NRI = " & Mid(robjControl.Items(intCpt).Tag, 2))
                                Case "L"
                                    blnReturn = fblnSingleLookUp("TTListeGen", "TTAC_NRI_Form", strCapID, gcnADOConnect, "TTLG_NRI = " & Mid(robjControl.Items(intCpt).Tag, 2))
                                Case Else
                                    strCapID = robjControl.Items(intCpt).Tag

                            End Select

                            If blnReturn And strCapID <> gvbNullstring Then
                                'strCaption = LoadResString(strCapID & vintLangue)
                                strCaption = fstrGetCaption(CInt(strCapID), vlngUserLangue, vblnShowNRI)
                                strCaption = Replace(strCaption, "&", "", 1, , vbTextCompare)
                                robjControl.Items(intCpt).ToolTipText = strCaption
                                'robjControl.Items(intCpt).Description = strCaption
                            Else
                                blnReturn = False
                                Exit For
                            End If
                        End If
                    Next intCpt
                Else
                    For intCpt = 0 To robjControl.Items.Count - 1
                        blnReturn = pfblnMenu_SetCaption(robjControl.items(intCpt), vlngUserLangue, vblnShowNRI, rstrBadCaptions, True)

                        Select Case TypeName(robjControl.items(intCpt))
                            Case "ToolStripButton"
                                'do nothing
                            Case "ToolStripDropDownButton"
                                For intCpt2 = 0 To robjControl.items(intCpt).dropdownitems.count - 1
                                    Call fblnControl_BindCaption(robjControl.items(intCpt).dropdownitems(intCpt2), vlngUserLangue, vlngUserGroupeNRI, vblnShowNRI, rstrBadCaptions, rToolTip)
                                Next intCpt2
                        End Select

                    Next intCpt
                End If

            Case "ctlAppConnect", "ImageList", "StatusBar", "ComboBox", "ctlListeCombo", "ctlData", "DTPicker", "ctlComboOri"
                'Do nothing

            Case "ctlTTListeGenTool"

            Case "ctlTTComment"
                'UPGRADE_WARNING: Couldn't resolve default property of object pfblnTTComment_SetCaption(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                blnReturn = pfblnTTComment_SetCaption(robjControl, vlngUserLangue, vblnShowNRI, rstrBadCaptions)

            Case "ctlTTFormBase", "ctlTTFormDetail"
                'UPGRADE_WARNING: Couldn't resolve default property of object robjControl.bln_SetControlCaption. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                blnReturn = robjControl.bln_SetControlCaption

            Case "ctlTTRefresh"
                Call fblnLook_SetStyle(robjControl)


            Case Else
                'Do Nothing
                Debug.Print(TypeName(robjControl))
        End Select

Exit_fblnControl_BindCaption:
        fblnControl_BindCaption = blnReturn
        Exit Function

Error_fblnControl_BindCaption:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_fblnControl_BindCaption
    End Function

    Private Function pfblnControl_SetCaption(ByRef rctlTemp As System.Windows.Forms.Control, ByVal vlngUserLangue As Integer, Optional ByVal vblnShowNRI As Boolean = False, Optional ByRef rstrBadCaptions As String = "", Optional ByRef rToolTip As ToolTip = Nothing) As Boolean
        On Error GoTo Error_pfblnControl_SetCaption
        Const strFCT_NAME As String = "pfblnControl_SetCaption"
        Dim blnReturn As Boolean
        Dim strCaption As String = gvbNullstring
        Dim strSetWhat As String '(T)oolTipText / (C)aption /() Both
        Dim lngTTAC_NRI As Integer
        Dim lngLangue As Integer
        'Dim tooToolTip As New ToolTip

        blnReturn = True
        lngLangue = vlngUserLangue

        'gToolTip = New ToolTip

        If CStr(rctlTemp.Tag) <> gvbNullstring Then
            If IsNumeric(rctlTemp.Tag) Then
                lngTTAC_NRI = CInt(rctlTemp.Tag)
                strSetWhat = gvbNullstring
            Else
                strSetWhat = Mid(rctlTemp.Tag, 1, 1)
                lngTTAC_NRI = CInt(Mid(rctlTemp.Tag, 2))
            End If

            strCaption = fstrGetCaption(lngTTAC_NRI, lngLangue, vblnShowNRI)
            If strCaption = gvbNullstring Or strCaption = gstrBAD_CAPTION Then
                strCaption = gstrBAD_CAPTION
                'UPGRADE_WARNING: TypeName has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
                rstrBadCaptions = TypeName(rctlTemp) & " " & rctlTemp.Name & ":" & rctlTemp.Tag
            Else
                'do nothing
            End If

            If TypeName(rctlTemp) = "Label" Then
                rctlTemp.AutoSize = True
            End If

            Select Case UCase(strSetWhat)
                Case gstrCancelAutoFormat
                    If TypeName(rctlTemp) = "Label" Then
                        rctlTemp.AutoSize = False
                    End If

                    rctlTemp.Text = strCaption

                Case gstrSetCaptionOnly
                    rctlTemp.Text = strCaption

                    If Not IsNothing(rToolTip) Then
                        rToolTip.SetToolTip(rctlTemp, gvbNullstring)
                    End If

                Case gstrSetToolTipTextOnly
                    If Not IsNothing(rToolTip) Then
                        rToolTip.SetToolTip(rctlTemp, strCaption)
                    End If
                    rctlTemp.Text = gvbNullstring

                Case gstrSetHasListeGen
                    MsgBox("Se control ne peut etre liee avec une liste generick :" & rctlTemp.Name, MsgBoxStyle.Critical, "Programmeur")

                Case Else 'Set both

                    rctlTemp.Text = strCaption
            End Select
        Else
            'MP: Si n'a pas de tag, ne fait rien (si un label a pas de tag, on le laisse tel quel)

        End If


        If TypeName(rctlTemp) = "Label" Then
            If Not IsNothing(rToolTip) Then
                rToolTip.SetToolTip(rctlTemp, "")
            End If
        Else
            'Do nothing
        End If


Exit_pfblnControl_SetCaption:
        'MP : en faisant ca, on a plus jamais de tooltip.
        'On va essayer de garder le tooltip global au module, je ne sais pas si ca va recreer des problemes de leak de memoires...
        'tooToolTip.Dispose()
        'tooToolTip = Nothing
        pfblnControl_SetCaption = blnReturn
        Exit Function

Error_pfblnControl_SetCaption:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_pfblnControl_SetCaption
    End Function

    Private Function pfblnTextBox_SetCaption(ByRef rctlTemp As System.Windows.Forms.Control, ByVal vlngUserLangue As Integer, Optional ByVal vblnShowNRI As Boolean = False, Optional ByRef rstrBadCaptions As String = "") As Boolean
        On Error GoTo Error_pfblnTextBox_SetCaption
        Const strFCT_NAME As String = "pfblnTextBox_SetCaption"
        Dim blnReturn As Boolean
        Dim strCaption As String = gvbnullstring
        Dim strSetWhat As String '(T)oolTipText / (C)aption /() Both
        Dim lngTTAC_NRI As Integer
        Dim lngLangue As Integer
        Dim tooToolTip As New ToolTip

        blnReturn = True
        lngLangue = vlngUserLangue

        If CStr(rctlTemp.Tag) <> gvbNullstring Then
            If IsNumeric(rctlTemp.Tag) Then
                lngTTAC_NRI = CInt(rctlTemp.Tag)
                strSetWhat = gvbNullstring
            Else
                strSetWhat = Mid(rctlTemp.Tag, 1, 1)
                lngTTAC_NRI = CInt(Mid(rctlTemp.Tag, 2))
            End If

            strCaption = fstrGetCaption(lngTTAC_NRI, lngLangue, vblnShowNRI)

            If strCaption = gvbNullstring Or strCaption = gstrBAD_CAPTION Then
                strCaption = gstrBAD_CAPTION
                rstrBadCaptions = "TxtBox " & rctlTemp.Name & ":" & rctlTemp.Tag
            Else
                'do nothing
            End If

            Select Case strSetWhat
                Case gstrSetCaptionOnly
                    rctlTemp.Text = strCaption

                Case gstrSetToolTipTextOnly

                    tooToolTip.SetToolTip(rctlTemp, strCaption)

                Case gstrSetHasListeGen
                    MsgBox("Se control ne peut etre liee avec une liste generick :" & rctlTemp.Name, MsgBoxStyle.Critical, "Programmeur")

                Case Else 'Set both

                    tooToolTip.SetToolTip(rctlTemp, strCaption)
                    rctlTemp.Text = strCaption
            End Select
        Else

            tooToolTip.SetToolTip(rctlTemp, "")
            rctlTemp.Text = ""
        End If


Exit_pfblnTextBox_SetCaption:
        tooToolTip.Dispose()
        tooToolTip = Nothing
        pfblnTextBox_SetCaption = blnReturn
        Exit Function

Error_pfblnTextBox_SetCaption:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_pfblnTextBox_SetCaption
    End Function

    Private Function pfblnGrid_SetCaption(ByRef rctlTemp As Object, ByVal vlngUserLangue As Integer, Optional ByVal vblnShowNRI As Boolean = False, Optional ByRef rstrBadCaptions As String = "") As Boolean
        On Error GoTo Error_pfblnGrid_SetCaption
        Const strFCT_NAME As String = "pfblnGrid_SetCaption"
        Dim blnReturn As Boolean
        Dim strCaption As String = gvbNullstring
        Dim strSetWath As String = gvbNullstring '(T)oolTipText / (C)aption /() Both
        Dim lngTTAC_NRI As Integer
        Dim lngLangue As Integer

        blnReturn = True
        lngLangue = vlngUserLangue

        'UPGRADE_WARNING: Couldn't resolve default property of object rctlTemp.Tag. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        If CStr(rctlTemp.Tag) <> gvbNullstring Then
            'UPGRADE_WARNING: Couldn't resolve default property of object rctlTemp.Tag. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            If IsNumeric(rctlTemp.Tag) Then
                'Get caption
                'UPGRADE_WARNING: Couldn't resolve default property of object rctlTemp.Tag. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                lngTTAC_NRI = rctlTemp.Tag
                strCaption = fstrGetCaption(lngTTAC_NRI, lngLangue, False)
            Else
                'Do nothing
            End If

            If strCaption = gvbNullstring Or strCaption = gstrBAD_CAPTION Then
                strCaption = gstrBAD_CAPTION
                'UPGRADE_WARNING: Couldn't resolve default property of object rctlTemp.Tag. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                'UPGRADE_WARNING: Couldn't resolve default property of object rctlTemp.Name. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                rstrBadCaptions = "Grid " & rctlTemp.Name & ":" & rctlTemp.Tag
            Else
                'do nothing
            End If

            'UPGRADE_WARNING: Couldn't resolve default property of object rctlTemp.FormatString. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            rctlTemp.FormatString = strCaption

            If vblnShowNRI Then

                rctlTemp.ToolTipText = "(" & lngTTAC_NRI & ")"
            Else
                'Do nothing
            End If
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object rctlTemp.FormatString. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            rctlTemp.FormatString = gvbNullstring
        End If


Exit_pfblnGrid_SetCaption:
        pfblnGrid_SetCaption = blnReturn
        Exit Function

Error_pfblnGrid_SetCaption:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_pfblnGrid_SetCaption
    End Function

    Private Function pfblnMenu_SetCaption(ByRef rmnuTemp As Object, ByVal vlngUserLangue As Integer, Optional ByVal vblnShowNRI As Boolean = False, Optional ByRef rstrBadCaptions As String = "", Optional ByVal vblnAsToolTip As Boolean = False) As Boolean
        On Error GoTo Error_pfblnMenu_SetCaption
        Const strFCT_NAME As String = "pfblnMenu_SetCaption"
        Dim blnReturn As Boolean
        Dim strCaption As String = gvbNullstring
        Dim strSQL As String = gvbNullstring

        blnReturn = True

        'UPGRADE_WARNING: Couldn't resolve default property of object rmnuTemp.Tag. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        If CStr(rmnuTemp.Tag) <> gvbNullstring Then
            If gRecMenuListe Is Nothing Then
                strSQL = "Select * from TTMenu"
                blnReturn = fblnRecord_SELECT(strSQL, gRecMenuListe, gcnADOConnect)
            Else
                ' Do nothing
            End If

            If blnReturn Then
                Call gRecMenuListe.MoveFirst()
                'UPGRADE_WARNING: Couldn't resolve default property of object rmnuTemp.Name. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                Call gRecMenuListe.Find("TTM_Constante = '" & rmnuTemp.Name & "'", , ADODB.SearchDirectionEnum.adSearchForward, 0)
                If Not gRecMenuListe.EOF Then
                    strCaption = fstrGetCaption(gRecMenuListe.Fields("TTAC_NRI").Value, vlngUserLangue, vblnShowNRI)
                Else
                    ' Do nothing
                End If
            Else
                ' Do nothing
            End If

            If strCaption <> gvbNullstring Then
                'UPGRADE_WARNING: Couldn't resolve default property of object rmnuTemp.Caption. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                If vblnAsToolTip Then
                    rmnuTemp.ToolTipText = strCaption
                Else
                    rmnuTemp.text = strCaption
                End If

            Else
                'UPGRADE_WARNING: Couldn't resolve default property of object rmnuTemp.Tag. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                'UPGRADE_WARNING: Couldn't resolve default property of object rmnuTemp.Name. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                rstrBadCaptions = "Menu " & rmnuTemp.Name & ":" & rmnuTemp.Tag
            End If
        Else
            'Do Nothing
        End If

Exit_pfblnMenu_SetCaption:
        pfblnMenu_SetCaption = blnReturn
        Exit Function

Error_pfblnMenu_SetCaption:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_pfblnMenu_SetCaption
    End Function

    Public Function fblnMenu_Secure(ByRef objForm As System.Windows.Forms.Form, ByVal vlngGroupeNRI As Integer, ByVal vlngTTAP_NRi As Integer, ByVal mcnAdoConnection As ADODB.Connection) As Boolean
        On Error GoTo Error_fblnMenu_Secure
        Const strFCT_NAME As String = "fblnMenu_Secure"
        Dim blnReturn As Boolean
        Dim objMenu As Object
        Dim intCpt As Integer
        Dim intCpt2 As Integer

        blnReturn = pfblnRecSecure_Set(vlngGroupeNRI, vlngTTAP_NRi, mcnAdoConnection)

        If blnReturn Then
            For Each objMenu In objForm.Controls
                'UPGRADE_WARNING: TypeName has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
                Select Case TypeName(objMenu)
                    Case "Menu"
                        gRecMenuSecur.MoveFirst()
                        'UPGRADE_WARNING: Couldn't resolve default property of object objMenu.Name. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        Call gRecMenuSecur.Find("TTM_Constante = '" & objMenu.Name & "' " & vbCrLf, , ADODB.SearchDirectionEnum.adSearchForward, 0)

                        If Not gRecMenuSecur.EOF Then
                            If gRecMenuSecur.Fields("TTMR_Right").Value = 1 Then
                                'objMenu.Visible = True
                                'UPGRADE_WARNING: Couldn't resolve default property of object objMenu.Enabled. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                                objMenu.Enabled = True
                            Else
                                'objMenu.Visible = False
                                'UPGRADE_WARNING: Couldn't resolve default property of object objMenu.Enabled. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                                objMenu.Enabled = False
                            End If
                        Else
                            'Menu pas existant dans la securite
                            'UPGRADE_WARNING: Couldn't resolve default property of object objMenu.Caption. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                            If objMenu.Caption <> "-" Then
                                'UPGRADE_WARNING: Couldn't resolve default property of object objMenu.Enabled. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                                objMenu.Enabled = False
                            Else
                                'do nothing
                            End If
                            'objMenu.Caption = "S_" & objMenu.Caption
                        End If

                    Case "MenuStrip"
                        For intCpt = 0 To objMenu.Items.Count - 1
                            Call pfblnMenu_SecureProcess(objMenu.items(intCpt))
                        Next intCpt

                    Case "Toolbar"
                        Call pfblnToolbar_Secure(objMenu)

                    Case "ToolStrip"
                        'Securite pas geree de la meme maniere dans Public
                        If gblnLogirackPublic Then
                            'OK
                        Else
                            For intCpt = 0 To objMenu.Items.Count - 1
                                Call pfblnMenu_SecureProcess(objMenu.items(intCpt))
                            Next intCpt
                        End If

                    Case "TableLayoutPanel"
                        For Each objMenu2 As Object In CType(objMenu, TableLayoutPanel).Controls
                            Select Case TypeName(objMenu2)
                                Case "Menu"
                                    gRecMenuSecur.MoveFirst()
                                    'UPGRADE_WARNING: Couldn't resolve default property of object objMenu.Name. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                                    Call gRecMenuSecur.Find("TTM_Constante = '" & objMenu2.Name & "' " & vbCrLf, , ADODB.SearchDirectionEnum.adSearchForward, 0)

                                    If Not gRecMenuSecur.EOF Then
                                        If gRecMenuSecur.Fields("TTMR_Right").Value = 1 Then
                                            'objMenu2.Visible = True
                                            'UPGRADE_WARNING: Couldn't resolve default property of object objMenu2.Enabled. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                                            objMenu2.Enabled = True
                                        Else
                                            'objMenu2.Visible = False
                                            'UPGRADE_WARNING: Couldn't resolve default property of object objMenu2.Enabled. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                                            objMenu2.Enabled = False
                                        End If
                                    Else
                                        'Menu pas existant dans la securite
                                        'UPGRADE_WARNING: Couldn't resolve default property of object objMenu2.Caption. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                                        If objMenu2.Caption <> "-" Then
                                            'UPGRADE_WARNING: Couldn't resolve default property of object objMenu2.Enabled. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                                            objMenu2.Enabled = False
                                        Else
                                            'do nothing
                                        End If
                                        'objMenu2.Caption = "S_" & objMenu.Caption
                                    End If

                                Case "MenuStrip"
                                    For intCpt = 0 To objMenu2.Items.Count - 1
                                        Call pfblnMenu_SecureProcess(objMenu2.items(intCpt))
                                    Next intCpt

                                Case "ToolStrip"
                                    'Securite pas geree de la meme maniere dans Public
                                    If gblnLogirackPublic Then
                                        'OK
                                    Else
                                        For intCpt = 0 To objMenu2.Items.Count - 1
                                            Call pfblnMenu_SecureProcess(objMenu2.items(intCpt))
                                        Next intCpt
                                    End If
                            End Select
                        Next objMenu2


                    Case Else
                        'Do nothing
                End Select
            Next objMenu
        Else
            ' Do nothing
        End If


Exit_fblnMenu_Secure:
        'UPGRADE_NOTE: Object objMenu may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objMenu = Nothing
        fblnMenu_Secure = blnReturn
        Exit Function

Error_fblnMenu_Secure:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_fblnMenu_Secure
    End Function
    '****************************************************************
    '* Nom de la fonction   : pfblnMenu_SecureProcess
    '*
    '*               Cree   : 14-10-2009   mpelletier
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
    Private Function pfblnMenu_SecureProcess(ByRef vobjMenu As Object) As Boolean
        On Error GoTo Error_pfblnMenu_SecureProcess
        Const strFCT_NAME As String = "pfblnMenu_SecureProcess"
        Dim blnReturn As Boolean
        Dim intCpt As Integer
        Dim intCpt2 As Integer

        blnReturn = True

        gRecMenuSecur.MoveFirst()

        Call gRecMenuSecur.Find("TTM_Constante = '" & vobjMenu.name & "' " & vbCrLf, , ADODB.SearchDirectionEnum.adSearchForward, 0)

        If Not gRecMenuSecur.EOF Then
            Select Case TypeName(vobjMenu)
                Case "ToolStripDropDownButton", "ToolStripButton"
                    vobjMenu.visible = IIf(gRecMenuSecur.Fields("TTMR_Right").Value = 1, True, False)
                Case Else
                    vobjMenu.Enabled = IIf(gRecMenuSecur.Fields("TTMR_Right").Value = 1, True, False)
            End Select
        Else
            'Menu pas existant dans la securite
            If vobjMenu.text <> "-" Then
                Select Case TypeName(vobjMenu)
                    Case "ToolStripDropDownButton", "ToolStripButton"
                        vobjMenu.visible = False
                    Case Else
                        vobjMenu.Enabled = False
                End Select
            Else
                'do nothing
            End If
        End If

        Select Case TypeName(vobjMenu)
            Case "ToolStripButton", "ToolStripSeparator"
                'do nothing
            Case Else
                For intCpt2 = 0 To vobjMenu.dropdownitems.count - 1
                    If blnReturn Then
                        blnReturn = pfblnMenu_SecureProcess(vobjMenu.dropdownitems(intCpt2))
                    Else
                        Exit For
                    End If
                Next intCpt2
        End Select

Exit_pfblnMenu_SecureProcess:
        pfblnMenu_SecureProcess = blnReturn
        Exit Function

Error_pfblnMenu_SecureProcess:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_pfblnMenu_SecureProcess
    End Function
    Private Function pfblnSSTab_SetCaption(ByRef sstabObj As Object, ByVal vlngUserLangue As Integer, Optional ByVal vblnShowNRI As Boolean = False, Optional ByRef rstrBadCaptions As String = "") As Object
        On Error GoTo Error_pfblnSSTab_SetCaption
        Const strFCT_NAME As String = "pfblnSSTab_SetCaption "
        Dim intCount As Short
        Dim strString As String = gvbnullstring
        Dim blnReturn As Boolean
        Dim intPositionStart As Short
        Dim intPositionEnd As Short

        blnReturn = True

        'UPGRADE_WARNING: Couldn't resolve default property of object sstabObj.Tag. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        If CStr(sstabObj.Tag) <> gvbNullstring Then
            'UPGRADE_WARNING: Couldn't resolve default property of object sstabObj.Tag. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            strString = fstrGetCaption(sstabObj.Tag, vlngUserLangue, vblnShowNRI)

            If strString = gvbNullstring Or strString = gstrBAD_CAPTION Then
                'UPGRADE_WARNING: Couldn't resolve default property of object sstabObj.Tag. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                'UPGRADE_WARNING: Couldn't resolve default property of object sstabObj.Name. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                rstrBadCaptions = "SSTab " & sstabObj.Name & ":" & sstabObj.Tag
            Else
                intPositionStart = 1

                'Les tabs controles ne supportent plus les mnemonics, on enleve donc les &
                strString = Replace(strString, "&", "")

                'UPGRADE_WARNING: Couldn't resolve default property of object sstabObj.Tabs. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                For intCount = 0 To sstabObj.tabcount - 1
                    intPositionEnd = InStr(intPositionStart, strString, "|", CompareMethod.Text)
                    If intPositionEnd = 0 Then intPositionEnd = Len(strString) + 1
                    'UPGRADE_WARNING: Couldn't resolve default property of object sstabObj.TabCaption. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    sstabObj.tabpages(intCount).text = Mid(strString, intPositionStart, intPositionEnd - intPositionStart)
                    intPositionStart = intPositionEnd + 1
                Next intCount
            End If
        Else
            'Do Nothing
        End If

Exit_pfblnSSTab_SetCaption:
        'UPGRADE_WARNING: Couldn't resolve default property of object pfblnSSTab_SetCaption. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        pfblnSSTab_SetCaption = blnReturn
        Exit Function

Error_pfblnSSTab_SetCaption:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_pfblnSSTab_SetCaption
    End Function

    Private Function pfblnTTComment_SetCaption(ByRef rctlControl As Object, ByVal vlngUserLangue As Integer, Optional ByVal vblnShowNRI As Boolean = False, Optional ByRef rstrBadCaptions As String = "") As Object
        On Error GoTo Error_pfblnTTComment_SetCaption
        Const strFCT_NAME As String = "pfblnTTComment_SetCaption"
        Dim blnReturn As Boolean
        Dim strTag As String = gvbnullstring
        Dim strCaption As String = gvbnullstring

        blnReturn = True

        'UPGRADE_WARNING: Couldn't resolve default property of object rctlControl.Tag. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        If (rctlControl.Tag) <> gvbNullstring Then
            'UPGRADE_WARNING: Couldn't resolve default property of object rctlControl.Tag. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            strTag = rctlControl.Tag

            If vblnShowNRI Then
                'UPGRADE_WARNING: Couldn't resolve default property of object rctlControl.Tag. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                strCaption = "(" & rctlControl.Tag & ")" & fstrGetCaption(CInt(strTag), vlngUserLangue, False)
            Else
                strCaption = fstrGetCaption(CInt(strTag), vlngUserLangue, False)
            End If

            If strCaption = gvbNullstring Or strCaption = gstrBAD_CAPTION Then
                'UPGRADE_WARNING: Couldn't resolve default property of object rctlControl.Name. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                rstrBadCaptions = rstrBadCaptions & "TTComment " & rctlControl.Name & ":" & strTag
            End If
        Else
            strCaption = fstrGetCaption(mintNote_cap, vlngUserLangue, False)
        End If

        'UPGRADE_WARNING: Couldn't resolve default property of object rctlControl.ToolTip. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        rctlControl.ToolTip = strCaption

Exit_pfblnTTComment_SetCaption:
        'UPGRADE_WARNING: Couldn't resolve default property of object pfblnTTComment_SetCaption. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        pfblnTTComment_SetCaption = blnReturn
        Exit Function

Error_pfblnTTComment_SetCaption:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_pfblnTTComment_SetCaption
    End Function

    Private Function pfblnToolBar_SetCaption(ByRef ToolBarObj As Object, ByVal vlngUserLangue As Integer, ByVal vlngUserGroupeNRI As Integer, Optional ByVal vblnShowNRI As Boolean = False, Optional ByRef rstrBadCaptions As String = "") As Object
        On Error GoTo Error_pfblnToolBar_SetCaption
        Const strFCT_NAME As String = "pfblnToolBar_SetCaption "
        Dim blnReturn As Boolean
        Dim intBouton As Short
        Dim intMenu As Short
        Dim lngTTLG_NRI As Integer
        Dim strWhat As String = gvbnullstring
        Dim strTag As String = gvbnullstring
        Dim strCaption As String = gvbnullstring

        blnReturn = True

        'Liste des bouton menus
        'UPGRADE_WARNING: Couldn't resolve default property of object ToolBarObj.Buttons. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        For intBouton = 1 To ToolBarObj.Buttons.Count
            'UPGRADE_WARNING: Couldn't resolve default property of object ToolBarObj.Buttons. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            strTag = ToolBarObj.Buttons(intBouton).Tag

            'Get what to set with caption
            Select Case True
                Case IsNumeric(strTag)
                    lngTTLG_NRI = CInt(strTag)
                    strWhat = gvbNullString
                Case UCase(Mid(strTag, 1, 1)) = "T"
                    lngTTLG_NRI = CInt(Mid(strTag, 2))
                    strWhat = "T"
                Case UCase(Mid(strTag, 1, 1)) = "C"
                    lngTTLG_NRI = CInt(Mid(strTag, 2))
                    strWhat = "C"
                Case Else
                    strWhat = gvbNullString
                    lngTTLG_NRI = 0
            End Select

            If lngTTLG_NRI <> 0 Then
                strCaption = fstrGetCaption(CInt(lngTTLG_NRI), vlngUserLangue, vblnShowNRI)
                If strCaption = gvbNullString Or strCaption = gstrBAD_CAPTION Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object ToolBarObj.Buttons. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    rstrBadCaptions = rstrBadCaptions & "ToolBarBouton " & ToolBarObj.Buttons(intBouton).Key & ":" & ToolBarObj.Buttons(intBouton).Tag & vbCrLf
                Else
                    'do nothing
                End If
            Else
                strCaption = gvbNullString
            End If

            'SET CAPTION OR TOOLTIP
            Select Case strWhat
                Case "T" 'ToolTip
                    'UPGRADE_WARNING: Couldn't resolve default property of object ToolBarObj.Buttons. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    ToolBarObj.Buttons(intBouton).Caption = gvbNullString
                    'UPGRADE_WARNING: Couldn't resolve default property of object ToolBarObj.Buttons. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'

                    ToolBarObj.Buttons(intBouton).ToolTipText = strCaption
                Case "C"
                    'UPGRADE_WARNING: Couldn't resolve default property of object ToolBarObj.Buttons. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    ToolBarObj.Buttons(intBouton).Caption = strCaption
                    'UPGRADE_WARNING: Couldn't resolve default property of object ToolBarObj.Buttons. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'

                    ToolBarObj.Buttons(intBouton).ToolTipText = gvbNullString
                Case Else
                    'UPGRADE_WARNING: Couldn't resolve default property of object ToolBarObj.Buttons. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    ToolBarObj.Buttons(intBouton).Caption = strCaption
                    'UPGRADE_WARNING: Couldn't resolve default property of object ToolBarObj.Buttons. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'

                    ToolBarObj.Buttons(intBouton).ToolTipText = strCaption
            End Select

            'Traitement des sous-menu-Boutton
            'UPGRADE_WARNING: Couldn't resolve default property of object ToolBarObj.Buttons. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            For intMenu = 1 To ToolBarObj.Buttons(intBouton).ButtonMenus.Count

                'UPGRADE_WARNING: Couldn't resolve default property of object ToolBarObj.Buttons. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                strTag = ToolBarObj.Buttons(intBouton).ButtonMenus(intMenu).Tag

                'GEt what to set with caption
                Select Case True
                    Case IsNumeric(strTag)
                        lngTTLG_NRI = CInt(strTag)
                        strWhat = gvbNullString
                    Case UCase(Mid(strTag, 1, 1)) = "T"
                        lngTTLG_NRI = CInt(Mid(strTag, 2))
                        strWhat = "T"
                    Case UCase(Mid(strTag, 1, 1)) = "C"
                        lngTTLG_NRI = CInt(Mid(strTag, 2))
                        strWhat = "C"
                    Case Else
                        strWhat = gvbNullString
                        lngTTLG_NRI = 0
                End Select

                If lngTTLG_NRI <> 0 Then
                    strCaption = fstrGetCaption(CInt(lngTTLG_NRI), vlngUserLangue, vblnShowNRI)
                    If strCaption = gvbNullString Or strCaption = gstrBAD_CAPTION Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object ToolBarObj.Buttons. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        rstrBadCaptions = rstrBadCaptions & "ToolBarSousMenu " & ToolBarObj.Buttons(intBouton).ButtonMenus(intMenu).Key & ":" & ToolBarObj.Buttons(intBouton).ButtonMenus(intMenu).Tag & vbCrLf
                    Else
                        'do nothing
                    End If
                Else
                    strCaption = gvbNullString
                End If

                'SET CAPTION OR TOOLTIP
                Select Case strWhat
                    Case "T" 'ToolTip
                        'UPGRADE_WARNING: Couldn't resolve default property of object ToolBarObj.Buttons. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        ToolBarObj.Buttons(intBouton).ButtonMenus.Item(intMenu).Text = gvbNullString
                    Case "C"
                        'UPGRADE_WARNING: Couldn't resolve default property of object ToolBarObj.Buttons. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        ToolBarObj.Buttons(intBouton).ButtonMenus(intMenu).Text = strCaption
                    Case Else
                        'UPGRADE_WARNING: Couldn't resolve default property of object ToolBarObj.Buttons. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        ToolBarObj.Buttons(intBouton).ButtonMenus(intMenu).Text = strCaption
                End Select

            Next intMenu

        Next intBouton


Exit_pfblnToolBar_SetCaption:
        'UPGRADE_WARNING: Couldn't resolve default property of object pfblnToolBar_SetCaption. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        pfblnToolBar_SetCaption = blnReturn
        Exit Function

Error_pfblnToolBar_SetCaption:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_pfblnToolBar_SetCaption
    End Function



    '****************************************************************
    '* Nom de la fonction   : fblnShowMSG
    '*
    '*               Cree   : 03-02-2000   LAM
    '*            Modifie   : **-**-****  ***
    '*
    '*                But   : Retour un message box avec les parametres
    '*
    '* Parametre(s):
    '*              IN :
    '*
    '*              OUT:    True, si succes. False, si erreur
    '*
    '****************************************************************
    Public Function fblnShowMSG(ByVal vlngMessageID As Integer, ByVal vlngLangue As Integer, ByVal intVBType As VariantType, Optional ByRef vParam As Object = Nothing, Optional ByVal vblnShowNRI As Boolean = False) As Boolean
        On Error GoTo Error_fblnShowMSG
        Const strFCT_NAME As String = "fblnShowMSG "
        Dim blnReturn As Boolean
        Dim strMessage As String = gvbnullstring
        Dim intCount As Short

        blnReturn = True
        intCount = 0

        'Get message
        strMessage = fstrGetCaption(vlngMessageID, vlngLangue, vblnShowNRI)

        Do While 0 <> InStr(1, strMessage, "@", CompareMethod.Text)
            'UPGRADE_WARNING: Couldn't resolve default property of object vParam(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            strMessage = Replace(strMessage, "@" & intCount + 1, vParam(intCount), , , CompareMethod.Text)
            intCount = intCount + 1
        Loop

        MsgBox(strMessage, intVBType, gstrEXEName)


Exit_fblnShowMSG:
        fblnShowMSG = blnReturn
        Exit Function

Error_fblnShowMSG:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_fblnShowMSG
    End Function

    '****************************************************************
    '* Nom de la fonction   : fblnShowAnswerMSG
    '*
    '*               Cree   : 03-02-2000   LAM
    '*            Modifie   : **-**-****  ***
    '*
    '*                But   : Retour un message box avec les parametres
    '*
    '* Parametre(s):
    '*              IN :
    '*
    '*              OUT:    True, si succes. False, si erreur
    '*
    '****************************************************************
    Public Function fblnShowAnswerMSG(ByVal vlngMessageID As Integer, ByVal vlngLangue As Integer, ByVal intVBType As VariantType, ByRef rintReturnCode As Short, Optional ByRef vParam As Object = Nothing) As Boolean
        On Error GoTo Error_fblnShowAnswerMSG
        Const strFCT_NAME As String = "fblnShowAnswerMSG "
        Dim blnReturn As Boolean
        Dim strMessage As String = gvbnullstring
        Dim intCount As Short

        blnReturn = True
        intCount = 0

        'Get message
        strMessage = fstrGetCaption(vlngMessageID, vlngLangue, False)

        Do While 0 <> InStr(1, strMessage, "@", CompareMethod.Text)
            'UPGRADE_WARNING: Couldn't resolve default property of object vParam(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            strMessage = Replace(strMessage, "@" & intCount + 1, vParam(intCount), , , CompareMethod.Text)
            intCount = intCount + 1
        Loop

        rintReturnCode = MsgBox(strMessage, intVBType, gstrEXEName)


Exit_fblnShowAnswerMSG:
        fblnShowAnswerMSG = blnReturn
        Exit Function

Error_fblnShowAnswerMSG:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_fblnShowAnswerMSG
    End Function





    '****************************************************************
    '* Nom de la fonction   : fblnCombo_Load
    '*
    '*               Cree   : 05-26-2000
    '*            Modifie   : **-**-****  ***
    '*
    '*                But   :
    '*
    '* Parametre(s)         :
    '*              IN      :
    '*
    '*              OUT     :    True, si succes. False, si erreur
    '*
    '****************************************************************
    Public Function fblnCombo_Load(ByRef rcboToLoad As Object, ByVal vstrSQL As String, ByVal vlngDefaultNRI As Integer, ByVal mcnAdoConnection As ADODB.Connection, Optional ByVal vblnAllowEmpty As Boolean = True) As Boolean
        On Error GoTo Error_fblnCombo_Load
        Const strFCT_NAME As String = "fblnCombo_Load"
        Dim blnReturn As Boolean
        Dim strNRIField As String = gvbNullstring
        Dim intStartPos As Short
        Dim intEndPos As Short
        Dim intFieldEndPos As Short
        Dim strTableName As String = gvbNullstring
        Dim blnDefItemFound As Boolean
        Dim strComboValue As String = gvbNullstring
        Dim strDescField As String = gvbNullstring
        Dim lngCount As Long
        Dim recRecord As New ADODB.Recordset
        Dim t As CheckedListBox

        blnReturn = True
        blnDefItemFound = False
        lngCount = 1
        'Vide le combo
        'UPGRADE_WARNING: Couldn't resolve default property of object rcboToLoad.Clear. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'

        Select Case TypeName(rcboToLoad)
            Case "CheckedListBox", "ComboBox", "ctlComboOri"
                rcboToLoad.items.clear()
            Case Else
                rcboToLoad.Clear()
        End Select


        If vblnAllowEmpty Then
            'UPGRADE_WARNING: Couldn't resolve default property of object rcboToLoad.AddItem. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            Select Case TypeName(rcboToLoad)
                Case "CheckedListBox", "ComboBox", "ctlComboOri"
                    rcboToLoad.items.add("")
                Case Else
                    rcboToLoad.AddItem("")
            End Select

        End If

        Call fblnRecord_SELECT(vstrSQL, recRecord, mcnAdoConnection)
        If Not recRecord.EOF Then
            Do While Not recRecord.EOF
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                If Not IsDBNull(recRecord.Fields(0).Value) And Not IsDBNull(recRecord.Fields(1).Value) Then

                    'Ajoute le texte

                    Select Case TypeName(rcboToLoad)
                        Case "CheckedListBox", "ComboBox", "ctlComboOri"
                            rcboToLoad.items.add(recRecord.Fields(1).Value)
                        Case Else
                            rcboToLoad.AddItem(recRecord.Fields(1).Value)
                    End Select

                    'Ajoute le NRI ITEMDATA
                    VB6.SetItemData(rcboToLoad, rcboToLoad.Items.Count - 1, recRecord.Fields(0).Value)
                    'rcboToLoad.ItemData(rcboToLoad.NewIndex) = recRecord.Fields(0).Value

                    'Valeur defaut
                    If vlngDefaultNRI = recRecord.Fields(0).Value Then
                        blnDefItemFound = True

                        'MP: ?
                        rcboToLoad.SelectedIndex = rcboToLoad.Items.Count - 1
                        'rcboToLoad.ListIndex = rcboToLoad.NewIndex
                    Else
                        'PAs de valeur par d/faut
                    End If
                Else
                    'Ne rien mettre pour cet item
                End If

                'Passe a l enregiatrement suivant
                recRecord.MoveNext()
                lngCount = lngCount + 1
            Loop

            If Not blnDefItemFound And vlngDefaultNRI <> 0 Then
                'Force addition of default item in combo

                'Find Name of Nri Field
                vstrSQL = Trim(vstrSQL)
                intStartPos = Len("SELECT ")
                intFieldEndPos = InStr(vstrSQL, ",")

                If InStr(1, Left(vstrSQL, intEndPos), "as", CompareMethod.Text) <> 0 Then
                    'There is an alias, flush it!
                    intEndPos = InStr(vstrSQL, "as")
                Else
                    'No alias for NRI Field
                    intEndPos = intFieldEndPos
                End If

                'Field defined
                strNRIField = Mid(vstrSQL, intStartPos, intEndPos - intStartPos)

                'Define Description field name (Don't mind the alias)
                intStartPos = intFieldEndPos + 1
                intEndPos = InStr(LTrim(Right(vstrSQL, Len(vstrSQL) - intStartPos)), " ")

                'Table Name defined
                strDescField = Mid(vstrSQL, intStartPos, intEndPos)

                'Define Table Name (First table after 'FROM' clause)
                intStartPos = InStr(1, vstrSQL, " FROM ", CompareMethod.Text) + Len("FROM ")
                intEndPos = InStr(LTrim(Right(vstrSQL, Len(vstrSQL) - intStartPos)), " ")

                'Table Name defined
                strTableName = Trim(Mid(vstrSQL, intStartPos, intEndPos))

                blnReturn = fblnSingleLookUp(strTableName, strDescField, strComboValue, mcnAdoConnection, Replace(strNRIField, "Distinct", "", , , CompareMethod.Text) & " = " & vlngDefaultNRI)

                If blnReturn Then
                    If strComboValue <> gvbNullString Then
                        'Ajoute le texte
                        Select Case TypeName(rcboToLoad)
                            Case "CheckedListBox", "ComboBox", "ctlComboOri"
                                rcboToLoad.items.add(strComboValue)
                            Case Else
                                rcboToLoad.AddItem(strComboValue)
                        End Select

                        'Ajoute le NRI ITEMDATA
                        VB6.SetItemData(rcboToLoad, rcboToLoad.Items.Count - 1, vlngDefaultNRI)
                        'rcboToLoad.ItemData(rcboToLoad.NewIndex) = vlngDefaultNRI

                        'MP: ?
                        rcboToLoad.SelectedIndex = rcboToLoad.Items.Count - 1

                    Else
                        'L'item par défaut n'a pas été trouvé
                        blnReturn = False
                        Call GerrToLog(mstrModule_NAME & "::" & strFCT_NAME & " --> L'item par défaut recherché (" & strNRIField & " #" & CStr(vlngDefaultNRI) & ") a été retiré par un autre usager.")
                    End If
                Else
                    'Erreur
                End If
            Else
                'Do need to add default item to combo
            End If
        Else
            'NO DATA
        End If


Exit_fblnCombo_Load:
        'UPGRADE_NOTE: Object recRecord may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        recRecord = Nothing
        fblnCombo_Load = blnReturn
        Exit Function

Error_fblnCombo_Load:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_fblnCombo_Load
    End Function




    'VAlide if TEXT in value
    Public Function fblnCombo_ValideValue(ByVal robjCombo As Object, ByRef rblnValid As Boolean) As Boolean
        On Error GoTo Error_fblnCombo_ValideValue
        Const strFCT_NAME As String = "fblnCombo_ValideValue"
        Dim blnReturn As Boolean
        Dim intCount As Short
        Dim strText As String = gvbnullstring
        Dim blnValide As Boolean

        blnReturn = True
        rblnValid = False
        'UPGRADE_WARNING: Couldn't resolve default property of object robjCombo.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        strText = Trim(robjCombo.Text)

        If strText <> gvbNullString Then
            'UPGRADE_WARNING: Couldn't resolve default property of object robjCombo.ListCount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            For intCount = 0 To robjCombo.ListCount
                'UPGRADE_WARNING: Couldn't resolve default property of object robjCombo.List. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                If strText = robjCombo.List(intCount) Then
                    blnValide = True
                    Exit For
                End If
            Next intCount

            If blnValide Then
                'UPGRADE_WARNING: Couldn't resolve default property of object robjCombo.ListIndex. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                robjCombo.ListIndex = intCount
                rblnValid = True
            Else
                rblnValid = False
                'UPGRADE_WARNING: Couldn't resolve default property of object robjCombo.SelStart. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                robjCombo.SelStart = 0
                'UPGRADE_WARNING: Couldn't resolve default property of object robjCombo.SelLength. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                'UPGRADE_WARNING: Couldn't resolve default property of object robjCombo.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                robjCombo.SelLength = Len(robjCombo.Text)
            End If
        Else
            rblnValid = True
        End If


Exit_fblnCombo_ValideValue:
        fblnCombo_ValideValue = blnReturn
        Exit Function

Error_fblnCombo_ValideValue:
        blnReturn = True
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_fblnCombo_ValideValue
    End Function

    Public Function fblnGrid_SetForExcell(ByRef robjGrid As Object) As Boolean
        'Double-click pour Exporter vers Excell

        'robjGrid.Row = 0
        'robjGrid.Col = 0

        robjGrid.Cell(CellPropertySettings.flexcpBackColor, 0, 0, "", "") = TT3DLL.clsConstante.TTColorMode.TTCM_INSERT
        'robjGrid.CellBackColor = System.Drawing.Color.FromArgb(100, 150, 100)
    End Function


    Public Function fblnGrid_Fill(ByRef robjGrid As Object, ByVal vstrSQL As String, ByVal mcnAdoConnection As ADODB.Connection, Optional ByVal lngMaxRow As Integer = 0) As Boolean
        On Error GoTo Error_fblnGrid_Fill
        Const strFCT_NAME As String = "fblnGrid_Fill"
        Dim blnReturn As Boolean
        Dim recRecord As New ADODB.Recordset

        'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.Clear. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        robjGrid.Clear()
        'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.Rows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        robjGrid.Rows = 0
        'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.Rows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        robjGrid.Rows = 1
        'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.FixedRows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        robjGrid.FixedRows = 1
        'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.Cols. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        robjGrid.Cols = 0
        'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.Cols. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        robjGrid.Cols = 1
        'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.FixedCols. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        robjGrid.FixedCols = 1

        If fblnRecord_SELECT(vstrSQL, recRecord, mcnAdoConnection, lngMaxRow) Then
            'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.DataSource. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            'UPGRADE_NOTE: Object robjGrid.DataSource may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            robjGrid.DataSource = Nothing
            'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.DataSource. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            robjGrid.DataSource = recRecord
            blnReturn = True
        Else
            'Do Nothing
        End If

        Call fblnGrid_SetForExcell(robjGrid)

        On Error Resume Next

        If recRecord.RecordCount > 0 Then
            'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.Row. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            robjGrid.Row = 1
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.Row. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            robjGrid.Row = 0
        End If

Exit_fblnGrid_Fill:
        'UPGRADE_NOTE: Object recRecord may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        recRecord = Nothing
        fblnGrid_Fill = blnReturn
        Exit Function

Error_fblnGrid_Fill:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_fblnGrid_Fill
    End Function
    Public Function fblnGrid_Fill_Net(ByRef robjGrid As TT3DLL.ctlTTGrid, ByVal vstrSQL As String, ByVal mcnAdoConnection_Net As SqlClient.SqlConnection, Optional ByVal lngMaxRow As Integer = 0) As Boolean
        On Error GoTo Error_fblnGrid_Fill_Net
        Const strFCT_NAME As String = "fblnGrid_Fill_Net"
        Dim blnReturn As Boolean
        Dim objTable As New DataTable

        robjGrid.DataSource = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.Clear. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        robjGrid.Clear()
        'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.Rows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        robjGrid.Rows = 0
        'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.Rows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        robjGrid.Rows = 1
        'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.FixedRows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        robjGrid.FixedRows = 1
        'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.Cols. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        robjGrid.Cols = 0
        'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.Cols. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        robjGrid.Cols = 1
        'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.FixedCols. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        robjGrid.FixedCols = 1

        If fblnRecord_SELECT_Net(vstrSQL, objTable, mcnAdoConnection_Net, lngMaxRow) Then
            'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.DataSource. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            'UPGRADE_NOTE: Object robjGrid.DataSource may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'

            'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.DataSource. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            robjGrid.DataSource = objTable
            blnReturn = True
        Else
            'Do Nothing
        End If

        Call fblnGrid_SetForExcell(robjGrid)

        On Error Resume Next

        If objTable.Rows.Count > 0 Then
            'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.Row. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            robjGrid.Row = 1
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.Row. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            robjGrid.Row = 0
        End If

Exit_fblnGrid_Fill_Net:
        'UPGRADE_NOTE: Object recRecord may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objTable = Nothing
        fblnGrid_Fill_Net = blnReturn
        Exit Function

Error_fblnGrid_Fill_Net:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_fblnGrid_Fill_Net
    End Function


    '****************************************************************
    '* Nom de la fonction   : fblnDisabledComboTexBox
    '*
    '*               Cree   : 30-03-2000   LL
    '*            Modifie   : 01-04-2000   LAM (Désolé !!!!!!!)
    '*
    '*                But   : Disabled combobox et textbox
    '*
    '* Parametre(s):
    '*              IN :
    '*
    '*              OUT:    True, si succes. False, si erreur
    '*
    '****************************************************************
    Public Function fblnControl_Disabled(ByRef robjField As Object) As Boolean
        On Error GoTo Error_fblnControl_Disabled
        Const strFCT_NAME As String = "fblnControl_Disabled"
        Dim blnReturn As Boolean

        blnReturn = True

        'Check if object type is valid
        'UPGRADE_WARNING: TypeName has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        Select Case TypeName(robjField)
            Case "TextBox", "MaskedTextBox"
                robjField.ReadOnly = True
                'MP: new...
                robjField.TabStop = False
                'UPGRADE_WARNING: Couldn't resolve default property of object robjField.BackColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                robjField.BackColor = System.Drawing.SystemColors.Control
            Case "ComboBox"
                robjField.Enabled = False
                robjField.BackColor = System.Drawing.SystemColors.Control

            Case "ctlComboOri"
                robjField.ReadOnly = True
                robjField.BackColor = System.Drawing.SystemColors.Control

            Case "ctlTTCombo", "ctlTTAdresse"
                'UPGRADE_WARNING: Couldn't resolve default property of object robjField.Enabled. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                robjField.Enabled = False

                'UPGRADE_WARNING: Couldn't resolve default property of object robjField.UseInGrid. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                If Not robjField.UseInGrid Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object robjField.TabStop. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    robjField.TabStop = False
                Else
                    'le tab stop doit toujours etre a faux dans les grilles
                End If

            Case "ctlTTTextCombo_3PL", "ctlTTFilter_3PL"
                robjField.Enabled = False

            Case "ctlTTMultiLangue"
                'UPGRADE_WARNING: Couldn't resolve default property of object robjField.TabStop. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                robjField.TabStop = False
                'UPGRADE_WARNING: Couldn't resolve default property of object robjField.Enabled. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                robjField.Enabled = False

            Case "DTPicker"
                'UPGRADE_WARNING: Couldn't resolve default property of object robjField.Enabled. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                robjField.Enabled = False

            Case "CommandButton"
                'UPGRADE_WARNING: Couldn't resolve default property of object robjField.Enabled. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                robjField.Enabled = False

            Case "ListBox", "ListView", "CheckedListBox"
                'UPGRADE_WARNING: Couldn't resolve default property of object robjField.Enabled. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                'robjField.Enabled = False
                'UPGRADE_WARNING: Couldn't resolve default property of object robjField.BackColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                robjField.enabled = False
                robjField.BackColor = System.Drawing.SystemColors.Control

            Case Else
                'UPGRADE_WARNING: Couldn't resolve default property of object robjField.Enabled. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                robjField.Enabled = False
        End Select


Exit_fblnControl_Disabled:
        fblnControl_Disabled = blnReturn
        Exit Function

Error_fblnControl_Disabled:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_fblnControl_Disabled
    End Function

    '****************************************************************
    '* Nom de la fonction   : fblnControl_Enabled
    '*
    '*               Cree   : 03-04-2000   KF
    '*            Modifie   :
    '*
    '*                But   : Enabled combobox et textbox
    '*
    '* Parametre(s):
    '*              IN :
    '*
    '*              OUT:    True, si succes. False, si erreur
    '*
    '****************************************************************
    Public Function fblnControl_Enabled(ByRef robjField As Object) As Boolean
        On Error GoTo Error_fblnControl_Enabled
        Const strFCT_NAME As String = "fblnControl_Enabled"
        Dim blnReturn As Boolean

        blnReturn = True

        'Check if object type is valid
        'UPGRADE_WARNING: TypeName has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        Select Case TypeName(robjField)
            Case "TextBox", "MaskedTextBox"
                'UPGRADE_WARNING: Couldn't resolve default property of object robjField.Locked. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                robjField.ReadOnly = False
                'UPGRADE_WARNING: Couldn't resolve default property of object robjField.TabStop. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                robjField.TabStop = True
                'UPGRADE_WARNING: Couldn't resolve default property of object robjField.BackColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                robjField.BackColor = System.Drawing.SystemColors.Window

            Case "ComboBox"
                'UPGRADE_WARNING: Couldn't resolve default property of object robjField.TabStop. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                robjField.TabStop = True
                robjField.enabled = True
                'UPGRADE_WARNING: Couldn't resolve default property of object robjField.BackColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                robjField.BackColor = System.Drawing.SystemColors.Window

            Case "ctlComboOri"
                robjField.TabStop = True
                robjField.ReadOnly = False
                robjField.BackColor = System.Drawing.SystemColors.Window

            Case "ctlTTCombo", "ctlTTAdresse"
                'UPGRADE_WARNING: Couldn't resolve default property of object robjField.Enabled. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                robjField.Enabled = True

                'UPGRADE_WARNING: Couldn't resolve default property of object robjField.UseInGrid. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                If Not robjField.UseInGrid Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object robjField.TabStop. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    robjField.TabStop = True
                Else
                    'le tab stop doit toujours etre a faux dans les grilles
                End If

            Case "ctlTTTextCombo_3PL", "ctlTTFilter_3PL"
                robjField.Enabled = True

            Case "ctlTTMultiLangue"
                'UPGRADE_WARNING: Couldn't resolve default property of object robjField.TabStop. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                robjField.TabStop = True
                'UPGRADE_WARNING: Couldn't resolve default property of object robjField.Enabled. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                robjField.Enabled = True

            Case "DTPicker"
                'UPGRADE_WARNING: Couldn't resolve default property of object robjField.Enabled. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                robjField.Enabled = True

            Case "CommandButton"
                'UPGRADE_WARNING: Couldn't resolve default property of object robjField.Enabled. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                robjField.Enabled = True

            Case "ListBox", "ListView", "CheckedListBox"
                'UPGRADE_WARNING: Couldn't resolve default property of object robjField.Enabled. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                robjField.Enabled = True
                'UPGRADE_WARNING: Couldn't resolve default property of object robjField.BackColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                robjField.BackColor = System.Drawing.SystemColors.Window

            Case Else
                'UPGRADE_WARNING: Couldn't resolve default property of object robjField.Enabled. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                robjField.Enabled = True
        End Select


Exit_fblnControl_Enabled:
        fblnControl_Enabled = blnReturn
        Exit Function

Error_fblnControl_Enabled:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_fblnControl_Enabled
    End Function

    '****************************************************************
    '* Nom de la fonction   : pfblnTTCombo_SetCaption
    '*
    '*               Cree   : 10-01-2002   lamorin
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
    Private Function pfblnTTCombo_SetCaption(ByRef rctlControl As Object, ByVal vlngUserLangue As Integer, Optional ByVal vblnShowNRI As Boolean = False, Optional ByRef rstrBadCaption As Object = gvbNullString) As Boolean
        On Error GoTo Error_pfblnTTCombo_SetCaption
        Const strFCT_NAME As String = "pfblnTTCombo_SetCaption"
        Dim blnReturn As Boolean
        Dim strTag1 As String = gvbnullstring
        Dim strTag2 As String = gvbnullstring
        Dim inpPosition As Short
        Dim strCaption As String = gvbnullstring

        blnReturn = True

        'UPGRADE_WARNING: Couldn't resolve default property of object rctlControl.Tag. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        If CStr(rctlControl.Tag) <> gvbNullstring Then
            'UPGRADE_WARNING: Couldn't resolve default property of object rctlControl.Tag. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            inpPosition = InStr(1, rctlControl.Tag, "|", CompareMethod.Text)
            'UPGRADE_WARNING: Couldn't resolve default property of object rctlControl.Tag. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            strTag1 = Mid(rctlControl.Tag, 1, inpPosition - 1)
            'UPGRADE_WARNING: Couldn't resolve default property of object rctlControl.Tag. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            strTag2 = Mid(rctlControl.Tag, inpPosition + 1)

            If vblnShowNRI Then
                'UPGRADE_WARNING: Couldn't resolve default property of object rctlControl.Tag. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                strCaption = "(" & rctlControl.Tag & ")" & fstrGetCaption(CInt(strTag1), vlngUserLangue, False)
            Else
                strCaption = fstrGetCaption(CInt(strTag1), vlngUserLangue, False)
            End If

            'UPGRADE_WARNING: Couldn't resolve default property of object rctlControl.mstrFormCaption. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            rctlControl.mstrFormCaption = strCaption
            'UPGRADE_WARNING: Couldn't resolve default property of object rctlControl.mstrGridCaption. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            rctlControl.mstrGridCaption = fstrGetCaption(CInt(strTag2), vlngUserLangue, False)

            'UPGRADE_WARNING: Couldn't resolve default property of object rctlControl.mstrGridCaption. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            'UPGRADE_WARNING: Couldn't resolve default property of object rctlControl.mstrFormCaption. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            If rctlControl.mstrFormCaption = gstrBAD_CAPTION Or rctlControl.mstrGridCaption = gstrBAD_CAPTION Then
                'UPGRADE_WARNING: Couldn't resolve default property of object rctlControl.Name. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                'UPGRADE_WARNING: Couldn't resolve default property of object rstrBadCaption. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                rstrBadCaption = "TTCombo " & rctlControl.Name & ":"
                'UPGRADE_WARNING: Couldn't resolve default property of object rctlControl.mstrFormCaption. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                If rctlControl.mstrFormCaption = gstrBAD_CAPTION Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object rstrBadCaption. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    rstrBadCaption = rstrBadCaption & "Form;" & strTag1 & " "
                Else
                    'do nothing
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object rctlControl.mstrGridCaption. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                If rctlControl.mstrGridCaption = gstrBAD_CAPTION Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object rstrBadCaption. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    rstrBadCaption = rstrBadCaption & "Grid;" & strTag2
                Else

                End If
            Else
                'do nothing
            End If
        Else
            ' Do nothing
        End If


Exit_pfblnTTCombo_SetCaption:
        pfblnTTCombo_SetCaption = blnReturn
        Exit Function

Error_pfblnTTCombo_SetCaption:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_pfblnTTCombo_SetCaption
    End Function

    '****************************************************************
    '* Nom de la fonction   : fblnListeBox_GetSelectedItemData
    '*
    '*               Cree   : 28-02-2002   lamorin
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
    Public Function fblnListeBox_GetSelectedItemData(ByVal vlstMyListe As Object, ByRef rstrFilter As String) As Boolean
        On Error GoTo Error_fblnListeBox_GetSelectedItemData
        Const strFCT_NAME As String = "fblnListeBox_GetSelectedItemData"
        Dim blnReturn As Boolean
        Dim lngCount As Long

        blnReturn = True
        rstrFilter = gvbNullString

        'UPGRADE_WARNING: Couldn't resolve default property of object vlstMyListe.ListCount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        For lngCount = 0 To vlstMyListe.Items.Count - 1
            'UPGRADE_WARNING: Couldn't resolve default property of object vlstMyListe.Selected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'

            If vlstMyListe.GetItemChecked(lngCount) Then
                'UPGRADE_WARNING: Couldn't resolve default property of object vlstMyListe.ItemData. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                rstrFilter = rstrFilter & VB6.GetItemData(vlstMyListe, lngCount) & ", "
            Else
                'Do nothing
            End If
        Next lngCount

        If Len(rstrFilter) > 0 Then
            rstrFilter = Mid(rstrFilter, 1, Len(rstrFilter) - 2)
        Else
            'Do nothing
        End If


Exit_fblnListeBox_GetSelectedItemData:
        fblnListeBox_GetSelectedItemData = blnReturn
        Exit Function

Error_fblnListeBox_GetSelectedItemData:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_fblnListeBox_GetSelectedItemData
    End Function

    Public Function fblnListeBox_SelectDefaultItemData(ByVal vlstMyListe As Object, ByVal vvarItemData As Object) As Boolean
        On Error GoTo Error_fblnListeBox_SelectDefaultItemData
        Const strFCT_NAME As String = "fblnListeBox_SelectDefaultItemData"
        Dim blnReturn As Boolean
        Dim intCount As Short
        Dim intCountItem As Short

        blnReturn = True

        For intCountItem = 0 To UBound(vvarItemData)

            For intCount = 0 To vlstMyListe.items.count - 1
                If CStr(VB6.GetItemData(vlstMyListe, intCount)) = CStr(vvarItemData(intCountItem)) Then
                    vlstMyListe.SetItemChecked(intCount, True)
                Else
                    'Do nothing
                End If
            Next intCount
        Next intCountItem


Exit_fblnListeBox_SelectDefaultItemData:
        fblnListeBox_SelectDefaultItemData = blnReturn
        Exit Function

Error_fblnListeBox_SelectDefaultItemData:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_fblnListeBox_SelectDefaultItemData
    End Function

    '****************************************************************
    '* Nom de la fonction   : pfblnToolbar_Secure
    '*
    '*               Cree   : 26-03-2002   lamorin
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
    Private Function pfblnToolbar_Secure(ByRef objToolBar As Object) As Boolean
        On Error GoTo Error_pfblnToolbar_Secure
        Const strFCT_NAME As String = "pfblnToolbar_Secure"
        Dim blnReturn As Boolean
        Dim recRec As New ADODB.Recordset
        Dim strName As String = gvbnullstring
        Dim intBouton As Short
        Dim intMenu As Short

        blnReturn = True

        'UPGRADE_WARNING: Couldn't resolve default property of object objToolBar.Buttons. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        For intBouton = 1 To objToolBar.Buttons.Count
            'UPGRADE_WARNING: Couldn't resolve default property of object objToolBar.Buttons. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            strName = objToolBar.Buttons(intBouton).Key

            gRecMenuSecur.MoveFirst()
            Call gRecMenuSecur.Find("TTM_Constante = '" & strName & "' " & vbCrLf, , ADODB.SearchDirectionEnum.adSearchForward, 0)

            If Not gRecMenuSecur.EOF Then
                If gRecMenuSecur.Fields("TTMR_Right").Value = 1 Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object objToolBar.Buttons. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    objToolBar.Buttons(intBouton).Visible = True
                    'UPGRADE_WARNING: Couldn't resolve default property of object objToolBar.Buttons. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    objToolBar.Buttons(intBouton).Enabled = True
                Else
                    'UPGRADE_WARNING: Couldn't resolve default property of object objToolBar.Buttons. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    objToolBar.Buttons(intBouton).Visible = False
                End If
            Else
                'Pas de securite
                'UPGRADE_WARNING: Couldn't resolve default property of object objToolBar.Buttons. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                objToolBar.Buttons(intBouton).Caption = "No securirty"
            End If

            'Traitement des sous-menu-Boutton
            'UPGRADE_WARNING: Couldn't resolve default property of object objToolBar.Buttons. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            For intMenu = 1 To objToolBar.Buttons(intBouton).ButtonMenus.Count
                'UPGRADE_WARNING: Couldn't resolve default property of object objToolBar.Buttons. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                strName = objToolBar.Buttons(intBouton).ButtonMenus(intMenu).Key

                gRecMenuSecur.MoveFirst()
                Call gRecMenuSecur.Find("TTM_Constante = '" & strName & "' " & vbCrLf, , ADODB.SearchDirectionEnum.adSearchForward, 0)

                If Not gRecMenuSecur.EOF Then
                    If gRecMenuSecur.Fields("TTMR_Right").Value = 1 Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object objToolBar.Buttons. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        objToolBar.Buttons(intBouton).ButtonMenus(intMenu).Visible = True
                    Else
                        'UPGRADE_WARNING: Couldn't resolve default property of object objToolBar.Buttons. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                        objToolBar.Buttons(intBouton).ButtonMenus(intMenu).Visible = False
                    End If
                Else
                    'Pas de securite
                    'UPGRADE_WARNING: Couldn't resolve default property of object objToolBar.Buttons. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    objToolBar.Buttons(intBouton).ButtonMenus(intMenu).Text = "No securirty"
                End If
            Next intMenu
        Next intBouton


Exit_pfblnToolbar_Secure:
        'UPGRADE_NOTE: Object recRec may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        recRec = Nothing
        pfblnToolbar_Secure = blnReturn
        Exit Function

Error_pfblnToolbar_Secure:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_pfblnToolbar_Secure
    End Function

    '****************************************************************
    '* Nom de la fonction   : pfblnRecSecure_Set
    '*
    '*               Cree   : 26-03-2002   lamorin
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
    Private Function pfblnRecSecure_Set(ByVal vlngGroupeNRI As Short, ByVal vlngTTAP_NRi As Integer, ByVal mcnAdoConnection As ADODB.Connection) As Boolean
        On Error GoTo Error_pfblnRecSecure_Set
        Const strFCT_NAME As String = "pfblnRecSecure_Set"
        Dim blnReturn As Boolean
        Dim strSQL As String = gvbnullstring

        blnReturn = True

        If gRecMenuSecur Is Nothing Then
            'MP 2004-01-14: Quand un nouveau menu est créé, pour les groupes autres que l'administration
            'ces menus n'apparaissait pas dans la requete, ce qui causait des no security sur le bind de ces menus.
            'Donc, quand un groupe n'a pas encore les droits sur un menu, le droit est 0.
            strSQL = " SELECT distinct TTM_D.TTM_NRI,  " & vbCrLf
            strSQL = strSQL & "     TTM_D.TTM_Constante,  " & vbCrLf
            strSQL = strSQL & "     TTMR_Right = CASE WHEN TTM_NotSecure=1  " & vbCrLf
            strSQL = strSQL & "               THEN 1 " & vbCrLf
            strSQL = strSQL & "               ELSE CASE WHEN TTMR_Right IS NULL " & vbCrLf
            strSQL = strSQL & "                     THEN 0  " & vbCrLf
            strSQL = strSQL & "                     ELSE TTMR_Right " & vbCrLf
            strSQL = strSQL & "                    END " & vbCrLf
            strSQL = strSQL & "              END " & vbCrLf
            strSQL = strSQL & " FROM TTM_D  " & vbCrLf
            strSQL = strSQL & "     LEFT JOIN TTMR_D ON TTM_D.TTM_NRI = TTMR_D.TTM_NRI AND TTMR_D.TTG_NRI = " & vlngGroupeNRI & vbCrLf

            blnReturn = fblnRecord_SELECT(strSQL, gRecMenuSecur, mcnAdoConnection)
        Else
            'Do nothing
        End If


Exit_pfblnRecSecure_Set:
        pfblnRecSecure_Set = blnReturn
        Exit Function

Error_pfblnRecSecure_Set:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_pfblnRecSecure_Set
    End Function

    Public Function fstrIniFile_InputBox(ByRef strSection As String, ByRef strKey As String, ByRef strDefault As String) As String
        Const strFCT_NAME As String = "fstrIniFile_InputBox"
        Dim strReponse As String = gvbNullstring
        Dim strTitle As String = gvbNullstring
        Dim strPrompt As String = gvbNullstring

        strReponse = gvbNullstring
        strTitle = "Initialize " & gstrEXEShortName & ".ini"
        strPrompt = "Section :" & vbTab & strSection & vbCrLf
        strPrompt = strPrompt & "Key       :" & vbTab & strKey

        strReponse = InputBox(strPrompt, strTitle, strDefault)


Exit_fstrIniFile_InputBox:
        fstrIniFile_InputBox = strReponse
        Exit Function

Error_fstrIniFile_InputBox:
        strReponse = gvbNullstring
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_fstrIniFile_InputBox
    End Function
	'****************************************************************
	'* Nom de la fonction   : fblnGrid_FillWithRecordset
	'*
	'*               Cree   : 24-10-2002   mpelletier
	'*            Modifie   : **-**-****  ***
	'*
	'*                But   : Utile dans les cas comme la liste generique ou on doit garder
	'*                        le recordset dans la fenetre (filtres,...).
	'*
	'* Parametre(s):
	'*              IN :
	'*
	'*              OUT:    True, si succes. False, si erreur
	'*
	'****************************************************************
	Public Function fblnGrid_FillWithRecordset(ByRef robjGrid As Object, ByVal vrecRecord As ADODB.Recordset) As Boolean
		On Error GoTo Error_fblnGrid_FillWithRecordset
		Const strFCT_NAME As String = "fblnGrid_FillWithRecordset"
        Dim blnReturn As Boolean

		
		blnReturn = True
		
		'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.Clear. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		robjGrid.Clear()
		'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.Rows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		robjGrid.Rows = 0
		'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.Rows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		robjGrid.Rows = 1
		'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.FixedRows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		robjGrid.FixedRows = 1
		'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.Cols. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		robjGrid.Cols = 0
		'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.Cols. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		robjGrid.Cols = 1
		'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.FixedCols. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		robjGrid.FixedCols = 1
		
		'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.DataSource. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		'UPGRADE_NOTE: Object robjGrid.DataSource may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		robjGrid.DataSource = Nothing
		'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.DataSource. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		robjGrid.DataSource = vrecRecord

        Call fblnGrid_SetForExcell(robjGrid)

		If vrecRecord.RecordCount > 0 Then
			'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.Row. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			robjGrid.Row = 1
		Else
			'UPGRADE_WARNING: Couldn't resolve default property of object robjGrid.Row. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			robjGrid.Row = 0
        End If

		
Exit_fblnGrid_FillWithRecordset: 
		fblnGrid_FillWithRecordset = blnReturn
		Exit Function
		
Error_fblnGrid_FillWithRecordset: 
		blnReturn = False
		Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
		Resume Exit_fblnGrid_FillWithRecordset
    End Function
    '****************************************************************
    '* Nom de la fonction   : fblnGrid_FillWithRecordset
    '*
    '*               Cree   : 24-10-2002   mpelletier
    '*            Modifie   : **-**-****  ***
    '*
    '*                But   : Utile dans les cas comme la liste generique ou on doit garder
    '*                        le recordset dans la fenetre (filtres,...).
    '*
    '* Parametre(s):
    '*              IN :
    '*
    '*              OUT:    True, si succes. False, si erreur
    '*
    '****************************************************************
    Public Function fblnGrid_FillWithRecordset_Net(ByRef robjGrid As TT3DLL.ctlTTGrid, ByVal vrecRecord As ADODB.Recordset, Optional ByVal vblnKeepDisconnectedDataSource As Boolean = False) As Boolean
        On Error GoTo Error_fblnGrid_FillWithRecordset_Net
        Const strFCT_NAME As String = "fblnGrid_FillWithRecordset_Net"
        Dim blnReturn As Boolean
        'Dim myDA As New OleDb.OleDbDataAdapter()
        Dim myDA As New OleDb.OleDbDataAdapter()
        Dim myDS As DataSet = New DataSet()
        Dim myDT As DataTable = New DataTable
        Dim cCol As New DataColumn
        Dim lngCount As Long
        Dim intCpt As Integer
        Dim dtSourceDatas As DataTable
        Dim intRow As Integer
        Dim intCol As Integer
        Dim lstCols As System.Collections.Generic.List(Of Object)

        blnReturn = True

        robjGrid.DataSource = Nothing

        'robjGrid.Clear()
        robjGrid.Rows = 0
        On Error Resume Next
        robjGrid.Rows = 1
        robjGrid.FixedRows = 1

        'MP: Des fois ça plante avec un Cell range invalid...
        On Error Resume Next
        robjGrid.Cols = 0
        On Error GoTo Error_fblnGrid_FillWithRecordset_Net

        robjGrid.Cols = 1
        robjGrid.FixedCols = 1

        lngCount = vrecRecord.RecordCount

        myDA.Fill(myDS, vrecRecord, "MyTable")
        dtSourceDatas = myDS.Tables(0)

        'Set les colonnes
        robjGrid.Cols = myDS.Tables(0).Columns.Count + 1

        If (Not vblnKeepDisconnectedDataSource) Then
            ' Binder la grille à la source des données.
            For Each cCol In dtSourceDatas.Columns
                myDT.Columns.Add(cCol.ColumnName, GetType(Object))
            Next

            myDT.Merge(dtSourceDatas, False, MissingSchemaAction.Ignore)

            robjGrid.DataSource = myDT
        Else
            Call pfblnGRD_Unbound(robjGrid, dtSourceDatas)
            'Dim cRow As DataRow

            '' Conserver la grille en mode unbound.            
            'For intRow = 0 To dtSourceDatas.Rows.Count - 1
            '    lstCols = Nothing
            '    lstCols = New System.Collections.Generic.List(Of Object)
            '    lstCols.Add(gvbNullstring)
            '    lstCols.AddRange(dtSourceDatas.Rows(intRow).ItemArray)
            '    robjGrid.AddItem(lstCols.ToArray())
            'Next
        End If

        Call fblnGrid_SetForExcell(robjGrid)

        If (robjGrid.Parent IsNot Nothing) Then
            If lngCount > 0 Then
                robjGrid.Row = 1
            Else
                robjGrid.Row = 0
            End If
        End If


Exit_fblnGrid_FillWithRecordset_Net:
        'robjGrid.Redraw = C1.Win.C1FlexGrid.Classic.RedrawSettings.flexRDBuffered
        lstCols = Nothing
        myDA = Nothing
        myDS = Nothing
        myDT = Nothing
        cCol = Nothing
        dtSourceDatas = Nothing
        fblnGrid_FillWithRecordset_Net = blnReturn
        Exit Function

Error_fblnGrid_FillWithRecordset_Net:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_fblnGrid_FillWithRecordset_Net
    End Function


    '****************************************************************
    '* Nom de la fonction   : fblnGrid_FillWithDataTable_Net
    '*
    '*               Cree   : 03-01-2011  Eric
    '*            Modifie   : **-**-****  ***
    '*
    '*                But   : Fill la grille à partir d'un dataTable
    '*
    '* Parametre(s):
    '*              IN :
    '*
    '*              OUT:    True, si succes. False, si erreur
    '*
    '****************************************************************
    Public Function fblnGrid_FillWithDataTable_Net(ByRef robjGrid As TT3DLL.ctlTTGrid, _
                                                   ByVal vdataTable As System.Data.DataTable, _
                                                   Optional ByVal vblnKeepDisconnectedDataSource As Boolean = False) As Boolean
        On Error GoTo Error_fblnGrid_FillWithDataTable_Net
        Const strFCT_NAME As String = "fblnGrid_FillWithDataTable_Net"
        Dim blnReturn As Boolean
        Dim lngCount As Long
        Dim intRow As Integer
        Dim intCol As Integer
        Dim lstCols As System.Collections.Generic.List(Of Object)
        Dim myDT As DataTable = New DataTable
        Dim cCol As New DataColumn

        blnReturn = True

        robjGrid.DataSource = Nothing

        'robjGrid.Clear()
        robjGrid.Rows = 0
        On Error Resume Next
        robjGrid.Rows = 1
        robjGrid.FixedRows = 1

        'MP: Des fois ça plante avec un Cell range invalid...
        On Error Resume Next
        robjGrid.Cols = 0
        On Error GoTo Error_fblnGrid_FillWithDataTable_Net

        robjGrid.Cols = 1
        robjGrid.FixedCols = 1

        lngCount = vdataTable.Rows.Count

        'Set les colonnes
        robjGrid.Cols = vdataTable.Columns.Count + 1

        If (Not vblnKeepDisconnectedDataSource) Then

            ' Binder la grille à la source des données.
            For Each cCol In vdataTable.Columns
                myDT.Columns.Add(cCol.ColumnName, GetType(Object))
            Next

            myDT.Merge(vdataTable, False, MissingSchemaAction.Ignore)

            robjGrid.DataSource = myDT

        Else
            Call pfblnGRD_Unbound(robjGrid, vdataTable)
            

        End If


        Call fblnGrid_SetForExcell(robjGrid)


        If lngCount > 0 Then
            robjGrid.Row = 1
        Else
            robjGrid.Row = 0
        End If

Exit_fblnGrid_FillWithDataTable_Net:
        'robjGrid.Redraw = C1.Win.C1FlexGrid.Classic.RedrawSettings.flexRDBuffered
        lstCols = Nothing
        myDT = Nothing
        cCol = Nothing
        fblnGrid_FillWithDataTable_Net = blnReturn
        Exit Function

Error_fblnGrid_FillWithDataTable_Net:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_fblnGrid_FillWithDataTable_Net
    End Function

    '****************************************************************
    '* Nom de la fonction   : fblnGrid_FillWithListe_Net
    '*
    '*               Cree   : 10-12-2011  Nicolas
    '*            Modifie   : **-**-****  ***
    '*
    '*                But   : Fill la grille à partir d'une Liste
    '*
    '* Parametre(s):
    '*              IN :
    '*
    '*              OUT:    True, si succes. False, si erreur
    '*
    '****************************************************************
    Public Function fblnGrid_FillWithListe_Net(ByRef robjGrid As TT3DLL.ctlTTGrid, _
                                                   ByVal vListe As TT3DLL.clsListe(Of TT3DLL.IItem)) As Boolean
        On Error GoTo Error_fblnGrid_FillWithListe_Net
        Const strFCT_NAME As String = "fblnGrid_FillWithListe_Net"
        Dim blnReturn As Boolean
        Dim lngCount As Long
        Dim intRow As Integer
        Dim intCol As Integer
        Dim lstCols As System.Collections.Generic.List(Of Object)
        Dim myDT As DataTable = New DataTable
        Dim cCol As New DataColumn

        blnReturn = True

        robjGrid.DataSource = Nothing

        'robjGrid.Clear()
        robjGrid.Rows = 0
        On Error Resume Next
        robjGrid.Rows = 1
        robjGrid.FixedRows = 1

        'MP: Des fois ça plante avec un Cell range invalid...
        On Error Resume Next
        robjGrid.Cols = 0
        On Error GoTo Error_fblnGrid_FillWithListe_Net

        robjGrid.Cols = 1
        robjGrid.FixedCols = 1

        lngCount = vListe.Items.Count

        For Each cItem As TT3DLL.clsItem In vListe
            If robjGrid.Cols = 1 Then
                robjGrid.Cols = cItem.ToString().Split("|").Count()
            End If

            robjGrid.AddItem(cItem.ToString().Split("|"))
        Next

        Call fblnGrid_SetForExcell(robjGrid)

        If lngCount > 0 Then
            robjGrid.Row = 1
        Else
            robjGrid.Row = 0
        End If

Exit_fblnGrid_FillWithListe_Net:
        'robjGrid.Redraw = C1.Win.C1FlexGrid.Classic.RedrawSettings.flexRDBuffered
        lstCols = Nothing
        myDT = Nothing
        cCol = Nothing
        fblnGrid_FillWithListe_Net = blnReturn
        Exit Function

Error_fblnGrid_FillWithListe_Net:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_fblnGrid_FillWithListe_Net
    End Function

    '*************************************************************
    '*
    '* Name:        pfblnGRD_Unbound
    '*
    '* Par:        	Michael,  2011-05-18
    '*
    '* Description: 
    '*
    '* IN:
    '* OUT: False si erreur, True si OK
    '*
    '*************************************************************
    Private Function pfblnGRD_Unbound(ByRef robjGrid As TT3DLL.ctlTTGrid, ByRef rdtDataTable As DataTable) As Boolean
        On Error GoTo Error_pfblnGRD_Unbound
        Const strFCT_NAME As String = "pfblnGRD_Unbound"

        Dim blnReturn As Boolean
        Dim intRow As Long
        Dim lstCols As System.Collections.Generic.List(Of Object)

        blnReturn = True
        'robjGrid.DataSource = rdtDataTable
        'Conserver la grille en mode unbound.            
        For intRow = 0 To rdtDataTable.Rows.Count - 1
            lstCols = Nothing
            lstCols = New System.Collections.Generic.List(Of Object)
            lstCols.Add(gvbNullstring)
            lstCols.AddRange(rdtDataTable.Rows(intRow).ItemArray)
            robjGrid.AddItem(lstCols.ToArray())
        Next

        'robjGrid.Rows = rdtDataTable.Rows.Count + 1
        'robjGrid.Cols = rdtDataTable.Columns.Count + 1

        'Dim dr As DataRow, dc As DataColumn
        'Dim rowIndex%, colIndex%

        'rowIndex = 0

        'For Each dr In rdtDataTable.Rows
        '    rowIndex = rowIndex + 1
        '    colIndex = 0
        '    For Each dc In rdtDataTable.Columns
        '        colIndex = colIndex + 1
        '        robjGrid(rowIndex, colIndex) = dr(dc)
        '    Next
        'Next


Exit_pfblnGRD_Unbound:
        lstCols = Nothing
        pfblnGRD_Unbound = blnReturn
        Exit Function

Error_pfblnGRD_Unbound:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_pfblnGRD_Unbound
    End Function

    '****************************************************************
    '* Nom de la fonction   : fblnGrid_FillWithDataView_Net
    '*
    '*               Cree   : 03-01-2011  Eric
    '*            Modifie   : **-**-****  ***
    '*
    '*                But   : Fill la grille à partir d'un dataView
    '*
    '* Parametre(s):
    '*              IN :
    '*
    '*              OUT:    True, si succes. False, si erreur
    '*
    '****************************************************************
    Public Function fblnGrid_FillWithDataView_Net(ByRef robjGrid As TT3DLL.ctlTTGrid, _
                                                   ByVal vdataView As System.Data.DataView, _
                                                   Optional ByVal vblnKeepDisconnectedDataSource As Boolean = False) As Boolean
        On Error GoTo Error_fblnGrid_FillWithDataView_Net
        Const strFCT_NAME As String = "fblnGrid_FillWithDataView_Net"
        Dim blnReturn As Boolean
        Dim lngCount As Long
        Dim intRow As Integer
        Dim lstCols As System.Collections.Generic.List(Of Object)
        Dim myDT As DataTable = New DataTable
        Dim myDTView As DataTable = New DataTable
        Dim cCol As New DataColumn

        blnReturn = True

        robjGrid.DataSource = Nothing

        'robjGrid.Clear()
        robjGrid.Rows = 0
        On Error Resume Next
        robjGrid.Rows = 1
        robjGrid.FixedRows = 1

        'MP: Des fois ça plante avec un Cell range invalid...
        On Error Resume Next
        robjGrid.Cols = 0
        On Error GoTo Error_fblnGrid_FillWithDataView_Net

        robjGrid.Cols = 1
        robjGrid.FixedCols = 1

        lngCount = vdataView.Count

        'Set les colonnes
        robjGrid.Cols = vdataView.Table.Columns.Count + 1

        If (Not vblnKeepDisconnectedDataSource) Then
            'robjGrid.DataSource = vdataView

            myDTView = vdataView.ToTable
            ' Binder la grille à la source des données.
            For Each cCol In myDTView.Columns
                myDT.Columns.Add(cCol.ColumnName, GetType(Object))
            Next

            myDT.Merge(myDTView, False, MissingSchemaAction.Ignore)

            robjGrid.DataSource = myDT
        Else
            ' Conserver la grille en mode unbound.            
            For intRow = 0 To vdataView.Count - 1
                lstCols = Nothing
                lstCols = New System.Collections.Generic.List(Of Object)
                lstCols.Add(gvbNullstring)
                lstCols.AddRange(vdataView(intRow).Row.ItemArray)
                robjGrid.AddItem(lstCols.ToArray())
            Next
        End If

        Call fblnGrid_SetForExcell(robjGrid)

        If lngCount > 0 Then
            robjGrid.Row = 1
        Else
            robjGrid.Row = 0
        End If

Exit_fblnGrid_FillWithDataView_Net:
        cCol = Nothing
        myDT = Nothing
        myDTView = Nothing
        lstCols = Nothing
        fblnGrid_FillWithDataView_Net = blnReturn
        Exit Function

Error_fblnGrid_FillWithDataView_Net:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_fblnGrid_FillWithDataView_Net
    End Function

    '****************************************************************
    '* Nom de la fonction   : fblnCloneRecordSet
    '*
    '*               Cree   : 31-03-2010   ***
    '*            Modifie   : **********  ***
    '*
    '*                But   :
    '*
    '* Parametre(s):
    '*              IN :
    '*
    '*              OUT:    True, si succes. False, si erreur
    '*
    '****************************************************************
    Public Function fblnCloneRecordSet(ByRef robjToClone As ADODB.Recordset, ByRef robjCloned As Object) As Boolean
        On Error GoTo Error_fblnCloneRecordSet
        Const strFCT_NAME As String = "fblnCloneRecordSet"
        Dim blnReturn As Boolean
        Dim strFilter As String = IIf(robjToClone.Filter = "0", gvbNullstring, robjToClone.Filter)
        Dim intPos As Integer = robjToClone.AbsolutePosition

        robjToClone.Filter = gvbNullstring
        Dim oStr As New ADODB.Stream()
        robjToClone.Save(oStr)

        robjToClone.Filter = strFilter

        If intPos > 0 Then
            robjToClone.AbsolutePosition = intPos
        End If

        Dim resRecordset As New ADODB.Recordset()

        'ouvrir le stream object dans un nouveau recordset
        resRecordset.Open(oStr, , , robjToClone.LockType)

        'retourne le recordset cloned
        robjCloned = resRecordset

        robjCloned.filter = strFilter

        resRecordset = Nothing
        blnReturn = True

Exit_fblnCloneRecordSet:
        fblnCloneRecordSet = blnReturn
        Exit Function

Error_fblnCloneRecordSet:
        blnReturn = False
        Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        Resume Exit_fblnCloneRecordSet
    End Function

    '****************************************************************
    '* Nom de la fonction   : fblnCheckExecute
    '*
    '*               Cree   : 07-08-2001  Rani
    '*            Modifie   : **-**-****  ****
    '*
    '*                But   :
    '*
    '* Parametre(s):
    '*              IN :
    '*
    '*              OUT:    True, si succes. False, si erreur
    '*
    '****************************************************************
    Public Function fblnCheckExecute(ByRef vlngErrorNumber As Integer, ByRef vintCount As Short, ByVal mcnAdoConnection As ADODB.Connection) As Boolean
        'KF 20080807: Ne pas utiiser le Goto Error car il re-initialise l'objet Err utilisé dans les fonctions en amont
        'On Error GoTo Error_fblnCheckExecute
        'Const strFCT_NAME As String = "fblnCheckExecute"
        Dim blnReturn As Boolean

        If (vlngErrorNumber = mlngDeadLock_Error Or vintCount = 1) And (vintCount <= gintSQLRETRY) Then

            blnReturn = True
        Else
            ' Ré-ouvrir connexion SQL sur perte de connexion.
            ' Le code d'erreur pour un deadlock ou tout autre problème connexion SQL est le même error rocde.
            blnReturn = False
            If (vlngErrorNumber = mlngDeadLock_Error Or vlngErrorNumber = mlngInvalidConnection_Error) Then

                Try
                    GerrToLog("VB6: " & mcnAdoConnection.ConnectionString)
                    If (mcnAdoConnection.State <> ADODB.ObjectStateEnum.adStateClosed) Then
                        mcnAdoConnection.Close()
                    End If
                    mcnAdoConnection.Open()
                    blnReturn = True
                    vlngErrorNumber = 0
                Catch ex As Exception
                    blnReturn = False
                    GerrToLog(ex.ToString())
                End Try
            End If

        End If

Exit_fblnCheckExecute:        
        fblnCheckExecute = blnReturn
        Exit Function

        'Error_fblnCheckExecute:
        '    blnReturn = False
        '    Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        '    Resume Exit_fblnCheckExecute
    End Function

    '****************************************************************
    '* Nom de la fonction   : fblnCheckExecute
    '*
    '*               Cree   : 07-08-2001  Rani
    '*            Modifie   : **-**-****  ****
    '*
    '*                But   :
    '*
    '* Parametre(s):
    '*              IN :
    '*
    '*              OUT:    True, si succes. False, si erreur
    '*
    '****************************************************************
    Public Function fblnCheckExecute(ByRef vlngErrorNumber As Integer, ByRef vintCount As Short, ByVal mcnAdoConnectionNet As SqlConnection) As Boolean
        'KF 20080807: Ne pas utiiser le Goto Error car il re-initialise l'objet Err utilisé dans les fonctions en amont
        'On Error GoTo Error_fblnCheckExecute
        'Const strFCT_NAME As String = "fblnCheckExecute"
        Dim blnReturn As Boolean        

        If (vlngErrorNumber = mlngDeadLock_Error Or vintCount = 1) And (vintCount <= gintSQLRETRY) Then

            blnReturn = True
        Else
            ' Ré-ouvrir connexion SQL sur perte de connexion.
            ' Le code d'erreur pour un deadlock ou tout autre
            blnReturn = False
            If (vlngErrorNumber = mlngDeadLock_Error Or vlngErrorNumber = mlngInvalidConnection_Error) Then

                Try

                    GerrToLog(".NET: " & mcnAdoConnectionNet.ConnectionString)

                    If (mcnAdoConnectionNet.State <> ConnectionState.Closed) Then
                        mcnAdoConnectionNet.Close()
                    End If
                    mcnAdoConnectionNet.Open()
                    blnReturn = True
                    vlngErrorNumber = 0
                Catch ex As Exception
                    blnReturn = False
                    GerrToLog(ex.ToString())
                End Try
            End If
        End If

Exit_fblnCheckExecute:        
        fblnCheckExecute = blnReturn
        Exit Function

        'Error_fblnCheckExecute:
        '    blnReturn = False
        '    Call GerrLogError(Err, mstrModule_NAME, strFCT_NAME)
        '    Resume Exit_fblnCheckExecute
    End Function
End Module
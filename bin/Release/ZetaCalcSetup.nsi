;Include Modern UI

  !include "MUI2.nsh"
  !include "FileAssociation.nsh"

Name "ZetaCalc 1.01"
OutFile "ZetaCalc1.01Setup.exe"
InstallDir "$PROGRAMFILES\Zeta Centauri\ZetaCalc"
InstallDirRegKey HKLM "Software\ZetaCalc" "Install_Dir"
RequestExecutionLevel admin
!define MUI_ICON "Calc.ico"
!define MUI_UNICON "Calc.ico"

;Version Information

  VIProductVersion "1.0.1.0"
  VIAddVersionKey /LANG=${LANG_ENGLISH} "ProductName" "ZetaCalc"
  VIAddVersionKey /LANG=${LANG_ENGLISH} "CompanyName" "Zeta Centauri"
  VIAddVersionKey /LANG=${LANG_ENGLISH} "LegalCopyright" "Copyright 2013 Zeta Centauri"
  VIAddVersionKey /LANG=${LANG_ENGLISH} "FileDescription" "ZetaCalc Installer"
  VIAddVersionKey /LANG=${LANG_ENGLISH} "FileVersion" "1.0.1.0"
  VIAddVersionKey /LANG=${LANG_ENGLISH} "ProductVersion" "1.0.1.0"

;Interface Settings

  !define MUI_ABORTWARNING

;Pages

  !insertmacro MUI_PAGE_LICENSE "License.txt"
  !insertmacro MUI_PAGE_INSTFILES
      !define MUI_FINISHPAGE_NOAUTOCLOSE
      !define MUI_FINISHPAGE_RUN
      !define MUI_FINISHPAGE_RUN_CHECKED
      !define MUI_FINISHPAGE_RUN_TEXT "Launch ZetaCalc"
      !define MUI_FINISHPAGE_RUN_FUNCTION "LaunchProgram"
      !define MUI_FINISHPAGE_SHOWREADME ""
      !define MUI_FINISHPAGE_SHOWREADME_NOTCHECKED
      !define MUI_FINISHPAGE_SHOWREADME_TEXT "Create Desktop Shortcut"
      !define MUI_FINISHPAGE_SHOWREADME_FUNCTION finishpageaction
  !insertmacro MUI_PAGE_FINISH
  
  !insertmacro MUI_UNPAGE_CONFIRM
  !insertmacro MUI_UNPAGE_INSTFILES

;Languages
 
  !insertmacro MUI_LANGUAGE "English"


; The stuff to install
Section "ZetaCalc"

  SectionIn RO
  
  ; Set output path to the installation directory.
  SetOutPath $INSTDIR
  
  ; Put file there
  File "ZetaCalc.exe"
  File "License.txt"
  File "Calc.ico"
  
  ; Write the installation path into the registry
  WriteRegStr HKLM SOFTWARE\ZetaCalc "Install_Dir" "$INSTDIR"
  
  ; Write the uninstall keys for Windows
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\ZetaCalc" "DisplayName" "ZetaCalc"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\ZetaCalc" "DisplayVersion" "1.01"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\ZetaCalc" "Publisher" "Zeta Centauri"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\ZetaCalc" "DisplayIcon" "$INSTDIR\Calc.ico"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\ZetaCalc" "UninstallString" '"$INSTDIR\uninstall.exe"'
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\ZetaCalc" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\ZetaCalc" "NoRepair" 1
  WriteUninstaller "uninstall.exe"

SectionEnd

; Optional section (can be disabled by the user)
Section "Start Menu Shortcuts"

  CreateDirectory "$SMPROGRAMS\Zeta Centauri\ZetaCalc"
  CreateShortCut "$SMPROGRAMS\Zeta Centauri\ZetaCalc\ZetaCalc.lnk" "$INSTDIR\ZetaCalc.exe" "" "" 0
  ;CreateShortCut "$SMPROGRAMS\Zeta Centauri\ZetaCalc\Uninstall.lnk" "$INSTDIR\uninstall.exe" "" "$INSTDIR\uninstall.exe" 0
  WriteINIStr "$SMPROGRAMS\Zeta Centauri\ZetaCalc\ZetaCalc Website.url" "InternetShortcut" "URL" "http://zetacentauri.com/software_zetacalc.htm"
  
SectionEnd

; Uninstaller

Section "Uninstall"
  
  ; Remove registry keys
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\ZetaCalc"
  DeleteRegKey HKLM SOFTWARE\ZetaCalc

  ; Remove files and uninstaller
  Delete $INSTDIR\ZetaCalc.exe
  Delete $INSTDIR\License.txt
  Delete $INSTDIR\uninstall.exe
  Delete $INSTDIR\Calc.ico

  ; Remove shortcuts, if any
  Delete "$SMPROGRAMS\Zeta Centauri\ZetaCalc\*.*"
  Delete "$DESKTOP\ZetaCalc.lnk"
  Delete "$SMPROGRAMS\Zeta Centauri\ZetaCalc\ZetaCalc Website.url"
  ;DeleteINISec "$SMPROGRAMS\Zeta Centauri\ZetaCalc\ZetaCalc Website.url" "InternetShortcut"

  ; Remove directories used
  RMDir "$SMPROGRAMS\Zeta Centauri\ZetaCalc"
  RMDir "$SMPROGRAMS\Zeta Centauri"
  RMDir "$INSTDIR"

SectionEnd

Function LaunchProgram
  ExecShell "" "$SMPROGRAMS\Zeta Centauri\ZetaCalc\ZetaCalc.lnk"
FunctionEnd

Function finishpageaction
  CreateShortcut "$DESKTOP\ZetaCalc.lnk" "$instdir\ZetaCalc.exe"
FunctionEnd
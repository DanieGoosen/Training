REM Instructions:
REM   This bat file copies all the recently modified dlls from your bin folder, and copies it to the 
REM   Sybrin Bin folder in LocalAppData. It also force closes Enterprise Manager + PresentationHost
REM   if found, so that nothing will lock the dlls when trying to copy.

REM * Add this file to the following location: 			C:\Jenkins\
REM * Add the following command to the Post Build Events: 	C:\Jenkins\PostBuildEvents.bat "$(TargetDir)"

SET targetArg=%1
SET destination="%APPDATA%\..\local\Sybrin\Bin"

ECHO .
ECHO Source:			[%targetArg%]
ECHO Destination: 	[%destination%]
ECHO .
ECHO +--------------------------------+
ECHO ¦        Copying Files           ¦
ECHO +--------------------------------+

TASKLIST /FI "IMAGENAME eq PresentationHost.exe" 2>NUL | find /I /N "PresentationHost.exe">NUL
if "%ERRORLEVEL%"=="0" taskkill /f /im PresentationHost.exe

TASKLIST /FI "IMAGENAME eq idmsEMgr.exe" 2>NUL | find /I /N "idmsEMgr.exe">NUL
if "%ERRORLEVEL%"=="0" taskkill /f /im idmsEMgr.exe

TASKLIST /FI "IMAGENAME eq idmsView.exe" 2>NUL | find /I /N "idmsView.exe">NUL
if "%ERRORLEVEL%"=="0" taskkill /f /im idmsView.exe

xcopy /D /Y "%targetArg%*.dll" "%destination%"
if errorlevel 1 goto BuildEventFailed
xcopy /D /Y "%targetArg%*.pdb" "%destination%"
if errorlevel 1 goto BuildEventFailed
xcopy /D /Y "%targetArg%*.exe" "%destination%"
if errorlevel 1 goto BuildEventFailed
 
REM Exit properly because the build will not fail 
REM unless the final step exits with an error code
goto BuildEventOK
:BuildEventFailed
ECHO +--------------------------------+
ECHO ¦            FAILED              ¦
ECHO +--------------------------------+
ECHO .
exit 0
:BuildEventOK
ECHO +--------------------------------+
ECHO ¦           SUCCESS              ¦
ECHO +--------------------------------+
ECHO .
exit 0

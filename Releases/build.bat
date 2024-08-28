@echo off

REM Copy ../Art/icon.png to the current directory
copy /y ..\Art\icon.png .
REM Copy ../Art/manifest.json to the current directory
copy /y ..\Art\manifest.json .
REM Copy ../README.md to the current directory
copy /y ..\README.md .
REM Copy ../CHANGELOG.md to the current directory
copy /y ..\CHANGELOG.md .
REM Copy all files from ../Coroner/build/bin/Debug to the current directory
xcopy /s /y /q ..\CoronerMimics\build\bin\Debug\* .\
REM Copy Strings_* files from ../Coroner to the current directory, excluding Strings_test.xml
xcopy /s /y /q ..\LanguageData\* .\BepInEx\config\EliteMasterEric-Coroner\

REM Create a zip file named Coroner.zip containing all files (except build.bat and Strings_test.xml) in the current directory
"C:\Program Files\7-Zip\7z.exe" a -r CoronerMimics.zip * -x!build.bat -x!CoronerMimics.zip

for %%I in (*) do if not "%%I"=="CoronerMimics.zip" if not "%%I"=="build.bat" del /q "%%I"
for /d %%D in (*) do if not "%%D"=="CoronerMimics.zip" if not "%%D"=="build.bat" rd /s /q "%%D"
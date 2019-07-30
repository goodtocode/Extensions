ECHO OFF
REM Usage: Call SNKCreate.bat GoodToCodeFramework.snk
ECHO Starting SNKCreate.bat GoodToCodeFramework.snk

%WINDIR%\system32\attrib.exe *.* -r /s

..\Utility\SNK\sn.exe –k GoodToCodeFramework.snk

exit 0

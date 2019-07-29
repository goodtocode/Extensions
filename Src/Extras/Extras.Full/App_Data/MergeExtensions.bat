REM ***********************
REM *** Lib file merge ****
REM ***********************
REM To Get ILMerge: PM> Install-Package ilmerge 
REM PostBuildEvent: Call $(ProjectDir)PostBuildMerge.bat
REM Common are: $(TargetPath) = output file, $(TargetDir) = current full bin path, $(OutDir) = current bin path 
REM Notes: /target:exe /targetplatform:v4,C:\Windows\Microsoft.NET\Framework64\v4.0.30319 /wildcards
REM echo %errorlevel%
REM if errorlevel 1 Then echo failed with %errorlevel%

REM ***
REM *** Variables
REM ***
SET BuildLocationBin=..\bin
SET BuildLocationLib=..\lib
SET ILMerge=C:\Projects\Build\Tools\ILMerge\ILMerge.exe

REM ***
REM *** Source DLLs
REM ***
SET ExtensionsUniversal=%BuildLocationBin%\GoodToCode.Extensions.Core.dll
SET ExtensionsFull=%BuildLocationBin%\GoodToCode.Extensions.Standard.dll
SET ExtrasUniversal=%BuildLocationBin%\GoodToCode.Extras.Core.dll
SET ExtrasFull=%BuildLocationBin%\GoodToCode.Extras.Standard.dll

REM ***
REM *** Destination DLLs
REM ***
REM *** Runtimes
SET ExtensionsUniversalLib=%BuildLocationLib%\GoodToCode.Extensions.Core.dll
SET ExtensionsFullLib=%BuildLocationLib%\GoodToCode.Extensions.Standard.dll

REM ***
ECHO *** Create and init build location
REM ***
MD %BuildLocationBin%
MD %BuildLocationLib%
%WINDIR%\system32\attrib.exe %BuildLocationBin%\*.* -r /s
%WINDIR%\system32\attrib.exe %BuildLocationLib%\*.* -r /s
Del %BuildLocationLib%\*.log

REM *** 
ECHO *** Merge Runtimes
REM *** All:  /log: /out: /targetplatform:v4,"%ProgramFiles%\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0" 
REM *** Lib:  /union
REM *** Docs: /XmlDocs /targetplatform:v4,"%ProgramFiles%\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0" 
REM *** Signing: /keyfile:"$(ProjectDir)$(AssemblyOriginatorKeyFile)" 
REM ***
cd %BuildLocationBin%
Del %DestinationUniversal%
Del %DestinationFull%
%ILMerge% /target:DLL /XMLDocs /union /log:%ExtensionsUniversalLib%.log /out:%ExtensionsUniversalLib% %ExtensionsUniversal% %ExtrasUniversal%
Echo Done - %DestinationUniversal%
%ILMerge% /target:DLL /XMLDocs /union /log:%ExtensionsFullLib%.log /out:%ExtensionsFullLib% %ExtensionsUniversal% %ExtrasUniversal% %ExtensionsFull% %ExtrasFull%
Echo Done - %DestinationFull%

Echo *** Merge Complete ***
REM exit 0

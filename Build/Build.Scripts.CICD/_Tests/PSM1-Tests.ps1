﻿#-----------------------------------------------------------------------
# <copyright file="Adhoc-tests.ps1" company="GoodToCode Source">
#      Copyright (c) GoodToCode Source. All rights reserved.
#      All rights are reserved. Reproduction or transmission in whole or in part, in
#      any form or by any means, electronic, mechanical or otherwise, is prohibited
#      without the prior written consent of the copyright owner.
# </copyright>
#-----------------------------------------------------------------------

# ***
# *** Parameters
# ***
param(
	[String]$Path = '\\Dev-Web-01.dev.goodtocode.com', 
	[String]$Build = '\\Dev-Vm-01.dev.goodtocode.com\Vault\Builds\SprintsTest',
	[String]$Domain = 'code.goodtocode.com',
	[String]$Database = 'DatabaseServer.dev.goodtocode.com',
	[String]$SolutionFolder = 'Quick-Start'
)

# ***
# *** Initialize
# ***
Set-ExecutionPolicy Unrestricted -Scope Process -Force
$VerbosePreference = 'SilentlyContinue' # 'Continue'
[String]$ThisScript = $MyInvocation.MyCommand.Path
[String]$ThisDir = Split-Path $ThisScript
Set-Location $ThisDir # Ensure our location is correct, so we can use relative paths
Write-Host "*****************************"
Write-Host "*** Starting: $ThisScript on $(Get-Date -format 'u')"
Write-Host "*****************************"

# Imports
Import-Module "..\..\Build.Scripts.Modules\Code\GoodToCode.Code.psm1"
Import-Module "..\..\Build.Scripts.Modules\System\GoodToCode.System.psm1"

# ***
# *** Execute
# ***
[String] $DevenvExe = Find-DevEnv
&"C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\Common7\IDE\devenv.exe" C:\Source\Framework\3.00-Alpha\Extensions\Extensions.sln /Rebuild
#-Path \\Dev-Vm-01.dev.goodtocode.com\Vault\Builds\Sprints\2018.07\docs.goodtocode.com\GoodToCode-Extensions\src\Extensions.sln 
#Restore-Solution -Path C:\Source\Framework\3.00-Alpha\Extensions\Extensions.sln
﻿#-----------------------------------------------------------------------
# <copyright file="Framework-for-WebAPI-Shopping-Cart.ps1" company="GoodToCode Source">
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
	[String]$Build = '\\Dev-Vm-01.dev.goodtocode.com\Vault\Builds\Sprints',
	[String]$Domain = 'www.goodtocode.com',
	[String]$Database = 'DatabaseServer.dev.goodtocode.com',	
	[String]$ProductName = 'Framework-for-WebAPI',
	[String]$RepoName = 'GoodToCode-Framework',
	[String]$SubFolder = 'docs.goodtocode.com',
	[String]$Relative='..\..\',
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
Import-Module ($Relative + "Build.Scripts.Modules\Code\GoodToCode.Code.psm1")
Import-Module ($Relative + "Build.Scripts.Modules\System\GoodToCode.System.psm1")

# ***
# *** Validate and cleanse
# ***
$Path = Set-Unc -Path $Path
$Build = Set-Unc -Path $Build

# ***
# *** Locals
# ***
$BuildSprint = [String]::Format("{0}\{1}\{2}", $Build, (Get-Date).ToString("yyyy.MM"), $SubFolder)
$BuildFull = [String]::Format("{0}\{1}", $BuildSprint, $ProductName)
$PathFull = [String]::Format("{0}\Sites\{1}\{2}", $Path, $SubFolder, $RepoName)

# ***
Write-Verbose "Builds"
# ***
Copy-SourceCode -Path $BuildFull -RepoName $RepoName -ProductName $ProductName -SolutionFolder $SolutionFolder -Snk GoodToCodeFramework.snk

# ***
Write-Verbose "Zip"
# ***
Remove-Path -Path "$BuildFull\src\Framework.Android"
Remove-Path -Path "$BuildFull\src\Framework.DesktopApp"
Remove-Path -Path "$BuildFull\src\Framework.iOS"
Remove-Path -Path "$BuildFull\src\Framework.UniversalApp.Full"
Remove-Path -Path "$BuildFull\src\Framework.UniversalApp.Core"
Remove-Path -Path "$BuildFull\src\Framework.WebApp.Core"
Remove-Path -Path "$BuildFull\src\Framework.WebApp.Full"
Clear-Solution -Path $BuildFull -Include *.snk, *.log, *.txt, *.bak, *.tmp, *.vspscc, *.vssscc, *.csproj.vspscc, *.sqlproj.vspscc, *.cache, __*.PNG, *.vstemplate
Copy-File -Path ("$BuildSprint\GoodToCode-Extensions.zip") -Destination $BuildFull
Copy-File -Path ("$BuildSprint\GoodToCode-Framework.zip") -Destination $BuildFull
Compress-Solution -Path $BuildFull -Destination $BuildSprint -RepoName $RepoName -File ("GoodToCode-$ProductName.zip")

# ***
Write-Verbose "Publish"
# ***
Copy-File -Path ("$BuildSprint\GoodToCode-$ProductName.zip") -Destination $PathFull

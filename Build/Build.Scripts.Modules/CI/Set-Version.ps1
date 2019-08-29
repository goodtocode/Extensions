#-----------------------------------------------------------------------
# <copyright file="Set-Version.ps1" company="GoodToCode">
#      Copyright (c) GoodToCode. All rights reserved.
#      All rights are reserved. Reproduction or transmission in whole or in part, in
#      any form or by any means, electronic, mechanical or otherwise, is prohibited
#      without the prior written consent of the copyright owner.
# </copyright>
#-----------------------------------------------------------------------

# ***
# *** Parameters
# ***
param
(
	[Parameter(Mandatory=$true,ValueFromPipelineByPropertyName=$true)]
    [string] $Path= $(throw '-Path is a required parameter. $(Build.SourcesDirectory)'),
	[Version] $Version= "4.19"
)

# ***
# *** Initialize
# ***
Set-ExecutionPolicy Unrestricted -Scope Process -Force
$VerbosePreference = 'SilentlyContinue' # 'Continue'
[String]$ThisScript = $MyInvocation.MyCommand.Path
[String]$ThisDir = Split-Path $ThisScript
[DateTime]$Now = Get-Date
Set-Location $ThisDir # Ensure our location is correct, so we can use relative paths
Write-Host "*****************************"
Write-Host "*** Starting: $ThisScript on $Now"
Write-Host "*****************************"
# Imports
Import-Module "..\Code\GoodToCode.Code.psm1"
Import-Module "..\System\GoodToCode.System.psm1"

# ***
# *** Validate and cleanse
# ***
$Path = Set-Unc -Path $Path

# ***
# *** Locals
# ***

# ***
# *** Execute
# ***
$Year = get-date �format yy
[Version]$VersionToReplace = "4.19.01"
[String]$Major = $Version.Major.Replace('-1', '1')
[String]$Minor = $Version.Minor.Replace('-1', $Year)
[String]$Revision = $Version.Revision.Replace('-1', '')
[String]$Build = $Version.Build.Replace('-1', '')

Write-Host "Set-Version -Path $Path -Version $Version"
# .Net Projects
$CsVersion = Get-Version -Major $Major -Minor $Minor -Revision $Revision -Build $Build
Update-ContentsByTag -Path $Path -Value $CsVersion -Open '<version>' -Close '</version>' -Include *.nuspec
Update-LineByContains -Path $Path -Contains "AssemblyVersion(" -Line "[assembly: AssemblyVersion(""$CsVersion"")]" -Include AssemblyInfo.cs
# Vsix Templates
Update-TextByContains -Path $Path -Contains "<Identity Id" -Old $VersionToReplace -New $Version -Include *.vsixmanifest
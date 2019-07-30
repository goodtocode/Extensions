﻿#-----------------------------------------------------------------------
# <copyright file="Framework-Docs.ps1" company="GoodToCode Source">
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
	[String]$Domain = 'docs.goodtocode.com',
	[String]$Database = 'DatabaseServer.dev.goodtocode.com'
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
Write-Host "*** Starting: $ThisScript On: $(Get-Date)"
Write-Host "*****************************"
# Imports
Import-Module "..\..\Build.Scripts.Modules\Code\GoodToCode.Code.psm1"
Import-Module "..\..\Build.Scripts.Modules\System\GoodToCode.System.psm1"

# ***
# *** Validate and cleanse
# ***
$Path = Set-Unc -Path $Path


# ***
# *** Locals
# ***
$Path = Set-Unc -Path $Path
[String]$Solution="..\..\..\Docs\Framework.Docs.sln"
[String]$Source="..\..\..\Docs\Framework.Docs\GoodToCode-Framework"
$Path=[String]::Format("{0}\{1}", $Path, "\Sites\docs.goodtocode.com\Reference\GoodToCode-Framework")

# ***
# *** Execute
# ***
Restore-Solution -Path $Solution
Copy-WebSite -Path $Source -Destination $Path


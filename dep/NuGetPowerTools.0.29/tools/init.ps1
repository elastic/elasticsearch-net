param($installPath, $toolsPath, $package, $project)
Import-Module (Join-Path $toolsPath NuGetPowerTools.psd1)

Write-Host ""
Write-Host "*************************************************************************************"
Write-Host " INSTRUCTIONS"
Write-Host "*************************************************************************************"
Write-Host " - To enable building a package from a project use the Enable-PackageBuild command"
Write-Host " - To enable restoring packages on build use the Enable-PackageRestore command."
Write-Host " - When using one of the above commands, a .nuget folder will been added to your" 
Write-Host "   solution root. Make sure you check it in!"
Write-Host " - For for information, see https://github.com/davidfowl/NuGetPowerTools"
Write-Host "*************************************************************************************"
Write-Host ""
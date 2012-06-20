function Resolve-ProjectName {
    param(
        [parameter(ValueFromPipelineByPropertyName = $true)]
        [string[]]$ProjectName
    )
    
    if($ProjectName) {
        $projects = Get-Project $ProjectName
    }
    else {
        # All projects by default
        $projects = Get-Project -All
    }
    
    $projects
}

function Get-InstallPath {
    param(
        $package
    )
    # Get the repository path
    $componentModel = Get-VSComponentModel
    $repositorySettings = $componentModel.GetService([NuGet.VisualStudio.IRepositorySettings])
    $pathResolver = New-Object NuGet.DefaultPackagePathResolver($repositorySettings.RepositoryPath)
    $pathResolver.GetInstallPath($package)
}

function Ensure-NuGetBuild {
    # Install the nuget command line if it doesn't exist
    $solutionDir = Get-SolutionDir
    $nugetToolsPath = (Join-Path $solutionDir .nuget)
    
    if(!(Test-Path $nugetToolsPath) -or !(Get-ChildItem $nugetToolsPath)) {
        Install-Package NuGet.Build -Source 'https://go.microsoft.com/fwlink/?LinkID=206669'
        
        $nugetBuildPackage = @(Get-Package NuGet.Build)[0]
        $nugetExePackage = @(Get-Package NuGet.CommandLine)[0]
        
        if(!$nugetBuildPackage -and !$nugetExePackage) {
            return $false
        }
        
        # Get the package path
        $nugetBuildPath = Get-InstallPath $nugetBuildPackage
        $nugetExePath = Get-InstallPath $nugetExePackage
        
        if(!(Test-Path $nugetToolsPath)) {
            mkdir $nugetToolsPath | Out-Null
        }

        Write-Host "Copying nuget.exe and msbuild scripts to $nugetToolsPath"

        Copy-Item "$nugetBuildPath\tools\*.*" $nugetToolsPath -Force | Out-Null
        Copy-Item "$nugetExePath\tools\*.*" $nugetToolsPath -Force | Out-Null
        Uninstall-Package NuGet.Build -RemoveDependencies

        Write-Host "Don't forget to commit the .nuget folder"
    }

    return $true
}

function Add-NuGetTargets {
    param(
        [parameter(ValueFromPipelineByPropertyName = $true)]
        [string[]]$ProjectName
    )
    Process {
        if($ProjectName) {
            $projects = Get-Project $ProjectName
        }
        else {
            # All projects by default
            $projects = Get-Project -All
        }

        if(!$projects) {
            Write-Error "Unable to locate project. Make sure it isn't unloaded."
            return
        }
        
        $targetsPath = '$(SolutionDir)\.nuget\NuGet.targets'
        
        $projects | %{ 
            $project = $_
            try {
                 if($project.Type -eq 'Web Site') {
                    Write-Warning "Skipping '$($project.Name)', Website projects are not supported"
                    return
                 }
                 
                 if(!$initialized) {
                    # Make sure the nuget tools exists
                    $initialized = Ensure-NuGetBuild
                 }
                 
                 $project | Add-SolutionDirProperty
                 
                 $buildProject = $project | Get-MSBuildProject
                 if(!($buildProject.Xml.Imports | ?{ $_.Project -eq $targetsPath } )) {
                    $buildProject.Xml.AddImport($targetsPath) | Out-Null
                    $project.Save()
                    $buildProject.ReevaluateIfNecessary()

                    "Updated '$($project.Name)' to use 'NuGet.targets'"
                 }
            }
            catch {
                Write-Warning "Failed to add import 'NuGet.targets' to $($project.Name)"
            }
        }
    }
}

function Enable-PackageRestore {
    param(
        [parameter(ValueFromPipelineByPropertyName = $true)]
        [string[]]$ProjectName
    )
    
    # Add the nuget targets on demand
    Add-NuGetTargets $ProjectName
    
    (Resolve-ProjectName $ProjectName) | %{ 
        $_ | Set-MSBuildProperty RestorePackages true
        "Enabled package restore for $($_.Name)"
    }
}

function Disable-PackageRestore {
    param(
        [parameter(ValueFromPipelineByPropertyName = $true)]
        [string[]]$ProjectName
    )
    (Resolve-ProjectName $ProjectName) | %{ 
        $_ | Set-MSBuildProperty RestorePackages false
        "Disabled package restore for $($_.Name)"
    }
}

function Enable-PackageBuild {
    param(
        [parameter(ValueFromPipelineByPropertyName = $true)]
        [string[]]$ProjectName
    )
    
    # Add the nuget targets on demand
    Add-NuGetTargets $ProjectName
    
    (Resolve-ProjectName $ProjectName) | %{ 
        $_ | Set-MSBuildProperty BuildPackage true
        "Enabled package build for $($_.Name)"
    }
}

function Disable-PackageBuild {
    param(
        [parameter(ValueFromPipelineByPropertyName = $true)]
        [string[]]$ProjectName
    )
    (Resolve-ProjectName $ProjectName) | %{ 
        $_ | Set-MSBuildProperty BuildPackage false
        "Disabled package build for $($_.Name)"
    }
}

# Statement completion for project names
'Enable-PackageRestore', 'Disable-PackageRestore', 'Enable-PackageBuild', 'Disable-PackageBuild' | %{ 
    Register-TabExpansion $_ @{
        ProjectName = { Get-Project -All | Select -ExpandProperty Name }
    }
}

Export-ModuleMember Enable-PackageRestore, Disable-PackageRestore, Enable-PackageBuild, Disable-PackageBuild
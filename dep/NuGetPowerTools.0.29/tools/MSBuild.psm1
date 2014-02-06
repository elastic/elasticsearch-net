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
        $projects = Get-Project
    }
    
    $projects
}

function Get-MSBuildProject {
    param(
        [parameter(ValueFromPipelineByPropertyName = $true)]
        [string[]]$ProjectName
    )
    Process {
        (Resolve-ProjectName $ProjectName) | % {
            $path = $_.FullName
            @([Microsoft.Build.Evaluation.ProjectCollection]::GlobalProjectCollection.GetLoadedProjects($path))[0]
        }
    }
}

function Add-Import {
    param(
        [parameter(Position = 0, Mandatory = $true)]
        [string]$Path,
        [parameter(Position = 1, ValueFromPipelineByPropertyName = $true)]
        [string[]]$ProjectName
    )
    Process {
        (Resolve-ProjectName $ProjectName) | %{
            $buildProject = $_ | Get-MSBuildProject
            $buildProject.Xml.AddImport($Path)
            $_.Save()
        }
    }
}

function Set-MSBuildProperty {
    param(
        [parameter(Position = 0, Mandatory = $true)]
        $PropertyName,
        [parameter(Position = 1, Mandatory = $true)]
        $PropertyValue,
        [parameter(Position = 2, ValueFromPipelineByPropertyName = $true)]
        [string[]]$ProjectName
    )
    Process {
        (Resolve-ProjectName $ProjectName) | %{
            $buildProject = $_ | Get-MSBuildProject
            $buildProject.SetProperty($PropertyName, $PropertyValue) | Out-Null
            $_.Save()
        }
    }
}

function Get-MSBuildProperty {
    param(
        [parameter(Position = 0, Mandatory = $true)]
        $PropertyName,
        [parameter(Position = 2, ValueFromPipelineByPropertyName = $true)]
        [string]$ProjectName
    )
    
    $buildProject = Get-MSBuildProject $ProjectName
    $buildProject.GetProperty($PropertyName)
}

function Add-SolutionDirProperty {  
    param(
        [parameter(ValueFromPipelineByPropertyName = $true)]
        [string[]]$ProjectName
    )
    
    (Resolve-ProjectName $ProjectName) | %{
        $buildProject = $_ | Get-MSBuildProject
        
         if(!($buildProject.Xml.Properties | ?{ $_.Name -eq 'SolutionDir' })) {
            # Get the relative path to the solution
            $relativeSolutionPath = [NuGet.PathUtility]::GetRelativePath($_.FullName, $dte.Solution.Properties.Item("Path").Value)
            $relativeSolutionPath = [IO.Path]::GetDirectoryName($relativeSolutionPath)
            $relativeSolutionPath = [NuGet.PathUtility]::EnsureTrailingSlash($relativeSolutionPath)
            
            $solutionDirProperty = $buildProject.Xml.AddProperty("SolutionDir", $relativeSolutionPath)
            $solutionDirProperty.Condition = '$(SolutionDir) == '''' Or $(SolutionDir) == ''*Undefined*'''
            $_.Save()
         }
     }
}


'Set-MSBuildProperty', 'Add-SolutionDirProperty', 'Add-Import','Add-SolutionDirProperty' | %{ 
    Register-TabExpansion $_ @{
        ProjectName = { Get-Project -All | Select -ExpandProperty Name }
    }
}

Register-TabExpansion 'Get-MSBuildProperty' @{
    ProjectName = { Get-Project -All | Select -ExpandProperty Name }
    PropertyName = {param($context)
        if($context.ProjectName) {
            $buildProject = Get-MSBuildProject $context.ProjectName
        }
        
        if(!$buildProject) {
            $buildProject = Get-MSBuildProject
        }
        
        $buildProject.Xml.Properties | Sort Name | Select -ExpandProperty Name -Unique
    }
}

Export-ModuleMember Get-MSBuildProject, Add-SolutionDirProperty, Add-Import, Get-MSBuildProperty, Set-MSBuildProperty
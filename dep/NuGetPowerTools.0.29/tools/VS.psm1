function Get-SolutionDir {
    if($dte.Solution -and $dte.Solution.IsOpen) {
        return Split-Path $dte.Solution.Properties.Item("Path").Value
    }
    else {
        throw "Solution not avaliable"
    }
}

function Get-ProjectPropertyValue {
    param(
        [parameter(Mandatory = $true)]
        [string]$ProjectName,
        [parameter(Mandatory = $true)]
        [string]$PropertyName
    )    
    try {
        $property = (Get-Project $ProjectName).Properties.Item($PropertyName)
        if($property) {
            return $property.Value
        }
    }
    catch {
    }
    return $null
}

Export-ModuleMember *
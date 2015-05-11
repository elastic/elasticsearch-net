param($installPath, $toolsPath, $package, $project)

# Automatically adds assembly binding redirects to the target project's configuration file
# in order to map the package's dependencies to the assemblies referenced by the project.
Add-BindingRedirect -ProjectName $project.Name

$source = "http://nuget.org/nuget.exe"
$destination = ".\build\tools\nuget\nuget.exe"

$wc = New-Object System.Net.WebClient
$wc.DownloadFile($source, $destination)
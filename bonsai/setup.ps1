if (!(Test-Path "./Bonsai.exe")) {
    Invoke-WebRequest "https://github.com/bonsai-rx/bonsai/releases/download/2.7.2/Bonsai.zip" -OutFile "temp.zip"
    Move-Item -Path "NuGet.config" "temp.config"
    Expand-Archive "temp.zip" -DestinationPath "." -Force
    Move-Item -Path "temp.config" "NuGet.config" -Force
    Remove-Item -Path "temp.zip"
    Remove-Item -Path "Bonsai32.exe"
    Get-ChildItem -Path "Packages" -Recurse -Filter *git2-*.dll |
        Where-Object FullName -NotLike "*win-x86*" |
        Copy-Item -Destination "." -Force
}
& .\Bonsai.exe --no-editor
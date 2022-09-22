if (!(Test-Path "./Bonsai.exe")) {
    Invoke-WebRequest "https://www.myget.org/F/bonsai-boost/api/v2/package/Bonsai/2.7.0-rc3" -OutFile "temp.zip"
    Expand-Archive "temp.zip" -DestinationPath "temp" -Force
    Move-Item -Path "temp/lib/net472/Bonsai.exe" "." -Force
    Remove-Item -Path "temp.zip"
    Remove-Item -Path "temp" -Recurse
}
& .\Bonsai.exe --no-editor
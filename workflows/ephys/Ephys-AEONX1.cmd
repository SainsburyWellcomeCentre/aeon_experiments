:loop
powershell -ep Bypass -c "& ..\..\bonsai\Bonsai.exe --lib $(Resolve-Path ..\onix-refactor\OpenEphys.Onix\OpenEphys.Onix\bin\x64\Release\net472\) Ephys-AEONX1.bonsai %* *>&1 | tee -a Ephys-AEONX1.log"
timeout /t 5
goto :loop
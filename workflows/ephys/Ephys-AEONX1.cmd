:loop
powershell -ep Bypass -c "& '..\..\bonsai\Bonsai.exe' --no-editor Ephys-AEONX1.bonsai *>&1 | tee -a Ephys-AEONX1.log"
timeout /t 20
goto :loop
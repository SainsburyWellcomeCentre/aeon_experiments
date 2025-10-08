:loop
powershell -ep Bypass -c "& ..\..\bonsai\Bonsai.exe Ephys-AEONX1.bonsai %* *>&1 | tee -a Ephys-AEONX1.log"
timeout /t 5
goto :loop
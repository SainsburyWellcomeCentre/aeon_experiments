if (!(Test-Path "./venv")) {
	python.exe -m venv .venv
	.\.venv\Scripts\pip.exe install -r .\requirements.txt
}

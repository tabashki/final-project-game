@echo off
setlocal

:: Define input and output files
set ASEPRITE_FILE=sprites.ase
set OUTPUT_SHEET=spritesheet.png
set OUTPUT_JSON=spritesheet.json

:: Run Aseprite CLI
echo Running Aseprite to export %ASEPRITE_FILE%
aseprite -b "%ASEPRITE_FILE%" ^
  --sheet "%OUTPUT_SHEET%" ^
  --data "%OUTPUT_JSON%" ^
  --format json-array

:: Check for errors
if %ERRORLEVEL% NEQ 0 (
    echo Aseprite export failed!
    exit /b %ERRORLEVEL%
)

echo Aseprite export completed successfully.

endlocal
pause

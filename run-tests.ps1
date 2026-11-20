# SE Lab 9 - run every test project and produce TRX + HTML reports in .\TestResults
# Usage (PowerShell, in this folder):  .\run-tests.ps1        (add -Headless to hide Chrome)
param([switch]$Headless)
if ($Headless) { $env:HEADLESS = "1" }
dotnet restore
dotnet test Lab9Testing.sln --logger "trx" --logger "html" --logger "console;verbosity=normal" --results-directory .\TestResults

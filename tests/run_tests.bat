:: lifted from https://gist.github.com/crankery/b6105d2283cd0eadf85d

@echo off

setlocal EnableDelayedExpansion

if [%1]==[/?] goto :help

:: root is the folder containing this script (without trailing backslash)
set root=%~dp0
set root=%root:~0,-1%

:: put xunit binaries into a folder without versioning in the name
set bin=%root%\xunit

:: set defaults
set resultCode=0
set outputPath=^".\xunit-results.xml^"
set failOnError=0

:: process command line
if not [%1]==[] if not [%1]==[-] set failOnError=%1


:: clear out old bin path
if exist "%bin%" rmdir "%bin%" /s /q
mkdir "%bin%"

echo Copying xunit assemblies
:: Copy the current xunit console runner to the bin folder
for /f "tokens=*" %%a in ('dir /b /s "%root%\..\libraries\Nuget\xunit.runner.console.2.1.0\tools\*"') do (
  copy "%%a" "%bin%" >NUL
)

:: Copy the current xunit exeuction library for .net 4.5 to the bin folder
for /f "tokens=*" %%a in ('dir /b /s  "%root%\..\libraries\Nuget\xunit.extensibility.execution.2.1.0\lib\net45\*"') do (
  copy "%%a" "%bin%" >NUL
)
echo Copy complete

:: x86 Special Cases

echo Discovering test projects
:: Discover test projects
set testAssemblies=
for /f "tokens=*" %%a in ('dir /b /s /a:d "%root%\*.Tests"') do (
  :: copy the execution library into each test library output folder
  copy "%bin%\xunit.execution.dll" "%%a\bin\Release\" >NUL

  :: add this assembly to the list of assemblies (delayed expansion)
  set testAssembly=^"%%a\bin\Release\%%~nxa.dll^"
  
 
  echo "adding assembly !testAssembly!"
  if [!testAssemblies!]==[] (
    set testAssemblies=!testAssembly!
  ) else (
    set testAssemblies=!testAssemblies! !testAssembly!
  )
)

:: run the xunit console runner
echo on

:: 32 bit version
"%bin%\xunit.console.exe" %testAssemblies% -xml %outputPath% -parallel all

@echo off

if /i %failOnError% neq 0 (
  set resultCode=%ERRORLEVEL%
)

exit /b %resultCode%

:help
echo USAGE: %~xn0 [fail-on-error]
echo.
echo Arguments are optional, supply - to use default value.
echo.
echo fail-on-error: report test failures to calling process (0 or 1)
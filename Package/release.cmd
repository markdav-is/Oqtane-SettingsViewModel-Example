del "*.nupkg"
"..\..\oqtane\oqtane.package\nuget.exe" pack Marks.Module.Wiki.nuspec 
XCOPY "*.nupkg" "..\..\oqtane\Oqtane.Server\Packages\" /Y


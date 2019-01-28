nuget pack ..\src\DeployCmsData.Core\DeployCmsData.Core.csproj
nuget pack ..\src\DeployCmsData.UmbracoCms\DeployCmsData.UmbracoCms.csproj

xcopy *.nupkg c:\repos\nuget\ /D /Y
del *.nupkg
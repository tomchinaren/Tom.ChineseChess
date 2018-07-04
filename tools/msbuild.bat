set fdir=%WINDIR%\Microsoft.NET\Framework64

if not exist %fdir% (
	set fdir=%WINDIR%\Microsoft.NET\Framework
)

set msbuild=C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe
%msbuild% ../src/Tom.ChineseChess.Sdk/Tom.ChineseChess.Sdk.csproj /t:Clean;Rebuild /p:Configuration=Release;VisualStudioVersion=12.0;OutputPath="..\..\output\net45\sdk"

pause
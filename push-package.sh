#!/bin/bash -e
PACKAGE_VERSION="9.0.0"
dotnet pack src/NHibernate.AspNetCore.Identity/NHibernate.AspNetCore.Identity.csproj -c Release
dotnet nuget push src/NHibernate.AspNetCore.Identity/bin/Release/NHibernate.AspNetCore.Identity.$PACKAGE_VERSION.nupkg -s nuget.org -k $(cat ~/.nuget/key.txt)
rm src/NHibernate.AspNetCore.Identity/bin/Release/*.nupkg

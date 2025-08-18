#!/bin/bash -e
export PACKAGE_VERSION="9.0.5"
export PACKAGE_RELEASE_NOTES="Update to .NET 9.0.8;Update Identity.Core to 9.0.8;"
export PACKAGE_TAGS="nhibernate, aspnetcore, identity"

PROJECTS=( \
  "src/NHibernate.AspNetCore.Identity/NHibernate.AspNetCore.Identity.csproj" \
)

for PROJ in "${PROJECTS[@]}"
do
  echo "packing $PROJ"
  dotnet pack $PROJ -c Release --output ./nupkgs/
done

dotnet nuget push ./nupkgs/*.nupkg -s nuget.org -k $(cat ~/.nuget/github.txt) \
  --skip-duplicate

rm -rf ./nupkgs

#!/bin/bash -e
# Pack locally to verify the release metadata; pushing is done by the
# publish-nuget workflow through nuget.org Trusted Publishing.
cd "$(dirname "$0")"
source ./release.env

PROJECTS=(
  "src/NHibernate.AspNetCore.Identity/NHibernate.AspNetCore.Identity.csproj"
)

rm -rf ./nupkgs
for PROJ in "${PROJECTS[@]}"; do
  echo "packing $PROJ"
  dotnet pack "$PROJ" -c Release --output ./nupkgs/
done

ls -l ./nupkgs
echo "verify the packages, then tag and push: git tag $PACKAGE_VERSION && git push origin master $PACKAGE_VERSION"

#!/usr/bin/env bash
CODACY_COVERAGE_VERSION="1.0.8"

curl -Ls "https://github.com/codacy/csharp-codacy-coverage/releases/download/$CODACY_COVERAGE_VERSION/Codacy.CSharpCoverage_linux-x64" --output Codacy.CSharpCoverage_linux-x64
chmod +x ./Codacy.CSharpCoverage_linux-x64
./Codacy.CSharpCoverage_linux-x64 -c $REPO_COMMIT_UUID -t $CODACY_PROJECT_TOKEN -r coverage/**/*.xml -e opencover

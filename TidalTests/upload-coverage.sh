#!/usr/bin/env bash
CODACY_COVERAGE_VERSION="1.0.8"
SHORT_HASH=$(git rev-parse --short "$GITHUB_SHA")

curl -Ls "https://github.com/codacy/csharp-codacy-coverage/releases/download/$CODACY_COVERAGE_VERSION/Codacy.CSharpCoverage_linux-x64" --output Codacy.CSharpCoverage_linux-x64
chmod +x ./Codacy.CSharpCoverage_linux-x64
./Codacy.CSharpCoverage_linux-x64 -c $GITHUB_SHA -t $SHORT_HASH -r coverage/**/*.xml -e opencover

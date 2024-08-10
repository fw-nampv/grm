#!/bin/bash
dotnet pack -c Release && 
dotnet tool install --global GitRepoManager --add-source ./nupkg
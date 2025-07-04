#!/bin/bash

# Build script for the Blazor blog
# This script ensures proper content copying and building

echo "🚀 Building Blazor Static Blog..."

# Clean previous builds
echo "🧹 Cleaning previous builds..."
rm -rf bin obj dist wwwroot/content

# Restore dependencies
echo "📦 Restoring dependencies..."
dotnet restore

# Copy content files
echo "📄 Copying content files..."
mkdir -p wwwroot/content
cp -r Content/* wwwroot/content/

# Build the project
echo "🔨 Building project..."
dotnet build -c Release

# Publish for deployment
echo "📦 Publishing for deployment..."
dotnet publish -c Release -o dist

echo "✅ Build complete! Output is in the 'dist' folder."
echo "🌐 To run locally: dotnet run"
echo "📁 To deploy: use the contents of 'dist/wwwroot' folder"

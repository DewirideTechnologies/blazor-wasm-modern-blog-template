#!/bin/bash

# Development script for the Blazor blog
# This script sets up the development environment

echo "💻 Setting up development environment..."

# Copy content files
echo "📄 Copying content files..."
mkdir -p wwwroot/content
cp -r Content/* wwwroot/content/

# Start development server
echo "🚀 Starting development server..."
echo "🌐 Your blog will be available at: http://localhost:5000"
echo "🔄 Press Ctrl+C to stop the server"

dotnet run

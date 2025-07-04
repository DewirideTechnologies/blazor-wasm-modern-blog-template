# Modern Blazor Static Blog

A beautiful, modern static blog built with Blazor WebAssembly and .NET 9. Features markdown/MDX support, dark mode, and premium styling.

## Features

✨ **Modern Design**
- Clean, elegant interface with premium styling
- Responsive layout that works on all devices
- Smooth animations and polished UI components
- Light/dark theme support with system preference detection

🚀 **Blazor WebAssembly**
- Fully client-side application
- No backend required
- Fast, app-like experience
- Static hosting compatible

📝 **Content Management**
- Write posts in Markdown (.md) or MDX (.mdx)
- Support for frontmatter metadata
- File-driven content (no CMS needed)
- Custom component support via shortcodes

🎨 **Theming**
- Automatic dark/light mode
- Respects system preferences
- Theme persistence with localStorage
- CSS custom properties for easy customization

📱 **Responsive & Accessible**
- Mobile-first design
- Touch-friendly interactions
- Accessible markup and navigation
- Progressive Web App ready

## Quick Start

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- A code editor (VS Code recommended)

### Getting Started

1. **Clone or download this template**
   ```bash
   git clone <your-repo-url>
   cd blazor-blog
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Run the development server**
   ```bash
   dotnet run
   ```

4. **Open your browser** to `https://localhost:5001`

### Adding Content

1. **Create a new blog post** in the `Content/` folder:
   ```bash
   touch Content/my-new-post.md
   ```

2. **Add frontmatter and content**:
   ```markdown
   ---
   title: "My Amazing Post"
   slug: "my-amazing-post"
   author: "Your Name"
   publishDate: 2024-01-15T10:00:00Z
   summary: "This is what my post is about"
   tags: ["technology", "blazor"]
   format: "md"
   draft: false
   ---

   # My Amazing Post

   Your content goes here...
   ```

3. **Update the manifest** at `wwwroot/content/manifest.json`:
   ```json
   [
     "welcome-to-my-blog.md",
     "my-new-post.md"
   ]
   ```

4. **Build and see your changes**:
   ```bash
   dotnet build
   dotnet run
   ```

## Content Format

### Frontmatter Fields

- `title`: Post title (required)
- `slug`: URL slug (required, auto-generated if not provided)
- `author`: Author name (required)
- `publishDate`: ISO 8601 date (required)
- `summary`: Brief description (optional but recommended)
- `tags`: Array of tags (optional)
- `format`: "md" or "mdx" (optional, defaults to file extension)
- `category`: Post category (optional)
- `featuredImage`: Path to featured image (optional)
- `draft`: Boolean to hide from public (optional, defaults to false)

### Shortcodes

Use shortcodes to add rich components to your content:

```markdown
[[Alert type="info" title="Note" message="This is an informational alert"]]

[[Callout type="warning" title="Important" content="Pay attention to this"]]

[[Image src="/images/photo.jpg" alt="Description" caption="Photo caption"]]

[[Quote quote="Great things are done by a series of small things brought together." author="Vincent Van Gogh"]]
```

## Deployment

This blog can be deployed to any static hosting platform:

### Azure Static Web Apps

1. **Connect your repository** to Azure Static Web Apps
2. **Use these build settings**:
   - Build command: `dotnet publish -c Release -o dist`
   - App artifact location: `dist/wwwroot`

### GitHub Pages

1. **Enable GitHub Pages** in your repository settings
2. **Add a GitHub Action** for automated deployment:
   ```yaml
   name: Deploy to GitHub Pages
   on:
     push:
       branches: [ main ]
   jobs:
     deploy:
       runs-on: ubuntu-latest
       steps:
         - uses: actions/checkout@v3
         - name: Setup .NET
           uses: actions/setup-dotnet@v3
           with:
             dotnet-version: '9.0.x'
         - name: Publish
           run: dotnet publish -c Release -o dist
         - name: Deploy
           uses: peaceiris/actions-gh-pages@v3
           with:
             github_token: ${{ secrets.GITHUB_TOKEN }}
             publish_dir: dist/wwwroot
   ```

### Netlify

1. **Connect your repository** to Netlify
2. **Use these build settings**:
   - Build command: `dotnet publish -c Release -o dist`
   - Publish directory: `dist/wwwroot`

## Customization

### Styling

The blog uses CSS custom properties for theming. You can customize colors, fonts, and spacing by modifying the CSS variables in `wwwroot/css/app.css`:

```css
:root {
  --primary-color: #3b82f6;    /* Blue */
  --accent-color: #a855f7;     /* Purple */
  --background: #ffffff;       /* White */
  --text-color: #0f172a;       /* Dark gray */
  /* ... more variables */
}
```

### Adding Custom Components

1. **Create a new Blazor component** in `Components/`
2. **Add it to the shortcode processor** in `Services/MarkdownService.cs`
3. **Use it in your content** with shortcode syntax

### Modifying Layout

- **Navigation**: Edit `Components/Layout/NavMenu.razor`
- **Main Layout**: Edit `Components/Layout/MainLayout.razor`
- **Theme Toggle**: Edit `Components/Layout/ThemeToggle.razor`

## Project Structure

```
/
├── Components/           # Blazor components
│   └── Layout/          # Layout components
├── Content/             # Blog post files (*.md, *.mdx)
├── Models/              # Data models
├── Pages/               # Page components
├── Services/            # Application services
├── wwwroot/             # Static web assets
│   ├── css/            # Stylesheets
│   └── content/        # Copied blog content (build output)
├── Blog.csproj         # Project file
└── Program.cs          # Application entry point
```

## Technologies Used

- **Blazor WebAssembly** - Client-side web framework
- **.NET 9** - Runtime and SDK
- **Markdig** - Markdown processing
- **YamlDotNet** - YAML frontmatter parsing
- **Modern CSS** - Responsive design with CSS Grid and custom properties

## License

This project is open source. Feel free to use it as a template for your own blog!

## Contributing

Contributions are welcome! Please feel free to submit issues or pull requests.

---

**Happy blogging!** 🚀

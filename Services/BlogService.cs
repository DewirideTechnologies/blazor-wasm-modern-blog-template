using System.Text.Json;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using Blog.Models;
using System.Text.RegularExpressions;

namespace Blog.Services;

public class BlogService
{
    private readonly HttpClient _httpClient;
    private readonly MarkdownService _markdownService;
    private readonly IDeserializer _yamlDeserializer;
    private List<BlogPost>? _cachedPosts;
    private static readonly Regex FrontMatterRegex = new(@"^---\s*\n(.*?)\n---\s*\n(.*)", RegexOptions.Singleline | RegexOptions.Compiled);

    public BlogService(HttpClient httpClient, MarkdownService markdownService)
    {
        _httpClient = httpClient;
        _markdownService = markdownService;
        _yamlDeserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .IgnoreUnmatchedProperties()
            .Build();
    }

    public async Task<List<BlogPost>> GetAllPostsAsync()
    {
        if (_cachedPosts != null)
            return _cachedPosts;

        _cachedPosts = new List<BlogPost>();

        // Try multiple approaches to find blog posts
        var loadedPosts = new List<BlogPost>();

        // 1. Try to load from manifest file
        try
        {
            var manifestJson = await _httpClient.GetStringAsync("content/manifest.json");
            var manifest = JsonSerializer.Deserialize<List<string>>(manifestJson, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            if (manifest != null)
            {
                foreach (var filePath in manifest)
                {
                    var post = await LoadPostFromFileAsync(filePath);
                    if (post != null && !post.Metadata?.Draft == true)
                    {
                        loadedPosts.Add(post);
                    }
                }
            }
        }
        catch
        {
            // Manifest not available, continue to other methods
        }

        // 2. Try to load known blog post files (fallback)
        if (loadedPosts.Count == 0)
        {
            var knownPosts = new[]
            {
                "welcome-to-my-blog.md",
                "blazor-webassembly-modern-apps.mdx",
                "modern-css-beautiful-interfaces-2024.md",
                "code-overflow-test.md",
                "multi-language-code-examples.md",
                "getting-started.md",
                "hello-world.md",
                "first-post.md"
            };

            foreach (var fileName in knownPosts)
            {
                try
                {
                    var post = await LoadPostFromFileAsync(fileName);
                    if (post != null && !post.Metadata?.Draft == true)
                    {
                        loadedPosts.Add(post);
                    }
                }
                catch
                {
                    // Skip files that don't exist
                }
            }
        }

        _cachedPosts = loadedPosts;

        // Sort by publish date descending
        _cachedPosts = _cachedPosts.OrderByDescending(p => p.PublishDate).ToList();

        return _cachedPosts;
    }

    public async Task<BlogPost?> GetPostBySlugAsync(string slug)
    {
        var posts = await GetAllPostsAsync();
        return posts.FirstOrDefault(p => p.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<List<BlogPost>> GetPostsByTagAsync(string tag)
    {
        var posts = await GetAllPostsAsync();
        return posts.Where(p => p.Tags.Contains(tag, StringComparer.OrdinalIgnoreCase)).ToList();
    }

    public async Task<List<string>> GetAllTagsAsync()
    {
        var posts = await GetAllPostsAsync();
        return posts
            .SelectMany(p => p.Tags)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(tag => tag)
            .ToList();
    }

    private async Task<BlogPost?> LoadPostFromFileAsync(string filePath)
    {
        try
        {
            var fullPath = filePath.StartsWith("content/") ? filePath : $"content/{filePath}";
            var content = await _httpClient.GetStringAsync(fullPath);

            return ParseBlogPost(content, filePath);
        }
        catch
        {
            return null;
        }
    }

    private BlogPost? ParseBlogPost(string fileContent, string filePath)
    {
        var match = FrontMatterRegex.Match(fileContent);

        if (!match.Success)
        {
            // No frontmatter, create a basic post
            return new BlogPost
            {
                Title = Path.GetFileNameWithoutExtension(filePath),
                Slug = CreateSlugFromFileName(filePath),
                Content = fileContent,
                RenderedContent = Path.GetExtension(filePath).ToLower() == ".mdx"
                    ? _markdownService.RenderMdx(fileContent)
                    : _markdownService.RenderMarkdown(fileContent),
                FilePath = filePath,
                Format = Path.GetExtension(filePath).ToLower() == ".mdx" ? "mdx" : "md",
                PublishDate = DateTime.Now,
                Author = "Anonymous"
            };
        }

        var frontMatterYaml = match.Groups[1].Value;
        var markdownContent = match.Groups[2].Value;

        try
        {
            var metadata = _yamlDeserializer.Deserialize<BlogPostMetadata>(frontMatterYaml);

            if (metadata == null)
                return null;

            // Calculate read time
            metadata.ReadTimeMinutes = _markdownService.CalculateReadTime(markdownContent);

            var post = new BlogPost
            {
                Title = metadata.Title,
                Slug = !string.IsNullOrEmpty(metadata.Slug) ? metadata.Slug : CreateSlugFromTitle(metadata.Title),
                Author = metadata.Author,
                PublishDate = metadata.PublishDate,
                Summary = metadata.Summary,
                Tags = metadata.Tags ?? new List<string>(),
                Format = !string.IsNullOrEmpty(metadata.Format) ? metadata.Format :
                        (Path.GetExtension(filePath).ToLower() == ".mdx" ? "mdx" : "md"),
                FilePath = filePath,
                Content = markdownContent,
                RenderedContent = metadata.Format == "mdx" || Path.GetExtension(filePath).ToLower() == ".mdx"
                    ? _markdownService.RenderMdx(markdownContent)
                    : _markdownService.RenderMarkdown(markdownContent),
                Metadata = metadata
            };

            return post;
        }
        catch
        {
            return null;
        }
    }

    private string CreateSlugFromFileName(string filePath)
    {
        var fileName = Path.GetFileNameWithoutExtension(filePath);
        return CreateSlugFromTitle(fileName);
    }

    private string CreateSlugFromTitle(string title)
    {
        if (string.IsNullOrEmpty(title))
            return "untitled";

        var slug = Regex.Replace(title.ToLowerInvariant(), @"[^a-z0-9\s-]", "").Trim();
        slug = Regex.Replace(slug, @"\s+", "-");
        return slug.Trim('-');
    }

    public void ClearCache()
    {
        _cachedPosts = null;
    }
}

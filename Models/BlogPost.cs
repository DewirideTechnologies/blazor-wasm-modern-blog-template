namespace Blog.Models;

public class BlogPost
{
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime PublishDate { get; set; }
    public string Summary { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
    public string Format { get; set; } = "md"; // "md" or "mdx"
    public string FilePath { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string RenderedContent { get; set; } = string.Empty;
    public BlogPostMetadata? Metadata { get; set; }
}

public class BlogPostMetadata
{
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime PublishDate { get; set; }
    public string Summary { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
    public string Format { get; set; } = "md";
    public string FilePath { get; set; } = string.Empty;
    public bool Draft { get; set; } = false;
    public string? FeaturedImage { get; set; }
    public string? Category { get; set; }
    public int ReadTimeMinutes { get; set; }
}

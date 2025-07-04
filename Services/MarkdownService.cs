using Markdig;
using System.Text.RegularExpressions;

namespace Blog.Services;

public class MarkdownService
{
    private readonly MarkdownPipeline _pipeline;
    private static readonly Regex ShortcodeRegex = new(@"\[\[(\w+)(?:\s+([^]]*))?\]\]", RegexOptions.Compiled);

    public MarkdownService()
    {
        _pipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .UseEmojiAndSmiley()
            .Build();
    }
    public string RenderMarkdown(string markdown)
    {
        if (string.IsNullOrEmpty(markdown))
            return string.Empty;

        // Process shortcodes first
        var processedMarkdown = ProcessShortcodes(markdown);

        // Render markdown to HTML
        var html = Markdown.ToHtml(processedMarkdown, _pipeline);

        // Post-process to ensure proper language classes for C# code
        html = PostProcessCodeBlocks(html);

        return html;
    }

    public string RenderMdx(string mdx)
    {
        if (string.IsNullOrEmpty(mdx))
            return string.Empty;

        // For MDX, we'll process it as enhanced markdown with more complex shortcode support
        var processedMdx = ProcessAdvancedShortcodes(mdx);

        // Render as markdown
        var html = Markdown.ToHtml(processedMdx, _pipeline);

        // Post-process to ensure proper language classes
        html = PostProcessCodeBlocks(html);

        return html;
    }

    private string PostProcessCodeBlocks(string html)
    {
        // Debug: Log the original HTML to see what we're working with
        System.Diagnostics.Debug.WriteLine($"Original HTML snippet: {html.Substring(0, Math.Min(500, html.Length))}");

        // First, fix code blocks that already have language classes
        html = System.Text.RegularExpressions.Regex.Replace(html,
            @"<pre><code class=""language-(\w+)""",
            @"<pre class=""language-$1""><code class=""language-$1""",
            RegexOptions.IgnoreCase);

        // Then, fix code blocks without language classes (fallback to text)
        html = System.Text.RegularExpressions.Regex.Replace(html,
            @"<pre><code(?!\s+class=""language-)",
            @"<pre class=""language-text""><code class=""language-text""",
            RegexOptions.IgnoreCase);

        // Debug: Log the processed HTML
        System.Diagnostics.Debug.WriteLine($"Processed HTML snippet: {html.Substring(0, Math.Min(500, html.Length))}");

        return html;
    }

    private string ProcessShortcodes(string content)
    {
        return ShortcodeRegex.Replace(content, match =>
        {
            var componentName = match.Groups[1].Value;
            var attributes = match.Groups[2].Value;

            return componentName.ToLower() switch
            {
                "alert" => ProcessAlertShortcode(attributes),
                "callout" => ProcessCalloutShortcode(attributes),
                "code" => ProcessCodeShortcode(attributes),
                "image" => ProcessImageShortcode(attributes),
                "video" => ProcessVideoShortcode(attributes),
                "quote" => ProcessQuoteShortcode(attributes),
                _ => $"<div class=\"shortcode-placeholder\" data-component=\"{componentName}\" data-attributes=\"{attributes}\">Component: {componentName}</div>"
            };
        });
    }

    private string ProcessAdvancedShortcodes(string content)
    {
        // Enhanced shortcode processing for MDX
        return ProcessShortcodes(content);
    }

    private string ProcessAlertShortcode(string attributes)
    {
        var type = ExtractAttribute(attributes, "type") ?? "info";
        var title = ExtractAttribute(attributes, "title") ?? "";
        var message = ExtractAttribute(attributes, "message") ?? "";

        return $@"
        <div class=""alert alert-{type}"">
            {(string.IsNullOrEmpty(title) ? "" : $"<div class=\"alert-title\">{title}</div>")}
            <div class=""alert-message"">{message}</div>
        </div>";
    }

    private string ProcessCalloutShortcode(string attributes)
    {
        var type = ExtractAttribute(attributes, "type") ?? "note";
        var title = ExtractAttribute(attributes, "title") ?? "";
        var content = ExtractAttribute(attributes, "content") ?? "";

        return $@"
        <div class=""callout callout-{type}"">
            {(string.IsNullOrEmpty(title) ? "" : $"<div class=\"callout-title\">{title}</div>")}
            <div class=""callout-content"">{content}</div>
        </div>";
    }

    private string ProcessCodeShortcode(string attributes)
    {
        var language = ExtractAttribute(attributes, "lang") ?? "text";
        var code = ExtractAttribute(attributes, "code") ?? "";
        var title = ExtractAttribute(attributes, "title") ?? "";

        // Normalize some common language aliases
        language = language.ToLower() switch
        {
            "cs" => "csharp",
            "js" => "javascript",
            "ts" => "typescript",
            "py" => "python",
            "rb" => "ruby",
            "cpp" => "cpp",
            "c++" => "cpp",
            "sh" => "bash",
            "yml" => "yaml",
            _ => language
        };

        return $@"
        <div class=""code-block"">
            {(string.IsNullOrEmpty(title) ? "" : $"<div class=\"code-title\">{title}</div>")}
            <pre class=""language-{language}""><code class=""language-{language}"">{System.Web.HttpUtility.HtmlEncode(code)}</code></pre>
        </div>";
    }

    private string ProcessImageShortcode(string attributes)
    {
        var src = ExtractAttribute(attributes, "src") ?? "";
        var alt = ExtractAttribute(attributes, "alt") ?? "";
        var caption = ExtractAttribute(attributes, "caption") ?? "";
        var width = ExtractAttribute(attributes, "width") ?? "";
        var height = ExtractAttribute(attributes, "height") ?? "";

        var sizeAttributes = "";
        if (!string.IsNullOrEmpty(width)) sizeAttributes += $" width=\"{width}\"";
        if (!string.IsNullOrEmpty(height)) sizeAttributes += $" height=\"{height}\"";

        return $@"
        <figure class=""image-figure"">
            <img src=""{src}"" alt=""{alt}""{sizeAttributes} class=""responsive-image"" />
            {(string.IsNullOrEmpty(caption) ? "" : $"<figcaption>{caption}</figcaption>")}
        </figure>";
    }

    private string ProcessVideoShortcode(string attributes)
    {
        var src = ExtractAttribute(attributes, "src") ?? "";
        var poster = ExtractAttribute(attributes, "poster") ?? "";
        var caption = ExtractAttribute(attributes, "caption") ?? "";

        return $@"
        <figure class=""video-figure"">
            <video controls class=""responsive-video"" {(string.IsNullOrEmpty(poster) ? "" : $"poster=\"{poster}\"")}>
                <source src=""{src}"" />
                Your browser does not support the video tag.
            </video>
            {(string.IsNullOrEmpty(caption) ? "" : $"<figcaption>{caption}</figcaption>")}
        </figure>";
    }

    private string ProcessQuoteShortcode(string attributes)
    {
        var quote = ExtractAttribute(attributes, "quote") ?? "";
        var author = ExtractAttribute(attributes, "author") ?? "";
        var source = ExtractAttribute(attributes, "source") ?? "";

        return $@"
        <blockquote class=""enhanced-quote"">
            <p>{quote}</p>
            {(string.IsNullOrEmpty(author) ? "" : $"<cite>— {author}{(string.IsNullOrEmpty(source) ? "" : $", {source}")}</cite>")}
        </blockquote>";
    }

    private string? ExtractAttribute(string attributes, string attributeName)
    {
        if (string.IsNullOrEmpty(attributes))
            return null;

        var regex = new Regex($@"{attributeName}=""([^""]*)""|{attributeName}='([^']*)'|{attributeName}=(\S+)", RegexOptions.IgnoreCase);
        var match = regex.Match(attributes);

        if (match.Success)
        {
            return match.Groups[1].Value ?? match.Groups[2].Value ?? match.Groups[3].Value;
        }

        return null;
    }

    public int CalculateReadTime(string content)
    {
        if (string.IsNullOrEmpty(content))
            return 0;

        // Remove HTML tags and markdown syntax for word count
        var plainText = Regex.Replace(content, @"<[^>]+>", "");
        plainText = Regex.Replace(plainText, @"[#*`_\[\]()]+", "");

        var words = plainText.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        var wordCount = words.Length;

        // Average reading speed is 200 words per minute
        return Math.Max(1, (int)Math.Ceiling(wordCount / 200.0));
    }
}

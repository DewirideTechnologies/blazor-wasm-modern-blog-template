---
title: Multi-Language Code Examples
description: Demonstrating syntax highlighting for various programming languages
date: 2024-01-15
tags: [programming, examples, syntax-highlighting]
---

# Multi-Language Code Examples

This blog post demonstrates syntax highlighting for various programming languages. Simply specify the language after the opening triple backticks.

## C# Example

```csharp
public class BlogPost
{
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public string GetFormattedDate()
    {
        return CreatedAt.ToString("MMMM dd, yyyy");
    }
}
```

## JavaScript Example

```javascript
const blogPost = {
    title: "My Blog Post",
    createdAt: new Date(),
    
    getFormattedDate() {
        return this.createdAt.toLocaleDateString('en-US', {
            year: 'numeric',
            month: 'long',
            day: 'numeric'
        });
    }
};
```

## Python Example

```python
from datetime import datetime

class BlogPost:
    def __init__(self, title, created_at=None):
        self.title = title
        self.created_at = created_at or datetime.now()
    
    def get_formatted_date(self):
        return self.created_at.strftime("%B %d, %Y")
```

## TypeScript Example

```typescript
interface IBlogPost {
    title: string;
    createdAt: Date;
    getFormattedDate(): string;
}

class BlogPost implements IBlogPost {
    constructor(public title: string, public createdAt: Date = new Date()) {}
    
    getFormattedDate(): string {
        return this.createdAt.toLocaleDateString('en-US', {
            year: 'numeric',
            month: 'long',
            day: 'numeric'
        });
    }
}
```

## SQL Example

```sql
CREATE TABLE BlogPosts (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(255) NOT NULL,
    Content NTEXT,
    CreatedAt DATETIME2 DEFAULT GETDATE()
);

SELECT Title, CreatedAt 
FROM BlogPosts 
WHERE CreatedAt >= DATEADD(day, -30, GETDATE())
ORDER BY CreatedAt DESC;
```

## HTML Example

```html
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>My Blog</title>
</head>
<body>
    <header>
        <h1>Welcome to My Blog</h1>
    </header>
    <main>
        <article>
            <h2>Latest Post</h2>
            <p>This is my latest blog post content.</p>
        </article>
    </main>
</body>
</html>
```

## JSON Example

```json
{
    "blogPost": {
        "title": "My Blog Post",
        "author": "John Doe",
        "tags": ["programming", "web", "development"],
        "publishedAt": "2024-01-15T10:00:00Z",
        "content": "This is the content of my blog post...",
        "metadata": {
            "wordCount": 150,
            "readingTime": "2 minutes"
        }
    }
}
```

## Bash Example

```bash
#!/bin/bash

# Deploy blog to production
echo "Starting blog deployment..."

# Build the project
dotnet build --configuration Release

# Run tests
dotnet test

# Deploy to server
rsync -avz ./bin/Release/ user@server:/var/www/blog/

echo "Blog deployment completed!"
```

## Go Example

```go
package main

import (
    "fmt"
    "time"
)

type BlogPost struct {
    Title     string
    CreatedAt time.Time
}

func (bp *BlogPost) GetFormattedDate() string {
    return bp.CreatedAt.Format("January 2, 2006")
}

func main() {
    post := &BlogPost{
        Title:     "My Go Blog Post",
        CreatedAt: time.Now(),
    }
    
    fmt.Printf("Post: %s, Date: %s\n", post.Title, post.GetFormattedDate())
}
```

## Rust Example

```rust
use chrono::{DateTime, Utc};

struct BlogPost {
    title: String,
    created_at: DateTime<Utc>,
}

impl BlogPost {
    fn new(title: String) -> Self {
        BlogPost {
            title,
            created_at: Utc::now(),
        }
    }
    
    fn get_formatted_date(&self) -> String {
        self.created_at.format("%B %d, %Y").to_string()
    }
}

fn main() {
    let post = BlogPost::new("My Rust Blog Post".to_string());
    println!("Post: {}, Date: {}", post.title, post.get_formatted_date());
}
```

## Usage Instructions

To use syntax highlighting in your blog posts:

1. Use triple backticks (```) to start a code block
2. Specify the language immediately after the opening backticks
3. Write your code
4. Close with triple backticks

**Supported languages include:**
- `csharp` or `cs` - C# 
- `javascript` or `js` - JavaScript
- `typescript` or `ts` - TypeScript
- `python` or `py` - Python
- `java` - Java
- `cpp` or `c++` - C++
- `c` - C
- `go` - Go
- `rust` - Rust
- `php` - PHP
- `ruby` or `rb` - Ruby
- `kotlin` - Kotlin
- `swift` - Swift
- `html` - HTML
- `css` - CSS
- `json` - JSON
- `xml` - XML
- `sql` - SQL
- `bash` or `sh` - Bash/Shell
- `powershell` - PowerShell
- `yaml` or `yml` - YAML
- `markdown` or `md` - Markdown
- `docker` - Dockerfile
- `git` - Git

The syntax highlighting will automatically detect the language and apply appropriate styling for both light and dark themes.

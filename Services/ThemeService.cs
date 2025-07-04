using Microsoft.JSInterop;

namespace Blog.Services;

public class ThemeService
{
    private readonly IJSRuntime _jsRuntime;
    private string _currentTheme = "system";
    
    public event Action? OnThemeChanged;

    public ThemeService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public string CurrentTheme => _currentTheme;

    public async Task InitializeAsync()
    {
        try
        {
            var savedTheme = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "theme");
            _currentTheme = string.IsNullOrEmpty(savedTheme) ? "system" : savedTheme;
            await ApplyThemeAsync(_currentTheme);
        }
        catch
        {
            _currentTheme = "system";
            await ApplyThemeAsync(_currentTheme);
        }
    }

    public async Task SetThemeAsync(string theme)
    {
        _currentTheme = theme;
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "theme", theme);
        await ApplyThemeAsync(theme);
        OnThemeChanged?.Invoke();
    }

    public async Task ToggleThemeAsync()
    {
        var newTheme = _currentTheme switch
        {
            "light" => "dark",
            "dark" => "system",
            _ => "light"
        };
        await SetThemeAsync(newTheme);
    }

    private async Task ApplyThemeAsync(string theme)
    {
        var script = theme switch
        {
            "dark" => "document.documentElement.classList.remove('light'); document.documentElement.classList.add('dark');",
            "light" => "document.documentElement.classList.remove('dark'); document.documentElement.classList.add('light');",
            _ => @"
                document.documentElement.classList.remove('light', 'dark');
                if (window.matchMedia('(prefers-color-scheme: dark)').matches) {
                    document.documentElement.classList.add('dark');
                } else {
                    document.documentElement.classList.add('light');
                }"
        };

        try
        {
            await _jsRuntime.InvokeVoidAsync("eval", script);
        }
        catch
        {
            // Fallback for initialization
        }
    }
}

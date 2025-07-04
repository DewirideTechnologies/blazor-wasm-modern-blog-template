---
title: "Code Block Overflow Test"
summary: "Testing how code blocks handle overflow in different container widths"
publishDate: "2024-01-04"
author: "Blog Admin"
tags: ["code", "testing", "css"]
---

# Code Block Overflow Test

This post is designed to test how code blocks handle overflow when they contain long lines of code.

## JavaScript Example

Here's a JavaScript function with a very long line that should scroll horizontally:

```javascript
function processDataWithVeryLongFunctionNameAndManyParameters(firstParameter, secondParameter, thirdParameter, fourthParameter, fifthParameter, sixthParameter, seventhParameter, eighthParameter, ninthParameter, tenthParameter) {
    const veryLongVariableName = "This is a very long string that demonstrates how horizontal scrolling works in code blocks when the content exceeds the container width";
    
    // This comment is also very long and should demonstrate horizontal scrolling: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua
    
    return {
        result: veryLongVariableName,
        processed: true,
        timestamp: Date.now(),
        metadata: {
            originalLength: veryLongVariableName.length,
            processingTime: performance.now(),
            additionalInfo: "This object contains properties with very long names to test horizontal scrolling"
        }
    };
}
```

## CSS Example

Here's a CSS rule with long property values:

```css
.very-long-selector-name-that-demonstrates-horizontal-scrolling {
    background: linear-gradient(135deg, rgba(255, 255, 255, 0.1) 0%, rgba(255, 255, 255, 0.05) 50%, rgba(255, 255, 255, 0.1) 100%);
    box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1), 0 1px 3px rgba(0, 0, 0, 0.1), inset 0 1px 0 rgba(255, 255, 255, 0.1);
    transform: perspective(1000px) rotateX(0deg) rotateY(0deg) rotateZ(0deg) translateX(0px) translateY(0px) translateZ(0px) scale(1);
}
```

## HTML Example

Here's an HTML element with many attributes:

```html
<div class="container component-wrapper data-visualization-container" id="main-content-area" data-testid="primary-container" data-analytics="user-interaction-tracking" data-component="data-visualization" data-theme="dark-mode" data-responsive="true" data-accessibility="enhanced" role="main" aria-labelledby="main-heading" aria-describedby="main-description">
    <p>This is a very long paragraph that demonstrates how HTML content behaves within code blocks when it exceeds the normal width constraints of the container.</p>
</div>
```

## Terminal Command Example

Here's a long terminal command:

```bash
curl -X POST "https://api.example.com/v1/data/process" \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c" \
  -d '{"data": {"items": [{"id": 1, "name": "Very long item name that demonstrates horizontal scrolling", "description": "This is a very long description that should wrap or scroll depending on the container"}]}}'
```

## Inline Code Test

Here's some inline code that's quite long: `const veryLongVariableName = "This is a very long string in inline code that should break properly"`.

## Multiple Code Blocks

Sometimes we have multiple code blocks in sequence:

```python
def very_long_function_name_that_demonstrates_horizontal_scrolling(parameter_one, parameter_two, parameter_three, parameter_four, parameter_five):
    """
    This is a very long docstring that explains what this function does in great detail and should demonstrate how text wrapping works in code blocks.
    """
    very_long_variable_name = "This is a very long string that demonstrates horizontal scrolling in Python code blocks"
    return very_long_variable_name
```

```json
{
  "veryLongPropertyName": "This is a very long JSON value that demonstrates horizontal scrolling in JSON code blocks",
  "anotherVeryLongPropertyName": {
    "nestedProperty": "This nested property also has a very long value to test horizontal scrolling behavior",
    "arrayProperty": [
      "This is the first item in an array with a very long string value",
      "This is the second item in an array with another very long string value"
    ]
  }
}
```

This should test various scenarios where code blocks might exceed the container width.

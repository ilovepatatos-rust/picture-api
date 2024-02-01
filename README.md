# Picture Framework
Picture framework exposes an api to draw text above images. It can be hosted via C# or within a Docker.

## How to use
1. Fonts must be within the `fonts` folder.
2. Api is hosted on the port 5000 by default.
3. The api accepts a POST request with a JSON body.

### JSON Format
```json
{
  "Background image url": "",
  "Text overlays": [
    {
      "Text": "",
      "Size": 24,
      "Font": "Arial.ttf",
      "Hex": "#FFFFFF",
      "Horizontal alignment [ LEFT | CENTER | RIGHT ]": "LEFT",
      "Vertical alignment [ TOP | CENTER | BOTTOM ]": "CENTERED",
      "Offset": {
        "X": 0.0,
        "Y": 0.0
      }
    }
  ],
  "Image overlays": [
    {
      "Image url": "",
      "Offset": {
        "X": 0.0,
        "Y": 0.0
      },
      "Size": "100, 100"
    }
  ]
}
```

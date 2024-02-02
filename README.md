# Dotentile 
Dotentile is GDAL-based library to convert lat long data into XYZ tiles
and vice versa.

Inspired by [mercantile](https://pypi.org/project/mercantile/)


## Examples

Converting lat lot with predefined zoom level to XYZ tiles
```csharp
using Dotentile;

var lon = 39.344371f;
var lat = 72.878013f;
var zoom = 14;

var xyz = Translation.XYZFromLatLon(lat, lon, zoom);
var expected = new List<int> { 11508, 6241, 14 };

```

Converting XYZ tiles to lat long
```csharp
var x = 11508;
var y = 6241;
var z = 14;

var latLon = Translation.LatLonFromXYZ(x, y, z);
var expected = new List<float> { 72.878013f, 39.344371f };
```

Getting all XYZ tiles which intersect Geojson polygon
```csharp
var geoJson = "{\"type\": \"Polygon\", \"coordinates\": [[[-180, 85], [180, 85], [180, -85], [-180, -85], [-180, 85]]]}";
var zoom = 14;
var tiles = Translation.TilesFromGeojson(geoJson, zoom);
```
using Dotentile;

namespace Dotentile.Tests;

public class TestTranslation
{
    [Fact]
    public void TestTranslationToXyz()
    {
        var lat = 39.344722f;
        var lon = 72.8761353f;
        var zoom = 14;

        var xyz = Translation.XYZFromLatLon(lat, lon, zoom);
        var expected = new List<int> { 11508, 6241, 14 };
        Assert.Equal(expected, xyz);
    }
    
    [Fact]
    public void TestTranslationToLatLon()
    {
        var x = 11508;
        var y = 6241;
        var z = 14;

        var latLon = Translation.LatLonFromXYZ(x, y, z);
        var expected = new Coordinate(72.8613281f, 39.3512917f);
        Assert.Equal(expected, latLon);
    }
    
    [Fact]
    public void TestTranslationToGeoJson()
    {
        var tile = new XYZTile(11508, 6241, 14);
        var geoJson = Translation.XYZAsGeoJson(tile);
        var expected = "{ \"type\": \"Point\", \"coordinates\": [39.35129, 72.86133] }";
        Assert.Equal(expected, geoJson);
    }
    
    [Fact]
    public void TestTilesFromGeoJson()
    {
        var geoJson = "{\"type\": \"Polygon\", \"coordinates\": [[[-180, 85], [180, 85], [180, -85], [-180, -85], [-180, 85]]]}";
        var zoom = 14;
        var tiles = Translation.TilesFromGeojson(geoJson, zoom);
        var expected = new List<XYZTile>
        {
            new XYZTile(11508, 6241, 14),
            new XYZTile(11508, 6242, 14),
            new XYZTile(11509, 6241, 14),
            new XYZTile(11509, 6242, 14)
        };
        Assert.Equal(expected, tiles);
    }
    
    [Fact]
    public void TestExtentOfXYZ()
    {
        var x = 11508;
        var y = 6241;
        var z = 14;
        var extent = Translation.ExtentOfXYZ(x, y, z);
        var expected = new Extent(39.3512917f, 72.8613281f, 39.344722f, 72.8761353f);
        Assert.Equal(expected, extent);
    }
}
using Dotentile;
using MaxRev.Gdal.Core;

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
        var expected = new Coordinate(39.3512917f, 72.8613281f);
        Assert.Equal(expected, latLon);
    }
    
    [Fact]
    public void TestTranslationToGeoJson()
    {
        var tile = new XYZTile(11508, 6241, 14);
        var geoJson = Translation.XYZAsGeoJson(tile);
        var expected = "{ \"type\": \"Point\", \"coordinates\": [72.86133, 39.35129] }";
        Assert.Equal(expected, geoJson);
    }
    
    [Fact]
    public void TestTilesFromGeoJson()
    {
        GdalBase.ConfigureAll();
        var filename = "lenin_peak_zone.geojson";
        var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
        var geoJson = File.ReadAllText(filepath);
        geoJson = geoJson.Replace("\n", "");
        var zoom = 14;
        var tiles = Translation.TilesFromGeojson(geoJson, zoom);
        var expected = new List<XYZTile>
        {
            new XYZTile(11505, 6238, 14),
            new XYZTile(11505, 6239, 14),
            new XYZTile(11505, 6240, 14),
            new XYZTile(11505, 6241, 14),
            new XYZTile(11505, 6242, 14),
            new XYZTile(11505, 6243, 14),
            new XYZTile(11505, 6244, 14),
            new XYZTile(11506, 6238, 14),
            new XYZTile(11506, 6239, 14),
            new XYZTile(11506, 6240, 14),
            new XYZTile(11506, 6241, 14),
            new XYZTile(11506, 6242, 14),
            new XYZTile(11506, 6243, 14),
            new XYZTile(11506, 6244, 14),
            new XYZTile(11507, 6238, 14),
            new XYZTile(11507, 6239, 14),
            new XYZTile(11507, 6240, 14),
            new XYZTile(11507, 6241, 14),
            new XYZTile(11507, 6242, 14),
            new XYZTile(11507, 6243, 14),
            new XYZTile(11507, 6244, 14),
            new XYZTile(11508, 6238, 14),
            new XYZTile(11508, 6239, 14),
            new XYZTile(11508, 6240, 14),
            new XYZTile(11508, 6241, 14),
            new XYZTile(11508, 6242, 14),
            new XYZTile(11508, 6243, 14),
            new XYZTile(11508, 6244, 14),
            new XYZTile(11509, 6238, 14),
            new XYZTile(11509, 6239, 14),
            new XYZTile(11509, 6240, 14),
            new XYZTile(11509, 6241, 14),
            new XYZTile(11509, 6242, 14),
            new XYZTile(11509, 6243, 14),
            new XYZTile(11509, 6244, 14),
            new XYZTile(11510, 6238, 14),
            new XYZTile(11510, 6239, 14),
            new XYZTile(11510, 6240, 14),
            new XYZTile(11510, 6241, 14),
            new XYZTile(11510, 6242, 14),
            new XYZTile(11510, 6243, 14),
            new XYZTile(11510, 6244, 14),
            new XYZTile(11511, 6238, 14),
            new XYZTile(11511, 6239, 14),
            new XYZTile(11511, 6240, 14),
            new XYZTile(11511, 6241, 14),
            new XYZTile(11511, 6242, 14),
            new XYZTile(11511, 6243, 14),
            new XYZTile(11511, 6244, 14),
            new XYZTile(11512, 6238, 14),
            new XYZTile(11512, 6239, 14),
            new XYZTile(11512, 6240, 14),
            new XYZTile(11512, 6241, 14),
            new XYZTile(11512, 6242, 14),
            new XYZTile(11512, 6243, 14),
            new XYZTile(11512, 6244, 14),
            new XYZTile(11513, 6238, 14),
            new XYZTile(11513, 6239, 14),
            new XYZTile(11513, 6240, 14),
            new XYZTile(11513, 6241, 14),
            new XYZTile(11513, 6242, 14),
            new XYZTile(11513, 6243, 14),
            new XYZTile(11513, 6244, 14),
            new XYZTile(11514, 6238, 14),
            new XYZTile(11514, 6239, 14),
            new XYZTile(11514, 6240, 14),
            new XYZTile(11514, 6241, 14),
            new XYZTile(11514, 6242, 14),
            new XYZTile(11514, 6243, 14),
            new XYZTile(11514, 6244, 14),
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
        var expected = new Extent(39.3512917f, 72.8613281f, 39.334297f, 72.8833f);
        Assert.Equal(expected, extent);
    }
}


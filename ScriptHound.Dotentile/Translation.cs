using OSGeo.OGR;

namespace Dotentile;

public struct XYZTile
{
    public XYZTile(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    public int X { get; }
    public int Y { get; }
    
    public int Z { get; }

    public override string ToString() => $"({X}, {Y}, {Z})";
}

public class Translation
{
    public static List<int> XYZFromLatLon(float lat, float lon, float zoom)
    {
        var n = (float)Math.Pow(2, zoom);
        var x = (int)(n * ((lon + 180) / 360));
        var y = (int)(n * (1 - (Math.Log(Math.Tan(lat * Math.PI / 180) + 1 / Math.Cos(lat * Math.PI / 180)) / Math.PI)) / 2);
        return new List<int> { x, y, (int)zoom };
    }

    public static List<float> LatLonFromXYZ(int x, int y, int z)
    {
        float lat = 0;
        float lon = 0;
        
        float p = (float)Math.Sqrt(x * x + y * y);
        lat = (float)Math.Atan2(z, p * (1.0f - 0.00669438f));
        float n = (float)Math.Atan2(z + 0.00669438f * 6378137.0f * (float)Math.Sin(lat), p);
        lon = (float)Math.Atan2(y, x);

        lat = lat * 180 / (float)Math.PI;
        lon = lon * 180 / (float)Math.PI;

        return new List<float> { lat, lon };
    }

    public static string XYZAsGeoJson(XYZTile tile)
    {
        var latLon = LatLonFromXYZ(tile.X, tile.Y, tile.Z);
        return $"{{ \"type\": \"Point\", \"coordinates\": [{latLon[1]}, {latLon[0]}] }}";
    }

    public static List<XYZTile> TilesFromGeojson(string geojson, int zoom)
    {
        var tiles = new List<XYZTile>();
        var geometry = Ogr.CreateGeometryFromJson(geojson);
        var extent = geometry.Boundary();
        var topLeft = XYZFromLatLon((float)extent.GetY(3), (float)extent.GetX(0), zoom);
        var bottomRight = XYZFromLatLon((float)extent.GetY(0), (float)extent.GetX(2), zoom);
        for (int x = topLeft[0]; x <= bottomRight[0]; x++)
        {
            for (int y = topLeft[1]; y <= bottomRight[1]; y++)
            {
                var tilePoint = Geometry.CreateFromWkt($"POINT({x} {y})");
                var tileIntersects = tilePoint.Intersects(geometry);
                if (tileIntersects)
                {
                    tiles.Add(new XYZTile(x, y, zoom));
                }
            }
        }

        return tiles;
    }
}

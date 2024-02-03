﻿using OSGeo.OGR;

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
    public static List<int> XYZFromLatLon(float lat, float lon, int zoom)
    {
        var x = (int)((lon + 180.0) / 360.0 * (1 << zoom));
        var y = (int)((1.0 - Math.Log(Math.Tan(lat * Math.PI / 180.0) + 1.0 / Math.Cos(lat * Math.PI / 180.0)) / Math.PI) / 2.0 * (1 << zoom));
        return new List<int> { x, y, zoom };
    }

    // <summary>
    // Method <c>LatLonFromXYZ</c> returns upper right corner of XYZ tile
    // </summary>
    public static List<float> LatLonFromXYZ(int x, int y, int z)
    {
        double n = Math.PI - ((2.0 * Math.PI * y) / Math.Pow(2.0, z));

        var lat = (float)((x / Math.Pow(2.0, z) * 360.0) - 180.0);
        var lon = (float)(180.0 / Math.PI * Math.Atan(Math.Sinh(n)));

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

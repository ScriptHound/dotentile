namespace Dotentile;


public struct Coordinate
{
    public Coordinate(float lat, float lon)
    {
        Lat = lat;
        Lon = lon;
    }
    public float Lat { get; }
    public float Lon { get; }
    
    public override string ToString() => $"({Lat}, {Lon})";
    
    public override bool Equals(object? obj)
    {
        if (obj == null || !(obj is Coordinate))
        {
            return false;
        }
        else
        {
            bool latIsEqual = this.Lat == ((Coordinate) obj).Lat;
            bool lonIsEqual = this.Lon == ((Coordinate) obj).Lon;
            return lonIsEqual && latIsEqual;
        }
            
    }
}

public struct Extent
{
    public Extent(float nelat, float nelon, float swlat, float swlon)
    {
        NElat = nelat;
        NElon = nelon;
        SWlat = swlat;
        SWlon = swlon;
    }
    public float NElat { get; }
    public float NElon { get; }
    public float SWlat { get; }
    public float SWlon { get; }
    public override string ToString() => $"NE: ({NElat}, {NElon}), SW: ({SWlat}, {SWlon})";
}

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

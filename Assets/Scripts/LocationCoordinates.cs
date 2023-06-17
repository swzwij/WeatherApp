public class LocationCoordinates
{
    private double _latitude;

    private double _longitude;

    public double Latitude => _latitude;

    public double Longitude => _longitude;  

    public LocationCoordinates(double latitude, double longitude)
    {
        this._latitude = latitude;
        this._longitude = longitude;
    }
}
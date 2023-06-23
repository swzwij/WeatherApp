namespace WeatherApp.API
{
    public class LocationRequest : APIRequest
    {
        private readonly double _latitude;

        private readonly double _longitude;

        public override string URL => $"https://geocode.maps.co/reverse?lat={_latitude}&lon={_longitude}";

        public LocationRequest(double latitude, double longitude)
        {
            _latitude = latitude;
            _longitude = longitude;
        }
    }
}

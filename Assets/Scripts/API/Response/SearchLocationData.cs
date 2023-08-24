using System.Collections.Generic;

namespace WeatherApp.API
{
    public class SearchLocationData
    {
        public Location[] locations;
    }

    [System.Serializable]
    public class Location
    {
        public int place_id;
        public string licence;
        public string powered_by;
        public string osm_type;
        public int osm_id;
        public string[] boundingbox;
        public string lat;
        public string lon;
        public string display_name;
        public string @class;
        public string type;
        public float importance;
    }
}
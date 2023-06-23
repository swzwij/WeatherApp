namespace WeatherApp.API
{
    /// <summary>
    /// Class to handle the location data.
    /// </summary>
    public class LocationData
    {
        /// <summary>
        /// The ID of the place.
        /// </summary>
        public int place_id;

        /// <summary>
        /// The license information.
        /// </summary>
        public string licence;

        /// <summary>
        /// The source or provider of the location data.
        /// </summary>
        public string powered_by;

        /// <summary>
        /// The type of OpenStreetMap object. 
        /// </summary>
        public string osm_type;

        /// <summary>
        /// The ID of the OpenStreetMap object.
        /// </summary>
        public int osm_id;

        /// <summary>
        /// The latitude coordinate of the location.
        /// </summary>
        public string latitude;

        /// <summary>
        /// The longitude coordinate of the location. 
        /// </summary>
        public string longitude;

        /// <summary>
        /// The display name or label of the location.
        /// </summary>
        public string display_name;

        /// <summary>
        /// The address.
        /// </summary>
        public Address address;

        /// <summary>
        /// The bounding box.
        /// </summary>
        public string[] boundingbox;

        /// <summary>
        /// Class to handle the address data.
        /// </summary>
        [System.Serializable]
        public class Address
        {
            /// <summary>
            /// The house number of the address.
            /// </summary>
            public string house_number;

            /// <summary>
            /// The street name.
            /// </summary>
            public string road;

            /// <summary>
            /// The city.
            /// </summary>
            public string city_district;

            /// <summary>
            /// The town.
            /// </summary>
            public string town;

            /// <summary>
            /// The state.
            /// </summary>
            public string state;

            /// <summary>
            /// The country.
            /// </summary>
            public string country;

            /// <summary>
            /// The postcode.
            /// </summary>
            public string postcode;

            /// <summary>
            /// The country code.
            /// </summary>
            public string country_code;
        }
    }
}
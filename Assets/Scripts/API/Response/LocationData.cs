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
        public readonly int place_id;

        /// <summary>
        /// The license information.
        /// </summary>
        public readonly string licence;

        /// <summary>
        /// The source or provider of the location data.
        /// </summary>
        public readonly string powered_by;

        /// <summary>
        /// The type of OpenStreetMap object. 
        /// </summary>
        public readonly string osm_type;

        /// <summary>
        /// The ID of the OpenStreetMap object.
        /// </summary>
        public readonly int osm_id;

        /// <summary>
        /// The latitude coordinate of the location.
        /// </summary>
        public readonly string latitude;

        /// <summary>
        /// The longitude coordinate of the location. 
        /// </summary>
        public readonly string longitude;

        /// <summary>
        /// The display name or label of the location.
        /// </summary>
        public readonly string display_name;

        /// <summary>
        /// The address.
        /// </summary>
        public readonly Address address;

        /// <summary>
        /// The bounding box.
        /// </summary>
        public readonly string[] boundingbox;

        /// <summary>
        /// Class to handle the address data.
        /// </summary>
        [System.Serializable]
        public class Address
        {
            /// <summary>
            /// The house number of the address.
            /// </summary>
            public readonly string house_number;

            /// <summary>
            /// The street name.
            /// </summary>
            public readonly string road;

            /// <summary>
            /// The city.
            /// </summary>
            public readonly string city_district;

            /// <summary>
            /// The town.
            /// </summary>
            public readonly string town;

            /// <summary>
            /// The state.
            /// </summary>
            public readonly string state;

            /// <summary>
            /// The country.
            /// </summary>
            public readonly string country;

            /// <summary>
            /// The postcode.
            /// </summary>
            public readonly string postcode;

            /// <summary>
            /// The country code.
            /// </summary>
            public readonly string country_code;
        }
    }
}
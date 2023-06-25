namespace WeatherApp.API
{
    /// <summary>
    /// Class to handle the base request for an API call.
    /// </summary>
    public abstract class APIRequest
    {
        /// <summary>
        /// The URL the API will make the call to.
        /// </summary>
        public abstract string URL { get; }
    }
}
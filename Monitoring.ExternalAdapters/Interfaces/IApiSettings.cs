namespace Monitoring.ExternalAdapters.Interfaces
{
    /// <summary>
    /// Api settings contract
    /// </summary>
    public interface IApiSettings
    {
        /// <summary>
        /// Api name, exemple: Google Places
        /// </summary>
        string ApiName { get; set; }

        /// <summary>
        /// Api base url
        /// </summary>
        string BaseUrl { get; set; }

        /// <summary>
        /// Private key to access the service
        /// </summary>
        string AccessKey { get; set; }

        /// <summary>
        /// Response format
        /// </summary>
        string MediaType { get; set; }

        /// <summary>
        /// Name of the class that manages api's response
        /// </summary>
        string AdapterName { get; set; }
    }
}

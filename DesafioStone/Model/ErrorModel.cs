using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace MonitoringApi.Model
{
    /// <summary>
    /// Represents error messages from the api
    /// </summary>
    public class ErrorModel 
    {
        /// <summary>
        /// HttpStatusCode
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Custom messages
        /// </summary>
        public List<string> Messages { get; set; }

        /// <summary>
        /// Creates errors model
        /// </summary>
        /// <param name="statusCode">Http status code</param>
        /// <param name="message">Messages</param>
        public ErrorModel(HttpStatusCode statusCode, string message): base()
        {
            Messages = new List<string>();
            StatusCode = (int)statusCode;
            Messages.Add(message);
        }

        /// <summary>
        /// Creates errors model
        /// </summary>
        /// <param name="statusCode">Http status code</param>
        /// <param name="messages">Messages</param>
        public ErrorModel(HttpStatusCode statusCode, IEnumerable<string> messages)
        {
            StatusCode = (int)statusCode;
            Messages = (List<string>)messages;
        }
    }
}

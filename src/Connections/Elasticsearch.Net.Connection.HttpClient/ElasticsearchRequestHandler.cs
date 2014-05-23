using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Elasticsearch.Net.Connection.HttpClient
{
    /// <summary>
    /// Internal Message Handler that is used by the ElasticHttpClient. This class cannot be inherited.
    /// </summary>
    internal sealed class ElasticsearchHttpMessageHandler : DelegatingHandler
    {
        /// <summary>
        /// Creates a new instance of the <see cref="T:System.Net.Http.DelegatingHandler" /> class with a specific inner handler.
        /// </summary>
        /// <param name="innerHandler">The inner handler which is responsible for processing the HTTP response messages.</param>
        public ElasticsearchHttpMessageHandler(HttpClientHandler innerHandler)
            : base(innerHandler ?? new WebRequestHandler())
        {
            
        }

        /// <summary>
        /// Sends an HTTP request to the inner handler to send to the server as an asynchronous operation.
        /// </summary>
        /// <param name="request">The HTTP request message to send to the server.</param>
        /// <param name="cancellationToken">A cancellation token to cancel operation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />. The task object representing the asynchronous operation.</returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            if (request != null && request.RequestUri != null && !string.IsNullOrWhiteSpace(request.RequestUri.UserInfo))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(request.RequestUri.UserInfo)));
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}

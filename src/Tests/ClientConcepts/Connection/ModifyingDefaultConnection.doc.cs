using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Newtonsoft.Json;
using Tests.Framework;
using Tests.Framework.MockData;

namespace Tests.ClientConcepts.Connection
{
    /**[[modifying-default-connection]]
     * === Modifying the default connection
     *
     * The client abstracts sending the request and creating a response behind `IConnection` and the default
     * implementation uses
     *
     * - https://msdn.microsoft.com/en-us/library/system.net.webrequest(v=vs.110).aspx[`System.Net.WebRequest`] for Desktop CLR
     * - https://msdn.microsoft.com/en-us/library/system.net.http.httpclient(v=vs.118).aspx[`System.Net.Http.HttpClient`] for Core CLR
     *
     * The reason for different implementations is that `WebRequest` and `ServicePoint` are not directly available
     * on netstandard 1.3.
     *
     * The Desktop CLR implementation using `WebRequest` is the most mature implementation, having been tried and trusted
     * in production since the beginning of NEST. For this reason, we aren't quite ready to it give up in favour of
     * a `HttpClient` implementation across all CLR versions.
     *
     * In addition to production usage, there are also a couple of important toggles that are easy to set against a
     * `ServicePoint` that are not possible to set as yet on `HttpClient`.
     *
     * Finally, another limitation is that `HttpClient` has no synchronous code paths, so supporting these means
     * doing hacky async patches which definitely need time to bake.
     *
     * So why would you ever want to pass your own `IConnection`? Let's look at a couple of examples
     *
     */
    public class ModifyingTheDefaultConnection
    {
        /**==== Using InMemoryConnection
         *
         * `InMemoryConnection` is an in-built `IConnection` that makes it easy to write unit tests against. It can be
         * configured to respond with default response bytes, HTTP status code and an exception when a call is made.
         *
         * `InMemoryConnection` **doesn't actually send any requests or receive any responses from Elasticsearch**;
         * requests are still serialized and the request bytes can be obtained on the response if `.DisableDirectStreaming` is
         * set to `true` on the request or globally
         */
        public void InMemoryConnectionDefaultCtor()
        {
            var connection = new InMemoryConnection();
            var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
            var settings = new ConnectionSettings(connectionPool, connection);
            var client = new ElasticClient(settings);
        }

        /**
		 * Here we create a new `ConnectionSettings` by using the overload that takes a `IConnectionPool` and an `IConnection`.
		 * We pass it an `InMemoryConnection` which, using the default parameterless constructor,
         * will return 200 for everything and never actually perform any IO.
         *
         * Let's see a more complex example
		 */
        [U]
        public void InMemoryConnectionOverloadedCtor()
        {
            var response = new
            {
                took = 1,
                timed_out = false,
                _shards = new
                {
                    total = 2,
                    successful = 2,
                    failed = 0
                },
                hits = new
                {
                    total = 25,
                    max_score = 1.0,
                    hits = Enumerable.Range(1, 25).Select(i => (object)new
                    {
                        _index = "project",
                        _type = "project",
                        _id = $"Project {i}",
                        _score = 1.0,
                        _source = new { name = $"Project {i}" }
                    }).ToArray()
                }
            };

            var responseBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));
            var connection = new InMemoryConnection(responseBytes, 200); // <1> `InMemoryConnection` is configured to **always** return `responseBytes` along with a 200 HTTP status code
            var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
            var settings = new ConnectionSettings(connectionPool, connection).DefaultIndex("project");
            var client = new ElasticClient(settings);

	        var searchResponse = client.Search<Project>(s => s.MatchAll());

	        /**
	         * We can now assert that the `searchResponse` is valid and contains documents deserialized
	         * from our fixed `InMemoryConnection` response
	         */
            searchResponse.ShouldBeValid();
            searchResponse.Documents.Count.Should().Be(25);
        }
    }
}

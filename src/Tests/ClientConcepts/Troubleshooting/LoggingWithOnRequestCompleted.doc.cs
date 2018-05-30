using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Xunit;

namespace Tests.ClientConcepts.Troubleshooting
{
    /**=== Logging with OnRequestCompleted
     * When constructing the connection settings to pass to the client, you can pass a callback of type
     * `Action<IApiCallDetails>` to the `OnRequestCompleted` method that can eavesdrop every time a
     * response(good or bad) is received.
     *
     * If you have complex logging needs this is a good place to add that in
     * since you have access to both the request and response details.
     */
    public class LoggingRequestsAndResponses
    {
        /**
        * In this example, we'll use `OnRequestCompleted` on connection settings to increment a counter each time
        * it's called.
        */
        [U]
        public async Task OnRequestCompletedIsCalled()
        {
            var counter = 0;
            var client = TestClient.GetInMemoryClient(connectionSettings =>
                connectionSettings.OnRequestCompleted(r => counter++)); // <1> Construct a client

            client.RootNodeInfo(); // <2> Make a synchronous call and assert the counter is incremented
            counter.Should().Be(1);

            await client.RootNodeInfoAsync(); // <3> Make an asynchronous call and assert the counter is incremented
            counter.Should().Be(2);
        }

        /**
		*`OnRequestCompleted` is called even when an exception is thrown, so it can be used even if the client is
        * configured to throw exceptions
		*/
        [U]
        public async Task OnRequestCompletedIsCalledWhenExceptionIsThrown()
        {
            var counter = 0;
            var client = TestClient.GetFixedReturnClient( // <1> Configure a client with a connection that **always returns a HTTP 500 response
                new { },
                500,
                connectionSettings => connectionSettings
                    .ThrowExceptions() // <2> Always throw exceptions when a call results in an exception
                    .OnRequestCompleted(r => counter++)
            );

            Assert.Throws<ElasticsearchClientException>(() => client.RootNodeInfo()); // <3> Assert an exception is thrown and the counter is incremented
            counter.Should().Be(1);

            await Assert.ThrowsAsync<ElasticsearchClientException>(async () => await client.RootNodeInfoAsync());
            counter.Should().Be(2);
        }

        /**
        * Here's an example using `OnRequestCompleted()` for more complex logging
        *
        * [NOTE]
        * --
        * By default, the client writes directly to the request stream and deserializes directly from the
        * response stream.
        *
        * If you would also like to capture the request and/or response bytes,
        * you also need to set `.DisableDirectStreaming()` to `true`.
        * --
		*/
        [U]
        public async Task UsingOnRequestCompletedForLogging()
        {
            var list = new List<string>();
            var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));

            var settings = new ConnectionSettings(connectionPool, new InMemoryConnection()) // <1> Here we use `InMemoryConnection` but in a real application, you'd use an `IConnection` that _actually_ sends the request, such as `HttpConnection`
                .DefaultIndex("default-index")
                .DisableDirectStreaming() // <2> Disable direct streaming so we can capture the request and response bytes
                .OnRequestCompleted(apiCallDetails => // <3> Perform some action when a request completes. Here, we're just adding to a list, but in your application you may be logging to a file.
                {
                    // log out the request and the request body, if one exists for the type of request
                    if (apiCallDetails.RequestBodyInBytes != null)
                    {
                        list.Add(
                            $"{apiCallDetails.HttpMethod} {apiCallDetails.Uri} " +
                            $"{Encoding.UTF8.GetString(apiCallDetails.RequestBodyInBytes)}");
                    }
                    else
                    {
                        list.Add($"{apiCallDetails.HttpMethod} {apiCallDetails.Uri}");
                    }

                    // log out the response and the response body, if one exists for the type of response
                    if (apiCallDetails.ResponseBodyInBytes != null)
                    {
                        list.Add($"Status: {apiCallDetails.HttpStatusCode}" +
                                 $"{Encoding.UTF8.GetString(apiCallDetails.ResponseBodyInBytes)}");
                    }
                    else
                    {
                        list.Add($"Status: {apiCallDetails.HttpStatusCode}");
                    }
                });

            var client = new ElasticClient(settings);

            var syncResponse = client.Search<object>(s => s // <4> Make a synchronous call
                .AllTypes()
                .AllIndices()
                .Scroll("2m")
                .Sort(ss => ss
                    .Ascending(SortSpecialField.DocumentIndexOrder)
                )
            );

            list.Count.Should().Be(2);

            var asyncResponse = await client.SearchAsync<object>(s => s // <5> Make an asynchronous call
                .AllTypes()
                .AllIndices()
                .Scroll("10m")
                .Sort(ss => ss
                    .Ascending(SortSpecialField.DocumentIndexOrder)
                )
            );

            list.Count.Should().Be(4);
            list.ShouldAllBeEquivalentTo(new[] // <6> Assert the list contains the contents written in the delegate passed to `OnRequestCompleted`
            {
                @"POST http://localhost:9200/_search?typed_keys=true&scroll=2m {""sort"":[{""_doc"":{""order"":""asc""}}]}",
                @"Status: 200",
                @"POST http://localhost:9200/_search?typed_keys=true&scroll=10m {""sort"":[{""_doc"":{""order"":""asc""}}]}",
                @"Status: 200"
            });
        }

        /**
         * When running an application in production, you probably don't want to disable direct streaming for _all_
         * requests, since doing so will incur a performance overhead, due to buffering request and
         * response bytes in memory. It can however be useful to capture requests and responses in an ad-hoc fashion,
         * perhaps to troubleshoot an issue in production.
         *
         * `DisableDirectStreaming` can be enabled on a _per-request_ basis for this purpose. In using this feature,
         * it is possible to configure a general logging mechanism in `OnRequestCompleted` and log out
         * request and responses only when necessary
         */
        [U]
        public async Task OnRequestCompletedPerRequest()
        {
            var list = new List<string>();
            var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));

            var settings = new ConnectionSettings(connectionPool, new InMemoryConnection())
                .DefaultIndex("default-index")
                .OnRequestCompleted(apiCallDetails =>
                {
                    // log out the request and the request body, if one exists for the type of request
                    if (apiCallDetails.RequestBodyInBytes != null)
                    {
                        list.Add(
                            $"{apiCallDetails.HttpMethod} {apiCallDetails.Uri} " +
                            $"{Encoding.UTF8.GetString(apiCallDetails.RequestBodyInBytes)}");
                    }
                    else
                    {
                        list.Add($"{apiCallDetails.HttpMethod} {apiCallDetails.Uri}");
                    }

                    // log out the response and the response body, if one exists for the type of response
                    if (apiCallDetails.ResponseBodyInBytes != null)
                    {
                        list.Add($"Status: {apiCallDetails.HttpStatusCode}" +
                                 $"{Encoding.UTF8.GetString(apiCallDetails.ResponseBodyInBytes)}");
                    }
                    else
                    {
                        list.Add($"Status: {apiCallDetails.HttpStatusCode}");
                    }
                });

            var client = new ElasticClient(settings);

            var syncResponse = client.Search<object>(s => s // <1> Make a synchronous call where the request and response bytes will not be buffered
                .AllTypes()
                .AllIndices()
                .Scroll("2m")
                .Sort(ss => ss
                    .Ascending(SortSpecialField.DocumentIndexOrder)
                )
            );

            list.Count.Should().Be(2);

            var asyncResponse = await client.SearchAsync<object>(s => s // <2> Make an asynchronous call where `DisableDirectStreaming()` is enabled
                .RequestConfiguration(r => r
                    .DisableDirectStreaming()
                )
                .AllTypes()
                .AllIndices()
                .Scroll("10m")
                .Sort(ss => ss
                    .Ascending(SortSpecialField.DocumentIndexOrder)
                )
            );

            list.Count.Should().Be(4);
            list.ShouldAllBeEquivalentTo(new[]
            {
                @"POST http://localhost:9200/_search?typed_keys=true&scroll=2m", // <3> Only the method and url for the first request is captured
                @"Status: 200",
                @"POST http://localhost:9200/_search?typed_keys=true&scroll=10m {""sort"":[{""_doc"":{""order"":""asc""}}]}", // <4> the body of the second request is captured
                @"Status: 200"
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elastic.Xunit.Sdk;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.ClientConcepts.Troubleshooting
{
    /**
     * === Debug information
     *
     * Every response from Elasticsearch.Net and NEST contains a `DebugInformation` property
     * that provides a human readable description of what happened during the request for both successful and
     * failed requests
     */
    public class DebugInformation : IntegrationDocumentationTestBase, IClusterFixture<ReadOnlyCluster>
    {
        public DebugInformation(ReadOnlyCluster cluster) : base(cluster) {}

        [I]
        public void DefaultDebug()
        {
            // hide
            var client = this.Client;

            var response = client.Search<Project>(s => s
                .Query(q => q
                    .MatchAll()
                )
            );

            response.DebugInformation.Should().Contain("Valid NEST response");
        }
        /**
         * This can be useful in tracking down numerous problems and can also be useful when filing an
         * {github}/issues[issue] on our github repository.
         *
         * By default, the request and response bytes are not available within the debug information, but
         * can be enabled globally on Connection Settings
         *
         */
        public void DisableDirectStreaming()
        {
            var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));

            var settings = new ConnectionSettings(connectionPool)
                .DisableDirectStreaming(); // <1> disable direct streaming for *all* requests

            var client = new ElasticClient(settings);
        }

        /**
         * or on a _per request_ basis
         */
        [I] public void DisableDirectStreamingPerRequest()
        {
            // hide
            var client = TestClient.DefaultInMemoryClient;

            var response = client.Search<Project>(s => s
                .RequestConfiguration(r => r
                    .DisableDirectStreaming() // <1> disable direct streaming for *this* request
                )
                .Query(q => q
                    .MatchAll()
                )
            );

            response.DebugInformation.Should().Contain("\"match_all\":");
        }
    }
}

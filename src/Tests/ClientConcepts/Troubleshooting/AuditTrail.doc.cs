using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Extensions;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.ClientConcepts.Troubleshooting
{
    /**
     * === Audit trail
     *
     * Elasticsearch.Net and NEST provide an audit trail for the events within the request pipeline that
	 * occur when a request is made. This audit trail is available on the response as demonstrated in the
	 * following example.
     */
    public class AuditTrail : IClusterFixture<ReadOnlyCluster>
    {
	    private readonly ReadOnlyCluster _cluster;

	    public AuditTrail(ReadOnlyCluster cluster)
	    {
		    _cluster = cluster;
	    }

	    [I] public void AvailableOnResponse()
	    {
			/**
			 * We'll use a Sniffing connection pool here since it sniffs on startup and pings before
			 * first usage, so we can get an audit trail with a few events out
			 */
			var pool = new SniffingConnectionPool(new []{ new Uri($"http://{TestClient.DefaultHost}:9200") });
		    var connectionSettings = new ConnectionSettings(pool)
				.InferMappingFor<Project>(i => i
					.IndexName("project")
				);

			var client = new ElasticClient(connectionSettings);

			/**
			 * After issuing the following request
			 */
		    var response = client.Search<Project>(s => s
				.MatchAll()
		    );

			/**
			 * The response contains an audit trail with four events
			 */
		    response.ApiCall.AuditTrail.Count.Should().Be(4);
		    response.ApiCall.AuditTrail[0].Event.Should().Be(AuditEvent.SniffOnStartup);
		    response.ApiCall.AuditTrail[1].Event.Should().Be(AuditEvent.SniffSuccess);
		    response.ApiCall.AuditTrail[2].Event.Should().Be(AuditEvent.PingSuccess);
		    response.ApiCall.AuditTrail[3].Event.Should().Be(AuditEvent.HealthyResponse);

			/**
			 * Each audit has a started and ended `DateTime` on it that will provide
			 * some understanding of how long it took
			 */
		    response.ApiCall.AuditTrail
				.Should().OnlyContain(a => a.Ended - a.Started > TimeSpan.Zero);

			/**
			 * The audit trail is also provided in the <<debug-information, Debug information>> in a human
			 * readable fashion, similar to
			 *
			 * ....
			 * Valid NEST response built from a successful low level call on POST: /project/project/_search
             * # Audit trail of this API call:
             *  - [1] SniffOnStartup: Took: 00:00:00.0360264
             *  - [2] SniffSuccess: Node: http://localhost:9200/ Took: 00:00:00.0310228
             *  - [3] PingSuccess: Node: http://127.0.0.1:9200/ Took: 00:00:00.0115074
             *  - [4] HealthyResponse: Node: http://127.0.0.1:9200/ Took: 00:00:00.1477640
             * # Request:
             * <Request stream not captured or already read to completion by serializer. Set DisableDirectStreaming() on ConnectionSettings to force it to be set on the response.>
             * # Response:
             * <Response stream not captured or already read to completion by serializer. Set DisableDirectStreaming() on ConnectionSettings to force it to be set on the response.>
			 * ....
			 *
			 * to help with troubleshooting
			 */
			var debugInformation = response.DebugInformation;
	    }
    }
}

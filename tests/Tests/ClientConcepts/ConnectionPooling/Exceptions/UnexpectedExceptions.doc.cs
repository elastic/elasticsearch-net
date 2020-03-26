using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Elasticsearch.Net.VirtualizedCluster;
using Elasticsearch.Net.VirtualizedCluster.Audit;
using FluentAssertions;
using Tests.Framework;

namespace Tests.ClientConcepts.ConnectionPooling.Exceptions
{
	public class UnexpectedExceptions
	{
		/**=== Unexpected exceptions
		*
		* When a client call throws an exception that the `IConnection` cannot handle, the exception will bubble
		* out of the client as an `UnexpectedElasticsearchClientException`, regardless of whether the client is configured to
		* throw exceptions or not.
		*
		* An `IConnection` is in charge of knowing which exceptions it can recover from and those it can't, and the default `IConnection`
		* in
		*
		* - Desktop CLR is based on `WebRequest` which can and will recover from `WebException`
		* - Core CLR is based on `HttpClient` which can and will recover from `HttpRequestException`
		*
		* Other exceptions will be grounds for immediately exiting the pipeline. Let's demonstrate this
		* using our Virtual cluster test framework
		*/
		[U] public async Task UnexpectedExceptionsBubbleOut()
		{
			var audit = new Auditor(() => VirtualClusterWith // <1> set up a cluster with 10 nodes
				.Nodes(10)
				.ClientCalls(r => r.SucceedAlways())
				.ClientCalls(r => r.OnPort(9201).FailAlways(new Exception("boom!"))) // <2> where node 2 on port 9201 always throws an exception
				.StaticConnectionPool()
				.Settings(s => s.DisablePing().EnableDebugMode())
			);

			audit = await audit.TraceCall(
				new ClientCall {
					{ AuditEvent.HealthyResponse, 9200 }, // <3> The first call to 9200 returns a healthy response
				}
			);

			audit = await audit.TraceUnexpectedException(
				new ClientCall {
					{ AuditEvent.BadResponse, 9201 }, // <4> ...but the second call, to 9201, returns a bad response
				},
				(e) =>
				{
					e.FailureReason.Should().Be(PipelineFailure.Unexpected);
					e.InnerException.Should().NotBeNull();
					e.InnerException.Message.Should().Be("boom!");
				}
			);
		}

		/**
		* Sometimes, an unexpected exception happens further down in the pipeline. In this scenario, we
		* wrap them inside an `UnexpectedElasticsearchClientException` so that information about where
		* in the pipeline the exception happened is not lost.
		*
		* In this next example, a call to 9200 fails with a `WebException`.
		* The call then rolls over to retry on 9201, which throws an hard exception from within `IConnection`.
		* Finally, we assert that we can still see the audit trail for the whole coordinated request.
		*/

		[U] public async Task WillFailOverKnowConnectionExceptionButNotUnexpected()
		{
			var audit = new Auditor(() => VirtualClusterWith
				.Nodes(10)
#if DOTNETCORE
				.ClientCalls(r => r.OnPort(9200).FailAlways(new System.Net.Http.HttpRequestException("recover"))) // <1> calls on 9200 set up to throw a `HttpRequestException` or a `WebException`
#else
				.ClientCalls(r => r.OnPort(9200).FailAlways(new System.Net.WebException("recover"))) // <1>
#endif
				.ClientCalls(r => r.OnPort(9201).FailAlways(new Exception("boom!"))) // <2> calls on 9201 set up to throw an `Exception`
				.StaticConnectionPool()
				.Settings(s => s.DisablePing())
			);

			audit = await audit.TraceUnexpectedException(
				new ClientCall {
					{ AuditEvent.BadResponse, 9200 },
					{ AuditEvent.BadResponse, 9201 }, // <3> Assert that the audit trail for the client call includes the bad response from 9200 and 9201
				},
				(e) =>
				{
					e.FailureReason.Should().Be(PipelineFailure.Unexpected);
					e.InnerException.Should().NotBeNull();
					e.InnerException.Message.Should().Be("boom!");
				}
			);
		}

		/**
		* An unexpected hard exception on ping and sniff is something we *do* try to recover from and failover to retrying on the next node.
		*
		* Here, pinging nodes on first use is enabled and the node on port 9200 throws an exception on ping; when this happens,
		* we still fallover to retry the ping on node on port 9201, where it succeeds.
		* Following this, the client call on 9201 throws a hard exception that we are not able to recover from
		*/
		[U] public async Task PingUnexceptedExceptionDoesFailOver()
		{
			var audit = new Auditor(() => VirtualClusterWith
				.Nodes(10)
				.Ping(r => r.OnPort(9200).FailAlways(new Exception("ping exception")))
				.Ping(r => r.OnPort(9201).SucceedAlways())
				.ClientCalls(r => r.OnPort(9201).FailAlways(new Exception("boom!")))
				.StaticConnectionPool()
				.AllDefaults()
			);

			audit = await audit.TraceUnexpectedException(
				new ClientCall {
					{ AuditEvent.PingFailure, 9200 },
					{ AuditEvent.PingSuccess, 9201 },
					{ AuditEvent.BadResponse, 9201 },
				},
				e =>
				{
					e.FailureReason.Should().Be(PipelineFailure.Unexpected);

					e.InnerException.Should().NotBeNull();
					e.InnerException.Message.Should().Be("boom!"); // <1> `InnerException` is the exception that brought the request down

					e.SeenExceptions.Should().NotBeEmpty(); // <2> The hard exception that happened on ping is still available though
					var pipelineException = e.SeenExceptions.First();
					pipelineException.FailureReason.Should().Be(PipelineFailure.PingFailure);
					pipelineException.InnerException.Message.Should().Be("ping exception");

					var pingException = e.AuditTrail.First(a => a.Event == AuditEvent.PingFailure).Exception; // <3> An exception can be hard to relate back to a point in time, so the exception is also available on the audit trail
					pingException.Should().NotBeNull();
					pingException.Message.Should().Be("ping exception");
				}
			);
		}
	}
}

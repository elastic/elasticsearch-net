using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;

namespace Tests.ClientConcepts.ConnectionPooling.Exceptions
{
	public class UnrecoverableExceptions
	{
		/**=== Unrecoverable exceptions
		 *
		* Unrecoverable exceptions are _expected_ exceptions that are grounds to exit the client pipeline immediately.
		*
		* [NOTE]
		* --
		* What do we mean by _expected_ exceptions? Aren't all exceptions _exceptional_?
		*
		* Well, there a some exceptions that can be thrown in the course of a request
		* that _may_ be expected, some of which can be retried on another node in the cluster, and some that cannot.
		* For example, an exception thrown when pinging a node throws an exception,
		* but this is an exception that the client expects can happen,
		* and can handle by trying a ping on another node. On the contrary, a bad authentication response from a node will throw an
		* exception, and the client understands that an exception under these circumstances should be handled by not retrying,
		* but by exiting the pipeline.
		* --
		*
		* By default, the client won't throw on any `ElasticsearchClientException` but instead return an invalid response
        * that can be detected by checking the `.IsValid` property on the response. You can change this behaviour with
		* by using `ThrowExceptions()` on <<configuration-options, `ConnectionSettings`>>.
		*
		*/
		[U] public void SomePipelineFailuresAreRecoverable()
		{
			var failures = Enum.GetValues(typeof(PipelineFailure)).Cast<PipelineFailure>();
			foreach (var failure in failures)
			{
				switch (failure)
				{
					/** The followinig pipeline failures are recoverable and will be retried */
					case PipelineFailure.PingFailure:
					case PipelineFailure.BadRequest:
					case PipelineFailure.BadResponse:
						var recoverable = new PipelineException(failure);
						recoverable.Recoverable.Should().BeTrue(failure.GetStringValue());
						break;

					/** The followinig pipeline failures are NOT recoverable and won't be retried */
					case PipelineFailure.BadAuthentication:
					case PipelineFailure.SniffFailure:
					case PipelineFailure.CouldNotStartSniffOnStartup:
					case PipelineFailure.MaxTimeoutReached:
					case PipelineFailure.MaxRetriesReached:
					case PipelineFailure.Unexpected:
					case PipelineFailure.NoNodesAttempted:
						var unrecoverable = new PipelineException(failure);
						unrecoverable.Recoverable.Should().BeFalse(failure.GetStringValue());
						break;
					default:
						throw new ArgumentOutOfRangeException(failure.GetStringValue());
				}
			}
		}

		/**
		 * As an example, let's use our Virtual cluster test framework to set up a 10 node cluster
		 * that always succeeds when pinged but fails with a 401 response when making client calls
		 */
		[U] public async Task BadAuthenticationIsUnrecoverable()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(10)
				.Ping(r => r.SucceedAlways()) // <1> Always succeed on ping
				.ClientCalls(r => r.FailAlways(401)) // <2> ...but always fail on calls with a 401 Bad Authentication response
				.StaticConnectionPool()
				.AllDefaults()
			);

			/**
			 * Now, let's make a client call. We'll see that the first audit event is a successful ping
			* followed by a bad response as a result of the 401 bad authentication response
			*/
			audit = await audit.TraceElasticsearchException(
				new ClientCall {
					{ AuditEvent.PingSuccess, 9200 }, // <1> First call results in a successful ping
					{ AuditEvent.BadResponse, 9200 }, // <2> Second call results in a bad response
				},
				exception =>
				{
					exception.FailureReason
						.Should().Be(PipelineFailure.BadAuthentication); // <3> The reason for the bad response is Bad Authentication
				}
			);
		}

		private static byte[] HtmlNginx401Response = Encoding.UTF8.GetBytes(@"<html>
<head><title>401 Authorization Required</title></head>
<body bgcolor=""white"">
<center><h1>401 Authorization Required</h1></center>
<hr><center>nginx/1.4.6 (Ubuntu)</center>
</body>
</html>
");

		/**
		 * When a bad authentication response occurs, the client attempts to deserialize the response body returned;
		 *
		 * In some setups you might be running behind a proxy and you might need to prevent the client from trying to deserialize
		 * bad json. In the following example an HTML response is return but with an application/json content type. If the proxy is not
		 * under your control you would need to be able to fix this in the client. Here we make the client aware that 401 responses
		 * should never be deserialized by calling `SkipDeserializationForStatusCodes()` on `ConnectionSettings`.
		 */
		[U] public async Task BadAuthenticationHtmlResponseIsIgnored()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(10)
				.Ping(r => r.SucceedAlways())
				.ClientCalls(r => r.FailAlways(401).ReturnByteResponse(HtmlNginx401Response, "application/json")) // <1> Always return a 401 bad response with a HTML response on client calls
				.StaticConnectionPool()
				.Settings(s=>s.SkipDeserializationForStatusCodes(401))
			);

			audit = await audit.TraceElasticsearchException(
				new ClientCall {
					{ AuditEvent.PingSuccess, 9200 },
					{ AuditEvent.BadResponse, 9200 },
				},
				(e) =>
				{
					e.FailureReason.Should().Be(PipelineFailure.BadAuthentication);
					e.Response.HttpStatusCode.Should().Be(401);
					e.Response.ResponseBodyInBytes.Should().BeNull(); // <2> Assert that the response body bytes are null
				}
			);
		}

		/**
		 * Now in this example, by turning on `DisableDirectStreaming()` on `ConnectionSettings`, we see the same behaviour exhibited
		 * as before, but this time however, the response body bytes are captured in the response and can be inspected.
		 * Also note that in this example the 401 returns the correct mime type for html so the client wont try to deserialize to json and
		 * we no longer need to set `SkipDeserializationForStatusCodes()`
		 */
		[U] public async Task BadAuthenticationHtmlResponseStillExposedWhenUsingDisableDirectStreaming()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(10)
				.Ping(r => r.SucceedAlways())
				.ClientCalls(r => r.FailAlways(401).ReturnByteResponse(HtmlNginx401Response, "text/html"))
				.StaticConnectionPool()
				.Settings(s => s.DisableDirectStreaming())
			);

			audit = await audit.TraceElasticsearchException(
				new ClientCall {
					{ AuditEvent.PingSuccess, 9200 },
					{ AuditEvent.BadResponse, 9200 },
				},
				(e) =>
				{
					e.FailureReason.Should().Be(PipelineFailure.BadAuthentication);
					e.Response.HttpStatusCode.Should().Be(401);
					e.Response.ResponseBodyInBytes.Should().NotBeNull(); // <1> Response bytes are set on the response
					var responseString = Encoding.UTF8.GetString(e.Response.ResponseBodyInBytes);
					responseString.Should().Contain("nginx/"); // <2> Assert that the response contains `"nginx/"`
					e.DebugInformation.Should().Contain("nginx/");
				}
			);
		}

		// hide
		[U] public async Task BadAuthOnGetClientCallDoesNotThrowSerializationException()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(10)
				.Ping(r => r.SucceedAlways())
				.ClientCalls(r => r.FailAlways(401).ReturnByteResponse(HtmlNginx401Response))
				.StaticConnectionPool()
				.Settings(s => s.DisableDirectStreaming().DefaultIndex("default-index").SkipDeserializationForStatusCodes(401))
				.ClientProxiesTo(
					(c, r) => c.Get<Project>("1", s=>s.RequestConfiguration(r)),
					async (c, r) => await c.GetAsync<Project>("1", s=>s.RequestConfiguration(r)) as IResponse
				)
			);

			audit = await audit.TraceElasticsearchException(
				new ClientCall {
					{ AuditEvent.PingSuccess, 9200 },
					{ AuditEvent.BadResponse, 9200 },
				},
				(e) =>
				{
					e.FailureReason.Should().Be(PipelineFailure.BadAuthentication);
					e.Response.HttpStatusCode.Should().Be(401);
					e.Response.ResponseBodyInBytes.Should().NotBeNull();
					var responseString = Encoding.UTF8.GetString(e.Response.ResponseBodyInBytes);
					responseString.Should().Contain("nginx/");
					e.DebugInformation.Should().Contain("nginx/");
				}
			);
		}
	}
}

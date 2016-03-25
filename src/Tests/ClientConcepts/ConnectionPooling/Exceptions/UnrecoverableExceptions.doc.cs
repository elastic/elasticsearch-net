using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;

namespace Tests.ClientConcepts.ConnectionPooling.Exceptions
{
	public class UnrecoverableExceptions
	{
		/** == Unrecoverable exceptions 
		* Unrecoverable exceptions are _excepted_ exceptions that are grounds to exit the client pipeline immediately. 
		* By default, the client won't throw on any `ElasticsearchClientException` but instead return an invalid response which
		* can be detected by checking `.IsValid` on the response 
		* You can configure the client to throw using `ThrowExceptions()` on `ConnectionSettings`. The following test
		* both a client that throws and one that returns an invalid response with an `.OriginalException` exposed 
		*/

		[U] public void SomePipelineFailuresAreRecoverable()
		{
			/** The following are recoverable exceptions */
			var recoverablExceptions = new[]
			{
				new PipelineException(PipelineFailure.BadResponse),
				new PipelineException(PipelineFailure.PingFailure),
			};

			recoverablExceptions.Should().OnlyContain(e => e.Recoverable);

			/** and the unrecoverable exceptions */
			var unrecoverableExceptions = new[]
			{
				new PipelineException(PipelineFailure.CouldNotStartSniffOnStartup),
				new PipelineException(PipelineFailure.SniffFailure),
				new PipelineException(PipelineFailure.Unexpected),
				new PipelineException(PipelineFailure.BadAuthentication),
				new PipelineException(PipelineFailure.MaxRetriesReached),
				new PipelineException(PipelineFailure.MaxTimeoutReached)
			};

			unrecoverableExceptions.Should().OnlyContain(e => !e.Recoverable);
		}

		/**	As an example, let's set up a 10 node cluster that will always succeed when pinged but
			will fail with a 401 response when making client calls
		*/
		[U] public async Task BadAuthenticationIsUnrecoverable()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(10)
				.Ping(r => r.SucceedAlways())
				.ClientCalls(r => r.FailAlways(401))
				.StaticConnectionPool()
				.AllDefaults()
			);

			/** Here we make a client call and determine that the first audit event was a successful ping, 
			* followed by a bad response as a result of a bad authentication response
			*/
			audit = await audit.TraceElasticsearchException(
				new ClientCall {
					{ AuditEvent.PingSuccess, 9200 },
					{ AuditEvent.BadResponse, 9200 },
				},
				(e) =>
				{
					e.FailureReason.Should().Be(PipelineFailure.BadAuthentication);
				}
			);
		}

		private static byte[] ResponseHtml = Encoding.UTF8.GetBytes(@"<html>
<head><title>401 Authorization Required</title></head>
<body bgcolor=""white"">
<center><h1>401 Authorization Required</h1></center>
<hr><center>nginx/1.4.6 (Ubuntu)</center>
</body>
</html>
");
		[U] public async Task BadAuthenticationHtmlResponseIsIgnored()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(10)
				.Ping(r => r.SucceedAlways())
				.ClientCalls(r => r.FailAlways(401).ReturnResponse(ResponseHtml))
				.StaticConnectionPool()
				.AllDefaults()
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
					e.Response.ResponseBodyInBytes.Should().BeNull();
				}
			);
		}

		[U] public async Task BadAuthenticationHtmlResponseStillExposedWhenUsingDisableDirectStreaming()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(10)
				.Ping(r => r.SucceedAlways())
				.ClientCalls(r => r.FailAlways(401).ReturnResponse(ResponseHtml))
				.StaticConnectionPool()
				.Settings(s=>s.DisableDirectStreaming())
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

		[U] public async Task BadAuthOnGetClientCallDoesNotThrowSerializationException()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(10)
				.Ping(r => r.SucceedAlways())
				.ClientCalls(r => r.FailAlways(401).ReturnResponse(ResponseHtml))
				.StaticConnectionPool()
				.Settings(s=>s.DisableDirectStreaming().DefaultIndex("default-index"))
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

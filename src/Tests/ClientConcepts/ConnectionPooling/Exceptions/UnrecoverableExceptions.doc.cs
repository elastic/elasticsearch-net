using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Tests.Framework;

namespace Tests.ClientConcepts.ConnectionPooling.Exceptions
{
	public class UnrecoverableExceptions
	{
		/** == Unrecoverable exceptions 
		* Unrecoverable exceptions are excepted exceptions that are grounds to exit the client pipeline immediately. 
		* By default the client won't throw on any ElasticsearchClientException but return an invalid response. 
		* You can configure the client to throw using ThrowExceptions() on ConnectionSettings. The following test
		* both a client that throws and one that returns an invalid response with an `.OriginalException` exposed 
		*/

		[U] public void SomePipelineFailuresAreRecoverable()
		{
			var recoverablExceptions = new[]
			{
				new PipelineException(PipelineFailure.BadResponse),
				new PipelineException(PipelineFailure.PingFailure),
			};
			recoverablExceptions.Should().OnlyContain(e => e.Recoverable);

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

		[U] public async Task BadAuthenticationIsUnrecoverable()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(10)
				.Ping(r => r.SucceedAlways())
				.ClientCalls(r => r.FailAlways(401))
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
				}
			);
		}
	}
}

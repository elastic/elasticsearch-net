using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Tests.Framework;

namespace Tests.ClientConcepts.ConnectionPooling.MaxRetries
{
	public class RespectsMaxRetry
	{
		/** == MaxRetries
		* By default retry as many times as we have nodes. However retries still respect the request timeout.
		* Meaning if you have a 100 node cluster and a request timeout of 20 seconds we will retry as many times as we can
		* but give up after 20 seconds
		*/

		[U] public async Task DefaultMaxIsNumberOfNodes()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(10)
				.ClientCalls(r => r.FailAlways())
				.ClientCalls(r => r.OnPort(9209).SucceedAlways())
				.StaticConnectionPool()
				.Settings(s => s.DisablePing())
			);

			audit = await audit.TraceCall(
				new ClientCall {
					{ AuditEvent.BadResponse, 9200 },
					{ AuditEvent.BadResponse, 9201 },
					{ AuditEvent.BadResponse, 9202 },
					{ AuditEvent.BadResponse, 9203 },
					{ AuditEvent.BadResponse, 9204 },
					{ AuditEvent.BadResponse, 9205 },
					{ AuditEvent.BadResponse, 9206 },
					{ AuditEvent.BadResponse, 9207 },
					{ AuditEvent.BadResponse, 9208 },
					{ AuditEvent.HealthyResponse, 9209 }
				}
            );
		}

		/**
		* When you have a 100 node cluster you might want to ensure a fixed number of retries. 
		* Remember that the actual number of requests is initial attempt + set number of retries 
		*/

		[U] public async Task FixedMaximumNumberOfRetries()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(10)
				.ClientCalls(r => r.FailAlways())
				.ClientCalls(r => r.OnPort(9209).SucceedAlways())
				.StaticConnectionPool()
				.Settings(s => s.DisablePing().MaximumRetries(3))
			);

			audit = await audit.TraceCall(
				new ClientCall {
					{ AuditEvent.BadResponse, 9200 },
					{ AuditEvent.BadResponse, 9201 },
					{ AuditEvent.BadResponse, 9202 },
					{ AuditEvent.BadResponse, 9203 },
				}
            );
		}
		/** 
		* In our previous test we simulated very fast failures, in the real world a call might take upwards of a second
		* Here we simulate a particular heavy search that takes 10 seconds to fail, our Request timeout is set to 20 seconds.
		* In this case it does not make sense to retry our 10 second query on 10 nodes. We should try it twice and give up before a third call is attempted
		*/
		[U] public async Task RespectsOveralRequestTimeout()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(10)
				.ClientCalls(r => r.FailAlways().Takes(TimeSpan.FromSeconds(10)))
				.ClientCalls(r => r.OnPort(9209).SucceedAlways())
				.StaticConnectionPool()
				.Settings(s => s.DisablePing().RequestTimeout(TimeSpan.FromSeconds(20)))
			);

			audit = await audit.TraceCall(
				new ClientCall {
					{ AuditEvent.BadResponse, 9200 },
					{ AuditEvent.BadResponse, 9201 },
				}
            );

		}

		/** 
		* If you set smaller request time outs you might not want it to also affect the retry timeout, therefor you can configure these separately too.
		* Here we simulate calls taking 3 seconds, a request time out of 2 and an overall retry timeout of 10 seconds.
		* We should see 5 attempts to perform this query, testing that our request timeout cuts the query off short and that our max retry timeout of 10
		* wins over the configured request timeout
		*/
		[U] public async Task RespectsMaxRetryTimeoutOverRequestTimeout()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(10)
				.ClientCalls(r => r.FailAlways().Takes(TimeSpan.FromSeconds(3)))
				.ClientCalls(r => r.OnPort(9209).SucceedAlways())
				.StaticConnectionPool()
				.Settings(s => s.DisablePing().RequestTimeout(TimeSpan.FromSeconds(2)).SetMaxRetryTimeout(TimeSpan.FromSeconds(10)))
			);

			audit = await audit.TraceCall(
				new ClientCall {
					{ AuditEvent.BadResponse, 9200 },
					{ AuditEvent.BadResponse, 9201 },
					{ AuditEvent.BadResponse, 9202 },
					{ AuditEvent.BadResponse, 9203 },
					{ AuditEvent.BadResponse, 9204 },
				}
            );

		}
		/** 
		* If your retry policy expands beyond available nodes we won't retry the same node twice
		*/
		[U] public async Task RetriesAreLimitedByNodesInPool()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(2)
				.ClientCalls(r => r.FailAlways().Takes(TimeSpan.FromSeconds(3)))
				.ClientCalls(r => r.OnPort(9209).SucceedAlways())
				.StaticConnectionPool()
				.Settings(s => s.DisablePing().RequestTimeout(TimeSpan.FromSeconds(2)).SetMaxRetryTimeout(TimeSpan.FromSeconds(10)))
			);

			audit = await audit.TraceCall(
				new ClientCall {
					{ AuditEvent.BadResponse, 9200 },
					{ AuditEvent.BadResponse, 9201 },
				}
            );
		}

		/** 
		* This makes setting any retry setting on a single node connection pool a NOOP, this is by design! 
		* Connection pooling and connection failover is about trying to fail sanely whilst still utilizing available resources and 
		* not giving up on the fail fast principle. It's *NOT* a mechanism for forcing requests to succeed.
		*/
		[U] public async Task DoesNotRetryOnSingleNodeConnectionPool()
		{
			var audit = new Auditor(() => Framework.Cluster
				.Nodes(10)
				.ClientCalls(r => r.FailAlways().Takes(TimeSpan.FromSeconds(3)))
				.ClientCalls(r => r.OnPort(9209).SucceedAlways())
				.SingleNodeConnection()
				.Settings(s => s.DisablePing().MaximumRetries(10))
			);

			audit = await audit.TraceCall(
				new ClientCall {
					{ AuditEvent.BadResponse, 9200 }
				}
            );

		}
	}
}

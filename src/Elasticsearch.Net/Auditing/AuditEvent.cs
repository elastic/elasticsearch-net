using System;
using static Elasticsearch.Net.AuditEvent;

namespace Elasticsearch.Net
{
	public enum AuditEvent
	{
		SniffOnStartup,
		SniffOnFail,
		SniffOnStaleCluster,

		SniffSuccess,
		SniffFailure,
		PingSuccess,
		PingFailure,

		Resurrection,
		AllNodesDead,
		BadResponse,
		HealthyResponse,

		MaxTimeoutReached,
		MaxRetriesReached,
		BadRequest,
		NoNodesAttempted,
		CancellationRequested,
		FailedOverAllNodes,
	}

	internal static class AuditEventExtensions
	{
		public static string GetString(this AuditEvent @event)
		{
			switch(@event)
			{
				case SniffOnStartup: return nameof(SniffOnStartup);
				case SniffOnFail: return nameof(SniffOnFail);
				case SniffOnStaleCluster: return nameof(SniffOnStaleCluster);
				case SniffSuccess: return nameof(SniffSuccess);
				case SniffFailure: return nameof(SniffFailure);
				case PingSuccess: return nameof(PingSuccess);
				case PingFailure: return nameof(PingFailure);
				case Resurrection: return nameof(Resurrection);
				case AllNodesDead: return nameof(AllNodesDead);
				case BadResponse: return nameof(BadResponse);
				case HealthyResponse: return nameof(HealthyResponse);
				case MaxTimeoutReached: return nameof(MaxTimeoutReached);
				case MaxRetriesReached: return nameof(MaxRetriesReached);
				case BadRequest: return nameof(BadRequest);
				case NoNodesAttempted: return nameof(NoNodesAttempted);
				case CancellationRequested: return nameof(CancellationRequested);
				case FailedOverAllNodes: return nameof(FailedOverAllNodes);
				default: return @event.GetStringValue(); //still cached but uses reflection
			}
		}
		
		
	}
	
}

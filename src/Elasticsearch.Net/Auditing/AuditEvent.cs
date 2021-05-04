// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Diagnostics;
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
		/// <summary>
		/// Returns the name of the event to be used for use in <see cref="DiagnosticSource"/>.
		/// <para>If this return null the event should not be reported on</para>
		/// <para>This indicates this event is monitored by a different component already</para>
		/// </summary>
		/// <returns>The diagnostic event name representation or null if it should go unreported</returns>
		public static string GetAuditDiagnosticEventName(this AuditEvent @event)
		{
			switch(@event)
			{
				case SniffFailure: 
				case SniffSuccess:
				case PingFailure: 
				case PingSuccess: 
				case BadResponse: 
				case HealthyResponse: 
					return null;
				case SniffOnStartup: return nameof(SniffOnStartup);
				case SniffOnFail: return nameof(SniffOnFail);
				case SniffOnStaleCluster: return nameof(SniffOnStaleCluster);
				case Resurrection: return nameof(Resurrection);
				case AllNodesDead: return nameof(AllNodesDead);
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

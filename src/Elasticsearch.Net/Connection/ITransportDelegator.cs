using Elasticsearch.Net.Connection.Configuration;
using Elasticsearch.Net.Connection.RequestState;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elasticsearch.Net.Connection
{
	internal interface ITransportDelegator
	{
		bool Ping(ITransportRequestState requestState);
		Task<bool> PingAsync(ITransportRequestState requestState);
		IList<Uri> Sniff(ITransportRequestState ownerState = null);
		void SniffClusterState(ITransportRequestState requestState = null);
		void SniffIfInformationIsTooOld(ITransportRequestState requestState);
		void SniffOnConnectionFailure(ITransportRequestState requestState);

		/// <summary>
		/// Returns either the fixed maximum set on the connection configuration settings or the number of nodes
		/// </summary>
		/// <param name="requestState"></param>
		int GetMaximumRetries(IRequestConfiguration requestConfiguration);

		bool SniffingDisabled(IRequestConfiguration requestConfiguration);
		bool SniffOnFaultDiscoveredMoreNodes(ITransportRequestState requestState, int retried, ElasticsearchResponse<Stream> streamResponse);

		/// <summary>
		/// Selects next node uri on request state
		/// </summary>
		/// <returns>bool hint whether the new current node needs to pinged first</returns>
		bool SelectNextNode(ITransportRequestState requestState);
	}
}

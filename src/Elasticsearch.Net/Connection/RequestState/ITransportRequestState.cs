using System;
using Elasticsearch.Net.Connection.Configuration;

namespace Elasticsearch.Net.Connection.RequestState
{
	internal interface ITransportRequestState
	{
		IRequestTimings InitiateRequest(RequestType requestType);
		Uri CreatePathOnCurrentNode(string path);
		IRequestConfiguration RequestConfiguration { get; }
		int Retried { get; }
		bool SniffedOnConnectionFailure { get; set; }
		int? Seed { get; set; }
		Uri CurrentNode { get; set; }
	}
}
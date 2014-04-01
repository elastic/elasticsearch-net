using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Elasticsearch.Net.Serialization;

namespace Elasticsearch.Net.Connection
{
	public interface ITransport
	{
		IConnectionConfigurationValues Settings { get; }
		IElasticsearchSerializer Serializer { get; }
		
		ElasticsearchResponse<T> DoRequest<T>(
			string method, 
			string path, 
			object data = null, 
			IRequestParameters requestParameters = null);

		IList<Uri> Sniff();
		void SniffClusterState();
		bool Ping(Uri baseUri);
		Task<bool> PingAsync(Uri baseUri);

		Task<ElasticsearchResponse<T>> DoRequestAsync<T>(
			string method, 
			string path, 
			object data = null, 
			IRequestParameters requestParameters = null);
	}

	public interface ITransportValues
	{
		IElasticsearchSerializer Serializer { get; }
	}
}
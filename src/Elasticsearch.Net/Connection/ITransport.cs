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
			NameValueCollection queryString = null, 
			object serializationState = null,
			int retried = 0, 
			int? seed = null
			);

		Task<ElasticsearchResponse<T>> DoRequestAsync<T>(
			string method, 
			string path, 
			object data = null, 
			NameValueCollection queryString = null,
			object serializationState = null,
			int retried = 0, 
			int? seed = null);
	}

	public interface ITransportValues
	{
		IElasticsearchSerializer Serializer { get; }
	}
}
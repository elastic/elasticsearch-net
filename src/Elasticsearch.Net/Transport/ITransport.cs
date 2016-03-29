using System;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public interface ITransport<out TConnectionSettings> 
		where TConnectionSettings : IConnectionConfigurationValues
	{
		TConnectionSettings Settings { get; }

		ElasticsearchResponse<T> Request<T>(HttpMethod method, string path, PostData<object> data = null, IRequestParameters requestParameters = null)
			where T : class;
		Task<ElasticsearchResponse<T>> RequestAsync<T>(HttpMethod method, string path, PostData<object> data = null, IRequestParameters requestParameters = null)
			where T : class;
	}

}
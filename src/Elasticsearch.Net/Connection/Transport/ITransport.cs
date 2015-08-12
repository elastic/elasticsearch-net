using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Elasticsearch.Net.Serialization;

namespace Elasticsearch.Net.Connection
{
	public interface ITransport<out TConnectionSettings> where TConnectionSettings : IConnectionConfigurationValues
	{
		TConnectionSettings Settings { get; }

		ElasticsearchResponse<T> Request<T>(HttpMethod method, string path, PostData<object> data = null, IRequestParameters requestParameters = null);
		Task<ElasticsearchResponse<T>> RequestAsync<T>(HttpMethod method, string path, PostData<object> data = null, IRequestParameters requestParameters = null);
	}

}
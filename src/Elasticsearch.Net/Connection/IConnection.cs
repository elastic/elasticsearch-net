using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elasticsearch.Net.Connection
{
	public interface IConnection
	{
		Task<ElasticsearchResponse<TReturn>> RequestAsync<TReturn>(RequestData requestData)
			where TReturn : class;

		ElasticsearchResponse<TReturn> Request<TReturn>(RequestData requestData)
			where TReturn : class;
	}
}

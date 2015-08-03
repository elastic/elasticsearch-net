using System;
using System.Threading.Tasks;

namespace Elasticsearch.Net.Connection
{
	public interface IRequestPipeline : IDisposable
	{
		bool FirstPoolUsage();

		ElasticsearchResponse<TReturn> CallElasticsearch<TReturn>(RequestData requestData);
		Task<ElasticsearchResponse<TReturn>> CallElasticsearchAsync<TReturn>(RequestData requestData);

		Task<bool> FirstPoolUsageAsync();

		void MarkAlive();
		void MarkDead();

		bool NextNode();

		void OutOfDateClusterInformation();
		Task OutOfDateClusterInformationAsync();

		void Ping();
		Task PingAsync();

		void BadResponse(IElasticsearchResponse response);
	}
}
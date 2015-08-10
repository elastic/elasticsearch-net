using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Providers;
using Nest;
using System;
using Tests.Framework.MockData;

namespace Tests.Framework
{
	public class VirtualizedCluster
	{
		private ElasticClient _client;
		private readonly VirtualCluster _cluster;
		private readonly IConnectionPool _connectionPool;
		private readonly TestDateTimeProvider _dateTimeProvider;
		private FixedPipelineFactory _fixedRequestPipeline;

		public VirtualizedCluster(VirtualCluster cluster, IConnectionPool pool, ConnectionSettings settings)
		{
			this._dateTimeProvider = new TestDateTimeProvider();
			this._fixedRequestPipeline = new FixedPipelineFactory(settings, this._dateTimeProvider);
			this._client = this._fixedRequestPipeline.Client;

			this._cluster = cluster;
			this._connectionPool = pool;
		}

		public ISearchResponse<Project> ClientCall()
		{
			return this._client.Search<Project>(s => s);
		}

		public void ChangeTime(Func<DateTime, DateTime> change) => change(_dateTimeProvider.MutableNow);
	}

	public class TestDateTimeProvider : DateTimeProvider
	{
		public DateTime MutableNow { get; set; } = DateTime.UtcNow;

		public override DateTime Now() => MutableNow;
	}

}
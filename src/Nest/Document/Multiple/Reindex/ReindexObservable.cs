using System;
using Elasticsearch.Net;

namespace Nest
{
	// TODO re-implement this now that ES has a reindex API

	public class ReindexObservable<T> : IDisposable, IObservable<IReindexResponse<T>> where T : class
	{
		private readonly IReindexRequest _reindexRequest;
		private readonly IConnectionSettingsValues _connectionSettings;

		private IElasticClient _client { get; set; }

		public ReindexObservable(IElasticClient client, IConnectionSettingsValues connectionSettings, IReindexRequest reindexRequest)
		{
			this._connectionSettings = connectionSettings;
			this._reindexRequest = reindexRequest;
			this._client = client;
		}

		public IDisposable Subscribe(IObserver<IReindexResponse<T>> observer)
		{
			return this;
		}

		public void Dispose()
		{
		}
	}
}

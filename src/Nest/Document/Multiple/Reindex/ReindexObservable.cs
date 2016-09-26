using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	public class ReindexObservable<T> : IDisposable, IObservable<IReindexResponse<T>> where T : class
	{
		private readonly IReindexRequest _reindexRequest;
		private readonly IConnectionSettingsValues _connectionSettings;
		private readonly IElasticClient _client;

		private Action<IHit<T>, T, IBulkIndexOperation<T>> Alter { get; set; }

		public ReindexObservable(IElasticClient client, IConnectionSettingsValues connectionSettings, IReindexRequest reindexRequest)
		{
			this._connectionSettings = connectionSettings;
			this._reindexRequest = reindexRequest;
			this._client = client;
		}
		public IDisposable Subscribe(ReindexObserver<T> observer)
		{
			this.Alter = observer.Alter;
			return this.Subscribe((IObserver<IReindexResponse<T>>)observer);
		}

		public IDisposable Subscribe(IObserver<IReindexResponse<T>> observer)
		{
			observer.ThrowIfNull(nameof(observer));
			try
			{
				this.Reindex(observer);
			}
			catch (Exception e)
			{
				observer.OnError(e);
			}
			return this;
		}

		private void Reindex(IObserver<IReindexResponse<T>> observer)
		{
			var fromIndex = this._reindexRequest.From.Resolve(this._connectionSettings);
			fromIndex.ThrowIfNullOrEmpty(nameof(fromIndex));
			var toIndex = this._reindexRequest.To.Resolve(this._connectionSettings);
			toIndex.ThrowIfNullOrEmpty(nameof(toIndex));

			this.CreateIndex(fromIndex, toIndex);

			var scroll = this._reindexRequest.Scroll ?? TimeSpan.FromMinutes(2);

			var searchResult = this.InitiateSearch(fromIndex, fromIndex, scroll);

			this.ScrollToCompletion(observer, fromIndex, toIndex, scroll, searchResult);

			observer.OnCompleted();
		}

		private ElasticsearchClientException Throw(string message, IApiCallDetails details) =>
			new ElasticsearchClientException(PipelineFailure.BadResponse, message, details);

		private void ScrollToCompletion(IObserver<IReindexResponse<T>> observer, string fromIndex, string toIndex, Time scroll, ISearchResponse<T> searchResult)
		{
			if (searchResult == null || !searchResult.IsValid)
				throw Throw($"Reindexing to {toIndex} failed unexpectedly during searching index {fromIndex}.", searchResult?.ApiCall);
			IBulkResponse indexResult = null;
			int page = 0;
			while (searchResult.IsValid && searchResult.Documents.HasAny())
			{
				indexResult = this.IndexSearchResults(searchResult, observer, toIndex, page);
				if (indexResult == null || !indexResult.IsValid)
					throw Throw($"Reindexing to {toIndex} failed unexpectedly during bulk indexing.", indexResult?.ApiCall);
				observer.OnNext(new ReindexResponse<T>()
				{
					BulkResponse = indexResult,
					SearchResponse = searchResult,
					Scroll = page
				});
				page++;
				searchResult = this._client.Scroll<T>(scroll, searchResult.ScrollId);
				if (searchResult == null || !searchResult.IsValid)
					throw Throw($"Reindexing to {toIndex} failed unexpectedly during searching index {fromIndex}.", searchResult?.ApiCall);
			}
		}

		private ISearchResponse<T> InitiateSearch(string fromIndex, string toIndex, Time scroll)
		{
			var size = this._reindexRequest.Size ?? 100;
			var searchResult = this._client.Search<T>(new SearchRequest<T>(fromIndex, this._reindexRequest.Type)
			{
				From = 0,
				Size = size,
				Query = this._reindexRequest.Query,
				Scroll = scroll
			});
			if (searchResult.Total <= 0)
				throw Throw($"Source index {fromIndex} doesn't contain any documents.", searchResult.ApiCall);
			return searchResult;
		}


		private void CreateIndex(string resolvedFrom, string resolvedTo)
		{
			var originalIndexSettings = this._client.GetIndex(resolvedFrom);
			var originalIndexState = originalIndexSettings.Indices[resolvedFrom];
			var createIndexRequest = this._reindexRequest.CreateIndexRequest ?? new CreateIndexRequest(resolvedTo, originalIndexState);
			var createIndexResponse = this._client.CreateIndex(createIndexRequest);
			if (!createIndexResponse.IsValid)
				throw Throw($"Failed to create destination index {resolvedTo}.", createIndexResponse.ApiCall);
		}

		public IBulkResponse IndexSearchResults(ISearchResponse<T> searchResult, IObserver<IReindexResponse<T>> observer, IndexName toIndex, int page)
		{
			if (!searchResult.IsValid)
				throw Throw($"Indexing failed on scroll #{page}.", searchResult.ApiCall);

			var hits = searchResult.Hits.ToList();
			var bulkOperations = new List<IBulkOperation>(hits.Count);
			foreach (var h in hits)
			{
				var item = new BulkIndexOperation<T>(h.Source)
				{
					Type = h.Type,
					Index = toIndex,
					Id = h.Id,
					Routing = h.Routing,
				};
				if (h.Parent != null) item.Parent = h.Parent;
				this.Alter?.Invoke(h, h.Source, item);
				bulkOperations.Add(item);
			}

			var indexResult = this._client.Bulk(new BulkRequest { Operations = bulkOperations });
			if (!indexResult.IsValid)
				throw Throw($"Failed indexing page {page}.", indexResult.ApiCall);

			return indexResult;
		}

		public void Dispose()
		{
		}
	}
}

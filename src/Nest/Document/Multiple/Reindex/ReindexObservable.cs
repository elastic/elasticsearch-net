using System;
using Elasticsearch.Net;

namespace Nest
{
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
			var fromIndex = this._reindexRequest.From;
			var toIndex = this._reindexRequest.To;
			var scroll = this._reindexRequest.Scroll ?? TimeSpan.FromMinutes(2);
			var size = this._reindexRequest.Size ?? 100;

			var resolvedFrom = fromIndex.Resolve(this._connectionSettings);
			resolvedFrom.ThrowIfNullOrEmpty(nameof(fromIndex));
			var resolvedTo = toIndex.Resolve(this._connectionSettings);
			resolvedTo.ThrowIfNullOrEmpty(nameof(toIndex));

			var indexSettings = this._client.GetIndex(fromIndex);
			Func<CreateIndexDescriptor, ICreateIndexRequest> settings =  (ci) => this._reindexRequest.CreateIndexRequest ?? ci;
			var createIndexResponse = this._client.CreateIndex(
				toIndex, (c) => settings(c.InitializeUsing(indexSettings.Indices[resolvedFrom])));
			if (!createIndexResponse.IsValid)
				throw new ElasticsearchClientException(PipelineFailure.BadResponse, $"Failed to create destination index {toIndex}.", createIndexResponse.ApiCall);

			var page = 0;
			var searchResult = this._client.Search<T>(
				s => s
					.Index(fromIndex)
					.Type(this._reindexRequest.Type)
					.From(0)
					.Size(size)
					.Query(q=>this._reindexRequest.Query)
					.SearchType(SearchType.Scan)
					.Scroll(scroll)
				);
			if (searchResult.Total <= 0)
				throw new ElasticsearchClientException(PipelineFailure.BadResponse, $"Source index {fromIndex} doesn't contain any documents.", searchResult.ApiCall);

			IBulkResponse indexResult = null;
			do
			{
				var result = searchResult;
				searchResult = this._client.Scroll<T>(scroll, result.ScrollId);
				if (searchResult.Documents.HasAny())
					indexResult = this.IndexSearchResults(searchResult, observer, toIndex, page);
				page++;
			} while (searchResult.IsValid && indexResult != null && indexResult.IsValid && searchResult.Documents.HasAny());


			observer.OnCompleted();
		}

		public IBulkResponse IndexSearchResults(ISearchResponse<T> searchResult,IObserver<IReindexResponse<T>> observer, IndexName toIndex, int page)
		{
			if (!searchResult.IsValid)
				throw new ElasticsearchClientException(PipelineFailure.BadResponse, $"Indexing failed on scroll #{page}.", searchResult.ApiCall);

			var bb = new BulkDescriptor();
			foreach (var d in searchResult.Hits)
			{
				IHit<T> d1 = d;
				bb.Index<T>(bi => Index(bi, d1, toIndex));
			}

			var indexResult = this._client.Bulk(bb);
			if (!indexResult.IsValid)
				throw new ElasticsearchClientException(PipelineFailure.BadResponse, $"Failed indexing page {page}.", indexResult.ApiCall);

			observer.OnNext(new ReindexResponse<T>()
			{
				BulkResponse = indexResult,
				SearchResponse = searchResult,
				Scroll = page
			});
			return indexResult;
		}

		private static BulkIndexDescriptor<T> Index(BulkIndexDescriptor<T> descriptor, IHit<T> hit, IndexName toIndex)
		{
			descriptor
				.Document(hit.Source)
				.Type(hit.Type)
				.Index(toIndex)
				.Id(hit.Id)
				.Routing(hit.Routing)
				.Timestamp(hit.Timestamp);

			if (hit.Parent != null)
				descriptor.Parent(hit.Parent);

			if (hit.Ttl.HasValue)
				descriptor.Ttl(hit.Ttl.Value);

			return descriptor;
		}
	
		public void Dispose()
		{
		}
	}
}
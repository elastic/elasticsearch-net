using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Elasticsearch.Net;
using static Nest.Infer;

namespace Nest
{
	public class ReindexObservable<T> : IDisposable, IObservable<IBulkAllResponse> where T : class
	{
		private readonly IReindexRequest<T> _reindexRequest;
		private readonly IConnectionSettingsValues _connectionSettings;
		private readonly IElasticClient _client;

		private Action<long> _incrementSeenDocuments = l => { };
		private Action _incrementSeenScrollOperations = () => { };
		private readonly CancellationTokenSource _compositeCancelTokenSource;
		private readonly CancellationToken _compositeCancelToken;

		public ReindexObservable(IElasticClient client, IConnectionSettingsValues connectionSettings, IReindexRequest<T> reindexRequest, CancellationToken cancellationToken)
		{
			this._connectionSettings = connectionSettings;
			this._reindexRequest = reindexRequest;
			this._client = client;
			this._compositeCancelTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
			this._compositeCancelToken = this._compositeCancelTokenSource.Token;
		}

		public IDisposable Subscribe(ReindexObserver<T> observer)
		{
			this._incrementSeenDocuments = observer.IncrementSeenScrollDocuments;
			this._incrementSeenScrollOperations = observer.IncrementSeenScrollOperations;
			return this.Subscribe((IObserver<IBulkAllResponse>) observer);
		}

		public IDisposable Subscribe(IObserver<IBulkAllResponse> observer)
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

		private void Reindex(IObserver<IBulkAllResponse> observer)
		{
			var bulkMeta = this._reindexRequest.BulkAll?.Invoke(Enumerable.Empty<IHit<T>>());
			var scrollAll = this._reindexRequest.ScrollAll;
			var toIndex = bulkMeta?.Index.Resolve(this._connectionSettings);

			var slices = this.CreateIndex(toIndex, scrollAll);

			var backPressure = this.CreateBackPressure(bulkMeta, scrollAll, slices);

			var scrollDocuments = this.ScrollAll(slices, backPressure)
				.SelectMany(r => r.SearchResponse.Hits);

			var observableBulk = this.BulkAll(scrollDocuments, backPressure, toIndex);

			//by casting the observable can potentially store more meta data on the user provided observer
			var moreInfoObserver = observer as BulkAllObserver;
			if (moreInfoObserver != null)
				observableBulk.Subscribe(moreInfoObserver);
			else observableBulk.Subscribe(observer);
		}

		private BulkAllObservable<IHit<T>> BulkAll(IEnumerable<IHit<T>> scrollDocuments, ProducerConsumerBackPressure backPressure, string toIndex)
		{
			var bulkAllRequest = this._reindexRequest.BulkAll?.Invoke(scrollDocuments);
			if (bulkAllRequest == null)
				throw new Exception("Reindex can not commence BulkAll was not defined, we have no way of knowing how to index the documents now.");

			bulkAllRequest.BackPressure = backPressure;
			bulkAllRequest.BufferToBulk = (bulk, hits) =>
			{
				foreach (var hit in hits)
				{
					var item = new BulkIndexOperation<T>(hit.Source)
					{
						Type = hit.Type,
						Index = toIndex,
						Id = hit.Id,
						Routing = hit.Routing,
					};
					if (hit.Parent != null) item.Parent = hit.Parent;
					bulk.AddOperation(item);
				}
			};

			var observableBulk = this._client.BulkAll(bulkAllRequest, this._compositeCancelToken);
			return observableBulk;
		}

		private ProducerConsumerBackPressure CreateBackPressure(IBulkAllRequest<IHit<T>> bulkMeta, IScrollAllRequest scrollAll, int slices)
		{
			var backPressureFactor = this._reindexRequest.BackPressureFactor;
			var maxConcurrentConsumers = bulkMeta?.MaxDegreeOfParallelism ?? CoordinatedRequestDefaults.BulkAllMaxDegreeOfParallelismDefault;
			var maxConcurrentProducers = scrollAll?.MaxDegreeOfParallelism ?? slices;
			var maxConcurrency = Math.Min(maxConcurrentConsumers, maxConcurrentProducers);

			var bulkSize = bulkMeta?.Size ?? CoordinatedRequestDefaults.BulkAllSizeDefault;
			var searchSize = scrollAll?.Search?.Size ?? 10;
			var producerBandwidth = searchSize * maxConcurrency * backPressureFactor;
			var funnelTooSmall =  producerBandwidth < bulkSize;
			if (funnelTooSmall)
				throw new Exception("The back pressure settings are squeezing too hard "
					+ $"searchSize:{searchSize} * maxConcurrency:{maxConcurrency} * backPressureFactor:{backPressureFactor} = {producerBandwidth}"
					+ $" which is smaller then the bulkSize:{bulkSize}. Meaning the producers won't have the chance to produce enough documents "
					+ "for a single bulk operation"
				);

			var backPressure = new ProducerConsumerBackPressure(backPressureFactor, maxConcurrency);
			return backPressure;
		}

		private IEnumerable<IScrollAllResponse<T>> ScrollAll(int slices, ProducerConsumerBackPressure backPressure)
		{
			var scrollAll = this._reindexRequest.ScrollAll;
			var scroll = this._reindexRequest.ScrollAll?.ScrollTime ?? TimeSpan.FromMinutes(2);

			var scrollAllRequest = new ScrollAllRequest(scroll, slices)
			{
				RoutingField = scrollAll.RoutingField,
				MaxDegreeOfParallelism = scrollAll.MaxDegreeOfParallelism ?? slices,
				Search = scrollAll.Search,
				BackPressure = backPressure
			};

			var scrollObservable = this._client.ScrollAll<T>(scrollAllRequest, this._compositeCancelToken);
			return new GetEnumerator<IScrollAllResponse<T>>()
				.ToEnumerable(scrollObservable)
				.Select(ObserveScrollAllResponse);
		}

		private IScrollAllResponse<T> ObserveScrollAllResponse(IScrollAllResponse<T> response)
		{
			this._incrementSeenScrollOperations();
			this._incrementSeenDocuments(response.SearchResponse.Hits.Count);
			return response;
		}

		private static ElasticsearchClientException Throw(string message, IApiCallDetails details) =>
			new ElasticsearchClientException(PipelineFailure.BadResponse, message, details);

		private int CreateIndex(string toIndex, IScrollAllRequest scrollAll)
		{
			var fromIndices = this._reindexRequest.ScrollAll?.Search?.Index ?? Indices<T>();

			if (string.IsNullOrEmpty(toIndex))
				throw new Exception($"Can not resolve the target index name to reindex to make sure the bulk all operation describes one");

			var numberOfShards = this.CreateIndexIfNeeded(fromIndices, toIndex);

			var slices = scrollAll?.Slices ?? numberOfShards;
			if (scrollAll?.Slices < 0 && numberOfShards.HasValue)
				slices = numberOfShards;
			else if (scrollAll?.Slices < 0)
				throw new Exception("Slices is a negative number and no sane default could be inferred from the origin index's number_of_shards");
			if (!slices.HasValue)
				throw new Exception("Reindex can not commence because slices is not specified and we could not get a number of "
				                    + "shards hint from the source, usually this could happen if the scroll all points to multiple indices and no slices have been set");
			return slices.Value;
		}

		/// <summary>
		/// Tries to create the target index if it does not exist already, if the source points to a single index we'll
		/// use the original index settings to bootstrap the create index request if not provided
		/// </summary>
		/// <returns>Either the number of shards from to source or the target as a slice hint to ScrollAll</returns>
		private int? CreateIndexIfNeeded(Indices fromIndices, string resolvedTo)
		{
			if (this._reindexRequest.OmitIndexCreation) return null;

			var pointsToSingleSourceIndex = fromIndices.Match((a) => false, (m) => m.Indices.Count == 1);
			var targetExistsAlready = this._client.IndexExists(resolvedTo);
			if (targetExistsAlready.Exists) return null;

			this._compositeCancelToken.ThrowIfCancellationRequested();
			IndexState originalIndexState = null;
			var resolvedFrom = fromIndices.Resolve(this._connectionSettings);

			if (pointsToSingleSourceIndex)
			{
				var getIndexResponse = this._client.GetIndex(resolvedFrom);
				this._compositeCancelToken.ThrowIfCancellationRequested();
				originalIndexState = getIndexResponse.Indices[resolvedFrom];
				if (this._reindexRequest.OmitIndexCreation)
					return originalIndexState.Settings.NumberOfShards;

				// Black list internal settings that cannot be copied over
				// See https://github.com/elastic/elasticsearch/issues/21096
				originalIndexState.Settings.Remove("index.provided_name");
				originalIndexState.Settings.Remove("index.creation_date");
				originalIndexState.Settings.Remove("index.version.created");
			}

			var createIndexRequest = this._reindexRequest.CreateIndexRequest ??
			                         (originalIndexState != null
				                         ? new CreateIndexRequest(resolvedTo, originalIndexState)
				                         : new CreateIndexRequest(resolvedTo));
			var createIndexResponse = this._client.CreateIndex(createIndexRequest);
			this._compositeCancelToken.ThrowIfCancellationRequested();
			if (!createIndexResponse.IsValid)
				throw Throw($"Failed to create destination index {resolvedTo}.", createIndexResponse.ApiCall);

			return createIndexRequest.Settings?.NumberOfShards;
		}

		public bool IsDisposed { get; private set; }
		public void Dispose()
		{
			this.IsDisposed = true;
			this._compositeCancelTokenSource?.Cancel();
		}
	}
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public class BulkObservable<T> : IDisposable, IObservable<IBulkResponse> where T : class
	{
		private readonly IPartitionedBulkRequest<T> _partionedBulkRequest;
		private readonly IConnectionSettingsValues _connectionSettings;
		private readonly IElasticClient _client;
		private readonly TimeSpan _backOffTime;
		private readonly int _backOffRetries;
		private readonly int _bulkSize;
		private readonly int? _maxDegreeOfParallelism;
		private Action _incrementFailed = () => { };
		private Action _incrementRetries = () => { };

		private Func<IBulkResponse, bool> HandleErrors { get; set; }

		public BulkObservable(IElasticClient client, IConnectionSettingsValues connectionSettings, IPartitionedBulkRequest<T> partionedBulkRequest)
		{
			this._client = client;
			this._connectionSettings = connectionSettings;
			this._partionedBulkRequest = partionedBulkRequest;
			this._backOffRetries = this._partionedBulkRequest.NumberOfBackOffRetries.GetValueOrDefault(0);
			this._backOffTime = (this._partionedBulkRequest?.BackOff?.ToTimeSpan() ?? TimeSpan.FromMinutes(1));
			this._bulkSize = this._partionedBulkRequest.Size.HasValue ? this._partionedBulkRequest.Size.Value : 1000;
			this._maxDegreeOfParallelism = this._partionedBulkRequest.MaxDegreeOfParallelism;
		}

		public IDisposable Subscribe(PartitionedBulkObserver observer)
		{
			_incrementFailed = observer.IncrementTotalNumberOfFailedBuffers;
			_incrementRetries = observer.IncrementTotalNumberOfRetries;
			return this.Subscribe((IObserver<IBulkResponse>)observer);
		}


		public IDisposable Subscribe(IObserver<IBulkResponse> observer)
		{
			observer.ThrowIfNull(nameof(observer));
			try
			{
				this.PartitionedBulk(observer);
			}
			catch (Exception e)
			{
				observer.OnError(e);
			}
			return this;
		}

		private ElasticsearchClientException Throw(string message, IApiCallDetails details) =>
			new ElasticsearchClientException(PipelineFailure.BadResponse, message, details);


		private void PartitionedBulk(IObserver<IBulkResponse> observer)
		{
			var documents = this._partionedBulkRequest.Documents;
			var partioned = new PartitionHelper<T>(documents, this._bulkSize, this._maxDegreeOfParallelism);
			partioned.ForEachAsync(
				(buffer) => BulkAsync(buffer, 0),
				(buffer, response) => observer.OnNext(response),
				() =>
				{
					if (this._partionedBulkRequest.RefreshOnCompleted)
					{
						var refresh = this._client.Refresh(this._partionedBulkRequest.Index);
						if (!refresh.IsValid) throw Throw($"Refreshing after all documents have indexed failed", refresh.ApiCall);
					}
					observer.OnCompleted();

				}
			);
		}

		private async Task<IBulkResponse> BulkAsync(IList<T> buffer, int backOffRetries)
		{
			var r = this._partionedBulkRequest;
			var response = await this._client.BulkAsync(s =>
			{
				s.IndexMany(buffer).Index(r.Index).Type(r.Type);
				if (!string.IsNullOrEmpty(r.Pipeline)) s.Pipeline(r.Pipeline);
				if (r.Refresh) s.Refresh(r.Refresh);
				if (!string.IsNullOrEmpty(r.Routing)) s.Pipeline(r.Routing);
				if (r.Consistency.HasValue) s.Consistency(r.Consistency.Value);
				return s;
			});
			if (!response.IsValid && backOffRetries < this._backOffRetries)
			{
				this._incrementRetries();
				//wait before or after fishing out retriable docs?
				await Task.Delay(this._backOffTime);
				var retryDocuments = response.Items.Zip(buffer, (i, d) => new { i, d })
					.Where(x => x.i.Status == 429)
					.Select(x => x.d)
					.ToList();

				return await this.BulkAsync(retryDocuments, ++backOffRetries);
			}
			else if (!response.IsValid)
			{
				this._incrementFailed();
				throw Throw($"Bulk indexing failed and after retrying {backOffRetries} times", response.ApiCall);
			}
			return response;
		}

		private sealed class PartitionHelper<TDocument> : IEnumerable<IList<TDocument>>
		{
			readonly IEnumerable<TDocument> _items;
			readonly int _partitionSize;
			readonly int? _semaphoreSize;
			bool _hasMoreItems;

			internal PartitionHelper(IEnumerable<TDocument> i, int ps, int? semaphoreSize)
			{
				_items = i;
				_semaphoreSize = semaphoreSize;
				_partitionSize = ps;
			}

			IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
			public IEnumerator<IList<TDocument>> GetEnumerator()
			{
				using (var enumerator = _items.GetEnumerator())
				{
					_hasMoreItems = enumerator.MoveNext();
					while (_hasMoreItems)
						yield return GetNextBatch(enumerator).ToList();
				}
			}

			IEnumerable<TDocument> GetNextBatch(IEnumerator<TDocument> enumerator)
			{
				for (int i = 0; i < _partitionSize; ++i)
				{
					yield return enumerator.Current;
					_hasMoreItems = enumerator.MoveNext();
					if (!_hasMoreItems) yield break;
				}
			}

			public Task ForEachAsync<TResult>(
				Func<IList<TDocument>, Task<TResult>> taskSelector,
				Action<IList<TDocument>, TResult> resultProcessor,
				Action onCompleted
			)
			{
				var semaphore = _semaphoreSize.HasValue
					? new SemaphoreSlim(initialCount: _semaphoreSize.Value, maxCount: _semaphoreSize.Value)
					: null;
				return Task.WhenAll(
						from item in this
						select ProcessAsync(item, taskSelector, resultProcessor, semaphore)
					)
					.ContinueWith((t) => onCompleted());
			}

			private async Task ProcessAsync<TSource, TResult>(
				TSource item,
				Func<TSource, Task<TResult>> taskSelector,
				Action<TSource, TResult> resultProcessor,
				SemaphoreSlim semaphoreSlim)
			{
				if (semaphoreSlim != null) await semaphoreSlim.WaitAsync();
				try
				{
					var result = await taskSelector(item);
					resultProcessor(item, result);
				}
				finally { if (semaphoreSlim != null) semaphoreSlim.Release(); }
			}
		}

		public void Dispose() { }
	}
}

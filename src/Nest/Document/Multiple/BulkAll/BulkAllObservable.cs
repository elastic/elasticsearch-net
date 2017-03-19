using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public class BulkAllObservable<T> : IDisposable, IObservable<IBulkAllResponse> where T : class
	{
		private readonly IBulkAllRequest<T> _partionedBulkRequest;
		private readonly IConnectionSettingsValues _connectionSettings;
		private readonly IElasticClient _client;
		private readonly TimeSpan _backOffTime;
		private readonly int _backOffRetries;
		private readonly int _bulkSize;
		private readonly int _maxDegreeOfParallelism;
		private Action _incrementFailed = () => { };
		private Action _incrementRetries = () => { };

		private readonly CancellationToken _cancelToken;
		private readonly CancellationToken _compositeCancelToken;
		private readonly CancellationTokenSource _compositeCancelTokenSource;

		private Func<IBulkResponse, bool> HandleErrors { get; set; }

		public BulkAllObservable(
			IElasticClient client,
			IConnectionSettingsValues connectionSettings,
			IBulkAllRequest<T> partionedBulkRequest,
			CancellationToken cancellationToken = default(CancellationToken)
			)
		{
			this._client = client;
			this._connectionSettings = connectionSettings;
			this._partionedBulkRequest = partionedBulkRequest;
			this._backOffRetries = this._partionedBulkRequest.BackOffRetries.GetValueOrDefault(0);
			this._backOffTime = (this._partionedBulkRequest?.BackOffTime?.ToTimeSpan() ?? TimeSpan.FromMinutes(1));
			this._bulkSize = this._partionedBulkRequest.Size ?? 1000;
			this._maxDegreeOfParallelism = this._partionedBulkRequest.MaxDegreeOfParallelism.HasValue
				? this._partionedBulkRequest.MaxDegreeOfParallelism.Value
				: 20;
			this._cancelToken = cancellationToken;
			this._compositeCancelTokenSource = CancellationTokenSource.CreateLinkedTokenSource(this._cancelToken);
			this._compositeCancelToken = this._compositeCancelTokenSource.Token;
		}

		public IDisposable Subscribe(BulkAllObserver observer)
		{
			_incrementFailed = observer.IncrementTotalNumberOfFailedBuffers;
			_incrementRetries = observer.IncrementTotalNumberOfRetries;
			return this.Subscribe((IObserver<IBulkAllResponse>)observer);
		}

		public IDisposable Subscribe(IObserver<IBulkAllResponse> observer)
		{
			observer.ThrowIfNull(nameof(observer));
			this.BulkAll(observer);
			return this;
		}

		private ElasticsearchClientException Throw(string message, IApiCallDetails details) =>
			new ElasticsearchClientException(PipelineFailure.BadResponse, message, details);

		private void BulkAll(IObserver<IBulkAllResponse> observer)
		{
			var documents = this._partionedBulkRequest.Documents;
			var partioned = new PartitionHelper<T>(documents, this._bulkSize, this._maxDegreeOfParallelism);
#pragma warning disable 4014
			partioned.ForEachAsync(
#pragma warning restore 4014
				(buffer, page) => this.BulkAsync(buffer, page, 0),
				(buffer, response) => observer.OnNext(response),
				ex => OnCompleted(ex, observer)
			);
		}

		private void OnCompleted(Exception exception, IObserver<IBulkAllResponse> observer)
		{
			if (exception != null)
				observer.OnError(exception);
			else
			{
				try
				{
					RefreshOnCompleted();
					observer.OnCompleted();
				}
				catch (Exception e)
				{
					observer.OnError(e);
				}
			}
		}

		private void RefreshOnCompleted()
		{
			if (!this._partionedBulkRequest.RefreshOnCompleted) return;
			var refresh = this._client.Refresh(this._partionedBulkRequest.Index);
			if (!refresh.IsValid) throw Throw($"Refreshing after all documents have indexed failed", refresh.ApiCall);
		}

		private async Task<IBulkAllResponse> BulkAsync(IList<T> buffer, long page, int backOffRetries)
		{
			this._compositeCancelToken.ThrowIfCancellationRequested();

			var r = this._partionedBulkRequest;
			var response = await this._client.BulkAsync(s =>
			{
				s.Index(r.Index).Type(r.Type);
				if (r.BufferToBulk != null) r.BufferToBulk(s, buffer);
				else s.IndexMany(buffer);
				if (r.Refresh.HasValue) s.Refresh(r.Refresh.Value);
				if (!string.IsNullOrEmpty(r.Routing)) s.Routing(r.Routing);
				if (r.Consistency.HasValue) s.Consistency(r.Consistency.Value);


				return s;
			}).ConfigureAwait(false);

			this._compositeCancelToken.ThrowIfCancellationRequested();
			if (!response.IsValid && backOffRetries < this._backOffRetries)
			{
				this._incrementRetries();
				//wait before or after fishing out retriable docs?
				await Task.Delay(this._backOffTime, this._compositeCancelToken).ConfigureAwait(false);
				var retryDocuments = response.Items.Zip(buffer, (i, d) => new { i, d })
					.Where(x => x.i.Status == 429)
					.Select(x => x.d)
					.ToList();

				return await this.BulkAsync(retryDocuments, page, ++backOffRetries).ConfigureAwait(false);
			}
			else if (!response.IsValid)
			{
				this._incrementFailed();
				throw Throw($"Bulk indexing failed and after retrying {backOffRetries} times", response.ApiCall);
			}
			return new BulkAllResponse { Retries = backOffRetries, Page = page };
		}

		private sealed class PartitionHelper<TDocument> : IEnumerable<IList<TDocument>>
		{
			readonly IEnumerable<TDocument> _items;
			readonly int _partitionSize;
			readonly int _semaphoreSize;
			bool _hasMoreItems;

			internal PartitionHelper(IEnumerable<TDocument> i, int ps, int semaphoreSize)
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


			public async Task ForEachAsync<TResult>(
				Func<IList<TDocument>, long, Task<TResult>> taskSelector,
				Action<IList<TDocument>, TResult> resultProcessor,
				Action<Exception> done
			)
			{
				var semaphore = new SemaphoreSlim(initialCount: _semaphoreSize, maxCount: _semaphoreSize);
				long page = 0;

				try
				{
					var tasks = new List<Task>();
					foreach (var item in this)
					{
						tasks.Add(ProcessAsync(item, taskSelector, resultProcessor, semaphore, page++));
						if (tasks.Count <= _semaphoreSize)
							continue;

						var task = await Task.WhenAny(tasks);
						tasks.Remove(task);
					}

					await Task.WhenAll(tasks);
					done(null);
				}
				catch (Exception e)
				{
					done(e);
					throw;
				}
			}

			private async Task ProcessAsync<TSource, TResult>(
				TSource item,
				Func<TSource, long, Task<TResult>> taskSelector,
				Action<TSource, TResult> resultProcessor,
				SemaphoreSlim semaphoreSlim,
				long page)
			{
				if (semaphoreSlim != null) await semaphoreSlim.WaitAsync().ConfigureAwait(false);
				try
				{
					var result = await taskSelector(item, page).ConfigureAwait(false);
					resultProcessor(item, result);
				}
				finally { if (semaphoreSlim != null) semaphoreSlim.Release(); }
			}
		}

		public bool IsDisposed { get; private set; }
		public void Dispose()
		{
			this.IsDisposed = true;
			this._compositeCancelTokenSource?.Cancel();
		}
	}
}

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
		private System.Action _incrementFailed = () => { };
		private System.Action _incrementRetries = () => { };

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
			this._maxDegreeOfParallelism = _partionedBulkRequest.MaxDegreeOfParallelism ?? 20;
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
			try
			{
				this.BulkAll(observer);
			}
			catch (Exception e)
			{
				observer.OnError(e);
			}
			return this;
		}

		private ElasticsearchClientException Throw(string message, IApiCallDetails details) =>
			new ElasticsearchClientException(PipelineFailure.BadResponse, message, details);

		private void BulkAll(IObserver<IBulkAllResponse> observer)
		{
			var documents = this._partionedBulkRequest.Documents;
			var partioned = new PartitionHelper<T>(documents, this._bulkSize, this._maxDegreeOfParallelism);
			partioned.ForEachAsync(
				(buffer, page) => this.BulkAsync(buffer, page, 0),
				(buffer, response) => observer.OnNext(response),
				t => OnCompleted(t, observer)
			);
		}

		private void OnCompleted(Task task, IObserver<IBulkAllResponse> observer)
		{
			switch (task.Status)
			{
				case System.Threading.Tasks.TaskStatus.RanToCompletion:
					if (this._partionedBulkRequest.RefreshOnCompleted)
					{
						var refresh = this._client.Refresh(this._partionedBulkRequest.Index);
						if (!refresh.IsValid) throw Throw($"Refreshing after all documents have indexed failed", refresh.ApiCall);
					}
					observer.OnCompleted();
					break;
				case System.Threading.Tasks.TaskStatus.Faulted:
					observer.OnError(task.Exception.InnerException);
					break;
				case System.Threading.Tasks.TaskStatus.Canceled:
					observer.OnError(new TaskCanceledException(task));
					break;
			}
		}

		private async Task<IBulkAllResponse> BulkAsync(IList<T> buffer, long page, int backOffRetries)
		{
			this._compositeCancelToken.ThrowIfCancellationRequested();

			var r = this._partionedBulkRequest;
			var response = await this._client.BulkAsync(s =>
			{
				s.IndexMany(buffer);
				s.Index(r.Index).Type(r.Type);
				if (!string.IsNullOrEmpty(r.Pipeline)) s.Pipeline(r.Pipeline);
				if (r.Refresh.HasValue) s.Refresh(r.Refresh.Value);
				if (!string.IsNullOrEmpty(r.Routing)) s.Routing(r.Routing);
				if (r.WaitForActiveShards.HasValue) s.WaitForActiveShards(r.WaitForActiveShards.ToString());
				return s;
			}, this._compositeCancelToken).ConfigureAwait(false);

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

			private IEnumerable<TDocument> GetNextBatch(IEnumerator<TDocument> enumerator)
			{
				for (int i = 0; i < _partitionSize; ++i)
				{
					yield return enumerator.Current;
					_hasMoreItems = enumerator.MoveNext();
					if (!_hasMoreItems) yield break;
				}
			}

			public Task ForEachAsync<TResult>(
				Func<IList<TDocument>, long, Task<TResult>> taskSelector,
				Action<IList<TDocument>, TResult> resultProcessor,
				Action<Task> done
			)
			{
				var semaphore = new SemaphoreSlim(initialCount: _semaphoreSize, maxCount: _semaphoreSize);
				long page = 0;
				return Task.WhenAll(
						from item in this
						select ProcessAsync(item, taskSelector, resultProcessor, semaphore, page++)
					).ContinueWith(done);
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
				catch
				{
					throw;
				}
				finally
				{
					semaphoreSlim?.Release();
				}
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

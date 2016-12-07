using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest.CommonAbstractions.Reactive;

namespace Nest
{
	public class BulkAllObservable<T> : IDisposable, IObservable<IBulkAllResponse> where T : class
	{
		private readonly IBulkAllRequest<T> _partionedBulkRequest;
		private readonly IElasticClient _client;
		private readonly TimeSpan _backOffTime;
		private readonly int _backOffRetries;
		private readonly int _bulkSize;
		private readonly int _maxDegreeOfParallelism;
		private Action _incrementFailed = () => { };
		private Action _incrementRetries = () => { };

		private readonly CancellationToken _compositeCancelToken;
		private readonly CancellationTokenSource _compositeCancelTokenSource;

		public BulkAllObservable(
			IElasticClient client,
			IBulkAllRequest<T> partionedBulkRequest,
			CancellationToken cancellationToken = default(CancellationToken)
			)
		{
			this._client = client;
			this._partionedBulkRequest = partionedBulkRequest;
			this._backOffRetries = this._partionedBulkRequest.BackOffRetries.GetValueOrDefault(CoordinatedRequestDefaults.BulkAllBackOffRetriesDefault);
			this._backOffTime = (this._partionedBulkRequest?.BackOffTime?.ToTimeSpan() ??  CoordinatedRequestDefaults.BulkAllBackOffTimeDefault);
			this._bulkSize = this._partionedBulkRequest.Size ?? CoordinatedRequestDefaults.BulkAllSizeDefault;
			this._maxDegreeOfParallelism = _partionedBulkRequest.MaxDegreeOfParallelism ?? CoordinatedRequestDefaults.BulkAllMaxDegreeOfParallelismDefault;

			this._compositeCancelTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
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

		private static ElasticsearchClientException Throw(string message, IApiCallDetails details) =>
			new ElasticsearchClientException(PipelineFailure.BadResponse, message, details);

		private void BulkAll(IObserver<IBulkAllResponse> observer)
		{
			var documents = this._partionedBulkRequest.Documents;
			var partioned = new PartitionHelper<T>(documents, this._bulkSize);
			partioned.ForEachAsync(
				(buffer, page) => this.BulkAsync(buffer, page, 0),
				(buffer, response) => observer.OnNext(response),
				t => OnCompleted(t, observer),
				this._maxDegreeOfParallelism
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
				default:
					throw new ArgumentOutOfRangeException();
			}
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
				this._partionedBulkRequest.BackPressure?.Release();
				throw Throw($"Bulk indexing failed and after retrying {backOffRetries} times", response.ApiCall);
			}
			this._partionedBulkRequest.BackPressure?.Release();
			return new BulkAllResponse { Retries = backOffRetries, Page = page };
		}

		public bool IsDisposed { get; private set; }
		public void Dispose()
		{
			this.IsDisposed = true;
			this._compositeCancelTokenSource?.Cancel();
		}
	}
}

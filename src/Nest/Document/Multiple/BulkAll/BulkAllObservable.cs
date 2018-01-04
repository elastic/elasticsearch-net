using System;
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
		private readonly IElasticClient _client;
		private readonly TimeSpan _backOffTime;
		private readonly int _backOffRetries;
		private readonly int _bulkSize;
		private readonly int _maxDegreeOfParallelism;
		private Action _incrementFailed = () => { };
		private Action _incrementRetries = () => { };

		private readonly CancellationToken _compositeCancelToken;
		private readonly CancellationTokenSource _compositeCancelTokenSource;
		private readonly Func<IBulkResponseItem, T, bool> _retryPredicate;

		public BulkAllObservable(
			IElasticClient client,
			IBulkAllRequest<T> partionedBulkRequest,
			CancellationToken cancellationToken = default(CancellationToken)
		)
		{
			this._client = client;
			this._partionedBulkRequest = partionedBulkRequest;
			this._backOffRetries = this._partionedBulkRequest.BackOffRetries.GetValueOrDefault(CoordinatedRequestDefaults.BulkAllBackOffRetriesDefault);
			this._backOffTime = (this._partionedBulkRequest?.BackOffTime?.ToTimeSpan() ?? CoordinatedRequestDefaults.BulkAllBackOffTimeDefault);
			this._bulkSize = this._partionedBulkRequest.Size ?? CoordinatedRequestDefaults.BulkAllSizeDefault;
			this._retryPredicate = this._partionedBulkRequest.RetryDocumentPredicate ?? RetryBulkActionPredicate;
			this._maxDegreeOfParallelism = _partionedBulkRequest.MaxDegreeOfParallelism ?? CoordinatedRequestDefaults.BulkAllMaxDegreeOfParallelismDefault;
			this._compositeCancelTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
			this._compositeCancelToken = this._compositeCancelTokenSource.Token;
		}

		public IDisposable Subscribe(BulkAllObserver observer)
		{
			_incrementFailed = observer.IncrementTotalNumberOfFailedBuffers;
			_incrementRetries = observer.IncrementTotalNumberOfRetries;
			return this.Subscribe((IObserver<IBulkAllResponse>) observer);
		}

		public IDisposable Subscribe(IObserver<IBulkAllResponse> observer)
		{
			observer.ThrowIfNull(nameof(observer));
			this.BulkAll(observer);
			return this;
		}

		private static ElasticsearchClientException Throw(string message, IApiCallDetails details) =>
			new ElasticsearchClientException(PipelineFailure.BadResponse, message, details);

		private void BulkAll(IObserver<IBulkAllResponse> observer)
		{
			var documents = this._partionedBulkRequest.Documents;
			var partioned = new PartitionHelper<T>(documents, this._bulkSize);
#pragma warning disable 4014
			partioned.ForEachAsync(
#pragma warning restore 4014
				(buffer, page) => this.BulkAsync(buffer, page, 0),
				(buffer, response) => observer.OnNext(response),
				ex => OnCompleted(ex, observer),
				this._maxDegreeOfParallelism
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
					if (!string.IsNullOrEmpty(r.Pipeline)) s.Pipeline(r.Pipeline);
					if (r.Refresh.HasValue) s.Refresh(r.Refresh.Value);
					if (r.Routing != null) s.Routing(r.Routing);
					if (r.WaitForActiveShards.HasValue) s.WaitForActiveShards(r.WaitForActiveShards.ToString());

					return s;
				}, this._compositeCancelToken)
				.ConfigureAwait(false);

			this._compositeCancelToken.ThrowIfCancellationRequested();

			var retryDocuments = response.Items.Zip(buffer, (i, d) => new {i, d})
				.Where(x=> !response.IsValid && this._retryPredicate(x.i, x.d))
				.Select(x => x.d)
				.ToList();

			if (retryDocuments.Count > 0 && backOffRetries < this._backOffRetries)
			{
				this._incrementRetries();
				await Task.Delay(this._backOffTime, this._compositeCancelToken).ConfigureAwait(false);
				return await this.BulkAsync(retryDocuments, page, ++backOffRetries).ConfigureAwait(false);
			}
			if (retryDocuments.Count > 0)
			{
				this._incrementFailed();
				this._partionedBulkRequest.BackPressure?.Release();
				throw Throw($"Bulk indexing failed and after retrying {backOffRetries} times", response.ApiCall);
			}
			this._partionedBulkRequest.BackPressure?.Release();
			return new BulkAllResponse {Retries = backOffRetries, Page = page};
		}

		private static bool RetryBulkActionPredicate(IBulkResponseItem bulkResponseItem, T d) => bulkResponseItem.Status == 429;

		public bool IsDisposed { get; private set; }

		public void Dispose()
		{
			this.IsDisposed = true;
			this._compositeCancelTokenSource?.Cancel();
		}
	}
}

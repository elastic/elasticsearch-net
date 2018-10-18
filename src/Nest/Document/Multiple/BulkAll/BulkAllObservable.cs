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
		private readonly IBulkAllRequest<T> _partitionedBulkRequest;
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
		private readonly Action<IBulkResponseItem, T> _droppedDocumentCallBack;

		public BulkAllObservable(
			IElasticClient client,
			IBulkAllRequest<T> partitionedBulkRequest,
			CancellationToken cancellationToken = default(CancellationToken)
		)
		{
			this._client = client;
			this._partitionedBulkRequest = partitionedBulkRequest;
			this._backOffRetries = this._partitionedBulkRequest.BackOffRetries.GetValueOrDefault(CoordinatedRequestDefaults.BulkAllBackOffRetriesDefault);
			this._backOffTime = (this._partitionedBulkRequest?.BackOffTime?.ToTimeSpan() ?? CoordinatedRequestDefaults.BulkAllBackOffTimeDefault);
			this._bulkSize = this._partitionedBulkRequest.Size ?? CoordinatedRequestDefaults.BulkAllSizeDefault;
			this._retryPredicate = this._partitionedBulkRequest.RetryDocumentPredicate ?? RetryBulkActionPredicate;
			this._droppedDocumentCallBack = this._partitionedBulkRequest.DroppedDocumentCallback ?? DroppedDocumentCallbackDefault;
			this._maxDegreeOfParallelism = _partitionedBulkRequest.MaxDegreeOfParallelism ?? CoordinatedRequestDefaults.BulkAllMaxDegreeOfParallelismDefault;
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

		private void BulkAll(IObserver<IBulkAllResponse> observer)
		{
			var documents = this._partitionedBulkRequest.Documents;
			var partitioned = new PartitionHelper<T>(documents, this._bulkSize);
#pragma warning disable 4014
			partitioned.ForEachAsync(
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
			if (!this._partitionedBulkRequest.RefreshOnCompleted) return;
			var indices = this._partitionedBulkRequest.RefreshIndices ?? this._partitionedBulkRequest.Index;
			if (indices == null) return;
			var refresh = this._client.Refresh(indices);
			if (!refresh.IsValid) throw Throw($"Refreshing after all documents have indexed failed", refresh.ApiCall);
		}

		private async Task<IBulkAllResponse> BulkAsync(IList<T> buffer, long page, int backOffRetries)
		{
			this._compositeCancelToken.ThrowIfCancellationRequested();

			var r = this._partitionedBulkRequest;
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

			if (!response.ApiCall.Success)
				return await this.HandleBulkRequest(buffer, page, backOffRetries, response);

			var documentsWithResponse = response.Items.Zip(buffer, Tuple.Create).ToList();

			this.HandleDroppedDocuments(documentsWithResponse, response);

			var retryDocuments = documentsWithResponse
				.Where(x=> !x.Item1.IsValid && this._retryPredicate(x.Item1, x.Item2))
				.Select(x => x.Item2)
				.ToList();

			if (retryDocuments.Count > 0 && backOffRetries < this._backOffRetries)
				return await this.RetryDocuments(page, ++backOffRetries, retryDocuments);
			else if (retryDocuments.Count > 0)
				throw this.ThrowOnBadBulk(response, $"Bulk indexing failed and after retrying {backOffRetries} times");

			this._partitionedBulkRequest.BackPressure?.Release();
			return new BulkAllResponse { Retries = backOffRetries, Page = page };
		}

		private void HandleDroppedDocuments(List<Tuple<IBulkResponseItem, T>> documentsWithResponse, IBulkResponse response)
		{
			var droppedDocuments = documentsWithResponse
				.Where(x => !x.Item1.IsValid && !this._retryPredicate(x.Item1, x.Item2))
				.ToList();
			if (droppedDocuments.Count <= 0) return;
			foreach (var dropped in droppedDocuments) this._droppedDocumentCallBack(dropped.Item1, dropped.Item2);
			if (!this._partitionedBulkRequest.ContinueAfterDroppedDocuments)
				throw this.ThrowOnBadBulk(response, $"BulkAll halted after receiving failures that can not be retried from _bulk");
		}

		private async Task<IBulkAllResponse> HandleBulkRequest(IList<T> buffer, long page, int backOffRetries, IBulkResponse response)
		{
			var clientException = response.ApiCall.OriginalException as ElasticsearchClientException;
			//TODO expose this on IAPiCallDetails as RetryLater in 7.0?
			var failureReason = clientException?.FailureReason.GetValueOrDefault(PipelineFailure.Unexpected);
			switch (failureReason)
			{
				case PipelineFailure.MaxRetriesReached:
					//TODO move this to its own PipelineFailure classification in 7.0
					if (response.ApiCall.AuditTrail.Last().Event == AuditEvent.FailedOverAllNodes)
						throw this.ThrowOnBadBulk(response, $"BulkAll halted after attempted bulk failed over all the active nodes");
					return await this.RetryDocuments(page, ++backOffRetries, buffer);
				case PipelineFailure.CouldNotStartSniffOnStartup:
				case PipelineFailure.BadAuthentication:
				case PipelineFailure.NoNodesAttempted:
				case PipelineFailure.SniffFailure:
				case PipelineFailure.Unexpected:
					throw this.ThrowOnBadBulk(response,
						$"BulkAll halted after {nameof(PipelineFailure)}{failureReason.GetStringValue()} from _bulk");
				default:
					return await this.RetryDocuments(page, ++backOffRetries, buffer);
			}
		}

		private async Task<IBulkAllResponse> RetryDocuments(long page, int backOffRetries, IList<T> retryDocuments)
		{
			this._incrementRetries();
			await Task.Delay(this._backOffTime, this._compositeCancelToken).ConfigureAwait(false);
			return await this.BulkAsync(retryDocuments, page, backOffRetries).ConfigureAwait(false);
		}

		private Exception ThrowOnBadBulk(IElasticsearchResponse response, string message)
		{
			this._incrementFailed();
			this._partitionedBulkRequest.BackPressure?.Release();
			return Throw(message, response.ApiCall);
		}
		private static ElasticsearchClientException Throw(string message, IApiCallDetails details) =>
			new ElasticsearchClientException(PipelineFailure.BadResponse, message, details);


		private static bool RetryBulkActionPredicate(IBulkResponseItem bulkResponseItem, T d) => bulkResponseItem.Status == 429;

		private static void DroppedDocumentCallbackDefault(IBulkResponseItem bulkResponseItem, T d) { }

		public bool IsDisposed { get; private set; }

		public void Dispose()
		{
			this.IsDisposed = true;
			this._compositeCancelTokenSource?.Cancel();
			this._compositeCancelTokenSource?.Dispose();
		}
	}
}

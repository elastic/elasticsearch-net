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
		private readonly int _backOffRetries;
		private readonly TimeSpan _backOffTime;
		private readonly int _bulkSize;
		private readonly IElasticClient _client;

		private readonly CancellationToken _compositeCancelToken;
		private readonly CancellationTokenSource _compositeCancelTokenSource;
		private readonly int _maxDegreeOfParallelism;
		private readonly IBulkAllRequest<T> _partionedBulkRequest;
		private readonly Func<BulkResponseItemBase, T, bool> _retryPredicate;
		private readonly Action<BulkResponseItemBase, T> _droppedDocumentCallBack;
		private Action _incrementFailed = () => { };
		private Action _incrementRetries = () => { };

		public BulkAllObservable(
			IElasticClient client,
			IBulkAllRequest<T> partionedBulkRequest,
			CancellationToken cancellationToken = default(CancellationToken)
		)
		{
			_client = client;
			_partionedBulkRequest = partionedBulkRequest;
			_backOffRetries = _partionedBulkRequest.BackOffRetries.GetValueOrDefault(CoordinatedRequestDefaults.BulkAllBackOffRetriesDefault);
			_backOffTime = _partionedBulkRequest?.BackOffTime?.ToTimeSpan() ?? CoordinatedRequestDefaults.BulkAllBackOffTimeDefault;
			_bulkSize = _partionedBulkRequest.Size ?? CoordinatedRequestDefaults.BulkAllSizeDefault;
			_retryPredicate = _partionedBulkRequest.RetryDocumentPredicate ?? RetryBulkActionPredicate;
			_droppedDocumentCallBack = _partionedBulkRequest.DroppedDocumentCallback ?? DroppedDocumentCallbackDefault;
			_maxDegreeOfParallelism = _partionedBulkRequest.MaxDegreeOfParallelism ?? CoordinatedRequestDefaults.BulkAllMaxDegreeOfParallelismDefault;

			_compositeCancelTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
			_compositeCancelToken = _compositeCancelTokenSource.Token;
		}

		public bool IsDisposed { get; private set; }

		public void Dispose()
		{
			IsDisposed = true;
			_compositeCancelTokenSource?.Cancel();
		}

		public IDisposable Subscribe(IObserver<IBulkAllResponse> observer)
		{
			observer.ThrowIfNull(nameof(observer));
			BulkAll(observer);
			return this;
		}

		public IDisposable Subscribe(BulkAllObserver observer)
		{
			_incrementFailed = observer.IncrementTotalNumberOfFailedBuffers;
			_incrementRetries = observer.IncrementTotalNumberOfRetries;
			return Subscribe((IObserver<IBulkAllResponse>)observer);
		}

		private void BulkAll(IObserver<IBulkAllResponse> observer)
		{
			var documents = _partionedBulkRequest.Documents;
			var partioned = new PartitionHelper<T>(documents, _bulkSize);
#pragma warning disable 4014
			partioned.ForEachAsync(
#pragma warning restore 4014
				(buffer, page) => BulkAsync(buffer, page, 0),
				(buffer, response) => observer.OnNext(response),
				ex => OnCompleted(ex, observer),
				_maxDegreeOfParallelism
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
			if (!_partionedBulkRequest.RefreshOnCompleted) return;

			var refresh = _client.Refresh(_partionedBulkRequest.Index);
			if (!refresh.IsValid) throw Throw($"Refreshing after all documents have indexed failed", refresh.ApiCall);
		}

		private async Task<IBulkAllResponse> BulkAsync(IList<T> buffer, long page, int backOffRetries)
		{
			_compositeCancelToken.ThrowIfCancellationRequested();

			var r = _partionedBulkRequest;
			var response = await _client.BulkAsync(s =>
				{
					s.Index(r.Index).Type(r.Type);
					if (r.BufferToBulk != null) r.BufferToBulk(s, buffer);
					else s.IndexMany(buffer);
					if (!string.IsNullOrEmpty(r.Pipeline)) s.Pipeline(r.Pipeline);
					if (r.Refresh.HasValue) s.Refresh(r.Refresh.Value);
					if (!string.IsNullOrEmpty(r.Routing)) s.Routing(r.Routing);
					if (r.WaitForActiveShards.HasValue) s.WaitForActiveShards(r.WaitForActiveShards.ToString());

					return s;
				}, _compositeCancelToken)
				.ConfigureAwait(false);

			_compositeCancelToken.ThrowIfCancellationRequested();

			if (!response.ApiCall.Success)
				return await HandleBulkRequest(buffer, page, backOffRetries, response);

			var documentsWithResponse = response.Items.Zip(buffer, Tuple.Create).ToList();

			HandleDroppedDocuments(documentsWithResponse, response);

			var retryDocuments = documentsWithResponse
				.Where(x => !x.Item1.IsValid && _retryPredicate(x.Item1, x.Item2))
				.Select(x => x.Item2)
				.ToList();

			if (retryDocuments.Count > 0 && backOffRetries < _backOffRetries)
				return await RetryDocuments(page, ++backOffRetries, retryDocuments);
			else if (retryDocuments.Count > 0)
				throw ThrowOnBadBulk(response, $"Bulk indexing failed and after retrying {backOffRetries} times");

			_partionedBulkRequest.BackPressure?.Release();
			return new BulkAllResponse { Retries = backOffRetries, Page = page };
		}

		private void HandleDroppedDocuments(List<Tuple<BulkResponseItemBase, T>> documentsWithResponse, IBulkResponse response)
		{
			var droppedDocuments = documentsWithResponse
				.Where(x => !x.Item1.IsValid && !_retryPredicate(x.Item1, x.Item2))
				.ToList();
			if (droppedDocuments.Count <= 0) return;

			foreach (var dropped in droppedDocuments) _droppedDocumentCallBack(dropped.Item1, dropped.Item2);
			if (!_partionedBulkRequest.ContinueAfterDroppedDocuments)
				throw ThrowOnBadBulk(response, $"BulkAll halted after receiving failures that can not be retried from _bulk");
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
						throw ThrowOnBadBulk(response, $"BulkAll halted after attempted bulk failed over all the active nodes");

					return await RetryDocuments(page, ++backOffRetries, buffer);
				case PipelineFailure.CouldNotStartSniffOnStartup:
				case PipelineFailure.BadAuthentication:
				case PipelineFailure.NoNodesAttempted:
				case PipelineFailure.SniffFailure:
				case PipelineFailure.Unexpected:
					throw ThrowOnBadBulk(response,
						$"BulkAll halted after {nameof(PipelineFailure)}{failureReason.GetStringValue()} from _bulk");
				default:
					return await RetryDocuments(page, ++backOffRetries, buffer);
			}
		}

		private async Task<IBulkAllResponse> RetryDocuments(long page, int backOffRetries, IList<T> retryDocuments)
		{
			_incrementRetries();
			await Task.Delay(_backOffTime, _compositeCancelToken).ConfigureAwait(false);
			return await BulkAsync(retryDocuments, page, backOffRetries).ConfigureAwait(false);
		}

		private Exception ThrowOnBadBulk(IBodyWithApiCallDetails response, string message)
		{
			_incrementFailed();
			_partionedBulkRequest.BackPressure?.Release();
			return Throw(message, response.ApiCall);
		}

		private static ElasticsearchClientException Throw(string message, IApiCallDetails details) =>
			new ElasticsearchClientException(PipelineFailure.BadResponse, message, details);


		private static bool RetryBulkActionPredicate(BulkResponseItemBase bulkResponseItem, T d) => bulkResponseItem.Status == 429;

		private static void DroppedDocumentCallbackDefault(BulkResponseItemBase bulkResponseItem, T d) { }
	}
}

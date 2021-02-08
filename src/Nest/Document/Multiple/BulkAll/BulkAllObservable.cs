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
		private readonly Action<IBulkResponseItem, T> _droppedDocumentCallBack;
		private readonly int _maxDegreeOfParallelism;
		private readonly IBulkAllRequest<T> _partitionedBulkRequest;
		private readonly Func<IBulkResponseItem, T, bool> _retryPredicate;
		private readonly Action<IBulkResponse> _bulkResponseCallback;
		private Action _incrementFailed = () => { };
		private Action _incrementRetries = () => { };

		public BulkAllObservable(
			IElasticClient client,
			IBulkAllRequest<T> partitionedBulkRequest,
			CancellationToken cancellationToken = default(CancellationToken)
		)
		{
			_client = client;
			_partitionedBulkRequest = partitionedBulkRequest;
			_backOffRetries = _partitionedBulkRequest.BackOffRetries.GetValueOrDefault(CoordinatedRequestDefaults.BulkAllBackOffRetriesDefault);
			_backOffTime = _partitionedBulkRequest?.BackOffTime?.ToTimeSpan() ?? CoordinatedRequestDefaults.BulkAllBackOffTimeDefault;
			_bulkSize = _partitionedBulkRequest.Size ?? CoordinatedRequestDefaults.BulkAllSizeDefault;
			_retryPredicate = _partitionedBulkRequest.RetryDocumentPredicate ?? RetryBulkActionPredicate;
			_droppedDocumentCallBack = _partitionedBulkRequest.DroppedDocumentCallback ?? DroppedDocumentCallbackDefault;
			_bulkResponseCallback = _partitionedBulkRequest.BulkResponseCallback;

			_maxDegreeOfParallelism =
				_partitionedBulkRequest.MaxDegreeOfParallelism ?? CoordinatedRequestDefaults.BulkAllMaxDegreeOfParallelismDefault;
			_compositeCancelTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
			_compositeCancelToken = _compositeCancelTokenSource.Token;
		}

		public bool IsDisposed { get; private set; }

		public void Dispose()
		{
			IsDisposed = true;
			_compositeCancelTokenSource?.Cancel();
			_compositeCancelTokenSource?.Dispose();
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
			var documents = _partitionedBulkRequest.Documents;
			var partitioned = new PartitionHelper<T>(documents, _bulkSize);
#pragma warning disable 4014
			partitioned.ForEachAsync(
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
			if (!_partitionedBulkRequest.RefreshOnCompleted) return;

			var indices = _partitionedBulkRequest.RefreshIndices ?? _partitionedBulkRequest.Index;
			if (indices == null) return;

			var refresh = _client.Refresh(indices, r => r.RequestConfiguration(rc =>
			{
				switch (_partitionedBulkRequest)
				{
					case IHelperCallable helperCallable when helperCallable.ParentMetaData is object:
						rc.RequestMetaData(helperCallable.ParentMetaData);
						break;
					default:
						rc.RequestMetaData(RequestMetaDataFactory.BulkHelperRequestMetaData());
						break;
				}

				return rc;
			}));

			if (!refresh.IsValid) throw Throw($"Refreshing after all documents have indexed failed", refresh.ApiCall);
		}

		private async Task<IBulkAllResponse> BulkAsync(IList<T> buffer, long page, int backOffRetries)
		{
			_compositeCancelToken.ThrowIfCancellationRequested();

			var request = _partitionedBulkRequest;
			var response = await _client.BulkAsync(s =>
				{
					s.Timeout(request.Timeout);
					s.Index(request.Index).Type(request.Type);
					if (request.BufferToBulk != null) request.BufferToBulk(s, buffer);
					else s.IndexMany(buffer);
					if (!string.IsNullOrEmpty(request.Pipeline)) s.Pipeline(request.Pipeline);
#pragma warning disable 618
					if (request.Refresh.HasValue) s.Refresh(request.Refresh.Value);
#pragma warning restore 618
					if (request.Routing != null) s.Routing(request.Routing);
					if (request.WaitForActiveShards.HasValue) s.WaitForActiveShards(request.WaitForActiveShards.ToString());

					switch (_partitionedBulkRequest)
					{
						case IHelperCallable helperCallable when helperCallable.ParentMetaData is object:
							s.RequestConfiguration(rc => rc.RequestMetaData(helperCallable.ParentMetaData));
							break;
						default:
							s.RequestConfiguration(rc => rc.RequestMetaData(RequestMetaDataFactory.BulkHelperRequestMetaData()));
							break;
					}

					return s;
				}, _compositeCancelToken)
				.ConfigureAwait(false);

			_compositeCancelToken.ThrowIfCancellationRequested();

			_bulkResponseCallback?.Invoke(response);

			if (!response.ApiCall.Success)
				return await HandleBulkRequest(buffer, page, backOffRetries, response);

			var successfulDocuments = new List<Tuple<IBulkResponseItem, T>>();
			var retryableDocuments = new List<T>();
			var droppedDocuments = new List<Tuple<IBulkResponseItem, T>>();

			foreach (var documentWithResponse in response.Items.Zip(buffer, Tuple.Create))
			{
				if (documentWithResponse.Item1.IsValid)
					successfulDocuments.Add(documentWithResponse);
				else
				{
					if (_retryPredicate(documentWithResponse.Item1, documentWithResponse.Item2))
						retryableDocuments.Add(documentWithResponse.Item2);
					else
						droppedDocuments.Add(documentWithResponse);
				}
			}

			HandleDroppedDocuments(droppedDocuments, response);

			if (retryableDocuments.Count > 0 && backOffRetries < _backOffRetries)
				return await RetryDocuments(page, ++backOffRetries, retryableDocuments).ConfigureAwait(false);

			if (retryableDocuments.Count > 0)
				throw ThrowOnBadBulk(response, $"Bulk indexing failed and after retrying {backOffRetries} times");

			request.BackPressure?.Release();

			return new BulkAllResponse { Retries = backOffRetries, Page = page, Items = response.Items };
		}

		private void HandleDroppedDocuments(List<Tuple<IBulkResponseItem, T>> droppedDocuments, IBulkResponse response)
		{
			if (droppedDocuments.Count <= 0) return;

			foreach (var dropped in droppedDocuments) _droppedDocumentCallBack(dropped.Item1, dropped.Item2);
			if (!_partitionedBulkRequest.ContinueAfterDroppedDocuments)
				throw ThrowOnBadBulk(response, $"{nameof(BulkAll)} halted after receiving failures that can not be retried from _bulk");
		}

		private async Task<IBulkAllResponse> HandleBulkRequest(IList<T> buffer, long page, int backOffRetries, IBulkResponse response)
		{
			var clientException = response.ApiCall.OriginalException as ElasticsearchClientException;
			var failureReason = clientException?.FailureReason; 
			var reason = failureReason?.GetStringValue() ?? nameof(PipelineFailure.BadRequest);
			switch (failureReason)
			{
				case PipelineFailure.MaxRetriesReached:
					//TODO move this to its own PipelineFailure classification in 7.0
					if (response.ApiCall.AuditTrail.Last().Event == AuditEvent.FailedOverAllNodes)
						throw ThrowOnBadBulk(response, $"{nameof(BulkAll)} halted after attempted bulk failed over all the active nodes");

					ThrowOnExhaustedRetries();
					return await RetryDocuments(page, ++backOffRetries, buffer).ConfigureAwait(false);
				case PipelineFailure.CouldNotStartSniffOnStartup:
				case PipelineFailure.BadAuthentication:
				case PipelineFailure.NoNodesAttempted:
				case PipelineFailure.SniffFailure:
				case PipelineFailure.Unexpected:
					throw ThrowOnBadBulk(response, $"{nameof(BulkAll)} halted after {nameof(PipelineFailure)}.{reason} from _bulk");
				default:
					ThrowOnExhaustedRetries();
					return await RetryDocuments(page, ++backOffRetries, buffer).ConfigureAwait(false);
			}

			void ThrowOnExhaustedRetries()
			{
				if (_partitionedBulkRequest.ContinueAfterDroppedDocuments || backOffRetries < _backOffRetries) return;

				throw ThrowOnBadBulk(response,
					$"{nameof(BulkAll)} halted after {nameof(PipelineFailure)}.{reason} from _bulk and exhausting retries ({backOffRetries})"
				);
			}
		}

		private async Task<IBulkAllResponse> RetryDocuments(long page, int backOffRetries, IList<T> retryDocuments)
		{
			_incrementRetries();
			await Task.Delay(_backOffTime, _compositeCancelToken).ConfigureAwait(false);
			return await BulkAsync(retryDocuments, page, backOffRetries).ConfigureAwait(false);
		}

		private Exception ThrowOnBadBulk(IElasticsearchResponse response, string message)
		{
			_incrementFailed();
			_partitionedBulkRequest.BackPressure?.Release();
			return Throw(message, response.ApiCall);
		}

		private static ElasticsearchClientException Throw(string message, IApiCallDetails details) =>
			new ElasticsearchClientException(PipelineFailure.BadResponse, message, details);


		private static bool RetryBulkActionPredicate(IBulkResponseItem bulkResponseItem, T d) => bulkResponseItem.Status == 429;

		private static void DroppedDocumentCallbackDefault(IBulkResponseItem bulkResponseItem, T d) { }
	}
}

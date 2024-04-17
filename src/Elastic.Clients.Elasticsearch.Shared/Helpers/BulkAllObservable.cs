// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

#if !ELASTICSEARCH_SERVERLESS

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;
using Elastic.Transport.Diagnostics.Auditing;
using Elastic.Transport.Extensions;
using Elastic.Transport.Products.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Bulk;
using Elastic.Clients.Elasticsearch.Requests;

namespace Elastic.Clients.Elasticsearch;

public sealed class BulkAllObservable<T> : IDisposable, IObservable<BulkAllResponse>
{
	private bool _disposedValue;

	private readonly int _backOffRetries;
	private readonly TimeSpan _backOffTime;
	private readonly int _bulkSize;
	private readonly ElasticsearchClient _client;

	private readonly CancellationToken _compositeCancelToken;
	private readonly CancellationTokenSource _compositeCancelTokenSource;
	private readonly Action<ResponseItem, T> _droppedDocumentCallBack;
	private readonly int _maxDegreeOfParallelism;
	private readonly IBulkAllRequest<T> _partitionedBulkRequest;
	private readonly Func<ResponseItem, T, bool> _retryPredicate;

	private readonly Action _incrementFailed = () => { };
	private readonly Action _incrementRetries = () => { };

	private readonly Action<BulkResponse> _bulkResponseCallback;

	public BulkAllObservable(ElasticsearchClient client, IBulkAllRequest<T> partitionedBulkRequest, CancellationToken cancellationToken = default)
	{
		_client = client;
		_partitionedBulkRequest = partitionedBulkRequest;
		_backOffRetries = _partitionedBulkRequest.BackOffRetries.GetValueOrDefault(CoordinatedRequestDefaults.BulkAllBackOffRetriesDefault);
		_backOffTime = _partitionedBulkRequest?.BackOffTime?.ToTimeSpan() ?? CoordinatedRequestDefaults.BulkAllBackOffTimeDefault;
		_bulkSize = _partitionedBulkRequest.Size ?? CoordinatedRequestDefaults.BulkAllSizeDefault;
		_retryPredicate = _partitionedBulkRequest.RetryDocumentPredicate ?? RetryBulkActionPredicate;
		_droppedDocumentCallBack = _partitionedBulkRequest.DroppedDocumentCallback ?? DroppedDocumentCallbackDefault;
		_bulkResponseCallback = _partitionedBulkRequest.BulkResponseCallback;
		_maxDegreeOfParallelism = _partitionedBulkRequest.MaxDegreeOfParallelism ?? CoordinatedRequestDefaults.BulkAllMaxDegreeOfParallelismDefault;
		_compositeCancelTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
		_compositeCancelToken = _compositeCancelTokenSource.Token;
	}

	private void BulkAll(IObserver<BulkAllResponse> observer)
	{
		var documents = _partitionedBulkRequest.Documents;
		var partitioned = new PartitionHelper<T>(documents, _bulkSize);
#pragma warning disable 4014
#pragma warning disable VSTHRD110 // Observe result of async calls
		partitioned.ForEachAsync(
#pragma warning restore 4014
				(buffer, page) => BulkAsync(buffer, page, 0),
			(buffer, response) => observer.OnNext(response),
			ex => OnCompleted(ex, observer),
			_maxDegreeOfParallelism
		);
#pragma warning restore VSTHRD110 // Observe result of async calls
	}

	private void OnCompleted(Exception exception, IObserver<BulkAllResponse> observer)
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
		if (!_partitionedBulkRequest.RefreshOnCompleted)
			return;

		var indices = _partitionedBulkRequest.RefreshIndices ?? _partitionedBulkRequest.Index;
		if (indices == null)
			return;

		var rc = _partitionedBulkRequest switch
		{
			IHelperCallable helperCallable when helperCallable.ParentMetaData is not null => helperCallable.ParentMetaData,
			_ => RequestMetaDataFactory.BulkHelperRequestMetaData(),
		};

		var request = new IndexManagement.RefreshRequest(indices);

		if (rc is not null)
			request.RequestConfiguration = new RequestConfiguration { RequestMetaData = rc };

#pragma warning disable VSTHRD002 // Avoid problematic synchronous waits
		var refresh = _client.Indices.RefreshAsync(request).Result;
#pragma warning restore VSTHRD002 // Avoid problematic synchronous waits

		if (!refresh.IsValidResponse)
			throw Throw($"Refreshing after all documents have indexed failed", refresh);
	}

	private async Task<BulkAllResponse> BulkAsync(IList<T> buffer, long page, int backOffRetries)
	{
		_compositeCancelToken.ThrowIfCancellationRequested();

		var request = _partitionedBulkRequest;

		var response = await _client.BulkAsync(s =>
		{
			s.RequestParameters.RequestConfiguration = new RequestConfiguration { DisableAuditTrail = false };
			s.Index(request.Index);
			s.Timeout(request.Timeout);

			if (request.BufferToBulk is not null)
			{
				request.BufferToBulk(s, buffer);
			}
			else
			{
				s.IndexMany(buffer);
			}

			if (!string.IsNullOrEmpty(request.Pipeline))
				s.Pipeline(request.Pipeline);
			if (request.Routing != null)
				s.Routing(request.Routing);
			if (request.WaitForActiveShards.HasValue)
				s.WaitForActiveShards(request.WaitForActiveShards.ToString());

			switch (_partitionedBulkRequest)
			{
				case IHelperCallable helperCallable when helperCallable.ParentMetaData is not null:
					s.RequestConfiguration(rc => rc.RequestMetaData(helperCallable.ParentMetaData));
					break;
				default:
					s.RequestConfiguration(rc => rc.RequestMetaData(RequestMetaDataFactory.BulkHelperRequestMetaData()));
					break;
			}

		}, _compositeCancelToken).ConfigureAwait(false);

		_compositeCancelToken.ThrowIfCancellationRequested();
		_bulkResponseCallback?.Invoke(response);

		if (!response.ApiCallDetails.HasSuccessfulStatusCode || !response.ApiCallDetails.HasExpectedContentType)
			return await HandleBulkRequestAsync(buffer, page, backOffRetries, response).ConfigureAwait(false);

		var retryableDocuments = new List<T>();
		var droppedDocuments = new List<Tuple<ResponseItem, T>>();

		var retryableDocsRemainingAfterRetriesExceeded = false;

		foreach (var documentWithResponse in response.Items.Zip(buffer, Tuple.Create))
		{
			if (documentWithResponse.Item1.IsValid)
				continue;

			if (_retryPredicate(documentWithResponse.Item1, documentWithResponse.Item2))
			{
				if (backOffRetries < _backOffRetries)
				{
					retryableDocuments.Add(documentWithResponse.Item2);
				}
				else
				{
					// We still have retriable documents but have exceeded all retries, so we mark these as
					// dropped so they get handled correctly.
					retryableDocsRemainingAfterRetriesExceeded = true;
					droppedDocuments.Add(documentWithResponse);
				}
			}
			else
			{
				droppedDocuments.Add(documentWithResponse);
			}
		}

		HandleDroppedDocuments(droppedDocuments, response);

		if (retryableDocsRemainingAfterRetriesExceeded)
		{
			throw ThrowOnBadBulk(response, $"Bulk indexing failed and after retrying {backOffRetries} times.");
		}
		else if (retryableDocuments.Count > 0)
		{
			return await RetryDocumentsAsync(page, ++backOffRetries, retryableDocuments).ConfigureAwait(false);
		}

		request.BackPressure?.Release();

		return new BulkAllResponse { Retries = backOffRetries, Page = page, Items = response.Items };
	}

	private void HandleDroppedDocuments(List<Tuple<ResponseItem, T>> droppedDocuments, BulkResponse response)
	{
		if (droppedDocuments.Count <= 0)
			return;

		foreach (var dropped in droppedDocuments)
			_droppedDocumentCallBack(dropped.Item1, dropped.Item2);

		if (!_partitionedBulkRequest.ContinueAfterDroppedDocuments)
			throw ThrowOnBadBulk(response, $"{nameof(BulkAll)} halted after receiving failures that can not be retried from _bulk");
	}

	private async Task<BulkAllResponse> HandleBulkRequestAsync(IList<T> buffer, long page, int backOffRetries, BulkResponse response)
	{
		var clientException = response.ApiCallDetails.OriginalException as TransportException;
		var failureReason = clientException?.FailureReason;
		var reason = failureReason?.GetStringValue() ?? nameof(PipelineFailure.BadRequest);
		switch (failureReason)
		{
			case PipelineFailure.MaxRetriesReached:
				if (response.ApiCallDetails.AuditTrail.Last().Event == AuditEvent.FailedOverAllNodes)
					throw ThrowOnBadBulk(response, $"{nameof(BulkAll)} halted after attempted bulk failed over all the active nodes");

				ThrowOnExhaustedRetries();
				return await RetryDocumentsAsync(page, ++backOffRetries, buffer).ConfigureAwait(false);
			case PipelineFailure.CouldNotStartSniffOnStartup:
			case PipelineFailure.BadAuthentication:
			case PipelineFailure.NoNodesAttempted:
			case PipelineFailure.SniffFailure:
			case PipelineFailure.Unexpected:
				throw ThrowOnBadBulk(response, $"{nameof(BulkAll)} halted after {nameof(PipelineFailure)}.{reason} from _bulk");
			case PipelineFailure.BadResponse:
			case PipelineFailure.PingFailure:
			case PipelineFailure.MaxTimeoutReached:
			case PipelineFailure.BadRequest:
			default:
				ThrowOnExhaustedRetries();
				return await RetryDocumentsAsync(page, ++backOffRetries, buffer).ConfigureAwait(false);
		}

		void ThrowOnExhaustedRetries()
		{
			if (backOffRetries < _backOffRetries)
				return;

			throw ThrowOnBadBulk(response,
				$"{nameof(BulkAll)} halted after {nameof(PipelineFailure)}.{reason} from _bulk and exhausting retries ({backOffRetries})");
		}
	}

	private async Task<BulkAllResponse> RetryDocumentsAsync(long page, int backOffRetries, IList<T> retryDocuments)
	{
		_incrementRetries();
		await Task.Delay(_backOffTime, _compositeCancelToken).ConfigureAwait(false);
		return await BulkAsync(retryDocuments, page, backOffRetries).ConfigureAwait(false);
	}

	private Exception ThrowOnBadBulk(ElasticsearchResponse response, string message)
	{
		_incrementFailed();
		_partitionedBulkRequest.BackPressure?.Release();
		return Throw(message, response);
	}

	private static bool RetryBulkActionPredicate(ResponseItem bulkResponseItem, T d) => bulkResponseItem.Status == 429;

	private static void DroppedDocumentCallbackDefault(ResponseItem bulkResponseItem, T d) { }

	public void Dispose()
	{
		if (!_disposedValue)
		{
			_compositeCancelTokenSource?.Cancel();
			_compositeCancelTokenSource?.Dispose();

			_disposedValue = true;
		}
	}

	public IDisposable Subscribe(IObserver<BulkAllResponse> observer)
	{
		observer.ThrowIfNull(nameof(observer));
		BulkAll(observer);
		return this;
	}

	private static TransportException Throw(string message, ElasticsearchResponse details) =>
		new(PipelineFailure.BadResponse, message, details);
}

#endif

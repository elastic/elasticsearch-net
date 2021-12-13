// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Elastic.Clients.Elasticsearch.Helpers;

public class BulkAllObservable<T> : IDisposable, IObservable<BulkAllResponse>
{
	private bool _disposedValue;

	private readonly int _backOffRetries;
	private readonly TimeSpan _backOffTime;
	private readonly int _bulkSize;
	private readonly IElasticClient _client;

	private readonly CancellationToken _compositeCancelToken;
	private readonly CancellationTokenSource _compositeCancelTokenSource;
	//private readonly Action<BulkResponseItemBase, T> _droppedDocumentCallBack;
	private readonly int _maxDegreeOfParallelism;
	private readonly BulkAllRequest<T> _partitionedBulkRequest;
	//private readonly Func<BulkResponseItemBase, T, bool> _retryPredicate;
	//private Action _incrementFailed = () => { };
	//private Action _incrementRetries = () => { };
	private readonly Action<BulkResponse> _bulkResponseCallback;

	public BulkAllObservable(IElasticClient client, BulkAllRequest<T> partitionedBulkRequest, CancellationToken cancellationToken = default)
	{
		_client = client;
		_partitionedBulkRequest = partitionedBulkRequest;
		_backOffRetries = _partitionedBulkRequest.BackOffRetries.GetValueOrDefault(CoordinatedRequestDefaults.BulkAllBackOffRetriesDefault);
		_backOffTime = _partitionedBulkRequest?.BackOffTime?.ToTimeSpan() ?? CoordinatedRequestDefaults.BulkAllBackOffTimeDefault;
		_bulkSize = _partitionedBulkRequest.Size ?? CoordinatedRequestDefaults.BulkAllSizeDefault;
		//_retryPredicate = _partitionedBulkRequest.RetryDocumentPredicate ?? RetryBulkActionPredicate;
		//_droppedDocumentCallBack = _partitionedBulkRequest.DroppedDocumentCallback ?? DroppedDocumentCallbackDefault;
		_bulkResponseCallback = _partitionedBulkRequest.BulkResponseCallback;

		_maxDegreeOfParallelism =
			_partitionedBulkRequest.MaxDegreeOfParallelism ?? CoordinatedRequestDefaults.BulkAllMaxDegreeOfParallelismDefault;
		_compositeCancelTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
		_compositeCancelToken = _compositeCancelTokenSource.Token;
	}

	private void BulkAll(IObserver<BulkAllResponse> observer)
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
		//if (!_partitionedBulkRequest.RefreshOnCompleted)
		//	return;

		//var indices = _partitionedBulkRequest.RefreshIndices ?? _partitionedBulkRequest.Index;
		//if (indices == null)
		//	return;

		//var refresh = _client.IndexManagement.Refresh(indices, r => r.RequestConfiguration(rc =>
		//{
		//	switch (_partitionedBulkRequest)
		//	{
		//		case IHelperCallable helperCallable when helperCallable.ParentMetaData is object:
		//			rc.RequestMetaData(helperCallable.ParentMetaData);
		//			break;
		//		default:
		//			rc.RequestMetaData(RequestMetaDataFactory.BulkHelperRequestMetaData());
		//			break;
		//	}

		//	return rc;
		//}));

		//if (!refresh.IsValid)
		//	throw Throw($"Refreshing after all documents have indexed failed", refresh.ApiCall);
	}

	private async Task<BulkAllResponse> BulkAsync(IList<T> buffer, long page, int backOffRetries)
	{
		_compositeCancelToken.ThrowIfCancellationRequested();

		var request = _partitionedBulkRequest;

		var ops = buffer.Select(o => new BulkIndexOperation<T>(o)).Cast<IBulkOperation>().ToList();

		var response = await _client.BulkAsync<T>(new BulkRequest<T>(request.Index) { Operations = ops }, _compositeCancelToken).ConfigureAwait(false);

		//var response = await _client.BulkAsync<T>(s =>
		//{
		//	s.Index(request.Index);
		//	//s.Timeout(request.Timeout);

		//	if (request.BufferToBulk is not null)
		//	{
		//		request.BufferToBulk(s, buffer);
		//	}
		//	else
		//	{
		//		s.IndexMany(buffer);
		//	}

		//	//if (!string.IsNullOrEmpty(request.Pipeline))
		//	//	s.Pipeline(request.Pipeline);
		//	//if (request.Routing != null)
		//	//	s.Routing(request.Routing);
		//	//if (request.WaitForActiveShards.HasValue)
		//	//	s.WaitForActiveShards(request.WaitForActiveShards.ToString());

		//	//switch (_partitionedBulkRequest)
		//	//{
		//	//	case IHelperCallable helperCallable when helperCallable.ParentMetaData is object:
		//	//		s.RequestConfiguration(rc => rc.RequestMetaData(helperCallable.ParentMetaData));
		//	//		break;
		//	//	default:
		//	//		s.RequestConfiguration(rc => rc.RequestMetaData(RequestMetaDataFactory.BulkHelperRequestMetaData()));
		//	//		break;
		//	//}

		//}, _compositeCancelToken).ConfigureAwait(false);

		_compositeCancelToken.ThrowIfCancellationRequested();
		_bulkResponseCallback?.Invoke(response);

		//if (!response.ApiCall.Success)
		//	return await HandleBulkRequest(buffer, page, backOffRetries, response).ConfigureAwait(false);

		var retryableDocuments = new List<T>();
		//var droppedDocuments = new List<Tuple<BulkResponseItemBase, T>>();

		foreach (var documentWithResponse in response.Items.Zip(buffer, Tuple.Create))
		{
			//if (documentWithResponse.Item1.IsValid)
			//	continue;

			//if (_retryPredicate(documentWithResponse.Item1, documentWithResponse.Item2))
			//	retryableDocuments.Add(documentWithResponse.Item2);
			//else
			//	droppedDocuments.Add(documentWithResponse);
		}

		//HandleDroppedDocuments(droppedDocuments, response);

		//if (retryableDocuments.Count > 0 && backOffRetries < _backOffRetries)
		//	return await RetryDocuments(page, ++backOffRetries, retryableDocuments).ConfigureAwait(false);

		//if (retryableDocuments.Count > 0)
		//	throw ThrowOnBadBulk(response, $"Bulk indexing failed and after retrying {backOffRetries} times");

		request.BackPressure?.Release();

		return new BulkAllResponse { Retries = backOffRetries, Page = page, /*Items = response.Items*/ };
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!_disposedValue)
		{
			if (disposing)
			{
				_compositeCancelTokenSource?.Cancel();
				_compositeCancelTokenSource?.Dispose();
			}

			_disposedValue = true;
		}
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	public IDisposable Subscribe(IObserver<BulkAllResponse> observer)
	{
		observer.ThrowIfNull(nameof(observer));
		BulkAll(observer);
		return this;
	}
}

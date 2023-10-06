// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using Elastic.Transport;
using Elastic.Clients.Elasticsearch.Core.Bulk;
using Elastic.Clients.Elasticsearch.Fluent;

namespace Elastic.Clients.Elasticsearch;

public sealed class BulkAllRequest<T> : IBulkAllRequest<T>, IHelperCallable
{
	public BulkAllRequest(IEnumerable<T> documents)
	{
		Documents = documents;
		Index = typeof(T);
	}

	public IEnumerable<T> Documents { get; }

	public IndexName Index { get; set; }

	public int? BackOffRetries { get; set; }

	public Duration? BackOffTime { get; set; }

	public ProducerConsumerBackPressure? BackPressure { get; set; }

	public Action<BulkRequestDescriptor, IList<T>>? BufferToBulk { get; set; }

	public bool ContinueAfterDroppedDocuments { get; set; }

	public int? Size { get; set; }

	public Action<ResponseItem, T>? DroppedDocumentCallback { get; set; }

	public Func<ResponseItem, T, bool>? RetryDocumentPredicate { get; set; }

	public Action<BulkResponse>? BulkResponseCallback { get; set; }

	public int? MaxDegreeOfParallelism { get; set; }

	public Duration? Timeout { get; set; }

	public string? Pipeline { get; set; }

	public WaitForActiveShards? WaitForActiveShards { get; set; }

	public Routing? Routing { get; set; }

	public bool RefreshOnCompleted { get; set; }

	public Indices? RefreshIndices { get; set; }
	internal RequestMetaData ParentMetaData { get; set; }

	RequestMetaData IHelperCallable.ParentMetaData { get => ParentMetaData; set => ParentMetaData = value; }
}

public sealed class BulkAllRequestDescriptor<T> : SerializableDescriptor<BulkAllRequestDescriptor<T>>, IBulkAllRequest<T>, IHelperCallable
{
	private readonly IEnumerable<T> _documents;

	private int? _backOffRetries;
	private Duration _backOffTime;
	private ProducerConsumerBackPressure _backPressure;
	private Action<BulkResponse> _bulkResponseCallback;
	private IndexName _index;
	private int? _maxDegreeOfParallism;
	private int? _size;
	private bool _refreshOnCompleted;
	private Action<BulkRequestDescriptor, IList<T>> _bufferToBulk;
	private Func<ResponseItem, T, bool> _retryDocumentPredicate;
	private Action<ResponseItem, T> _droppedDocumentCallback;
	private Routing _routing;
	private bool _continueAfterDroppedDocuments;
	private string _pipeline;
	private Indices _refreshIndices;
	private Duration _timeout;
	private WaitForActiveShards? _waitForActiveShards;
	private RequestMetaData _requestMetaData;

	public BulkAllRequestDescriptor(IEnumerable<T> documents)
	{
		_documents = documents;
		_index = typeof(T);
	}

	int? IBulkAllRequest<T>.BackOffRetries => _backOffRetries;
	Duration? IBulkAllRequest<T>.BackOffTime => _backOffTime;
	ProducerConsumerBackPressure? IBulkAllRequest<T>.BackPressure => _backPressure;
	Action<BulkRequestDescriptor, IList<T>>? IBulkAllRequest<T>.BufferToBulk => _bufferToBulk;
	Action<BulkResponse>? IBulkAllRequest<T>.BulkResponseCallback => _bulkResponseCallback;
	bool IBulkAllRequest<T>.ContinueAfterDroppedDocuments => _continueAfterDroppedDocuments;
	IEnumerable<T> IBulkAllRequest<T>.Documents => _documents;
	Action<ResponseItem, T>? IBulkAllRequest<T>.DroppedDocumentCallback => _droppedDocumentCallback;
	IndexName IBulkAllRequest<T>.Index => _index;
	int? IBulkAllRequest<T>.MaxDegreeOfParallelism => _maxDegreeOfParallism;
	string? IBulkAllRequest<T>.Pipeline => _pipeline;
	Indices? IBulkAllRequest<T>.RefreshIndices => _refreshIndices;
	bool IBulkAllRequest<T>.RefreshOnCompleted => _refreshOnCompleted;
	Func<ResponseItem, T, bool>? IBulkAllRequest<T>.RetryDocumentPredicate => _retryDocumentPredicate;
	Routing? IBulkAllRequest<T>.Routing => _routing;
	int? IBulkAllRequest<T>.Size => _size;
	Duration? IBulkAllRequest<T>.Timeout => _timeout;
	WaitForActiveShards? IBulkAllRequest<T>.WaitForActiveShards => _waitForActiveShards;
	RequestMetaData IHelperCallable.ParentMetaData { get => _requestMetaData; set => _requestMetaData = value; }

	public BulkAllRequestDescriptor<T> BackOffRetries(int? backOffRetries) => Assign(backOffRetries, (a, v) => a._backOffRetries = v);

	public BulkAllRequestDescriptor<T> BackOffTime(Duration? backOffTime) => Assign(backOffTime, (a, v) => a._backOffTime = v);

	public BulkAllRequestDescriptor<T> BackPressure(int maxConcurrency, int? backPressureFactor = null) =>
			Assign(new ProducerConsumerBackPressure(backPressureFactor, maxConcurrency), (a, v) => a._backPressure = v);

	public BulkAllRequestDescriptor<T> BufferToBulk(Action<BulkRequestDescriptor, IList<T>> modifier) => Assign(modifier, (a, v) => a._bufferToBulk = v);

	public BulkAllRequestDescriptor<T> BulkResponseCallback(Action<BulkResponse> callback) =>
			Assign(callback, (a, v) => a._bulkResponseCallback = v);

	public BulkAllRequestDescriptor<T> ContinueAfterDroppedDocuments(bool proceed = true) => Assign(proceed, (a, v) => a._continueAfterDroppedDocuments = v);

	public BulkAllRequestDescriptor<T> DroppedDocumentCallback(Action<ResponseItem, T> callback) =>
			Assign(callback, (a, v) => a._droppedDocumentCallback = v);

	public BulkAllRequestDescriptor<T> Index(IndexName index) => Assign(index, (a, v) => a._index = v);

	public BulkAllRequestDescriptor<T> Index<TOther>() where TOther : class => Assign(typeof(TOther), (a, v) => a._index = v);

	public BulkAllRequestDescriptor<T> MaxDegreeOfParallelism(int? parallelism) => Assign(parallelism, (a, v) => a._maxDegreeOfParallism = v);

	public BulkAllRequestDescriptor<T> Pipeline(string pipeline) => Assign(pipeline, (a, v) => a._pipeline = v);

	public BulkAllRequestDescriptor<T> RefreshIndices(Indices indicesToRefresh) => Assign(indicesToRefresh, (a, v) => a._refreshIndices = v);

	public BulkAllRequestDescriptor<T> RefreshOnCompleted(bool refreshOnCompleted = true) => Assign(refreshOnCompleted, (a, v) => a._refreshOnCompleted = v);

	public BulkAllRequestDescriptor<T> RetryDocumentPredicate(Func<ResponseItem, T, bool> predicate) =>
			Assign(predicate, (a, v) => a._retryDocumentPredicate = v);

	public BulkAllRequestDescriptor<T> Routing(Routing routing) => Assign(routing, (a, v) => a._routing = v);

	public BulkAllRequestDescriptor<T> Size(int? size) => Assign(size, (a, v) => a._size = v);

	public BulkAllRequestDescriptor<T> Timeout(Duration timeout) => Assign(timeout, (a, v) => a._timeout = v);

	public BulkAllRequestDescriptor<T> WaitForActiveShards(WaitForActiveShards? shards) => Assign(shards, (a, v) => a._waitForActiveShards = v);

	// This descriptor is not serializable and gets converted to a BullAllObservable
	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings) => throw new NotImplementedException();
}

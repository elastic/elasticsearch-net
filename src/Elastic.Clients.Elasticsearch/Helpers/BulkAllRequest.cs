// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Elastic.Clients.Elasticsearch.Helpers;

public sealed class BulkAllRequest<T> : IBulkAllRequest<T>
{
	public BulkAllRequest(IEnumerable<T> documents)
	{
		Documents = documents;
		Index = typeof(T);
	}

	public IEnumerable<T> Documents { get; }

	public IndexName Index { get; set; }

	public int? BackOffRetries { get; set; }

	public Time? BackOffTime { get; set; }

	public ProducerConsumerBackPressure? BackPressure { get; set; }

	public Action<BulkRequestDescriptor<T>, IList<T>>? BufferToBulk { get; set; }

	public bool ContinueAfterDroppedDocuments { get; set; }

	public int? Size { get; set; }

	public Action<BulkResponseItemBase, T>? DroppedDocumentCallback { get; set; }

	public Func<BulkResponseItemBase, T, bool>? RetryDocumentPredicate { get; set; }

	public Action<BulkResponse>? BulkResponseCallback { get; set; }

	public int? MaxDegreeOfParallelism { get; set; }

	public Time? Timeout { get; set; }

	public string? Pipeline { get; set; }

	public WaitForActiveShards? WaitForActiveShards { get; set; }

	public Routing? Routing { get; set; }

	public bool RefreshOnCompleted { get; set; }

	public Indices? RefreshIndices { get; set; }
}


public sealed class BulkAllRequestDescriptor<T> : DescriptorBase<BulkAllRequestDescriptor<T>>, IBulkAllRequest<T>
{
	private readonly IEnumerable<T> _documents;

	private int? _backOffRetries;
	private Time _backOffTime;
	private ProducerConsumerBackPressure _backPressure;
	private Action<BulkResponse> _bulkResponseCallback;
	private IndexName _index;
	private int? _maxDegreeOfParallism;
	private int? _size;
	private bool _refreshOnCompleted;
	private Action<BulkRequestDescriptor<T>, IList<T>> _bufferToBulk;
	private Func<BulkResponseItemBase, T, bool> _retryDocumentPredicate;
	private Action<BulkResponseItemBase, T> _droppedDocumentCallback;
	private Routing _routing;
	private bool _continueAfterDroppedDocuments;
	private string _pipeline;
	private Indices _refreshIndices;
	private Time _timeout;
	private WaitForActiveShards? _waitForActiveShards;

	public BulkAllRequestDescriptor(IEnumerable<T> documents)
	{
		_documents = documents;
		_index = typeof(T);
	}

	int? IBulkAllRequest<T>.BackOffRetries => _backOffRetries;
	Time? IBulkAllRequest<T>.BackOffTime => _backOffTime;
	ProducerConsumerBackPressure? IBulkAllRequest<T>.BackPressure => _backPressure;
	Action<BulkRequestDescriptor<T>, IList<T>>? IBulkAllRequest<T>.BufferToBulk => _bufferToBulk;
	Action<BulkResponse>? IBulkAllRequest<T>.BulkResponseCallback => _bulkResponseCallback;
	bool IBulkAllRequest<T>.ContinueAfterDroppedDocuments => _continueAfterDroppedDocuments;
	IEnumerable<T> IBulkAllRequest<T>.Documents => _documents;
	Action<BulkResponseItemBase, T>? IBulkAllRequest<T>.DroppedDocumentCallback => _droppedDocumentCallback;
	IndexName IBulkAllRequest<T>.Index => _index;
	int? IBulkAllRequest<T>.MaxDegreeOfParallelism => _maxDegreeOfParallism;
	string? IBulkAllRequest<T>.Pipeline => _pipeline;
	Indices? IBulkAllRequest<T>.RefreshIndices => _refreshIndices;
	bool IBulkAllRequest<T>.RefreshOnCompleted => _refreshOnCompleted;
	Func<BulkResponseItemBase, T, bool>? IBulkAllRequest<T>.RetryDocumentPredicate => _retryDocumentPredicate;
	Routing? IBulkAllRequest<T>.Routing => _routing;
	int? IBulkAllRequest<T>.Size => _size;
	Time? IBulkAllRequest<T>.Timeout => _timeout;
	WaitForActiveShards? IBulkAllRequest<T>.WaitForActiveShards => _waitForActiveShards;

	public BulkAllRequestDescriptor<T> BackOffRetries(int? backOffRetries) => Assign(backOffRetries, (a, v) => a._backOffRetries = v);

	public BulkAllRequestDescriptor<T> BackOffTime(Time? backOffTime) => Assign(backOffTime, (a, v) => a._backOffTime = v);

	public BulkAllRequestDescriptor<T> BackPressure(int maxConcurrency, int? backPressureFactor = null) =>
			Assign(new ProducerConsumerBackPressure(backPressureFactor, maxConcurrency), (a, v) => a._backPressure = v);

	public BulkAllRequestDescriptor<T> BufferToBulk(Action<BulkRequestDescriptor<T>, IList<T>> modifier) => Assign(modifier, (a, v) => a._bufferToBulk = v);

	public BulkAllRequestDescriptor<T> BulkResponseCallback(Action<BulkResponse> callback) =>
			Assign(callback, (a, v) => a._bulkResponseCallback = v);

	public BulkAllRequestDescriptor<T> ContinueAfterDroppedDocuments(bool proceed = true) => Assign(proceed, (a, v) => a._continueAfterDroppedDocuments = v);

	public BulkAllRequestDescriptor<T> DroppedDocumentCallback(Action<BulkResponseItemBase, T> callback) =>
			Assign(callback, (a, v) => a._droppedDocumentCallback = v);

	public BulkAllRequestDescriptor<T> Index(IndexName index) => Assign(index, (a, v) => a._index = v);

	public BulkAllRequestDescriptor<T> Index<TOther>() where TOther : class => Assign(typeof(TOther), (a, v) => a._index = v);

	public BulkAllRequestDescriptor<T> MaxDegreeOfParallelism(int? parallelism) => Assign(parallelism, (a, v) => a._maxDegreeOfParallism = v);

	public BulkAllRequestDescriptor<T> Pipeline(string pipeline) => Assign(pipeline, (a, v) => a._pipeline = v);

	public BulkAllRequestDescriptor<T> RefreshIndices(Indices indicesToRefresh) => Assign(indicesToRefresh, (a, v) => a._refreshIndices = v);

	public BulkAllRequestDescriptor<T> RefreshOnCompleted(bool refreshOnCompleted = true) => Assign(refreshOnCompleted, (a, v) => a._refreshOnCompleted = v);

	public BulkAllRequestDescriptor<T> RetryDocumentPredicate(Func<BulkResponseItemBase, T, bool> predicate) =>
			Assign(predicate, (a, v) => a._retryDocumentPredicate = v);

	public BulkAllRequestDescriptor<T> Routing(Routing routing) => Assign(routing, (a, v) => a._routing = v);

	public BulkAllRequestDescriptor<T> Size(int? size) => Assign(size, (a, v) => a._size = v);

	public BulkAllRequestDescriptor<T> Timeout(Time timeout) => Assign(timeout, (a, v) => a._timeout = v);

	public BulkAllRequestDescriptor<T> WaitForActiveShards(WaitForActiveShards? shards) => Assign(shards, (a, v) => a._waitForActiveShards = v);

	// This descriptor is not serializable and gets converted to a BullAllObservable
	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings) => throw new NotImplementedException();
}



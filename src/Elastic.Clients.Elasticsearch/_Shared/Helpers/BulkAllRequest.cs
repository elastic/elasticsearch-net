// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

using Elastic.Transport;
using Elastic.Clients.Elasticsearch.Core.Bulk;

namespace Elastic.Clients.Elasticsearch;

public sealed class BulkAllRequest<T> :
	IBulkAllRequest<T>,
	IHelperCallable
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


public sealed class BulkAllRequestDescriptor<T>
{
	internal BulkAllRequest<T> Instance { get; }

	public static implicit operator BulkAllRequest<T>(BulkAllRequestDescriptor<T> descriptor) => descriptor.Instance;

	public BulkAllRequestDescriptor(IEnumerable<T> documents)
	{
		Instance = new(documents);
	}

	public BulkAllRequestDescriptor<T> BackOffRetries(int? backOffRetries)
	{
		Instance.BackOffRetries = backOffRetries;
		return this;
	}

	public BulkAllRequestDescriptor<T> BackOffTime(Duration? backOffTime)
	{
		Instance.BackOffTime = backOffTime;
		return this;
	}

	public BulkAllRequestDescriptor<T> BackPressure(int maxConcurrency, int? backPressureFactor = null)
	{
		Instance.BackPressure = new ProducerConsumerBackPressure(backPressureFactor, maxConcurrency);
		return this;
	}

	public BulkAllRequestDescriptor<T> BufferToBulk(Action<BulkRequestDescriptor, IList<T>> modifier)
	{
		Instance.BufferToBulk = modifier;
		return this;
	}

	public BulkAllRequestDescriptor<T> BulkResponseCallback(Action<BulkResponse> callback)
	{
		Instance.BulkResponseCallback = callback;
		return this;
	}

	public BulkAllRequestDescriptor<T> ContinueAfterDroppedDocuments(bool proceed = true)
	{
		Instance.ContinueAfterDroppedDocuments = proceed;
		return this;
	}

	public BulkAllRequestDescriptor<T> DroppedDocumentCallback(Action<ResponseItem, T> callback)
	{
		Instance.DroppedDocumentCallback = callback;
		return this;
	}

	public BulkAllRequestDescriptor<T> Index(IndexName index)
	{
		Instance.Index = index;
		return this;
	}

	public BulkAllRequestDescriptor<T> Index<TOther>() where TOther : class
	{
		Instance.Index = typeof(TOther);
		return this;
	}

	public BulkAllRequestDescriptor<T> MaxDegreeOfParallelism(int? parallelism)
	{
		Instance.MaxDegreeOfParallelism = parallelism;
		return this;
	}

	public BulkAllRequestDescriptor<T> Pipeline(string pipeline)
	{
		Instance.Pipeline = pipeline;
		return this;
	}

	public BulkAllRequestDescriptor<T> RefreshIndices(Indices indicesToRefresh)
	{
		Instance.RefreshIndices = indicesToRefresh;
		return this;
	}

	public BulkAllRequestDescriptor<T> RefreshOnCompleted(bool refreshOnCompleted = true)
	{
		Instance.RefreshOnCompleted = refreshOnCompleted;
		return this;
	}

	public BulkAllRequestDescriptor<T> RetryDocumentPredicate(Func<ResponseItem, T, bool> predicate)
	{
		Instance.RetryDocumentPredicate = predicate;
		return this;
	}

	public BulkAllRequestDescriptor<T> Routing(Routing routing)
	{
		Instance.Routing = routing;
		return this;
	}

	public BulkAllRequestDescriptor<T> Size(int? size)
	{
		Instance.Size = size;
		return this;
	}

	public BulkAllRequestDescriptor<T> Timeout(Duration timeout)
	{
		Instance.Timeout = timeout;
		return this;
	}

	public BulkAllRequestDescriptor<T> WaitForActiveShards(WaitForActiveShards? shards)
	{
		Instance.WaitForActiveShards = shards;
		return this;
	}
}

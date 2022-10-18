// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using Elastic.Clients.Elasticsearch.Core.Bulk;

namespace Elastic.Clients.Elasticsearch;

public interface IBulkAllRequest<T>
{
	int? BackOffRetries { get; }
	Duration? BackOffTime { get; }
	ProducerConsumerBackPressure? BackPressure { get; }
	Action<BulkRequestDescriptor, IList<T>>? BufferToBulk { get; }
	Action<BulkResponse>? BulkResponseCallback { get; }
	bool ContinueAfterDroppedDocuments { get; }
	IEnumerable<T> Documents { get; }
	Action<ResponseItem, T>? DroppedDocumentCallback { get; }
	IndexName Index { get; }
	int? MaxDegreeOfParallelism { get; }
	string? Pipeline { get; }
	Indices? RefreshIndices { get; }
	bool RefreshOnCompleted { get; }
	Func<ResponseItem, T, bool>? RetryDocumentPredicate { get; }
	Routing? Routing { get; }
	int? Size { get; }
	Duration? Timeout { get; }
	WaitForActiveShards? WaitForActiveShards { get; }
}

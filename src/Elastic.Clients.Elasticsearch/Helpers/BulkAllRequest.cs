// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information


using System;
using System.Collections.Generic;

namespace Elastic.Clients.Elasticsearch.Helpers;

public sealed class BulkAllRequest<T>
{
	// TODO - Complete this

	public BulkAllRequest(IEnumerable<T> documents)
	{
		Documents = documents;
		Index = typeof(T);
	}

	public IEnumerable<T> Documents { get; }

	public IndexName Index { get; set; }

	public int? BackOffRetries { get; set; }

	public Time BackOffTime { get; set; }

	public ProducerConsumerBackPressure BackPressure { get; set; }

	public Action<BulkRequestDescriptor<T>, IList<T>> BufferToBulk { get; set; }

	public bool ContinueAfterDroppedDocuments { get; set; }

	public int? Size { get; set; }

	public Action<BulkResponse> BulkResponseCallback { get; set; }

	public int? MaxDegreeOfParallelism { get; set; }

	//public Action<BulkResponseItemBase, T> DroppedDocumentCallback { get; set; }
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch.Core.Bulk;

/// <summary>
/// Marker interface for types that can be serialised as an operation of a bulk API request.
/// </summary>
/// <remarks>Allows objects and descriptors to be stored in the same <see cref="BulkOperationsCollection"/>.</remarks>
public interface IBulkOperation
{
	void PrepareIndex(IndexName bulkRequestIndex);
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch;

public sealed partial class DeleteRequest<TDocument> : DeleteRequest
{
	public DeleteRequest(IndexName index, Id id) : base(index, id) { }

	public DeleteRequest(Id id) : this(typeof(TDocument), id)
	{
	}

	public DeleteRequest(TDocument documentWithId, IndexName index = null, Id id = null) : this(index ?? typeof(TDocument), id ?? Id.From(documentWithId)) { }
}

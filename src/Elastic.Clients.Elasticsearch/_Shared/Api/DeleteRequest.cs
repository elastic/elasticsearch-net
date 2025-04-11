// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;

namespace Elastic.Clients.Elasticsearch;

// Allows the generator to correctly detect that this type is used as base type, even when no code
// has been generated yet.
public partial class DeleteRequest;

public sealed class DeleteRequest<TDocument> : DeleteRequest
{
	[SetsRequiredMembers]
	public DeleteRequest(IndexName index, Id id) :
		base(index, id)
	{
	}

	[SetsRequiredMembers]
	public DeleteRequest(Id id) :
		this(typeof(TDocument), id)
	{
	}

	[SetsRequiredMembers]
	public DeleteRequest(TDocument documentWithId) :
		this(typeof(TDocument), Id.From(documentWithId))
	{
	}
}

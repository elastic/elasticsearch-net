// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

namespace Elastic.Clients.Elasticsearch;

// Allows the generator to correctly detect that this type is used as base type, even when no code
// has been generated yet.
public partial class CountRequest;

public sealed partial class CountRequest<TDocument> :
	CountRequest
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CountRequest() :
		base(typeof(TDocument))
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CountRequest(Indices index) :
		base(index)
	{
	}
}

public readonly partial struct CountRequestDescriptor
{
	[Obsolete("Use 'Indices()' instead.")]
	public CountRequestDescriptor Index(Indices indices)
	{
		Instance.Indices = indices;
		return this;
	}
}

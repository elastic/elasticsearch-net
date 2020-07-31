// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <summary>
	/// A request to the resolve index API
	/// </summary>
	[MapsApi("indices.resolve_index.json")]
	[ReadAs(typeof(ResolveIndexRequest))]
	public partial interface IResolveIndexRequest
	{
	}

	/// <inheritdoc cref="IResolveIndexRequest" />
	public partial class ResolveIndexRequest
	{
	}

	/// <inheritdoc cref="IResolveIndexRequest" />
	public partial class ResolveIndexDescriptor
	{
	}
}

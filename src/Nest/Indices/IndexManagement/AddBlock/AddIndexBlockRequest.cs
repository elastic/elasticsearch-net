// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <summary>
	/// A request to the indices add block API
	/// </summary>
	[MapsApi("indices.add_block.json")]
	[ReadAs(typeof(AddIndexBlockRequest))]
	public partial interface IAddIndexBlockRequest
	{
	}

	/// <inheritdoc cref="IAddIndexBlockRequest" />
	public partial class AddIndexBlockRequest
	{
	}

	/// <inheritdoc cref="IAddIndexBlockRequest" />
	public partial class AddIndexBlockDescriptor
	{
	}
}

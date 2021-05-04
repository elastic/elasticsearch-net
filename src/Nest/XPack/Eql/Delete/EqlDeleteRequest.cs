// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <summary>
	/// Request to deletes an async EQL search or a stored synchronous EQL search.
	/// The delete API also deletes results for the search.
	/// </summary>
	[MapsApi("eql.delete.json")]
	[ReadAs(typeof(EqlDeleteRequest))]
	public partial interface IEqlDeleteRequest { }

	/// <inheritdoc cref="IEqlDeleteRequest"/>
	public partial class EqlDeleteRequest
	{
	}

	/// <inheritdoc cref="IEqlDeleteRequest"/>
	public partial class EqlDeleteDescriptor
	{
	}
}

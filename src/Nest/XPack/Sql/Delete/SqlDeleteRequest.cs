// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <summary>
	/// Request to delete an async SQL search or a stored synchronous SQL search.
	/// If the search is still running, the API cancels it.
	/// </summary>
	[MapsApi("sql.delete_async")]
	[ReadAs(typeof(SqlDeleteRequest))]
	public partial interface ISqlDeleteRequest { }

	/// <inheritdoc cref="ISqlDeleteRequest"/>
	public partial class SqlDeleteRequest
	{
	}

	/// <inheritdoc cref="ISqlDeleteRequest"/>
	public partial class SqlDeleteDescriptor
	{
	}
}

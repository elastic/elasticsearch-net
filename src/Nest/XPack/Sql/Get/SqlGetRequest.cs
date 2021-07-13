// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <summary>
	/// Returns results for an async SQL search or a stored synchronous SQL search.
	/// </summary>
	[MapsApi("sql.get_async.json")]
	[ReadAs(typeof(SqlGetRequest))]
	public partial interface ISqlGetRequest { }

	/// <inheritdoc cref="ISqlGetRequest"/>
	public partial class SqlGetRequest { }

	/// <inheritdoc cref="ISqlGetRequest"/>
	public partial class SqlGetDescriptor { }
}

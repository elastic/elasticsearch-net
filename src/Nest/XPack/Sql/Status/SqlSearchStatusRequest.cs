// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	[MapsApi("sql.get_async_status")]
	[ReadAs(typeof(EqlSearchStatusRequest))]
	public partial interface ISqlSearchStatusRequest { }

	/// <inheritdoc cref="ISqlSearchStatusRequest"/>
	public partial class SqlSearchStatusRequest { }

	/// <inheritdoc cref="ISqlSearchStatusRequest"/>
	public partial class SqlSearchStatusDescriptor { }
}

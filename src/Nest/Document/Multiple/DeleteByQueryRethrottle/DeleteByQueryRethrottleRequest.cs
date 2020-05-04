// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿namespace Nest
{
	/// <summary>
	/// Rethrottles a running delete by query
	/// </summary>
	[MapsApi("delete_by_query_rethrottle")]
	public partial interface IDeleteByQueryRethrottleRequest { }

	/// <inheritdoc cref="IDeleteByQueryRethrottleRequest" />
	public partial class DeleteByQueryRethrottleRequest : IDeleteByQueryRethrottleRequest { }

	/// <inheritdoc cref="IDeleteByQueryRethrottleRequest" />
	public partial class DeleteByQueryRethrottleDescriptor : IDeleteByQueryRethrottleRequest { }
}

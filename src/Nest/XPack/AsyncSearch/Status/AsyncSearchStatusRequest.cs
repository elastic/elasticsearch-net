// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	[MapsApi("async_search.status.json")]
	[ReadAs(typeof(AsyncSearchStatusRequest))]
	public partial interface IAsyncSearchStatusRequest { }

	/// <inheritdoc cref="IAsyncSearchStatusRequest"/>
	public partial class AsyncSearchStatusRequest
	{
	}

	/// <inheritdoc cref="IAsyncSearchStatusRequest"/>
	public partial class AsyncSearchStatusDescriptor
	{
	}
}

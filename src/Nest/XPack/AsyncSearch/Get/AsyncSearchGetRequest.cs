// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	[MapsApi("async_search.get.json")]
	[ReadAs(typeof(AsyncSearchGetRequest))]
	public partial interface IAsyncSearchGetRequest { }

	/// <inheritdoc cref="IAsyncSearchGetRequest"/>
	public partial class AsyncSearchGetRequest
	{
	}

	/// <inheritdoc cref="IAsyncSearchGetRequest"/>
	public partial class AsyncSearchGetDescriptor
	{
	}
}

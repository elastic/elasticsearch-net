using System.Collections.Generic;

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

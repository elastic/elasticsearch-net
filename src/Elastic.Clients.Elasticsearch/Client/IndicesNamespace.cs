// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using System.Threading;

namespace Elastic.Clients.Elasticsearch.IndexManagement;

public partial class IndicesNamespace
{
	/// <summary>
	/// Refresh one or more indices. A refresh makes recent operations performed on one or more indices available for search. For data streams,
	/// the API runs the refresh operation on the stream’s backing indices. 
	/// </summary>
	/// <param name="indices">The index and/or data streams to refresh.</param>
	/// <returns>A <see cref="RefreshResponse"/> from the server.</returns>
	public RefreshResponse Refresh(Indices indices)
	{
		var request = new RefreshRequest(indices);
		request.BeforeRequest();
		return DoRequest<RefreshRequest, RefreshResponse>(request);
	}

	/// <summary>
	/// Refresh one or more indices. A refresh makes recent operations performed on one or more indices available for search. For data streams,
	/// the API runs the refresh operation on the stream’s backing indices. 
	/// </summary>
	/// <param name="indices">The index and/or data streams to refresh.</param>
	/// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the asynchronous operation.</param>
	/// <returns>A <see cref="RefreshResponse"/> from the server.</returns>
	public Task<RefreshResponse> RefreshAsync(Indices indices, CancellationToken cancellationToken = default)
	{
		var request = new RefreshRequest(indices);
		request.BeforeRequest();
		return DoRequestAsync<RefreshRequest, RefreshResponse>(request, cancellationToken);
	}
}

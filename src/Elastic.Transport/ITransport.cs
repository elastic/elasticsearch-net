// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading;
using System.Threading.Tasks;

namespace Elastic.Transport
{
	public interface ITransport<out TConnectionSettings>
		where TConnectionSettings : ITransportConfigurationValues

	{
		TConnectionSettings Settings { get; }

		TResponse Request<TResponse>(HttpMethod method, string path, PostData data = null, IRequestParameters requestParameters = null)
			where TResponse : class, ITransportResponse, new();

		Task<TResponse> RequestAsync<TResponse>(
			HttpMethod method, string path, CancellationToken ctx, PostData data = null, IRequestParameters requestParameters = null
		)
			where TResponse : class, ITransportResponse, new();
	}
}

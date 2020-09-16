// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public interface ITransport<out TConnectionSettings>
		where TConnectionSettings : IConnectionConfigurationValues

	{
		TConnectionSettings Settings { get; }

		TResponse Request<TResponse>(HttpMethod method, string path, PostData data = null, IRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new();

		Task<TResponse> RequestAsync<TResponse>(
			HttpMethod method, string path, CancellationToken ctx, PostData data = null, IRequestParameters requestParameters = null
		)
			where TResponse : class, IElasticsearchResponse, new();
	}
}

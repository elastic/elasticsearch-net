/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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

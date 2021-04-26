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

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Nest
{
	internal class TranslateSqlResponseBuilder : CustomResponseBuilderBase
	{
		public static TranslateSqlResponseBuilder Instance { get; } = new TranslateSqlResponseBuilder();

		public override object DeserializeResponse(ITransportSerializer builtInSerializer, IApiCallDetails response, Stream stream) =>
			response.Success
				? new TranslateSqlResponse { Result = builtInSerializer.Deserialize<ISearchRequest>(stream) }
				: new TranslateSqlResponse();

		public override async Task<object> DeserializeResponseAsync(
			ITransportSerializer builtInSerializer,
			IApiCallDetails response,
			Stream stream,
			CancellationToken ctx = default
		) =>
			response.Success
				? new TranslateSqlResponse
				{
					Result = await builtInSerializer.DeserializeAsync<ISearchRequest>(stream, ctx).ConfigureAwait(false)
				}
				: new TranslateSqlResponse();
	}
}

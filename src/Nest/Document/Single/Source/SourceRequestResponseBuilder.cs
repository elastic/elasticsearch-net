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
using Nest.Utf8Json;

namespace Nest
{
	public class SourceRequestResponseBuilder<TDocument> : CustomResponseBuilderBase
	{
		public static SourceRequestResponseBuilder<TDocument> Instance { get; } = new SourceRequestResponseBuilder<TDocument>();

		public override object DeserializeResponse(ITransportSerializer builtInSerializer, IApiCallDetails response, Stream stream)
		{
			if (response.Success)
			{
				if (builtInSerializer is IInternalSerializer internalSerializer &&
					internalSerializer.TryGetJsonFormatter(out var formatter))
				{
					var sourceSerializer = formatter.GetConnectionSettings().SourceSerializer;
					return new SourceResponse<TDocument> { Body = sourceSerializer.Deserialize<TDocument>(stream) };
				}

				return new SourceResponse<TDocument> { Body = builtInSerializer.Deserialize<TDocument>(stream) };
			}

			return new SourceResponse<TDocument>();
		}

		public override async Task<object> DeserializeResponseAsync(ITransportSerializer builtInSerializer, IApiCallDetails response, Stream stream, CancellationToken ctx = default)
		{
			if (response.Success)
			{
				if (builtInSerializer is IInternalSerializer internalSerializer &&
					internalSerializer.TryGetJsonFormatter(out var formatter))
				{
					var sourceSerializer = formatter.GetConnectionSettings().SourceSerializer;
					return new SourceResponse<TDocument>
					{
						Body = await sourceSerializer.DeserializeAsync<TDocument>(stream, ctx).ConfigureAwait(false)
					};
				}

				return new SourceResponse<TDocument>
				{
					Body = await builtInSerializer.DeserializeAsync<TDocument>(stream, ctx).ConfigureAwait(false)
				};
			}

			return new SourceResponse<TDocument>();
		}
	}
}

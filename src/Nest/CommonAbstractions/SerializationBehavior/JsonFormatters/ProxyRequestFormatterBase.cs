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

using Elastic.Transport;
using Nest.Utf8Json;

namespace Nest
{
	internal abstract class ProxyRequestFormatterBase<TRequestInterface, TRequest> : IJsonFormatter<TRequestInterface>
		where TRequestInterface : IProxyRequest
		where TRequest : TRequestInterface
	{
		public TRequestInterface Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			// TODO: Look at optimizing this. It looks like this could be replaced with SourceFormatter<T> on Document and a serialization ctor
			var segment = reader.ReadNextBlockSegment();
			var settings = formatterResolver.GetConnectionSettings();
			using (var ms = settings.MemoryStreamFactory.Create(segment.Array, segment.Offset, segment.Count))
			{
				//not optimized but deserializing create requests is far from common practice
				var genericType = typeof(TRequest).GenericTypeArguments[0];
				var o = settings.SourceSerializer.Deserialize(genericType, ms);

				// TRequest might be an open or closed generic type
				var request = typeof(TRequest).IsGenericTypeDefinition
					? (TRequest)typeof(TRequest).CreateGenericInstance(genericType, o, null, null)
					: (TRequest)typeof(TRequest).CreateInstance(o, null, null);

				return request;
			}
		}

		public void Serialize(ref JsonWriter writer, TRequestInterface value, IJsonFormatterResolver formatterResolver)
		{
			var untypedDocumentRequest = (IProxyRequest)value;
			var settings = formatterResolver.GetConnectionSettings();
			var serializer = settings.SourceSerializer;

			using (var ms = settings.MemoryStreamFactory.Create())
			{
				untypedDocumentRequest.WriteJson(serializer, ms, SerializationFormatting.None);
				writer.WriteRaw(ms);
			}
		}
	}
}

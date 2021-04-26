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

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>The built in internal serializer that the high level client NEST uses.</summary>
	internal class DefaultHighLevelSerializer : ITransportSerializer, IInternalSerializer
	{
		public DefaultHighLevelSerializer(IJsonFormatterResolver formatterResolver) => FormatterResolver = formatterResolver;

		private IJsonFormatterResolver FormatterResolver { get; }

		bool IInternalSerializer.TryGetJsonFormatter(out IJsonFormatterResolver formatterResolver)
		{
			formatterResolver = FormatterResolver;
			return true;
		}

		public T Deserialize<T>(Stream stream) =>
			JsonSerializer.Deserialize<T>(stream, FormatterResolver);

		public object Deserialize(Type type, Stream stream) =>
			JsonSerializer.NonGeneric.Deserialize(type, stream, FormatterResolver);

		public Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default) =>
			JsonSerializer.DeserializeAsync<T>(stream, FormatterResolver);

		public Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default) =>
			JsonSerializer.NonGeneric.DeserializeAsync(type, stream, FormatterResolver);

		public virtual void Serialize<T>(T data, Stream writableStream, SerializationFormatting formatting = SerializationFormatting.None) =>
			JsonSerializer.Serialize(writableStream, data, FormatterResolver);

		public Task SerializeAsync<T>(T data, Stream stream, SerializationFormatting formatting = SerializationFormatting.None,
			CancellationToken cancellationToken = default
		) => JsonSerializer.SerializeAsync(stream, data, FormatterResolver);

	}
}

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
using Elasticsearch.Net.Utf8Json;

namespace Elasticsearch.Net
{
	public class LowLevelRequestResponseSerializer : IElasticsearchSerializer, IInternalSerializer
	{
		public static readonly LowLevelRequestResponseSerializer Instance = new LowLevelRequestResponseSerializer();

		public object Deserialize(Type type, Stream stream) =>
			JsonSerializer.NonGeneric.Deserialize(type, stream, ElasticsearchNetFormatterResolver.Instance);

		public T Deserialize<T>(Stream stream) =>
			JsonSerializer.Deserialize<T>(stream, ElasticsearchNetFormatterResolver.Instance);

		public Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default) =>
			JsonSerializer.NonGeneric.DeserializeAsync(type, stream, ElasticsearchNetFormatterResolver.Instance);

		public Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default) =>
			JsonSerializer.DeserializeAsync<T>(stream, ElasticsearchNetFormatterResolver.Instance);

		public void Serialize<T>(T data, Stream writableStream, SerializationFormatting formatting = SerializationFormatting.None) =>
			JsonSerializer.Serialize(writableStream, data, ElasticsearchNetFormatterResolver.Instance);

		public Task SerializeAsync<T>(T data, Stream writableStream, SerializationFormatting formatting,
			CancellationToken cancellationToken = default
		) =>
			JsonSerializer.SerializeAsync(writableStream, data, ElasticsearchNetFormatterResolver.Instance);

		bool IInternalSerializer.TryGetJsonFormatter(out IJsonFormatterResolver formatterResolver)
		{
			formatterResolver = ElasticsearchNetFormatterResolver.Instance;
			return true;
		}
	}
}

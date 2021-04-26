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

using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	internal class CollapsedSourceFormatter<T> : SourceFormatter<T>
	{
		public override SerializationFormatting? ForceFormatting { get; } = SerializationFormatting.None;
	}

	internal class SourceFormatter<T> : IJsonFormatter<T>
	{
		public virtual SerializationFormatting? ForceFormatting { get; } = null;

		/// <summary>
		/// If SourceSerializer exposes a formatter we can use it directly
		/// </summary>
		private static bool AttemptFastPath(IElasticsearchSerializer serializer, out IJsonFormatterResolver formatter)
		{
			formatter = null;
			return serializer is IInternalSerializer s && s.TryGetJsonFormatter(out formatter);
		}


		public T Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var settings = formatterResolver.GetConnectionSettings();

			var sourceSerializer = settings.SourceSerializer;
			if (AttemptFastPath(sourceSerializer, out var formatter))
				return formatter.GetFormatter<T>().Deserialize(ref reader, formatter);

			var arraySegment = reader.ReadNextBlockSegment();
			using (var ms = settings.MemoryStreamFactory.Create(arraySegment.Array, arraySegment.Offset, arraySegment.Count))
				return sourceSerializer.Deserialize<T>(ms);
		}


		public virtual void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver)
		{
			var settings = formatterResolver.GetConnectionSettings();

			var sourceSerializer = settings.SourceSerializer;
			if (AttemptFastPath(sourceSerializer, out var formatter))
			{
				formatter.GetFormatter<T>().Serialize(ref writer, value, formatter);
				return;
			}

			var f = ForceFormatting ?? SerializationFormatting.None;
			writer.WriteSerialized(value, sourceSerializer, settings, f);
		}
	}
}

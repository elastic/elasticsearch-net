// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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

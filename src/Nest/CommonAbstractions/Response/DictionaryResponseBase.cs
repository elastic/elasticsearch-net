using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Utf8Json;
using Utf8Json.Internal;

namespace Nest
{
	public interface IDictionaryResponse<TKey, TValue> : IResponse
	{
		IReadOnlyDictionary<TKey, TValue> BackingDictionary { get; set; }
	}

	public abstract class DictionaryResponseBase<TKey, TValue> : ResponseBase, IDictionaryResponse<TKey, TValue>
	{
		protected IDictionaryResponse<TKey, TValue> Self => this;

		IReadOnlyDictionary<TKey, TValue> IDictionaryResponse<TKey, TValue>.BackingDictionary { get; set; } =
			EmptyReadOnly<TKey, TValue>.Dictionary;
	}

	internal class DictionaryResponseFormatterHelpers
	{
		private static readonly AutomataDictionary AutomataDictionary = new AutomataDictionary
		{
			{ "error", 0 },
			{ "status", 1 }
		};

		public static ArraySegment<byte> ReadServerErrorFirst(
			ref JsonReader reader,
			IJsonFormatterResolver formatterResolver,
			out Error error,
			out int? statusCode
		)
		{
			var segment = reader.ReadNextBlockSegment();
			var segmentReader = new JsonReader(segment.Array, segment.Offset);
			var count = 0;
			error = null;
			statusCode = null;

			while (segmentReader.ReadIsInObject(ref count))
			{
				var propertyName = segmentReader.ReadPropertyNameSegmentRaw();
				if (AutomataDictionary.TryGetValue(propertyName, out var value))
				{
					switch (value)
					{
						case 0:
							if (segmentReader.GetCurrentJsonToken() == JsonToken.String)
								error = new Error { Reason = reader.ReadString() };
							else
							{
								var formatter = formatterResolver.GetFormatter<Error>();
								error = formatter.Deserialize(ref segmentReader, formatterResolver);
							}
							break;
						case 1:
							if (segmentReader.GetCurrentJsonToken() == JsonToken.Number)
								statusCode = segmentReader.ReadInt32();
							break;
					}
				}
			}

			return segment;
		}
	}

	internal class DictionaryResponseFormatter<TResponse, TKey, TValue> : IJsonFormatter<TResponse>
		where TResponse : ResponseBase, IDictionaryResponse<TKey, TValue>, new()
	{
		public TResponse Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var segment = DictionaryResponseFormatterHelpers.ReadServerErrorFirst(ref reader, formatterResolver, out var error,
				out var statusCode);
			var segmentReader = new JsonReader(segment.Array, segment.Offset);
			var formatter = formatterResolver.GetFormatter<Dictionary<TKey, TValue>>();
			var dict = formatter.Deserialize(ref segmentReader, formatterResolver);

			var response = new TResponse
			{
				BackingDictionary = dict,
				Error = error,
				StatusCode = statusCode
			};
			return response;
		}

		public void Serialize(ref JsonWriter writer, TResponse value, IJsonFormatterResolver formatterResolver) => throw new NotSupportedException();
	}
}

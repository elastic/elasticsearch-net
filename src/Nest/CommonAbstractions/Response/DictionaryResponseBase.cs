// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch.Failures;
using Nest.Utf8Json;

namespace Nest
{
	//TODO can this interface in favor of the base class?
	public interface IDictionaryResponse<TKey, TValue> : IResponse
	{
		IReadOnlyDictionary<TKey, TValue> BackingDictionary { get; set; }
	}

	public abstract class DictionaryResponseBase<TKey, TValue> : ResponseBase, IDictionaryResponse<TKey, TValue>
	{
		[IgnoreDataMember]
		protected IDictionaryResponse<TKey, TValue> Self => this;

		IReadOnlyDictionary<TKey, TValue> IDictionaryResponse<TKey, TValue>.BackingDictionary { get; set; } =
			EmptyReadOnly<TKey, TValue>.Dictionary;
	}

	internal class ResponseFormatterHelpers
	{
		internal static readonly AutomataDictionary ServerErrorFields = new AutomataDictionary
		{
			{ "error", 0 },
			{ "status", 1 }
		};
	}

	internal class DictionaryResponseFormatter<TResponse, TKey, TValue> : IJsonFormatter<TResponse>
		where TResponse : ResponseBase, IDictionaryResponse<TKey, TValue>, new()
	{
		public TResponse Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var response = new TResponse();
			var keyFormatter = formatterResolver.GetFormatter<TKey>();
			var valueFormatter = formatterResolver.GetFormatter<TValue>();
			var dictionary = new Dictionary<TKey, TValue>();
			var count = 0;

			while (reader.ReadIsInObject(ref count))
			{
				var property = reader.ReadPropertyNameSegmentRaw();
				if (ResponseFormatterHelpers.ServerErrorFields.TryGetValue(property, out var errorValue))
				{
					switch (errorValue)
					{
						case 0:
							if (reader.GetCurrentJsonToken() == JsonToken.String)
								response.Error = new Error { Reason = reader.ReadString() };
							else
							{
								var formatter = formatterResolver.GetFormatter<Error>();
								response.Error = formatter.Deserialize(ref reader, formatterResolver);
							}
							break;
						case 1:
							if (reader.GetCurrentJsonToken() == JsonToken.Number)
								response.StatusCode = reader.ReadInt32();
							else
								reader.ReadNextBlock();
							break;
					}
				}
				else
				{
					// include opening string quote in reader (offset - 1)
					var propertyReader = new JsonReader(property.Array, property.Offset - 1);
					var key = keyFormatter.Deserialize(ref propertyReader, formatterResolver);
					var value = valueFormatter.Deserialize(ref reader, formatterResolver);
					dictionary.Add(key, value);
				}
			}

			response.BackingDictionary = dictionary;
			return response;
		}

		public void Serialize(ref JsonWriter writer, TResponse value, IJsonFormatterResolver formatterResolver) => throw new NotSupportedException();
	}
}

using System;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.Core;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace Elastic.Clients.Elasticsearch
{
	internal sealed class PercentageConverter : JsonConverter<Percentage>
	{
		public override Percentage Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var token = reader.TokenType;

			switch (token)
			{
				case JsonTokenType.String:
				{
					var value = reader.GetString();
					var result = (Percentage)Activator.CreateInstance(typeof(Percentage), value);
					return result;
				}
				case JsonTokenType.Number:
				{
					var value = reader.GetSingle();
					var result = (Percentage)Activator.CreateInstance(typeof(Percentage), value);
					return result;
				}
			}

			throw new SerializationException();
		}

		public override void Write(Utf8JsonWriter writer, Percentage value, JsonSerializerOptions options) =>
			throw new NotImplementedException();
	}

	
}

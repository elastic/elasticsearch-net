// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch
{
	[JsonConverter(typeof(SourceConfigConverter))]
	public partial class SourceConfig
	{
		public bool HasBoolValue => Tag == 0;

		public bool HasSourceFilterValue => Tag == 1;

		// TODO - Not null when
		public bool TryGetBool(out bool? value)
		{
			if (Tag == 0)
			{
				value = Item1;
				return true;
			}

			value = null;
			return false;
		}

		public bool TryGetSourceFilter(out SourceFilter? value)
		{
			if (Tag == 1)
			{
				value = Item2;
				return true;
			}

			value = null;
			return false;
		}
	}

	internal class SourceConfigConverter : JsonConverter<SourceConfig>
	{
		public override SourceConfig? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			switch (reader.TokenType)
			{
				case JsonTokenType.True:
				case JsonTokenType.False:
					var value = reader.GetBoolean();
					return new SourceConfig(value);

				case JsonTokenType.StartObject:
					var sourceFilter = JsonSerializer.Deserialize<SourceFilter>(ref reader, options);
					return new SourceConfig(sourceFilter);
			}

			return null;
		}

		public override void Write(Utf8JsonWriter writer, SourceConfig value, JsonSerializerOptions options) => throw new NotImplementedException();
	}
}

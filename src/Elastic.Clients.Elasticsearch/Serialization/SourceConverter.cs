// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch
{
	internal sealed class SourceConverter<T> : JsonConverter<SourceMarker<T>>
	{
		private readonly IElasticsearchClientSettings _settings;

		public SourceConverter(IElasticsearchClientSettings settings) => _settings = settings;

		public override SourceMarker<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => new()
		{
			Source = SourceSerialisation.Deserialize<T>(ref reader, _settings)
		};

		public override void Write(Utf8JsonWriter writer, SourceMarker<T> value, JsonSerializerOptions options) => SourceSerialisation.Serialize<T>(value.Source, writer, _settings);
	}
}

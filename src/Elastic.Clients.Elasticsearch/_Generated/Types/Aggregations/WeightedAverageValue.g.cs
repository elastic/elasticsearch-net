// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Aggregations
{
	public partial class WeightedAverageValue
	{
		[JsonInclude]
		[JsonPropertyName("field")]
		public string? Field { get; set; }

		[JsonInclude]
		[JsonPropertyName("missing")]
		public double? Missing { get; set; }

		[JsonInclude]
		[JsonPropertyName("script")]
		public Elastic.Clients.Elasticsearch.Script? Script { get; set; }
	}

	public sealed partial class WeightedAverageValueDescriptor<T> : DescriptorBase<WeightedAverageValueDescriptor<T>>
	{
		public WeightedAverageValueDescriptor()
		{
		}

		internal WeightedAverageValueDescriptor(Action<WeightedAverageValueDescriptor<T>> configure) => configure.Invoke(this);
		internal string? FieldValue { get; private set; }

		internal double? MissingValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Script? ScriptValue { get; private set; }

		public WeightedAverageValueDescriptor<T> Field(string? field) => Assign(field, (a, v) => a.FieldValue = v);
		public WeightedAverageValueDescriptor<T> Missing(double? missing) => Assign(missing, (a, v) => a.MissingValue = v);
		public WeightedAverageValueDescriptor<T> Script(Elastic.Clients.Elasticsearch.Script? script) => Assign(script, (a, v) => a.ScriptValue = v);
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (FieldValue is not null)
			{
				writer.WritePropertyName("field");
				JsonSerializer.Serialize(writer, FieldValue, options);
			}

			if (MissingValue.HasValue)
			{
				writer.WritePropertyName("missing");
				writer.WriteNumberValue(MissingValue.Value);
			}

			if (ScriptValue is not null)
			{
				writer.WritePropertyName("script");
				JsonSerializer.Serialize(writer, ScriptValue, options);
			}

			writer.WriteEndObject();
		}
	}
}
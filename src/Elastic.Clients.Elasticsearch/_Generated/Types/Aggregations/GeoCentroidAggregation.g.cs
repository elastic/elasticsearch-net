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
	public partial class GeoCentroidAggregation : Aggregations.MetricAggregationBase, IAggregationContainerVariant
	{
		public GeoCentroidAggregation(string name) : base(name)
		{
		}

		[JsonIgnore]
		string Aggregations.IAggregationContainerVariant.AggregationContainerVariantName => "geo_centroid";
		[JsonInclude]
		[JsonPropertyName("count")]
		public long? Count { get; set; }

		[JsonInclude]
		[JsonPropertyName("location")]
		public Elastic.Clients.Elasticsearch.GeoLocation? Location { get; set; }
	}

	public sealed partial class GeoCentroidAggregationDescriptor : DescriptorBase<GeoCentroidAggregationDescriptor>
	{
		public GeoCentroidAggregationDescriptor()
		{
		}

		internal GeoCentroidAggregationDescriptor(Action<GeoCentroidAggregationDescriptor> configure) => configure.Invoke(this);
		internal long? CountValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.GeoLocation? LocationValue { get; private set; }

		public GeoCentroidAggregationDescriptor Count(long? count) => Assign(count, (a, v) => a.CountValue = v);
		public GeoCentroidAggregationDescriptor Location(Elastic.Clients.Elasticsearch.GeoLocation? location) => Assign(location, (a, v) => a.LocationValue = v);
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (CountValue.HasValue)
			{
				writer.WritePropertyName("count");
				writer.WriteNumberValue(CountValue.Value);
			}

			if (LocationValue is not null)
			{
				writer.WritePropertyName("location");
				JsonSerializer.Serialize(writer, LocationValue, options);
			}

			writer.WriteEndObject();
		}
	}
}
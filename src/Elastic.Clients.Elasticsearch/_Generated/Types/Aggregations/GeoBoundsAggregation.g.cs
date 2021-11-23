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
	public partial class GeoBoundsAggregation : Aggregations.MetricAggregationBase, IAggregationContainerVariant
	{
		public GeoBoundsAggregation(string name) : base(name)
		{
		}

		[JsonIgnore]
		string Aggregations.IAggregationContainerVariant.AggregationContainerVariantName => "geo_bounds";
		[JsonInclude]
		[JsonPropertyName("wrap_longitude")]
		public bool? WrapLongitude { get; set; }
	}

	public sealed partial class GeoBoundsAggregationDescriptor : DescriptorBase<GeoBoundsAggregationDescriptor>
	{
		public GeoBoundsAggregationDescriptor()
		{
		}

		internal GeoBoundsAggregationDescriptor(Action<GeoBoundsAggregationDescriptor> configure) => configure.Invoke(this);
		internal bool? WrapLongitudeValue { get; private set; }

		public GeoBoundsAggregationDescriptor WrapLongitude(bool? wrapLongitude = true) => Assign(wrapLongitude, (a, v) => a.WrapLongitudeValue = v);
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (WrapLongitudeValue.HasValue)
			{
				writer.WritePropertyName("wrap_longitude");
				writer.WriteBooleanValue(WrapLongitudeValue.Value);
			}

			writer.WriteEndObject();
		}
	}
}
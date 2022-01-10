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
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Aggregations
{
	internal sealed class GeoLineAggregationConverter : JsonConverter<GeoLineAggregation>
	{
		public override GeoLineAggregation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException("Unexpected JSON detected.");
			reader.Read();
			var aggName = reader.GetString();
			if (aggName != "geo_line")
				throw new JsonException("Unexpected JSON detected.");
			var agg = new GeoLineAggregation(aggName);
			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					if (reader.ValueTextEquals("include_sort"))
					{
						var value = JsonSerializer.Deserialize<bool?>(ref reader, options);
						if (value is not null)
						{
							agg.IncludeSort = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("point"))
					{
						var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.GeoLinePoint>(ref reader, options);
						if (value is not null)
						{
							agg.Point = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("size"))
					{
						var value = JsonSerializer.Deserialize<int?>(ref reader, options);
						if (value is not null)
						{
							agg.Size = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("sort"))
					{
						var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.GeoLineSort>(ref reader, options);
						if (value is not null)
						{
							agg.Sort = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("sort_order"))
					{
						var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.SortOrder?>(ref reader, options);
						if (value is not null)
						{
							agg.SortOrder = value;
						}

						continue;
					}
				}
			}

			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					if (reader.ValueTextEquals("meta"))
					{
						var value = JsonSerializer.Deserialize<Dictionary<string, object>>(ref reader, options);
						if (value is not null)
						{
							agg.Meta = value;
						}

						continue;
					}
				}
			}

			reader.Read();
			return agg;
		}

		public override void Write(Utf8JsonWriter writer, GeoLineAggregation value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("geo_line");
			writer.WriteStartObject();
			if (value.IncludeSort.HasValue)
			{
				writer.WritePropertyName("include_sort");
				writer.WriteBooleanValue(value.IncludeSort.Value);
			}

			writer.WritePropertyName("point");
			JsonSerializer.Serialize(writer, value.Point, options);
			if (value.Size.HasValue)
			{
				writer.WritePropertyName("size");
				writer.WriteNumberValue(value.Size.Value);
			}

			writer.WritePropertyName("sort");
			JsonSerializer.Serialize(writer, value.Sort, options);
			if (value.SortOrder is not null)
			{
				writer.WritePropertyName("sort_order");
				JsonSerializer.Serialize(writer, value.SortOrder, options);
			}

			writer.WriteEndObject();
			if (value.Meta is not null)
			{
				writer.WritePropertyName("meta");
				JsonSerializer.Serialize(writer, value.Meta, options);
			}

			writer.WriteEndObject();
		}
	}

	[JsonConverter(typeof(GeoLineAggregationConverter))]
	public partial class GeoLineAggregation : Aggregations.AggregationBase
	{
		public GeoLineAggregation(string name) : base(name)
		{
		}

		[JsonInclude]
		[JsonPropertyName("include_sort")]
		public bool? IncludeSort { get; set; }

		[JsonInclude]
		[JsonPropertyName("point")]
		public Elastic.Clients.Elasticsearch.Aggregations.GeoLinePoint Point { get; set; }

		[JsonInclude]
		[JsonPropertyName("size")]
		public int? Size { get; set; }

		[JsonInclude]
		[JsonPropertyName("sort")]
		public Elastic.Clients.Elasticsearch.Aggregations.GeoLineSort Sort { get; set; }

		[JsonInclude]
		[JsonPropertyName("sort_order")]
		public Elastic.Clients.Elasticsearch.SortOrder? SortOrder { get; set; }
	}

	public sealed partial class GeoLineAggregationDescriptor<TDocument> : DescriptorBase<GeoLineAggregationDescriptor<TDocument>>
	{
		public GeoLineAggregationDescriptor()
		{
		}

		internal GeoLineAggregationDescriptor(Action<GeoLineAggregationDescriptor<TDocument>> configure) => configure.Invoke(this);
		internal bool? IncludeSortValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Aggregations.GeoLinePoint PointValue { get; private set; }

		internal int? SizeValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Aggregations.GeoLineSort SortValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.SortOrder? SortOrderValue { get; private set; }

		internal Dictionary<string, object>? MetaValue { get; private set; }

		internal GeoLinePointDescriptor<TDocument> PointDescriptor { get; private set; }

		internal GeoLineSortDescriptor<TDocument> SortDescriptor { get; private set; }

		internal Action<GeoLinePointDescriptor<TDocument>> PointDescriptorAction { get; private set; }

		internal Action<GeoLineSortDescriptor<TDocument>> SortDescriptorAction { get; private set; }

		public GeoLineAggregationDescriptor<TDocument> IncludeSort(bool? includeSort = true) => Assign(includeSort, (a, v) => a.IncludeSortValue = v);
		public GeoLineAggregationDescriptor<TDocument> Point(Elastic.Clients.Elasticsearch.Aggregations.GeoLinePoint point)
		{
			PointDescriptor = null;
			PointDescriptorAction = null;
			return Assign(point, (a, v) => a.PointValue = v);
		}

		public GeoLineAggregationDescriptor<TDocument> Point(Aggregations.GeoLinePointDescriptor<TDocument> descriptor)
		{
			PointValue = null;
			PointDescriptorAction = null;
			return Assign(descriptor, (a, v) => a.PointDescriptor = v);
		}

		public GeoLineAggregationDescriptor<TDocument> Point(Action<Aggregations.GeoLinePointDescriptor<TDocument>> configure)
		{
			PointValue = null;
			PointDescriptorAction = null;
			return Assign(configure, (a, v) => a.PointDescriptorAction = v);
		}

		public GeoLineAggregationDescriptor<TDocument> Size(int? size) => Assign(size, (a, v) => a.SizeValue = v);
		public GeoLineAggregationDescriptor<TDocument> Sort(Elastic.Clients.Elasticsearch.Aggregations.GeoLineSort sort)
		{
			SortDescriptor = null;
			SortDescriptorAction = null;
			return Assign(sort, (a, v) => a.SortValue = v);
		}

		public GeoLineAggregationDescriptor<TDocument> Sort(Aggregations.GeoLineSortDescriptor<TDocument> descriptor)
		{
			SortValue = null;
			SortDescriptorAction = null;
			return Assign(descriptor, (a, v) => a.SortDescriptor = v);
		}

		public GeoLineAggregationDescriptor<TDocument> Sort(Action<Aggregations.GeoLineSortDescriptor<TDocument>> configure)
		{
			SortValue = null;
			SortDescriptorAction = null;
			return Assign(configure, (a, v) => a.SortDescriptorAction = v);
		}

		public GeoLineAggregationDescriptor<TDocument> SortOrder(Elastic.Clients.Elasticsearch.SortOrder? sortOrder) => Assign(sortOrder, (a, v) => a.SortOrderValue = v);
		public GeoLineAggregationDescriptor<TDocument> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) => Assign(selector, (a, v) => a.MetaValue = v?.Invoke(new FluentDictionary<string, object>()));
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("geo_line");
			writer.WriteStartObject();
			if (IncludeSortValue.HasValue)
			{
				writer.WritePropertyName("include_sort");
				writer.WriteBooleanValue(IncludeSortValue.Value);
			}

			if (PointDescriptor is not null)
			{
				writer.WritePropertyName("point");
				JsonSerializer.Serialize(writer, PointDescriptor, options);
			}
			else if (PointDescriptorAction is not null)
			{
				writer.WritePropertyName("point");
				JsonSerializer.Serialize(writer, new Aggregations.GeoLinePointDescriptor<TDocument>(PointDescriptorAction), options);
			}
			else
			{
				writer.WritePropertyName("point");
				JsonSerializer.Serialize(writer, PointValue, options);
			}

			if (SizeValue.HasValue)
			{
				writer.WritePropertyName("size");
				writer.WriteNumberValue(SizeValue.Value);
			}

			if (SortDescriptor is not null)
			{
				writer.WritePropertyName("sort");
				JsonSerializer.Serialize(writer, SortDescriptor, options);
			}
			else if (SortDescriptorAction is not null)
			{
				writer.WritePropertyName("sort");
				JsonSerializer.Serialize(writer, new Aggregations.GeoLineSortDescriptor<TDocument>(SortDescriptorAction), options);
			}
			else
			{
				writer.WritePropertyName("sort");
				JsonSerializer.Serialize(writer, SortValue, options);
			}

			if (SortOrderValue is not null)
			{
				writer.WritePropertyName("sort_order");
				JsonSerializer.Serialize(writer, SortOrderValue, options);
			}

			writer.WriteEndObject();
			if (MetaValue is not null)
			{
				writer.WritePropertyName("meta");
				JsonSerializer.Serialize(writer, MetaValue, options);
			}

			writer.WriteEndObject();
		}
	}
}
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
namespace Elastic.Clients.Elasticsearch.TransformManagement
{
	public interface IPivotGroupByContainerVariant
	{
		string PivotGroupByContainerVariantName { get; }
	}

	[JsonConverter(typeof(PivotGroupByContainerConverter))]
	public partial class PivotGroupByContainer : IContainer
	{
		public PivotGroupByContainer(IPivotGroupByContainerVariant variant) => Variant = variant ?? throw new ArgumentNullException(nameof(variant));
		internal IPivotGroupByContainerVariant Variant { get; }
	}

	internal sealed class PivotGroupByContainerConverter : JsonConverter<PivotGroupByContainer>
	{
		public override PivotGroupByContainer Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			reader.Read();
			if (reader.TokenType != JsonTokenType.PropertyName)
			{
				throw new JsonException();
			}

			var propertyName = reader.GetString();
			if (propertyName == "date_histogram")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.DateHistogramAggregation?>(ref reader, options);
				reader.Read();
				return new PivotGroupByContainer(variant);
			}

			if (propertyName == "geotile_grid")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.GeoTileGridAggregation?>(ref reader, options);
				reader.Read();
				return new PivotGroupByContainer(variant);
			}

			if (propertyName == "histogram")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.HistogramAggregation?>(ref reader, options);
				reader.Read();
				return new PivotGroupByContainer(variant);
			}

			if (propertyName == "terms")
			{
				var variant = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.TermsAggregation?>(ref reader, options);
				reader.Read();
				return new PivotGroupByContainer(variant);
			}

			throw new JsonException();
		}

		public override void Write(Utf8JsonWriter writer, PivotGroupByContainer value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName(value.Variant.PivotGroupByContainerVariantName);
			switch (value.Variant)
			{
				case Elastic.Clients.Elasticsearch.Aggregations.DateHistogramAggregation variant:
					JsonSerializer.Serialize(writer, variant, options);
					break;
				case Elastic.Clients.Elasticsearch.Aggregations.GeoTileGridAggregation variant:
					JsonSerializer.Serialize(writer, variant, options);
					break;
				case Elastic.Clients.Elasticsearch.Aggregations.HistogramAggregation variant:
					JsonSerializer.Serialize(writer, variant, options);
					break;
				case Elastic.Clients.Elasticsearch.Aggregations.TermsAggregation variant:
					JsonSerializer.Serialize(writer, variant, options);
					break;
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class PivotGroupByContainerDescriptor<T> : DescriptorBase<PivotGroupByContainerDescriptor<T>>
	{
		public PivotGroupByContainerDescriptor()
		{
		}

		internal PivotGroupByContainerDescriptor(Action<PivotGroupByContainerDescriptor<T>> configure) => configure.Invoke(this);
		internal bool ContainsVariant { get; private set; }

		internal string ContainedVariantName { get; private set; }

		internal PivotGroupByContainer Container { get; private set; }

		internal object ContainerVariantDescriptorAction { get; private set; }

		private void Set(object descriptorAction, string variantName)
		{
			if (ContainsVariant)
				throw new Exception("TODO");
			ContainerVariantDescriptorAction = descriptorAction;
			ContainedVariantName = variantName;
			ContainsVariant = true;
		}

		private void Set(IPivotGroupByContainerVariant variant, string variantName)
		{
			if (ContainsVariant)
				throw new Exception("TODO");
			Container = new PivotGroupByContainer(variant);
			ContainedVariantName = variantName;
			ContainsVariant = true;
		}

		public void DateHistogram(Aggregations.DateHistogramAggregation variant) => Set(variant, "date_histogram");
		public void DateHistogram(Action<Aggregations.DateHistogramAggregationDescriptor<T>> configure) => Set(configure, "date_histogram");
		public void GeotileGrid(Aggregations.GeoTileGridAggregation variant) => Set(variant, "geotile_grid");
		public void GeotileGrid(Action<Aggregations.GeoTileGridAggregationDescriptor<T>> configure) => Set(configure, "geotile_grid");
		public void Histogram(Aggregations.HistogramAggregation variant) => Set(variant, "histogram");
		public void Histogram(Action<Aggregations.HistogramAggregationDescriptor<T>> configure) => Set(configure, "histogram");
		public void Terms(Aggregations.TermsAggregation variant) => Set(variant, "terms");
		public void Terms(Action<Aggregations.TermsAggregationDescriptor<T>> configure) => Set(configure, "terms");
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			if (!ContainsVariant)
			{
				writer.WriteNullValue();
				return;
			}

			if (Container is not null)
			{
				JsonSerializer.Serialize(writer, Container, options);
				return;
			}

			writer.WriteStartObject();
			writer.WritePropertyName(ContainedVariantName);
			writer.WriteStartObject();
			if (ContainedVariantName == "date_histogram")
			{
				var descriptor = new Aggregations.DateHistogramAggregationDescriptor<T>();
				((Action<Aggregations.DateHistogramAggregationDescriptor<T>>)ContainerVariantDescriptorAction).Invoke(descriptor);
				JsonSerializer.Serialize(writer, descriptor, options);
				Finalise();
				return;
			}

			if (ContainedVariantName == "geotile_grid")
			{
				var descriptor = new Aggregations.GeoTileGridAggregationDescriptor<T>();
				((Action<Aggregations.GeoTileGridAggregationDescriptor<T>>)ContainerVariantDescriptorAction).Invoke(descriptor);
				JsonSerializer.Serialize(writer, descriptor, options);
				Finalise();
				return;
			}

			if (ContainedVariantName == "histogram")
			{
				var descriptor = new Aggregations.HistogramAggregationDescriptor<T>();
				((Action<Aggregations.HistogramAggregationDescriptor<T>>)ContainerVariantDescriptorAction).Invoke(descriptor);
				JsonSerializer.Serialize(writer, descriptor, options);
				Finalise();
				return;
			}

			if (ContainedVariantName == "terms")
			{
				var descriptor = new Aggregations.TermsAggregationDescriptor<T>();
				((Action<Aggregations.TermsAggregationDescriptor<T>>)ContainerVariantDescriptorAction).Invoke(descriptor);
				JsonSerializer.Serialize(writer, descriptor, options);
				Finalise();
				return;
			}

			writer.WriteEndObject();
			writer.WriteEndObject();
			void Finalise()
			{
				writer.WriteEndObject();
				writer.WriteEndObject();
			}
		}
	}
}
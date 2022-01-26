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
namespace Elastic.Clients.Elasticsearch.QueryDsl
{
	public partial class SpanContainingQuery : QueryDsl.QueryBase, IQueryContainerVariant, ISpanQueryVariant
	{
		[JsonIgnore]
		string QueryDsl.IQueryContainerVariant.QueryContainerVariantName => "span_containing";
		[JsonIgnore]
		string QueryDsl.ISpanQueryVariant.SpanQueryVariantName => "span_containing";
		[JsonInclude]
		[JsonPropertyName("big")]
		public Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery Big { get; set; }

		[JsonInclude]
		[JsonPropertyName("little")]
		public Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery Little { get; set; }
	}

	public sealed partial class SpanContainingQueryDescriptor<TDocument> : DescriptorBase<SpanContainingQueryDescriptor<TDocument>>
	{
		public SpanContainingQueryDescriptor()
		{
		}

		internal SpanContainingQueryDescriptor(Action<SpanContainingQueryDescriptor<TDocument>> configure) => configure.Invoke(this);
		internal Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery BigValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery LittleValue { get; private set; }

		internal string? QueryNameValue { get; private set; }

		internal float? BoostValue { get; private set; }

		internal SpanQueryDescriptor<TDocument> BigDescriptor { get; private set; }

		internal SpanQueryDescriptor<TDocument> LittleDescriptor { get; private set; }

		internal Action<SpanQueryDescriptor<TDocument>> BigDescriptorAction { get; private set; }

		internal Action<SpanQueryDescriptor<TDocument>> LittleDescriptorAction { get; private set; }

		public SpanContainingQueryDescriptor<TDocument> Big(Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery big)
		{
			BigDescriptor = null;
			BigDescriptorAction = null;
			return Assign(big, (a, v) => a.BigValue = v);
		}

		public SpanContainingQueryDescriptor<TDocument> Big(QueryDsl.SpanQueryDescriptor<TDocument> descriptor)
		{
			BigValue = null;
			BigDescriptorAction = null;
			return Assign(descriptor, (a, v) => a.BigDescriptor = v);
		}

		public SpanContainingQueryDescriptor<TDocument> Big(Action<QueryDsl.SpanQueryDescriptor<TDocument>> configure)
		{
			BigValue = null;
			BigDescriptorAction = null;
			return Assign(configure, (a, v) => a.BigDescriptorAction = v);
		}

		public SpanContainingQueryDescriptor<TDocument> Little(Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery little)
		{
			LittleDescriptor = null;
			LittleDescriptorAction = null;
			return Assign(little, (a, v) => a.LittleValue = v);
		}

		public SpanContainingQueryDescriptor<TDocument> Little(QueryDsl.SpanQueryDescriptor<TDocument> descriptor)
		{
			LittleValue = null;
			LittleDescriptorAction = null;
			return Assign(descriptor, (a, v) => a.LittleDescriptor = v);
		}

		public SpanContainingQueryDescriptor<TDocument> Little(Action<QueryDsl.SpanQueryDescriptor<TDocument>> configure)
		{
			LittleValue = null;
			LittleDescriptorAction = null;
			return Assign(configure, (a, v) => a.LittleDescriptorAction = v);
		}

		public SpanContainingQueryDescriptor<TDocument> QueryName(string? queryName) => Assign(queryName, (a, v) => a.QueryNameValue = v);
		public SpanContainingQueryDescriptor<TDocument> Boost(float? boost) => Assign(boost, (a, v) => a.BoostValue = v);
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (BigDescriptor is not null)
			{
				writer.WritePropertyName("big");
				JsonSerializer.Serialize(writer, BigDescriptor, options);
			}
			else if (BigDescriptorAction is not null)
			{
				writer.WritePropertyName("big");
				JsonSerializer.Serialize(writer, new QueryDsl.SpanQueryDescriptor<TDocument>(BigDescriptorAction), options);
			}
			else
			{
				writer.WritePropertyName("big");
				JsonSerializer.Serialize(writer, BigValue, options);
			}

			if (LittleDescriptor is not null)
			{
				writer.WritePropertyName("little");
				JsonSerializer.Serialize(writer, LittleDescriptor, options);
			}
			else if (LittleDescriptorAction is not null)
			{
				writer.WritePropertyName("little");
				JsonSerializer.Serialize(writer, new QueryDsl.SpanQueryDescriptor<TDocument>(LittleDescriptorAction), options);
			}
			else
			{
				writer.WritePropertyName("little");
				JsonSerializer.Serialize(writer, LittleValue, options);
			}

			if (!string.IsNullOrEmpty(QueryNameValue))
			{
				writer.WritePropertyName("_name");
				writer.WriteStringValue(QueryNameValue);
			}

			if (BoostValue.HasValue)
			{
				writer.WritePropertyName("boost");
				writer.WriteNumberValue(BoostValue.Value);
			}

			writer.WriteEndObject();
		}
	}
}
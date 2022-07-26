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
	public sealed partial class SpanNearQuery : IQueryVariant, ISpanQueryVariant
	{
		[JsonInclude]
		[JsonPropertyName("_name")]
		public string? QueryName { get; set; }

		[JsonInclude]
		[JsonPropertyName("boost")]
		public float? Boost { get; set; }

		[JsonInclude]
		[JsonPropertyName("clauses")]
		public IEnumerable<Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery> Clauses { get; set; }

		[JsonInclude]
		[JsonPropertyName("in_order")]
		public bool? InOrder { get; set; }

		[JsonInclude]
		[JsonPropertyName("slop")]
		public int? Slop { get; set; }
	}

	public sealed partial class SpanNearQueryDescriptor<TDocument> : SerializableDescriptorBase<SpanNearQueryDescriptor<TDocument>>
	{
		internal SpanNearQueryDescriptor(Action<SpanNearQueryDescriptor<TDocument>> configure) => configure.Invoke(this);
		public SpanNearQueryDescriptor() : base()
		{
		}

		private IEnumerable<Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery> ClausesValue { get; set; }

		private SpanQueryDescriptor<TDocument> ClausesDescriptor { get; set; }

		private Action<SpanQueryDescriptor<TDocument>> ClausesDescriptorAction { get; set; }

		private Action<SpanQueryDescriptor<TDocument>>[] ClausesDescriptorActions { get; set; }

		private string? QueryNameValue { get; set; }

		private float? BoostValue { get; set; }

		private bool? InOrderValue { get; set; }

		private int? SlopValue { get; set; }

		public SpanNearQueryDescriptor<TDocument> Clauses(IEnumerable<Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery> clauses)
		{
			ClausesDescriptor = null;
			ClausesDescriptorAction = null;
			ClausesDescriptorActions = null;
			ClausesValue = clauses;
			return Self;
		}

		public SpanNearQueryDescriptor<TDocument> Clauses(SpanQueryDescriptor<TDocument> descriptor)
		{
			ClausesValue = null;
			ClausesDescriptorAction = null;
			ClausesDescriptorActions = null;
			ClausesDescriptor = descriptor;
			return Self;
		}

		public SpanNearQueryDescriptor<TDocument> Clauses(Action<SpanQueryDescriptor<TDocument>> configure)
		{
			ClausesValue = null;
			ClausesDescriptor = null;
			ClausesDescriptorActions = null;
			ClausesDescriptorAction = configure;
			return Self;
		}

		public SpanNearQueryDescriptor<TDocument> Clauses(params Action<SpanQueryDescriptor<TDocument>>[] configure)
		{
			ClausesValue = null;
			ClausesDescriptor = null;
			ClausesDescriptorAction = null;
			ClausesDescriptorActions = configure;
			return Self;
		}

		public SpanNearQueryDescriptor<TDocument> QueryName(string? queryName)
		{
			QueryNameValue = queryName;
			return Self;
		}

		public SpanNearQueryDescriptor<TDocument> Boost(float? boost)
		{
			BoostValue = boost;
			return Self;
		}

		public SpanNearQueryDescriptor<TDocument> InOrder(bool? inOrder = true)
		{
			InOrderValue = inOrder;
			return Self;
		}

		public SpanNearQueryDescriptor<TDocument> Slop(int? slop)
		{
			SlopValue = slop;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (ClausesDescriptor is not null)
			{
				writer.WritePropertyName("clauses");
				writer.WriteStartArray();
				JsonSerializer.Serialize(writer, ClausesDescriptor, options);
				writer.WriteEndArray();
			}
			else if (ClausesDescriptorAction is not null)
			{
				writer.WritePropertyName("clauses");
				writer.WriteStartArray();
				JsonSerializer.Serialize(writer, new SpanQueryDescriptor<TDocument>(ClausesDescriptorAction), options);
				writer.WriteEndArray();
			}
			else if (ClausesDescriptorActions is not null)
			{
				writer.WritePropertyName("clauses");
				writer.WriteStartArray();
				foreach (var action in ClausesDescriptorActions)
				{
					JsonSerializer.Serialize(writer, new SpanQueryDescriptor<TDocument>(action), options);
				}

				writer.WriteEndArray();
			}
			else
			{
				writer.WritePropertyName("clauses");
				JsonSerializer.Serialize(writer, ClausesValue, options);
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

			if (InOrderValue.HasValue)
			{
				writer.WritePropertyName("in_order");
				writer.WriteBooleanValue(InOrderValue.Value);
			}

			if (SlopValue.HasValue)
			{
				writer.WritePropertyName("slop");
				writer.WriteNumberValue(SlopValue.Value);
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class SpanNearQueryDescriptor : SerializableDescriptorBase<SpanNearQueryDescriptor>
	{
		internal SpanNearQueryDescriptor(Action<SpanNearQueryDescriptor> configure) => configure.Invoke(this);
		public SpanNearQueryDescriptor() : base()
		{
		}

		private IEnumerable<Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery> ClausesValue { get; set; }

		private SpanQueryDescriptor ClausesDescriptor { get; set; }

		private Action<SpanQueryDescriptor> ClausesDescriptorAction { get; set; }

		private Action<SpanQueryDescriptor>[] ClausesDescriptorActions { get; set; }

		private string? QueryNameValue { get; set; }

		private float? BoostValue { get; set; }

		private bool? InOrderValue { get; set; }

		private int? SlopValue { get; set; }

		public SpanNearQueryDescriptor Clauses(IEnumerable<Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery> clauses)
		{
			ClausesDescriptor = null;
			ClausesDescriptorAction = null;
			ClausesDescriptorActions = null;
			ClausesValue = clauses;
			return Self;
		}

		public SpanNearQueryDescriptor Clauses(SpanQueryDescriptor descriptor)
		{
			ClausesValue = null;
			ClausesDescriptorAction = null;
			ClausesDescriptorActions = null;
			ClausesDescriptor = descriptor;
			return Self;
		}

		public SpanNearQueryDescriptor Clauses(Action<SpanQueryDescriptor> configure)
		{
			ClausesValue = null;
			ClausesDescriptor = null;
			ClausesDescriptorActions = null;
			ClausesDescriptorAction = configure;
			return Self;
		}

		public SpanNearQueryDescriptor Clauses(params Action<SpanQueryDescriptor>[] configure)
		{
			ClausesValue = null;
			ClausesDescriptor = null;
			ClausesDescriptorAction = null;
			ClausesDescriptorActions = configure;
			return Self;
		}

		public SpanNearQueryDescriptor QueryName(string? queryName)
		{
			QueryNameValue = queryName;
			return Self;
		}

		public SpanNearQueryDescriptor Boost(float? boost)
		{
			BoostValue = boost;
			return Self;
		}

		public SpanNearQueryDescriptor InOrder(bool? inOrder = true)
		{
			InOrderValue = inOrder;
			return Self;
		}

		public SpanNearQueryDescriptor Slop(int? slop)
		{
			SlopValue = slop;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (ClausesDescriptor is not null)
			{
				writer.WritePropertyName("clauses");
				writer.WriteStartArray();
				JsonSerializer.Serialize(writer, ClausesDescriptor, options);
				writer.WriteEndArray();
			}
			else if (ClausesDescriptorAction is not null)
			{
				writer.WritePropertyName("clauses");
				writer.WriteStartArray();
				JsonSerializer.Serialize(writer, new SpanQueryDescriptor(ClausesDescriptorAction), options);
				writer.WriteEndArray();
			}
			else if (ClausesDescriptorActions is not null)
			{
				writer.WritePropertyName("clauses");
				writer.WriteStartArray();
				foreach (var action in ClausesDescriptorActions)
				{
					JsonSerializer.Serialize(writer, new SpanQueryDescriptor(action), options);
				}

				writer.WriteEndArray();
			}
			else
			{
				writer.WritePropertyName("clauses");
				JsonSerializer.Serialize(writer, ClausesValue, options);
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

			if (InOrderValue.HasValue)
			{
				writer.WritePropertyName("in_order");
				writer.WriteBooleanValue(InOrderValue.Value);
			}

			if (SlopValue.HasValue)
			{
				writer.WritePropertyName("slop");
				writer.WriteNumberValue(SlopValue.Value);
			}

			writer.WriteEndObject();
		}
	}
}
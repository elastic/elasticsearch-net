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

#nullable restore

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Aggregations;

public sealed partial class CompositeTermsAggregation
{
	/// <summary>
	/// <para>
	/// Either <c>field</c> or <c>script</c> must be present
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("field")]
	public Elastic.Clients.Elasticsearch.Field? Field { get; set; }
	[JsonInclude, JsonPropertyName("missing_bucket")]
	public bool? MissingBucket { get; set; }
	[JsonInclude, JsonPropertyName("missing_order")]
	public Elastic.Clients.Elasticsearch.Aggregations.MissingOrder? MissingOrder { get; set; }
	[JsonInclude, JsonPropertyName("order")]
	public Elastic.Clients.Elasticsearch.SortOrder? Order { get; set; }

	/// <summary>
	/// <para>
	/// Either <c>field</c> or <c>script</c> must be present
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("script")]
	public Elastic.Clients.Elasticsearch.Script? Script { get; set; }
	[JsonInclude, JsonPropertyName("value_type")]
	public Elastic.Clients.Elasticsearch.Aggregations.ValueType? ValueType { get; set; }
}

public sealed partial class CompositeTermsAggregationDescriptor<TDocument> : SerializableDescriptor<CompositeTermsAggregationDescriptor<TDocument>>
{
	internal CompositeTermsAggregationDescriptor(Action<CompositeTermsAggregationDescriptor<TDocument>> configure) => configure.Invoke(this);

	public CompositeTermsAggregationDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }
	private bool? MissingBucketValue { get; set; }
	private Elastic.Clients.Elasticsearch.Aggregations.MissingOrder? MissingOrderValue { get; set; }
	private Elastic.Clients.Elasticsearch.SortOrder? OrderValue { get; set; }
	private Elastic.Clients.Elasticsearch.Script? ScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.ScriptDescriptor ScriptDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.ScriptDescriptor> ScriptDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Aggregations.ValueType? ValueTypeValue { get; set; }

	/// <summary>
	/// <para>
	/// Either <c>field</c> or <c>script</c> must be present
	/// </para>
	/// </summary>
	public CompositeTermsAggregationDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field? field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Either <c>field</c> or <c>script</c> must be present
	/// </para>
	/// </summary>
	public CompositeTermsAggregationDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Either <c>field</c> or <c>script</c> must be present
	/// </para>
	/// </summary>
	public CompositeTermsAggregationDescriptor<TDocument> Field(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	public CompositeTermsAggregationDescriptor<TDocument> MissingBucket(bool? missingBucket = true)
	{
		MissingBucketValue = missingBucket;
		return Self;
	}

	public CompositeTermsAggregationDescriptor<TDocument> MissingOrder(Elastic.Clients.Elasticsearch.Aggregations.MissingOrder? missingOrder)
	{
		MissingOrderValue = missingOrder;
		return Self;
	}

	public CompositeTermsAggregationDescriptor<TDocument> Order(Elastic.Clients.Elasticsearch.SortOrder? order)
	{
		OrderValue = order;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Either <c>field</c> or <c>script</c> must be present
	/// </para>
	/// </summary>
	public CompositeTermsAggregationDescriptor<TDocument> Script(Elastic.Clients.Elasticsearch.Script? script)
	{
		ScriptDescriptor = null;
		ScriptDescriptorAction = null;
		ScriptValue = script;
		return Self;
	}

	public CompositeTermsAggregationDescriptor<TDocument> Script(Elastic.Clients.Elasticsearch.ScriptDescriptor descriptor)
	{
		ScriptValue = null;
		ScriptDescriptorAction = null;
		ScriptDescriptor = descriptor;
		return Self;
	}

	public CompositeTermsAggregationDescriptor<TDocument> Script(Action<Elastic.Clients.Elasticsearch.ScriptDescriptor> configure)
	{
		ScriptValue = null;
		ScriptDescriptor = null;
		ScriptDescriptorAction = configure;
		return Self;
	}

	public CompositeTermsAggregationDescriptor<TDocument> ValueType(Elastic.Clients.Elasticsearch.Aggregations.ValueType? valueType)
	{
		ValueTypeValue = valueType;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (FieldValue is not null)
		{
			writer.WritePropertyName("field");
			JsonSerializer.Serialize(writer, FieldValue, options);
		}

		if (MissingBucketValue.HasValue)
		{
			writer.WritePropertyName("missing_bucket");
			writer.WriteBooleanValue(MissingBucketValue.Value);
		}

		if (MissingOrderValue is not null)
		{
			writer.WritePropertyName("missing_order");
			JsonSerializer.Serialize(writer, MissingOrderValue, options);
		}

		if (OrderValue is not null)
		{
			writer.WritePropertyName("order");
			JsonSerializer.Serialize(writer, OrderValue, options);
		}

		if (ScriptDescriptor is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, ScriptDescriptor, options);
		}
		else if (ScriptDescriptorAction is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.ScriptDescriptor(ScriptDescriptorAction), options);
		}
		else if (ScriptValue is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, ScriptValue, options);
		}

		if (ValueTypeValue is not null)
		{
			writer.WritePropertyName("value_type");
			JsonSerializer.Serialize(writer, ValueTypeValue, options);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class CompositeTermsAggregationDescriptor : SerializableDescriptor<CompositeTermsAggregationDescriptor>
{
	internal CompositeTermsAggregationDescriptor(Action<CompositeTermsAggregationDescriptor> configure) => configure.Invoke(this);

	public CompositeTermsAggregationDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }
	private bool? MissingBucketValue { get; set; }
	private Elastic.Clients.Elasticsearch.Aggregations.MissingOrder? MissingOrderValue { get; set; }
	private Elastic.Clients.Elasticsearch.SortOrder? OrderValue { get; set; }
	private Elastic.Clients.Elasticsearch.Script? ScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.ScriptDescriptor ScriptDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.ScriptDescriptor> ScriptDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Aggregations.ValueType? ValueTypeValue { get; set; }

	/// <summary>
	/// <para>
	/// Either <c>field</c> or <c>script</c> must be present
	/// </para>
	/// </summary>
	public CompositeTermsAggregationDescriptor Field(Elastic.Clients.Elasticsearch.Field? field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Either <c>field</c> or <c>script</c> must be present
	/// </para>
	/// </summary>
	public CompositeTermsAggregationDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Either <c>field</c> or <c>script</c> must be present
	/// </para>
	/// </summary>
	public CompositeTermsAggregationDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	public CompositeTermsAggregationDescriptor MissingBucket(bool? missingBucket = true)
	{
		MissingBucketValue = missingBucket;
		return Self;
	}

	public CompositeTermsAggregationDescriptor MissingOrder(Elastic.Clients.Elasticsearch.Aggregations.MissingOrder? missingOrder)
	{
		MissingOrderValue = missingOrder;
		return Self;
	}

	public CompositeTermsAggregationDescriptor Order(Elastic.Clients.Elasticsearch.SortOrder? order)
	{
		OrderValue = order;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Either <c>field</c> or <c>script</c> must be present
	/// </para>
	/// </summary>
	public CompositeTermsAggregationDescriptor Script(Elastic.Clients.Elasticsearch.Script? script)
	{
		ScriptDescriptor = null;
		ScriptDescriptorAction = null;
		ScriptValue = script;
		return Self;
	}

	public CompositeTermsAggregationDescriptor Script(Elastic.Clients.Elasticsearch.ScriptDescriptor descriptor)
	{
		ScriptValue = null;
		ScriptDescriptorAction = null;
		ScriptDescriptor = descriptor;
		return Self;
	}

	public CompositeTermsAggregationDescriptor Script(Action<Elastic.Clients.Elasticsearch.ScriptDescriptor> configure)
	{
		ScriptValue = null;
		ScriptDescriptor = null;
		ScriptDescriptorAction = configure;
		return Self;
	}

	public CompositeTermsAggregationDescriptor ValueType(Elastic.Clients.Elasticsearch.Aggregations.ValueType? valueType)
	{
		ValueTypeValue = valueType;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (FieldValue is not null)
		{
			writer.WritePropertyName("field");
			JsonSerializer.Serialize(writer, FieldValue, options);
		}

		if (MissingBucketValue.HasValue)
		{
			writer.WritePropertyName("missing_bucket");
			writer.WriteBooleanValue(MissingBucketValue.Value);
		}

		if (MissingOrderValue is not null)
		{
			writer.WritePropertyName("missing_order");
			JsonSerializer.Serialize(writer, MissingOrderValue, options);
		}

		if (OrderValue is not null)
		{
			writer.WritePropertyName("order");
			JsonSerializer.Serialize(writer, OrderValue, options);
		}

		if (ScriptDescriptor is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, ScriptDescriptor, options);
		}
		else if (ScriptDescriptorAction is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.ScriptDescriptor(ScriptDescriptorAction), options);
		}
		else if (ScriptValue is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, ScriptValue, options);
		}

		if (ValueTypeValue is not null)
		{
			writer.WritePropertyName("value_type");
			JsonSerializer.Serialize(writer, ValueTypeValue, options);
		}

		writer.WriteEndObject();
	}
}
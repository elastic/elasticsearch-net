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

public sealed partial class TopMetricsAggregation
{
	/// <summary>
	/// <para>The field on which to run the aggregation.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("field")]
	public Elastic.Clients.Elasticsearch.Field? Field { get; set; }

	/// <summary>
	/// <para>The fields of the top document to return.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("metrics")]
	[SingleOrManyCollectionConverter(typeof(Elastic.Clients.Elasticsearch.Aggregations.TopMetricsValue))]
	public ICollection<Elastic.Clients.Elasticsearch.Aggregations.TopMetricsValue>? Metrics { get; set; }

	/// <summary>
	/// <para>The value to apply to documents that do not have a value.<br/>By default, documents without a value are ignored.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("missing")]
	public Elastic.Clients.Elasticsearch.FieldValue? Missing { get; set; }
	[JsonInclude, JsonPropertyName("script")]
	public Elastic.Clients.Elasticsearch.Script? Script { get; set; }

	/// <summary>
	/// <para>The number of top documents from which to return metrics.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("size")]
	public int? Size { get; set; }

	/// <summary>
	/// <para>The sort order of the documents.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("sort")]
	[SingleOrManyCollectionConverter(typeof(Elastic.Clients.Elasticsearch.SortOptions))]
	public ICollection<Elastic.Clients.Elasticsearch.SortOptions>? Sort { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.Aggregation(TopMetricsAggregation topMetricsAggregation) => Elastic.Clients.Elasticsearch.Aggregations.Aggregation.TopMetrics(topMetricsAggregation);
}

public sealed partial class TopMetricsAggregationDescriptor<TDocument> : SerializableDescriptor<TopMetricsAggregationDescriptor<TDocument>>
{
	internal TopMetricsAggregationDescriptor(Action<TopMetricsAggregationDescriptor<TDocument>> configure) => configure.Invoke(this);

	public TopMetricsAggregationDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Aggregations.TopMetricsValue>? MetricsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Aggregations.TopMetricsValueDescriptor<TDocument> MetricsDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Aggregations.TopMetricsValueDescriptor<TDocument>> MetricsDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Aggregations.TopMetricsValueDescriptor<TDocument>>[] MetricsDescriptorActions { get; set; }
	private Elastic.Clients.Elasticsearch.FieldValue? MissingValue { get; set; }
	private Elastic.Clients.Elasticsearch.Script? ScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.ScriptDescriptor ScriptDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.ScriptDescriptor> ScriptDescriptorAction { get; set; }
	private int? SizeValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.SortOptions>? SortValue { get; set; }
	private Elastic.Clients.Elasticsearch.SortOptionsDescriptor<TDocument> SortDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor<TDocument>> SortDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor<TDocument>>[] SortDescriptorActions { get; set; }

	/// <summary>
	/// <para>The field on which to run the aggregation.</para>
	/// </summary>
	public TopMetricsAggregationDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field? field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The field on which to run the aggregation.</para>
	/// </summary>
	public TopMetricsAggregationDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The field on which to run the aggregation.</para>
	/// </summary>
	public TopMetricsAggregationDescriptor<TDocument> Field(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The fields of the top document to return.</para>
	/// </summary>
	public TopMetricsAggregationDescriptor<TDocument> Metrics(ICollection<Elastic.Clients.Elasticsearch.Aggregations.TopMetricsValue>? metrics)
	{
		MetricsDescriptor = null;
		MetricsDescriptorAction = null;
		MetricsDescriptorActions = null;
		MetricsValue = metrics;
		return Self;
	}

	public TopMetricsAggregationDescriptor<TDocument> Metrics(Elastic.Clients.Elasticsearch.Aggregations.TopMetricsValueDescriptor<TDocument> descriptor)
	{
		MetricsValue = null;
		MetricsDescriptorAction = null;
		MetricsDescriptorActions = null;
		MetricsDescriptor = descriptor;
		return Self;
	}

	public TopMetricsAggregationDescriptor<TDocument> Metrics(Action<Elastic.Clients.Elasticsearch.Aggregations.TopMetricsValueDescriptor<TDocument>> configure)
	{
		MetricsValue = null;
		MetricsDescriptor = null;
		MetricsDescriptorActions = null;
		MetricsDescriptorAction = configure;
		return Self;
	}

	public TopMetricsAggregationDescriptor<TDocument> Metrics(params Action<Elastic.Clients.Elasticsearch.Aggregations.TopMetricsValueDescriptor<TDocument>>[] configure)
	{
		MetricsValue = null;
		MetricsDescriptor = null;
		MetricsDescriptorAction = null;
		MetricsDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>The value to apply to documents that do not have a value.<br/>By default, documents without a value are ignored.</para>
	/// </summary>
	public TopMetricsAggregationDescriptor<TDocument> Missing(Elastic.Clients.Elasticsearch.FieldValue? missing)
	{
		MissingValue = missing;
		return Self;
	}

	public TopMetricsAggregationDescriptor<TDocument> Script(Elastic.Clients.Elasticsearch.Script? script)
	{
		ScriptDescriptor = null;
		ScriptDescriptorAction = null;
		ScriptValue = script;
		return Self;
	}

	public TopMetricsAggregationDescriptor<TDocument> Script(Elastic.Clients.Elasticsearch.ScriptDescriptor descriptor)
	{
		ScriptValue = null;
		ScriptDescriptorAction = null;
		ScriptDescriptor = descriptor;
		return Self;
	}

	public TopMetricsAggregationDescriptor<TDocument> Script(Action<Elastic.Clients.Elasticsearch.ScriptDescriptor> configure)
	{
		ScriptValue = null;
		ScriptDescriptor = null;
		ScriptDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>The number of top documents from which to return metrics.</para>
	/// </summary>
	public TopMetricsAggregationDescriptor<TDocument> Size(int? size)
	{
		SizeValue = size;
		return Self;
	}

	/// <summary>
	/// <para>The sort order of the documents.</para>
	/// </summary>
	public TopMetricsAggregationDescriptor<TDocument> Sort(ICollection<Elastic.Clients.Elasticsearch.SortOptions>? sort)
	{
		SortDescriptor = null;
		SortDescriptorAction = null;
		SortDescriptorActions = null;
		SortValue = sort;
		return Self;
	}

	public TopMetricsAggregationDescriptor<TDocument> Sort(Elastic.Clients.Elasticsearch.SortOptionsDescriptor<TDocument> descriptor)
	{
		SortValue = null;
		SortDescriptorAction = null;
		SortDescriptorActions = null;
		SortDescriptor = descriptor;
		return Self;
	}

	public TopMetricsAggregationDescriptor<TDocument> Sort(Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor<TDocument>> configure)
	{
		SortValue = null;
		SortDescriptor = null;
		SortDescriptorActions = null;
		SortDescriptorAction = configure;
		return Self;
	}

	public TopMetricsAggregationDescriptor<TDocument> Sort(params Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor<TDocument>>[] configure)
	{
		SortValue = null;
		SortDescriptor = null;
		SortDescriptorAction = null;
		SortDescriptorActions = configure;
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

		if (MetricsDescriptor is not null)
		{
			writer.WritePropertyName("metrics");
			JsonSerializer.Serialize(writer, MetricsDescriptor, options);
		}
		else if (MetricsDescriptorAction is not null)
		{
			writer.WritePropertyName("metrics");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Aggregations.TopMetricsValueDescriptor<TDocument>(MetricsDescriptorAction), options);
		}
		else if (MetricsDescriptorActions is not null)
		{
			writer.WritePropertyName("metrics");
			if (MetricsDescriptorActions.Length != 1)
				writer.WriteStartArray();
			foreach (var action in MetricsDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Aggregations.TopMetricsValueDescriptor<TDocument>(action), options);
			}

			if (MetricsDescriptorActions.Length != 1)
				writer.WriteEndArray();
		}
		else if (MetricsValue is not null)
		{
			writer.WritePropertyName("metrics");
			SingleOrManySerializationHelper.Serialize<Elastic.Clients.Elasticsearch.Aggregations.TopMetricsValue>(MetricsValue, writer, options);
		}

		if (MissingValue is not null)
		{
			writer.WritePropertyName("missing");
			JsonSerializer.Serialize(writer, MissingValue, options);
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
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.SortOptionsDescriptor<TDocument>(SortDescriptorAction), options);
		}
		else if (SortDescriptorActions is not null)
		{
			writer.WritePropertyName("sort");
			if (SortDescriptorActions.Length != 1)
				writer.WriteStartArray();
			foreach (var action in SortDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.SortOptionsDescriptor<TDocument>(action), options);
			}

			if (SortDescriptorActions.Length != 1)
				writer.WriteEndArray();
		}
		else if (SortValue is not null)
		{
			writer.WritePropertyName("sort");
			SingleOrManySerializationHelper.Serialize<Elastic.Clients.Elasticsearch.SortOptions>(SortValue, writer, options);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class TopMetricsAggregationDescriptor : SerializableDescriptor<TopMetricsAggregationDescriptor>
{
	internal TopMetricsAggregationDescriptor(Action<TopMetricsAggregationDescriptor> configure) => configure.Invoke(this);

	public TopMetricsAggregationDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Aggregations.TopMetricsValue>? MetricsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Aggregations.TopMetricsValueDescriptor MetricsDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Aggregations.TopMetricsValueDescriptor> MetricsDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Aggregations.TopMetricsValueDescriptor>[] MetricsDescriptorActions { get; set; }
	private Elastic.Clients.Elasticsearch.FieldValue? MissingValue { get; set; }
	private Elastic.Clients.Elasticsearch.Script? ScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.ScriptDescriptor ScriptDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.ScriptDescriptor> ScriptDescriptorAction { get; set; }
	private int? SizeValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.SortOptions>? SortValue { get; set; }
	private Elastic.Clients.Elasticsearch.SortOptionsDescriptor SortDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor> SortDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor>[] SortDescriptorActions { get; set; }

	/// <summary>
	/// <para>The field on which to run the aggregation.</para>
	/// </summary>
	public TopMetricsAggregationDescriptor Field(Elastic.Clients.Elasticsearch.Field? field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The field on which to run the aggregation.</para>
	/// </summary>
	public TopMetricsAggregationDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The field on which to run the aggregation.</para>
	/// </summary>
	public TopMetricsAggregationDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The fields of the top document to return.</para>
	/// </summary>
	public TopMetricsAggregationDescriptor Metrics(ICollection<Elastic.Clients.Elasticsearch.Aggregations.TopMetricsValue>? metrics)
	{
		MetricsDescriptor = null;
		MetricsDescriptorAction = null;
		MetricsDescriptorActions = null;
		MetricsValue = metrics;
		return Self;
	}

	public TopMetricsAggregationDescriptor Metrics(Elastic.Clients.Elasticsearch.Aggregations.TopMetricsValueDescriptor descriptor)
	{
		MetricsValue = null;
		MetricsDescriptorAction = null;
		MetricsDescriptorActions = null;
		MetricsDescriptor = descriptor;
		return Self;
	}

	public TopMetricsAggregationDescriptor Metrics(Action<Elastic.Clients.Elasticsearch.Aggregations.TopMetricsValueDescriptor> configure)
	{
		MetricsValue = null;
		MetricsDescriptor = null;
		MetricsDescriptorActions = null;
		MetricsDescriptorAction = configure;
		return Self;
	}

	public TopMetricsAggregationDescriptor Metrics(params Action<Elastic.Clients.Elasticsearch.Aggregations.TopMetricsValueDescriptor>[] configure)
	{
		MetricsValue = null;
		MetricsDescriptor = null;
		MetricsDescriptorAction = null;
		MetricsDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>The value to apply to documents that do not have a value.<br/>By default, documents without a value are ignored.</para>
	/// </summary>
	public TopMetricsAggregationDescriptor Missing(Elastic.Clients.Elasticsearch.FieldValue? missing)
	{
		MissingValue = missing;
		return Self;
	}

	public TopMetricsAggregationDescriptor Script(Elastic.Clients.Elasticsearch.Script? script)
	{
		ScriptDescriptor = null;
		ScriptDescriptorAction = null;
		ScriptValue = script;
		return Self;
	}

	public TopMetricsAggregationDescriptor Script(Elastic.Clients.Elasticsearch.ScriptDescriptor descriptor)
	{
		ScriptValue = null;
		ScriptDescriptorAction = null;
		ScriptDescriptor = descriptor;
		return Self;
	}

	public TopMetricsAggregationDescriptor Script(Action<Elastic.Clients.Elasticsearch.ScriptDescriptor> configure)
	{
		ScriptValue = null;
		ScriptDescriptor = null;
		ScriptDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>The number of top documents from which to return metrics.</para>
	/// </summary>
	public TopMetricsAggregationDescriptor Size(int? size)
	{
		SizeValue = size;
		return Self;
	}

	/// <summary>
	/// <para>The sort order of the documents.</para>
	/// </summary>
	public TopMetricsAggregationDescriptor Sort(ICollection<Elastic.Clients.Elasticsearch.SortOptions>? sort)
	{
		SortDescriptor = null;
		SortDescriptorAction = null;
		SortDescriptorActions = null;
		SortValue = sort;
		return Self;
	}

	public TopMetricsAggregationDescriptor Sort(Elastic.Clients.Elasticsearch.SortOptionsDescriptor descriptor)
	{
		SortValue = null;
		SortDescriptorAction = null;
		SortDescriptorActions = null;
		SortDescriptor = descriptor;
		return Self;
	}

	public TopMetricsAggregationDescriptor Sort(Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor> configure)
	{
		SortValue = null;
		SortDescriptor = null;
		SortDescriptorActions = null;
		SortDescriptorAction = configure;
		return Self;
	}

	public TopMetricsAggregationDescriptor Sort(params Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor>[] configure)
	{
		SortValue = null;
		SortDescriptor = null;
		SortDescriptorAction = null;
		SortDescriptorActions = configure;
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

		if (MetricsDescriptor is not null)
		{
			writer.WritePropertyName("metrics");
			JsonSerializer.Serialize(writer, MetricsDescriptor, options);
		}
		else if (MetricsDescriptorAction is not null)
		{
			writer.WritePropertyName("metrics");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Aggregations.TopMetricsValueDescriptor(MetricsDescriptorAction), options);
		}
		else if (MetricsDescriptorActions is not null)
		{
			writer.WritePropertyName("metrics");
			if (MetricsDescriptorActions.Length != 1)
				writer.WriteStartArray();
			foreach (var action in MetricsDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Aggregations.TopMetricsValueDescriptor(action), options);
			}

			if (MetricsDescriptorActions.Length != 1)
				writer.WriteEndArray();
		}
		else if (MetricsValue is not null)
		{
			writer.WritePropertyName("metrics");
			SingleOrManySerializationHelper.Serialize<Elastic.Clients.Elasticsearch.Aggregations.TopMetricsValue>(MetricsValue, writer, options);
		}

		if (MissingValue is not null)
		{
			writer.WritePropertyName("missing");
			JsonSerializer.Serialize(writer, MissingValue, options);
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
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.SortOptionsDescriptor(SortDescriptorAction), options);
		}
		else if (SortDescriptorActions is not null)
		{
			writer.WritePropertyName("sort");
			if (SortDescriptorActions.Length != 1)
				writer.WriteStartArray();
			foreach (var action in SortDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.SortOptionsDescriptor(action), options);
			}

			if (SortDescriptorActions.Length != 1)
				writer.WriteEndArray();
		}
		else if (SortValue is not null)
		{
			writer.WritePropertyName("sort");
			SingleOrManySerializationHelper.Serialize<Elastic.Clients.Elasticsearch.SortOptions>(SortValue, writer, options);
		}

		writer.WriteEndObject();
	}
}
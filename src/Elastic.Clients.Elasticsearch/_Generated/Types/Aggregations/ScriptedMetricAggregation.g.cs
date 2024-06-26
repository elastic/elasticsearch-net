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

public sealed partial class ScriptedMetricAggregation
{
	/// <summary>
	/// <para>Runs once on each shard after document collection is complete.<br/>Allows the aggregation to consolidate the state returned from each shard.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("combine_script")]
	public Elastic.Clients.Elasticsearch.Script? CombineScript { get; set; }

	/// <summary>
	/// <para>The field on which to run the aggregation.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("field")]
	public Elastic.Clients.Elasticsearch.Field? Field { get; set; }

	/// <summary>
	/// <para>Runs prior to any collection of documents.<br/>Allows the aggregation to set up any initial state.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("init_script")]
	public Elastic.Clients.Elasticsearch.Script? InitScript { get; set; }

	/// <summary>
	/// <para>Run once per document collected.<br/>If no `combine_script` is specified, the resulting state needs to be stored in the `state` object.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("map_script")]
	public Elastic.Clients.Elasticsearch.Script? MapScript { get; set; }

	/// <summary>
	/// <para>The value to apply to documents that do not have a value.<br/>By default, documents without a value are ignored.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("missing")]
	public Elastic.Clients.Elasticsearch.FieldValue? Missing { get; set; }

	/// <summary>
	/// <para>A global object with script parameters for `init`, `map` and `combine` scripts.<br/>It is shared between the scripts.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("params")]
	public IDictionary<string, object>? Params { get; set; }

	/// <summary>
	/// <para>Runs once on the coordinating node after all shards have returned their results.<br/>The script is provided with access to a variable `states`, which is an array of the result of the `combine_script` on each shard.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("reduce_script")]
	public Elastic.Clients.Elasticsearch.Script? ReduceScript { get; set; }
	[JsonInclude, JsonPropertyName("script")]
	public Elastic.Clients.Elasticsearch.Script? Script { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.Aggregation(ScriptedMetricAggregation scriptedMetricAggregation) => Elastic.Clients.Elasticsearch.Aggregations.Aggregation.ScriptedMetric(scriptedMetricAggregation);
}

public sealed partial class ScriptedMetricAggregationDescriptor<TDocument> : SerializableDescriptor<ScriptedMetricAggregationDescriptor<TDocument>>
{
	internal ScriptedMetricAggregationDescriptor(Action<ScriptedMetricAggregationDescriptor<TDocument>> configure) => configure.Invoke(this);

	public ScriptedMetricAggregationDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Script? CombineScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.Script? InitScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.Script? MapScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.FieldValue? MissingValue { get; set; }
	private IDictionary<string, object>? ParamsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Script? ReduceScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.Script? ScriptValue { get; set; }

	/// <summary>
	/// <para>Runs once on each shard after document collection is complete.<br/>Allows the aggregation to consolidate the state returned from each shard.</para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor<TDocument> CombineScript(Elastic.Clients.Elasticsearch.Script? combineScript)
	{
		CombineScriptValue = combineScript;
		return Self;
	}

	/// <summary>
	/// <para>The field on which to run the aggregation.</para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field? field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The field on which to run the aggregation.</para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The field on which to run the aggregation.</para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor<TDocument> Field(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>Runs prior to any collection of documents.<br/>Allows the aggregation to set up any initial state.</para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor<TDocument> InitScript(Elastic.Clients.Elasticsearch.Script? initScript)
	{
		InitScriptValue = initScript;
		return Self;
	}

	/// <summary>
	/// <para>Run once per document collected.<br/>If no `combine_script` is specified, the resulting state needs to be stored in the `state` object.</para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor<TDocument> MapScript(Elastic.Clients.Elasticsearch.Script? mapScript)
	{
		MapScriptValue = mapScript;
		return Self;
	}

	/// <summary>
	/// <para>The value to apply to documents that do not have a value.<br/>By default, documents without a value are ignored.</para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor<TDocument> Missing(Elastic.Clients.Elasticsearch.FieldValue? missing)
	{
		MissingValue = missing;
		return Self;
	}

	/// <summary>
	/// <para>A global object with script parameters for `init`, `map` and `combine` scripts.<br/>It is shared between the scripts.</para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor<TDocument> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		ParamsValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	/// <summary>
	/// <para>Runs once on the coordinating node after all shards have returned their results.<br/>The script is provided with access to a variable `states`, which is an array of the result of the `combine_script` on each shard.</para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor<TDocument> ReduceScript(Elastic.Clients.Elasticsearch.Script? reduceScript)
	{
		ReduceScriptValue = reduceScript;
		return Self;
	}

	public ScriptedMetricAggregationDescriptor<TDocument> Script(Elastic.Clients.Elasticsearch.Script? script)
	{
		ScriptValue = script;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (CombineScriptValue is not null)
		{
			writer.WritePropertyName("combine_script");
			JsonSerializer.Serialize(writer, CombineScriptValue, options);
		}

		if (FieldValue is not null)
		{
			writer.WritePropertyName("field");
			JsonSerializer.Serialize(writer, FieldValue, options);
		}

		if (InitScriptValue is not null)
		{
			writer.WritePropertyName("init_script");
			JsonSerializer.Serialize(writer, InitScriptValue, options);
		}

		if (MapScriptValue is not null)
		{
			writer.WritePropertyName("map_script");
			JsonSerializer.Serialize(writer, MapScriptValue, options);
		}

		if (MissingValue is not null)
		{
			writer.WritePropertyName("missing");
			JsonSerializer.Serialize(writer, MissingValue, options);
		}

		if (ParamsValue is not null)
		{
			writer.WritePropertyName("params");
			JsonSerializer.Serialize(writer, ParamsValue, options);
		}

		if (ReduceScriptValue is not null)
		{
			writer.WritePropertyName("reduce_script");
			JsonSerializer.Serialize(writer, ReduceScriptValue, options);
		}

		if (ScriptValue is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, ScriptValue, options);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class ScriptedMetricAggregationDescriptor : SerializableDescriptor<ScriptedMetricAggregationDescriptor>
{
	internal ScriptedMetricAggregationDescriptor(Action<ScriptedMetricAggregationDescriptor> configure) => configure.Invoke(this);

	public ScriptedMetricAggregationDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Script? CombineScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.Script? InitScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.Script? MapScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.FieldValue? MissingValue { get; set; }
	private IDictionary<string, object>? ParamsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Script? ReduceScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.Script? ScriptValue { get; set; }

	/// <summary>
	/// <para>Runs once on each shard after document collection is complete.<br/>Allows the aggregation to consolidate the state returned from each shard.</para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor CombineScript(Elastic.Clients.Elasticsearch.Script? combineScript)
	{
		CombineScriptValue = combineScript;
		return Self;
	}

	/// <summary>
	/// <para>The field on which to run the aggregation.</para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor Field(Elastic.Clients.Elasticsearch.Field? field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The field on which to run the aggregation.</para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The field on which to run the aggregation.</para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>Runs prior to any collection of documents.<br/>Allows the aggregation to set up any initial state.</para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor InitScript(Elastic.Clients.Elasticsearch.Script? initScript)
	{
		InitScriptValue = initScript;
		return Self;
	}

	/// <summary>
	/// <para>Run once per document collected.<br/>If no `combine_script` is specified, the resulting state needs to be stored in the `state` object.</para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor MapScript(Elastic.Clients.Elasticsearch.Script? mapScript)
	{
		MapScriptValue = mapScript;
		return Self;
	}

	/// <summary>
	/// <para>The value to apply to documents that do not have a value.<br/>By default, documents without a value are ignored.</para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor Missing(Elastic.Clients.Elasticsearch.FieldValue? missing)
	{
		MissingValue = missing;
		return Self;
	}

	/// <summary>
	/// <para>A global object with script parameters for `init`, `map` and `combine` scripts.<br/>It is shared between the scripts.</para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		ParamsValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	/// <summary>
	/// <para>Runs once on the coordinating node after all shards have returned their results.<br/>The script is provided with access to a variable `states`, which is an array of the result of the `combine_script` on each shard.</para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor ReduceScript(Elastic.Clients.Elasticsearch.Script? reduceScript)
	{
		ReduceScriptValue = reduceScript;
		return Self;
	}

	public ScriptedMetricAggregationDescriptor Script(Elastic.Clients.Elasticsearch.Script? script)
	{
		ScriptValue = script;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (CombineScriptValue is not null)
		{
			writer.WritePropertyName("combine_script");
			JsonSerializer.Serialize(writer, CombineScriptValue, options);
		}

		if (FieldValue is not null)
		{
			writer.WritePropertyName("field");
			JsonSerializer.Serialize(writer, FieldValue, options);
		}

		if (InitScriptValue is not null)
		{
			writer.WritePropertyName("init_script");
			JsonSerializer.Serialize(writer, InitScriptValue, options);
		}

		if (MapScriptValue is not null)
		{
			writer.WritePropertyName("map_script");
			JsonSerializer.Serialize(writer, MapScriptValue, options);
		}

		if (MissingValue is not null)
		{
			writer.WritePropertyName("missing");
			JsonSerializer.Serialize(writer, MissingValue, options);
		}

		if (ParamsValue is not null)
		{
			writer.WritePropertyName("params");
			JsonSerializer.Serialize(writer, ParamsValue, options);
		}

		if (ReduceScriptValue is not null)
		{
			writer.WritePropertyName("reduce_script");
			JsonSerializer.Serialize(writer, ReduceScriptValue, options);
		}

		if (ScriptValue is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, ScriptValue, options);
		}

		writer.WriteEndObject();
	}
}
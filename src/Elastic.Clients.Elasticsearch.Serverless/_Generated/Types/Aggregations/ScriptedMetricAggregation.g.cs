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

using Elastic.Clients.Elasticsearch.Serverless.Fluent;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.Aggregations;

public sealed partial class ScriptedMetricAggregation
{
	/// <summary>
	/// <para>
	/// Runs once on each shard after document collection is complete.
	/// Allows the aggregation to consolidate the state returned from each shard.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("combine_script")]
	public Elastic.Clients.Elasticsearch.Serverless.Script? CombineScript { get; set; }

	/// <summary>
	/// <para>
	/// The field on which to run the aggregation.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("field")]
	public Elastic.Clients.Elasticsearch.Serverless.Field? Field { get; set; }

	/// <summary>
	/// <para>
	/// Runs prior to any collection of documents.
	/// Allows the aggregation to set up any initial state.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("init_script")]
	public Elastic.Clients.Elasticsearch.Serverless.Script? InitScript { get; set; }

	/// <summary>
	/// <para>
	/// Run once per document collected.
	/// If no <c>combine_script</c> is specified, the resulting state needs to be stored in the <c>state</c> object.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("map_script")]
	public Elastic.Clients.Elasticsearch.Serverless.Script? MapScript { get; set; }

	/// <summary>
	/// <para>
	/// The value to apply to documents that do not have a value.
	/// By default, documents without a value are ignored.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("missing")]
	public Elastic.Clients.Elasticsearch.Serverless.FieldValue? Missing { get; set; }

	/// <summary>
	/// <para>
	/// A global object with script parameters for <c>init</c>, <c>map</c> and <c>combine</c> scripts.
	/// It is shared between the scripts.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("params")]
	public IDictionary<string, object>? Params { get; set; }

	/// <summary>
	/// <para>
	/// Runs once on the coordinating node after all shards have returned their results.
	/// The script is provided with access to a variable <c>states</c>, which is an array of the result of the <c>combine_script</c> on each shard.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("reduce_script")]
	public Elastic.Clients.Elasticsearch.Serverless.Script? ReduceScript { get; set; }
	[JsonInclude, JsonPropertyName("script")]
	public Elastic.Clients.Elasticsearch.Serverless.Script? Script { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Serverless.Aggregations.Aggregation(ScriptedMetricAggregation scriptedMetricAggregation) => Elastic.Clients.Elasticsearch.Serverless.Aggregations.Aggregation.ScriptedMetric(scriptedMetricAggregation);
}

public sealed partial class ScriptedMetricAggregationDescriptor<TDocument> : SerializableDescriptor<ScriptedMetricAggregationDescriptor<TDocument>>
{
	internal ScriptedMetricAggregationDescriptor(Action<ScriptedMetricAggregationDescriptor<TDocument>> configure) => configure.Invoke(this);

	public ScriptedMetricAggregationDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Serverless.Script? CombineScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor CombineScriptDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor> CombineScriptDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Field? FieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Script? InitScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor InitScriptDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor> InitScriptDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Script? MapScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor MapScriptDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor> MapScriptDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.FieldValue? MissingValue { get; set; }
	private IDictionary<string, object>? ParamsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Script? ReduceScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor ReduceScriptDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor> ReduceScriptDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Script? ScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor ScriptDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor> ScriptDescriptorAction { get; set; }

	/// <summary>
	/// <para>
	/// Runs once on each shard after document collection is complete.
	/// Allows the aggregation to consolidate the state returned from each shard.
	/// </para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor<TDocument> CombineScript(Elastic.Clients.Elasticsearch.Serverless.Script? combineScript)
	{
		CombineScriptDescriptor = null;
		CombineScriptDescriptorAction = null;
		CombineScriptValue = combineScript;
		return Self;
	}

	public ScriptedMetricAggregationDescriptor<TDocument> CombineScript(Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor descriptor)
	{
		CombineScriptValue = null;
		CombineScriptDescriptorAction = null;
		CombineScriptDescriptor = descriptor;
		return Self;
	}

	public ScriptedMetricAggregationDescriptor<TDocument> CombineScript(Action<Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor> configure)
	{
		CombineScriptValue = null;
		CombineScriptDescriptor = null;
		CombineScriptDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field on which to run the aggregation.
	/// </para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Serverless.Field? field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field on which to run the aggregation.
	/// </para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field on which to run the aggregation.
	/// </para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor<TDocument> Field(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Runs prior to any collection of documents.
	/// Allows the aggregation to set up any initial state.
	/// </para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor<TDocument> InitScript(Elastic.Clients.Elasticsearch.Serverless.Script? initScript)
	{
		InitScriptDescriptor = null;
		InitScriptDescriptorAction = null;
		InitScriptValue = initScript;
		return Self;
	}

	public ScriptedMetricAggregationDescriptor<TDocument> InitScript(Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor descriptor)
	{
		InitScriptValue = null;
		InitScriptDescriptorAction = null;
		InitScriptDescriptor = descriptor;
		return Self;
	}

	public ScriptedMetricAggregationDescriptor<TDocument> InitScript(Action<Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor> configure)
	{
		InitScriptValue = null;
		InitScriptDescriptor = null;
		InitScriptDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Run once per document collected.
	/// If no <c>combine_script</c> is specified, the resulting state needs to be stored in the <c>state</c> object.
	/// </para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor<TDocument> MapScript(Elastic.Clients.Elasticsearch.Serverless.Script? mapScript)
	{
		MapScriptDescriptor = null;
		MapScriptDescriptorAction = null;
		MapScriptValue = mapScript;
		return Self;
	}

	public ScriptedMetricAggregationDescriptor<TDocument> MapScript(Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor descriptor)
	{
		MapScriptValue = null;
		MapScriptDescriptorAction = null;
		MapScriptDescriptor = descriptor;
		return Self;
	}

	public ScriptedMetricAggregationDescriptor<TDocument> MapScript(Action<Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor> configure)
	{
		MapScriptValue = null;
		MapScriptDescriptor = null;
		MapScriptDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The value to apply to documents that do not have a value.
	/// By default, documents without a value are ignored.
	/// </para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor<TDocument> Missing(Elastic.Clients.Elasticsearch.Serverless.FieldValue? missing)
	{
		MissingValue = missing;
		return Self;
	}

	/// <summary>
	/// <para>
	/// A global object with script parameters for <c>init</c>, <c>map</c> and <c>combine</c> scripts.
	/// It is shared between the scripts.
	/// </para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor<TDocument> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		ParamsValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// Runs once on the coordinating node after all shards have returned their results.
	/// The script is provided with access to a variable <c>states</c>, which is an array of the result of the <c>combine_script</c> on each shard.
	/// </para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor<TDocument> ReduceScript(Elastic.Clients.Elasticsearch.Serverless.Script? reduceScript)
	{
		ReduceScriptDescriptor = null;
		ReduceScriptDescriptorAction = null;
		ReduceScriptValue = reduceScript;
		return Self;
	}

	public ScriptedMetricAggregationDescriptor<TDocument> ReduceScript(Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor descriptor)
	{
		ReduceScriptValue = null;
		ReduceScriptDescriptorAction = null;
		ReduceScriptDescriptor = descriptor;
		return Self;
	}

	public ScriptedMetricAggregationDescriptor<TDocument> ReduceScript(Action<Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor> configure)
	{
		ReduceScriptValue = null;
		ReduceScriptDescriptor = null;
		ReduceScriptDescriptorAction = configure;
		return Self;
	}

	public ScriptedMetricAggregationDescriptor<TDocument> Script(Elastic.Clients.Elasticsearch.Serverless.Script? script)
	{
		ScriptDescriptor = null;
		ScriptDescriptorAction = null;
		ScriptValue = script;
		return Self;
	}

	public ScriptedMetricAggregationDescriptor<TDocument> Script(Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor descriptor)
	{
		ScriptValue = null;
		ScriptDescriptorAction = null;
		ScriptDescriptor = descriptor;
		return Self;
	}

	public ScriptedMetricAggregationDescriptor<TDocument> Script(Action<Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor> configure)
	{
		ScriptValue = null;
		ScriptDescriptor = null;
		ScriptDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (CombineScriptDescriptor is not null)
		{
			writer.WritePropertyName("combine_script");
			JsonSerializer.Serialize(writer, CombineScriptDescriptor, options);
		}
		else if (CombineScriptDescriptorAction is not null)
		{
			writer.WritePropertyName("combine_script");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor(CombineScriptDescriptorAction), options);
		}
		else if (CombineScriptValue is not null)
		{
			writer.WritePropertyName("combine_script");
			JsonSerializer.Serialize(writer, CombineScriptValue, options);
		}

		if (FieldValue is not null)
		{
			writer.WritePropertyName("field");
			JsonSerializer.Serialize(writer, FieldValue, options);
		}

		if (InitScriptDescriptor is not null)
		{
			writer.WritePropertyName("init_script");
			JsonSerializer.Serialize(writer, InitScriptDescriptor, options);
		}
		else if (InitScriptDescriptorAction is not null)
		{
			writer.WritePropertyName("init_script");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor(InitScriptDescriptorAction), options);
		}
		else if (InitScriptValue is not null)
		{
			writer.WritePropertyName("init_script");
			JsonSerializer.Serialize(writer, InitScriptValue, options);
		}

		if (MapScriptDescriptor is not null)
		{
			writer.WritePropertyName("map_script");
			JsonSerializer.Serialize(writer, MapScriptDescriptor, options);
		}
		else if (MapScriptDescriptorAction is not null)
		{
			writer.WritePropertyName("map_script");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor(MapScriptDescriptorAction), options);
		}
		else if (MapScriptValue is not null)
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

		if (ReduceScriptDescriptor is not null)
		{
			writer.WritePropertyName("reduce_script");
			JsonSerializer.Serialize(writer, ReduceScriptDescriptor, options);
		}
		else if (ReduceScriptDescriptorAction is not null)
		{
			writer.WritePropertyName("reduce_script");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor(ReduceScriptDescriptorAction), options);
		}
		else if (ReduceScriptValue is not null)
		{
			writer.WritePropertyName("reduce_script");
			JsonSerializer.Serialize(writer, ReduceScriptValue, options);
		}

		if (ScriptDescriptor is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, ScriptDescriptor, options);
		}
		else if (ScriptDescriptorAction is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor(ScriptDescriptorAction), options);
		}
		else if (ScriptValue is not null)
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

	private Elastic.Clients.Elasticsearch.Serverless.Script? CombineScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor CombineScriptDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor> CombineScriptDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Field? FieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Script? InitScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor InitScriptDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor> InitScriptDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Script? MapScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor MapScriptDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor> MapScriptDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.FieldValue? MissingValue { get; set; }
	private IDictionary<string, object>? ParamsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Script? ReduceScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor ReduceScriptDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor> ReduceScriptDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Script? ScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor ScriptDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor> ScriptDescriptorAction { get; set; }

	/// <summary>
	/// <para>
	/// Runs once on each shard after document collection is complete.
	/// Allows the aggregation to consolidate the state returned from each shard.
	/// </para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor CombineScript(Elastic.Clients.Elasticsearch.Serverless.Script? combineScript)
	{
		CombineScriptDescriptor = null;
		CombineScriptDescriptorAction = null;
		CombineScriptValue = combineScript;
		return Self;
	}

	public ScriptedMetricAggregationDescriptor CombineScript(Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor descriptor)
	{
		CombineScriptValue = null;
		CombineScriptDescriptorAction = null;
		CombineScriptDescriptor = descriptor;
		return Self;
	}

	public ScriptedMetricAggregationDescriptor CombineScript(Action<Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor> configure)
	{
		CombineScriptValue = null;
		CombineScriptDescriptor = null;
		CombineScriptDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field on which to run the aggregation.
	/// </para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor Field(Elastic.Clients.Elasticsearch.Serverless.Field? field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field on which to run the aggregation.
	/// </para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field on which to run the aggregation.
	/// </para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Runs prior to any collection of documents.
	/// Allows the aggregation to set up any initial state.
	/// </para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor InitScript(Elastic.Clients.Elasticsearch.Serverless.Script? initScript)
	{
		InitScriptDescriptor = null;
		InitScriptDescriptorAction = null;
		InitScriptValue = initScript;
		return Self;
	}

	public ScriptedMetricAggregationDescriptor InitScript(Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor descriptor)
	{
		InitScriptValue = null;
		InitScriptDescriptorAction = null;
		InitScriptDescriptor = descriptor;
		return Self;
	}

	public ScriptedMetricAggregationDescriptor InitScript(Action<Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor> configure)
	{
		InitScriptValue = null;
		InitScriptDescriptor = null;
		InitScriptDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Run once per document collected.
	/// If no <c>combine_script</c> is specified, the resulting state needs to be stored in the <c>state</c> object.
	/// </para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor MapScript(Elastic.Clients.Elasticsearch.Serverless.Script? mapScript)
	{
		MapScriptDescriptor = null;
		MapScriptDescriptorAction = null;
		MapScriptValue = mapScript;
		return Self;
	}

	public ScriptedMetricAggregationDescriptor MapScript(Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor descriptor)
	{
		MapScriptValue = null;
		MapScriptDescriptorAction = null;
		MapScriptDescriptor = descriptor;
		return Self;
	}

	public ScriptedMetricAggregationDescriptor MapScript(Action<Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor> configure)
	{
		MapScriptValue = null;
		MapScriptDescriptor = null;
		MapScriptDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The value to apply to documents that do not have a value.
	/// By default, documents without a value are ignored.
	/// </para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor Missing(Elastic.Clients.Elasticsearch.Serverless.FieldValue? missing)
	{
		MissingValue = missing;
		return Self;
	}

	/// <summary>
	/// <para>
	/// A global object with script parameters for <c>init</c>, <c>map</c> and <c>combine</c> scripts.
	/// It is shared between the scripts.
	/// </para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		ParamsValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// Runs once on the coordinating node after all shards have returned their results.
	/// The script is provided with access to a variable <c>states</c>, which is an array of the result of the <c>combine_script</c> on each shard.
	/// </para>
	/// </summary>
	public ScriptedMetricAggregationDescriptor ReduceScript(Elastic.Clients.Elasticsearch.Serverless.Script? reduceScript)
	{
		ReduceScriptDescriptor = null;
		ReduceScriptDescriptorAction = null;
		ReduceScriptValue = reduceScript;
		return Self;
	}

	public ScriptedMetricAggregationDescriptor ReduceScript(Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor descriptor)
	{
		ReduceScriptValue = null;
		ReduceScriptDescriptorAction = null;
		ReduceScriptDescriptor = descriptor;
		return Self;
	}

	public ScriptedMetricAggregationDescriptor ReduceScript(Action<Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor> configure)
	{
		ReduceScriptValue = null;
		ReduceScriptDescriptor = null;
		ReduceScriptDescriptorAction = configure;
		return Self;
	}

	public ScriptedMetricAggregationDescriptor Script(Elastic.Clients.Elasticsearch.Serverless.Script? script)
	{
		ScriptDescriptor = null;
		ScriptDescriptorAction = null;
		ScriptValue = script;
		return Self;
	}

	public ScriptedMetricAggregationDescriptor Script(Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor descriptor)
	{
		ScriptValue = null;
		ScriptDescriptorAction = null;
		ScriptDescriptor = descriptor;
		return Self;
	}

	public ScriptedMetricAggregationDescriptor Script(Action<Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor> configure)
	{
		ScriptValue = null;
		ScriptDescriptor = null;
		ScriptDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (CombineScriptDescriptor is not null)
		{
			writer.WritePropertyName("combine_script");
			JsonSerializer.Serialize(writer, CombineScriptDescriptor, options);
		}
		else if (CombineScriptDescriptorAction is not null)
		{
			writer.WritePropertyName("combine_script");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor(CombineScriptDescriptorAction), options);
		}
		else if (CombineScriptValue is not null)
		{
			writer.WritePropertyName("combine_script");
			JsonSerializer.Serialize(writer, CombineScriptValue, options);
		}

		if (FieldValue is not null)
		{
			writer.WritePropertyName("field");
			JsonSerializer.Serialize(writer, FieldValue, options);
		}

		if (InitScriptDescriptor is not null)
		{
			writer.WritePropertyName("init_script");
			JsonSerializer.Serialize(writer, InitScriptDescriptor, options);
		}
		else if (InitScriptDescriptorAction is not null)
		{
			writer.WritePropertyName("init_script");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor(InitScriptDescriptorAction), options);
		}
		else if (InitScriptValue is not null)
		{
			writer.WritePropertyName("init_script");
			JsonSerializer.Serialize(writer, InitScriptValue, options);
		}

		if (MapScriptDescriptor is not null)
		{
			writer.WritePropertyName("map_script");
			JsonSerializer.Serialize(writer, MapScriptDescriptor, options);
		}
		else if (MapScriptDescriptorAction is not null)
		{
			writer.WritePropertyName("map_script");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor(MapScriptDescriptorAction), options);
		}
		else if (MapScriptValue is not null)
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

		if (ReduceScriptDescriptor is not null)
		{
			writer.WritePropertyName("reduce_script");
			JsonSerializer.Serialize(writer, ReduceScriptDescriptor, options);
		}
		else if (ReduceScriptDescriptorAction is not null)
		{
			writer.WritePropertyName("reduce_script");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor(ReduceScriptDescriptorAction), options);
		}
		else if (ReduceScriptValue is not null)
		{
			writer.WritePropertyName("reduce_script");
			JsonSerializer.Serialize(writer, ReduceScriptValue, options);
		}

		if (ScriptDescriptor is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, ScriptDescriptor, options);
		}
		else if (ScriptDescriptorAction is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.ScriptDescriptor(ScriptDescriptorAction), options);
		}
		else if (ScriptValue is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, ScriptValue, options);
		}

		writer.WriteEndObject();
	}
}
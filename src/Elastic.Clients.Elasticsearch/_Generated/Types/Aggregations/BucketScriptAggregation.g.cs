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

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Aggregations;

internal sealed partial class BucketScriptAggregationConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Aggregations.BucketScriptAggregation>
{
	private static readonly System.Text.Json.JsonEncodedText PropBucketsPath = System.Text.Json.JsonEncodedText.Encode("buckets_path");
	private static readonly System.Text.Json.JsonEncodedText PropFormat = System.Text.Json.JsonEncodedText.Encode("format");
	private static readonly System.Text.Json.JsonEncodedText PropGapPolicy = System.Text.Json.JsonEncodedText.Encode("gap_policy");
	private static readonly System.Text.Json.JsonEncodedText PropScript = System.Text.Json.JsonEncodedText.Encode("script");

	public override Elastic.Clients.Elasticsearch.Aggregations.BucketScriptAggregation Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<object?> propBucketsPath = default;
		LocalJsonValue<string?> propFormat = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Aggregations.GapPolicy?> propGapPolicy = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Script?> propScript = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBucketsPath.TryReadProperty(ref reader, options, PropBucketsPath, null))
			{
				continue;
			}

			if (propFormat.TryReadProperty(ref reader, options, PropFormat, null))
			{
				continue;
			}

			if (propGapPolicy.TryReadProperty(ref reader, options, PropGapPolicy, null))
			{
				continue;
			}

			if (propScript.TryReadProperty(ref reader, options, PropScript, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Aggregations.BucketScriptAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			BucketsPath = propBucketsPath.Value,
			Format = propFormat.Value,
			GapPolicy = propGapPolicy.Value,
			Script = propScript.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Aggregations.BucketScriptAggregation value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBucketsPath, value.BucketsPath, null, null);
		writer.WriteProperty(options, PropFormat, value.Format, null, null);
		writer.WriteProperty(options, PropGapPolicy, value.GapPolicy, null, null);
		writer.WriteProperty(options, PropScript, value.Script, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Aggregations.BucketScriptAggregationConverter))]
public sealed partial class BucketScriptAggregation
{
#if NET7_0_OR_GREATER
	public BucketScriptAggregation()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public BucketScriptAggregation()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal BucketScriptAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Path to the buckets that contain one set of values to correlate.
	/// </para>
	/// </summary>
	public object? BucketsPath { get; set; }

	/// <summary>
	/// <para>
	/// <c>DecimalFormat</c> pattern for the output value.
	/// If specified, the formatted value is returned in the aggregation’s <c>value_as_string</c> property.
	/// </para>
	/// </summary>
	public string? Format { get; set; }

	/// <summary>
	/// <para>
	/// Policy to apply when gaps are found in the data.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GapPolicy? GapPolicy { get; set; }

	/// <summary>
	/// <para>
	/// The script to run for this aggregation.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Script? Script { get; set; }
}

public readonly partial struct BucketScriptAggregationDescriptor
{
	internal Elastic.Clients.Elasticsearch.Aggregations.BucketScriptAggregation Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public BucketScriptAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.BucketScriptAggregation instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public BucketScriptAggregationDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Aggregations.BucketScriptAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Aggregations.BucketScriptAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.BucketScriptAggregation instance) => new Elastic.Clients.Elasticsearch.Aggregations.BucketScriptAggregationDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.BucketScriptAggregation(Elastic.Clients.Elasticsearch.Aggregations.BucketScriptAggregationDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Path to the buckets that contain one set of values to correlate.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.BucketScriptAggregationDescriptor BucketsPath(object? value)
	{
		Instance.BucketsPath = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// <c>DecimalFormat</c> pattern for the output value.
	/// If specified, the formatted value is returned in the aggregation’s <c>value_as_string</c> property.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.BucketScriptAggregationDescriptor Format(string? value)
	{
		Instance.Format = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Policy to apply when gaps are found in the data.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.BucketScriptAggregationDescriptor GapPolicy(Elastic.Clients.Elasticsearch.Aggregations.GapPolicy? value)
	{
		Instance.GapPolicy = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The script to run for this aggregation.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.BucketScriptAggregationDescriptor Script(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.Script = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The script to run for this aggregation.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.BucketScriptAggregationDescriptor Script()
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// The script to run for this aggregation.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.BucketScriptAggregationDescriptor Script(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.BucketScriptAggregation Build(System.Action<Elastic.Clients.Elasticsearch.Aggregations.BucketScriptAggregationDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Aggregations.BucketScriptAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Aggregations.BucketScriptAggregationDescriptor(new Elastic.Clients.Elasticsearch.Aggregations.BucketScriptAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
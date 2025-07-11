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

namespace Elastic.Clients.Elasticsearch.IndexManagement;

internal sealed partial class DataStreamLifecycleWithRolloverConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRollover>
{
	private static readonly System.Text.Json.JsonEncodedText PropDataRetention = System.Text.Json.JsonEncodedText.Encode("data_retention");
	private static readonly System.Text.Json.JsonEncodedText PropDownsampling = System.Text.Json.JsonEncodedText.Encode("downsampling");
	private static readonly System.Text.Json.JsonEncodedText PropEnabled = System.Text.Json.JsonEncodedText.Encode("enabled");
	private static readonly System.Text.Json.JsonEncodedText PropRollover = System.Text.Json.JsonEncodedText.Encode("rollover");

	public override Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRollover Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propDataRetention = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleDownsampling?> propDownsampling = default;
		LocalJsonValue<bool?> propEnabled = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleRolloverConditions?> propRollover = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDataRetention.TryReadProperty(ref reader, options, PropDataRetention, null))
			{
				continue;
			}

			if (propDownsampling.TryReadProperty(ref reader, options, PropDownsampling, null))
			{
				continue;
			}

			if (propEnabled.TryReadProperty(ref reader, options, PropEnabled, static bool? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<bool>(o)))
			{
				continue;
			}

			if (propRollover.TryReadProperty(ref reader, options, PropRollover, null))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRollover(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			DataRetention = propDataRetention.Value,
			Downsampling = propDownsampling.Value,
			Enabled = propEnabled.Value,
			Rollover = propRollover.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRollover value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDataRetention, value.DataRetention, null, null);
		writer.WriteProperty(options, PropDownsampling, value.Downsampling, null, null);
		writer.WriteProperty(options, PropEnabled, value.Enabled, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, bool? v) => w.WriteNullableValue<bool>(o, v));
		writer.WriteProperty(options, PropRollover, value.Rollover, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Data stream lifecycle with rollover can be used to display the configuration including the default rollover conditions,
/// if asked.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRolloverConverter))]
public sealed partial class DataStreamLifecycleWithRollover
{
#if NET7_0_OR_GREATER
	public DataStreamLifecycleWithRollover()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public DataStreamLifecycleWithRollover()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DataStreamLifecycleWithRollover(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// If defined, every document added to this data stream will be stored at least for this time frame.
	/// Any time after this duration the document could be deleted.
	/// When empty, every document in this data stream will be stored indefinitely.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? DataRetention { get; set; }

	/// <summary>
	/// <para>
	/// The downsampling configuration to execute for the managed backing index after rollover.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleDownsampling? Downsampling { get; set; }

	/// <summary>
	/// <para>
	/// If defined, it turns data stream lifecycle on/off (<c>true</c>/<c>false</c>) for this data stream. A data stream lifecycle
	/// that's disabled (enabled: <c>false</c>) will have no effect on the data stream.
	/// </para>
	/// </summary>
	public bool? Enabled { get; set; }

	/// <summary>
	/// <para>
	/// The conditions which will trigger the rollover of a backing index as configured by the cluster setting <c>cluster.lifecycle.default.rollover</c>.
	/// This property is an implementation detail and it will only be retrieved when the query param <c>include_defaults</c> is set to true.
	/// The contents of this field are subject to change.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleRolloverConditions? Rollover { get; set; }
}

/// <summary>
/// <para>
/// Data stream lifecycle with rollover can be used to display the configuration including the default rollover conditions,
/// if asked.
/// </para>
/// </summary>
public readonly partial struct DataStreamLifecycleWithRolloverDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRollover Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DataStreamLifecycleWithRolloverDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRollover instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DataStreamLifecycleWithRolloverDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRollover(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRolloverDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRollover instance) => new Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRolloverDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRollover(Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRolloverDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// If defined, every document added to this data stream will be stored at least for this time frame.
	/// Any time after this duration the document could be deleted.
	/// When empty, every document in this data stream will be stored indefinitely.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRolloverDescriptor DataRetention(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.DataRetention = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The downsampling configuration to execute for the managed backing index after rollover.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRolloverDescriptor Downsampling(Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleDownsampling? value)
	{
		Instance.Downsampling = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The downsampling configuration to execute for the managed backing index after rollover.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRolloverDescriptor Downsampling(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleDownsamplingDescriptor> action)
	{
		Instance.Downsampling = Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleDownsamplingDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// If defined, it turns data stream lifecycle on/off (<c>true</c>/<c>false</c>) for this data stream. A data stream lifecycle
	/// that's disabled (enabled: <c>false</c>) will have no effect on the data stream.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRolloverDescriptor Enabled(bool? value = true)
	{
		Instance.Enabled = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The conditions which will trigger the rollover of a backing index as configured by the cluster setting <c>cluster.lifecycle.default.rollover</c>.
	/// This property is an implementation detail and it will only be retrieved when the query param <c>include_defaults</c> is set to true.
	/// The contents of this field are subject to change.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRolloverDescriptor Rollover(Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleRolloverConditions? value)
	{
		Instance.Rollover = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The conditions which will trigger the rollover of a backing index as configured by the cluster setting <c>cluster.lifecycle.default.rollover</c>.
	/// This property is an implementation detail and it will only be retrieved when the query param <c>include_defaults</c> is set to true.
	/// The contents of this field are subject to change.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRolloverDescriptor Rollover()
	{
		Instance.Rollover = Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleRolloverConditionsDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// The conditions which will trigger the rollover of a backing index as configured by the cluster setting <c>cluster.lifecycle.default.rollover</c>.
	/// This property is an implementation detail and it will only be retrieved when the query param <c>include_defaults</c> is set to true.
	/// The contents of this field are subject to change.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRolloverDescriptor Rollover(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleRolloverConditionsDescriptor>? action)
	{
		Instance.Rollover = Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleRolloverConditionsDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRollover Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRolloverDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRollover(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRolloverDescriptor(new Elastic.Clients.Elasticsearch.IndexManagement.DataStreamLifecycleWithRollover(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
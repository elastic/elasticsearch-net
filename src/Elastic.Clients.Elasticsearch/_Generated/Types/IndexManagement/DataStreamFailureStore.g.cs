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

internal sealed partial class DataStreamFailureStoreConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.DataStreamFailureStore>
{
	private static readonly System.Text.Json.JsonEncodedText PropEnabled = System.Text.Json.JsonEncodedText.Encode("enabled");
	private static readonly System.Text.Json.JsonEncodedText PropLifecycle = System.Text.Json.JsonEncodedText.Encode("lifecycle");

	public override Elastic.Clients.Elasticsearch.IndexManagement.DataStreamFailureStore Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool?> propEnabled = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.FailureStoreLifecycle?> propLifecycle = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propEnabled.TryReadProperty(ref reader, options, PropEnabled, static bool? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<bool>(o)))
			{
				continue;
			}

			if (propLifecycle.TryReadProperty(ref reader, options, PropLifecycle, null))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.DataStreamFailureStore(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Enabled = propEnabled.Value,
			Lifecycle = propLifecycle.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.DataStreamFailureStore value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropEnabled, value.Enabled, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, bool? v) => w.WriteNullableValue<bool>(o, v));
		writer.WriteProperty(options, PropLifecycle, value.Lifecycle, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Data stream failure store contains the configuration of the failure store for a given data stream.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.DataStreamFailureStoreConverter))]
public sealed partial class DataStreamFailureStore
{
#if NET7_0_OR_GREATER
	public DataStreamFailureStore()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public DataStreamFailureStore()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DataStreamFailureStore(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// If defined, it turns the failure store on/off (<c>true</c>/<c>false</c>) for this data stream. A data stream failure store
	/// that's disabled (enabled: <c>false</c>) will redirect no new failed indices to the failure store; however, it will
	/// not remove any existing data from the failure store.
	/// </para>
	/// </summary>
	public bool? Enabled { get; set; }

	/// <summary>
	/// <para>
	/// If defined, it specifies the lifecycle configuration for the failure store of this data stream.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.FailureStoreLifecycle? Lifecycle { get; set; }
}

/// <summary>
/// <para>
/// Data stream failure store contains the configuration of the failure store for a given data stream.
/// </para>
/// </summary>
public readonly partial struct DataStreamFailureStoreDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.DataStreamFailureStore Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DataStreamFailureStoreDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.DataStreamFailureStore instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DataStreamFailureStoreDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.DataStreamFailureStore(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.DataStreamFailureStoreDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.DataStreamFailureStore instance) => new Elastic.Clients.Elasticsearch.IndexManagement.DataStreamFailureStoreDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.DataStreamFailureStore(Elastic.Clients.Elasticsearch.IndexManagement.DataStreamFailureStoreDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// If defined, it turns the failure store on/off (<c>true</c>/<c>false</c>) for this data stream. A data stream failure store
	/// that's disabled (enabled: <c>false</c>) will redirect no new failed indices to the failure store; however, it will
	/// not remove any existing data from the failure store.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.DataStreamFailureStoreDescriptor Enabled(bool? value = true)
	{
		Instance.Enabled = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If defined, it specifies the lifecycle configuration for the failure store of this data stream.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.DataStreamFailureStoreDescriptor Lifecycle(Elastic.Clients.Elasticsearch.IndexManagement.FailureStoreLifecycle? value)
	{
		Instance.Lifecycle = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If defined, it specifies the lifecycle configuration for the failure store of this data stream.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.DataStreamFailureStoreDescriptor Lifecycle()
	{
		Instance.Lifecycle = Elastic.Clients.Elasticsearch.IndexManagement.FailureStoreLifecycleDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// If defined, it specifies the lifecycle configuration for the failure store of this data stream.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.DataStreamFailureStoreDescriptor Lifecycle(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.FailureStoreLifecycleDescriptor>? action)
	{
		Instance.Lifecycle = Elastic.Clients.Elasticsearch.IndexManagement.FailureStoreLifecycleDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.DataStreamFailureStore Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.DataStreamFailureStoreDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.IndexManagement.DataStreamFailureStore(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.DataStreamFailureStoreDescriptor(new Elastic.Clients.Elasticsearch.IndexManagement.DataStreamFailureStore(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
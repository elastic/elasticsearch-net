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

namespace Elastic.Clients.Elasticsearch.IndexLifecycleManagement;

internal sealed partial class IlmPolicyConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.IlmPolicy>
{
	private static readonly System.Text.Json.JsonEncodedText PropMeta = System.Text.Json.JsonEncodedText.Encode("_meta");
	private static readonly System.Text.Json.JsonEncodedText PropPhases = System.Text.Json.JsonEncodedText.Encode("phases");

	public override Elastic.Clients.Elasticsearch.IndexLifecycleManagement.IlmPolicy Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IDictionary<string, object>?> propMeta = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.Phases> propPhases = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propMeta.TryReadProperty(ref reader, options, PropMeta, static System.Collections.Generic.IDictionary<string, object>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, object>(o, null, null)))
			{
				continue;
			}

			if (propPhases.TryReadProperty(ref reader, options, PropPhases, null))
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
		return new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.IlmPolicy(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Meta = propMeta.Value,
			Phases = propPhases.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexLifecycleManagement.IlmPolicy value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropMeta, value.Meta, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, object>? v) => w.WriteDictionaryValue<string, object>(o, v, null, null));
		writer.WriteProperty(options, PropPhases, value.Phases, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.IlmPolicyConverter))]
public sealed partial class IlmPolicy
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IlmPolicy(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.Phases phases)
	{
		Phases = phases;
	}
#if NET7_0_OR_GREATER
	public IlmPolicy()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public IlmPolicy()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal IlmPolicy(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Arbitrary metadata that is not automatically generated or used by Elasticsearch.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IDictionary<string, object>? Meta { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.IndexLifecycleManagement.Phases Phases { get; set; }
}

public readonly partial struct IlmPolicyDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexLifecycleManagement.IlmPolicy Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IlmPolicyDescriptor(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.IlmPolicy instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IlmPolicyDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.IlmPolicy(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexLifecycleManagement.IlmPolicyDescriptor(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.IlmPolicy instance) => new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.IlmPolicyDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexLifecycleManagement.IlmPolicy(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.IlmPolicyDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Arbitrary metadata that is not automatically generated or used by Elasticsearch.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.IlmPolicyDescriptor Meta(System.Collections.Generic.IDictionary<string, object>? value)
	{
		Instance.Meta = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Arbitrary metadata that is not automatically generated or used by Elasticsearch.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.IlmPolicyDescriptor Meta()
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Arbitrary metadata that is not automatically generated or used by Elasticsearch.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.IlmPolicyDescriptor Meta(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject>? action)
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.IlmPolicyDescriptor AddMeta(string key, object value)
	{
		Instance.Meta ??= new System.Collections.Generic.Dictionary<string, object>();
		Instance.Meta.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.IlmPolicyDescriptor Phases(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.Phases value)
	{
		Instance.Phases = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.IlmPolicyDescriptor Phases()
	{
		Instance.Phases = Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhasesDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.IlmPolicyDescriptor Phases(System.Action<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhasesDescriptor>? action)
	{
		Instance.Phases = Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhasesDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexLifecycleManagement.IlmPolicy Build(System.Action<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.IlmPolicyDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.IlmPolicyDescriptor(new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.IlmPolicy(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
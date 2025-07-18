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

internal sealed partial class AllocateActionConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateAction>
{
	private static readonly System.Text.Json.JsonEncodedText PropExclude = System.Text.Json.JsonEncodedText.Encode("exclude");
	private static readonly System.Text.Json.JsonEncodedText PropInclude = System.Text.Json.JsonEncodedText.Encode("include");
	private static readonly System.Text.Json.JsonEncodedText PropNumberOfReplicas = System.Text.Json.JsonEncodedText.Encode("number_of_replicas");
	private static readonly System.Text.Json.JsonEncodedText PropRequire = System.Text.Json.JsonEncodedText.Encode("require");
	private static readonly System.Text.Json.JsonEncodedText PropTotalShardsPerNode = System.Text.Json.JsonEncodedText.Encode("total_shards_per_node");

	public override Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateAction Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IDictionary<string, string>?> propExclude = default;
		LocalJsonValue<System.Collections.Generic.IDictionary<string, string>?> propInclude = default;
		LocalJsonValue<int?> propNumberOfReplicas = default;
		LocalJsonValue<System.Collections.Generic.IDictionary<string, string>?> propRequire = default;
		LocalJsonValue<int?> propTotalShardsPerNode = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propExclude.TryReadProperty(ref reader, options, PropExclude, static System.Collections.Generic.IDictionary<string, string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, string>(o, null, null)))
			{
				continue;
			}

			if (propInclude.TryReadProperty(ref reader, options, PropInclude, static System.Collections.Generic.IDictionary<string, string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, string>(o, null, null)))
			{
				continue;
			}

			if (propNumberOfReplicas.TryReadProperty(ref reader, options, PropNumberOfReplicas, static int? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<int>(o)))
			{
				continue;
			}

			if (propRequire.TryReadProperty(ref reader, options, PropRequire, static System.Collections.Generic.IDictionary<string, string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, string>(o, null, null)))
			{
				continue;
			}

			if (propTotalShardsPerNode.TryReadProperty(ref reader, options, PropTotalShardsPerNode, static int? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<int>(o)))
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
		return new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateAction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Exclude = propExclude.Value,
			Include = propInclude.Value,
			NumberOfReplicas = propNumberOfReplicas.Value,
			Require = propRequire.Value,
			TotalShardsPerNode = propTotalShardsPerNode.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateAction value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropExclude, value.Exclude, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, string>? v) => w.WriteDictionaryValue<string, string>(o, v, null, null));
		writer.WriteProperty(options, PropInclude, value.Include, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, string>? v) => w.WriteDictionaryValue<string, string>(o, v, null, null));
		writer.WriteProperty(options, PropNumberOfReplicas, value.NumberOfReplicas, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, int? v) => w.WriteNullableValue<int>(o, v));
		writer.WriteProperty(options, PropRequire, value.Require, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, string>? v) => w.WriteDictionaryValue<string, string>(o, v, null, null));
		writer.WriteProperty(options, PropTotalShardsPerNode, value.TotalShardsPerNode, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, int? v) => w.WriteNullableValue<int>(o, v));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateActionConverter))]
public sealed partial class AllocateAction
{
#if NET7_0_OR_GREATER
	public AllocateAction()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public AllocateAction()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal AllocateAction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public System.Collections.Generic.IDictionary<string, string>? Exclude { get; set; }
	public System.Collections.Generic.IDictionary<string, string>? Include { get; set; }
	public int? NumberOfReplicas { get; set; }
	public System.Collections.Generic.IDictionary<string, string>? Require { get; set; }
	public int? TotalShardsPerNode { get; set; }
}

public readonly partial struct AllocateActionDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateAction Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AllocateActionDescriptor(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateAction instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AllocateActionDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateAction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateActionDescriptor(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateAction instance) => new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateActionDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateAction(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateActionDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateActionDescriptor Exclude(System.Collections.Generic.IDictionary<string, string>? value)
	{
		Instance.Exclude = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateActionDescriptor Exclude()
	{
		Instance.Exclude = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateActionDescriptor Exclude(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString>? action)
	{
		Instance.Exclude = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateActionDescriptor AddExclude(string key, string value)
	{
		Instance.Exclude ??= new System.Collections.Generic.Dictionary<string, string>();
		Instance.Exclude.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateActionDescriptor Include(System.Collections.Generic.IDictionary<string, string>? value)
	{
		Instance.Include = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateActionDescriptor Include()
	{
		Instance.Include = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateActionDescriptor Include(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString>? action)
	{
		Instance.Include = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateActionDescriptor AddInclude(string key, string value)
	{
		Instance.Include ??= new System.Collections.Generic.Dictionary<string, string>();
		Instance.Include.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateActionDescriptor NumberOfReplicas(int? value)
	{
		Instance.NumberOfReplicas = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateActionDescriptor Require(System.Collections.Generic.IDictionary<string, string>? value)
	{
		Instance.Require = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateActionDescriptor Require()
	{
		Instance.Require = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateActionDescriptor Require(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString>? action)
	{
		Instance.Require = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateActionDescriptor AddRequire(string key, string value)
	{
		Instance.Require ??= new System.Collections.Generic.Dictionary<string, string>();
		Instance.Require.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateActionDescriptor TotalShardsPerNode(int? value)
	{
		Instance.TotalShardsPerNode = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateAction Build(System.Action<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateActionDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateAction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateActionDescriptor(new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.AllocateAction(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
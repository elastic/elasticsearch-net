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

namespace Elastic.Clients.Elasticsearch.Security;

internal sealed partial class AccessConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.Access>
{
	private static readonly System.Text.Json.JsonEncodedText PropReplication = System.Text.Json.JsonEncodedText.Encode("replication");
	private static readonly System.Text.Json.JsonEncodedText PropSearch = System.Text.Json.JsonEncodedText.Encode("search");

	public override Elastic.Clients.Elasticsearch.Security.Access Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.ReplicationAccess>?> propReplication = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.SearchAccess>?> propSearch = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propReplication.TryReadProperty(ref reader, options, PropReplication, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.ReplicationAccess>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Security.ReplicationAccess>(o, null)))
			{
				continue;
			}

			if (propSearch.TryReadProperty(ref reader, options, PropSearch, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.SearchAccess>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Security.SearchAccess>(o, null)))
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
		return new Elastic.Clients.Elasticsearch.Security.Access(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Replication = propReplication.Value,
			Search = propSearch.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.Access value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropReplication, value.Replication, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.ReplicationAccess>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Security.ReplicationAccess>(o, v, null));
		writer.WriteProperty(options, PropSearch, value.Search, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.SearchAccess>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Security.SearchAccess>(o, v, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.AccessConverter))]
public sealed partial class Access
{
#if NET7_0_OR_GREATER
	public Access()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public Access()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Access(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// A list of indices permission entries for cross-cluster replication.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.ReplicationAccess>? Replication { get; set; }

	/// <summary>
	/// <para>
	/// A list of indices permission entries for cross-cluster search.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.SearchAccess>? Search { get; set; }
}

public readonly partial struct AccessDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Security.Access Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AccessDescriptor(Elastic.Clients.Elasticsearch.Security.Access instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AccessDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Security.Access(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Security.AccessDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Security.Access instance) => new Elastic.Clients.Elasticsearch.Security.AccessDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.Access(Elastic.Clients.Elasticsearch.Security.AccessDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A list of indices permission entries for cross-cluster replication.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.AccessDescriptor<TDocument> Replication(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.ReplicationAccess>? value)
	{
		Instance.Replication = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of indices permission entries for cross-cluster replication.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.AccessDescriptor<TDocument> Replication(params Elastic.Clients.Elasticsearch.Security.ReplicationAccess[] values)
	{
		Instance.Replication = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of indices permission entries for cross-cluster replication.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.AccessDescriptor<TDocument> Replication(params System.Action<Elastic.Clients.Elasticsearch.Security.ReplicationAccessDescriptor>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Security.ReplicationAccess>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Security.ReplicationAccessDescriptor.Build(action));
		}

		Instance.Replication = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of indices permission entries for cross-cluster search.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.AccessDescriptor<TDocument> Search(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.SearchAccess>? value)
	{
		Instance.Search = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of indices permission entries for cross-cluster search.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.AccessDescriptor<TDocument> Search(params Elastic.Clients.Elasticsearch.Security.SearchAccess[] values)
	{
		Instance.Search = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of indices permission entries for cross-cluster search.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.AccessDescriptor<TDocument> Search(params System.Action<Elastic.Clients.Elasticsearch.Security.SearchAccessDescriptor<TDocument>>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Security.SearchAccess>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Security.SearchAccessDescriptor<TDocument>.Build(action));
		}

		Instance.Search = items;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Security.Access Build(System.Action<Elastic.Clients.Elasticsearch.Security.AccessDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Security.Access(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Security.AccessDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Security.Access(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct AccessDescriptor
{
	internal Elastic.Clients.Elasticsearch.Security.Access Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AccessDescriptor(Elastic.Clients.Elasticsearch.Security.Access instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AccessDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Security.Access(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Security.AccessDescriptor(Elastic.Clients.Elasticsearch.Security.Access instance) => new Elastic.Clients.Elasticsearch.Security.AccessDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.Access(Elastic.Clients.Elasticsearch.Security.AccessDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A list of indices permission entries for cross-cluster replication.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.AccessDescriptor Replication(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.ReplicationAccess>? value)
	{
		Instance.Replication = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of indices permission entries for cross-cluster replication.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.AccessDescriptor Replication(params Elastic.Clients.Elasticsearch.Security.ReplicationAccess[] values)
	{
		Instance.Replication = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of indices permission entries for cross-cluster replication.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.AccessDescriptor Replication(params System.Action<Elastic.Clients.Elasticsearch.Security.ReplicationAccessDescriptor>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Security.ReplicationAccess>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Security.ReplicationAccessDescriptor.Build(action));
		}

		Instance.Replication = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of indices permission entries for cross-cluster search.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.AccessDescriptor Search(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.SearchAccess>? value)
	{
		Instance.Search = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of indices permission entries for cross-cluster search.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.AccessDescriptor Search(params Elastic.Clients.Elasticsearch.Security.SearchAccess[] values)
	{
		Instance.Search = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of indices permission entries for cross-cluster search.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.AccessDescriptor Search(params System.Action<Elastic.Clients.Elasticsearch.Security.SearchAccessDescriptor>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Security.SearchAccess>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Security.SearchAccessDescriptor.Build(action));
		}

		Instance.Search = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of indices permission entries for cross-cluster search.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.AccessDescriptor Search<T>(params System.Action<Elastic.Clients.Elasticsearch.Security.SearchAccessDescriptor<T>>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Security.SearchAccess>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Security.SearchAccessDescriptor<T>.Build(action));
		}

		Instance.Search = items;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Security.Access Build(System.Action<Elastic.Clients.Elasticsearch.Security.AccessDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Security.Access(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Security.AccessDescriptor(new Elastic.Clients.Elasticsearch.Security.Access(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
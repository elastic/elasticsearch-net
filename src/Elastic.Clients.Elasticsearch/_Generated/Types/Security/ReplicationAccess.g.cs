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

internal sealed partial class ReplicationAccessConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.ReplicationAccess>
{
	private static readonly System.Text.Json.JsonEncodedText PropAllowRestrictedIndices = System.Text.Json.JsonEncodedText.Encode("allow_restricted_indices");
	private static readonly System.Text.Json.JsonEncodedText PropNames = System.Text.Json.JsonEncodedText.Encode("names");

	public override Elastic.Clients.Elasticsearch.Security.ReplicationAccess Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool?> propAllowRestrictedIndices = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.IndexName>> propNames = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAllowRestrictedIndices.TryReadProperty(ref reader, options, PropAllowRestrictedIndices, null))
			{
				continue;
			}

			if (propNames.TryReadProperty(ref reader, options, PropNames, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.IndexName> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadSingleOrManyCollectionValue<Elastic.Clients.Elasticsearch.IndexName>(o, null)!))
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
		return new Elastic.Clients.Elasticsearch.Security.ReplicationAccess(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AllowRestrictedIndices = propAllowRestrictedIndices.Value,
			Names = propNames.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.ReplicationAccess value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAllowRestrictedIndices, value.AllowRestrictedIndices, null, null);
		writer.WriteProperty(options, PropNames, value.Names, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.IndexName> v) => w.WriteSingleOrManyCollectionValue<Elastic.Clients.Elasticsearch.IndexName>(o, v, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.ReplicationAccessConverter))]
public sealed partial class ReplicationAccess
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ReplicationAccess(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.IndexName> names)
	{
		Names = names;
	}
#if NET7_0_OR_GREATER
	public ReplicationAccess()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public ReplicationAccess()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ReplicationAccess(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// This needs to be set to true if the patterns in the names field should cover system indices.
	/// </para>
	/// </summary>
	public bool? AllowRestrictedIndices { get; set; }

	/// <summary>
	/// <para>
	/// A list of indices (or index name patterns) to which the permissions in this entry apply.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.IndexName> Names { get; set; }
}

public readonly partial struct ReplicationAccessDescriptor
{
	internal Elastic.Clients.Elasticsearch.Security.ReplicationAccess Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ReplicationAccessDescriptor(Elastic.Clients.Elasticsearch.Security.ReplicationAccess instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ReplicationAccessDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Security.ReplicationAccess(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Security.ReplicationAccessDescriptor(Elastic.Clients.Elasticsearch.Security.ReplicationAccess instance) => new Elastic.Clients.Elasticsearch.Security.ReplicationAccessDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.ReplicationAccess(Elastic.Clients.Elasticsearch.Security.ReplicationAccessDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// This needs to be set to true if the patterns in the names field should cover system indices.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.ReplicationAccessDescriptor AllowRestrictedIndices(bool? value = true)
	{
		Instance.AllowRestrictedIndices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of indices (or index name patterns) to which the permissions in this entry apply.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.ReplicationAccessDescriptor Names(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.IndexName> value)
	{
		Instance.Names = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of indices (or index name patterns) to which the permissions in this entry apply.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.ReplicationAccessDescriptor Names(params Elastic.Clients.Elasticsearch.IndexName[] values)
	{
		Instance.Names = [.. values];
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Security.ReplicationAccess Build(System.Action<Elastic.Clients.Elasticsearch.Security.ReplicationAccessDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Security.ReplicationAccessDescriptor(new Elastic.Clients.Elasticsearch.Security.ReplicationAccess(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
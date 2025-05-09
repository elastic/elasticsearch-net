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

namespace Elastic.Clients.Elasticsearch.QueryDsl;

internal sealed partial class TermsLookupConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.QueryDsl.TermsLookup>
{
	private static readonly System.Text.Json.JsonEncodedText PropId = System.Text.Json.JsonEncodedText.Encode("id");
	private static readonly System.Text.Json.JsonEncodedText PropIndex = System.Text.Json.JsonEncodedText.Encode("index");
	private static readonly System.Text.Json.JsonEncodedText PropPath = System.Text.Json.JsonEncodedText.Encode("path");
	private static readonly System.Text.Json.JsonEncodedText PropRouting = System.Text.Json.JsonEncodedText.Encode("routing");

	public override Elastic.Clients.Elasticsearch.QueryDsl.TermsLookup Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Id> propId = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexName> propIndex = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propPath = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Routing?> propRouting = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propId.TryReadProperty(ref reader, options, PropId, null))
			{
				continue;
			}

			if (propIndex.TryReadProperty(ref reader, options, PropIndex, null))
			{
				continue;
			}

			if (propPath.TryReadProperty(ref reader, options, PropPath, null))
			{
				continue;
			}

			if (propRouting.TryReadProperty(ref reader, options, PropRouting, null))
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
		return new Elastic.Clients.Elasticsearch.QueryDsl.TermsLookup(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Id = propId.Value,
			Index = propIndex.Value,
			Path = propPath.Value,
			Routing = propRouting.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.QueryDsl.TermsLookup value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropId, value.Id, null, null);
		writer.WriteProperty(options, PropIndex, value.Index, null, null);
		writer.WriteProperty(options, PropPath, value.Path, null, null);
		writer.WriteProperty(options, PropRouting, value.Routing, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.QueryDsl.TermsLookupConverter))]
public sealed partial class TermsLookup
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TermsLookup(Elastic.Clients.Elasticsearch.Id id, Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.Field path)
	{
		Id = id;
		Index = index;
		Path = path;
	}
#if NET7_0_OR_GREATER
	public TermsLookup()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public TermsLookup()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal TermsLookup(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id Id { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.IndexName Index { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Field Path { get; set; }
	public Elastic.Clients.Elasticsearch.Routing? Routing { get; set; }
}

public readonly partial struct TermsLookupDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.TermsLookup Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TermsLookupDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.TermsLookup instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TermsLookupDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.TermsLookup(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.TermsLookupDescriptor<TDocument>(Elastic.Clients.Elasticsearch.QueryDsl.TermsLookup instance) => new Elastic.Clients.Elasticsearch.QueryDsl.TermsLookupDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.TermsLookup(Elastic.Clients.Elasticsearch.QueryDsl.TermsLookupDescriptor<TDocument> descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.QueryDsl.TermsLookupDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.Id = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.TermsLookupDescriptor<TDocument> Index(Elastic.Clients.Elasticsearch.IndexName value)
	{
		Instance.Index = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.TermsLookupDescriptor<TDocument> Path(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Path = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.TermsLookupDescriptor<TDocument> Path(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Path = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.TermsLookupDescriptor<TDocument> Routing(Elastic.Clients.Elasticsearch.Routing? value)
	{
		Instance.Routing = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.TermsLookup Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.TermsLookupDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.TermsLookupDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.QueryDsl.TermsLookup(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct TermsLookupDescriptor
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.TermsLookup Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TermsLookupDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.TermsLookup instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TermsLookupDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.TermsLookup(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.TermsLookupDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.TermsLookup instance) => new Elastic.Clients.Elasticsearch.QueryDsl.TermsLookupDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.TermsLookup(Elastic.Clients.Elasticsearch.QueryDsl.TermsLookupDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.QueryDsl.TermsLookupDescriptor Id(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.Id = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.TermsLookupDescriptor Index(Elastic.Clients.Elasticsearch.IndexName value)
	{
		Instance.Index = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.TermsLookupDescriptor Path(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Path = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.TermsLookupDescriptor Path<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Path = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.TermsLookupDescriptor Routing(Elastic.Clients.Elasticsearch.Routing? value)
	{
		Instance.Routing = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.TermsLookup Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.TermsLookupDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.TermsLookupDescriptor(new Elastic.Clients.Elasticsearch.QueryDsl.TermsLookup(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
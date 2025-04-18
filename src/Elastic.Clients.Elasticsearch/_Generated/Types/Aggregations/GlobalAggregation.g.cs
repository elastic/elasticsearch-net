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

internal sealed partial class GlobalAggregationConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Aggregations.GlobalAggregation>
{
	public override Elastic.Clients.Elasticsearch.Aggregations.GlobalAggregation Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Aggregations.GlobalAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Aggregations.GlobalAggregation value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Aggregations.GlobalAggregationConverter))]
public sealed partial class GlobalAggregation
{
#if NET7_0_OR_GREATER
	public GlobalAggregation()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public GlobalAggregation()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GlobalAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}
}

public readonly partial struct GlobalAggregationDescriptor
{
	internal Elastic.Clients.Elasticsearch.Aggregations.GlobalAggregation Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GlobalAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.GlobalAggregation instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GlobalAggregationDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Aggregations.GlobalAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Aggregations.GlobalAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.GlobalAggregation instance) => new Elastic.Clients.Elasticsearch.Aggregations.GlobalAggregationDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.GlobalAggregation(Elastic.Clients.Elasticsearch.Aggregations.GlobalAggregationDescriptor descriptor) => descriptor.Instance;

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.GlobalAggregation Build(System.Action<Elastic.Clients.Elasticsearch.Aggregations.GlobalAggregationDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Aggregations.GlobalAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Aggregations.GlobalAggregationDescriptor(new Elastic.Clients.Elasticsearch.Aggregations.GlobalAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
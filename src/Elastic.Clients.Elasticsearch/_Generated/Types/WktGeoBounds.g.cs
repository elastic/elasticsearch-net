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

namespace Elastic.Clients.Elasticsearch;

internal sealed partial class WktGeoBoundsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.WktGeoBounds>
{
	private static readonly System.Text.Json.JsonEncodedText PropWkt = System.Text.Json.JsonEncodedText.Encode("wkt");

	public override Elastic.Clients.Elasticsearch.WktGeoBounds Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propWkt = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propWkt.TryReadProperty(ref reader, options, PropWkt, null))
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
		return new Elastic.Clients.Elasticsearch.WktGeoBounds(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Wkt = propWkt.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.WktGeoBounds value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropWkt, value.Wkt, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.WktGeoBoundsConverter))]
public sealed partial class WktGeoBounds
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public WktGeoBounds(string wkt)
	{
		Wkt = wkt;
	}
#if NET7_0_OR_GREATER
	public WktGeoBounds()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public WktGeoBounds()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal WktGeoBounds(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	string Wkt { get; set; }
}

public readonly partial struct WktGeoBoundsDescriptor
{
	internal Elastic.Clients.Elasticsearch.WktGeoBounds Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public WktGeoBoundsDescriptor(Elastic.Clients.Elasticsearch.WktGeoBounds instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public WktGeoBoundsDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.WktGeoBounds(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.WktGeoBoundsDescriptor(Elastic.Clients.Elasticsearch.WktGeoBounds instance) => new Elastic.Clients.Elasticsearch.WktGeoBoundsDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.WktGeoBounds(Elastic.Clients.Elasticsearch.WktGeoBoundsDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.WktGeoBoundsDescriptor Wkt(string value)
	{
		Instance.Wkt = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.WktGeoBounds Build(System.Action<Elastic.Clients.Elasticsearch.WktGeoBoundsDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.WktGeoBoundsDescriptor(new Elastic.Clients.Elasticsearch.WktGeoBounds(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
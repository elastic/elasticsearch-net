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

internal sealed partial class TopLeftBottomRightGeoBoundsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.TopLeftBottomRightGeoBounds>
{
	private static readonly System.Text.Json.JsonEncodedText PropBottomRight = System.Text.Json.JsonEncodedText.Encode("bottom_right");
	private static readonly System.Text.Json.JsonEncodedText PropTopLeft = System.Text.Json.JsonEncodedText.Encode("top_left");

	public override Elastic.Clients.Elasticsearch.TopLeftBottomRightGeoBounds Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.GeoLocation> propBottomRight = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.GeoLocation> propTopLeft = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBottomRight.TryReadProperty(ref reader, options, PropBottomRight, null))
			{
				continue;
			}

			if (propTopLeft.TryReadProperty(ref reader, options, PropTopLeft, null))
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
		return new Elastic.Clients.Elasticsearch.TopLeftBottomRightGeoBounds(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			BottomRight = propBottomRight.Value,
			TopLeft = propTopLeft.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.TopLeftBottomRightGeoBounds value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBottomRight, value.BottomRight, null, null);
		writer.WriteProperty(options, PropTopLeft, value.TopLeft, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.TopLeftBottomRightGeoBoundsConverter))]
public sealed partial class TopLeftBottomRightGeoBounds
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TopLeftBottomRightGeoBounds(Elastic.Clients.Elasticsearch.GeoLocation bottomRight, Elastic.Clients.Elasticsearch.GeoLocation topLeft)
	{
		BottomRight = bottomRight;
		TopLeft = topLeft;
	}
#if NET7_0_OR_GREATER
	public TopLeftBottomRightGeoBounds()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public TopLeftBottomRightGeoBounds()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal TopLeftBottomRightGeoBounds(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.GeoLocation BottomRight { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.GeoLocation TopLeft { get; set; }
}

public readonly partial struct TopLeftBottomRightGeoBoundsDescriptor
{
	internal Elastic.Clients.Elasticsearch.TopLeftBottomRightGeoBounds Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TopLeftBottomRightGeoBoundsDescriptor(Elastic.Clients.Elasticsearch.TopLeftBottomRightGeoBounds instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TopLeftBottomRightGeoBoundsDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.TopLeftBottomRightGeoBounds(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.TopLeftBottomRightGeoBoundsDescriptor(Elastic.Clients.Elasticsearch.TopLeftBottomRightGeoBounds instance) => new Elastic.Clients.Elasticsearch.TopLeftBottomRightGeoBoundsDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.TopLeftBottomRightGeoBounds(Elastic.Clients.Elasticsearch.TopLeftBottomRightGeoBoundsDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.TopLeftBottomRightGeoBoundsDescriptor BottomRight(Elastic.Clients.Elasticsearch.GeoLocation value)
	{
		Instance.BottomRight = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TopLeftBottomRightGeoBoundsDescriptor BottomRight(System.Func<Elastic.Clients.Elasticsearch.GeoLocationFactory, Elastic.Clients.Elasticsearch.GeoLocation> action)
	{
		Instance.BottomRight = Elastic.Clients.Elasticsearch.GeoLocationFactory.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.TopLeftBottomRightGeoBoundsDescriptor TopLeft(Elastic.Clients.Elasticsearch.GeoLocation value)
	{
		Instance.TopLeft = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TopLeftBottomRightGeoBoundsDescriptor TopLeft(System.Func<Elastic.Clients.Elasticsearch.GeoLocationFactory, Elastic.Clients.Elasticsearch.GeoLocation> action)
	{
		Instance.TopLeft = Elastic.Clients.Elasticsearch.GeoLocationFactory.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.TopLeftBottomRightGeoBounds Build(System.Action<Elastic.Clients.Elasticsearch.TopLeftBottomRightGeoBoundsDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.TopLeftBottomRightGeoBoundsDescriptor(new Elastic.Clients.Elasticsearch.TopLeftBottomRightGeoBounds(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
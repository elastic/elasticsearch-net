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

internal sealed partial class CoordsGeoBoundsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.CoordsGeoBounds>
{
	private static readonly System.Text.Json.JsonEncodedText PropBottom = System.Text.Json.JsonEncodedText.Encode("bottom");
	private static readonly System.Text.Json.JsonEncodedText PropLeft = System.Text.Json.JsonEncodedText.Encode("left");
	private static readonly System.Text.Json.JsonEncodedText PropRight = System.Text.Json.JsonEncodedText.Encode("right");
	private static readonly System.Text.Json.JsonEncodedText PropTop = System.Text.Json.JsonEncodedText.Encode("top");

	public override Elastic.Clients.Elasticsearch.CoordsGeoBounds Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<double> propBottom = default;
		LocalJsonValue<double> propLeft = default;
		LocalJsonValue<double> propRight = default;
		LocalJsonValue<double> propTop = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBottom.TryReadProperty(ref reader, options, PropBottom, null))
			{
				continue;
			}

			if (propLeft.TryReadProperty(ref reader, options, PropLeft, null))
			{
				continue;
			}

			if (propRight.TryReadProperty(ref reader, options, PropRight, null))
			{
				continue;
			}

			if (propTop.TryReadProperty(ref reader, options, PropTop, null))
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
		return new Elastic.Clients.Elasticsearch.CoordsGeoBounds(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Bottom = propBottom.Value,
			Left = propLeft.Value,
			Right = propRight.Value,
			Top = propTop.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.CoordsGeoBounds value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBottom, value.Bottom, null, null);
		writer.WriteProperty(options, PropLeft, value.Left, null, null);
		writer.WriteProperty(options, PropRight, value.Right, null, null);
		writer.WriteProperty(options, PropTop, value.Top, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.CoordsGeoBoundsConverter))]
public sealed partial class CoordsGeoBounds
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CoordsGeoBounds(double bottom, double left, double right, double top)
	{
		Bottom = bottom;
		Left = left;
		Right = right;
		Top = top;
	}
#if NET7_0_OR_GREATER
	public CoordsGeoBounds()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public CoordsGeoBounds()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal CoordsGeoBounds(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	double Bottom { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	double Left { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	double Right { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	double Top { get; set; }
}

public readonly partial struct CoordsGeoBoundsDescriptor
{
	internal Elastic.Clients.Elasticsearch.CoordsGeoBounds Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CoordsGeoBoundsDescriptor(Elastic.Clients.Elasticsearch.CoordsGeoBounds instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CoordsGeoBoundsDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.CoordsGeoBounds(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.CoordsGeoBoundsDescriptor(Elastic.Clients.Elasticsearch.CoordsGeoBounds instance) => new Elastic.Clients.Elasticsearch.CoordsGeoBoundsDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.CoordsGeoBounds(Elastic.Clients.Elasticsearch.CoordsGeoBoundsDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.CoordsGeoBoundsDescriptor Bottom(double value)
	{
		Instance.Bottom = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CoordsGeoBoundsDescriptor Left(double value)
	{
		Instance.Left = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CoordsGeoBoundsDescriptor Right(double value)
	{
		Instance.Right = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CoordsGeoBoundsDescriptor Top(double value)
	{
		Instance.Top = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.CoordsGeoBounds Build(System.Action<Elastic.Clients.Elasticsearch.CoordsGeoBoundsDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.CoordsGeoBoundsDescriptor(new Elastic.Clients.Elasticsearch.CoordsGeoBounds(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
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

internal sealed partial class GeoLinePointConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Aggregations.GeoLinePoint>
{
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");

	public override Elastic.Clients.Elasticsearch.Aggregations.GeoLinePoint Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propField.TryReadProperty(ref reader, options, PropField, null))
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
		return new Elastic.Clients.Elasticsearch.Aggregations.GeoLinePoint(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Field = propField.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Aggregations.GeoLinePoint value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropField, value.Field, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Aggregations.GeoLinePointConverter))]
public sealed partial class GeoLinePoint
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoLinePoint(Elastic.Clients.Elasticsearch.Field field)
	{
		Field = field;
	}
#if NET7_0_OR_GREATER
	public GeoLinePoint()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public GeoLinePoint()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GeoLinePoint(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The name of the geo_point field.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Field Field { get; set; }
}

public readonly partial struct GeoLinePointDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Aggregations.GeoLinePoint Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoLinePointDescriptor(Elastic.Clients.Elasticsearch.Aggregations.GeoLinePoint instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoLinePointDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Aggregations.GeoLinePoint(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Aggregations.GeoLinePointDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Aggregations.GeoLinePoint instance) => new Elastic.Clients.Elasticsearch.Aggregations.GeoLinePointDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.GeoLinePoint(Elastic.Clients.Elasticsearch.Aggregations.GeoLinePointDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The name of the geo_point field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeoLinePointDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the geo_point field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeoLinePointDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.GeoLinePoint Build(System.Action<Elastic.Clients.Elasticsearch.Aggregations.GeoLinePointDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Aggregations.GeoLinePointDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Aggregations.GeoLinePoint(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct GeoLinePointDescriptor
{
	internal Elastic.Clients.Elasticsearch.Aggregations.GeoLinePoint Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoLinePointDescriptor(Elastic.Clients.Elasticsearch.Aggregations.GeoLinePoint instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GeoLinePointDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Aggregations.GeoLinePoint(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Aggregations.GeoLinePointDescriptor(Elastic.Clients.Elasticsearch.Aggregations.GeoLinePoint instance) => new Elastic.Clients.Elasticsearch.Aggregations.GeoLinePointDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.GeoLinePoint(Elastic.Clients.Elasticsearch.Aggregations.GeoLinePointDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The name of the geo_point field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeoLinePointDescriptor Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the geo_point field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.GeoLinePointDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.GeoLinePoint Build(System.Action<Elastic.Clients.Elasticsearch.Aggregations.GeoLinePointDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Aggregations.GeoLinePointDescriptor(new Elastic.Clients.Elasticsearch.Aggregations.GeoLinePoint(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
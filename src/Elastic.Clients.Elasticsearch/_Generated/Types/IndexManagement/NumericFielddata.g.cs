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

namespace Elastic.Clients.Elasticsearch.IndexManagement;

internal sealed partial class NumericFielddataConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.NumericFielddata>
{
	private static readonly System.Text.Json.JsonEncodedText PropFormat = System.Text.Json.JsonEncodedText.Encode("format");

	public override Elastic.Clients.Elasticsearch.IndexManagement.NumericFielddata Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.NumericFielddataFormat> propFormat = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propFormat.TryReadProperty(ref reader, options, PropFormat, null))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.NumericFielddata(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Format = propFormat.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.NumericFielddata value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropFormat, value.Format, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.NumericFielddataConverter))]
public sealed partial class NumericFielddata
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public NumericFielddata(Elastic.Clients.Elasticsearch.IndexManagement.NumericFielddataFormat format)
	{
		Format = format;
	}
#if NET7_0_OR_GREATER
	public NumericFielddata()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public NumericFielddata()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal NumericFielddata(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.IndexManagement.NumericFielddataFormat Format { get; set; }
}

public readonly partial struct NumericFielddataDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.NumericFielddata Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public NumericFielddataDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.NumericFielddata instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public NumericFielddataDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.NumericFielddata(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.NumericFielddataDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.NumericFielddata instance) => new Elastic.Clients.Elasticsearch.IndexManagement.NumericFielddataDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.NumericFielddata(Elastic.Clients.Elasticsearch.IndexManagement.NumericFielddataDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.IndexManagement.NumericFielddataDescriptor Format(Elastic.Clients.Elasticsearch.IndexManagement.NumericFielddataFormat value)
	{
		Instance.Format = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.NumericFielddata Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.NumericFielddataDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.NumericFielddataDescriptor(new Elastic.Clients.Elasticsearch.IndexManagement.NumericFielddata(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
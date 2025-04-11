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

namespace Elastic.Clients.Elasticsearch.Cluster;

internal sealed partial class NodePackagingTypeConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Cluster.NodePackagingType>
{
	private static readonly System.Text.Json.JsonEncodedText PropCount = System.Text.Json.JsonEncodedText.Encode("count");
	private static readonly System.Text.Json.JsonEncodedText PropFlavor = System.Text.Json.JsonEncodedText.Encode("flavor");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");

	public override Elastic.Clients.Elasticsearch.Cluster.NodePackagingType Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int> propCount = default;
		LocalJsonValue<string> propFlavor = default;
		LocalJsonValue<string> propType = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCount.TryReadProperty(ref reader, options, PropCount, null))
			{
				continue;
			}

			if (propFlavor.TryReadProperty(ref reader, options, PropFlavor, null))
			{
				continue;
			}

			if (propType.TryReadProperty(ref reader, options, PropType, null))
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
		return new Elastic.Clients.Elasticsearch.Cluster.NodePackagingType(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Count = propCount.Value,
			Flavor = propFlavor.Value,
			Type = propType.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Cluster.NodePackagingType value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCount, value.Count, null, null);
		writer.WriteProperty(options, PropFlavor, value.Flavor, null, null);
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Cluster.NodePackagingTypeConverter))]
public sealed partial class NodePackagingType
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public NodePackagingType(int count, string flavor, string type)
	{
		Count = count;
		Flavor = flavor;
		Type = type;
	}
#if NET7_0_OR_GREATER
	public NodePackagingType()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public NodePackagingType()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal NodePackagingType(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Number of selected nodes using the distribution flavor and file type.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int Count { get; set; }

	/// <summary>
	/// <para>
	/// Type of Elasticsearch distribution. This is always <c>default</c>.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Flavor { get; set; }

	/// <summary>
	/// <para>
	/// File type (such as <c>tar</c> or <c>zip</c>) used for the distribution package.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Type { get; set; }
}
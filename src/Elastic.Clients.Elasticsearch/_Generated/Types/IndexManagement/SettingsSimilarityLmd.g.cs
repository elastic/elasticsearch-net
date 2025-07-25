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

internal sealed partial class SettingsSimilarityLmdConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityLmd>
{
	private static readonly System.Text.Json.JsonEncodedText PropMu = System.Text.Json.JsonEncodedText.Encode("mu");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");

	public override Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityLmd Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<double?> propMu = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propMu.TryReadProperty(ref reader, options, PropMu, static double? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<double>(o)))
			{
				continue;
			}

			if (reader.ValueTextEquals(PropType))
			{
				reader.Skip();
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityLmd(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Mu = propMu.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityLmd value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropMu, value.Mu, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, double? v) => w.WriteNullableValue<double>(o, v));
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityLmdConverter))]
public sealed partial class SettingsSimilarityLmd : Elastic.Clients.Elasticsearch.IndexManagement.ISettingsSimilarity
{
#if NET7_0_OR_GREATER
	public SettingsSimilarityLmd()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public SettingsSimilarityLmd()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal SettingsSimilarityLmd(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public double? Mu { get; set; }

	public string Type => "LMDirichlet";
}

public readonly partial struct SettingsSimilarityLmdDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityLmd Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SettingsSimilarityLmdDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityLmd instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SettingsSimilarityLmdDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityLmd(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityLmdDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityLmd instance) => new Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityLmdDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityLmd(Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityLmdDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityLmdDescriptor Mu(double? value)
	{
		Instance.Mu = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityLmd Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityLmdDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityLmd(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityLmdDescriptor(new Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityLmd(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
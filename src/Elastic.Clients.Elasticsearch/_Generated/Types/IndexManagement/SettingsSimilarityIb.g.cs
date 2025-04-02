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

internal sealed partial class SettingsSimilarityIbConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityIb>
{
	private static readonly System.Text.Json.JsonEncodedText PropDistribution = System.Text.Json.JsonEncodedText.Encode("distribution");
	private static readonly System.Text.Json.JsonEncodedText PropLambda = System.Text.Json.JsonEncodedText.Encode("lambda");
	private static readonly System.Text.Json.JsonEncodedText PropNormalization = System.Text.Json.JsonEncodedText.Encode("normalization");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");

	public override Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityIb Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.IBDistribution> propDistribution = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IBLambda> propLambda = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Normalization> propNormalization = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDistribution.TryReadProperty(ref reader, options, PropDistribution, null))
			{
				continue;
			}

			if (propLambda.TryReadProperty(ref reader, options, PropLambda, null))
			{
				continue;
			}

			if (propNormalization.TryReadProperty(ref reader, options, PropNormalization, null))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityIb(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Distribution = propDistribution.Value,
			Lambda = propLambda.Value,
			Normalization = propNormalization.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityIb value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDistribution, value.Distribution, null, null);
		writer.WriteProperty(options, PropLambda, value.Lambda, null, null);
		writer.WriteProperty(options, PropNormalization, value.Normalization, null, null);
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityIbConverter))]
public sealed partial class SettingsSimilarityIb : Elastic.Clients.Elasticsearch.IndexManagement.ISettingsSimilarity
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SettingsSimilarityIb(Elastic.Clients.Elasticsearch.IBDistribution distribution, Elastic.Clients.Elasticsearch.IBLambda lambda, Elastic.Clients.Elasticsearch.Normalization normalization)
	{
		Distribution = distribution;
		Lambda = lambda;
		Normalization = normalization;
	}
#if NET7_0_OR_GREATER
	public SettingsSimilarityIb()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public SettingsSimilarityIb()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal SettingsSimilarityIb(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.IBDistribution Distribution { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.IBLambda Lambda { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Normalization Normalization { get; set; }

	public string Type => "IB";
}

public readonly partial struct SettingsSimilarityIbDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityIb Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SettingsSimilarityIbDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityIb instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SettingsSimilarityIbDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityIb(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityIbDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityIb instance) => new Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityIbDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityIb(Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityIbDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityIbDescriptor Distribution(Elastic.Clients.Elasticsearch.IBDistribution value)
	{
		Instance.Distribution = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityIbDescriptor Lambda(Elastic.Clients.Elasticsearch.IBLambda value)
	{
		Instance.Lambda = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityIbDescriptor Normalization(Elastic.Clients.Elasticsearch.Normalization value)
	{
		Instance.Normalization = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityIb Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityIbDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityIbDescriptor(new Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityIb(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
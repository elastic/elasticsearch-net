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

internal sealed partial class SettingsSimilarityDfrConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityDfr>
{
	private static readonly System.Text.Json.JsonEncodedText PropAfterEffect = System.Text.Json.JsonEncodedText.Encode("after_effect");
	private static readonly System.Text.Json.JsonEncodedText PropBasicModel = System.Text.Json.JsonEncodedText.Encode("basic_model");
	private static readonly System.Text.Json.JsonEncodedText PropNormalization = System.Text.Json.JsonEncodedText.Encode("normalization");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");

	public override Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityDfr Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.DFRAfterEffect> propAfterEffect = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.DFRBasicModel> propBasicModel = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Normalization> propNormalization = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAfterEffect.TryReadProperty(ref reader, options, PropAfterEffect, null))
			{
				continue;
			}

			if (propBasicModel.TryReadProperty(ref reader, options, PropBasicModel, null))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityDfr(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AfterEffect = propAfterEffect.Value,
			BasicModel = propBasicModel.Value,
			Normalization = propNormalization.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityDfr value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAfterEffect, value.AfterEffect, null, null);
		writer.WriteProperty(options, PropBasicModel, value.BasicModel, null, null);
		writer.WriteProperty(options, PropNormalization, value.Normalization, null, null);
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityDfrConverter))]
public sealed partial class SettingsSimilarityDfr : Elastic.Clients.Elasticsearch.IndexManagement.ISettingsSimilarity
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SettingsSimilarityDfr(Elastic.Clients.Elasticsearch.DFRAfterEffect afterEffect, Elastic.Clients.Elasticsearch.DFRBasicModel basicModel, Elastic.Clients.Elasticsearch.Normalization normalization)
	{
		AfterEffect = afterEffect;
		BasicModel = basicModel;
		Normalization = normalization;
	}
#if NET7_0_OR_GREATER
	public SettingsSimilarityDfr()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public SettingsSimilarityDfr()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal SettingsSimilarityDfr(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.DFRAfterEffect AfterEffect { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.DFRBasicModel BasicModel { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Normalization Normalization { get; set; }

	public string Type => "DFR";
}

public readonly partial struct SettingsSimilarityDfrDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityDfr Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SettingsSimilarityDfrDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityDfr instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SettingsSimilarityDfrDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityDfr(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityDfrDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityDfr instance) => new Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityDfrDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityDfr(Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityDfrDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityDfrDescriptor AfterEffect(Elastic.Clients.Elasticsearch.DFRAfterEffect value)
	{
		Instance.AfterEffect = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityDfrDescriptor BasicModel(Elastic.Clients.Elasticsearch.DFRBasicModel value)
	{
		Instance.BasicModel = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityDfrDescriptor Normalization(Elastic.Clients.Elasticsearch.Normalization value)
	{
		Instance.Normalization = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityDfr Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityDfrDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityDfrDescriptor(new Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityDfr(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
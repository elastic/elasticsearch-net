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

internal sealed partial class SettingsSimilarityScriptedConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityScripted>
{
	private static readonly System.Text.Json.JsonEncodedText PropScript = System.Text.Json.JsonEncodedText.Encode("script");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");
	private static readonly System.Text.Json.JsonEncodedText PropWeightScript = System.Text.Json.JsonEncodedText.Encode("weight_script");

	public override Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityScripted Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Script> propScript = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Script?> propWeightScript = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propScript.TryReadProperty(ref reader, options, PropScript, null))
			{
				continue;
			}

			if (reader.ValueTextEquals(PropType))
			{
				reader.Skip();
				continue;
			}

			if (propWeightScript.TryReadProperty(ref reader, options, PropWeightScript, null))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityScripted(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Script = propScript.Value,
			WeightScript = propWeightScript.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityScripted value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropScript, value.Script, null, null);
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteProperty(options, PropWeightScript, value.WeightScript, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityScriptedConverter))]
public sealed partial class SettingsSimilarityScripted : Elastic.Clients.Elasticsearch.IndexManagement.ISettingsSimilarity
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SettingsSimilarityScripted(Elastic.Clients.Elasticsearch.Script script)
	{
		Script = script;
	}
#if NET7_0_OR_GREATER
	public SettingsSimilarityScripted()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public SettingsSimilarityScripted()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal SettingsSimilarityScripted(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Script Script { get; set; }

	public string Type => "scripted";

	public Elastic.Clients.Elasticsearch.Script? WeightScript { get; set; }
}

public readonly partial struct SettingsSimilarityScriptedDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityScripted Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SettingsSimilarityScriptedDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityScripted instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SettingsSimilarityScriptedDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityScripted(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityScriptedDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityScripted instance) => new Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityScriptedDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityScripted(Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityScriptedDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityScriptedDescriptor Script(Elastic.Clients.Elasticsearch.Script value)
	{
		Instance.Script = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityScriptedDescriptor Script()
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityScriptedDescriptor Script(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityScriptedDescriptor WeightScript(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.WeightScript = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityScriptedDescriptor WeightScript()
	{
		Instance.WeightScript = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityScriptedDescriptor WeightScript(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.WeightScript = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityScripted Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityScriptedDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityScriptedDescriptor(new Elastic.Clients.Elasticsearch.IndexManagement.SettingsSimilarityScripted(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
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

namespace Elastic.Clients.Elasticsearch.Inference;

internal sealed partial class ElasticsearchTaskSettingsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Inference.ElasticsearchTaskSettings>
{
	private static readonly System.Text.Json.JsonEncodedText PropReturnDocuments = System.Text.Json.JsonEncodedText.Encode("return_documents");

	public override Elastic.Clients.Elasticsearch.Inference.ElasticsearchTaskSettings Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool?> propReturnDocuments = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propReturnDocuments.TryReadProperty(ref reader, options, PropReturnDocuments, static bool? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<bool>(o)))
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
		return new Elastic.Clients.Elasticsearch.Inference.ElasticsearchTaskSettings(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ReturnDocuments = propReturnDocuments.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Inference.ElasticsearchTaskSettings value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropReturnDocuments, value.ReturnDocuments, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, bool? v) => w.WriteNullableValue<bool>(o, v));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Inference.ElasticsearchTaskSettingsConverter))]
public sealed partial class ElasticsearchTaskSettings
{
#if NET7_0_OR_GREATER
	public ElasticsearchTaskSettings()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public ElasticsearchTaskSettings()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ElasticsearchTaskSettings(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// For a <c>rerank</c> task, return the document instead of only the index.
	/// </para>
	/// </summary>
	public bool? ReturnDocuments { get; set; }
}

public readonly partial struct ElasticsearchTaskSettingsDescriptor
{
	internal Elastic.Clients.Elasticsearch.Inference.ElasticsearchTaskSettings Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ElasticsearchTaskSettingsDescriptor(Elastic.Clients.Elasticsearch.Inference.ElasticsearchTaskSettings instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ElasticsearchTaskSettingsDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Inference.ElasticsearchTaskSettings(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Inference.ElasticsearchTaskSettingsDescriptor(Elastic.Clients.Elasticsearch.Inference.ElasticsearchTaskSettings instance) => new Elastic.Clients.Elasticsearch.Inference.ElasticsearchTaskSettingsDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Inference.ElasticsearchTaskSettings(Elastic.Clients.Elasticsearch.Inference.ElasticsearchTaskSettingsDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// For a <c>rerank</c> task, return the document instead of only the index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.ElasticsearchTaskSettingsDescriptor ReturnDocuments(bool? value = true)
	{
		Instance.ReturnDocuments = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Inference.ElasticsearchTaskSettings Build(System.Action<Elastic.Clients.Elasticsearch.Inference.ElasticsearchTaskSettingsDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Inference.ElasticsearchTaskSettings(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Inference.ElasticsearchTaskSettingsDescriptor(new Elastic.Clients.Elasticsearch.Inference.ElasticsearchTaskSettings(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
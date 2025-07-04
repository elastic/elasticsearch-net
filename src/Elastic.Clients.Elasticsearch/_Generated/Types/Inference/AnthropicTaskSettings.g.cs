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

internal sealed partial class AnthropicTaskSettingsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Inference.AnthropicTaskSettings>
{
	private static readonly System.Text.Json.JsonEncodedText PropMaxTokens = System.Text.Json.JsonEncodedText.Encode("max_tokens");
	private static readonly System.Text.Json.JsonEncodedText PropTemperature = System.Text.Json.JsonEncodedText.Encode("temperature");
	private static readonly System.Text.Json.JsonEncodedText PropTopK = System.Text.Json.JsonEncodedText.Encode("top_k");
	private static readonly System.Text.Json.JsonEncodedText PropTopP = System.Text.Json.JsonEncodedText.Encode("top_p");

	public override Elastic.Clients.Elasticsearch.Inference.AnthropicTaskSettings Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int> propMaxTokens = default;
		LocalJsonValue<float?> propTemperature = default;
		LocalJsonValue<int?> propTopK = default;
		LocalJsonValue<float?> propTopP = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propMaxTokens.TryReadProperty(ref reader, options, PropMaxTokens, null))
			{
				continue;
			}

			if (propTemperature.TryReadProperty(ref reader, options, PropTemperature, static float? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<float>(o)))
			{
				continue;
			}

			if (propTopK.TryReadProperty(ref reader, options, PropTopK, static int? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<int>(o)))
			{
				continue;
			}

			if (propTopP.TryReadProperty(ref reader, options, PropTopP, static float? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<float>(o)))
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
		return new Elastic.Clients.Elasticsearch.Inference.AnthropicTaskSettings(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			MaxTokens = propMaxTokens.Value,
			Temperature = propTemperature.Value,
			TopK = propTopK.Value,
			TopP = propTopP.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Inference.AnthropicTaskSettings value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropMaxTokens, value.MaxTokens, null, null);
		writer.WriteProperty(options, PropTemperature, value.Temperature, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, float? v) => w.WriteNullableValue<float>(o, v));
		writer.WriteProperty(options, PropTopK, value.TopK, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, int? v) => w.WriteNullableValue<int>(o, v));
		writer.WriteProperty(options, PropTopP, value.TopP, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, float? v) => w.WriteNullableValue<float>(o, v));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Inference.AnthropicTaskSettingsConverter))]
public sealed partial class AnthropicTaskSettings
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AnthropicTaskSettings(int maxTokens)
	{
		MaxTokens = maxTokens;
	}
#if NET7_0_OR_GREATER
	public AnthropicTaskSettings()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public AnthropicTaskSettings()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal AnthropicTaskSettings(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// For a <c>completion</c> task, it is the maximum number of tokens to generate before stopping.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int MaxTokens { get; set; }

	/// <summary>
	/// <para>
	/// For a <c>completion</c> task, it is the amount of randomness injected into the response.
	/// For more details about the supported range, refer to Anthropic documentation.
	/// </para>
	/// </summary>
	public float? Temperature { get; set; }

	/// <summary>
	/// <para>
	/// For a <c>completion</c> task, it specifies to only sample from the top K options for each subsequent token.
	/// It is recommended for advanced use cases only.
	/// You usually only need to use <c>temperature</c>.
	/// </para>
	/// </summary>
	public int? TopK { get; set; }

	/// <summary>
	/// <para>
	/// For a <c>completion</c> task, it specifies to use Anthropic's nucleus sampling.
	/// In nucleus sampling, Anthropic computes the cumulative distribution over all the options for each subsequent token in decreasing probability order and cuts it off once it reaches the specified probability.
	/// You should either alter <c>temperature</c> or <c>top_p</c>, but not both.
	/// It is recommended for advanced use cases only.
	/// You usually only need to use <c>temperature</c>.
	/// </para>
	/// </summary>
	public float? TopP { get; set; }
}

public readonly partial struct AnthropicTaskSettingsDescriptor
{
	internal Elastic.Clients.Elasticsearch.Inference.AnthropicTaskSettings Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AnthropicTaskSettingsDescriptor(Elastic.Clients.Elasticsearch.Inference.AnthropicTaskSettings instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AnthropicTaskSettingsDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Inference.AnthropicTaskSettings(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Inference.AnthropicTaskSettingsDescriptor(Elastic.Clients.Elasticsearch.Inference.AnthropicTaskSettings instance) => new Elastic.Clients.Elasticsearch.Inference.AnthropicTaskSettingsDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Inference.AnthropicTaskSettings(Elastic.Clients.Elasticsearch.Inference.AnthropicTaskSettingsDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// For a <c>completion</c> task, it is the maximum number of tokens to generate before stopping.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.AnthropicTaskSettingsDescriptor MaxTokens(int value)
	{
		Instance.MaxTokens = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// For a <c>completion</c> task, it is the amount of randomness injected into the response.
	/// For more details about the supported range, refer to Anthropic documentation.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.AnthropicTaskSettingsDescriptor Temperature(float? value)
	{
		Instance.Temperature = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// For a <c>completion</c> task, it specifies to only sample from the top K options for each subsequent token.
	/// It is recommended for advanced use cases only.
	/// You usually only need to use <c>temperature</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.AnthropicTaskSettingsDescriptor TopK(int? value)
	{
		Instance.TopK = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// For a <c>completion</c> task, it specifies to use Anthropic's nucleus sampling.
	/// In nucleus sampling, Anthropic computes the cumulative distribution over all the options for each subsequent token in decreasing probability order and cuts it off once it reaches the specified probability.
	/// You should either alter <c>temperature</c> or <c>top_p</c>, but not both.
	/// It is recommended for advanced use cases only.
	/// You usually only need to use <c>temperature</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.AnthropicTaskSettingsDescriptor TopP(float? value)
	{
		Instance.TopP = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Inference.AnthropicTaskSettings Build(System.Action<Elastic.Clients.Elasticsearch.Inference.AnthropicTaskSettingsDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Inference.AnthropicTaskSettingsDescriptor(new Elastic.Clients.Elasticsearch.Inference.AnthropicTaskSettings(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
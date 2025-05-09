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

namespace Elastic.Clients.Elasticsearch.MachineLearning;

internal sealed partial class ChunkingConfigConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.ChunkingConfig>
{
	private static readonly System.Text.Json.JsonEncodedText PropMode = System.Text.Json.JsonEncodedText.Encode("mode");
	private static readonly System.Text.Json.JsonEncodedText PropTimeSpan = System.Text.Json.JsonEncodedText.Encode("time_span");

	public override Elastic.Clients.Elasticsearch.MachineLearning.ChunkingConfig Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.ChunkingMode> propMode = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propTimeSpan = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propMode.TryReadProperty(ref reader, options, PropMode, null))
			{
				continue;
			}

			if (propTimeSpan.TryReadProperty(ref reader, options, PropTimeSpan, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.ChunkingConfig(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Mode = propMode.Value,
			TimeSpan = propTimeSpan.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.ChunkingConfig value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropMode, value.Mode, null, null);
		writer.WriteProperty(options, PropTimeSpan, value.TimeSpan, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.ChunkingConfigConverter))]
public sealed partial class ChunkingConfig
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ChunkingConfig(Elastic.Clients.Elasticsearch.MachineLearning.ChunkingMode mode)
	{
		Mode = mode;
	}
#if NET7_0_OR_GREATER
	public ChunkingConfig()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public ChunkingConfig()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ChunkingConfig(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// If the mode is <c>auto</c>, the chunk size is dynamically calculated;
	/// this is the recommended value when the datafeed does not use aggregations.
	/// If the mode is <c>manual</c>, chunking is applied according to the specified <c>time_span</c>;
	/// use this mode when the datafeed uses aggregations. If the mode is <c>off</c>, no chunking is applied.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.MachineLearning.ChunkingMode Mode { get; set; }

	/// <summary>
	/// <para>
	/// The time span that each search will be querying. This setting is applicable only when the <c>mode</c> is set to <c>manual</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? TimeSpan { get; set; }
}

public readonly partial struct ChunkingConfigDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.ChunkingConfig Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ChunkingConfigDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.ChunkingConfig instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ChunkingConfigDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.ChunkingConfig(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.ChunkingConfigDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.ChunkingConfig instance) => new Elastic.Clients.Elasticsearch.MachineLearning.ChunkingConfigDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.ChunkingConfig(Elastic.Clients.Elasticsearch.MachineLearning.ChunkingConfigDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// If the mode is <c>auto</c>, the chunk size is dynamically calculated;
	/// this is the recommended value when the datafeed does not use aggregations.
	/// If the mode is <c>manual</c>, chunking is applied according to the specified <c>time_span</c>;
	/// use this mode when the datafeed uses aggregations. If the mode is <c>off</c>, no chunking is applied.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.ChunkingConfigDescriptor Mode(Elastic.Clients.Elasticsearch.MachineLearning.ChunkingMode value)
	{
		Instance.Mode = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The time span that each search will be querying. This setting is applicable only when the <c>mode</c> is set to <c>manual</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.ChunkingConfigDescriptor TimeSpan(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.TimeSpan = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.ChunkingConfig Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.ChunkingConfigDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.ChunkingConfigDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.ChunkingConfig(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
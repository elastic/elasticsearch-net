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

internal sealed partial class DownsamplingRoundConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.DownsamplingRound>
{
	private static readonly System.Text.Json.JsonEncodedText PropAfter = System.Text.Json.JsonEncodedText.Encode("after");
	private static readonly System.Text.Json.JsonEncodedText PropConfig = System.Text.Json.JsonEncodedText.Encode("config");

	public override Elastic.Clients.Elasticsearch.IndexManagement.DownsamplingRound Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration> propAfter = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.DownsampleConfig> propConfig = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAfter.TryReadProperty(ref reader, options, PropAfter, null))
			{
				continue;
			}

			if (propConfig.TryReadProperty(ref reader, options, PropConfig, null))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.DownsamplingRound(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			After = propAfter.Value,
			Config = propConfig.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.DownsamplingRound value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAfter, value.After, null, null);
		writer.WriteProperty(options, PropConfig, value.Config, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.DownsamplingRoundConverter))]
public sealed partial class DownsamplingRound
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DownsamplingRound(Elastic.Clients.Elasticsearch.Duration after, Elastic.Clients.Elasticsearch.IndexManagement.DownsampleConfig config)
	{
		After = after;
		Config = config;
	}
#if NET7_0_OR_GREATER
	public DownsamplingRound()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public DownsamplingRound()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DownsamplingRound(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The duration since rollover when this downsampling round should execute
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Duration After { get; set; }

	/// <summary>
	/// <para>
	/// The downsample configuration to execute.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.IndexManagement.DownsampleConfig Config { get; set; }
}

public readonly partial struct DownsamplingRoundDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.DownsamplingRound Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DownsamplingRoundDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.DownsamplingRound instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DownsamplingRoundDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.DownsamplingRound(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.DownsamplingRoundDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.DownsamplingRound instance) => new Elastic.Clients.Elasticsearch.IndexManagement.DownsamplingRoundDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.DownsamplingRound(Elastic.Clients.Elasticsearch.IndexManagement.DownsamplingRoundDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The duration since rollover when this downsampling round should execute
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.DownsamplingRoundDescriptor After(Elastic.Clients.Elasticsearch.Duration value)
	{
		Instance.After = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The downsample configuration to execute.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.DownsamplingRoundDescriptor Config(Elastic.Clients.Elasticsearch.IndexManagement.DownsampleConfig value)
	{
		Instance.Config = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The downsample configuration to execute.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.DownsamplingRoundDescriptor Config(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.DownsampleConfigDescriptor> action)
	{
		Instance.Config = Elastic.Clients.Elasticsearch.IndexManagement.DownsampleConfigDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.DownsamplingRound Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.DownsamplingRoundDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.DownsamplingRoundDescriptor(new Elastic.Clients.Elasticsearch.IndexManagement.DownsamplingRound(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
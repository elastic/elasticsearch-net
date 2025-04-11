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

namespace Elastic.Clients.Elasticsearch.QueryDsl;

internal sealed partial class RankFeatureFunctionLogarithmConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionLogarithm>
{
	private static readonly System.Text.Json.JsonEncodedText PropScalingFactor = System.Text.Json.JsonEncodedText.Encode("scaling_factor");

	public override Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionLogarithm Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<float> propScalingFactor = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propScalingFactor.TryReadProperty(ref reader, options, PropScalingFactor, null))
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
		return new Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionLogarithm(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ScalingFactor = propScalingFactor.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionLogarithm value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropScalingFactor, value.ScalingFactor, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionLogarithmConverter))]
public sealed partial class RankFeatureFunctionLogarithm
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RankFeatureFunctionLogarithm(float scalingFactor)
	{
		ScalingFactor = scalingFactor;
	}
#if NET7_0_OR_GREATER
	public RankFeatureFunctionLogarithm()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public RankFeatureFunctionLogarithm()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal RankFeatureFunctionLogarithm(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Configurable scaling factor.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	float ScalingFactor { get; set; }
}

public readonly partial struct RankFeatureFunctionLogarithmDescriptor
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionLogarithm Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RankFeatureFunctionLogarithmDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionLogarithm instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RankFeatureFunctionLogarithmDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionLogarithm(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionLogarithmDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionLogarithm instance) => new Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionLogarithmDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionLogarithm(Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionLogarithmDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Configurable scaling factor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionLogarithmDescriptor ScalingFactor(float value)
	{
		Instance.ScalingFactor = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionLogarithm Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionLogarithmDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionLogarithmDescriptor(new Elastic.Clients.Elasticsearch.QueryDsl.RankFeatureFunctionLogarithm(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
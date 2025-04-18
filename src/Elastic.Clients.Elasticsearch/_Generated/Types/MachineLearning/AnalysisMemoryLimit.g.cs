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

internal sealed partial class AnalysisMemoryLimitConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.AnalysisMemoryLimit>
{
	private static readonly System.Text.Json.JsonEncodedText PropModelMemoryLimit = System.Text.Json.JsonEncodedText.Encode("model_memory_limit");

	public override Elastic.Clients.Elasticsearch.MachineLearning.AnalysisMemoryLimit Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propModelMemoryLimit = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propModelMemoryLimit.TryReadProperty(ref reader, options, PropModelMemoryLimit, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.AnalysisMemoryLimit(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ModelMemoryLimit = propModelMemoryLimit.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.AnalysisMemoryLimit value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropModelMemoryLimit, value.ModelMemoryLimit, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.AnalysisMemoryLimitConverter))]
public sealed partial class AnalysisMemoryLimit
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AnalysisMemoryLimit(string modelMemoryLimit)
	{
		ModelMemoryLimit = modelMemoryLimit;
	}
#if NET7_0_OR_GREATER
	public AnalysisMemoryLimit()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public AnalysisMemoryLimit()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal AnalysisMemoryLimit(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Limits can be applied for the resources required to hold the mathematical models in memory. These limits are approximate and can be set per job. They do not control the memory used by other processes, for example the Elasticsearch Java processes.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string ModelMemoryLimit { get; set; }
}

public readonly partial struct AnalysisMemoryLimitDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.AnalysisMemoryLimit Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AnalysisMemoryLimitDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.AnalysisMemoryLimit instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AnalysisMemoryLimitDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.AnalysisMemoryLimit(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.AnalysisMemoryLimitDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.AnalysisMemoryLimit instance) => new Elastic.Clients.Elasticsearch.MachineLearning.AnalysisMemoryLimitDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.AnalysisMemoryLimit(Elastic.Clients.Elasticsearch.MachineLearning.AnalysisMemoryLimitDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Limits can be applied for the resources required to hold the mathematical models in memory. These limits are approximate and can be set per job. They do not control the memory used by other processes, for example the Elasticsearch Java processes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.AnalysisMemoryLimitDescriptor ModelMemoryLimit(string value)
	{
		Instance.ModelMemoryLimit = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.AnalysisMemoryLimit Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.AnalysisMemoryLimitDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.AnalysisMemoryLimitDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.AnalysisMemoryLimit(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
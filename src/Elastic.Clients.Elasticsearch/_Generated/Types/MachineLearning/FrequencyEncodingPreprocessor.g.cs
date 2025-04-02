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

internal sealed partial class FrequencyEncodingPreprocessorConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessor>
{
	private static readonly System.Text.Json.JsonEncodedText PropFeatureName = System.Text.Json.JsonEncodedText.Encode("feature_name");
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");
	private static readonly System.Text.Json.JsonEncodedText PropFrequencyMap = System.Text.Json.JsonEncodedText.Encode("frequency_map");

	public override Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessor Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propFeatureName = default;
		LocalJsonValue<string> propField = default;
		LocalJsonValue<System.Collections.Generic.IDictionary<string, double>> propFrequencyMap = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propFeatureName.TryReadProperty(ref reader, options, PropFeatureName, null))
			{
				continue;
			}

			if (propField.TryReadProperty(ref reader, options, PropField, null))
			{
				continue;
			}

			if (propFrequencyMap.TryReadProperty(ref reader, options, PropFrequencyMap, static System.Collections.Generic.IDictionary<string, double> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, double>(o, null, null)!))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			FeatureName = propFeatureName.Value,
			Field = propField.Value,
			FrequencyMap = propFrequencyMap.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessor value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropFeatureName, value.FeatureName, null, null);
		writer.WriteProperty(options, PropField, value.Field, null, null);
		writer.WriteProperty(options, PropFrequencyMap, value.FrequencyMap, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, double> v) => w.WriteDictionaryValue<string, double>(o, v, null, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessorConverter))]
public sealed partial class FrequencyEncodingPreprocessor
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FrequencyEncodingPreprocessor(string featureName, string field, System.Collections.Generic.IDictionary<string, double> frequencyMap)
	{
		FeatureName = featureName;
		Field = field;
		FrequencyMap = frequencyMap;
	}
#if NET7_0_OR_GREATER
	public FrequencyEncodingPreprocessor()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public FrequencyEncodingPreprocessor()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal FrequencyEncodingPreprocessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	string FeatureName { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Field { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IDictionary<string, double> FrequencyMap { get; set; }
}

public readonly partial struct FrequencyEncodingPreprocessorDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessor Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FrequencyEncodingPreprocessorDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessor instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FrequencyEncodingPreprocessorDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessorDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessor instance) => new Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessorDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessor(Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessorDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessorDescriptor FeatureName(string value)
	{
		Instance.FeatureName = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessorDescriptor Field(string value)
	{
		Instance.Field = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessorDescriptor FrequencyMap(System.Collections.Generic.IDictionary<string, double> value)
	{
		Instance.FrequencyMap = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessorDescriptor FrequencyMap()
	{
		Instance.FrequencyMap = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringDouble.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessorDescriptor FrequencyMap(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringDouble>? action)
	{
		Instance.FrequencyMap = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringDouble.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessorDescriptor AddFrequencyMap(string key, double value)
	{
		Instance.FrequencyMap ??= new System.Collections.Generic.Dictionary<string, double>();
		Instance.FrequencyMap.Add(key, value);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessor Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessorDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessorDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
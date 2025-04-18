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

internal sealed partial class OneHotEncodingPreprocessorConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessor>
{
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");
	private static readonly System.Text.Json.JsonEncodedText PropHotMap = System.Text.Json.JsonEncodedText.Encode("hot_map");

	public override Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessor Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propField = default;
		LocalJsonValue<System.Collections.Generic.IDictionary<string, string>> propHotMap = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propField.TryReadProperty(ref reader, options, PropField, null))
			{
				continue;
			}

			if (propHotMap.TryReadProperty(ref reader, options, PropHotMap, static System.Collections.Generic.IDictionary<string, string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, string>(o, null, null)!))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Field = propField.Value,
			HotMap = propHotMap.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessor value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropField, value.Field, null, null);
		writer.WriteProperty(options, PropHotMap, value.HotMap, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, string> v) => w.WriteDictionaryValue<string, string>(o, v, null, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessorConverter))]
public sealed partial class OneHotEncodingPreprocessor
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public OneHotEncodingPreprocessor(string field, System.Collections.Generic.IDictionary<string, string> hotMap)
	{
		Field = field;
		HotMap = hotMap;
	}
#if NET7_0_OR_GREATER
	public OneHotEncodingPreprocessor()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public OneHotEncodingPreprocessor()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal OneHotEncodingPreprocessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	string Field { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IDictionary<string, string> HotMap { get; set; }
}

public readonly partial struct OneHotEncodingPreprocessorDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessor Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public OneHotEncodingPreprocessorDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessor instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public OneHotEncodingPreprocessorDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessorDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessor instance) => new Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessorDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessor(Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessorDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessorDescriptor Field(string value)
	{
		Instance.Field = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessorDescriptor HotMap(System.Collections.Generic.IDictionary<string, string> value)
	{
		Instance.HotMap = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessorDescriptor HotMap()
	{
		Instance.HotMap = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessorDescriptor HotMap(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString>? action)
	{
		Instance.HotMap = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessorDescriptor AddHotMap(string key, string value)
	{
		Instance.HotMap ??= new System.Collections.Generic.Dictionary<string, string>();
		Instance.HotMap.Add(key, value);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessor Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessorDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessorDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
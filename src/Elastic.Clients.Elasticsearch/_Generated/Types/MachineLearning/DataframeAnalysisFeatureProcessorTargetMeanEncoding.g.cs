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

internal sealed partial class DataframeAnalysisFeatureProcessorTargetMeanEncodingConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncoding>
{
	private static readonly System.Text.Json.JsonEncodedText PropDefaultValue = System.Text.Json.JsonEncodedText.Encode("default_value");
	private static readonly System.Text.Json.JsonEncodedText PropFeatureName = System.Text.Json.JsonEncodedText.Encode("feature_name");
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");
	private static readonly System.Text.Json.JsonEncodedText PropTargetMap = System.Text.Json.JsonEncodedText.Encode("target_map");

	public override Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncoding Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int> propDefaultValue = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Name> propFeatureName = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		LocalJsonValue<System.Collections.Generic.IDictionary<string, object>> propTargetMap = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDefaultValue.TryReadProperty(ref reader, options, PropDefaultValue, null))
			{
				continue;
			}

			if (propFeatureName.TryReadProperty(ref reader, options, PropFeatureName, null))
			{
				continue;
			}

			if (propField.TryReadProperty(ref reader, options, PropField, null))
			{
				continue;
			}

			if (propTargetMap.TryReadProperty(ref reader, options, PropTargetMap, static System.Collections.Generic.IDictionary<string, object> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, object>(o, null, null)!))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncoding(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			DefaultValue = propDefaultValue.Value,
			FeatureName = propFeatureName.Value,
			Field = propField.Value,
			TargetMap = propTargetMap.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncoding value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDefaultValue, value.DefaultValue, null, null);
		writer.WriteProperty(options, PropFeatureName, value.FeatureName, null, null);
		writer.WriteProperty(options, PropField, value.Field, null, null);
		writer.WriteProperty(options, PropTargetMap, value.TargetMap, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, object> v) => w.WriteDictionaryValue<string, object>(o, v, null, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncodingConverter))]
public sealed partial class DataframeAnalysisFeatureProcessorTargetMeanEncoding
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DataframeAnalysisFeatureProcessorTargetMeanEncoding(int defaultValue, Elastic.Clients.Elasticsearch.Name featureName, Elastic.Clients.Elasticsearch.Field field, System.Collections.Generic.IDictionary<string, object> targetMap)
	{
		DefaultValue = defaultValue;
		FeatureName = featureName;
		Field = field;
		TargetMap = targetMap;
	}
#if NET7_0_OR_GREATER
	public DataframeAnalysisFeatureProcessorTargetMeanEncoding()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public DataframeAnalysisFeatureProcessorTargetMeanEncoding()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DataframeAnalysisFeatureProcessorTargetMeanEncoding(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The default value if field value is not found in the target_map.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int DefaultValue { get; set; }

	/// <summary>
	/// <para>
	/// The resulting feature name.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Name FeatureName { get; set; }

	/// <summary>
	/// <para>
	/// The name of the field to encode.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Field Field { get; set; }

	/// <summary>
	/// <para>
	/// The field value to target mean transition map.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IDictionary<string, object> TargetMap { get; set; }
}

public readonly partial struct DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncoding Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncoding instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncoding(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor<TDocument>(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncoding instance) => new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncoding(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The default value if field value is not found in the target_map.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor<TDocument> DefaultValue(int value)
	{
		Instance.DefaultValue = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The resulting feature name.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor<TDocument> FeatureName(Elastic.Clients.Elasticsearch.Name value)
	{
		Instance.FeatureName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the field to encode.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the field to encode.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field value to target mean transition map.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor<TDocument> TargetMap(System.Collections.Generic.IDictionary<string, object> value)
	{
		Instance.TargetMap = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field value to target mean transition map.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor<TDocument> TargetMap()
	{
		Instance.TargetMap = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringObject.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// The field value to target mean transition map.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor<TDocument> TargetMap(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringObject>? action)
	{
		Instance.TargetMap = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringObject.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor<TDocument> AddTargetMap(string key, object value)
	{
		Instance.TargetMap ??= new System.Collections.Generic.Dictionary<string, object>();
		Instance.TargetMap.Add(key, value);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncoding Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncoding(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncoding Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncoding instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncoding(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncoding instance) => new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncoding(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The default value if field value is not found in the target_map.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor DefaultValue(int value)
	{
		Instance.DefaultValue = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The resulting feature name.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor FeatureName(Elastic.Clients.Elasticsearch.Name value)
	{
		Instance.FeatureName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the field to encode.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the field to encode.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field value to target mean transition map.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor TargetMap(System.Collections.Generic.IDictionary<string, object> value)
	{
		Instance.TargetMap = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field value to target mean transition map.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor TargetMap()
	{
		Instance.TargetMap = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringObject.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// The field value to target mean transition map.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor TargetMap(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringObject>? action)
	{
		Instance.TargetMap = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringObject.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor AddTargetMap(string key, object value)
	{
		Instance.TargetMap ??= new System.Collections.Generic.Dictionary<string, object>();
		Instance.TargetMap.Add(key, value);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncoding Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncodingDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisFeatureProcessorTargetMeanEncoding(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
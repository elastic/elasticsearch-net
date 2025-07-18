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

internal sealed partial class DetectorConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.Detector>
{
	private static readonly System.Text.Json.JsonEncodedText PropByFieldName = System.Text.Json.JsonEncodedText.Encode("by_field_name");
	private static readonly System.Text.Json.JsonEncodedText PropCustomRules = System.Text.Json.JsonEncodedText.Encode("custom_rules");
	private static readonly System.Text.Json.JsonEncodedText PropDetectorDescription = System.Text.Json.JsonEncodedText.Encode("detector_description");
	private static readonly System.Text.Json.JsonEncodedText PropDetectorIndex = System.Text.Json.JsonEncodedText.Encode("detector_index");
	private static readonly System.Text.Json.JsonEncodedText PropExcludeFrequent = System.Text.Json.JsonEncodedText.Encode("exclude_frequent");
	private static readonly System.Text.Json.JsonEncodedText PropFieldName = System.Text.Json.JsonEncodedText.Encode("field_name");
	private static readonly System.Text.Json.JsonEncodedText PropFunction = System.Text.Json.JsonEncodedText.Encode("function");
	private static readonly System.Text.Json.JsonEncodedText PropOverFieldName = System.Text.Json.JsonEncodedText.Encode("over_field_name");
	private static readonly System.Text.Json.JsonEncodedText PropPartitionFieldName = System.Text.Json.JsonEncodedText.Encode("partition_field_name");
	private static readonly System.Text.Json.JsonEncodedText PropUseNull = System.Text.Json.JsonEncodedText.Encode("use_null");

	public override Elastic.Clients.Elasticsearch.MachineLearning.Detector Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field?> propByFieldName = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.MachineLearning.DetectionRule>?> propCustomRules = default;
		LocalJsonValue<string?> propDetectorDescription = default;
		LocalJsonValue<int?> propDetectorIndex = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.ExcludeFrequent?> propExcludeFrequent = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field?> propFieldName = default;
		LocalJsonValue<string?> propFunction = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field?> propOverFieldName = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field?> propPartitionFieldName = default;
		LocalJsonValue<bool?> propUseNull = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propByFieldName.TryReadProperty(ref reader, options, PropByFieldName, null))
			{
				continue;
			}

			if (propCustomRules.TryReadProperty(ref reader, options, PropCustomRules, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.MachineLearning.DetectionRule>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.DetectionRule>(o, null)))
			{
				continue;
			}

			if (propDetectorDescription.TryReadProperty(ref reader, options, PropDetectorDescription, null))
			{
				continue;
			}

			if (propDetectorIndex.TryReadProperty(ref reader, options, PropDetectorIndex, static int? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<int>(o)))
			{
				continue;
			}

			if (propExcludeFrequent.TryReadProperty(ref reader, options, PropExcludeFrequent, static Elastic.Clients.Elasticsearch.MachineLearning.ExcludeFrequent? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<Elastic.Clients.Elasticsearch.MachineLearning.ExcludeFrequent>(o)))
			{
				continue;
			}

			if (propFieldName.TryReadProperty(ref reader, options, PropFieldName, null))
			{
				continue;
			}

			if (propFunction.TryReadProperty(ref reader, options, PropFunction, null))
			{
				continue;
			}

			if (propOverFieldName.TryReadProperty(ref reader, options, PropOverFieldName, null))
			{
				continue;
			}

			if (propPartitionFieldName.TryReadProperty(ref reader, options, PropPartitionFieldName, null))
			{
				continue;
			}

			if (propUseNull.TryReadProperty(ref reader, options, PropUseNull, static bool? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<bool>(o)))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.Detector(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ByFieldName = propByFieldName.Value,
			CustomRules = propCustomRules.Value,
			DetectorDescription = propDetectorDescription.Value,
			DetectorIndex = propDetectorIndex.Value,
			ExcludeFrequent = propExcludeFrequent.Value,
			FieldName = propFieldName.Value,
			Function = propFunction.Value,
			OverFieldName = propOverFieldName.Value,
			PartitionFieldName = propPartitionFieldName.Value,
			UseNull = propUseNull.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.Detector value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropByFieldName, value.ByFieldName, null, null);
		writer.WriteProperty(options, PropCustomRules, value.CustomRules, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.MachineLearning.DetectionRule>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.DetectionRule>(o, v, null));
		writer.WriteProperty(options, PropDetectorDescription, value.DetectorDescription, null, null);
		writer.WriteProperty(options, PropDetectorIndex, value.DetectorIndex, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, int? v) => w.WriteNullableValue<int>(o, v));
		writer.WriteProperty(options, PropExcludeFrequent, value.ExcludeFrequent, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.MachineLearning.ExcludeFrequent? v) => w.WriteNullableValue<Elastic.Clients.Elasticsearch.MachineLearning.ExcludeFrequent>(o, v));
		writer.WriteProperty(options, PropFieldName, value.FieldName, null, null);
		writer.WriteProperty(options, PropFunction, value.Function, null, null);
		writer.WriteProperty(options, PropOverFieldName, value.OverFieldName, null, null);
		writer.WriteProperty(options, PropPartitionFieldName, value.PartitionFieldName, null, null);
		writer.WriteProperty(options, PropUseNull, value.UseNull, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, bool? v) => w.WriteNullableValue<bool>(o, v));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.DetectorConverter))]
public sealed partial class Detector
{
#if NET7_0_OR_GREATER
	public Detector()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public Detector()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Detector(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The field used to split the data. In particular, this property is used for analyzing the splits with respect to their own history. It is used for finding unusual values in the context of the split.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Field? ByFieldName { get; set; }

	/// <summary>
	/// <para>
	/// Custom rules enable you to customize the way detectors operate. For example, a rule may dictate conditions under which results should be skipped. Kibana refers to custom rules as job rules.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.MachineLearning.DetectionRule>? CustomRules { get; set; }

	/// <summary>
	/// <para>
	/// A description of the detector.
	/// </para>
	/// </summary>
	public string? DetectorDescription { get; set; }

	/// <summary>
	/// <para>
	/// A unique identifier for the detector. This identifier is based on the order of the detectors in the <c>analysis_config</c>, starting at zero. If you specify a value for this property, it is ignored.
	/// </para>
	/// </summary>
	public int? DetectorIndex { get; set; }

	/// <summary>
	/// <para>
	/// If set, frequent entities are excluded from influencing the anomaly results. Entities can be considered frequent over time or frequent in a population. If you are working with both over and by fields, you can set <c>exclude_frequent</c> to <c>all</c> for both fields, or to <c>by</c> or <c>over</c> for those specific fields.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.ExcludeFrequent? ExcludeFrequent { get; set; }

	/// <summary>
	/// <para>
	/// The field that the detector uses in the function. If you use an event rate function such as count or rare, do not specify this field. The <c>field_name</c> cannot contain double quotes or backslashes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Field? FieldName { get; set; }

	/// <summary>
	/// <para>
	/// The analysis function that is used. For example, <c>count</c>, <c>rare</c>, <c>mean</c>, <c>min</c>, <c>max</c>, or <c>sum</c>.
	/// </para>
	/// </summary>
	public string? Function { get; set; }

	/// <summary>
	/// <para>
	/// The field used to split the data. In particular, this property is used for analyzing the splits with respect to the history of all splits. It is used for finding unusual values in the population of all splits.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Field? OverFieldName { get; set; }

	/// <summary>
	/// <para>
	/// The field used to segment the analysis. When you use this property, you have completely independent baselines for each value of this field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Field? PartitionFieldName { get; set; }

	/// <summary>
	/// <para>
	/// Defines whether a new series is used as the null series when there is no value for the by or partition fields.
	/// </para>
	/// </summary>
	public bool? UseNull { get; set; }
}

public readonly partial struct DetectorDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.Detector Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DetectorDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.Detector instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DetectorDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.Detector(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor<TDocument>(Elastic.Clients.Elasticsearch.MachineLearning.Detector instance) => new Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.Detector(Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The field used to split the data. In particular, this property is used for analyzing the splits with respect to their own history. It is used for finding unusual values in the context of the split.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor<TDocument> ByFieldName(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.ByFieldName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field used to split the data. In particular, this property is used for analyzing the splits with respect to their own history. It is used for finding unusual values in the context of the split.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor<TDocument> ByFieldName(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.ByFieldName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Custom rules enable you to customize the way detectors operate. For example, a rule may dictate conditions under which results should be skipped. Kibana refers to custom rules as job rules.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor<TDocument> CustomRules(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.MachineLearning.DetectionRule>? value)
	{
		Instance.CustomRules = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Custom rules enable you to customize the way detectors operate. For example, a rule may dictate conditions under which results should be skipped. Kibana refers to custom rules as job rules.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor<TDocument> CustomRules(params Elastic.Clients.Elasticsearch.MachineLearning.DetectionRule[] values)
	{
		Instance.CustomRules = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Custom rules enable you to customize the way detectors operate. For example, a rule may dictate conditions under which results should be skipped. Kibana refers to custom rules as job rules.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor<TDocument> CustomRules(params System.Action<Elastic.Clients.Elasticsearch.MachineLearning.DetectionRuleDescriptor<TDocument>>?[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.MachineLearning.DetectionRule>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.MachineLearning.DetectionRuleDescriptor<TDocument>.Build(action));
		}

		Instance.CustomRules = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// A description of the detector.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor<TDocument> DetectorDescription(string? value)
	{
		Instance.DetectorDescription = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A unique identifier for the detector. This identifier is based on the order of the detectors in the <c>analysis_config</c>, starting at zero. If you specify a value for this property, it is ignored.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor<TDocument> DetectorIndex(int? value)
	{
		Instance.DetectorIndex = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If set, frequent entities are excluded from influencing the anomaly results. Entities can be considered frequent over time or frequent in a population. If you are working with both over and by fields, you can set <c>exclude_frequent</c> to <c>all</c> for both fields, or to <c>by</c> or <c>over</c> for those specific fields.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor<TDocument> ExcludeFrequent(Elastic.Clients.Elasticsearch.MachineLearning.ExcludeFrequent? value)
	{
		Instance.ExcludeFrequent = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field that the detector uses in the function. If you use an event rate function such as count or rare, do not specify this field. The <c>field_name</c> cannot contain double quotes or backslashes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor<TDocument> FieldName(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.FieldName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field that the detector uses in the function. If you use an event rate function such as count or rare, do not specify this field. The <c>field_name</c> cannot contain double quotes or backslashes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor<TDocument> FieldName(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.FieldName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The analysis function that is used. For example, <c>count</c>, <c>rare</c>, <c>mean</c>, <c>min</c>, <c>max</c>, or <c>sum</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor<TDocument> Function(string? value)
	{
		Instance.Function = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field used to split the data. In particular, this property is used for analyzing the splits with respect to the history of all splits. It is used for finding unusual values in the population of all splits.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor<TDocument> OverFieldName(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.OverFieldName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field used to split the data. In particular, this property is used for analyzing the splits with respect to the history of all splits. It is used for finding unusual values in the population of all splits.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor<TDocument> OverFieldName(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.OverFieldName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field used to segment the analysis. When you use this property, you have completely independent baselines for each value of this field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor<TDocument> PartitionFieldName(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.PartitionFieldName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field used to segment the analysis. When you use this property, you have completely independent baselines for each value of this field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor<TDocument> PartitionFieldName(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.PartitionFieldName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines whether a new series is used as the null series when there is no value for the by or partition fields.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor<TDocument> UseNull(bool? value = true)
	{
		Instance.UseNull = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.Detector Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.MachineLearning.Detector(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.MachineLearning.Detector(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct DetectorDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.Detector Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DetectorDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.Detector instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DetectorDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.Detector(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.Detector instance) => new Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.Detector(Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The field used to split the data. In particular, this property is used for analyzing the splits with respect to their own history. It is used for finding unusual values in the context of the split.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor ByFieldName(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.ByFieldName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field used to split the data. In particular, this property is used for analyzing the splits with respect to their own history. It is used for finding unusual values in the context of the split.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor ByFieldName<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.ByFieldName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Custom rules enable you to customize the way detectors operate. For example, a rule may dictate conditions under which results should be skipped. Kibana refers to custom rules as job rules.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor CustomRules(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.MachineLearning.DetectionRule>? value)
	{
		Instance.CustomRules = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Custom rules enable you to customize the way detectors operate. For example, a rule may dictate conditions under which results should be skipped. Kibana refers to custom rules as job rules.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor CustomRules(params Elastic.Clients.Elasticsearch.MachineLearning.DetectionRule[] values)
	{
		Instance.CustomRules = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Custom rules enable you to customize the way detectors operate. For example, a rule may dictate conditions under which results should be skipped. Kibana refers to custom rules as job rules.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor CustomRules(params System.Action<Elastic.Clients.Elasticsearch.MachineLearning.DetectionRuleDescriptor>?[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.MachineLearning.DetectionRule>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.MachineLearning.DetectionRuleDescriptor.Build(action));
		}

		Instance.CustomRules = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// Custom rules enable you to customize the way detectors operate. For example, a rule may dictate conditions under which results should be skipped. Kibana refers to custom rules as job rules.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor CustomRules<T>(params System.Action<Elastic.Clients.Elasticsearch.MachineLearning.DetectionRuleDescriptor<T>>?[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.MachineLearning.DetectionRule>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.MachineLearning.DetectionRuleDescriptor<T>.Build(action));
		}

		Instance.CustomRules = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// A description of the detector.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor DetectorDescription(string? value)
	{
		Instance.DetectorDescription = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A unique identifier for the detector. This identifier is based on the order of the detectors in the <c>analysis_config</c>, starting at zero. If you specify a value for this property, it is ignored.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor DetectorIndex(int? value)
	{
		Instance.DetectorIndex = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If set, frequent entities are excluded from influencing the anomaly results. Entities can be considered frequent over time or frequent in a population. If you are working with both over and by fields, you can set <c>exclude_frequent</c> to <c>all</c> for both fields, or to <c>by</c> or <c>over</c> for those specific fields.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor ExcludeFrequent(Elastic.Clients.Elasticsearch.MachineLearning.ExcludeFrequent? value)
	{
		Instance.ExcludeFrequent = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field that the detector uses in the function. If you use an event rate function such as count or rare, do not specify this field. The <c>field_name</c> cannot contain double quotes or backslashes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor FieldName(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.FieldName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field that the detector uses in the function. If you use an event rate function such as count or rare, do not specify this field. The <c>field_name</c> cannot contain double quotes or backslashes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor FieldName<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.FieldName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The analysis function that is used. For example, <c>count</c>, <c>rare</c>, <c>mean</c>, <c>min</c>, <c>max</c>, or <c>sum</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor Function(string? value)
	{
		Instance.Function = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field used to split the data. In particular, this property is used for analyzing the splits with respect to the history of all splits. It is used for finding unusual values in the population of all splits.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor OverFieldName(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.OverFieldName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field used to split the data. In particular, this property is used for analyzing the splits with respect to the history of all splits. It is used for finding unusual values in the population of all splits.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor OverFieldName<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.OverFieldName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field used to segment the analysis. When you use this property, you have completely independent baselines for each value of this field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor PartitionFieldName(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.PartitionFieldName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field used to segment the analysis. When you use this property, you have completely independent baselines for each value of this field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor PartitionFieldName<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.PartitionFieldName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines whether a new series is used as the null series when there is no value for the by or partition fields.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor UseNull(bool? value = true)
	{
		Instance.UseNull = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.Detector Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.MachineLearning.Detector(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.DetectorDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.Detector(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
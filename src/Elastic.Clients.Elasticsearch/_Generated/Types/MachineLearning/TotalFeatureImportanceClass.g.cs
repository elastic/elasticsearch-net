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

internal sealed partial class TotalFeatureImportanceClassConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.TotalFeatureImportanceClass>
{
	private static readonly System.Text.Json.JsonEncodedText PropClassName = System.Text.Json.JsonEncodedText.Encode("class_name");
	private static readonly System.Text.Json.JsonEncodedText PropImportance = System.Text.Json.JsonEncodedText.Encode("importance");

	public override Elastic.Clients.Elasticsearch.MachineLearning.TotalFeatureImportanceClass Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propClassName = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.TotalFeatureImportanceStatistics>> propImportance = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propClassName.TryReadProperty(ref reader, options, PropClassName, null))
			{
				continue;
			}

			if (propImportance.TryReadProperty(ref reader, options, PropImportance, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.TotalFeatureImportanceStatistics> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.TotalFeatureImportanceStatistics>(o, null)!))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.TotalFeatureImportanceClass(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ClassName = propClassName.Value,
			Importance = propImportance.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.TotalFeatureImportanceClass value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropClassName, value.ClassName, null, null);
		writer.WriteProperty(options, PropImportance, value.Importance, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.TotalFeatureImportanceStatistics> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.TotalFeatureImportanceStatistics>(o, v, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.TotalFeatureImportanceClassConverter))]
public sealed partial class TotalFeatureImportanceClass
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TotalFeatureImportanceClass(string className, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.TotalFeatureImportanceStatistics> importance)
	{
		ClassName = className;
		Importance = importance;
	}
#if NET7_0_OR_GREATER
	public TotalFeatureImportanceClass()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public TotalFeatureImportanceClass()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal TotalFeatureImportanceClass(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The target class value. Could be a string, boolean, or number.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string ClassName { get; set; }

	/// <summary>
	/// <para>
	/// A collection of feature importance statistics related to the training data set for this particular feature.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.TotalFeatureImportanceStatistics> Importance { get; set; }
}
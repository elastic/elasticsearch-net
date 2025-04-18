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

internal sealed partial class DataframeAnalysisAnalyzedFieldsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFields>
{
	private static readonly System.Text.Json.JsonEncodedText PropExcludes = System.Text.Json.JsonEncodedText.Encode("excludes");
	private static readonly System.Text.Json.JsonEncodedText PropIncludes = System.Text.Json.JsonEncodedText.Encode("includes");

	public override Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFields Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		if (reader.TokenType is not System.Text.Json.JsonTokenType.StartObject)
		{
			var value = reader.ReadValue<System.Collections.Generic.ICollection<string>?>(options, static System.Collections.Generic.ICollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null));
			return new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFields(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
			{
				Includes = value
			};
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.ICollection<string>?> propExcludes = default;
		LocalJsonValue<System.Collections.Generic.ICollection<string>?> propIncludes = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propExcludes.TryReadProperty(ref reader, options, PropExcludes, static System.Collections.Generic.ICollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
			{
				continue;
			}

			if (propIncludes.TryReadProperty(ref reader, options, PropIncludes, static System.Collections.Generic.ICollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFields(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Excludes = propExcludes.Value,
			Includes = propIncludes.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFields value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropExcludes, value.Excludes, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropIncludes, value.Includes, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFieldsConverter))]
public sealed partial class DataframeAnalysisAnalyzedFields
{
#if NET7_0_OR_GREATER
	public DataframeAnalysisAnalyzedFields()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public DataframeAnalysisAnalyzedFields()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DataframeAnalysisAnalyzedFields(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// An array of strings that defines the fields that will be included in the analysis.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<string>? Excludes { get; set; }

	/// <summary>
	/// <para>
	/// An array of strings that defines the fields that will be excluded from the analysis. You do not need to add fields with unsupported data types to excludes, these fields are excluded from the analysis automatically.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<string>? Includes { get; set; }
}

public readonly partial struct DataframeAnalysisAnalyzedFieldsDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFields Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DataframeAnalysisAnalyzedFieldsDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFields instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DataframeAnalysisAnalyzedFieldsDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFields(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFieldsDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFields instance) => new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFieldsDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFields(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFieldsDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// An array of strings that defines the fields that will be included in the analysis.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFieldsDescriptor Excludes(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.Excludes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// An array of strings that defines the fields that will be included in the analysis.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFieldsDescriptor Excludes(params string[] values)
	{
		Instance.Excludes = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// An array of strings that defines the fields that will be excluded from the analysis. You do not need to add fields with unsupported data types to excludes, these fields are excluded from the analysis automatically.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFieldsDescriptor Includes(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.Includes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// An array of strings that defines the fields that will be excluded from the analysis. You do not need to add fields with unsupported data types to excludes, these fields are excluded from the analysis automatically.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFieldsDescriptor Includes(params string[] values)
	{
		Instance.Includes = [.. values];
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFields Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFieldsDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFields(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFieldsDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalysisAnalyzedFields(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
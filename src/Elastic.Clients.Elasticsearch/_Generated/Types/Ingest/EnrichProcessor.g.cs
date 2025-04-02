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

namespace Elastic.Clients.Elasticsearch.Ingest;

internal sealed partial class EnrichProcessorConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Ingest.EnrichProcessor>
{
	private static readonly System.Text.Json.JsonEncodedText PropDescription = System.Text.Json.JsonEncodedText.Encode("description");
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");
	private static readonly System.Text.Json.JsonEncodedText PropIf = System.Text.Json.JsonEncodedText.Encode("if");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreFailure = System.Text.Json.JsonEncodedText.Encode("ignore_failure");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreMissing = System.Text.Json.JsonEncodedText.Encode("ignore_missing");
	private static readonly System.Text.Json.JsonEncodedText PropMaxMatches = System.Text.Json.JsonEncodedText.Encode("max_matches");
	private static readonly System.Text.Json.JsonEncodedText PropOnFailure = System.Text.Json.JsonEncodedText.Encode("on_failure");
	private static readonly System.Text.Json.JsonEncodedText PropOverride = System.Text.Json.JsonEncodedText.Encode("override");
	private static readonly System.Text.Json.JsonEncodedText PropPolicyName = System.Text.Json.JsonEncodedText.Encode("policy_name");
	private static readonly System.Text.Json.JsonEncodedText PropShapeRelation = System.Text.Json.JsonEncodedText.Encode("shape_relation");
	private static readonly System.Text.Json.JsonEncodedText PropTag = System.Text.Json.JsonEncodedText.Encode("tag");
	private static readonly System.Text.Json.JsonEncodedText PropTargetField = System.Text.Json.JsonEncodedText.Encode("target_field");

	public override Elastic.Clients.Elasticsearch.Ingest.EnrichProcessor Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propDescription = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Script?> propIf = default;
		LocalJsonValue<bool?> propIgnoreFailure = default;
		LocalJsonValue<bool?> propIgnoreMissing = default;
		LocalJsonValue<int?> propMaxMatches = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>?> propOnFailure = default;
		LocalJsonValue<bool?> propOverride = default;
		LocalJsonValue<string> propPolicyName = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.GeoShapeRelation?> propShapeRelation = default;
		LocalJsonValue<string?> propTag = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propTargetField = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDescription.TryReadProperty(ref reader, options, PropDescription, null))
			{
				continue;
			}

			if (propField.TryReadProperty(ref reader, options, PropField, null))
			{
				continue;
			}

			if (propIf.TryReadProperty(ref reader, options, PropIf, null))
			{
				continue;
			}

			if (propIgnoreFailure.TryReadProperty(ref reader, options, PropIgnoreFailure, null))
			{
				continue;
			}

			if (propIgnoreMissing.TryReadProperty(ref reader, options, PropIgnoreMissing, null))
			{
				continue;
			}

			if (propMaxMatches.TryReadProperty(ref reader, options, PropMaxMatches, null))
			{
				continue;
			}

			if (propOnFailure.TryReadProperty(ref reader, options, PropOnFailure, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Ingest.Processor>(o, null)))
			{
				continue;
			}

			if (propOverride.TryReadProperty(ref reader, options, PropOverride, null))
			{
				continue;
			}

			if (propPolicyName.TryReadProperty(ref reader, options, PropPolicyName, null))
			{
				continue;
			}

			if (propShapeRelation.TryReadProperty(ref reader, options, PropShapeRelation, null))
			{
				continue;
			}

			if (propTag.TryReadProperty(ref reader, options, PropTag, null))
			{
				continue;
			}

			if (propTargetField.TryReadProperty(ref reader, options, PropTargetField, null))
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
		return new Elastic.Clients.Elasticsearch.Ingest.EnrichProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Description = propDescription.Value,
			Field = propField.Value,
			If = propIf.Value,
			IgnoreFailure = propIgnoreFailure.Value,
			IgnoreMissing = propIgnoreMissing.Value,
			MaxMatches = propMaxMatches.Value,
			OnFailure = propOnFailure.Value,
			Override = propOverride.Value,
			PolicyName = propPolicyName.Value,
			ShapeRelation = propShapeRelation.Value,
			Tag = propTag.Value,
			TargetField = propTargetField.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Ingest.EnrichProcessor value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDescription, value.Description, null, null);
		writer.WriteProperty(options, PropField, value.Field, null, null);
		writer.WriteProperty(options, PropIf, value.If, null, null);
		writer.WriteProperty(options, PropIgnoreFailure, value.IgnoreFailure, null, null);
		writer.WriteProperty(options, PropIgnoreMissing, value.IgnoreMissing, null, null);
		writer.WriteProperty(options, PropMaxMatches, value.MaxMatches, null, null);
		writer.WriteProperty(options, PropOnFailure, value.OnFailure, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Ingest.Processor>(o, v, null));
		writer.WriteProperty(options, PropOverride, value.Override, null, null);
		writer.WriteProperty(options, PropPolicyName, value.PolicyName, null, null);
		writer.WriteProperty(options, PropShapeRelation, value.ShapeRelation, null, null);
		writer.WriteProperty(options, PropTag, value.Tag, null, null);
		writer.WriteProperty(options, PropTargetField, value.TargetField, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorConverter))]
public sealed partial class EnrichProcessor
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public EnrichProcessor(Elastic.Clients.Elasticsearch.Field field, string policyName, Elastic.Clients.Elasticsearch.Field targetField)
	{
		Field = field;
		PolicyName = policyName;
		TargetField = targetField;
	}
#if NET7_0_OR_GREATER
	public EnrichProcessor()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public EnrichProcessor()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal EnrichProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// <para>
	/// The field in the input document that matches the policies match_field used to retrieve the enrichment data.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Field Field { get; set; }

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Script? If { get; set; }

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	public bool? IgnoreFailure { get; set; }

	/// <summary>
	/// <para>
	/// If <c>true</c> and <c>field</c> does not exist, the processor quietly exits without modifying the document.
	/// </para>
	/// </summary>
	public bool? IgnoreMissing { get; set; }

	/// <summary>
	/// <para>
	/// The maximum number of matched documents to include under the configured target field.
	/// The <c>target_field</c> will be turned into a json array if <c>max_matches</c> is higher than 1, otherwise <c>target_field</c> will become a json object.
	/// In order to avoid documents getting too large, the maximum allowed value is 128.
	/// </para>
	/// </summary>
	public int? MaxMatches { get; set; }

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? OnFailure { get; set; }

	/// <summary>
	/// <para>
	/// If processor will update fields with pre-existing non-null-valued field.
	/// When set to <c>false</c>, such fields will not be touched.
	/// </para>
	/// </summary>
	public bool? Override { get; set; }

	/// <summary>
	/// <para>
	/// The name of the enrich policy to use.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string PolicyName { get; set; }

	/// <summary>
	/// <para>
	/// A spatial relation operator used to match the geoshape of incoming documents to documents in the enrich index.
	/// This option is only used for <c>geo_match</c> enrich policy types.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.GeoShapeRelation? ShapeRelation { get; set; }

	/// <summary>
	/// <para>
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	public string? Tag { get; set; }

	/// <summary>
	/// <para>
	/// Field added to incoming documents to contain enrich data. This field contains both the <c>match_field</c> and <c>enrich_fields</c> specified in the enrich policy.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Field TargetField { get; set; }
}

public readonly partial struct EnrichProcessorDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Ingest.EnrichProcessor Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public EnrichProcessorDescriptor(Elastic.Clients.Elasticsearch.Ingest.EnrichProcessor instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public EnrichProcessorDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Ingest.EnrichProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Ingest.EnrichProcessor instance) => new Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.EnrichProcessor(Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor<TDocument> Description(string? value)
	{
		Instance.Description = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field in the input document that matches the policies match_field used to retrieve the enrichment data.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field in the input document that matches the policies match_field used to retrieve the enrichment data.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor<TDocument> If(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.If = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor<TDocument> If()
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor<TDocument> If(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor<TDocument> IgnoreFailure(bool? value = true)
	{
		Instance.IgnoreFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c> and <c>field</c> does not exist, the processor quietly exits without modifying the document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor<TDocument> IgnoreMissing(bool? value = true)
	{
		Instance.IgnoreMissing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum number of matched documents to include under the configured target field.
	/// The <c>target_field</c> will be turned into a json array if <c>max_matches</c> is higher than 1, otherwise <c>target_field</c> will become a json object.
	/// In order to avoid documents getting too large, the maximum allowed value is 128.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor<TDocument> MaxMatches(int? value)
	{
		Instance.MaxMatches = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor<TDocument> OnFailure(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? value)
	{
		Instance.OnFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor<TDocument> OnFailure()
	{
		Instance.OnFailure = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor<TDocument>.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor<TDocument> OnFailure(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor<TDocument>>? action)
	{
		Instance.OnFailure = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor<TDocument> OnFailure(params Elastic.Clients.Elasticsearch.Ingest.Processor[] values)
	{
		Instance.OnFailure = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor<TDocument> OnFailure(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Ingest.Processor>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<TDocument>.Build(action));
		}

		Instance.OnFailure = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// If processor will update fields with pre-existing non-null-valued field.
	/// When set to <c>false</c>, such fields will not be touched.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor<TDocument> Override(bool? value = true)
	{
		Instance.Override = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the enrich policy to use.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor<TDocument> PolicyName(string value)
	{
		Instance.PolicyName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A spatial relation operator used to match the geoshape of incoming documents to documents in the enrich index.
	/// This option is only used for <c>geo_match</c> enrich policy types.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor<TDocument> ShapeRelation(Elastic.Clients.Elasticsearch.GeoShapeRelation? value)
	{
		Instance.ShapeRelation = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor<TDocument> Tag(string? value)
	{
		Instance.Tag = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Field added to incoming documents to contain enrich data. This field contains both the <c>match_field</c> and <c>enrich_fields</c> specified in the enrich policy.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor<TDocument> TargetField(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.TargetField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Field added to incoming documents to contain enrich data. This field contains both the <c>match_field</c> and <c>enrich_fields</c> specified in the enrich policy.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor<TDocument> TargetField(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.TargetField = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Ingest.EnrichProcessor Build(System.Action<Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Ingest.EnrichProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct EnrichProcessorDescriptor
{
	internal Elastic.Clients.Elasticsearch.Ingest.EnrichProcessor Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public EnrichProcessorDescriptor(Elastic.Clients.Elasticsearch.Ingest.EnrichProcessor instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public EnrichProcessorDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Ingest.EnrichProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor(Elastic.Clients.Elasticsearch.Ingest.EnrichProcessor instance) => new Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.EnrichProcessor(Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Description of the processor.
	/// Useful for describing the purpose of the processor or its configuration.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor Description(string? value)
	{
		Instance.Description = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field in the input document that matches the policies match_field used to retrieve the enrichment data.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field in the input document that matches the policies match_field used to retrieve the enrichment data.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor If(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.If = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor If()
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Conditionally execute the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor If(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.If = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Ignore failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor IgnoreFailure(bool? value = true)
	{
		Instance.IgnoreFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c> and <c>field</c> does not exist, the processor quietly exits without modifying the document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor IgnoreMissing(bool? value = true)
	{
		Instance.IgnoreMissing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum number of matched documents to include under the configured target field.
	/// The <c>target_field</c> will be turned into a json array if <c>max_matches</c> is higher than 1, otherwise <c>target_field</c> will become a json object.
	/// In order to avoid documents getting too large, the maximum allowed value is 128.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor MaxMatches(int? value)
	{
		Instance.MaxMatches = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor OnFailure(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Ingest.Processor>? value)
	{
		Instance.OnFailure = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor OnFailure()
	{
		Instance.OnFailure = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor OnFailure(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor>? action)
	{
		Instance.OnFailure = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor OnFailure<T>(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor<T>>? action)
	{
		Instance.OnFailure = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfProcessor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor OnFailure(params Elastic.Clients.Elasticsearch.Ingest.Processor[] values)
	{
		Instance.OnFailure = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor OnFailure(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Ingest.Processor>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor.Build(action));
		}

		Instance.OnFailure = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// Handle failures for the processor.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor OnFailure<T>(params System.Action<Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<T>>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Ingest.Processor>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Ingest.ProcessorDescriptor<T>.Build(action));
		}

		Instance.OnFailure = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// If processor will update fields with pre-existing non-null-valued field.
	/// When set to <c>false</c>, such fields will not be touched.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor Override(bool? value = true)
	{
		Instance.Override = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the enrich policy to use.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor PolicyName(string value)
	{
		Instance.PolicyName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A spatial relation operator used to match the geoshape of incoming documents to documents in the enrich index.
	/// This option is only used for <c>geo_match</c> enrich policy types.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor ShapeRelation(Elastic.Clients.Elasticsearch.GeoShapeRelation? value)
	{
		Instance.ShapeRelation = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Identifier for the processor.
	/// Useful for debugging and metrics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor Tag(string? value)
	{
		Instance.Tag = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Field added to incoming documents to contain enrich data. This field contains both the <c>match_field</c> and <c>enrich_fields</c> specified in the enrich policy.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor TargetField(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.TargetField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Field added to incoming documents to contain enrich data. This field contains both the <c>match_field</c> and <c>enrich_fields</c> specified in the enrich policy.
	/// Supports template snippets.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor TargetField<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.TargetField = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Ingest.EnrichProcessor Build(System.Action<Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Ingest.EnrichProcessorDescriptor(new Elastic.Clients.Elasticsearch.Ingest.EnrichProcessor(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
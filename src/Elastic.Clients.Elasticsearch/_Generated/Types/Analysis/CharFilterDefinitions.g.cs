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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Analysis;
public partial class CharFilterDefinitions : IsADictionary<string, ICharFilterDefinition>
{
	public CharFilterDefinitions()
	{
	}

	public CharFilterDefinitions(IDictionary<string, ICharFilterDefinition> container) : base(container)
	{
	}

	public void Add(string name, ICharFilterDefinition charFilterDefinition) => BackingDictionary.Add(Sanitize(name), charFilterDefinition);
	public bool TryGetCharFilterDefinition(string name, [NotNullWhen(returnValue: true)] out ICharFilterDefinition charFilterDefinition) => BackingDictionary.TryGetValue(name, out charFilterDefinition);
	public bool TryGetCharFilterDefinition<T>(string name, [NotNullWhen(returnValue: true)] out T? charFilterDefinition)
		where T : class, ICharFilterDefinition
	{
		if (BackingDictionary.TryGetValue(name, out var matchedValue) && matchedValue is T finalValue)
		{
			charFilterDefinition = finalValue;
			return true;
		}

		charFilterDefinition = null;
		return false;
	}
}

public sealed partial class CharFilterDefinitionsDescriptor : IsADictionaryDescriptor<CharFilterDefinitionsDescriptor, CharFilterDefinitions, string, ICharFilterDefinition>
{
	public CharFilterDefinitionsDescriptor() : base(new CharFilterDefinitions())
	{
	}

	public CharFilterDefinitionsDescriptor(CharFilterDefinitions charFilterDefinitions) : base(charFilterDefinitions ?? new CharFilterDefinitions())
	{
	}

	public CharFilterDefinitionsDescriptor HtmlStripCharFilter(string charFilterDefinitionName) => AssignVariant<HtmlStripCharFilterDescriptor, HtmlStripCharFilter>(charFilterDefinitionName, null);
	public CharFilterDefinitionsDescriptor HtmlStripCharFilter(string charFilterDefinitionName, Action<HtmlStripCharFilterDescriptor> configure) => AssignVariant<HtmlStripCharFilterDescriptor, HtmlStripCharFilter>(charFilterDefinitionName, configure);
	public CharFilterDefinitionsDescriptor HtmlStripCharFilter(string charFilterDefinitionName, HtmlStripCharFilter htmlStripCharFilter) => AssignVariant(charFilterDefinitionName, htmlStripCharFilter);
	public CharFilterDefinitionsDescriptor IcuNormalizationCharFilter(string charFilterDefinitionName) => AssignVariant<IcuNormalizationCharFilterDescriptor, IcuNormalizationCharFilter>(charFilterDefinitionName, null);
	public CharFilterDefinitionsDescriptor IcuNormalizationCharFilter(string charFilterDefinitionName, Action<IcuNormalizationCharFilterDescriptor> configure) => AssignVariant<IcuNormalizationCharFilterDescriptor, IcuNormalizationCharFilter>(charFilterDefinitionName, configure);
	public CharFilterDefinitionsDescriptor IcuNormalizationCharFilter(string charFilterDefinitionName, IcuNormalizationCharFilter icuNormalizationCharFilter) => AssignVariant(charFilterDefinitionName, icuNormalizationCharFilter);
	public CharFilterDefinitionsDescriptor KuromojiIterationMarkCharFilter(string charFilterDefinitionName) => AssignVariant<KuromojiIterationMarkCharFilterDescriptor, KuromojiIterationMarkCharFilter>(charFilterDefinitionName, null);
	public CharFilterDefinitionsDescriptor KuromojiIterationMarkCharFilter(string charFilterDefinitionName, Action<KuromojiIterationMarkCharFilterDescriptor> configure) => AssignVariant<KuromojiIterationMarkCharFilterDescriptor, KuromojiIterationMarkCharFilter>(charFilterDefinitionName, configure);
	public CharFilterDefinitionsDescriptor KuromojiIterationMarkCharFilter(string charFilterDefinitionName, KuromojiIterationMarkCharFilter kuromojiIterationMarkCharFilter) => AssignVariant(charFilterDefinitionName, kuromojiIterationMarkCharFilter);
	public CharFilterDefinitionsDescriptor MappingCharFilter(string charFilterDefinitionName) => AssignVariant<MappingCharFilterDescriptor, MappingCharFilter>(charFilterDefinitionName, null);
	public CharFilterDefinitionsDescriptor MappingCharFilter(string charFilterDefinitionName, Action<MappingCharFilterDescriptor> configure) => AssignVariant<MappingCharFilterDescriptor, MappingCharFilter>(charFilterDefinitionName, configure);
	public CharFilterDefinitionsDescriptor MappingCharFilter(string charFilterDefinitionName, MappingCharFilter mappingCharFilter) => AssignVariant(charFilterDefinitionName, mappingCharFilter);
	public CharFilterDefinitionsDescriptor PatternReplaceCharFilter(string charFilterDefinitionName) => AssignVariant<PatternReplaceCharFilterDescriptor, PatternReplaceCharFilter>(charFilterDefinitionName, null);
	public CharFilterDefinitionsDescriptor PatternReplaceCharFilter(string charFilterDefinitionName, Action<PatternReplaceCharFilterDescriptor> configure) => AssignVariant<PatternReplaceCharFilterDescriptor, PatternReplaceCharFilter>(charFilterDefinitionName, configure);
	public CharFilterDefinitionsDescriptor PatternReplaceCharFilter(string charFilterDefinitionName, PatternReplaceCharFilter patternReplaceCharFilter) => AssignVariant(charFilterDefinitionName, patternReplaceCharFilter);
}

internal sealed partial class CharFilterDefinitionInterfaceConverter : JsonConverter<ICharFilterDefinition>
{
	public override ICharFilterDefinition Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var copiedReader = reader;
		string? type = null;
		using var jsonDoc = JsonDocument.ParseValue(ref copiedReader);
		if (jsonDoc is not null && jsonDoc.RootElement.TryGetProperty("type", out var readType) && readType.ValueKind == JsonValueKind.String)
		{
			type = readType.ToString();
		}

		switch (type)
		{
			case "kuromoji_iteration_mark":
				return JsonSerializer.Deserialize<KuromojiIterationMarkCharFilter>(ref reader, options);
			case "icu_normalizer":
				return JsonSerializer.Deserialize<IcuNormalizationCharFilter>(ref reader, options);
			case "pattern_replace":
				return JsonSerializer.Deserialize<PatternReplaceCharFilter>(ref reader, options);
			case "mapping":
				return JsonSerializer.Deserialize<MappingCharFilter>(ref reader, options);
			case "html_strip":
				return JsonSerializer.Deserialize<HtmlStripCharFilter>(ref reader, options);
			default:
				ThrowHelper.ThrowUnknownTaggedUnionVariantJsonException(type, typeof(ICharFilterDefinition));
				return null;
		}
	}

	public override void Write(Utf8JsonWriter writer, ICharFilterDefinition value, JsonSerializerOptions options)
	{
		if (value is null)
		{
			writer.WriteNullValue();
			return;
		}

		switch (value.Type)
		{
			case "kuromoji_iteration_mark":
				JsonSerializer.Serialize(writer, value, typeof(KuromojiIterationMarkCharFilter), options);
				return;
			case "icu_normalizer":
				JsonSerializer.Serialize(writer, value, typeof(IcuNormalizationCharFilter), options);
				return;
			case "pattern_replace":
				JsonSerializer.Serialize(writer, value, typeof(PatternReplaceCharFilter), options);
				return;
			case "mapping":
				JsonSerializer.Serialize(writer, value, typeof(MappingCharFilter), options);
				return;
			case "html_strip":
				JsonSerializer.Serialize(writer, value, typeof(HtmlStripCharFilter), options);
				return;
			default:
				var type = value.GetType();
				JsonSerializer.Serialize(writer, value, type, options);
				return;
		}
	}
}

[JsonConverter(typeof(CharFilterDefinitionInterfaceConverter))]
public partial interface ICharFilterDefinition
{
	public string Type { get; }
}
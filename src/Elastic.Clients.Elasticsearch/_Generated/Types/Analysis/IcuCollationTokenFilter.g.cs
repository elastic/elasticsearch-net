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

namespace Elastic.Clients.Elasticsearch.Analysis;

internal sealed partial class IcuCollationTokenFilterConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Analysis.IcuCollationTokenFilter>
{
	private static readonly System.Text.Json.JsonEncodedText PropAlternate = System.Text.Json.JsonEncodedText.Encode("alternate");
	private static readonly System.Text.Json.JsonEncodedText PropCaseFirst = System.Text.Json.JsonEncodedText.Encode("caseFirst");
	private static readonly System.Text.Json.JsonEncodedText PropCaseLevel = System.Text.Json.JsonEncodedText.Encode("caseLevel");
	private static readonly System.Text.Json.JsonEncodedText PropCountry = System.Text.Json.JsonEncodedText.Encode("country");
	private static readonly System.Text.Json.JsonEncodedText PropDecomposition = System.Text.Json.JsonEncodedText.Encode("decomposition");
	private static readonly System.Text.Json.JsonEncodedText PropHiraganaQuaternaryMode = System.Text.Json.JsonEncodedText.Encode("hiraganaQuaternaryMode");
	private static readonly System.Text.Json.JsonEncodedText PropLanguage = System.Text.Json.JsonEncodedText.Encode("language");
	private static readonly System.Text.Json.JsonEncodedText PropNumeric = System.Text.Json.JsonEncodedText.Encode("numeric");
	private static readonly System.Text.Json.JsonEncodedText PropRules = System.Text.Json.JsonEncodedText.Encode("rules");
	private static readonly System.Text.Json.JsonEncodedText PropStrength = System.Text.Json.JsonEncodedText.Encode("strength");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");
	private static readonly System.Text.Json.JsonEncodedText PropVariableTop = System.Text.Json.JsonEncodedText.Encode("variableTop");
	private static readonly System.Text.Json.JsonEncodedText PropVariant = System.Text.Json.JsonEncodedText.Encode("variant");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override Elastic.Clients.Elasticsearch.Analysis.IcuCollationTokenFilter Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Analysis.IcuCollationAlternate?> propAlternate = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Analysis.IcuCollationCaseFirst?> propCaseFirst = default;
		LocalJsonValue<bool?> propCaseLevel = default;
		LocalJsonValue<string?> propCountry = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Analysis.IcuCollationDecomposition?> propDecomposition = default;
		LocalJsonValue<bool?> propHiraganaQuaternaryMode = default;
		LocalJsonValue<string?> propLanguage = default;
		LocalJsonValue<bool?> propNumeric = default;
		LocalJsonValue<string?> propRules = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Analysis.IcuCollationStrength?> propStrength = default;
		LocalJsonValue<string?> propVariableTop = default;
		LocalJsonValue<string?> propVariant = default;
		LocalJsonValue<string?> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAlternate.TryReadProperty(ref reader, options, PropAlternate, static Elastic.Clients.Elasticsearch.Analysis.IcuCollationAlternate? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<Elastic.Clients.Elasticsearch.Analysis.IcuCollationAlternate>(o)))
			{
				continue;
			}

			if (propCaseFirst.TryReadProperty(ref reader, options, PropCaseFirst, static Elastic.Clients.Elasticsearch.Analysis.IcuCollationCaseFirst? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<Elastic.Clients.Elasticsearch.Analysis.IcuCollationCaseFirst>(o)))
			{
				continue;
			}

			if (propCaseLevel.TryReadProperty(ref reader, options, PropCaseLevel, static bool? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<bool>(o)))
			{
				continue;
			}

			if (propCountry.TryReadProperty(ref reader, options, PropCountry, null))
			{
				continue;
			}

			if (propDecomposition.TryReadProperty(ref reader, options, PropDecomposition, static Elastic.Clients.Elasticsearch.Analysis.IcuCollationDecomposition? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<Elastic.Clients.Elasticsearch.Analysis.IcuCollationDecomposition>(o)))
			{
				continue;
			}

			if (propHiraganaQuaternaryMode.TryReadProperty(ref reader, options, PropHiraganaQuaternaryMode, static bool? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<bool>(o)))
			{
				continue;
			}

			if (propLanguage.TryReadProperty(ref reader, options, PropLanguage, null))
			{
				continue;
			}

			if (propNumeric.TryReadProperty(ref reader, options, PropNumeric, static bool? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<bool>(o)))
			{
				continue;
			}

			if (propRules.TryReadProperty(ref reader, options, PropRules, null))
			{
				continue;
			}

			if (propStrength.TryReadProperty(ref reader, options, PropStrength, static Elastic.Clients.Elasticsearch.Analysis.IcuCollationStrength? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<Elastic.Clients.Elasticsearch.Analysis.IcuCollationStrength>(o)))
			{
				continue;
			}

			if (reader.ValueTextEquals(PropType))
			{
				reader.Skip();
				continue;
			}

			if (propVariableTop.TryReadProperty(ref reader, options, PropVariableTop, null))
			{
				continue;
			}

			if (propVariant.TryReadProperty(ref reader, options, PropVariant, null))
			{
				continue;
			}

			if (propVersion.TryReadProperty(ref reader, options, PropVersion, null))
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
		return new Elastic.Clients.Elasticsearch.Analysis.IcuCollationTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Alternate = propAlternate.Value,
			CaseFirst = propCaseFirst.Value,
			CaseLevel = propCaseLevel.Value,
			Country = propCountry.Value,
			Decomposition = propDecomposition.Value,
			HiraganaQuaternaryMode = propHiraganaQuaternaryMode.Value,
			Language = propLanguage.Value,
			Numeric = propNumeric.Value,
			Rules = propRules.Value,
			Strength = propStrength.Value,
			VariableTop = propVariableTop.Value,
			Variant = propVariant.Value,
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Analysis.IcuCollationTokenFilter value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAlternate, value.Alternate, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.Analysis.IcuCollationAlternate? v) => w.WriteNullableValue<Elastic.Clients.Elasticsearch.Analysis.IcuCollationAlternate>(o, v));
		writer.WriteProperty(options, PropCaseFirst, value.CaseFirst, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.Analysis.IcuCollationCaseFirst? v) => w.WriteNullableValue<Elastic.Clients.Elasticsearch.Analysis.IcuCollationCaseFirst>(o, v));
		writer.WriteProperty(options, PropCaseLevel, value.CaseLevel, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, bool? v) => w.WriteNullableValue<bool>(o, v));
		writer.WriteProperty(options, PropCountry, value.Country, null, null);
		writer.WriteProperty(options, PropDecomposition, value.Decomposition, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.Analysis.IcuCollationDecomposition? v) => w.WriteNullableValue<Elastic.Clients.Elasticsearch.Analysis.IcuCollationDecomposition>(o, v));
		writer.WriteProperty(options, PropHiraganaQuaternaryMode, value.HiraganaQuaternaryMode, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, bool? v) => w.WriteNullableValue<bool>(o, v));
		writer.WriteProperty(options, PropLanguage, value.Language, null, null);
		writer.WriteProperty(options, PropNumeric, value.Numeric, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, bool? v) => w.WriteNullableValue<bool>(o, v));
		writer.WriteProperty(options, PropRules, value.Rules, null, null);
		writer.WriteProperty(options, PropStrength, value.Strength, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.Analysis.IcuCollationStrength? v) => w.WriteNullableValue<Elastic.Clients.Elasticsearch.Analysis.IcuCollationStrength>(o, v));
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteProperty(options, PropVariableTop, value.VariableTop, null, null);
		writer.WriteProperty(options, PropVariant, value.Variant, null, null);
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Analysis.IcuCollationTokenFilterConverter))]
public sealed partial class IcuCollationTokenFilter : Elastic.Clients.Elasticsearch.Analysis.ITokenFilter
{
#if NET7_0_OR_GREATER
	public IcuCollationTokenFilter()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public IcuCollationTokenFilter()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal IcuCollationTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public Elastic.Clients.Elasticsearch.Analysis.IcuCollationAlternate? Alternate { get; set; }
	public Elastic.Clients.Elasticsearch.Analysis.IcuCollationCaseFirst? CaseFirst { get; set; }
	public bool? CaseLevel { get; set; }
	public string? Country { get; set; }
	public Elastic.Clients.Elasticsearch.Analysis.IcuCollationDecomposition? Decomposition { get; set; }
	public bool? HiraganaQuaternaryMode { get; set; }
	public string? Language { get; set; }
	public bool? Numeric { get; set; }
	public string? Rules { get; set; }
	public Elastic.Clients.Elasticsearch.Analysis.IcuCollationStrength? Strength { get; set; }

	public string Type => "icu_collation";

	public string? VariableTop { get; set; }
	public string? Variant { get; set; }
	public string? Version { get; set; }
}

public readonly partial struct IcuCollationTokenFilterDescriptor
{
	internal Elastic.Clients.Elasticsearch.Analysis.IcuCollationTokenFilter Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IcuCollationTokenFilterDescriptor(Elastic.Clients.Elasticsearch.Analysis.IcuCollationTokenFilter instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IcuCollationTokenFilterDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Analysis.IcuCollationTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Analysis.IcuCollationTokenFilterDescriptor(Elastic.Clients.Elasticsearch.Analysis.IcuCollationTokenFilter instance) => new Elastic.Clients.Elasticsearch.Analysis.IcuCollationTokenFilterDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Analysis.IcuCollationTokenFilter(Elastic.Clients.Elasticsearch.Analysis.IcuCollationTokenFilterDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Analysis.IcuCollationTokenFilterDescriptor Alternate(Elastic.Clients.Elasticsearch.Analysis.IcuCollationAlternate? value)
	{
		Instance.Alternate = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.IcuCollationTokenFilterDescriptor CaseFirst(Elastic.Clients.Elasticsearch.Analysis.IcuCollationCaseFirst? value)
	{
		Instance.CaseFirst = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.IcuCollationTokenFilterDescriptor CaseLevel(bool? value = true)
	{
		Instance.CaseLevel = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.IcuCollationTokenFilterDescriptor Country(string? value)
	{
		Instance.Country = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.IcuCollationTokenFilterDescriptor Decomposition(Elastic.Clients.Elasticsearch.Analysis.IcuCollationDecomposition? value)
	{
		Instance.Decomposition = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.IcuCollationTokenFilterDescriptor HiraganaQuaternaryMode(bool? value = true)
	{
		Instance.HiraganaQuaternaryMode = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.IcuCollationTokenFilterDescriptor Language(string? value)
	{
		Instance.Language = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.IcuCollationTokenFilterDescriptor Numeric(bool? value = true)
	{
		Instance.Numeric = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.IcuCollationTokenFilterDescriptor Rules(string? value)
	{
		Instance.Rules = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.IcuCollationTokenFilterDescriptor Strength(Elastic.Clients.Elasticsearch.Analysis.IcuCollationStrength? value)
	{
		Instance.Strength = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.IcuCollationTokenFilterDescriptor VariableTop(string? value)
	{
		Instance.VariableTop = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.IcuCollationTokenFilterDescriptor Variant(string? value)
	{
		Instance.Variant = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.IcuCollationTokenFilterDescriptor Version(string? value)
	{
		Instance.Version = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Analysis.IcuCollationTokenFilter Build(System.Action<Elastic.Clients.Elasticsearch.Analysis.IcuCollationTokenFilterDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Analysis.IcuCollationTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Analysis.IcuCollationTokenFilterDescriptor(new Elastic.Clients.Elasticsearch.Analysis.IcuCollationTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
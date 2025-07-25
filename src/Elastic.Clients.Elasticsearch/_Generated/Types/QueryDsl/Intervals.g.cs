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

namespace Elastic.Clients.Elasticsearch.QueryDsl;

internal sealed partial class IntervalsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.QueryDsl.Intervals>
{
	private static readonly System.Text.Json.JsonEncodedText VariantAllOf = System.Text.Json.JsonEncodedText.Encode("all_of");
	private static readonly System.Text.Json.JsonEncodedText VariantAnyOf = System.Text.Json.JsonEncodedText.Encode("any_of");
	private static readonly System.Text.Json.JsonEncodedText VariantFuzzy = System.Text.Json.JsonEncodedText.Encode("fuzzy");
	private static readonly System.Text.Json.JsonEncodedText VariantMatch = System.Text.Json.JsonEncodedText.Encode("match");
	private static readonly System.Text.Json.JsonEncodedText VariantPrefix = System.Text.Json.JsonEncodedText.Encode("prefix");
	private static readonly System.Text.Json.JsonEncodedText VariantRange = System.Text.Json.JsonEncodedText.Encode("range");
	private static readonly System.Text.Json.JsonEncodedText VariantRegexp = System.Text.Json.JsonEncodedText.Encode("regexp");
	private static readonly System.Text.Json.JsonEncodedText VariantWildcard = System.Text.Json.JsonEncodedText.Encode("wildcard");

	public override Elastic.Clients.Elasticsearch.QueryDsl.Intervals Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		string? variantType = null;
		object? variant = null;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (reader.ValueTextEquals(VariantAllOf))
			{
				variantType = VariantAllOf.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAllOf>(options, null);
				continue;
			}

			if (reader.ValueTextEquals(VariantAnyOf))
			{
				variantType = VariantAnyOf.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAnyOf>(options, null);
				continue;
			}

			if (reader.ValueTextEquals(VariantFuzzy))
			{
				variantType = VariantFuzzy.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFuzzy>(options, null);
				continue;
			}

			if (reader.ValueTextEquals(VariantMatch))
			{
				variantType = VariantMatch.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatch>(options, null);
				continue;
			}

			if (reader.ValueTextEquals(VariantPrefix))
			{
				variantType = VariantPrefix.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsPrefix>(options, null);
				continue;
			}

			if (reader.ValueTextEquals(VariantRange))
			{
				variantType = VariantRange.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsRange>(options, null);
				continue;
			}

			if (reader.ValueTextEquals(VariantRegexp))
			{
				variantType = VariantRegexp.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsRegexp>(options, null);
				continue;
			}

			if (reader.ValueTextEquals(VariantWildcard))
			{
				variantType = VariantWildcard.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcard>(options, null);
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
		return new Elastic.Clients.Elasticsearch.QueryDsl.Intervals(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			VariantType = variantType,
			Variant = variant
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.QueryDsl.Intervals value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		switch (value.VariantType)
		{
			case null:
				break;
			case "all_of":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAllOf)value.Variant, null, null);
				break;
			case "any_of":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAnyOf)value.Variant, null, null);
				break;
			case "fuzzy":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFuzzy)value.Variant, null, null);
				break;
			case "match":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatch)value.Variant, null, null);
				break;
			case "prefix":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.QueryDsl.IntervalsPrefix)value.Variant, null, null);
				break;
			case "range":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.QueryDsl.IntervalsRange)value.Variant, null, null);
				break;
			case "regexp":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.QueryDsl.IntervalsRegexp)value.Variant, null, null);
				break;
			case "wildcard":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcard)value.Variant, null, null);
				break;
			default:
				throw new System.Text.Json.JsonException($"Variant '{value.VariantType}' is not supported for type '{nameof(Elastic.Clients.Elasticsearch.QueryDsl.Intervals)}'.");
		}

		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsConverter))]
public sealed partial class Intervals
{
	internal string? VariantType { get; set; }
	internal object? Variant { get; set; }
#if NET7_0_OR_GREATER
	public Intervals()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public Intervals()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Intervals(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Returns matches that span a combination of other rules.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAllOf? AllOf { get => GetVariant<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAllOf>("all_of"); set => SetVariant("all_of", value); }

	/// <summary>
	/// <para>
	/// Returns intervals produced by any of its sub-rules.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAnyOf? AnyOf { get => GetVariant<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAnyOf>("any_of"); set => SetVariant("any_of", value); }

	/// <summary>
	/// <para>
	/// Matches analyzed text.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFuzzy? Fuzzy { get => GetVariant<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFuzzy>("fuzzy"); set => SetVariant("fuzzy", value); }

	/// <summary>
	/// <para>
	/// Matches analyzed text.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatch? Match { get => GetVariant<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatch>("match"); set => SetVariant("match", value); }

	/// <summary>
	/// <para>
	/// Matches terms that start with a specified set of characters.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsPrefix? Prefix { get => GetVariant<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsPrefix>("prefix"); set => SetVariant("prefix", value); }
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsRange? Range { get => GetVariant<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsRange>("range"); set => SetVariant("range", value); }
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsRegexp? Regexp { get => GetVariant<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsRegexp>("regexp"); set => SetVariant("regexp", value); }

	/// <summary>
	/// <para>
	/// Matches terms using a wildcard pattern.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcard? Wildcard { get => GetVariant<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcard>("wildcard"); set => SetVariant("wildcard", value); }

	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.Intervals(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAllOf value) => new Elastic.Clients.Elasticsearch.QueryDsl.Intervals { AllOf = value };
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.Intervals(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAnyOf value) => new Elastic.Clients.Elasticsearch.QueryDsl.Intervals { AnyOf = value };
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.Intervals(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFuzzy value) => new Elastic.Clients.Elasticsearch.QueryDsl.Intervals { Fuzzy = value };
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.Intervals(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatch value) => new Elastic.Clients.Elasticsearch.QueryDsl.Intervals { Match = value };
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.Intervals(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsPrefix value) => new Elastic.Clients.Elasticsearch.QueryDsl.Intervals { Prefix = value };
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.Intervals(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsRange value) => new Elastic.Clients.Elasticsearch.QueryDsl.Intervals { Range = value };
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.Intervals(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsRegexp value) => new Elastic.Clients.Elasticsearch.QueryDsl.Intervals { Regexp = value };
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.Intervals(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcard value) => new Elastic.Clients.Elasticsearch.QueryDsl.Intervals { Wildcard = value };

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	private T? GetVariant<T>(string type)
	{
		if (string.Equals(VariantType, type, System.StringComparison.Ordinal) && Variant is T result)
		{
			return result;
		}

		return default;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	private void SetVariant<T>(string type, T? value)
	{
		VariantType = type;
		Variant = value;
	}
}

public readonly partial struct IntervalsDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.Intervals Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IntervalsDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.Intervals instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IntervalsDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.Intervals(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor<TDocument>(Elastic.Clients.Elasticsearch.QueryDsl.Intervals instance) => new Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.Intervals(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Returns matches that span a combination of other rules.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor<TDocument> AllOf(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAllOf? value)
	{
		Instance.AllOf = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Returns matches that span a combination of other rules.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor<TDocument> AllOf(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAllOfDescriptor<TDocument>> action)
	{
		Instance.AllOf = Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAllOfDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Returns intervals produced by any of its sub-rules.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor<TDocument> AnyOf(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAnyOf? value)
	{
		Instance.AnyOf = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Returns intervals produced by any of its sub-rules.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor<TDocument> AnyOf(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAnyOfDescriptor<TDocument>> action)
	{
		Instance.AnyOf = Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAnyOfDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Matches analyzed text.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor<TDocument> Fuzzy(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFuzzy? value)
	{
		Instance.Fuzzy = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Matches analyzed text.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor<TDocument> Fuzzy(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFuzzyDescriptor<TDocument>> action)
	{
		Instance.Fuzzy = Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFuzzyDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Matches analyzed text.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor<TDocument> Match(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatch? value)
	{
		Instance.Match = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Matches analyzed text.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor<TDocument> Match(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor<TDocument>> action)
	{
		Instance.Match = Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Matches terms that start with a specified set of characters.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor<TDocument> Prefix(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsPrefix? value)
	{
		Instance.Prefix = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Matches terms that start with a specified set of characters.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor<TDocument> Prefix(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsPrefixDescriptor<TDocument>> action)
	{
		Instance.Prefix = Elastic.Clients.Elasticsearch.QueryDsl.IntervalsPrefixDescriptor<TDocument>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor<TDocument> Range(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsRange? value)
	{
		Instance.Range = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor<TDocument> Range()
	{
		Instance.Range = Elastic.Clients.Elasticsearch.QueryDsl.IntervalsRangeDescriptor<TDocument>.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor<TDocument> Range(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsRangeDescriptor<TDocument>>? action)
	{
		Instance.Range = Elastic.Clients.Elasticsearch.QueryDsl.IntervalsRangeDescriptor<TDocument>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor<TDocument> Regexp(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsRegexp? value)
	{
		Instance.Regexp = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor<TDocument> Regexp(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsRegexpDescriptor<TDocument>> action)
	{
		Instance.Regexp = Elastic.Clients.Elasticsearch.QueryDsl.IntervalsRegexpDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Matches terms using a wildcard pattern.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor<TDocument> Wildcard(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcard? value)
	{
		Instance.Wildcard = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Matches terms using a wildcard pattern.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor<TDocument> Wildcard(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcardDescriptor<TDocument>> action)
	{
		Instance.Wildcard = Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcardDescriptor<TDocument>.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.Intervals Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.QueryDsl.Intervals(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct IntervalsDescriptor
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.Intervals Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IntervalsDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.Intervals instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IntervalsDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.Intervals(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.Intervals instance) => new Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.Intervals(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Returns matches that span a combination of other rules.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor AllOf(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAllOf? value)
	{
		Instance.AllOf = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Returns matches that span a combination of other rules.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor AllOf(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAllOfDescriptor> action)
	{
		Instance.AllOf = Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAllOfDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Returns matches that span a combination of other rules.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor AllOf<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAllOfDescriptor<T>> action)
	{
		Instance.AllOf = Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAllOfDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Returns intervals produced by any of its sub-rules.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor AnyOf(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAnyOf? value)
	{
		Instance.AnyOf = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Returns intervals produced by any of its sub-rules.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor AnyOf(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAnyOfDescriptor> action)
	{
		Instance.AnyOf = Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAnyOfDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Returns intervals produced by any of its sub-rules.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor AnyOf<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAnyOfDescriptor<T>> action)
	{
		Instance.AnyOf = Elastic.Clients.Elasticsearch.QueryDsl.IntervalsAnyOfDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Matches analyzed text.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor Fuzzy(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFuzzy? value)
	{
		Instance.Fuzzy = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Matches analyzed text.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor Fuzzy(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFuzzyDescriptor> action)
	{
		Instance.Fuzzy = Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFuzzyDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Matches analyzed text.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor Fuzzy<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFuzzyDescriptor<T>> action)
	{
		Instance.Fuzzy = Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFuzzyDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Matches analyzed text.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor Match(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatch? value)
	{
		Instance.Match = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Matches analyzed text.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor Match(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor> action)
	{
		Instance.Match = Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Matches analyzed text.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor Match<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor<T>> action)
	{
		Instance.Match = Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Matches terms that start with a specified set of characters.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor Prefix(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsPrefix? value)
	{
		Instance.Prefix = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Matches terms that start with a specified set of characters.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor Prefix(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsPrefixDescriptor> action)
	{
		Instance.Prefix = Elastic.Clients.Elasticsearch.QueryDsl.IntervalsPrefixDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Matches terms that start with a specified set of characters.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor Prefix<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsPrefixDescriptor<T>> action)
	{
		Instance.Prefix = Elastic.Clients.Elasticsearch.QueryDsl.IntervalsPrefixDescriptor<T>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor Range(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsRange? value)
	{
		Instance.Range = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor Range()
	{
		Instance.Range = Elastic.Clients.Elasticsearch.QueryDsl.IntervalsRangeDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor Range(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsRangeDescriptor>? action)
	{
		Instance.Range = Elastic.Clients.Elasticsearch.QueryDsl.IntervalsRangeDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor Range<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsRangeDescriptor<T>>? action)
	{
		Instance.Range = Elastic.Clients.Elasticsearch.QueryDsl.IntervalsRangeDescriptor<T>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor Regexp(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsRegexp? value)
	{
		Instance.Regexp = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor Regexp(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsRegexpDescriptor> action)
	{
		Instance.Regexp = Elastic.Clients.Elasticsearch.QueryDsl.IntervalsRegexpDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor Regexp<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsRegexpDescriptor<T>> action)
	{
		Instance.Regexp = Elastic.Clients.Elasticsearch.QueryDsl.IntervalsRegexpDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Matches terms using a wildcard pattern.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor Wildcard(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcard? value)
	{
		Instance.Wildcard = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Matches terms using a wildcard pattern.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor Wildcard(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcardDescriptor> action)
	{
		Instance.Wildcard = Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcardDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Matches terms using a wildcard pattern.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor Wildcard<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcardDescriptor<T>> action)
	{
		Instance.Wildcard = Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcardDescriptor<T>.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.Intervals Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.IntervalsDescriptor(new Elastic.Clients.Elasticsearch.QueryDsl.Intervals(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
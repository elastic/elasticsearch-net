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

internal sealed partial class ShingleTokenFilterConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Analysis.ShingleTokenFilter>
{
	private static readonly System.Text.Json.JsonEncodedText PropFillerToken = System.Text.Json.JsonEncodedText.Encode("filler_token");
	private static readonly System.Text.Json.JsonEncodedText PropMaxShingleSize = System.Text.Json.JsonEncodedText.Encode("max_shingle_size");
	private static readonly System.Text.Json.JsonEncodedText PropMinShingleSize = System.Text.Json.JsonEncodedText.Encode("min_shingle_size");
	private static readonly System.Text.Json.JsonEncodedText PropOutputUnigrams = System.Text.Json.JsonEncodedText.Encode("output_unigrams");
	private static readonly System.Text.Json.JsonEncodedText PropOutputUnigramsIfNoShingles = System.Text.Json.JsonEncodedText.Encode("output_unigrams_if_no_shingles");
	private static readonly System.Text.Json.JsonEncodedText PropTokenSeparator = System.Text.Json.JsonEncodedText.Encode("token_separator");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override Elastic.Clients.Elasticsearch.Analysis.ShingleTokenFilter Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propFillerToken = default;
		LocalJsonValue<int?> propMaxShingleSize = default;
		LocalJsonValue<int?> propMinShingleSize = default;
		LocalJsonValue<bool?> propOutputUnigrams = default;
		LocalJsonValue<bool?> propOutputUnigramsIfNoShingles = default;
		LocalJsonValue<string?> propTokenSeparator = default;
		LocalJsonValue<string?> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propFillerToken.TryReadProperty(ref reader, options, PropFillerToken, null))
			{
				continue;
			}

			if (propMaxShingleSize.TryReadProperty(ref reader, options, PropMaxShingleSize, static int? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<int>(o)))
			{
				continue;
			}

			if (propMinShingleSize.TryReadProperty(ref reader, options, PropMinShingleSize, static int? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<int>(o)))
			{
				continue;
			}

			if (propOutputUnigrams.TryReadProperty(ref reader, options, PropOutputUnigrams, static bool? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<bool>(o)))
			{
				continue;
			}

			if (propOutputUnigramsIfNoShingles.TryReadProperty(ref reader, options, PropOutputUnigramsIfNoShingles, static bool? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<bool>(o)))
			{
				continue;
			}

			if (propTokenSeparator.TryReadProperty(ref reader, options, PropTokenSeparator, null))
			{
				continue;
			}

			if (reader.ValueTextEquals(PropType))
			{
				reader.Skip();
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
		return new Elastic.Clients.Elasticsearch.Analysis.ShingleTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			FillerToken = propFillerToken.Value,
			MaxShingleSize = propMaxShingleSize.Value,
			MinShingleSize = propMinShingleSize.Value,
			OutputUnigrams = propOutputUnigrams.Value,
			OutputUnigramsIfNoShingles = propOutputUnigramsIfNoShingles.Value,
			TokenSeparator = propTokenSeparator.Value,
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Analysis.ShingleTokenFilter value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropFillerToken, value.FillerToken, null, null);
		writer.WriteProperty(options, PropMaxShingleSize, value.MaxShingleSize, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, int? v) => w.WriteNullableValue<int>(o, v));
		writer.WriteProperty(options, PropMinShingleSize, value.MinShingleSize, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, int? v) => w.WriteNullableValue<int>(o, v));
		writer.WriteProperty(options, PropOutputUnigrams, value.OutputUnigrams, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, bool? v) => w.WriteNullableValue<bool>(o, v));
		writer.WriteProperty(options, PropOutputUnigramsIfNoShingles, value.OutputUnigramsIfNoShingles, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, bool? v) => w.WriteNullableValue<bool>(o, v));
		writer.WriteProperty(options, PropTokenSeparator, value.TokenSeparator, null, null);
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Analysis.ShingleTokenFilterConverter))]
public sealed partial class ShingleTokenFilter : Elastic.Clients.Elasticsearch.Analysis.ITokenFilter
{
#if NET7_0_OR_GREATER
	public ShingleTokenFilter()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public ShingleTokenFilter()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ShingleTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// String used in shingles as a replacement for empty positions that do not contain a token. This filler token is only used in shingles, not original unigrams. Defaults to an underscore (<c>_</c>).
	/// </para>
	/// </summary>
	public string? FillerToken { get; set; }

	/// <summary>
	/// <para>
	/// Maximum number of tokens to concatenate when creating shingles. Defaults to <c>2</c>.
	/// </para>
	/// </summary>
	public int? MaxShingleSize { get; set; }

	/// <summary>
	/// <para>
	/// Minimum number of tokens to concatenate when creating shingles. Defaults to <c>2</c>.
	/// </para>
	/// </summary>
	public int? MinShingleSize { get; set; }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the output includes the original input tokens. If <c>false</c>, the output only includes shingles; the original input tokens are removed. Defaults to <c>true</c>.
	/// </para>
	/// </summary>
	public bool? OutputUnigrams { get; set; }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the output includes the original input tokens only if no shingles are produced; if shingles are produced, the output only includes shingles. Defaults to <c>false</c>.
	/// </para>
	/// </summary>
	public bool? OutputUnigramsIfNoShingles { get; set; }

	/// <summary>
	/// <para>
	/// Separator used to concatenate adjacent tokens to form a shingle. Defaults to a space (<c>" "</c>).
	/// </para>
	/// </summary>
	public string? TokenSeparator { get; set; }

	public string Type => "shingle";

	public string? Version { get; set; }
}

public readonly partial struct ShingleTokenFilterDescriptor
{
	internal Elastic.Clients.Elasticsearch.Analysis.ShingleTokenFilter Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ShingleTokenFilterDescriptor(Elastic.Clients.Elasticsearch.Analysis.ShingleTokenFilter instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ShingleTokenFilterDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Analysis.ShingleTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Analysis.ShingleTokenFilterDescriptor(Elastic.Clients.Elasticsearch.Analysis.ShingleTokenFilter instance) => new Elastic.Clients.Elasticsearch.Analysis.ShingleTokenFilterDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Analysis.ShingleTokenFilter(Elastic.Clients.Elasticsearch.Analysis.ShingleTokenFilterDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// String used in shingles as a replacement for empty positions that do not contain a token. This filler token is only used in shingles, not original unigrams. Defaults to an underscore (<c>_</c>).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Analysis.ShingleTokenFilterDescriptor FillerToken(string? value)
	{
		Instance.FillerToken = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Maximum number of tokens to concatenate when creating shingles. Defaults to <c>2</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Analysis.ShingleTokenFilterDescriptor MaxShingleSize(int? value)
	{
		Instance.MaxShingleSize = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Minimum number of tokens to concatenate when creating shingles. Defaults to <c>2</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Analysis.ShingleTokenFilterDescriptor MinShingleSize(int? value)
	{
		Instance.MinShingleSize = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the output includes the original input tokens. If <c>false</c>, the output only includes shingles; the original input tokens are removed. Defaults to <c>true</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Analysis.ShingleTokenFilterDescriptor OutputUnigrams(bool? value = true)
	{
		Instance.OutputUnigrams = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the output includes the original input tokens only if no shingles are produced; if shingles are produced, the output only includes shingles. Defaults to <c>false</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Analysis.ShingleTokenFilterDescriptor OutputUnigramsIfNoShingles(bool? value = true)
	{
		Instance.OutputUnigramsIfNoShingles = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Separator used to concatenate adjacent tokens to form a shingle. Defaults to a space (<c>" "</c>).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Analysis.ShingleTokenFilterDescriptor TokenSeparator(string? value)
	{
		Instance.TokenSeparator = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.ShingleTokenFilterDescriptor Version(string? value)
	{
		Instance.Version = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Analysis.ShingleTokenFilter Build(System.Action<Elastic.Clients.Elasticsearch.Analysis.ShingleTokenFilterDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Analysis.ShingleTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Analysis.ShingleTokenFilterDescriptor(new Elastic.Clients.Elasticsearch.Analysis.ShingleTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
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

namespace Elastic.Clients.Elasticsearch.Core.Search;

internal sealed partial class SourceFilterConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.Search.SourceFilter>
{
	private static readonly System.Text.Json.JsonEncodedText PropExcludes = System.Text.Json.JsonEncodedText.Encode("excludes");
	private static readonly System.Text.Json.JsonEncodedText PropExcludes1 = System.Text.Json.JsonEncodedText.Encode("exclude");
	private static readonly System.Text.Json.JsonEncodedText PropExcludeVectors = System.Text.Json.JsonEncodedText.Encode("exclude_vectors");
	private static readonly System.Text.Json.JsonEncodedText PropIncludes = System.Text.Json.JsonEncodedText.Encode("includes");
	private static readonly System.Text.Json.JsonEncodedText PropIncludes1 = System.Text.Json.JsonEncodedText.Encode("include");

	public override Elastic.Clients.Elasticsearch.Core.Search.SourceFilter Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		if (reader.TokenType is not System.Text.Json.JsonTokenType.StartObject)
		{
			var value = reader.ReadValue<Elastic.Clients.Elasticsearch.Fields?>(options, static Elastic.Clients.Elasticsearch.Fields? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<Elastic.Clients.Elasticsearch.Fields?>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.SingleOrManyFieldsMarker)));
			return new Elastic.Clients.Elasticsearch.Core.Search.SourceFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
			{
				Includes = value
			};
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Fields?> propExcludes = default;
		LocalJsonValue<bool?> propExcludeVectors = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Fields?> propIncludes = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propExcludes.TryReadProperty(ref reader, options, PropExcludes, static Elastic.Clients.Elasticsearch.Fields? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<Elastic.Clients.Elasticsearch.Fields?>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.SingleOrManyFieldsMarker))) || propExcludes.TryReadProperty(ref reader, options, PropExcludes1, static Elastic.Clients.Elasticsearch.Fields? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<Elastic.Clients.Elasticsearch.Fields?>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.SingleOrManyFieldsMarker))))
			{
				continue;
			}

			if (propExcludeVectors.TryReadProperty(ref reader, options, PropExcludeVectors, static bool? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<bool>(o)))
			{
				continue;
			}

			if (propIncludes.TryReadProperty(ref reader, options, PropIncludes, static Elastic.Clients.Elasticsearch.Fields? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<Elastic.Clients.Elasticsearch.Fields?>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.SingleOrManyFieldsMarker))) || propIncludes.TryReadProperty(ref reader, options, PropIncludes1, static Elastic.Clients.Elasticsearch.Fields? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<Elastic.Clients.Elasticsearch.Fields?>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.SingleOrManyFieldsMarker))))
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
		return new Elastic.Clients.Elasticsearch.Core.Search.SourceFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Excludes = propExcludes.Value,
			ExcludeVectors = propExcludeVectors.Value,
			Includes = propIncludes.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.Search.SourceFilter value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropExcludes, value.Excludes, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.Fields? v) => w.WriteValueEx<Elastic.Clients.Elasticsearch.Fields?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.SingleOrManyFieldsMarker)));
		writer.WriteProperty(options, PropExcludeVectors, value.ExcludeVectors, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, bool? v) => w.WriteNullableValue<bool>(o, v));
		writer.WriteProperty(options, PropIncludes, value.Includes, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.Fields? v) => w.WriteValueEx<Elastic.Clients.Elasticsearch.Fields?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.SingleOrManyFieldsMarker)));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.Search.SourceFilterConverter))]
public sealed partial class SourceFilter
{
#if NET7_0_OR_GREATER
	public SourceFilter()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public SourceFilter()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal SourceFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// A list of fields to exclude from the returned source.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? Excludes { get; set; }

	/// <summary>
	/// <para>
	/// If <c>true</c>, vector fields are excluded from the returned source.
	/// </para>
	/// <para>
	/// This option takes precedence over <c>includes</c>: any vector field will
	/// remain excluded even if it matches an <c>includes</c> rule.
	/// </para>
	/// </summary>
	public bool? ExcludeVectors { get; set; }

	/// <summary>
	/// <para>
	/// A list of fields to include in the returned source.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? Includes { get; set; }
}

public readonly partial struct SourceFilterDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Core.Search.SourceFilter Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SourceFilterDescriptor(Elastic.Clients.Elasticsearch.Core.Search.SourceFilter instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SourceFilterDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Core.Search.SourceFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Core.Search.SourceFilterDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Core.Search.SourceFilter instance) => new Elastic.Clients.Elasticsearch.Core.Search.SourceFilterDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Core.Search.SourceFilter(Elastic.Clients.Elasticsearch.Core.Search.SourceFilterDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A list of fields to exclude from the returned source.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.SourceFilterDescriptor<TDocument> Excludes(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.Excludes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of fields to exclude from the returned source.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.SourceFilterDescriptor<TDocument> Excludes(params System.Linq.Expressions.Expression<System.Func<TDocument, object?>>[] value)
	{
		Instance.Excludes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, vector fields are excluded from the returned source.
	/// </para>
	/// <para>
	/// This option takes precedence over <c>includes</c>: any vector field will
	/// remain excluded even if it matches an <c>includes</c> rule.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.SourceFilterDescriptor<TDocument> ExcludeVectors(bool? value = true)
	{
		Instance.ExcludeVectors = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of fields to include in the returned source.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.SourceFilterDescriptor<TDocument> Includes(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.Includes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of fields to include in the returned source.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.SourceFilterDescriptor<TDocument> Includes(params System.Linq.Expressions.Expression<System.Func<TDocument, object?>>[] value)
	{
		Instance.Includes = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Core.Search.SourceFilter Build(System.Action<Elastic.Clients.Elasticsearch.Core.Search.SourceFilterDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Core.Search.SourceFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Core.Search.SourceFilterDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Core.Search.SourceFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct SourceFilterDescriptor
{
	internal Elastic.Clients.Elasticsearch.Core.Search.SourceFilter Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SourceFilterDescriptor(Elastic.Clients.Elasticsearch.Core.Search.SourceFilter instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SourceFilterDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Core.Search.SourceFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Core.Search.SourceFilterDescriptor(Elastic.Clients.Elasticsearch.Core.Search.SourceFilter instance) => new Elastic.Clients.Elasticsearch.Core.Search.SourceFilterDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Core.Search.SourceFilter(Elastic.Clients.Elasticsearch.Core.Search.SourceFilterDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A list of fields to exclude from the returned source.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.SourceFilterDescriptor Excludes(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.Excludes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of fields to exclude from the returned source.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.SourceFilterDescriptor Excludes<T>(params System.Linq.Expressions.Expression<System.Func<T, object?>>[] value)
	{
		Instance.Excludes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, vector fields are excluded from the returned source.
	/// </para>
	/// <para>
	/// This option takes precedence over <c>includes</c>: any vector field will
	/// remain excluded even if it matches an <c>includes</c> rule.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.SourceFilterDescriptor ExcludeVectors(bool? value = true)
	{
		Instance.ExcludeVectors = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of fields to include in the returned source.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.SourceFilterDescriptor Includes(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.Includes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A list of fields to include in the returned source.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.SourceFilterDescriptor Includes<T>(params System.Linq.Expressions.Expression<System.Func<T, object?>>[] value)
	{
		Instance.Includes = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Core.Search.SourceFilter Build(System.Action<Elastic.Clients.Elasticsearch.Core.Search.SourceFilterDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Core.Search.SourceFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Core.Search.SourceFilterDescriptor(new Elastic.Clients.Elasticsearch.Core.Search.SourceFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}
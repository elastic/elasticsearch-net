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

internal sealed partial class SourceConfigParamConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam>
{
	public override Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		var selector = static (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => JsonUnionSelector.ByTokenType(ref r, o, Elastic.Clients.Elasticsearch.Serialization.JsonTokenTypes.True | Elastic.Clients.Elasticsearch.Serialization.JsonTokenTypes.False, Elastic.Clients.Elasticsearch.Serialization.JsonTokenTypes.String | Elastic.Clients.Elasticsearch.Serialization.JsonTokenTypes.StartArray);
		return selector(ref reader, options) switch
		{
			Elastic.Clients.Elasticsearch.UnionTag.T1 => new Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam(reader.ReadValue<bool>(options, null)),
			Elastic.Clients.Elasticsearch.UnionTag.T2 => new Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam(reader.ReadValue<Elastic.Clients.Elasticsearch.Fields>(options, static Elastic.Clients.Elasticsearch.Fields (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<Elastic.Clients.Elasticsearch.Fields>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.SingleOrManyFieldsMarker)))),
			_ => throw new System.InvalidOperationException($"Failed to select a union variant for type '{nameof(Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam)}")
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam value, System.Text.Json.JsonSerializerOptions options)
	{
		switch (value.Tag)
		{
			case Elastic.Clients.Elasticsearch.UnionTag.T1:
				{
					writer.WriteValue(options, value.Value1, null);
					break;
				}

			case Elastic.Clients.Elasticsearch.UnionTag.T2:
				{
					writer.WriteValue(options, value.Value2, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.Fields v) => w.WriteValueEx<Elastic.Clients.Elasticsearch.Fields>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.SingleOrManyFieldsMarker)));
					break;
				}

			default:
				throw new System.InvalidOperationException($"Unrecognized tag value: {value.Tag}");
		}
	}
}

/// <summary>
/// <para>
/// Defines how to fetch a source. Fetching can be disabled entirely, or the source can be filtered.
/// Used as a query parameter along with the <c>_source_includes</c> and <c>_source_excludes</c> parameters.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParamConverter))]
public sealed partial class SourceConfigParam : Elastic.Clients.Elasticsearch.Union<bool, Elastic.Clients.Elasticsearch.Fields>
{
	public SourceConfigParam(bool value) : base(value)
	{
	}

	public SourceConfigParam(Elastic.Clients.Elasticsearch.Fields value) : base(value)
	{
	}

	public static implicit operator Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam(bool value) => new Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam(value);
	public static implicit operator Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam(Elastic.Clients.Elasticsearch.Fields value) => new Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam(value);
}

public readonly partial struct SourceConfigParamFactory<TDocument>
{
	public Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam Fetch(bool value = true)
	{
		return new Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam(value);
	}

	public Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam Fields(Elastic.Clients.Elasticsearch.Fields value)
	{
		return new Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam(value);
	}

	public Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam Fields(params System.Linq.Expressions.Expression<System.Func<TDocument, object?>>[] value)
	{
		return new Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam(value);
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam Build(System.Func<Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParamFactory<TDocument>, Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParamFactory<TDocument>();
		return action.Invoke(builder);
	}
}

public readonly partial struct SourceConfigParamFactory
{
	public Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam Fetch(bool value = true)
	{
		return new Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam(value);
	}

	public Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam Fields(Elastic.Clients.Elasticsearch.Fields value)
	{
		return new Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam(value);
	}

	public Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam Fields<T>(params System.Linq.Expressions.Expression<System.Func<T, object?>>[] value)
	{
		return new Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam(value);
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam Build(System.Func<Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParamFactory, Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParamFactory();
		return action.Invoke(builder);
	}
}
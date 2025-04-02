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

namespace Elastic.Clients.Elasticsearch.Aggregations;

internal sealed partial class BucketsConverter<TBucket> : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Aggregations.Buckets<TBucket>>
{
	public override Elastic.Clients.Elasticsearch.Aggregations.Buckets<TBucket> Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		var selector = static (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => JsonUnionSelector.ByTokenType(ref r, o, Elastic.Clients.Elasticsearch.Serialization.JsonTokenTypes.StartObject, Elastic.Clients.Elasticsearch.Serialization.JsonTokenTypes.StartArray);
		return selector(ref reader, options) switch
		{
			Elastic.Clients.Elasticsearch.UnionTag.T1 => new Elastic.Clients.Elasticsearch.Aggregations.Buckets<TBucket>(reader.ReadValue<System.Collections.Generic.IDictionary<string, TBucket>>(options, static System.Collections.Generic.IDictionary<string, TBucket> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, TBucket>(o, null, null)!)),
			Elastic.Clients.Elasticsearch.UnionTag.T2 => new Elastic.Clients.Elasticsearch.Aggregations.Buckets<TBucket>(reader.ReadValue<System.Collections.Generic.ICollection<TBucket>>(options, static System.Collections.Generic.ICollection<TBucket> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<TBucket>(o, null)!)),
			_ => throw new System.InvalidOperationException($"Failed to select a union variant for type '{nameof(Elastic.Clients.Elasticsearch.Aggregations.Buckets<TBucket>)}")
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Aggregations.Buckets<TBucket> value, System.Text.Json.JsonSerializerOptions options)
	{
		switch (value.Tag)
		{
			case Elastic.Clients.Elasticsearch.UnionTag.T1:
				{
					writer.WriteValue(options, value.Value1, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, TBucket> v) => w.WriteDictionaryValue<string, TBucket>(o, v, null, null));
					break;
				}

			case Elastic.Clients.Elasticsearch.UnionTag.T2:
				{
					writer.WriteValue(options, value.Value2, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<TBucket> v) => w.WriteCollectionValue<TBucket>(o, v, null));
					break;
				}

			default:
				throw new System.InvalidOperationException($"Unrecognized tag value: {value.Tag}");
		}
	}
}

internal sealed partial class BucketsConverterFactory : System.Text.Json.Serialization.JsonConverterFactory
{
	public override bool CanConvert(System.Type typeToConvert)
	{
		return typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(Buckets<>);
	}

	public override System.Text.Json.Serialization.JsonConverter CreateConverter(System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		var args = typeToConvert.GetGenericArguments();
#pragma warning disable IL3050
		var converter = (System.Text.Json.Serialization.JsonConverter)System.Activator.CreateInstance(typeof(BucketsConverter<>).MakeGenericType(args[0]), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public, binder: null, args: null, culture: null)!;
#pragma warning restore IL3050
		return converter;
	}
}

/// <summary>
/// <para>
/// Aggregation buckets. By default they are returned as an array, but if the aggregation has keys configured for
/// the different buckets, the result is a dictionary.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Aggregations.BucketsConverterFactory))]
public sealed partial class Buckets<TBucket> : Elastic.Clients.Elasticsearch.Union<System.Collections.Generic.IDictionary<string, TBucket>, System.Collections.Generic.ICollection<TBucket>>
{
	public Buckets(System.Collections.Generic.IDictionary<string, TBucket> value) : base(value)
	{
	}

	public Buckets(System.Collections.Generic.ICollection<TBucket> value) : base(value)
	{
	}

	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.Buckets<TBucket>(System.Collections.Generic.Dictionary<string, TBucket> value) => new Elastic.Clients.Elasticsearch.Aggregations.Buckets<TBucket>(value);
	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.Buckets<TBucket>(TBucket[] value) => new Elastic.Clients.Elasticsearch.Aggregations.Buckets<TBucket>(value);
}

public readonly partial struct BucketsOfQueryBuilder<TDocument>
{
	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query> Keyed(System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.QueryDsl.Query> value)
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query>(value);
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query> Keyed()
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query>(Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringQuery<TDocument>.Build(null));
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query> Keyed(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringQuery<TDocument>>? action)
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query>(Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringQuery<TDocument>.Build(action));
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query> Array(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query> value)
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query>(value);
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query> Array()
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query>(Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery<TDocument>.Build(null));
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query> Array(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery<TDocument>>? action)
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query>(Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery<TDocument>.Build(action));
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query> Array(params Elastic.Clients.Elasticsearch.QueryDsl.Query[] values)
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query>([.. values]);
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query> Array(params System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.QueryDsl.Query>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>.Build(action));
		}

		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query>(items);
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query> Build(System.Func<Elastic.Clients.Elasticsearch.Aggregations.BucketsOfQueryBuilder<TDocument>, Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Aggregations.BucketsOfQueryBuilder<TDocument>();
		return action.Invoke(builder);
	}
}

public readonly partial struct BucketsOfQueryBuilder
{
	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query> Keyed(System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.QueryDsl.Query> value)
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query>(value);
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query> Keyed()
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query>(Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringQuery.Build(null));
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query> Keyed(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringQuery>? action)
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query>(Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringQuery.Build(action));
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query> Keyed<T>(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringQuery<T>>? action)
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query>(Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringQuery<T>.Build(action));
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query> Array(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query> value)
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query>(value);
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query> Array()
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query>(Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery.Build(null));
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query> Array(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery>? action)
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query>(Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery.Build(action));
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query> Array<T>(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery<T>>? action)
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query>(Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfQuery<T>.Build(action));
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query> Array(params Elastic.Clients.Elasticsearch.QueryDsl.Query[] values)
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query>([.. values]);
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query> Array(params System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.QueryDsl.Query>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor.Build(action));
		}

		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query>(items);
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query> Array<T>(params System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.QueryDsl.Query>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>.Build(action));
		}

		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query>(items);
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query> Build(System.Func<Elastic.Clients.Elasticsearch.Aggregations.BucketsOfQueryBuilder, Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.QueryDsl.Query>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Aggregations.BucketsOfQueryBuilder();
		return action.Invoke(builder);
	}
}

public readonly partial struct BucketsOfApiKeyQueryBuilder<TDocument>
{
	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery> Keyed(System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Security.ApiKeyQuery> value)
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery>(value);
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery> Keyed()
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery>(Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringApiKeyQuery<TDocument>.Build(null));
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery> Keyed(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringApiKeyQuery<TDocument>>? action)
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery>(Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringApiKeyQuery<TDocument>.Build(action));
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery> Array(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery> value)
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery>(value);
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery> Array()
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery>(Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfApiKeyQuery<TDocument>.Build(null));
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery> Array(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfApiKeyQuery<TDocument>>? action)
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery>(Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfApiKeyQuery<TDocument>.Build(action));
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery> Array(params Elastic.Clients.Elasticsearch.Security.ApiKeyQuery[] values)
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery>([.. values]);
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery> Array(params System.Action<Elastic.Clients.Elasticsearch.Security.ApiKeyQueryDescriptor<TDocument>>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Security.ApiKeyQueryDescriptor<TDocument>.Build(action));
		}

		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery>(items);
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery> Build(System.Func<Elastic.Clients.Elasticsearch.Aggregations.BucketsOfApiKeyQueryBuilder<TDocument>, Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Aggregations.BucketsOfApiKeyQueryBuilder<TDocument>();
		return action.Invoke(builder);
	}
}

public readonly partial struct BucketsOfApiKeyQueryBuilder
{
	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery> Keyed(System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Security.ApiKeyQuery> value)
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery>(value);
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery> Keyed()
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery>(Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringApiKeyQuery.Build(null));
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery> Keyed(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringApiKeyQuery>? action)
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery>(Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringApiKeyQuery.Build(action));
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery> Keyed<T>(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringApiKeyQuery<T>>? action)
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery>(Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringApiKeyQuery<T>.Build(action));
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery> Array(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery> value)
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery>(value);
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery> Array()
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery>(Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfApiKeyQuery.Build(null));
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery> Array(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfApiKeyQuery>? action)
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery>(Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfApiKeyQuery.Build(action));
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery> Array<T>(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfApiKeyQuery<T>>? action)
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery>(Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfApiKeyQuery<T>.Build(action));
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery> Array(params Elastic.Clients.Elasticsearch.Security.ApiKeyQuery[] values)
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery>([.. values]);
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery> Array(params System.Action<Elastic.Clients.Elasticsearch.Security.ApiKeyQueryDescriptor>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Security.ApiKeyQueryDescriptor.Build(action));
		}

		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery>(items);
	}

	public Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery> Array<T>(params System.Action<Elastic.Clients.Elasticsearch.Security.ApiKeyQueryDescriptor<T>>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Security.ApiKeyQueryDescriptor<T>.Build(action));
		}

		return new Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery>(items);
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery> Build(System.Func<Elastic.Clients.Elasticsearch.Aggregations.BucketsOfApiKeyQueryBuilder, Elastic.Clients.Elasticsearch.Aggregations.Buckets<Elastic.Clients.Elasticsearch.Security.ApiKeyQuery>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Aggregations.BucketsOfApiKeyQueryBuilder();
		return action.Invoke(builder);
	}
}
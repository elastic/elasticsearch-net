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

internal sealed partial class TermsQueryFieldConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField>
{
	public override Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		var selector = static (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => JsonUnionSelector.ByTokenType(ref r, o, Elastic.Clients.Elasticsearch.Serialization.JsonTokenTypes.StartArray, Elastic.Clients.Elasticsearch.Serialization.JsonTokenTypes.StartObject);
		return selector(ref reader, options) switch
		{
			Elastic.Clients.Elasticsearch.UnionTag.T1 => new Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField(reader.ReadValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>>(options, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.FieldValue>(o, null)!)),
			Elastic.Clients.Elasticsearch.UnionTag.T2 => new Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField(reader.ReadValue<Elastic.Clients.Elasticsearch.QueryDsl.TermsLookup>(options, null)),
			_ => throw new System.InvalidOperationException($"Failed to select a union variant for type '{nameof(Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField)}")
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField value, System.Text.Json.JsonSerializerOptions options)
	{
		switch (value.Tag)
		{
			case Elastic.Clients.Elasticsearch.UnionTag.T1:
				{
					writer.WriteValue(options, value.Value1, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.FieldValue>(o, v, null));
					break;
				}

			case Elastic.Clients.Elasticsearch.UnionTag.T2:
				{
					writer.WriteValue(options, value.Value2, null);
					break;
				}

			default:
				throw new System.InvalidOperationException($"Unrecognized tag value: {value.Tag}");
		}
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryFieldConverter))]
public sealed partial class TermsQueryField : Elastic.Clients.Elasticsearch.Union<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>, Elastic.Clients.Elasticsearch.QueryDsl.TermsLookup>
{
	public TermsQueryField(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue> value) : base(value)
	{
	}

	public TermsQueryField(Elastic.Clients.Elasticsearch.QueryDsl.TermsLookup value) : base(value)
	{
	}

	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField(Elastic.Clients.Elasticsearch.FieldValue[] value) => new Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField(value);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField(Elastic.Clients.Elasticsearch.QueryDsl.TermsLookup value) => new Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField(value);
}

public readonly partial struct TermsQueryFieldFactory<TDocument>
{
	public Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField Value(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue> value)
	{
		return new Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField(value);
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField Value(params Elastic.Clients.Elasticsearch.FieldValue[] values)
	{
		return new Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField([.. values]);
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField Lookup(Elastic.Clients.Elasticsearch.QueryDsl.TermsLookup value)
	{
		return new Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField(value);
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField Lookup(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.TermsLookupDescriptor<TDocument>> action)
	{
		return new Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField(Elastic.Clients.Elasticsearch.QueryDsl.TermsLookupDescriptor<TDocument>.Build(action));
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField Build(System.Func<Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryFieldFactory<TDocument>, Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryFieldFactory<TDocument>();
		return action.Invoke(builder);
	}
}

public readonly partial struct TermsQueryFieldFactory
{
	public Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField Value(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue> value)
	{
		return new Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField(value);
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField Value(params Elastic.Clients.Elasticsearch.FieldValue[] values)
	{
		return new Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField([.. values]);
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField Lookup(Elastic.Clients.Elasticsearch.QueryDsl.TermsLookup value)
	{
		return new Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField(value);
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField Lookup(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.TermsLookupDescriptor> action)
	{
		return new Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField(Elastic.Clients.Elasticsearch.QueryDsl.TermsLookupDescriptor.Build(action));
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField Lookup<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.TermsLookupDescriptor<T>> action)
	{
		return new Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField(Elastic.Clients.Elasticsearch.QueryDsl.TermsLookupDescriptor<T>.Build(action));
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField Build(System.Func<Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryFieldFactory, Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryFieldFactory();
		return action.Invoke(builder);
	}
}
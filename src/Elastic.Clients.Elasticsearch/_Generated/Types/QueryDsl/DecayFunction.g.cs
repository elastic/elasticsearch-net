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

internal sealed partial class IDecayFunctionConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction>
{
	public override Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		return reader.ReadValue<Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunction>(options, null);
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction value, System.Text.Json.JsonSerializerOptions options)
	{
		switch (value)
		{
			case Elastic.Clients.Elasticsearch.QueryDsl.DateDecayFunction v:
				writer.WriteValue(options, v, null);
				break;
			case Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunction v:
				writer.WriteValue(options, v, null);
				break;
			case Elastic.Clients.Elasticsearch.QueryDsl.NumericDecayFunction v:
				writer.WriteValue(options, v, null);
				break;
			case Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunction v:
				writer.WriteValue(options, v, null);
				break;
			default:
				throw new System.Text.Json.JsonException($"Variant '{value.Type}' is not supported for type '{nameof(Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction)}'.");
		}
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunctionConverter))]
public partial interface IDecayFunction
{
	public string Type { get; }
}

public readonly partial struct IDecayFunctionFactory<TDocument>
{
	public Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction Date(Elastic.Clients.Elasticsearch.QueryDsl.DateDecayFunction value)
	{
		return value;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction Date(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.DateDecayFunctionDescriptor<TDocument>> action)
	{
		return Elastic.Clients.Elasticsearch.QueryDsl.DateDecayFunctionDescriptor<TDocument>.Build(action);
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction Geo(Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunction value)
	{
		return value;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction Geo(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunctionDescriptor<TDocument>> action)
	{
		return Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunctionDescriptor<TDocument>.Build(action);
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction Numeric(Elastic.Clients.Elasticsearch.QueryDsl.NumericDecayFunction value)
	{
		return value;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction Numeric(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.NumericDecayFunctionDescriptor<TDocument>> action)
	{
		return Elastic.Clients.Elasticsearch.QueryDsl.NumericDecayFunctionDescriptor<TDocument>.Build(action);
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction Untyped(Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunction value)
	{
		return value;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction Untyped(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunctionDescriptor<TDocument>> action)
	{
		return Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunctionDescriptor<TDocument>.Build(action);
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction Build(System.Func<Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunctionFactory<TDocument>, Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunctionFactory<TDocument>();
		return action.Invoke(builder);
	}
}

public readonly partial struct IDecayFunctionFactory
{
	public Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction Date(Elastic.Clients.Elasticsearch.QueryDsl.DateDecayFunction value)
	{
		return value;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction Date(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.DateDecayFunctionDescriptor> action)
	{
		return Elastic.Clients.Elasticsearch.QueryDsl.DateDecayFunctionDescriptor.Build(action);
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction Date<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.DateDecayFunctionDescriptor<T>> action)
	{
		return Elastic.Clients.Elasticsearch.QueryDsl.DateDecayFunctionDescriptor<T>.Build(action);
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction Geo(Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunction value)
	{
		return value;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction Geo(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunctionDescriptor> action)
	{
		return Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunctionDescriptor.Build(action);
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction Geo<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunctionDescriptor<T>> action)
	{
		return Elastic.Clients.Elasticsearch.QueryDsl.GeoDecayFunctionDescriptor<T>.Build(action);
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction Numeric(Elastic.Clients.Elasticsearch.QueryDsl.NumericDecayFunction value)
	{
		return value;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction Numeric(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.NumericDecayFunctionDescriptor> action)
	{
		return Elastic.Clients.Elasticsearch.QueryDsl.NumericDecayFunctionDescriptor.Build(action);
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction Numeric<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.NumericDecayFunctionDescriptor<T>> action)
	{
		return Elastic.Clients.Elasticsearch.QueryDsl.NumericDecayFunctionDescriptor<T>.Build(action);
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction Untyped(Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunction value)
	{
		return value;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction Untyped(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunctionDescriptor> action)
	{
		return Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunctionDescriptor.Build(action);
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction Untyped<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunctionDescriptor<T>> action)
	{
		return Elastic.Clients.Elasticsearch.QueryDsl.UntypedDecayFunctionDescriptor<T>.Build(action);
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction Build(System.Func<Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunctionFactory, Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunction> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.IDecayFunctionFactory();
		return action.Invoke(builder);
	}
}
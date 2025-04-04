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

internal sealed partial class CategorizeTextAnalyzerConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzer>
{
	public override Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzer Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		var selector = static (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => JsonUnionSelector.ByTokenType(ref r, o, Elastic.Clients.Elasticsearch.Serialization.JsonTokenTypes.String, Elastic.Clients.Elasticsearch.Serialization.JsonTokenTypes.StartObject);
		return selector(ref reader, options) switch
		{
			Elastic.Clients.Elasticsearch.UnionTag.T1 => new Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzer(reader.ReadValue<string>(options, null)),
			Elastic.Clients.Elasticsearch.UnionTag.T2 => new Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzer(reader.ReadValue<Elastic.Clients.Elasticsearch.Aggregations.CustomCategorizeTextAnalyzer>(options, null)),
			_ => throw new System.InvalidOperationException($"Failed to select a union variant for type '{nameof(Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzer)}")
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzer value, System.Text.Json.JsonSerializerOptions options)
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
					writer.WriteValue(options, value.Value2, null);
					break;
				}

			default:
				throw new System.InvalidOperationException($"Unrecognized tag value: {value.Tag}");
		}
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzerConverter))]
public sealed partial class CategorizeTextAnalyzer : Elastic.Clients.Elasticsearch.Union<string, Elastic.Clients.Elasticsearch.Aggregations.CustomCategorizeTextAnalyzer>
{
	public CategorizeTextAnalyzer(string value) : base(value)
	{
	}

	public CategorizeTextAnalyzer(Elastic.Clients.Elasticsearch.Aggregations.CustomCategorizeTextAnalyzer value) : base(value)
	{
	}

	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzer(string value) => new Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzer(value);
	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzer(Elastic.Clients.Elasticsearch.Aggregations.CustomCategorizeTextAnalyzer value) => new Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzer(value);
}

public readonly partial struct CategorizeTextAnalyzerFactory
{
	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzer Builtin(string value)
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzer(value);
	}

	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzer Custom(Elastic.Clients.Elasticsearch.Aggregations.CustomCategorizeTextAnalyzer value)
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzer(value);
	}

	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzer Custom()
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzer(Elastic.Clients.Elasticsearch.Aggregations.CustomCategorizeTextAnalyzerDescriptor.Build(null));
	}

	public Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzer Custom(System.Action<Elastic.Clients.Elasticsearch.Aggregations.CustomCategorizeTextAnalyzerDescriptor>? action)
	{
		return new Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzer(Elastic.Clients.Elasticsearch.Aggregations.CustomCategorizeTextAnalyzerDescriptor.Build(action));
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzer Build(System.Func<Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzerFactory, Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzer> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Aggregations.CategorizeTextAnalyzerFactory();
		return action.Invoke(builder);
	}
}
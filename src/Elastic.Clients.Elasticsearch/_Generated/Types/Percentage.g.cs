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

namespace Elastic.Clients.Elasticsearch;

internal sealed partial class PercentageConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Percentage>
{
	public override Elastic.Clients.Elasticsearch.Percentage Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		var selector = static (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => JsonUnionSelector.ByTokenType(ref r, o, Elastic.Clients.Elasticsearch.Serialization.JsonTokenTypes.String, Elastic.Clients.Elasticsearch.Serialization.JsonTokenTypes.Number);
		return selector(ref reader, options) switch
		{
			Elastic.Clients.Elasticsearch.UnionTag.T1 => new Elastic.Clients.Elasticsearch.Percentage(reader.ReadValue<string>(options, null)),
			Elastic.Clients.Elasticsearch.UnionTag.T2 => new Elastic.Clients.Elasticsearch.Percentage(reader.ReadValue<float>(options, null)),
			_ => throw new System.InvalidOperationException($"Failed to select a union variant for type '{nameof(Elastic.Clients.Elasticsearch.Percentage)}")
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Percentage value, System.Text.Json.JsonSerializerOptions options)
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

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.PercentageConverter))]
public sealed partial class Percentage : Elastic.Clients.Elasticsearch.Union<string, float>
{
	public Percentage(string value) : base(value)
	{
	}

	public Percentage(float value) : base(value)
	{
	}

	public static implicit operator Elastic.Clients.Elasticsearch.Percentage(string value) => new Elastic.Clients.Elasticsearch.Percentage(value);
	public static implicit operator Elastic.Clients.Elasticsearch.Percentage(float value) => new Elastic.Clients.Elasticsearch.Percentage(value);
}

public readonly partial struct PercentageFactory
{
	public Elastic.Clients.Elasticsearch.Percentage Value(string value)
	{
		return new Elastic.Clients.Elasticsearch.Percentage(value);
	}

	public Elastic.Clients.Elasticsearch.Percentage Value(float value)
	{
		return new Elastic.Clients.Elasticsearch.Percentage(value);
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Percentage Build(System.Func<Elastic.Clients.Elasticsearch.PercentageFactory, Elastic.Clients.Elasticsearch.Percentage> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.PercentageFactory();
		return action.Invoke(builder);
	}
}
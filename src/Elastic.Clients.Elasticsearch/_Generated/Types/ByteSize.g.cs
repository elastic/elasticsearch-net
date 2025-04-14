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

internal sealed partial class ByteSizeConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.ByteSize>
{
	public override Elastic.Clients.Elasticsearch.ByteSize Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		var selector = static (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => JsonUnionSelector.ByTokenType(ref r, o, Elastic.Clients.Elasticsearch.Serialization.JsonTokenTypes.Number, Elastic.Clients.Elasticsearch.Serialization.JsonTokenTypes.String);
		return selector(ref reader, options) switch
		{
			Elastic.Clients.Elasticsearch.UnionTag.T1 => new Elastic.Clients.Elasticsearch.ByteSize(reader.ReadValue<long>(options, null)),
			Elastic.Clients.Elasticsearch.UnionTag.T2 => new Elastic.Clients.Elasticsearch.ByteSize(reader.ReadValue<string>(options, null)),
			_ => throw new System.InvalidOperationException($"Failed to select a union variant for type '{nameof(Elastic.Clients.Elasticsearch.ByteSize)}")
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.ByteSize value, System.Text.Json.JsonSerializerOptions options)
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

/// <summary>
/// <para><see href="https://www.elastic.co/docs/reference/elasticsearch/rest-apis/api-conventions#byte-units">Learn more about this API in the Elasticsearch documentation.</see></para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.ByteSizeConverter))]
public sealed partial class ByteSize : Elastic.Clients.Elasticsearch.Union<long, string>
{
	public ByteSize(long value) : base(value)
	{
	}

	public ByteSize(string value) : base(value)
	{
	}

	public static implicit operator Elastic.Clients.Elasticsearch.ByteSize(long value) => new Elastic.Clients.Elasticsearch.ByteSize(value);
	public static implicit operator Elastic.Clients.Elasticsearch.ByteSize(string value) => new Elastic.Clients.Elasticsearch.ByteSize(value);
}

public readonly partial struct ByteSizeFactory
{
	public Elastic.Clients.Elasticsearch.ByteSize Value(long value)
	{
		return new Elastic.Clients.Elasticsearch.ByteSize(value);
	}

	public Elastic.Clients.Elasticsearch.ByteSize Value(string value)
	{
		return new Elastic.Clients.Elasticsearch.ByteSize(value);
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.ByteSize Build(System.Func<Elastic.Clients.Elasticsearch.ByteSizeFactory, Elastic.Clients.Elasticsearch.ByteSize> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.ByteSizeFactory();
		return action.Invoke(builder);
	}
}
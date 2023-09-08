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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Aggregations;

public sealed partial class HdrMethod
{
	/// <summary>
	/// <para>Specifies the resolution of values for the histogram in number of significant digits.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("number_of_significant_value_digits")]
	public int? NumberOfSignificantValueDigits { get; set; }
}

public sealed partial class HdrMethodDescriptor : SerializableDescriptor<HdrMethodDescriptor>
{
	internal HdrMethodDescriptor(Action<HdrMethodDescriptor> configure) => configure.Invoke(this);

	public HdrMethodDescriptor() : base()
	{
	}

	private int? NumberOfSignificantValueDigitsValue { get; set; }

	/// <summary>
	/// <para>Specifies the resolution of values for the histogram in number of significant digits.</para>
	/// </summary>
	public HdrMethodDescriptor NumberOfSignificantValueDigits(int? numberOfSignificantValueDigits)
	{
		NumberOfSignificantValueDigitsValue = numberOfSignificantValueDigits;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (NumberOfSignificantValueDigitsValue.HasValue)
		{
			writer.WritePropertyName("number_of_significant_value_digits");
			writer.WriteNumberValue(NumberOfSignificantValueDigitsValue.Value);
		}

		writer.WriteEndObject();
	}
}
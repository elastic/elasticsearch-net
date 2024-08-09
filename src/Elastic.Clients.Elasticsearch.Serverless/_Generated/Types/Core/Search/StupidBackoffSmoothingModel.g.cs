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

using Elastic.Clients.Elasticsearch.Serverless.Fluent;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.Core.Search;

public sealed partial class StupidBackoffSmoothingModel
{
	/// <summary>
	/// <para>
	/// A constant factor that the lower order n-gram model is discounted by.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("discount")]
	public double Discount { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Serverless.Core.Search.SmoothingModel(StupidBackoffSmoothingModel stupidBackoffSmoothingModel) => Elastic.Clients.Elasticsearch.Serverless.Core.Search.SmoothingModel.StupidBackoff(stupidBackoffSmoothingModel);
}

public sealed partial class StupidBackoffSmoothingModelDescriptor : SerializableDescriptor<StupidBackoffSmoothingModelDescriptor>
{
	internal StupidBackoffSmoothingModelDescriptor(Action<StupidBackoffSmoothingModelDescriptor> configure) => configure.Invoke(this);

	public StupidBackoffSmoothingModelDescriptor() : base()
	{
	}

	private double DiscountValue { get; set; }

	/// <summary>
	/// <para>
	/// A constant factor that the lower order n-gram model is discounted by.
	/// </para>
	/// </summary>
	public StupidBackoffSmoothingModelDescriptor Discount(double discount)
	{
		DiscountValue = discount;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("discount");
		writer.WriteNumberValue(DiscountValue);
		writer.WriteEndObject();
	}
}
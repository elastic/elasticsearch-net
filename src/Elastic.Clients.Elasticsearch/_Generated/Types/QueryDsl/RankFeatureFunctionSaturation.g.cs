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

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.QueryDsl
{
	public partial class RankFeatureFunctionSaturation : QueryDsl.RankFeatureFunction
	{
		[JsonInclude]
		[JsonPropertyName("pivot")]
		public float? Pivot { get; set; }
	}

	public sealed partial class RankFeatureFunctionSaturationDescriptor : DescriptorBase<RankFeatureFunctionSaturationDescriptor>
	{
		internal RankFeatureFunctionSaturationDescriptor(Action<RankFeatureFunctionSaturationDescriptor> configure) => configure.Invoke(this);
		public RankFeatureFunctionSaturationDescriptor() : base()
		{
		}

		private float? PivotValue { get; set; }

		public RankFeatureFunctionSaturationDescriptor Pivot(float? pivot)
		{
			PivotValue = pivot;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (PivotValue.HasValue)
			{
				writer.WritePropertyName("pivot");
				writer.WriteNumberValue(PivotValue.Value);
			}

			writer.WriteEndObject();
		}
	}
}
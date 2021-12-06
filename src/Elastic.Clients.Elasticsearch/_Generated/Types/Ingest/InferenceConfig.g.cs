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
namespace Elastic.Clients.Elasticsearch.Ingest
{
	public partial class InferenceConfig
	{
		[JsonInclude]
		[JsonPropertyName("regression")]
		public Elastic.Clients.Elasticsearch.Ingest.InferenceConfigRegression? Regression { get; set; }
	}

	public sealed partial class InferenceConfigDescriptor : DescriptorBase<InferenceConfigDescriptor>
	{
		public InferenceConfigDescriptor()
		{
		}

		internal InferenceConfigDescriptor(Action<InferenceConfigDescriptor> configure) => configure.Invoke(this);
		internal Elastic.Clients.Elasticsearch.Ingest.InferenceConfigRegression? RegressionValue { get; private set; }

		internal InferenceConfigRegressionDescriptor RegressionDescriptor { get; private set; }

		internal Action<InferenceConfigRegressionDescriptor> RegressionDescriptorAction { get; private set; }

		public InferenceConfigDescriptor Regression(Elastic.Clients.Elasticsearch.Ingest.InferenceConfigRegression? regression)
		{
			RegressionDescriptor = null;
			RegressionDescriptorAction = null;
			return Assign(regression, (a, v) => a.RegressionValue = v);
		}

		public InferenceConfigDescriptor Regression(Ingest.InferenceConfigRegressionDescriptor descriptor)
		{
			RegressionValue = null;
			RegressionDescriptorAction = null;
			return Assign(descriptor, (a, v) => a.RegressionDescriptor = v);
		}

		public InferenceConfigDescriptor Regression(Action<Ingest.InferenceConfigRegressionDescriptor> configure)
		{
			RegressionValue = null;
			RegressionDescriptorAction = null;
			return Assign(configure, (a, v) => a.RegressionDescriptorAction = v);
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (RegressionDescriptor is not null)
			{
				writer.WritePropertyName("regression");
				JsonSerializer.Serialize(writer, RegressionDescriptor, options);
			}
			else if (RegressionDescriptorAction is not null)
			{
				writer.WritePropertyName("regression");
				JsonSerializer.Serialize(writer, new Ingest.InferenceConfigRegressionDescriptor(RegressionDescriptorAction), options);
			}
			else if (RegressionValue is not null)
			{
				writer.WritePropertyName("regression");
				JsonSerializer.Serialize(writer, RegressionValue, options);
			}

			writer.WriteEndObject();
		}
	}
}
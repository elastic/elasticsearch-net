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
namespace Elastic.Clients.Elasticsearch
{
	public sealed partial class LinearInterpolationSmoothingModel
	{
		[JsonInclude]
		[JsonPropertyName("bigram_lambda")]
		public double BigramLambda { get; set; }

		[JsonInclude]
		[JsonPropertyName("trigram_lambda")]
		public double TrigramLambda { get; set; }

		[JsonInclude]
		[JsonPropertyName("unigram_lambda")]
		public double UnigramLambda { get; set; }
	}

	public sealed partial class LinearInterpolationSmoothingModelDescriptor : SerializableDescriptorBase<LinearInterpolationSmoothingModelDescriptor>
	{
		internal LinearInterpolationSmoothingModelDescriptor(Action<LinearInterpolationSmoothingModelDescriptor> configure) => configure.Invoke(this);
		public LinearInterpolationSmoothingModelDescriptor() : base()
		{
		}

		private double BigramLambdaValue { get; set; }

		private double TrigramLambdaValue { get; set; }

		private double UnigramLambdaValue { get; set; }

		public LinearInterpolationSmoothingModelDescriptor BigramLambda(double bigramLambda)
		{
			BigramLambdaValue = bigramLambda;
			return Self;
		}

		public LinearInterpolationSmoothingModelDescriptor TrigramLambda(double trigramLambda)
		{
			TrigramLambdaValue = trigramLambda;
			return Self;
		}

		public LinearInterpolationSmoothingModelDescriptor UnigramLambda(double unigramLambda)
		{
			UnigramLambdaValue = unigramLambda;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("bigram_lambda");
			writer.WriteNumberValue(BigramLambdaValue);
			writer.WritePropertyName("trigram_lambda");
			writer.WriteNumberValue(TrigramLambdaValue);
			writer.WritePropertyName("unigram_lambda");
			writer.WriteNumberValue(UnigramLambdaValue);
			writer.WriteEndObject();
		}
	}
}
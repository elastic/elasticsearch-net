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
namespace Elastic.Clients.Elasticsearch.Ml
{
	public partial class PassThroughInferenceOptions : IInferenceConfigCreateVariant
	{
		[JsonIgnore]
		string IInferenceConfigCreateVariant.InferenceConfigCreateVariantName => "pass_through";
		[JsonInclude]
		[JsonPropertyName("results_field")]
		public string? ResultsField { get; set; }

		[JsonInclude]
		[JsonPropertyName("tokenization")]
		public Elastic.Clients.Elasticsearch.Ml.TokenizationConfigContainer? Tokenization { get; set; }
	}

	public sealed partial class PassThroughInferenceOptionsDescriptor : SerializableDescriptorBase<PassThroughInferenceOptionsDescriptor>
	{
		internal PassThroughInferenceOptionsDescriptor(Action<PassThroughInferenceOptionsDescriptor> configure) => configure.Invoke(this);
		public PassThroughInferenceOptionsDescriptor() : base()
		{
		}

		private string? ResultsFieldValue { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.TokenizationConfigContainer? TokenizationValue { get; set; }

		private TokenizationConfigContainerDescriptor TokenizationDescriptor { get; set; }

		private Action<TokenizationConfigContainerDescriptor> TokenizationDescriptorAction { get; set; }

		public PassThroughInferenceOptionsDescriptor ResultsField(string? resultsField)
		{
			ResultsFieldValue = resultsField;
			return Self;
		}

		public PassThroughInferenceOptionsDescriptor Tokenization(Elastic.Clients.Elasticsearch.Ml.TokenizationConfigContainer? tokenization)
		{
			TokenizationDescriptor = null;
			TokenizationDescriptorAction = null;
			TokenizationValue = tokenization;
			return Self;
		}

		public PassThroughInferenceOptionsDescriptor Tokenization(TokenizationConfigContainerDescriptor descriptor)
		{
			TokenizationValue = null;
			TokenizationDescriptorAction = null;
			TokenizationDescriptor = descriptor;
			return Self;
		}

		public PassThroughInferenceOptionsDescriptor Tokenization(Action<TokenizationConfigContainerDescriptor> configure)
		{
			TokenizationValue = null;
			TokenizationDescriptor = null;
			TokenizationDescriptorAction = configure;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (!string.IsNullOrEmpty(ResultsFieldValue))
			{
				writer.WritePropertyName("results_field");
				writer.WriteStringValue(ResultsFieldValue);
			}

			if (TokenizationDescriptor is not null)
			{
				writer.WritePropertyName("tokenization");
				JsonSerializer.Serialize(writer, TokenizationDescriptor, options);
			}
			else if (TokenizationDescriptorAction is not null)
			{
				writer.WritePropertyName("tokenization");
				JsonSerializer.Serialize(writer, new TokenizationConfigContainerDescriptor(TokenizationDescriptorAction), options);
			}
			else if (TokenizationValue is not null)
			{
				writer.WritePropertyName("tokenization");
				JsonSerializer.Serialize(writer, TokenizationValue, options);
			}

			writer.WriteEndObject();
		}
	}
}
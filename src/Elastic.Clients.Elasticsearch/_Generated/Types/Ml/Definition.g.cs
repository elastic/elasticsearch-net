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
	public partial class Definition
	{
		[JsonInclude]
		[JsonPropertyName("preprocessors")]
		public IEnumerable<Elastic.Clients.Elasticsearch.Ml.Preprocessor>? Preprocessors { get; set; }

		[JsonInclude]
		[JsonPropertyName("trained_model")]
		public Elastic.Clients.Elasticsearch.Ml.TrainedModel TrainedModel { get; set; }
	}

	public sealed partial class DefinitionDescriptor : SerializableDescriptorBase<DefinitionDescriptor>
	{
		internal DefinitionDescriptor(Action<DefinitionDescriptor> configure) => configure.Invoke(this);
		public DefinitionDescriptor() : base()
		{
		}

		private IEnumerable<Elastic.Clients.Elasticsearch.Ml.Preprocessor>? PreprocessorsValue { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.TrainedModel TrainedModelValue { get; set; }

		private TrainedModelDescriptor TrainedModelDescriptor { get; set; }

		private Action<TrainedModelDescriptor> TrainedModelDescriptorAction { get; set; }

		public DefinitionDescriptor Preprocessors(IEnumerable<Elastic.Clients.Elasticsearch.Ml.Preprocessor>? preprocessors)
		{
			PreprocessorsValue = preprocessors;
			return Self;
		}

		public DefinitionDescriptor TrainedModel(Elastic.Clients.Elasticsearch.Ml.TrainedModel trainedModel)
		{
			TrainedModelDescriptor = null;
			TrainedModelDescriptorAction = null;
			TrainedModelValue = trainedModel;
			return Self;
		}

		public DefinitionDescriptor TrainedModel(TrainedModelDescriptor descriptor)
		{
			TrainedModelValue = null;
			TrainedModelDescriptorAction = null;
			TrainedModelDescriptor = descriptor;
			return Self;
		}

		public DefinitionDescriptor TrainedModel(Action<TrainedModelDescriptor> configure)
		{
			TrainedModelValue = null;
			TrainedModelDescriptorAction = null;
			TrainedModelDescriptorAction = configure;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (PreprocessorsValue is not null)
			{
				writer.WritePropertyName("preprocessors");
				JsonSerializer.Serialize(writer, PreprocessorsValue, options);
			}

			if (TrainedModelDescriptor is not null)
			{
				writer.WritePropertyName("trained_model");
				JsonSerializer.Serialize(writer, TrainedModelDescriptor, options);
			}
			else if (TrainedModelDescriptorAction is not null)
			{
				writer.WritePropertyName("trained_model");
				JsonSerializer.Serialize(writer, new TrainedModelDescriptor(TrainedModelDescriptorAction), options);
			}
			else
			{
				writer.WritePropertyName("trained_model");
				JsonSerializer.Serialize(writer, TrainedModelValue, options);
			}

			writer.WriteEndObject();
		}
	}
}
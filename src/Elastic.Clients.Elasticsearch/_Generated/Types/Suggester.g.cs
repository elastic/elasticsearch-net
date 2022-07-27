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
	internal sealed class SuggesterConverter : JsonConverter<Suggester>
	{
		public override Suggester Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException("Unexpected JSON detected.");
			var variant = new Suggester();
			Dictionary<string, Elastic.Clients.Elasticsearch.FieldSuggester> additionalProperties = null;
			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					var property = reader.GetString();
					if (property == "text")
					{
						variant.Text = JsonSerializer.Deserialize<string?>(ref reader, options);
						continue;
					}

					additionalProperties ??= new Dictionary<string, Elastic.Clients.Elasticsearch.FieldSuggester>();
					var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.FieldSuggester>(ref reader, options);
					additionalProperties.Add(property, value);
				}
			}

			variant.Suggesters = additionalProperties;
			return variant;
		}

		public override void Write(Utf8JsonWriter writer, Suggester value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			if (value.Suggesters != null)
			{
				foreach (var additionalProperty in value.Suggesters)
				{
					writer.WritePropertyName(additionalProperty.Key);
					JsonSerializer.Serialize(writer, additionalProperty.Value, options);
				}
			}

			if (!string.IsNullOrEmpty(value.Text))
			{
				writer.WritePropertyName("text");
				writer.WriteStringValue(value.Text);
			}

			writer.WriteEndObject();
		}
	}

	[JsonConverter(typeof(SuggesterConverter))]
	public sealed partial class Suggester
	{
		public Dictionary<string, Elastic.Clients.Elasticsearch.FieldSuggester> Suggesters { get; set; }

		public string? Text { get; set; }
	}

	public sealed partial class SuggesterDescriptor : SerializableDescriptorBase<SuggesterDescriptor>
	{
		internal SuggesterDescriptor(Action<SuggesterDescriptor> configure) => configure.Invoke(this);
		public SuggesterDescriptor() : base()
		{
		}

		private Dictionary<string, Elastic.Clients.Elasticsearch.FieldSuggester> SuggestersValue { get; set; }

		private string? TextValue { get; set; }

		public SuggesterDescriptor Suggesters(Func<FluentDictionary<string, Elastic.Clients.Elasticsearch.FieldSuggester>, FluentDictionary<string, Elastic.Clients.Elasticsearch.FieldSuggester>> selector)
		{
			SuggestersValue = selector?.Invoke(new FluentDictionary<string, Elastic.Clients.Elasticsearch.FieldSuggester>());
			return Self;
		}

		public SuggesterDescriptor Text(string? text)
		{
			TextValue = text;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (!string.IsNullOrEmpty(TextValue))
			{
				writer.WritePropertyName("text");
				writer.WriteStringValue(TextValue);
			}

			if (SuggestersValue != null)
			{
				foreach (var additionalProperty in SuggestersValue)
				{
					writer.WritePropertyName(additionalProperty.Key);
					JsonSerializer.Serialize(writer, additionalProperty.Value, options);
				}
			}

			writer.WriteEndObject();
		}
	}
}
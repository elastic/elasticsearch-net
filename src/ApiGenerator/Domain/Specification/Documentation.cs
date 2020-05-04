// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Newtonsoft.Json;

namespace ApiGenerator.Domain.Specification
{
	[JsonConverter(typeof(DocumentationConverter))]
	public class Documentation
	{
		public string Description { get; set; }

		private string _url;
		public string Url
		{
			get => _url;
			set => _url = value?.Replace("http://", "https://");
		}
	}

	public class DocumentationConverter : JsonConverter
	{
		public override bool CanWrite { get; } = false;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotSupportedException();

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{

			var documentation = new Documentation();

			if (reader.TokenType == JsonToken.String)
			{
				documentation.Url = (string)reader.Value;
				return documentation;
			}

			while (reader.Read())
			{
				if (reader.TokenType == JsonToken.EndObject)
					break;

				var prop = (string)reader.Value;
				switch (prop)
				{
					case "url":
						documentation.Url = reader.ReadAsString();
						break;
					case "description":
						documentation.Description = reader.ReadAsString();
						break;
					default:
						throw new Exception($"Property '{prop}' unexpected in documentation object");
				}
			}
			return documentation;
		}

		public override bool CanConvert(Type objectType) => true;
	}
}

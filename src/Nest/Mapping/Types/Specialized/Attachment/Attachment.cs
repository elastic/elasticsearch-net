using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(AttachmentConverter))]
	public class Attachment
	{
		/// <summary>
		/// The author.
		/// </summary>
		[JsonProperty("author")]
		public string Author { get; set; }

		/// <summary>
		/// The base64 encoded content. Can be explicitly set
		/// </summary>
		[JsonProperty("content")]
		public string Content { get; set; }

		/// <summary>
		/// The length of the content before text extraction.
		/// </summary>
		[JsonProperty("content_length")]
		public long? ContentLength { get; set; }

		/// <summary>
		/// The content type of the attachment. Can be explicitly set
		/// </summary>
		[JsonProperty("content_type")]
		public string ContentType { get; set; }

		/// <summary>
		/// The date of the attachment.
		/// </summary>
		[JsonProperty("date")]
		public DateTime? Date { get; set; }

		/// <summary>
		/// The keywords in the attachment.
		/// </summary>
		[JsonProperty("keywords")]
		public string Keywords { get; set; }

		/// <summary>
		/// The language of the attachment. Can be explicitly set.
		/// </summary>
		[JsonProperty("language")]
		public string Language { get; set; }

		/// <summary>
		/// Detect the language of the attachment. Language detection is
		/// disabled by default.
		/// </summary>
		[JsonProperty("detect_language")]
		public bool? DetectLanguage { get; set; }

		/// <summary>
		/// The name of the attachment. Can be explicitly set
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		/// The title of the attachment.
		/// </summary>
		[JsonProperty("title")]
		public string Title { get; set; }

		/// <summary>
		/// Determines how many characters are extracted when indexing the content.
		/// By default, 100000 characters are extracted when indexing the content.
		/// -1 can be set to extract all text, but note that all the text needs to be
		/// allowed to be represented in memory
		/// </summary>
		[JsonProperty("indexed_chars")]
		public long? IndexedCharacters { get; set; }

		/// <summary>
		/// Whether the attachment contains explicit metadata in addition to the
		/// content. Used at indexing time to determine the serialized form of the
		/// attachment.
		/// </summary>
		[JsonIgnore]
		public bool ContainsMetadata => !Name.IsNullOrEmpty() ||
		                                !ContentType.IsNullOrEmpty() ||
		                                !Language.IsNullOrEmpty() ||
										DetectLanguage.HasValue ||
										IndexedCharacters.HasValue;
	}

	internal class AttachmentConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var attachment = (Attachment)value;
			if (!attachment.ContainsMetadata)
			{
				writer.WriteValue(attachment.Content);
			}
			else
			{
				writer.WriteStartObject();
				writer.WritePropertyName("_content");
				writer.WriteValue(attachment.Content);

				if (!string.IsNullOrEmpty(attachment.Name))
				{
					writer.WritePropertyName("_name");
					writer.WriteValue(attachment.Name);
				}

				if (!string.IsNullOrEmpty(attachment.ContentType))
				{
					writer.WritePropertyName("_content_type");
					writer.WriteValue(attachment.ContentType);
				}

				if (!string.IsNullOrEmpty(attachment.Language))
				{
					writer.WritePropertyName("_language");
					writer.WriteValue(attachment.Language);
				}

				if (attachment.DetectLanguage.HasValue)
				{
					writer.WritePropertyName("_detect_language");
					writer.WriteValue(attachment.DetectLanguage.Value);
				}

				if (attachment.IndexedCharacters.HasValue)
				{
					writer.WritePropertyName("_indexed_chars");
					writer.WriteValue(attachment.IndexedCharacters.Value);
				}

				writer.WriteEndObject();
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.String)
			{
				return new Attachment { Content = (string)reader.Value };
			}

			if (reader.TokenType == JsonToken.StartObject)
			{
				var attachment = new Attachment();
				while (reader.Read())
				{
					if (reader.TokenType == JsonToken.PropertyName)
					{
						var propertyName = (string)reader.Value;
						switch (propertyName)
						{
							case "_content":
								attachment.Content = reader.ReadAsString();
								break;
							case "_name":
								attachment.Name = reader.ReadAsString();
								break;
							case "_content_type":
								attachment.ContentType = reader.ReadAsString();
								break;
							case "_language":
								attachment.Language = reader.ReadAsString();
								break;
							case "_detect_language":
								attachment.DetectLanguage = reader.ReadAsBoolean();
								break;
							case "_indexed_chars":
								reader.Read();
								attachment.IndexedCharacters = (long?)reader.Value;
								break;
						}
					}
					if (reader.TokenType == JsonToken.EndObject)
					{
						break;
					}
				}
				return attachment;
			}
			return null;
		}

		public override bool CanConvert(Type objectType) => objectType == typeof(Attachment);
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;
namespace Nest
{
	/// <summary>
	/// An attachment indexed with an ingest pipeline using the ingest-attachment plugin.
	/// Convenience class for working with attachment fields.
	/// </summary>
	[JsonFormatter(typeof(AttachmentFormatter))]
	public class Attachment
	{
		/// <summary>
		/// The author
		/// </summary>
		[DataMember(Name = "author")]
		public string Author { get; set; }

		/// <summary>
		/// Whether the attachment contains explicit metadata in addition to the
		/// content. Used at indexing time to determine the serialized form of the
		/// attachment.
		/// </summary>
		[IgnoreDataMember]
		[Ignore]
		public bool ContainsMetadata =>
			!Author.IsNullOrEmpty() ||
			ContentLength.HasValue ||
			!ContentType.IsNullOrEmpty() ||
			Date.HasValue ||
			DetectLanguage.HasValue ||
			IndexedCharacters.HasValue ||
			!Keywords.IsNullOrEmpty() ||
			!Language.IsNullOrEmpty() ||
			!Name.IsNullOrEmpty() ||
			!Title.IsNullOrEmpty();

		/// <summary>
		/// The base64 encoded content. Can be explicitly set
		/// </summary>
		[DataMember(Name = "content")]
		public string Content { get; set; }

		/// <summary>
		/// The length of the content before text extraction.
		/// </summary>
		[DataMember(Name = "content_length")]
		public long? ContentLength { get; set; }

		/// <summary>
		/// The content type of the attachment. Can be explicitly set
		/// </summary>
		[DataMember(Name = "content_type")]
		public string ContentType { get; set; }

		/// <summary>
		/// The date of the attachment.
		/// </summary>
		[DataMember(Name = "date")]
		public DateTime? Date { get; set; }

		/// <summary>
		/// Detect the language of the attachment. Language detection is
		/// disabled by default.
		/// </summary>
		[DataMember(Name = "detect_language")]
		public bool? DetectLanguage { get; set; }

		/// <summary>
		/// Determines how many characters are extracted when indexing the content.
		/// By default, 100000 characters are extracted when indexing the content.
		/// -1 can be set to extract all text, but note that all the text needs to be
		/// allowed to be represented in memory
		/// </summary>
		[DataMember(Name = "indexed_chars")]
		public long? IndexedCharacters { get; set; }

		/// <summary>
		/// The keywords in the attachment.
		/// </summary>
		[DataMember(Name = "keywords")]
		public string Keywords { get; set; }

		/// <summary>
		/// The language of the attachment. Can be explicitly set.
		/// </summary>
		[DataMember(Name = "language")]
		public string Language { get; set; }

		/// <summary>
		/// The name of the attachment. Can be explicitly set
		/// </summary>
		[DataMember(Name = "name")]
		public string Name { get; set; }

		/// <summary>
		/// The title of the attachment.
		/// </summary>
		[DataMember(Name = "title")]
		public string Title { get; set; }
	}

	internal class AttachmentFormatter : IJsonFormatter<Attachment>
	{
		private static readonly AutomataDictionary AutomataDictionary = new AutomataDictionary
		{
			{ "_content", 0 },
			{ "content", 0 },
			{ "_name", 1 },
			{ "name", 1 },
			{ "author", 2 },
			{ "keywords", 3 },
			{ "date", 4 },
			{ "_content_type", 5 },
			{ "content_type", 5 },
			{ "_content_length", 6 },
			{ "content_length", 6 },
			{ "contentlength", 6 },
			{ "_language", 7 },
			{ "language", 7 },
			{ "_detect_language", 8 },
			{ "detect_language", 8 },
			{ "_indexed_chars", 9 },
			{ "indexed_chars", 9 },
			{ "title", 10 },
		};

		public Attachment Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();

			if (token == JsonToken.String)
				return new Attachment { Content = reader.ReadString() };

			if (token == JsonToken.BeginObject)
			{
				var attachment = new Attachment();
				var count = 0;
				while (reader.ReadIsInObject(ref count))
				{
					var propertyName = reader.ReadPropertyNameSegmentRaw();
					if (AutomataDictionary.TryGetValue(propertyName, out var value))
					{
						switch (value)
						{
							case 0:
								attachment.Content = reader.ReadString();
								break;
							case 1:
								attachment.Name = reader.ReadString();
								break;
							case 2:
								attachment.Author = reader.ReadString();
								break;
							case 3:
								attachment.Keywords = reader.ReadString();
								break;
							case 4:
								attachment.Date = formatterResolver.GetFormatter<DateTime?>()
									.Deserialize(ref reader, formatterResolver);
								break;
							case 5:
								attachment.ContentType = reader.ReadString();
								break;
							case 6:
								attachment.ContentLength = reader.ReadNullableLong();
								break;
							case 7:
								attachment.Language = reader.ReadString();
								break;
							case 8:
								attachment.DetectLanguage = reader.ReadNullableBoolean();
								break;
							case 9:
								attachment.IndexedCharacters = reader.ReadNullableLong();
								break;
							case 10:
								attachment.Title = reader.ReadString();
								break;
						}
					}
				}

				return attachment;
			}

			return null;
		}

		public void Serialize(ref JsonWriter writer, Attachment value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			if (!value.ContainsMetadata)
				writer.WriteString(value.Content);
			else
			{
				writer.WriteBeginObject();
				writer.WritePropertyName("content");
				writer.WriteString(value.Content);

				if (!string.IsNullOrEmpty(value.Author))
				{
					writer.WriteValueSeparator();
					writer.WritePropertyName("author");
					writer.WriteString(value.Author);
				}

				if (value.ContentLength.HasValue)
				{
					writer.WriteValueSeparator();
					writer.WritePropertyName("content_length");
					writer.WriteInt64(value.ContentLength.Value);
				}

				if (!string.IsNullOrEmpty(value.ContentType))
				{
					writer.WriteValueSeparator();
					writer.WritePropertyName("content_type");
					writer.WriteString(value.ContentType);
				}

				if (value.Date.HasValue)
				{
					writer.WriteValueSeparator();
					writer.WritePropertyName("date");
					formatterResolver.GetFormatter<DateTime?>().Serialize(ref writer, value.Date, formatterResolver);
				}

				if (value.DetectLanguage.HasValue)
				{
					writer.WriteValueSeparator();
					writer.WritePropertyName("detect_language");
					writer.WriteBoolean(value.DetectLanguage.Value);
				}

				if (value.IndexedCharacters.HasValue)
				{
					writer.WriteValueSeparator();
					writer.WritePropertyName("indexed_chars");
					writer.WriteInt64(value.IndexedCharacters.Value);
				}

				if (!string.IsNullOrEmpty(value.Keywords))
				{
					writer.WriteValueSeparator();
					writer.WritePropertyName("keywords");
					writer.WriteString(value.Keywords);
				}

				if (!string.IsNullOrEmpty(value.Language))
				{
					writer.WriteValueSeparator();
					writer.WritePropertyName("language");
					writer.WriteString(value.Language);
				}

				if (!string.IsNullOrEmpty(value.Name))
				{
					writer.WriteValueSeparator();
					writer.WritePropertyName("name");
					writer.WriteString(value.Name);
				}

				if (!string.IsNullOrEmpty(value.Title))
				{
					writer.WriteValueSeparator();
					writer.WritePropertyName("title");
					writer.WriteString(value.Title);
				}

				writer.WriteEndObject();
			}
		}
	}
}

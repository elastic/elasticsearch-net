// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections.Generic;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Internal;


namespace Nest
{
	[JsonFormatter(typeof(EmailAttachmentsFormatter))]
	public interface IEmailAttachments : IIsADictionary<string, IEmailAttachment> { }

	public class EmailAttachments : IsADictionaryBase<string, IEmailAttachment>, IEmailAttachments
	{
		public EmailAttachments() { }

		public EmailAttachments(IDictionary<string, IEmailAttachment> attachments) : base(attachments) { }

		public void Add(string name, IEmailAttachment attachment) => BackingDictionary.Add(name, attachment);
	}

	public class EmailAttachmentsDescriptor : DescriptorPromiseBase<EmailAttachmentsDescriptor, IEmailAttachments>
	{
		public EmailAttachmentsDescriptor() : base(new EmailAttachments()) { }

		public EmailAttachmentsDescriptor HttpAttachment(string name, Func<HttpAttachmentDescriptor, IHttpAttachment> selector)
		{
			PromisedValue.Add(name, selector?.Invoke(new HttpAttachmentDescriptor()));
			return this;
		}

		public EmailAttachmentsDescriptor DataAttachment(string name, Func<DataAttachmentDescriptor, IDataAttachment> selector)
		{
			PromisedValue.Add(name, selector?.Invoke(new DataAttachmentDescriptor()));
			return this;
		}
	}

	public interface IEmailAttachment { }

	internal class EmailAttachmentsFormatter : IJsonFormatter<IEmailAttachments>
	{
		private static readonly AutomataDictionary AttachmentType = new AutomataDictionary
		{
			{ "http", 0 },
			{ "data", 1 }
		};

		public IEmailAttachments Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
				return null;

			var attachments = new Dictionary<string, IEmailAttachment>();

			var count = 0;
			while (reader.ReadIsInObject(ref count))
			{
				var name = reader.ReadPropertyName();
				var innerCount = 0;
				while (reader.ReadIsInObject(ref innerCount))
				{
					if (AttachmentType.TryGetValue(reader.ReadPropertyNameSegmentRaw(), out var value))
					{
						IEmailAttachment attachment;
						switch (value)
						{
							case 0:
								attachment = formatterResolver.GetFormatter<HttpAttachment>()
									.Deserialize(ref reader, formatterResolver);
								attachments.Add(name, attachment);
								break;
							case 1:
								attachment = formatterResolver.GetFormatter<DataAttachment>()
									.Deserialize(ref reader, formatterResolver);
								attachments.Add(name, attachment);
								break;
						}
					}
				}
			}

			return new EmailAttachments(attachments);
		}

		public void Serialize(ref JsonWriter writer, IEmailAttachments value, IJsonFormatterResolver formatterResolver)
		{
			writer.WriteBeginObject();
			var attachments = (IDictionary<string, IEmailAttachment>)value;
			if (attachments != null)
			{
				var count = 0;

				foreach (var attachment in attachments)
				{
					if (count > 0)
						writer.WriteValueSeparator();

					writer.WritePropertyName(attachment.Key);
					writer.WriteBeginObject();

					var emailAttachment = attachment.Value;
					switch (emailAttachment)
					{
						case IHttpAttachment http:
							writer.WritePropertyName("http");
							var httpFormatter = formatterResolver.GetFormatter<IHttpAttachment>();
							httpFormatter.Serialize(ref writer, http, formatterResolver);
							break;
						case IDataAttachment data:
							writer.WritePropertyName("data");
							var dataFormatter = formatterResolver.GetFormatter<IDataAttachment>();
							dataFormatter.Serialize(ref writer, data, formatterResolver);
							break;
						default:
							throw new ArgumentException($"{emailAttachment.GetType().FullName} is not a supported email attachment type");
					}

					writer.WriteEndObject();

					count++;
				}
			}
			writer.WriteEndObject();
		}
	}
}
